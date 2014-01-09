using System;
using System.Collections.Generic;
using System.IO;

using C3D.IO;

namespace C3D
{
    /// <summary>
    /// C3D文件读取器
    /// </summary>
    public sealed class C3DReader : IDisposable
    {
        #region 字段
        private C3DBinaryReader _reader;
        private Byte _firstParameterBlockIndex;
        private C3DProcessorType _processorType;
        private Int32 _currentFrameIndex;//-1表示应该Seek并重新开始读取
        #endregion

        #region 属性
        /// <summary>
        /// 获取创建C3D文件的处理器类型
        /// </summary>
        public C3DProcessorType CreateProcessorType
        {
            get { return this._processorType; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 从流中初始化C3D文件读取器
        /// </summary>
        /// <param name="stream">流</param>
        /// <exception cref="C3DException">文件格式错误</exception>
        public C3DReader(Stream stream)
        {
            this._reader = new C3DBinaryReader(stream);
            this._currentFrameIndex = -1;

            this._firstParameterBlockIndex = this._reader.ReadByte();
            Byte signature = this._reader.ReadByte();

            if (signature != C3DConstants.FILE_SIGNATURE)
            {
                throw new C3DException("This is not C3D file.");
            }

            Int32 paramsStartPosition = (this._firstParameterBlockIndex - 1) * C3DConstants.FILE_SECTION_SIZE;
            this._reader.BaseStream.Seek(paramsStartPosition + 3, SeekOrigin.Begin);//跳过signature和paramsBlocksCount
            this._processorType = (C3DProcessorType)this._reader.ReadByte();

            this._reader.SetProcessorType(this._processorType);
            this._reader.BaseStream.Seek(0, SeekOrigin.Begin);
        }
        #endregion

        #region 方法
        #region ReadHeader
        /// <summary>
        /// 读取C3D文件头
        /// </summary>
        /// <returns>C3D文件头</returns>
        public C3DHeader ReadHeader()
        {
            this._reader.BaseStream.Seek(0, SeekOrigin.Begin);

            Byte[] headerData = this._reader.ReadBytes(C3DConstants.FILE_SECTION_SIZE);
            C3DHeader header = new C3DHeader(this._processorType, headerData);

            return header;
        }
        #endregion

        #region ReadParameters
        /// <summary>
        /// 读取C3D文件参数
        /// </summary>
        /// <returns>C3D文件参数集合</returns>
        public C3DParameterDictionary ReadParameters()
        {
            Dictionary<Int32, C3DParameterGroup> groups = new Dictionary<Int32, C3DParameterGroup>();
            Dictionary<Int32, List<C3DParameter>> parameters = new Dictionary<Int32, List<C3DParameter>>();

            Int32 startPosition = (this._firstParameterBlockIndex - 1) * C3DConstants.FILE_SECTION_SIZE;
            this._reader.BaseStream.Seek(startPosition + 2, SeekOrigin.Begin);//跳过前2个字节

            Byte paramsBlocksCount = this._reader.ReadByte();//Parameter所占Block数量
            Byte processorType = this._reader.ReadByte();

            Int64 endPosition = startPosition + paramsBlocksCount * C3DConstants.FILE_SECTION_SIZE;
            Int16 nextPosition = 0;

            do
            {
                SByte nameLength = this._reader.ReadSByte();//Byte 1
                Boolean isLocked = nameLength < 0;
                nameLength = Math.Abs(nameLength);
                
                SByte id = this._reader.ReadSByte();//Byte 2
                Byte[] name = this._reader.ReadBytes(nameLength);//Byte 3

                nextPosition = this._reader.ReadInt16();//Byte 3 + n
                Byte[] lastData = (nextPosition > 0 ? this._reader.ReadBytes(nextPosition - 2) : null);//nextPosition占2字节

                if (id > 0)//C3DParameter
                {
                    C3DParameter parameter = new C3DParameter(this._processorType, id, name, isLocked, lastData);

                    List<C3DParameter> list = null;
                    if (!parameters.TryGetValue(parameter.ID, out list))
                    {
                        list = new List<C3DParameter>();
                    }

                    list.Add(parameter);
                    parameters[parameter.ID] = list;
                }
                else//C3DParameterGroup
                {
                    C3DParameterGroup group = new C3DParameterGroup(id, name, isLocked, lastData);
                    groups[-group.ID] = group;
                }
            }
            while (nextPosition > 0 && this._reader.BaseStream.Position < endPosition);

            return C3DParameterDictionary.CreateParameterDictionaryFromParameterList(groups, parameters);
        }
        #endregion

        #region ReadNextFrame
        /// <summary>
        /// 读取C3D下一帧
        /// </summary>
        /// <param name="dictionary">C3D参数字典</param>
        /// <returns>C3D帧数据</returns>
        public C3DFrame ReadNextFrame(C3DParameterDictionary dictionary)
        {
            return this.ReadNextFrame(C3DParameterCache.CreateCache(dictionary));
        }

        /// <summary>
        /// 读取C3D下一帧
        /// </summary>
        /// <param name="cache">C3D参数缓存</param>
        /// <exception cref="C3DException">帧数据读取错误</exception>
        /// <returns>C3D帧数据</returns>
        public C3DFrame ReadNextFrame(C3DParameterCache cache)
        {
            if (cache.FirstDataBlockPosition < 0)
            {
                return null;
            }

            if (this._currentFrameIndex >= cache.FrameCount)
            {
                this._currentFrameIndex = -1;
                return null;
            }

            if (this._currentFrameIndex < 0)
            {
                this._reader.BaseStream.Seek(cache.FirstDataBlockPosition, SeekOrigin.Begin);
                this._currentFrameIndex = 0;
            }

            if (this._reader.BaseStream.Position >= this._reader.BaseStream.Length)
            {
                this._currentFrameIndex = -1;
                return null;
            }

            C3DFrame data = null;

            try
            {
                data = cache.ScaleFactor < 0 ? this.ReadNextFloatFrame(cache) : this.ReadNextIntFrame(cache);
                this._currentFrameIndex++;

                return data;
            }
            catch (Exception ex)
            {
                throw new C3DException("The frame data has broken.", ex);
            }
        }

        /// <summary>
        /// 读取C3D下一浮点帧
        /// </summary>
        /// <param name="cache">C3D参数缓存</param>
        /// <returns>C3D帧数据</returns>
        private C3DFrame ReadNextFloatFrame(C3DParameterCache cache)
        {
            C3DPoint3DData[] pointDatas = new C3DPoint3DData[cache.PointCount];
            C3DAnalogSamples[] analogSamples = new C3DAnalogSamples[cache.AnalogChannelCount];

            for (Int32 i = 0; i < pointDatas.Length; i++)
            {
                pointDatas[i] = new C3DPoint3DData(this._reader.ReadSingle(), this._reader.ReadSingle(), this._reader.ReadSingle(), this._reader.ReadSingle(), cache.ScaleFactor);
            }

            for (Int32 i = 0; i < analogSamples.Length; i++)
            {
                analogSamples[i] = new C3DAnalogSamples(cache.AnalogSamplesPerFrame);
            }

            for (Int32 j = 0; j < cache.AnalogSamplesPerFrame; j++)
            {
                for (Int32 i = 0; i < cache.AnalogChannelCount; i++)
                {
                    Single data = this._reader.ReadSingle();
                    analogSamples[i][j] = (data - ((cache.AnalogZeroOffset != null && cache.AnalogZeroOffset.Length > i) ? cache.AnalogZeroOffset[i] : (Int16)0))
                        * cache.AnalogGeneralScale * (cache.AnalogChannelScale != null && cache.AnalogChannelScale.Length > i ? cache.AnalogChannelScale[i] : 1.0F);
                }
            }

            return new C3DFrame(pointDatas, analogSamples);
        }

        /// <summary>
        /// 读取C3D下一整型帧
        /// </summary>
        /// <param name="cache">C3D参数缓存</param>
        /// <returns>C3D帧数据</returns>
        private C3DFrame ReadNextIntFrame(C3DParameterCache cache)
        {
            C3DPoint3DData[] pointDatas = new C3DPoint3DData[cache.PointCount];
            C3DAnalogSamples[] analogSamples = new C3DAnalogSamples[cache.AnalogChannelCount];

            for (Int32 i = 0; i < pointDatas.Length; i++)
            {
                pointDatas[i] = new C3DPoint3DData(this._reader.ReadInt16(), this._reader.ReadInt16(), this._reader.ReadInt16(), this._reader.ReadInt16(), cache.ScaleFactor);
            }

            for (Int32 i = 0; i < analogSamples.Length; i++)
            {
                analogSamples[i] = new C3DAnalogSamples(cache.AnalogSamplesPerFrame);
            }

            for (Int32 j = 0; j < cache.AnalogSamplesPerFrame; j++)
            {
                for (Int32 i = 0; i < cache.AnalogChannelCount; i++)
                {
                    Int16 data = this._reader.ReadInt16();
                    analogSamples[i][j] = (data - ((cache.AnalogZeroOffset != null && cache.AnalogZeroOffset.Length > i) ? cache.AnalogZeroOffset[i] : (Int16)0))
                        * cache.AnalogGeneralScale * (cache.AnalogChannelScale != null && cache.AnalogChannelScale.Length > i ? cache.AnalogChannelScale[i] : 1.0F);
                }
            }

            return new C3DFrame(pointDatas, analogSamples);
        }
        #endregion

        #region Close
        /// <summary>
        /// 关闭C3D文件读取器和基础流
        /// </summary>
        public void Close()
        {
            this.Dispose(true);
        }
        #endregion
        #endregion

        #region Dispose
        private void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                if (this._reader != null)
                {
                    this._reader.Close();
                }
            }
        }

        /// <summary>
        /// 释放C3D文件读取器所占用的资源
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }
        #endregion
    }
}