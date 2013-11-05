using System;
using System.Text;

using C3D.Number;

namespace C3D
{
    /// <summary>
    /// C3D参数项类
    /// </summary>
    public sealed class C3DParameter : AbstractC3DParameter
    {
        #region 字段
        private C3DParameterType _parameterType;
        private Byte[] _dimensions;
        private Byte[] _parameterData;
        #endregion

        #region 属性
        /// <summary>
        /// 获取参数类型
        /// </summary>
        public C3DParameterType C3DParameterType
        {
            get { return this._parameterType; }
        }

        /// <summary>
        /// 自动获取第1个参数数据
        /// </summary>
        public Object InnerData
        {
            get { return this.GetData(); }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的C3D参数项
        /// </summary>
        /// <param name="processorType">原处理器类型</param>
        /// <param name="id">参数项ID</param>
        /// <param name="name">参数项名称数据块</param>
        /// <param name="isLocked">是否锁定</param>
        /// <param name="lastData">最后一部分数据块</param>
        internal C3DParameter(C3DProcessorType processorType, SByte id, Byte[] name, Boolean isLocked, Byte[] lastData)
            : base(id, name, isLocked)
        {
            if (lastData == null)
            {
                this._parameterType = C3DParameterType.Invalid;
                this._dimensions = null;
                this._parameterData = null;
                return;
            }

            this._parameterType = (C3DParameterType)lastData[0];
            Int32 dataLength = 1, dimension = lastData[1];

            if (dimension > 0)
            {
                this._dimensions = new Byte[dimension];

                for (Byte i = 0; i < dimension; i++)
                {
                    this._dimensions[i] = lastData[2 + i];
                    dataLength *= this._dimensions[i];
                }

                dataLength *= GetParameterDataSize(this._parameterType);
            }
            else
            {
                this._dimensions = null;
                dataLength = GetParameterDataSize(this._parameterType);
            }

            this._parameterData = new Byte[dataLength];
            Array.Copy(lastData, 2 + dimension, this._parameterData, 0, this._parameterData.Length);

            //修正不同处理器文档格式问题
            if (this._parameterType == C3DParameterType.Int16 || this._parameterType == C3DParameterType.Single)
            {
                this.UpdateData(this._parameterType, processorType, dimension);
            }

            this.SetDescription(lastData, 2 + dimension + this._parameterData.Length);
        }

        /// <summary>
        /// 初始化新的C3D参数项
        /// </summary>
        /// <param name="id">参数项ID</param>
        /// <param name="name">参数项名称</param>
        /// <param name="description">参数项描述</param>
        internal C3DParameter(SByte id, String name, String description)
            : base(id, name, description)
        { }
        #endregion

        #region GetData
        /// <summary>
        /// 自动获取第1个参数数据
        /// </summary>
        /// <returns>参数数据</returns>
        public Object GetData()
        {
            switch (this._parameterType)
            {
                case C3DParameterType.Invalid:
                    return null;
                case C3DParameterType.Char:
                    if (this._dimensions == null || this._dimensions.Length == 0) return this.GetData<Char>();
                    else if (this._dimensions.Length == 1) return this.GetData<String>();
                    else if (this._dimensions.Length == 2) return this.GetData<String[]>();
                    else if (this._dimensions.Length == 3) return this.GetData<Char[, ,]>();
                    break;
                case C3DParameterType.Byte:
                    if (this._dimensions == null || this._dimensions.Length == 0) return this.GetData<Byte>();
                    else if (this._dimensions.Length == 1) return this.GetData<Byte[]>();
                    else if (this._dimensions.Length == 2) return this.GetData<Byte[,]>();
                    else if (this._dimensions.Length == 3) return this.GetData<Byte[, ,]>();
                    break;
                case C3DParameterType.Int16:
                    if (this._dimensions == null || this._dimensions.Length == 0) return this.GetData<Int16>();
                    else if (this._dimensions.Length == 1) return this.GetData<Int16[]>();
                    else if (this._dimensions.Length == 2) return this.GetData<Int16[,]>();
                    else if (this._dimensions.Length == 3) return this.GetData<Int16[, ,]>();
                    break;
                case C3DParameterType.Single:
                    if (this._dimensions == null || this._dimensions.Length == 0) return this.GetData<Single>();
                    else if (this._dimensions.Length == 1) return this.GetData<Single[]>();
                    else if (this._dimensions.Length == 2) return this.GetData<Single[,]>();
                    else if (this._dimensions.Length == 3) return this.GetData<Single[, ,]>();
                    break;
            }

            throw new C3DException("Cannot get data automatically.");
        }

        /// <summary>
        /// 获取第1个参数数据
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <returns>参数数据</returns>
        public T GetData<T>()
        {
            return GetData<T>(0);
        }

        /// <summary>
        /// 获取指定索引的参数数据
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="index">指定索引(从0开始计数)</param>
        /// <exception cref="C3DException">数据类型错误</exception>
        /// <returns>参数数据</returns>
        public T GetData<T>(Int32 index)
        {
            #region Base Type
            if (typeof(T) == typeof(Char))
            {
                return (T)(Object)(Char)this._parameterData[index];
            }

            if (typeof(T) == typeof(Byte))
            {
                return (T)(Object)this._parameterData[index];
            }

            if (typeof(T) == typeof(Int16))
            {
                return (T)(Object)C3DBitConverter.ToInt16(this._parameterData, index * sizeof(Int16));
            }

            if (typeof(T) == typeof(Single))
            {
                return (T)(Object)C3DBitConverter.ToSingle(this._parameterData, index * sizeof(Single));
            }

            if (typeof(T) == typeof(String))
            {
                if (this._dimensions == null || this._dimensions.Length != 1 || this._parameterType != C3DParameterType.Char)
                {
                    throw new C3DException("Parameter \"" + this.Name + "\" is not string type.");
                }

                return (T)(Object)this.ReadStringFromBytes(this._parameterData, 0, this._dimensions[0]);
            }
            #endregion

            #region 1D-Array
            if (typeof(T) == typeof(Char[]))
            {
                return (T)(Object)this.Get1DArray<Char>();
            }
            
            if (typeof(T) == typeof(Byte[]))
            {
                return (T)(Object)this.Get1DArray<Byte>();
            }
            
            if (typeof(T) == typeof(Int16[]))
            {
                return (T)(Object)this.Get1DArray<Int16>();
            }
            
            if (typeof(T) == typeof(Single[]))
            {
                return (T)(Object)this.Get1DArray<Single>();
            }
            if (typeof(T) == typeof(String[]))
            {
                if (this._dimensions == null || this._dimensions.Length != 2 || this._parameterType != C3DParameterType.Char)
                {
                    throw new C3DException("Parameter \"" + Name + "\" is not string array type.");
                }

                String[] ret = new String[this._dimensions[1]];

                for (Int32 i = 0; i < this._dimensions[1]; i++)
                {
                    ret[i] = this.ReadStringFromBytes(this._parameterData, i * this._dimensions[0], this._dimensions[0]);
                }

                return (T)(Object)ret;
            }
            #endregion

            #region 2D-Array
            if (typeof(T) == typeof(Char[,]))
            {
                return (T)(Object)this.Get2DArray<Char>();
            }

            if (typeof(T) == typeof(Byte[,]))
            {
                return (T)(Object)this.Get2DArray<Byte>();
            }

            if (typeof(T) == typeof(Int16[,]))
            {
                return (T)(Object)this.Get2DArray<Int16>();
            }

            if (typeof(T) == typeof(Single[,]))
            {
                return (T)(Object)this.Get2DArray<Single>();
            }
            #endregion

            #region 3D-Array
            if (typeof(T) == typeof(Char[, ,]))
            {
                return (T)(Object)this.Get3DArray<Char>();
            }

            if (typeof(T) == typeof(Byte[, ,]))
            {
                return (T)(Object)this.Get3DArray<Byte>();
            }

            if (typeof(T) == typeof(Int16[, ,]))
            {
                return (T)(Object)this.Get3DArray<Int16>();
            }

            if (typeof(T) == typeof(Single[, ,]))
            {
                return (T)(Object)this.Get3DArray<Single>();
            }
            #endregion

            throw new C3DException("Parameter type is unknown.");
        }

        #region GetArray
        private T[] Get1DArray<T>()
        {
            if (this._dimensions == null || this._dimensions.Length != 1)
            {
                throw new C3DException("Parameter \"" + Name + "\" is not 1D array.");
            }

            T[] ret = new T[this._dimensions[0]];

            for (Int32 index = 0; index < this._dimensions[0]; index++)
            {
                ret[index] = this.GetData<T>(index);
            }

            return ret;
        }

        private T[,] Get2DArray<T>()
        {
            if (this._dimensions == null || this._dimensions.Length != 2)
            {
                throw new C3DException("Parameter \"" + Name + "\" is not 2D array.");
            }

            T[,] ret = new T[this._dimensions[0], this._dimensions[1]];
            Int32 index = 0;

            for (Int32 x = 0; x < this._dimensions[0]; x++)
            {
                for (Int32 y = 0; y < this._dimensions[1]; y++)
                {
                    ret[x, y] = GetData<T>(index++);
                }
            }
            return ret;
        }

        private T[, ,] Get3DArray<T>()
        {
            if (this._dimensions == null || this._dimensions.Length != 3)
            {
                throw new C3DException("Parameter \"" + Name + "\" is not 3D array.");
            }

            T[, ,] ret = new T[this._dimensions[0], this._dimensions[1], this._dimensions[2]];
            Int32 index = 0;

            for (Int32 x = 0; x < this._dimensions[0]; x++)
            {
                for (Int32 y = 0; y < this._dimensions[1]; y++)
                {
                    for (Int32 z = 0; z < this._dimensions[2]; z++)
                    {
                        ret[x, y, z] = GetData<T>(index++);
                    }
                }
            }
            return ret;
        }
        #endregion

        #region GetByteArray
        private Byte[][] Get1DByteArray(Int32 size)
        {
            if (this._dimensions == null || this._dimensions.Length != 1)
            {
                throw new C3DException("Parameter \"" + Name + "\" is not 1D array.");
            }

            Byte[][] ret = new Byte[this._dimensions[0]][];

            for (Int32 index = 0; index < this._dimensions[0]; index++)
            {
                ret[index] = new Byte[size];

                for (Int32 i = 0; i < size; i++)
                {
                    ret[index][i] = this._parameterData[index * size + i];
                }
            }

            return ret;
        }

        private Byte[,][] Get2DByteArray(Int32 size)
        {
            if (this._dimensions == null || this._dimensions.Length != 2)
            {
                throw new C3DException("Parameter \"" + Name + "\" is not 2D array.");
            }

            Byte[,][] ret = new Byte[this._dimensions[0], this._dimensions[1]][];
            Int32 index = 0;

            for (Int32 x = 0; x < this._dimensions[0]; x++)
            {
                for (Int32 y = 0; y < this._dimensions[1]; y++)
                {
                    ret[x, y] = new Byte[size];

                    for (Int32 i = 0; i < size; i++)
                    {
                        ret[x, y][i] = this._parameterData[index * size + i];
                    }

                    index++;
                }
            }

            return ret;
        }

        private Byte[, ,][] Get3DByteArray(Int32 size)
        {
            if (this._dimensions == null || this._dimensions.Length != 3)
            {
                throw new C3DException("Parameter \"" + Name + "\" is not 3D array.");
            }

            Byte[, ,][] ret = new Byte[this._dimensions[0], this._dimensions[1], this._dimensions[2]][];
            Int32 index = 0;

            for (Int32 x = 0; x < this._dimensions[0]; x++)
            {
                for (Int32 y = 0; y < this._dimensions[1]; y++)
                {
                    for (Int32 z = 0; z < this._dimensions[2]; z++)
                    {
                        ret[x, y, z] = new Byte[size];

                        for (Int32 i = 0; i < size; i++)
                        {
                            ret[x, y, z][i] = this._parameterData[index * size + i];
                        }

                        index++;
                    }
                }
            }

            return ret;
        }
        #endregion
        #endregion

        #region SetData
        /// <summary>
        /// 设置维度信息
        /// </summary>
        /// <param name="data">维度信息</param>
        private void SetDimension(params Byte[] data)
        {
            this._dimensions = (data == null || data.Length == 0) ? null : data;
        }

        /// <summary>
        /// 设置参数数据
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="data">参数数据</param>
        /// <exception cref="NullReferenceException">不允许设置空数据</exception>
        /// <exception cref="C3DException">参数已被锁定</exception>
        /// <exception cref="C3DException">未知数据类型</exception>
        public void SetData<T>(T data)
        {
            if (data == null)
            {
                throw new NullReferenceException("Cannot set null data.");
            }

            if (this.IsLocked)
            {
                throw new C3DException("This parameter is locked, you cannot modify it.");
            }

            this.InternalSetData<T>(data);
        }

        /// <summary>
        /// 设置参数数据
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="data">参数数据</param>
        /// <exception cref="C3DException">未知数据类型</exception>
        internal void InternalSetData<T>(T data)
        {
            #region Basic Type
            if (typeof(T) == typeof(Char))
            {
                this.SetDimension();
                this._parameterType = C3DParameterType.Char;
                this._parameterData = new Byte[1] { Convert.ToByte(data) };
                return;
            }

            if (typeof(T) == typeof(Byte))
            {
                this.SetDimension();
                this._parameterType = C3DParameterType.Byte;
                this._parameterData = new Byte[1] { Convert.ToByte(data) };
                return;
            }

            if (typeof(T) == typeof(Int16))
            {
                this.SetDimension();
                this._parameterType = C3DParameterType.Int16;
                this._parameterData = C3DBitConverter.GetBytes((Int16)(Object)data);
                return;
            }

            if (typeof(T) == typeof(Single))
            {
                this.SetDimension();
                this._parameterType = C3DParameterType.Single;
                this._parameterData = C3DBitConverter.GetBytes((Single)(Object)data);
                return;
            }

            if (typeof(T) == typeof(String))
            {
                String s = (String)(Object)data;

                this.SetDimension((Byte)s.Length);
                this._parameterType = C3DParameterType.Char;
                this._parameterData = this.WrtieStringToBytes(s);
                return;
            }
            #endregion

            #region 1D-Array
            if (typeof(T) == typeof(Char[]))
            {
                Char[] array = ((Char[])(Object)data);
                Int32 size = this.GetParameterDataSize(C3DParameterType.Char);

                this.SetDimension((Byte)array.Length);
                this._parameterType = C3DParameterType.Char;
                this._parameterData = new Byte[array.Length * size];

                for (Int32 i = 0; i < array.Length; i++)
                {
                    this._parameterData[i] = (Byte)array[i];
                }

                return;
            }

            if (typeof(T) == typeof(Byte[]))
            {
                Byte[] array = ((Byte[])(Object)data);
                Int32 size = this.GetParameterDataSize(C3DParameterType.Byte);

                this.SetDimension((Byte)array.Length);
                this._parameterType = C3DParameterType.Byte;
                this._parameterData = new Byte[array.Length * size];

                for (Int32 i = 0; i < array.Length; i++)
                {
                    this._parameterData[i] = array[i];
                }

                return;
            }

            if (typeof(T) == typeof(Int16[]))
            {
                Int16[] array = ((Int16[])(Object)data);
                Int32 size = this.GetParameterDataSize(C3DParameterType.Int16);

                this.SetDimension((Byte)array.Length);
                this._parameterType = C3DParameterType.Int16;
                this._parameterData = new Byte[array.Length * size];

                for (Int32 i = 0; i < array.Length; i++)
                {
                    Array.Copy(C3DBitConverter.GetBytes(array[i]), 0, this._parameterData, i * size, size);
                }

                return;
            }

            if (typeof(T) == typeof(Single[]))
            {
                Single[] array = ((Single[])(Object)data);
                Int32 size = this.GetParameterDataSize(C3DParameterType.Single);

                this.SetDimension((Byte)array.Length);
                this._parameterType = C3DParameterType.Single;
                this._parameterData = new Byte[array.Length * size];

                for (Int32 i = 0; i < array.Length; i++)
                {
                    Array.Copy(C3DBitConverter.GetBytes(array[i]), 0, this._parameterData, i * size, size);
                }

                return;
            }

            if (typeof(T) == typeof(String[]))
            {
                String[] array = ((String[])(Object)data);
                Byte count = (Byte)array.Length, maxLen = 0;

                for (Int32 i = 0; i < array.Length; i++)
                {
                    maxLen = Math.Max((Byte)array[i].Length, maxLen);
                }

                this.SetDimension(maxLen, count);
                this._parameterType = C3DParameterType.Char;
                this._parameterData = new Byte[count * maxLen];

                for (Int32 i = 0; i < this._parameterData.Length; i++)
                {
                    this._parameterData[i] = 0x20;//全部初始化为空格
                }

                for (Int32 i = 0; i < count; i++)
                {
                    this.WrtieStringToBytes(array[i], 0, array[i].Length, this._parameterData, i * maxLen);
                }

                return;
            }
            #endregion

            #region 2D-Array
            if (typeof(T) == typeof(Char[,]))
            {
                Char[,] array = ((Char[,])(Object)data);
                Int32 size = this.GetParameterDataSize(C3DParameterType.Char);

                this.SetDimension((Byte)array.GetLength(0), (Byte)array.GetLength(1));
                this._parameterType = C3DParameterType.Char;
                this._parameterData = new Byte[array.Length * size];

                Int32 index = 0;
                for (Int32 x = 0; x < array.GetLength(0); x++)
                {
                    for (Int32 y = 0; y < array.GetLength(1); y++)
                    {
                        this._parameterData[index++] = (Byte)array[x, y];
                    }
                }

                return;
            }

            if (typeof(T) == typeof(Byte[,]))
            {
                Byte[,] array = ((Byte[,])(Object)data);
                Int32 size = this.GetParameterDataSize(C3DParameterType.Byte);

                this.SetDimension((Byte)array.GetLength(0), (Byte)array.GetLength(1));
                this._parameterType = C3DParameterType.Byte;
                this._parameterData = new Byte[array.Length * size];

                Int32 index = 0;
                for (Int32 x = 0; x < array.GetLength(0); x++)
                {
                    for (Int32 y = 0; y < array.GetLength(1); y++)
                    {
                        this._parameterData[index++] = array[x, y];
                    }
                }

                return;
            }

            if (typeof(T) == typeof(Int16[,]))
            {
                Int16[,] array = ((Int16[,])(Object)data);
                Int32 size = this.GetParameterDataSize(C3DParameterType.Int16);

                this.SetDimension((Byte)array.GetLength(0), (Byte)array.GetLength(1));
                this._parameterType = C3DParameterType.Int16;
                this._parameterData = new Byte[array.Length * size];

                Int32 index = 0;
                for (Int32 x = 0; x < array.GetLength(0); x++)
                {
                    for (Int32 y = 0; y < array.GetLength(1); y++)
                    {
                        Array.Copy(C3DBitConverter.GetBytes(array[x, y]), 0, this._parameterData, index++ * size, size);
                    }
                }

                return;
            }

            if (typeof(T) == typeof(Single[,]))
            {
                Single[,] array = ((Single[,])(Object)data);
                Int32 size = this.GetParameterDataSize(C3DParameterType.Single);

                this.SetDimension((Byte)array.GetLength(0), (Byte)array.GetLength(1));
                this._parameterType = C3DParameterType.Single;
                this._parameterData = new Byte[array.Length * size];

                Int32 index = 0;
                for (Int32 x = 0; x < array.GetLength(0); x++)
                {
                    for (Int32 y = 0; y < array.GetLength(1); y++)
                    {
                        Array.Copy(C3DBitConverter.GetBytes(array[x, y]), 0, this._parameterData, index++ * size, size);
                    }
                }

                return;
            }
            #endregion

            #region 3D-Array
            if (typeof(T) == typeof(Char[, ,]))
            {
                Char[, ,] array = ((Char[, ,])(Object)data);
                Int32 size = this.GetParameterDataSize(C3DParameterType.Char);

                this.SetDimension((Byte)array.GetLength(0), (Byte)array.GetLength(1), (Byte)array.GetLength(2));
                this._parameterType = C3DParameterType.Char;
                this._parameterData = new Byte[array.Length * size];

                Int32 index = 0;
                for (Int32 x = 0; x < array.GetLength(0); x++)
                {
                    for (Int32 y = 0; y < array.GetLength(1); y++)
                    {
                        for (Int32 z = 0; z < array.GetLength(2); z++)
                        {
                            this._parameterData[index++] = (Byte)array[x, y, z];
                        }
                    }
                }

                return;
            }

            if (typeof(T) == typeof(Byte[, ,]))
            {
                Byte[, ,] array = ((Byte[, ,])(Object)data);
                Int32 size = this.GetParameterDataSize(C3DParameterType.Byte);

                this.SetDimension((Byte)array.GetLength(0), (Byte)array.GetLength(1), (Byte)array.GetLength(2));
                this._parameterType = C3DParameterType.Byte;
                this._parameterData = new Byte[array.Length * size];

                Int32 index = 0;
                for (Int32 x = 0; x < array.GetLength(0); x++)
                {
                    for (Int32 y = 0; y < array.GetLength(1); y++)
                    {
                        for (Int32 z = 0; z < array.GetLength(2); z++)
                        {
                            this._parameterData[index++] = array[x, y, z];
                        }
                    }
                }

                return;
            }

            if (typeof(T) == typeof(Int16[, ,]))
            {
                Int16[, ,] array = ((Int16[, ,])(Object)data);
                Int32 size = this.GetParameterDataSize(C3DParameterType.Int16);

                this.SetDimension((Byte)array.GetLength(0), (Byte)array.GetLength(1), (Byte)array.GetLength(2));
                this._parameterType = C3DParameterType.Int16;
                this._parameterData = new Byte[array.Length * size];

                Int32 index = 0;
                for (Int32 x = 0; x < array.GetLength(0); x++)
                {
                    for (Int32 y = 0; y < array.GetLength(1); y++)
                    {
                        for (Int32 z = 0; z < array.GetLength(2); z++)
                        {
                            Array.Copy(C3DBitConverter.GetBytes(array[x, y, z]), 0, this._parameterData, index++ * size, size);
                        }
                    }
                }

                return;
            }

            if (typeof(T) == typeof(Single[, ,]))
            {
                Single[, ,] array = ((Single[, ,])(Object)data);
                Int32 size = this.GetParameterDataSize(C3DParameterType.Single);

                this.SetDimension((Byte)array.GetLength(0), (Byte)array.GetLength(1), (Byte)array.GetLength(2));
                this._parameterType = C3DParameterType.Single;
                this._parameterData = new Byte[array.Length * size];

                Int32 index = 0;
                for (Int32 x = 0; x < array.GetLength(0); x++)
                {
                    for (Int32 y = 0; y < array.GetLength(1); y++)
                    {
                        for (Int32 z = 0; z < array.GetLength(2); z++)
                        {
                            Array.Copy(C3DBitConverter.GetBytes(array[x, y, z]), 0, this._parameterData, index++ * size, size);
                        }
                    }
                }

                return;
            }
            #endregion

            throw new C3DException("Parameter type is unknown.");
        }
        #endregion

        #region GetLastDataArrayWithoutDescrption
        /// <summary>
        /// 获取不包含描述的最后数据块
        /// </summary>
        /// <returns>不包含描述的最后数据块</returns>
        internal override Byte[] GetLastDataArrayWithoutDescrption()
        {
            if (this._parameterType == C3DParameterType.Invalid)
            {
                return null;
            }

            Byte[] data = new Byte[2 + (this._dimensions == null ? 0 : this._dimensions.Length) + (this._parameterData == null ? 0 : this._parameterData.Length)];

            data[0] = (Byte)this._parameterType;
            data[1] = (Byte)(this._dimensions == null ? 0 : this._dimensions.Length);

            if (data[1] > 0)
            {
                Array.Copy(this._dimensions, 0, data, 2, this._dimensions.Length);
            }

            Array.Copy(this._parameterData, 0, data, 2 + data[1], this._parameterData.Length);

            return data;
        }
        #endregion

        #region UpdateData
        /// <summary>
        /// 更新参数内数据
        /// </summary>
        /// <param name="parameterType">参数类型</param>
        /// <param name="processorType">处理器类型</param>
        /// <param name="dimensionCount">维数</param>
        private void UpdateData(C3DParameterType parameterType, C3DProcessorType processorType, Int32 dimensionCount)
        {
            #region Basic Type
            if (dimensionCount == 0)
            {
                if (parameterType == C3DParameterType.Int16)
                {
                    this.InternalSetData<Int16>(C3DBitConverter.ToInt16(processorType, this._parameterData));
                }
                else if (parameterType == C3DParameterType.Single)
                {
                    this.InternalSetData<Single>(C3DBitConverter.ToSingle(processorType, this._parameterData));
                }

                return;
            }
            #endregion

            #region 1D-Array
            if (dimensionCount == 1)
            {
                if (parameterType == C3DParameterType.Int16)
                {
                    Byte[][] datas = this.Get1DByteArray(sizeof(Int16));
                    Int16[] newdata = new Int16[datas.Length];

                    for (Int32 i = 0; i < datas.Length; i++)
                    {
                        newdata[i] = C3DBitConverter.ToInt16(processorType, datas[i]);
                    }

                    this.InternalSetData<Int16[]>(newdata);
                }
                else if (parameterType == C3DParameterType.Single)
                {
                    Byte[][] datas = this.Get1DByteArray(sizeof(Single));
                    Single[] newdata = new Single[datas.Length];

                    for (Int32 i = 0; i < datas.Length; i++)
                    {
                        newdata[i] = C3DBitConverter.ToSingle(processorType, datas[i]);
                    }

                    this.InternalSetData<Single[]>(newdata);
                }

                return;
            }
            #endregion

            #region 2D-Array
            if (dimensionCount == 2)
            {
                if (parameterType == C3DParameterType.Int16)
                {
                    Byte[,][] datas = this.Get2DByteArray(sizeof(Int16));
                    Int16[,] newdata = new Int16[datas.GetLength(0), datas.GetLength(1)];

                    for (Int32 x = 0; x < datas.GetLength(0); x++)
                    {
                        for (Int32 y = 0; y < datas.GetLength(1); y++)
                        {
                            newdata[x, y] = C3DBitConverter.ToInt16(processorType, datas[x, y]);
                        }
                    }

                    this.InternalSetData<Int16[,]>(newdata);
                }
                else if (parameterType == C3DParameterType.Single)
                {
                    Byte[,][] datas = this.Get2DByteArray(sizeof(Single));
                    Single[,] newdata = new Single[datas.GetLength(0), datas.GetLength(1)];

                    for (Int32 x = 0; x < datas.GetLength(0); x++)
                    {
                        for (Int32 y = 0; y < datas.GetLength(1); y++)
                        {
                            newdata[x, y] = C3DBitConverter.ToSingle(processorType, datas[x, y]);
                        }
                    }

                    this.InternalSetData<Single[,]>(newdata);
                }

                return;
            }
            #endregion

            #region 3D-Array
            if (dimensionCount == 3)
            {
                if (parameterType == C3DParameterType.Int16)
                {
                    Byte[, ,][] datas = this.Get3DByteArray(sizeof(Int16));
                    Int16[, ,] newdata = new Int16[datas.GetLength(0), datas.GetLength(1), datas.GetLength(2)];

                    for (Int32 x = 0; x < datas.GetLength(0); x++)
                    {
                        for (Int32 y = 0; y < datas.GetLength(1); y++)
                        {
                            for (Int32 z = 0; z < datas.GetLength(2); z++)
                            {
                                newdata[x, y, z] = C3DBitConverter.ToInt16(processorType, datas[x, y, z]);
                            }
                        }
                    }

                    this.InternalSetData<Int16[, ,]>(newdata);
                }
                else if (parameterType == C3DParameterType.Single)
                {
                    Byte[, ,][] datas = this.Get3DByteArray(sizeof(Single));
                    Single[, ,] newdata = new Single[datas.GetLength(0), datas.GetLength(1), datas.GetLength(2)];

                    for (Int32 x = 0; x < datas.GetLength(0); x++)
                    {
                        for (Int32 y = 0; y < datas.GetLength(1); y++)
                        {
                            for (Int32 z = 0; z < datas.GetLength(2); z++)
                            {
                                newdata[x, y, z] = C3DBitConverter.ToSingle(processorType, datas[x, y, z]);
                            }
                        }
                    }

                    this.InternalSetData<Single[, ,]>(newdata);
                }

                return;
            }
            #endregion
        }
        #endregion

        #region 重载方法
        public override String ToString()
        {
            return String.Format("{0}, Name = {1}, Type = {2}", this.GetType().ToString(), this.Name, this._parameterType.ToString());
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取参数数据大小
        /// </summary>
        /// <param name="type">参数数据类型</param>
        /// <returns>参数数据大小</returns>
        private Int32 GetParameterDataSize(C3DParameterType type)
        {
            return Math.Abs((SByte)type);
        }
        #endregion
    }
}