using System;
using System.Text;

namespace C3D
{
    /// <summary>
    /// 抽象C3D参数类
    /// </summary>
    public abstract class AbstractC3DParameter
    {
        #region 字段
        private SByte _id;
        private String _name;
        private String _description;
        private Boolean _isLocked;
        #endregion

        #region 属性
        /// <summary>
        /// 获取参数ID
        /// </summary>
        public SByte ID
        {
            get { return this._id; }
        }

        /// <summary>
        /// 获取参数名称
        /// </summary>
        public String Name
        {
            get { return this._name; }
        }

        /// <summary>
        /// 获取或设置参数描述
        /// </summary>
        public String Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        /// <summary>
        /// 获取或设置是否锁定
        /// </summary>
        public Boolean IsLocked
        {
            get { return this._isLocked; }
            set { this._isLocked = value; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的抽象C3D参数类
        /// </summary>
        /// <param name="id">参数项ID</param>
        /// <param name="name">参数项名称数据块</param>
        /// <param name="isLocked">是否锁定</param>
        public AbstractC3DParameter(SByte id, Byte[] name, Boolean isLocked)
        {
            this._id = id;
            this._name = this.ReadStringFromBytes(name).ToUpperInvariant();
            this._description = String.Empty;
            this._isLocked = isLocked;
        }

        /// <summary>
        /// 初始化新的抽象C3D参数类
        /// </summary>
        /// <param name="id">参数项ID</param>
        /// <param name="name">参数项名称</param>
        /// <param name="description">参数项描述</param>
        public AbstractC3DParameter(SByte id, String name, String description)
        {
            this._id = id;
            this._name = name.ToUpperInvariant();
            this._description = description;
            this._isLocked = false;
        }
        #endregion

        #region 抽象方法
        /// <summary>
        /// 获取不包含描述的最后数据块
        /// </summary>
        /// <returns>不包含描述的最后数据块</returns>
        internal abstract Byte[] GetLastDataArrayWithoutDescrption();
        #endregion

        #region 内部方法
        /// <summary>
        /// 获取参数名称字节数组
        /// </summary>
        /// <returns>参数名称字节数组</returns>
        internal Byte[] GetNameByteArray()
        {
            return this.WrtieStringToBytes(this._name);
        }

        /// <summary>
        /// 获取参数描述字节数组
        /// </summary>
        /// <returns>参数描述字节数组</returns>
        internal Byte[] GetDescriptionByteArray()
        {
            return this.WrtieStringToBytes(this._description);
        }
        #endregion

        #region 保护方法
        /// <summary>
        /// 从数据块中读取字符串
        /// </summary>
        /// <param name="data">字符串数据块</param>
        /// <returns>字符串内容</returns>
        protected String ReadStringFromBytes(Byte[] data)
        {
            return (data != null ? Encoding.ASCII.GetString(data, 0, data.Length) : String.Empty);
        }

        /// <summary>
        /// 从数据块中读取字符串
        /// </summary>
        /// <param name="data">字符串数据块</param>
        /// <param name="startIndex">数据起始索引</param>
        /// <param name="length">数据长度</param>
        /// <returns>字符串内容</returns>
        protected String ReadStringFromBytes(Byte[] data, Int32 startIndex, Int32 length)
        {
            return (data != null ? Encoding.ASCII.GetString(data, startIndex, length) : String.Empty);
        }

        /// <summary>
        /// 将字符串写入数据块
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>数据块</returns>
        protected Byte[] WrtieStringToBytes(String s)
        {
            return Encoding.ASCII.GetBytes(s);
        }

        /// <summary>
        /// 将字符串写入数据块
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="startIndex">第一个字符索引</param>
        /// <param name="charCount">字符数量</param>
        /// <param name="data">数据块</param>
        /// <param name="byteIndex">数组开始写入位置</param>
        /// <returns>实际写入字节数</returns>
        protected Int32 WrtieStringToBytes(String s, Int32 startIndex, Int32 charCount, Byte[] data, Int32 byteIndex)
        {
            return Encoding.ASCII.GetBytes(s, startIndex, charCount, data, byteIndex);
        }

        /// <summary>
        /// 从最后一部分数据中读取描述信息
        /// </summary>
        /// <param name="data">数据块</param>
        /// <param name="startIndex">描述信息开始索引</param>
        /// <returns>描述信息</returns>
        protected void SetDescription(Byte[] data, Int32 startIndex)
        {
            this._description = String.Empty;

            if (data == null)
            {
                return;
            }

            if (startIndex < data.Length)
            {
                Byte length = data[startIndex];

                if (startIndex + length < data.Length)
                {
                    this._description = this.ReadStringFromBytes(data, 1 + startIndex, length);
                }
            }
        }
        #endregion

        #region 重载方法
        /// <summary>
        /// 输出C3D参数项的信息
        /// </summary>
        /// <returns>参数项的信息</returns>
        public override String ToString()
        {
            return String.Format("{0}, Name = {1}", this.GetType().ToString(), this.Name);
        }
        #endregion
    }
}