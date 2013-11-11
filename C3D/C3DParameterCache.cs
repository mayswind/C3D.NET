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
            if (dictionary != null && dictionary.ContainsGroup("POINT"))
            {
                this.LoadPointsParametersFromDictionary(dictionary);
            }
            else if (header != null)
            {
                this.LoadPointsParametersFromHeader(header);
            }

            if (dictionary != null && dictionary.ContainsGroup("ANALOG"))
            {
                this.LoadAnalogParametersFromDictionary(dictionary);
            }
            else if (header != null)
            {
                this.LoadAnalogParametersFromHeader(header);
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 从C3D文件头中读取3D坐标相关参数
        /// </summary>
        /// <param name="header">C3D文件头</param>
        private void LoadPointsParametersFromHeader(C3DHeader header)
        {
            this._firstDataBlockPosition = (header.FirstDataSectionID - 1) * C3DFile.SECTION_SIZE;

            this._pointCount = header.PointCount;
            this._scaleFactor = header.ScaleFactor;
            this._frameCount = (UInt16)(header.LastFrameIndex - header.FirstFrameIndex + 1);
            this._frameRate = header.FrameRate;
        }

        /// <summary>
        /// 从C3D参数字典中读取3D坐标相关参数
        /// </summary>
        /// <param name="dictionary">C3D参数字典</param>
        private void LoadPointsParametersFromDictionary(C3DParameterDictionary dictionary)
        {
            this._firstDataBlockPosition = ((UInt16)dictionary["POINT", "DATA_START"].GetData<Int16>() - 1) * C3DFile.SECTION_SIZE;

            this._pointCount = (UInt16)dictionary["POINT", "USED"].GetData<Int16>();
            this._scaleFactor = dictionary["POINT", "SCALE"].GetData<Single>();
            this._frameCount = (UInt16)dictionary["POINT", "FRAMES"].GetData<Int16>();
            this._frameRate = dictionary["POINT", "RATE"].GetData<Single>();
        }

        /// <summary>
        /// 从C3D文件头中读取模拟采样相关参数
        /// </summary>
        /// <param name="header">C3D文件头</param>
        private void LoadAnalogParametersFromHeader(C3DHeader header)
        {
            this._analogChannelCount = (UInt16)(header.AnalogSamplesPerFrame != 0 ? header.AnalogMeasurementCount / header.AnalogSamplesPerFrame : 0);
            this._analogSamplesPerFrame = (UInt16)header.AnalogSamplesPerFrame;

            this._analogGeneralScale = 1.0F;
            this._analogChannelScale = null;
            this._analogZeroOffset = null;
        }

        /// <summary>
        /// 从C3D参数字典中读取模拟采样相关参数
        /// </summary>
        /// <param name="dictionary">C3D参数字典</param>
        private void LoadAnalogParametersFromDictionary(C3DParameterDictionary dictionary)
        {
            this._analogChannelCount = (UInt16)dictionary["ANALOG", "USED"].GetData<Int16>();
            this._analogSamplesPerFrame = (UInt16)(dictionary["ANALOG", "RATE"].GetData<Single>() / this._frameRate);

            this._analogGeneralScale = dictionary["ANALOG", "GEN_SCALE"].GetData<Single>();
            this._analogChannelScale = dictionary["ANALOG", "SCALE"].GetData<Single[]>();
            this._analogZeroOffset = dictionary["ANALOG", "OFFSET"].GetData<Int16[]>();
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