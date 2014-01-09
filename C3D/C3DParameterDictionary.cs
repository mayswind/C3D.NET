using System;
using System.Collections;
using System.Collections.Generic;

namespace C3D
{
    /// <summary>
    /// C3D参数字典
    /// </summary>
    public sealed class C3DParameterDictionary : IEnumerable<C3DParameterGroup>, ICollection<C3DParameterGroup>
    {
        #region 字段
        private IDictionary<Int32, C3DParameterGroup> _paramGroups = null;
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化空的C3D参数字典
        /// </summary>
        private C3DParameterDictionary()
        {
            this._paramGroups = new SortedDictionary<Int32, C3DParameterGroup>();
        }
        #endregion

        #region 索引器
        /// <summary>
        /// 获取或设置指定参数项
        /// </summary>
        /// <param name="groupName">参数组名称</param>
        /// <param name="parameterName">参数项名称</param>
        /// <exception cref="C3DException">参数组不存在</exception>
        /// <returns>参数项</returns>
        public C3DParameter this[String groupName, String parameterName]
        {
            get
            {
                C3DParameterGroup group = this.GetGroup(groupName);

                return (group != null ? group[parameterName] : null);
            }
            set
            {
                C3DParameterGroup group = this.GetGroup(groupName);

                if (group == null)
                {
                    throw new C3DException("No such parameter group!");
                }

                group[parameterName] = value;
            }
        }

        /// <summary>
        /// 获取或设置指定路径的参数项
        /// </summary>
        /// <param name="parameterPath">分号(:)分隔的参数项路径</param>
        /// <exception cref="C3DException">参数项路径错误</exception>
        /// <returns>参数项</returns>
        public C3DParameter this[String parameterPath]
        {
            get
            {
                String[] names = parameterPath.Split(':');

                if (names.Length != 2)
                {
                    return null;
                }

                return this[names[0], names[1]];
            }
            set
            {
                String[] names = parameterPath.Split(':');

                if (names.Length != 2)
                {
                    throw new C3DException("Parameter path is INVALID!");
                }

                this[names[0], names[1]] = value;
            }
        }

        /// <summary>
        /// 获取指定索引的参数组
        /// </summary>
        /// <param name="groupIndex">参数组索引(大于0的数字)</param>
        /// <returns>参数组</returns>
        public C3DParameterGroup this[Int32 groupIndex]
        {
            get { return this._paramGroups[groupIndex]; }
        }
        #endregion

        #region 方法
        #region Parameter
        /// <summary>
        /// 获取是否包含指定路径的参数
        /// </summary>
        /// <param name="parameterPath">分号(:)分隔的参数项路径</param>
        /// <returns>是否包含指定路径的参数</returns>
        public Boolean ContainsParameter(String parameterPath)
        {
            return (this[parameterPath] != null);
        }

        /// <summary>
        /// 获取是否包含指定参数
        /// </summary>
        /// <param name="groupName">参数组名称</param>
        /// <param name="parameterName">参数项名称</param>
        /// <returns>是否包含指定参数</returns>
        public Boolean ContainsParameter(String groupName, String parameterName)
        {
            return (this[groupName, parameterName] != null);
        }

        /// <summary>
        /// 设置指定参数项
        /// </summary>
        /// <typeparam name="T">参数项数据类型</typeparam>
        /// <param name="groupName">参数组名称</param>
        /// <param name="parameterName">参数项名称</param>
        /// <param name="description">参数项描述</param>
        /// <param name="data">参数项数据</param>
        /// <exception cref="C3DException">没有指定参数组</exception>
        /// <returns>设置后的参数项</returns>
        public C3DParameter SetParameter<T>(String groupName, String parameterName, String description, T data)
        {
            C3DParameterGroup group = this.GetGroup(groupName);

            if (group == null)
            {
                throw new C3DException("No such parameter group!");
            }

            C3DParameter param = group[parameterName];

            if (param == null)
            {
                param = group.Add(parameterName, description);
            }
            else
            {
                if (description != null)
                {
                    param.Description = description;
                }
            }

            param.SetData<T>(data);

            return param;
        }

        /// <summary>
        /// 设置指定参数项
        /// </summary>
        /// <typeparam name="T">参数项数据类型</typeparam>
        /// <param name="groupName">参数组名称</param>
        /// <param name="parameterName">参数项名称</param>
        /// <param name="data">参数项数据</param>
        /// <exception cref="C3DException">没有指定参数组</exception>
        /// <returns>设置后的参数项</returns>
        public C3DParameter SetParameter<T>(String groupName, String parameterName, T data)
        {
            return this.SetParameter<T>(groupName, parameterName, null, data);
        }

        /// <summary>
        /// 获取指定参数项
        /// </summary>
        /// <param name="groupName">参数组名称</param>
        /// <param name="parameterName">参数项名称</param>
        /// <returns>指定参数项</returns>
        public C3DParameter GetParameter(String groupName, String parameterName)
        {
            return this[groupName, parameterName];
        }

        /// <summary>
        /// 获取指定路径的参数项
        /// </summary>
        /// <param name="parameterPath">分号(:)分隔的参数项路径</param>
        /// <returns>指定路径的参数项</returns>
        public C3DParameter GetParameter(String parameterPath)
        {
            return this[parameterPath];
        }

        /// <summary>
        /// 获取指定参数项的数据
        /// </summary>
        /// <param name="groupName">参数组名称</param>
        /// <param name="parameterName">参数项名称</param>
        /// <returns>指定参数项的数据</returns>
        public T GetParameterData<T>(String groupName, String parameterName)
        {
            C3DParameter param = this[groupName, parameterName];

            return (param != null ? param.GetData<T>() : default(T));
        }

        /// <summary>
        /// 获取指定路径的参数项的数据
        /// </summary>
        /// <param name="parameterPath">分号(:)分隔的参数项路径</param>
        /// <returns>指定路径的参数项的数据</returns>
        public T GetParameterData<T>(String parameterPath)
        {
            C3DParameter param = this[parameterPath];

            return (param != null ? param.GetData<T>() : default(T));
        }
        #endregion

        #region Group
        /// <summary>
        /// 获取是否包含指定组名的参数组
        /// </summary>
        /// <param name="name">参数组名称</param>
        /// <returns>是否包含指定组名的参数组</returns>
        public Boolean ContainsGroup(String name)
        {
            return (this.GetGroup(name) != null);
        }

        /// <summary>
        /// 设置C3D参数组
        /// </summary>
        /// <param name="id">参数组ID(大于0的数字)</param>
        /// <param name="name">参数组名称</param>
        /// <param name="description">参数组描述</param>
        /// <exception cref="ArgumentOutOfRangeException">参数组ID小于等于0</exception>
        public C3DParameterGroup SetGroup(Byte id, String name, String description)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException("Parameter group ID cannot be less than or equal zero.");
            }

            C3DParameterGroup group = new C3DParameterGroup((SByte)(-id), name.ToUpperInvariant(), description);
            this._paramGroups[id] = group;

            return group;
        }

        /// <summary>
        /// 通过组名获取C3D参数组
        /// </summary>
        /// <param name="name">参数组名称</param>
        /// <returns>C3D参数组</returns>
        public C3DParameterGroup GetGroup(String name)
        {
            name = name.ToUpperInvariant();

            foreach (KeyValuePair<Int32, C3DParameterGroup> pair in this._paramGroups)
            {
                if (String.Equals(pair.Value.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return pair.Value;
                }
            }

            return null;
        }
        #endregion
        #endregion

        #region 接口方法
        #region IEnumerable
        /// <summary>
        /// 返回一个循环访问C3D参数字典集合的枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public IEnumerator<C3DParameterGroup> GetEnumerator()
        {
            return this._paramGroups.Values.GetEnumerator();
        }

        /// <summary>
        /// 返回一个循环访问C3D参数字典集合的枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._paramGroups.Values.GetEnumerator();
        }
        #endregion

        #region ICollection
        /// <summary>
        /// 获取C3D参数集合内参数组的数量
        /// </summary>
        public Int32 Count
        {
            get { return this._paramGroups.Count; }
        }

        Boolean ICollection<C3DParameterGroup>.IsReadOnly
        {
            get { return false; }
        }

        void ICollection<C3DParameterGroup>.Add(C3DParameterGroup group)
        {
            if (group == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                this._paramGroups[group.ID] = group;
            }
        }

        /// <summary>
        /// 清空C3D参数字段中所有的参数组
        /// </summary>
        public void Clear()
        {
            this._paramGroups.Clear();
        }

        Boolean ICollection<C3DParameterGroup>.Contains(C3DParameterGroup item)
        {
            return this._paramGroups.Values.Contains(item);
        }

        void ICollection<C3DParameterGroup>.CopyTo(C3DParameterGroup[] array, Int32 arrayIndex)
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
            foreach (KeyValuePair<Int32, C3DParameterGroup> pair in this._paramGroups)
            {
                array[index++] = pair.Value;
            }
        }

        Boolean ICollection<C3DParameterGroup>.Remove(C3DParameterGroup group)
        {
            if (group == null)
            {
                return false;
            }
            else
            {
                return this._paramGroups.Remove(group.ID);
            }
        }
        #endregion
        #endregion

        #region 静态方法
        /// <summary>
        /// 创建完全空的C3D参数组字典
        /// </summary>
        /// <returns>C3D参数组字典</returns>
        public static C3DParameterDictionary CreateEmptyParameterDictionary()
        {
            return new C3DParameterDictionary();
        }

        /// <summary>
        /// 创建新的C3D参数组字典
        /// </summary>
        /// <returns>C3D参数组字典</returns>
        public static C3DParameterDictionary CreateNewParameterDictionary()
        {
            C3DParameterDictionary dict = new C3DParameterDictionary();

            C3DParameterGroup groupPoint = dict.SetGroup(1, "POINT", "");
            groupPoint.Add<UInt16>("USED", "", C3DConstants.DEFAULT_POINT_USED);
            groupPoint.Add<Single>("SCALE", "", C3DConstants.DEFAULT_POINT_SCALE);
            groupPoint.Add<Single>("RATE", "", C3DConstants.DEFAULT_POINT_RATE);
            groupPoint.Add<UInt16>("DATA_START", "", C3DConstants.FILE_DEFAULT_FIRST_PARAM_SECTION);
            groupPoint.Add<UInt16>("FRAMES", "", C3DConstants.DEFAULT_POINT_LAST_FRAME - C3DConstants.DEFAULT_POINT_FIRST_FRAME + 1);

            C3DParameterGroup groupAnalog = dict.SetGroup(2, "ANALOG", "");
            groupAnalog.Add<UInt16>("USED", "", C3DConstants.DEFAULT_ANALOG_USED);
            groupAnalog.Add<Single>("RATE", "", C3DConstants.DEFAULT_ANALOG_RATE);

            C3DParameterGroup groupForcePlatform = dict.SetGroup(3, "FORCE_PLATFORM", "");

            return dict;
        }

        /// <summary>
        /// 从已有参数组列表里创建C3D参数组字典
        /// </summary>
        /// <param name="groups">C3D参数组字典</param>
        /// <param name="parameters">C3D参数项字典</param>
        /// <returns>C3D参数组字典</returns>
        internal static C3DParameterDictionary CreateParameterDictionaryFromParameterList(IDictionary<Int32, C3DParameterGroup> groups, IDictionary<Int32, List<C3DParameter>> parameters)
        {
            C3DParameterDictionary dict = new C3DParameterDictionary();

            if (groups == null || groups.Count <= 0)
            {
                return dict;
            }

            foreach (KeyValuePair<Int32, C3DParameterGroup> pair in groups)
            {
                C3DParameterGroup group = pair.Value;
                List<C3DParameter> groupParameters = null;

                if (group != null && group.ID < 0)
                {
                    if (parameters.TryGetValue(-group.ID, out groupParameters) && groupParameters != null)
                    {
                        for (Int32 i = 0; i < groupParameters.Count; i++)
                        {
                            group[groupParameters[i].Name] = groupParameters[i];
                        }
                    }

                    dict._paramGroups[-group.ID] = group;
                }
            }

            return dict;
        }
        #endregion
    }
}