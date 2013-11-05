using System;
using System.Collections.Generic;
using System.Text;

namespace C3D.Number
{
    /// <summary>
    /// C3D字节转换器
    /// </summary>
    internal static class C3DBitConverter
    {
        #region ToInt16
        /// <summary>
        /// 从字节数组中获取16位浮点数字
        /// </summary>
        /// <param name="type">处理器类型</param>
        /// <param name="data">字节数组</param>
        /// <param name="startIndex">数据起始位置</param>
        /// <returns>16位浮点数字</returns>
        internal static Int16 ToInt16(C3DProcessorType type, Byte[] data, Int32 startIndex)
        {
            if (C3DBitConverter.IsNeedReverse(type))
            {
                Array.Reverse(data, startIndex, 2);
            }

            return BitConverter.ToInt16(data, startIndex);
        }

        /// <summary>
        /// 从字节数组中获取16位浮点数字
        /// </summary>
        /// <param name="type">处理器类型</param>
        /// <param name="data">字节数组</param>
        /// <returns>16位浮点数字</returns>
        internal static Int16 ToInt16(C3DProcessorType type, Byte[] data)
        {
            return ToInt16(type, data, 0);
        }

        /// <summary>
        /// 从字节数组中获取16位浮点数字
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="startIndex">数据起始位置</param>
        /// <returns>16位浮点数字</returns>
        internal static Int16 ToInt16(Byte[] data, Int32 startIndex)
        {
            return ToInt16(C3DFile.DEFAULT_PROCESSOR_TYPE, data, startIndex);
        }

        /// <summary>
        /// 从字节数组中获取16位浮点数字
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <returns>16位浮点数字</returns>
        internal static Int16 ToInt16(Byte[] data)
        {
            return ToInt16(C3DFile.DEFAULT_PROCESSOR_TYPE, data, 0);
        }
        #endregion

        #region ToSingle
        /// <summary>
        /// 从字节数组中获取32位单精度浮点数字
        /// </summary>
        /// <param name="type">处理器类型</param>
        /// <param name="data">字节数组</param>
        /// <param name="startIndex">数据起始位置</param>
        /// <returns>32位单精度浮点数字</returns>
        internal static Single ToSingle(C3DProcessorType type, Byte[] data, Int32 startIndex)
        {
            if (C3DBitConverter.IsNeedReverse(type))
            {
                Array.Reverse(data, startIndex, 4);
            }

            if (type == C3DProcessorType.DEC)
            {
                return new VAXSingle(data, startIndex).ToSingle();
            }
            else
            {
                return BitConverter.ToSingle(data, startIndex);
            }
        }

        /// <summary>
        /// 从字节数组中获取32位单精度浮点数字
        /// </summary>
        /// <param name="type">处理器类型</param>
        /// <param name="data">字节数组</param>
        /// <returns>32位单精度浮点数字</returns>
        internal static Single ToSingle(C3DProcessorType type, Byte[] data)
        {
            return ToSingle(type, data, 0);
        }

        /// <summary>
        /// 从字节数组中获取32位单精度浮点数字
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="startIndex">数据起始位置</param>
        /// <returns>32位单精度浮点数字</returns>
        internal static Single ToSingle(Byte[] data, Int32 startIndex)
        {
            return ToSingle(C3DFile.DEFAULT_PROCESSOR_TYPE, data, startIndex);
        }

        /// <summary>
        /// 从字节数组中获取32位单精度浮点数字
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <returns>32位单精度浮点数字</returns>
        internal static Single ToSingle(Byte[] data)
        {
            return ToSingle(C3DFile.DEFAULT_PROCESSOR_TYPE, data, 0);
        }
        #endregion

        #region GetBytes
        /// <summary>
        /// 返回给定数据的字节数组
        /// </summary>
        /// <param name="value">16位整型</param>
        /// <returns>字节数组</returns>
        internal static Byte[] GetBytes(Int16 value)
        {
            Byte[] data = BitConverter.GetBytes(value);

            if (C3DBitConverter.IsNeedReverse(C3DFile.DEFAULT_PROCESSOR_TYPE))
            {
                Array.Reverse(data, 0, 2);
            }

            return data;
        }

        /// <summary>
        /// 返回给定数据的字节数组
        /// </summary>
        /// <param name="value">32位单精度浮点型</param>
        /// <returns>字节数组</returns>
        internal static Byte[] GetBytes(Single value)
        {
            Byte[] data = BitConverter.GetBytes(value);

            if (C3DBitConverter.IsNeedReverse(C3DFile.DEFAULT_PROCESSOR_TYPE))
            {
                Array.Reverse(data, 0, 4);
            }

            return data;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取是否需要反转数组
        /// </summary>
        /// <param name="type">处理器类型</param>
        /// <returns>是否需要反转数组</returns>
        private static Boolean IsNeedReverse(C3DProcessorType type)
        {
            if (type == C3DProcessorType.MIPS) return BitConverter.IsLittleEndian;//MIPS是Big-Endian，如果机器是Little-Endian需要反转

            return !BitConverter.IsLittleEndian;//其他情况当机器是Big-Endian才需要反转
        }
        #endregion
    }
}