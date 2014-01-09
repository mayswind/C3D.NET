using System;
using System.Text;

namespace C3D
{
    /// <summary>
    /// C3D模拟数据
    /// </summary>
    public struct C3DAnalogSamples
    {
        #region 字段
        private Single[] _analogSample;
        #endregion

        #region 属性
        /// <summary>
        /// 获取采样个数
        /// </summary>
        public Int32 SampleCount
        {
            get { return this._analogSample.Length; }
        }
        #endregion

        #region 索引器
        /// <summary>
        /// 获取或设置指定索引的采样
        /// </summary>
        /// <param name="index">采样索引</param>
        /// <returns>指定索引的采样</returns>
        public Single this[Int32 index]
        {
            get { return this._analogSample[index]; }
            set { this._analogSample[index] = value; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的C3D模拟数据
        /// </summary>
        /// <param name="samplesPerFrame">每帧采样个数</param>
        public C3DAnalogSamples(UInt16 samplesPerFrame)
        {
            this._analogSample = new Single[samplesPerFrame];
        }
        #endregion

        #region 重载方法
        /// <summary>
        /// 输出C3D模拟数据的信息
        /// </summary>
        /// <returns>模拟数据的信息</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("C3DAnalogSamples, [");

            for (Int32 i = 0; i < this._analogSample.Length; i++)
            {
                sb.Append(this._analogSample[i].ToString());
                sb.Append((i < this._analogSample.Length - 1 ? ',' : ']'));
            }

            return sb.ToString();
        }
        #endregion
    }
}