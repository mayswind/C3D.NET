using System;
using System.IO;

using C3D.Number;

namespace C3D.IO
{
    /// <summary>
    /// C3D二进制文件读取器
    /// </summary>
    internal sealed class C3DBinaryReader : IDisposable
    {
        #region 字段
        private Stream _stream;
        private C3DProcessorType _processorType;
        #endregion

        #region 属性
        /// <summary>
        /// 获取基础流
        /// </summary>
        internal Stream BaseStream
        {
            get { return this._stream; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的C3D二进制文件读取器
        /// </summary>
        /// <param name="input">C3D文件流</param>
        internal C3DBinaryReader(Stream input)
        {
            this._stream = input;
            this._processorType = C3DConstants.FILE_DEFAULT_PROCESSOR_TYPE;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 设置处理器类型
        /// </summary>
        /// <param name="type">处理器类型</param>
        internal void SetProcessorType(C3DProcessorType type)
        {
            this._processorType = type;
        }

        /// <summary>
        /// 从当前流中将指定字节读入字节数组
        /// </summary>
        /// <param name="count">要读取的字节数</param>
        /// <exception cref="EndOfStreamException">读取超出文件末尾</exception>
        /// <returns>读取后的字节数组</returns>
        internal Byte[] ReadBytes(Int32 count)
        {
            Byte[] result = new Byte[count];
            Int32 bytesRead = 0;

            do
            {
                Int32 n = this._stream.Read(result, bytesRead, count - bytesRead);

                if (n == 0)
                {
                    break;
                }

                bytesRead += n;
            }
            while (bytesRead < count);

            if (bytesRead != result.Length)
            {
                throw new System.IO.EndOfStreamException();
            }

            return result;
        }

        /// <summary>
        /// 从当前流中读取的下一个字节
        /// </summary>
        /// <exception cref="EndOfStreamException">读取超出文件末尾</exception>
        /// <returns>下一个字节</returns>
        internal Byte ReadByte()
        {
            Int32 n = this._stream.ReadByte();
            
            if (n == -1)
            {
                throw new System.IO.EndOfStreamException();
            }

            return (Byte)n;
        }

        /// <summary>
        /// 从当前流中读取的下一个有符号字节
        /// </summary>
        /// <exception cref="EndOfStreamException">读取超出文件末尾</exception>
        /// <returns>下一个有符号字节</returns>
        internal SByte ReadSByte()
        {
            Int32 n = this._stream.ReadByte();

            if (n == -1)
            {
                throw new System.IO.EndOfStreamException();
            }

            return (SByte)n;
        }

        /// <summary>
        /// 从当前流中读取下一个16位有符号整数
        /// </summary>
        /// <returns>下一个16位有符号整数</returns>
        internal Int16 ReadInt16()
        {
            Byte[] data = this.ReadBytes(2);

            return C3DBitConverter.ToInt16(this._processorType, data, 0);
        }

        /// <summary>
        /// 从当前流中读取下一个32位单精度浮点数
        /// </summary>
        /// <returns>下一个32位单精度浮点数</returns>
        internal Single ReadSingle()
        {
            Byte[] data = this.ReadBytes(4);

            return C3DBitConverter.ToSingle(this._processorType, data);
        }

        /// <summary>
        /// 关闭C3D二进制文件读取器和基础流
        /// </summary>
        internal void Close()
        {
            this.Dispose(true);
        }
        #endregion

        #region Dispose
        private void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                if (this._stream != null)
                {
                    this._stream.Close();
                }
            }
        }

        void IDisposable.Dispose()
        {
            this.Dispose(true);
        }
        #endregion
    }
}