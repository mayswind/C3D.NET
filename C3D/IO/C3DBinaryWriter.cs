using System;
using System.IO;

using C3D.Number;

namespace C3D.IO
{
    /// <summary>
    /// C3D二进制文件写入器
    /// </summary>
    internal sealed class C3DBinaryWriter : IDisposable
    {
        #region 字段
        private Stream _stream;
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
        /// 初始化新的C3D二进制文件写入器
        /// </summary>
        /// <param name="input">C3D文件流</param>
        internal C3DBinaryWriter(Stream output)
        {
            this._stream = output;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 将字节数组写入当前位置
        /// </summary>
        /// <param name="data">要写入的字节数组</param>
        internal void Write(Byte[] data)
        {
            this._stream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// 将字节写入当前位置
        /// </summary>
        /// <param name="data">要写入的字节</param>
        internal void Write(Byte data)
        {
            this._stream.WriteByte(data);
        }

        /// <summary>
        /// 将有符号的字节写入当前位置
        /// </summary>
        /// <param name="data">要写入的有符号字节</param>
        internal void Write(SByte data)
        {
            this._stream.WriteByte((Byte)data);
        }

        /// <summary>
        /// 将16位有符号整数写入当前位置
        /// </summary>
        /// <param name="data">要写入的16位有符号整数</param>
        internal void Write(Int16 data)
        {
            Byte[] temp = C3DBitConverter.GetBytes(data);
            this.Write(temp);
        }

        /// <summary>
        /// 将32位单精度浮点数写入当前位置
        /// </summary>
        /// <param name="data">要写入的32位单精度浮点数</param>
        internal void Write(Single data)
        {
            Byte[] temp = C3DBitConverter.GetBytes(data);
            this.Write(temp);
        }

        /// <summary>
        /// 设置当前流中的位置
        /// </summary>
        /// <param name="offset">相对参考位置的偏移量</param>
        /// <param name="origin">参考位置</param>
        /// <returns>偏移后的新位置</returns>
        internal Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            return this._stream.Seek(offset, origin);
        }

        /// <summary>
        /// 写入数据到文件
        /// </summary>
        internal void Flush()
        {
            this._stream.Flush();
        }

        /// <summary>
        /// 关闭C3D二进制文件写入器和基础流
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