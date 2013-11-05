using System;
using System.Runtime.Serialization;

namespace C3D
{
    /// <summary>
    /// C3D异常类
    /// </summary>
    [Serializable]
    public sealed class C3DException : Exception
    {
        /// <summary>
        /// 初始化新的C3D异常
        /// </summary>
        public C3DException() { }

        /// <summary>
        /// 初始化新的C3D异常
        /// </summary>
        /// <param name="message">异常信息</param>
        public C3DException(String message) : base(message) { }

        /// <summary>
        /// 初始化新的C3D异常
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="innerException">包含异常</param>
        public C3DException(String message, Exception innerException)
            : base(message, innerException)
        { }
    }
}