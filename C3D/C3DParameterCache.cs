using System;

namespace C3D
{
    /// <summary>
    /// C3D参数缓存
    /// </summary>
    internal sealed class C3DParameterCache
    {
        #region 字段
        private Int32 _firstDataBlockPosition;

        private UInt16 _pointCount;
        private Single _scaleFactor;
        private UInt16 _frameCount;
        private Single _frameRate;

        private UInt16 _analogChannelCount;
        private UInt16 _analogSamplesPerFrame;

        private Single _analogGeneralScale;
        private Single[] _analogChannelScale;
        private Int16[] _analogZeroOffset;
        #endregion

        #region 属性
        /// <summary>
        /// 获取3D和模拟数据区第一个Block位置
        /// </summary>
        internal Int32 FirstDataBlockPosition
        {
            get { return this._firstDataBlockPosition; }
        }

        /// <summary>
        /// 获取3D坐标点个数
        /// </summary>
        internal UInt16 PointCount
        {
            get { return this._pointCount; }
        }

        /// <summary>
        /// 获取比例因子(3D浮点坐标为负数)
        /// </summary>
        internal Single ScaleFactor
        {
            get { return this._scaleFactor; }
        }

        /// <summary>
        /// 获取总帧数
        /// </summary>
        internal UInt16 FrameCount
        {
            get { return this._frameCount; }
        }

        /// <summary>
        /// 获取帧率
        /// </summary>
        internal Single FrameRate
        {
            get { return this._frameRate; }
        }

        /// <summary>
        /// 获取模拟数据通道的个数
        /// </summary>
        internal UInt16 AnalogChannelCount
        {
            get { return this._analogChannelCount; }
        }

        /// <summary>
        /// 获取每帧模拟样例个数
        /// </summary>
        internal UInt16 AnalogSamplesPerFrame
        {
            get { return this._analogSamplesPerFrame; }
        }

        internal Single AnalogGeneralScale
        {
            get { return this._analogGeneralScale; }
        }

        internal Single[] AnalogChannelScale
        {
            get { return this._analogChannelScale; }
        }

        internal Int16[] AnalogZeroOffset
        {
            get { return this._analogZeroOffset; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化取参数缓存信息
        /// </summary>
        /// <param name="header">C3D文件头</param>
        /// <param name="dictionary">C3D参数字典</param>
        private C3DParameterCache(C3DHeader header, C3DParameterDictionary dictionary)
        {
            this.LoadFirstDataBlockPosition(header, dictionary);

            this.LoadPointCount(header, dictionary);
            this.LoadScaleFactor(header, dictionary);
            this.LoadFrameCount(header, dictionary);
            this.LoadFrameRate(header, dictionary);

            this.LoadAnalogChannelCount(header, dictionary);
            this.LoadAnalogSamplesPerFrame(header, dictionary);

            this.LoadAnalogGeneralScale(dictionary);
            this.LoadAnalogChannelScale(dictionary);
            this.LoadAnalogZeroOffset(dictionary);
        }
        #endregion

        #region 私有方法
        private void LoadFirstDataBlockPosition(C3DHeader header, C3DParameterDictionary dictionary)
        {
            this._firstDataBlockPosition = 0;

            if (dictionary != null && dictionary.ContainsParameter("POINT", "DATA_START"))
            {
                this._firstDataBlockPosition = ((UInt16)Convert.ToInt16(dictionary["POINT", "DATA_START"].GetData(0)) - 1) * C3DFile.SECTION_SIZE;
            }

            if (header != null && this._firstDataBlockPosition <= 0)
            {
                this._firstDataBlockPosition = (header.FirstDataSectionID - 1) * C3DFile.SECTION_SIZE;
            }
        }

        private void LoadPointCount(C3DHeader header, C3DParameterDictionary dictionary)
        {
            if (dictionary != null && dictionary.ContainsParameter("POINT", "USED"))
            {
                this._pointCount = (UInt16)Convert.ToInt16(dictionary["POINT", "USED"].GetData(0));
            }
            else if (header != null)
            {
                this._pointCount = header.PointCount;
            }
            else
            {
                this._pointCount = 0;
            }
        }

        private void LoadScaleFactor(C3DHeader header, C3DParameterDictionary dictionary)
        {
            if (dictionary != null && dictionary.ContainsParameter("POINT", "SCALE"))
            {
                this._scaleFactor = Convert.ToSingle(dictionary["POINT", "SCALE"].GetData(0));
            }
            else if (header != null)
            {
                this._scaleFactor = header.ScaleFactor;
            }
            else
            {
                this._scaleFactor = 0;
            }
        }

        private void LoadFrameCount(C3DHeader header, C3DParameterDictionary dictionary)
        {
            if (dictionary != null && dictionary.ContainsParameter("POINT", "FRAMES"))
            {
                this._frameCount = (UInt16)Convert.ToInt16(dictionary["POINT", "FRAMES"].GetData(0));
            }
            else if (header != null)
            {
                this._frameCount = (UInt16)(header.LastFrameIndex - header.FirstFrameIndex + 1);
            }
            else
            {
                this._frameCount = 0;
            }
        }

        private void LoadFrameRate(C3DHeader header, C3DParameterDictionary dictionary)
        {
            if (dictionary != null && dictionary.ContainsParameter("POINT", "RATE"))
            {
                this._frameRate = Convert.ToSingle(dictionary["POINT", "RATE"].GetData(0));
            }
            else if (header != null)
            {
                this._frameRate = header.FrameRate;
            }
            else
            {
                this._frameRate = 0;
            }
        }

        private void LoadAnalogChannelCount(C3DHeader header, C3DParameterDictionary dictionary)
        {
            if (dictionary != null && dictionary.ContainsParameter("ANALOG", "USED"))
            {
                this._analogChannelCount = (UInt16)Convert.ToInt16(dictionary["ANALOG", "USED"].GetData(0));
            }
            else if (header != null)
            {
                this._analogChannelCount = (UInt16)(header.AnalogSamplesPerFrame != 0 ? header.AnalogMeasurementCount / header.AnalogSamplesPerFrame : 0);
            }
            else
            {
                this._analogChannelCount = 0;
            }
        }

        private void LoadAnalogSamplesPerFrame(C3DHeader header, C3DParameterDictionary dictionary)
        {
            if (dictionary != null && dictionary.ContainsParameter("ANALOG", "RATE"))
            {
                this._analogSamplesPerFrame = (UInt16)(Convert.ToSingle(dictionary["ANALOG", "RATE"].GetData(0)) / this._frameRate);
            }
            else if (header != null)
            {
                this._analogSamplesPerFrame = header.AnalogSamplesPerFrame;
            }
            else
            {
                this._analogSamplesPerFrame = 0;
            }
        }

        private void LoadAnalogGeneralScale(C3DParameterDictionary dictionary)
        {
            if (dictionary != null && dictionary.ContainsParameter("ANALOG", "GEN_SCALE"))
            {
                this._analogGeneralScale = Convert.ToSingle(dictionary["ANALOG", "GEN_SCALE"].GetData(0));
            }
            else
            {
                this._analogGeneralScale = 1.0F;
            }
        }

        private void LoadAnalogChannelScale(C3DParameterDictionary dictionary)
        {
            if (dictionary != null && dictionary.ContainsParameter("ANALOG", "SCALE"))
            {
                this._analogChannelScale = this.LoadFromArray<Single>(dictionary["ANALOG", "SCALE"], this._analogChannelCount);
            }
            else
            {
                this._analogChannelScale = null;
            }
        }

        private void LoadAnalogZeroOffset(C3DParameterDictionary dictionary)
        {
            if (dictionary != null && dictionary.ContainsParameter("ANALOG", "OFFSET"))
            {
                this._analogZeroOffset = this.LoadFromArray<Int16>(dictionary["ANALOG", "OFFSET"], this._analogChannelCount);
            }
            else
            {
                this._analogZeroOffset = null;
            }
        }

        private T[] LoadFromArray<T>(C3DParameter parameter, Int32 size)
        {
            Object raw = (parameter != null ? parameter.GetData() : null);
            T[] ret = null;

            if (raw != null && raw is T[])
            {
                ret = raw as T[];
            }
            else if (raw != null && raw is T && size > 0)
            {
                ret = new T[size];
                T unit = (T)raw;

                for (Int32 i = 0; i < ret.Length; i++)
                {
                    ret[i] = unit;
                }
            }

            return ret;
        }
        #endregion

        #region 静态方法
        /// <summary>
        /// 从C3D文件头中创建参数缓存
        /// </summary>
        /// <param name="header">C3D文件头</param>
        internal static C3DParameterCache CreateCache(C3DHeader header)
        {
            return new C3DParameterCache(header, null);
        }

        /// <summary>
        /// 从C3D参数字典中创建参数缓存
        /// </summary>
        /// <param name="dictionary">C3D参数字典</param>
        internal static C3DParameterCache CreateCache(C3DParameterDictionary dictionary)
        {
            return new C3DParameterCache(null, dictionary);
        }

        /// <summary>
        /// 从C3D文件创建参数缓存
        /// </summary>
        /// <param name="file">C3D文件</param>
        internal static C3DParameterCache CreateCache(C3DFile file)
        {
            return new C3DParameterCache(file.Header, file.Parameters);
        }
        #endregion
    }
}