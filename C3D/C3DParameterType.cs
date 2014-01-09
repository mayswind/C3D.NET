using System;

namespace C3D
{
    /// <summary>
    /// 参数类型
    /// </summary>
    public enum C3DParameterType : sbyte
    {
        /// <summary>
        /// 无效类型
        /// </summary>
        Invalid = 0,
        
        /// <summary>
        /// 字符类型
        /// </summary>
        Char = -1,

        /// <summary>
        /// 字节类型
        /// </summary>
        Byte = 1,

        /// <summary>
        /// 16位整型数字
        /// </summary>
        Int16 = 2,

        /// <summary>
        /// 单精度浮点数字
        /// </summary>
        Single = 4
    }
}