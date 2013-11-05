using System;
using System.Collections.Generic;
using System.IO;

using C3D.IO;

namespace C3D
{
    /// <summary>
    /// C3D文件写入器
    /// </summary>
    public sealed class C3DWriter : IDisposable
    {
        #region 字段
        private C3DBinaryWriter _writer;
        private Int64 _dataStartOffset;
        private Int16 _newDataStartBlockIndex;
        #endregion

        #region 构造方法
        /// <summary>
        /// 从流中初始化C3D文件写入器
        /// </summary>
        /// <param name="stream">流</param>
        public C3DWriter(Stream stream)
        {
            this._writer = new C3DBinaryWriter(stream);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 写入C3D文件
        /// </summary>
        /// <param name="file">C3D文件</param>
        public void WriteC3DFile(C3DFile file)
        {
            this._dataStartOffset = 0;
            this._newDataStartBlockIndex = 0;

            this._writer.Write(new Byte[C3DFile.SECTION_SIZE]);
            this.WriteParameters(file.Header.FirstParameterSectionID, file.Parameters);

            C3DParameterCache paramCache = C3DParameterCache.CreateCache(file);
            this.WriteFrameCollection(paramCache, file.AllFrames);

            this.UpdateHeaderAndParameters(file);
            this.WriteHeader(file.Header);
        }

        /// <summary>
        /// 写入数据到文件
        /// </summary>
        public void Flush()
        {
            this._writer.Flush();
        }

        /// <summary>
        /// 关闭C3D文件写入器和基础流
        /// </summary>
        public void Close()
        {
            this.Dispose(true);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 写入C3D文件头
        /// </summary>
        /// <param name="header">C3D文件头</param>
        private void WriteHeader(C3DHeader header)
        {
            this._writer.Seek(0, SeekOrigin.Begin);
            this._writer.Write(header.ToArray());
        }

        /// <summary>
        /// 写入C3D文件参数字典
        /// </summary>
        /// <param name="firstParameterBlockIndex">参数第一块索引(从开始计数)</param>
        /// <param name="dictionary">C3D文件参数字典</param>
        private void WriteParameters(Byte firstParameterBlockIndex, C3DParameterDictionary dictionary)
        {
            Int32 startPosition = (firstParameterBlockIndex - 1) * C3DFile.SECTION_SIZE;
            this._writer.Seek(startPosition, SeekOrigin.Begin);
            this._writer.Write(C3DParameterDictionary.FIRST_PARAM_BLOCK);
            this._writer.Write(C3DParameterDictionary.SIGNATURE);
            this._writer.Write((Byte)0);//Parameter所占Block数量占位
            this._writer.Write((Byte)C3DFile.DEFAULT_PROCESSOR_TYPE);

            foreach (C3DParameterGroup group in dictionary)
            {
                if (group.ID < 0)
                {
                    this.WriteParameter(null, group, false);

                    foreach (C3DParameter param in group)
                    {
                        this.WriteParameter(group, param, false);
                    }
                }
            }

            C3DParameterGroup lastGroup = new C3DParameterGroup(0, "", "");
            this.WriteParameter(null, lastGroup, true);

            this._newDataStartBlockIndex = (Int16)((this._writer.BaseStream.Position + C3DFile.SECTION_SIZE) / C3DFile.SECTION_SIZE + 1);
            this._writer.Write(new Byte[(this._newDataStartBlockIndex - 1) * C3DFile.SECTION_SIZE - this._writer.BaseStream.Position]);

            this._writer.Seek(startPosition + 2, SeekOrigin.Begin);
            this._writer.Write((Byte)(this._newDataStartBlockIndex - 2));//减去Header所占第1个Block
        }

        /// <summary>
        /// 写入C3D文件参数
        /// </summary>
        /// <param name="parent">C3D父参数组</param>
        /// <param name="param">C3D参数</param>
        /// <param name="isLast">是否是最后一个</param>
        private void WriteParameter(C3DParameterGroup parent, AbstractC3DParameter param, Boolean isLast)
        {
            this._writer.Write((SByte)(param.IsLocked ? -param.Name.Length : param.Name.Length));
            this._writer.Write(param.ID);
            this._writer.Write(param.GetNameByteArray());

            Byte[] lastData = (param.ID > 0 ? param.GetLastDataArrayWithoutDescrption() : null);

            Int16 nextPosition = (Int16)(isLast ? 0 :
                (2 + (lastData == null ? 0 : lastData.Length) + //nextPosition占2字节
                1 + (String.IsNullOrEmpty(param.Description) ? 0 : param.Description.Length)));//descrptionLength占1字节
            this._writer.Write(nextPosition);

            if (parent != null && String.Equals(parent.Name, "POINT", StringComparison.OrdinalIgnoreCase))
            {
                if (String.Equals(param.Name, "DATA_START", StringComparison.OrdinalIgnoreCase))
                {
                    this._dataStartOffset = this._writer.BaseStream.Position;
                }
            }

            if (lastData != null)
            {
                this._writer.Write(lastData);
            }

            this._writer.Write((Byte)param.Description.Length);
            this._writer.Write(param.GetDescriptionByteArray());
        }

        /// <summary>
        /// 写入C3D帧集合
        /// </summary>
        /// <param name="cache">C3D参数缓存</param>
        /// <param name="frameCollection">C3D帧集合</param>
        private void WriteFrameCollection(C3DParameterCache cache, C3DFrameCollection frameCollection)
        {
            Int32 startPosition = (this._newDataStartBlockIndex - 1) * C3DFile.SECTION_SIZE;
            this._writer.Seek(startPosition, SeekOrigin.Begin);

            for (Int32 i = 0; i < frameCollection.Count; i++)
            {
                if (cache.ScaleFactor < 0)
                {
                    this.WriteFloatFrame(cache, frameCollection[i]);
                }
                else
                {
                    this.WriteIntFrame(cache, frameCollection[i]);
                }
            }

            Int16 finalIndex = (Int16)((this._writer.BaseStream.Position + C3DFile.SECTION_SIZE) / C3DFile.SECTION_SIZE + 1);
            this._writer.Write(new Byte[(finalIndex - 1) * C3DFile.SECTION_SIZE - this._writer.BaseStream.Position]);
        }

        /// <summary>
        /// 写入浮点帧
        /// </summary>
        /// <param name="cache">C3D参数缓存</param>
        /// <param name="frame">C3D帧数据</param>
        private void WriteFloatFrame(C3DParameterCache cache, C3DFrame frame)
        {
            if (frame.Point3Ds != null)
            {
                for (Int32 i = 0; i < frame.Point3Ds.Length; i++)
                {
                    this._writer.Write(frame.Point3Ds[i].X);
                    this._writer.Write(frame.Point3Ds[i].Y);
                    this._writer.Write(frame.Point3Ds[i].Z);
                    this._writer.Write(frame.Point3Ds[i].GetFloatLastPart(cache.ScaleFactor));
                }
            }

            if (frame.AnalogSamples != null)
            {
                for (Int32 j = 0; j < cache.AnalogSamplesPerFrame; j++)
                {
                    for (Int32 i = 0; i < cache.AnalogChannelCount; i++)
                    {
                        Single data = frame.AnalogSamples[i][j] / cache.AnalogGeneralScale / (cache.AnalogChannelScale != null && cache.AnalogChannelScale.Length > 0 ? cache.AnalogChannelScale[i] : 1.0F)
                             + ((cache.AnalogZeroOffset != null && cache.AnalogZeroOffset.Length > 0) ? cache.AnalogZeroOffset[i] : (Int16)0);

                        this._writer.Write(data);
                    }
                }
            }
        }

        /// <summary>
        /// 写入整型帧
        /// </summary>
        /// <param name="cache">C3D参数缓存</param>
        /// <param name="frame">C3D帧数据</param>
        private void WriteIntFrame(C3DParameterCache cache, C3DFrame frame)
        {
            if (frame.Point3Ds != null)
            {
                for (Int32 i = 0; i < frame.Point3Ds.Length; i++)
                {
                    this._writer.Write((Int16)Math.Round(frame.Point3Ds[i].X / cache.ScaleFactor, MidpointRounding.AwayFromZero));
                    this._writer.Write((Int16)Math.Round(frame.Point3Ds[i].Y / cache.ScaleFactor, MidpointRounding.AwayFromZero));
                    this._writer.Write((Int16)Math.Round(frame.Point3Ds[i].Z / cache.ScaleFactor, MidpointRounding.AwayFromZero));
                    this._writer.Write(frame.Point3Ds[i].GetIntLastPart(cache.ScaleFactor));
                }
            }

            if (frame.AnalogSamples != null)
            {
                for (Int32 j = 0; j < cache.AnalogSamplesPerFrame; j++)
                {
                    for (Int32 i = 0; i < cache.AnalogChannelCount; i++)
                    {
                        Single data = frame.AnalogSamples[i][j] / cache.AnalogGeneralScale / (cache.AnalogChannelScale != null && cache.AnalogChannelScale.Length > 0 ? cache.AnalogChannelScale[i] : 1.0F)
                             + ((cache.AnalogZeroOffset != null && cache.AnalogZeroOffset.Length > 0) ? cache.AnalogZeroOffset[i] : (Int16)0);

                        this._writer.Write((Int16)Math.Round(data, MidpointRounding.AwayFromZero));
                    }
                }
            }
        }

        /// <summary>
        /// 更新C3D文件头和参数字典
        /// </summary>
        /// <param name="file">C3D文件</param>
        private void UpdateHeaderAndParameters(C3DFile file)
        {
            file.Header.FirstDataSectionID = this._newDataStartBlockIndex;
            C3DParameter dataStart = file.Parameters["POINT", "DATA_START"];
            if (dataStart != null)
            {
                dataStart.InternalSetData<Int16>(this._newDataStartBlockIndex);

                this._writer.Seek(this._dataStartOffset, SeekOrigin.Begin);
                this._writer.Write(dataStart.GetLastDataArrayWithoutDescrption());
            }
        }
        #endregion

        #region Dispose
        private void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                if (this._writer != null)
                {
                    this._writer.Close();
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }
        #endregion
    }
}