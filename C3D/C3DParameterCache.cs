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

        private Int16 _analogChannelCount;
        private Int16 _analogSamplesPerFrame;

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
        internal Int16 AnalogChannelCount
        {
            get { return this._analogChannelCount; }
        }

        /// <summary>
        /// 获取每帧模拟样例个数
        /// </summary>
        internal Int16 AnalogSamplesPerFrame
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
        /// 从C3D文件头中获取参数缓存信息
        /// </summary>
        /// <param name="header">C3D文件头</param>
        /// <remarks>
        /// 万不得已请不要使用此方法，否则模拟数据将读取出现错误。
        /// 由于文件头不包含AnalogGenScale、AnalogScale、AnalogZeroOffset
        /// </remarks>
        private C3DParameterCache(C3DHeader header)
        {
            this._firstDataBlockPosition = (header.FirstDataSectionID - 1) * C3DFile.SECTION_SIZE;

            this._pointCount = header.PointCount;
            this._scaleFactor = header.ScaleFactor;
            this._frameCount = (UInt16)(header.LastFrameIndex - header.FirstFrameIndex + 1);
            this._frameRate = header.FrameRate;

            this._analogChannelCount = (Int16)(header.AnalogSamplesPerFrame != 0 ? header.AnalogMeasurementCount / header.AnalogSamplesPerFrame : 0);
            this._analogSamplesPerFrame = header.AnalogSamplesPerFrame;

            this._analogGeneralScale = 1.0F;
            this._analogChannelScale = null;
            this._analogZeroOffset = null;
        }

        /// <summary>
        /// 从C3D参数字典中获取参数缓存信息
        /// </summary>
        /// <param name="dictionary">C3D参数字典</param>
        private C3DParameterCache(C3DParameterDictionary dictionary)
        {
            this._firstDataBlockPosition = (dictionary["POINT", "DATA_START"].GetData<Int16>() - 1) * C3DFile.SECTION_SIZE;

            this._pointCount = (UInt16)dictionary["POINT", "USED"].GetData<Int16>();
            this._scaleFactor = dictionary["POINT", "SCALE"].GetData<Single>();
            this._frameCount = (UInt16)dictionary["POINT", "FRAMES"].GetData<Int16>();
            this._frameRate = dictionary["POINT", "RATE"].GetData<Single>();

            this._analogChannelCount = dictionary["ANALOG", "USED"].GetData<Int16>();
            this._analogSamplesPerFrame = (Int16)(dictionary["ANALOG", "RATE"].GetData<Single>() / this._frameRate);

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
            return new C3DParameterCache(header);
        }

        /// <summary>
        /// 从C3D参数字典中创建参数缓存
        /// </summary>
        /// <param name="dictionary">C3D参数字典</param>
        internal static C3DParameterCache CreateCache(C3DParameterDictionary dictionary)
        {
            return new C3DParameterCache(dictionary);
        }

        /// <summary>
        /// 从C3D文件创建参数缓存
        /// </summary>
        /// <param name="file">C3D文件</param>
        internal static C3DParameterCache CreateCache(C3DFile file)
        {
            try
            {
                if (file.Parameters == null || file.Parameters.Count <= 0)
                {
                    return new C3DParameterCache(file.Parameters);
                }
                else
                {
                    return new C3DParameterCache(file.Header);
                }
            }
            catch
            {
                return new C3DParameterCache(file.Header);
            }
        }
        #endregion
    }
}