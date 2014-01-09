using System;

namespace C3D.Numerics
{
    /// <summary>
    /// VAX单精度浮点数字
    /// </summary>
    /// <remarks>
    /// SEF         S        EEEEEEEE        FFFFFFF        FFFFFFFF        FFFFFFFF
    /// bits        1        2      9        10                                    32          
    /// bytes       byte2           byte1                   byte4           byte3
    /// </remarks>
    public struct VAXSingle
    {
        #region 常量
        private const Int32 LENGTH = 4;
        private const Double BASE = 2.0;
        private const Double EXPONENT_BIAS = 128.0;
        private const Double MANTISSA_CONSTANT = 0.5;
        private const Double E24 = 16777216.0;
        #endregion

        #region 字段
        private Byte[] _data;
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的VAX单精度浮点数字
        /// </summary>
        /// <param name="data">VAX单精度浮点数字字节数组</param>
        /// <param name="startIndex">数据起始位置</param>
        public VAXSingle(Byte[] data, Int32 startIndex)
        {
            this._data = new Byte[VAXSingle.LENGTH];
            Array.Copy(data, startIndex, this._data, 0, VAXSingle.LENGTH);
        }

        /// <summary>
        /// 初始化新的VAX单精度浮点数字
        /// </summary>
        /// <param name="num">系统标准的单精度浮点数字</param>
        public VAXSingle(Single num)
        {
            Int32 s = (num >= 0 ? 0 : 1);

            Double v = Math.Abs(num);
            Int32 e = (Int32)(Math.Log(v) / Math.Log(2.0) + 1.0 + VAXSingle.EXPONENT_BIAS);

            Double m = (v / Math.Pow(VAXSingle.BASE, e - VAXSingle.EXPONENT_BIAS)) - VAXSingle.MANTISSA_CONSTANT;
            Int32 f = (Int32)(m * VAXSingle.E24);

            this._data = new Byte[VAXSingle.LENGTH];
            this._data[1] = (Byte)((s << 7) + ((e & 0xFE) >> 1));
            this._data[0] = (Byte)(((e & 0x01) << 7) + ((f & 0x007F0000) >> 16));
            this._data[3] = (Byte)((f & 0x0000FF00) >> 8);
            this._data[2] = (Byte)(f & 0x000000FF);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取系统标准的单精度浮点数字
        /// </summary>
        /// <returns>系统标准的单精度浮点数字</returns>
        public Single ToSingle()
        {
            Byte b1 = this._data[1];
            Byte b2 = this._data[0];
            Byte b3 = this._data[3];
            Byte b4 = this._data[2];

            Double s = (b1 & 0x80) >> 7;
            Double e = ((b1 & 0x7F) << 1) + ((b2 & 0x80) >> 7);
            Double f = ((b2 & 0x7F) << 16) + (b3 << 8) + b4;
            Double m = f / VAXSingle.E24;

            if (e == 0 && s == 0) return 0;
            if (e == 0 && s == 1) return Single.NaN;

            return (Single)((s == 0 ? 1.0 : -1.0) * (VAXSingle.MANTISSA_CONSTANT + m) * Math.Pow(VAXSingle.BASE, e - VAXSingle.EXPONENT_BIAS));
        }

        /// <summary>
        /// 获取VAX单精度浮点数据字节数组
        /// </summary>
        /// <returns>字节数组</returns>
        public Byte[] ToArray()
        {
            Byte[] data = new Byte[VAXSingle.LENGTH];

            Array.Copy(this._data, data, VAXSingle.LENGTH);

            return data;
        }
        #endregion
    }
}