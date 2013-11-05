using System;

namespace C3D
{
    /// <summary>
    /// C3D帧数据
    /// </summary>
    public sealed class C3DFrame
    {
        #region 字段
        private C3DPoint3DData[] _point3DDatas;
        private C3DAnalogSamples[] _analogSamples;
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置C3D 3D坐标点集合
        /// </summary>
        public C3DPoint3DData[] Point3Ds
        {
            get { return this._point3DDatas; }
            set { this._point3DDatas = value; }
        }

        /// <summary>
        /// 获取或设置C3D模拟采样集合
        /// </summary>
        public C3DAnalogSamples[] AnalogSamples
        {
            get { return this._analogSamples; }
            set { this._analogSamples = value; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的C3D帧数据
        /// </summary>
        /// <param name="point3Ds">3D坐标点集合</param>
        /// <param name="analogSamples">模拟采样集合</param>
        public C3DFrame(C3DPoint3DData[] point3Ds, C3DAnalogSamples[] analogSamples)
        {
            this._point3DDatas = point3Ds;
            this._analogSamples = analogSamples;
        }

        /// <summary>
        /// 初始化新的C3D帧数据
        /// </summary>
        /// <param name="point3Ds">3D坐标点集合</param>
        public C3DFrame(C3DPoint3DData[] point3Ds)
        {
            this._point3DDatas = point3Ds;
            this._analogSamples = null;
        }

        /// <summary>
        /// 初始化新的C3D帧数据
        /// </summary>
        /// <param name="analogSamples">模拟采样集合</param>
        public C3DFrame(C3DAnalogSamples[] analogSamples)
        {
            this._point3DDatas = null;
            this._analogSamples = analogSamples;
        }
        #endregion
    }
}