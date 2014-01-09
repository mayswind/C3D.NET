using System;
using System.Collections;
using System.Collections.Generic;

namespace C3D
{
    /// <summary>
    /// C3D参数组
    /// </summary>
    public sealed class C3DParameterGroup : AbstractC3DParameter, IEnumerable<C3DParameter>, ICollection<C3DParameter>
    {
        #region 字段
        private IDictionary<String, C3DParameter> _parameters;
        #endregion

        #region 索引器
        /// <summary>
        /// 获取或设置指定名称的C3D参数项
        /// </summary>
        /// <param name="name">参数项名称</param>
        /// <returns>C3D参数项</returns>
        /// <exception cref="NullReferenceException">不允许设置空的参数项</exception>
        /// <exception cref="C3DException">参数项名称必须与设置的名称相同</exception>
        public C3DParameter this[String name]
        {
            get
            {
                C3DParameter param = null;
                return this._parameters.TryGetValue(name.ToUpperInvariant(), out param) ? param : null; 
            }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }

                if (!String.Equals(name, value.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new C3DException("The name inputed is not equal to the parameter name.");
                }

                this._parameters[name.ToUpperInvariant()] = value;
            }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的C3D参数组
        /// </summary>
        /// <param name="id">参数组ID</param>
        /// <param name="name">参数组名称数据块</param>
        /// <param name="isLocked">是否锁定</param>
        /// <param name="lastData">最后一部分数据块</param>
        internal C3DParameterGroup(SByte id, Byte[] name, Boolean isLocked, Byte[] lastData)
            : base(id, name, isLocked)
        {
            this.SetDescription(lastData, 0);
            this._parameters = new SortedDictionary<String, C3DParameter>();
        }

        /// <summary>
        /// 初始化新的C3D参数组
        /// </summary>
        /// <param name="id">参数项ID</param>
        /// <param name="name">参数项名称</param>
        /// <param name="description">参数项描述</param>
        internal C3DParameterGroup(SByte id, String name, String description)
            : base(id, name, description)
        {
            this._parameters = new SortedDictionary<String, C3DParameter>();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 向参数组内添加一个参数项并返回该项
        /// </summary>
        /// <param name="name">参数项名称</param>
        /// <param name="description">参数项描述</param>
        /// <exception cref="ArgumentException">参数项名称已存在</exception>
        public C3DParameter Add(String name, String description)
        {
            C3DParameter param = new C3DParameter(Math.Abs(this.ID), name, description);
            this.Add(param);

            return param;
        }

        /// <summary>
        /// 向参数组内添加一个参数项并返回该项
        /// </summary>
        /// <typeparam name="T">参数项数据类型</typeparam>
        /// <param name="name">参数项名称</param>
        /// <param name="description">参数项描述</param>
        /// <param name="data">参数项数据</param>
        /// <exception cref="ArgumentException">参数项名称已存在</exception>
        public C3DParameter Add<T>(String name, String description, T data)
        {
            C3DParameter param = this.Add(name, description);
            param.SetData<T>(data);

            return param;
        }

        /// <summary>
        /// 判断参数组内是否存在指定参数名称的参数项
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>是否存在指定参数名称的参数项</returns>
        public Boolean Contains(String name)
        {
            return this._parameters.ContainsKey(name.ToUpperInvariant());
        }
        #endregion

        #region GetLastDataArrayWithoutDescrption
        /// <summary>
        /// 获取不包含描述的最后数据块
        /// </summary>
        /// <returns>不包含描述的最后数据块</returns>
        internal override Byte[] GetLastDataArrayWithoutDescrption()
        {
            return null;
        }
        #endregion

        #region 接口方法
        #region IEnumerable
        /// <summary>
        /// 返回一个循环访问C3D参数组集合的枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public IEnumerator<C3DParameter> GetEnumerator()
        {
            return this._parameters.Values.GetEnumerator();
        }

        /// <summary>
        /// 返回一个循环访问C3D参数组集合的枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._parameters.Values.GetEnumerator();
        }
        #endregion

        #region ICollection
        /// <summary>
        /// 获取C3D参数组内参数的数量
        /// </summary>
        public Int32 Count
        {
            get { return this._parameters.Count; }
        }

        Boolean ICollection<C3DParameter>.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// 添加一个新的C3D参数
        /// </summary>
        /// <param name="item">C3D参数</param>
        public void Add(C3DParameter item)
        {
            if (item == null)
            {
                throw new NullReferenceException();
            }

            if (this._parameters.ContainsKey(item.Name.ToUpperInvariant()))
            {
                throw new ArgumentException("This parameter name exists.");
            }

            this._parameters[item.Name.ToUpperInvariant()] = item;
        }

        /// <summary>
        /// 清空C3D参数组内所有参数
        /// </summary>
        public void Clear()
        {
            this._parameters.Clear();
        }

        /// <summary>
        /// 判断是否包含指定的C3D参数
        /// </summary>
        /// <param name="item">指定的C3D参数</param>
        /// <returns>是否包含指定的C3D参数</returns>
        public Boolean Contains(C3DParameter item)
        {
            return this._parameters.Values.Contains(item);
        }

        void ICollection<C3DParameter>.CopyTo(C3DParameter[] array, Int32 arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if ((array != null) && (array.Rank != 1))
            {
                throw new ArgumentException();
            }

            Int32 index = arrayIndex;
            foreach (KeyValuePair<String, C3DParameter> pair in this._parameters)
            {
                array[index++] = pair.Value;
            }
        }

        Boolean ICollection<C3DParameter>.Remove(C3DParameter item)
        {
            if (item == null)
            {
                return false;
            }
            else
            {
                return this._parameters.Remove(item.Name.ToUpperInvariant());
            }
        }
        #endregion
        #endregion
    }
}