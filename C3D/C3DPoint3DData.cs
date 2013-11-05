using System;
using System.Text;

using C3D.Number;

namespace C3D
{
    /// <summary>
    /// C3D 3D坐标点结构体
    /// </summary>
    public struct C3DPoint3DData
    {
        #region 字段
        private Single _x;
        private Single _y;
        private Single _z;
        private Byte _cameraMask;
        private Single _residual;
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置X轴坐标点
        /// </summary>
        public Single X
        {
            get { return this._x; }
            set { this._x = value; }
        }

        /// <summary>
        /// 获取或设置Y轴坐标点
        /// </summary>
        public Single Y
        {
            get { return this._y; }
            set { this._y = value; }
        }

        /// <summary>
        /// 获取或设置Z轴坐标点
        /// </summary>
        public Single Z
        {
            get { return this._z; }
            set { this._z = value; }
        }

        /// <summary>
        /// 获取或设置相机ID
        /// </summary>
        public Byte CameraMask
        {
            get { return this._cameraMask; }
            set { this._cameraMask = value; }
        }

        /// <summary>
        /// 获取相机ID信息
        /// </summary>
        /// <returns>相机ID信息</returns>
        public String CameraMaskInfo
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                for (Int32 i = 0; i < 7; i++)
                {
                    sb.Append((this._cameraMask & (Int32)Math.Pow(2, i)) >> i);
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// 获取或设置Residual
        /// </summary>
        public Single Residual
        {
            get { return this._residual; }
            set { this._residual = value; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化C3D 3D坐标点结构体
        /// </summary>
        /// <param name="x">X轴坐标点</param>
        /// <param name="y">Y轴坐标点</param>
        /// <param name="z">Z轴坐标点</param>
        /// <param name="lastPart">剩余部分</param>
        /// <param name="scaleFactor">比例因子</param>
        public C3DPoint3DData(Int16 x, Int16 y, Int16 z, Int16 lastPart, Single scaleFactor)
        {
            Byte[] data = C3DBitConverter.GetBytes(lastPart);

            if (lastPart > -1)
            {
                this._x = x * scaleFactor;
                this._y = y * scaleFactor;
                this._z = z * scaleFactor;
                this._residual = Math.Abs((SByte)data[0] * scaleFactor);
                this._cameraMask = (Byte)(data[1] & 0x7F);
            }
            else
            {
                this._x = this._y = this._z = 0;
                this._residual = -1.0F;
                this._cameraMask = 0;
            }
        }

        /// <summary>
        /// 初始化C3D 3D坐标点结构体
        /// </summary>
        /// <param name="x">X轴坐标点</param>
        /// <param name="y">Y轴坐标点</param>
        /// <param name="z">Z轴坐标点</param>
        /// <param name="lastPart">剩余部分</param>
        /// <param name="scaleFactor">比例因子</param>
        public C3DPoint3DData(Single x, Single y, Single z, Single lastPart, Single scaleFactor)
        {
            Byte[] data = C3DBitConverter.GetBytes((Int16)lastPart);

            if ((Int16)lastPart > -1)
            {
                this._x = x;
                this._y = y;
                this._z = z;
                this._residual = Math.Abs((SByte)data[0] * scaleFactor);
                this._cameraMask = (Byte)(data[1] & 0x7F);
            }
            else
            {
                this._x = this._y = this._z = 0;
                this._residual = -1.0F;
                this._cameraMask = 0;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 设置指定相机是否被使用
        /// </summary>
        /// <param name="cameraID">相机ID(从1开始计数)</param>
        /// <param name="isUsed">是否被使用</param>
        /// <exception cref="ArgumentOutOfRangeException">相机ID超出范围</exception>
        public void SetCameraUsed(Byte cameraID, Boolean isUsed)
        {
            if (cameraID < 1 || cameraID > 7)
            {
                throw new ArgumentOutOfRangeException("Camera ID is INVALID!");
            }

            if (isUsed)
            {
                this._cameraMask = (Byte)(this._cameraMask | (1 << (cameraID - 1)));
            }
            else
            {
                this._cameraMask = (Byte)(this._cameraMask & (0x7F - (1 << (cameraID - 1))));
            }
        }

        /// <summary>
        /// 设置指定相机是否被使用
        /// </summary>
        /// <param name="isUsed">是否被使用</param>
        /// <param name="cameraIDs">相机ID组(从1开始计数)</param>
        public void SetCameraUsed(Boolean isUsed, params Byte[] cameraIDs)
        {
            if (cameraIDs == null && cameraIDs.Length <= 0)
            {
                return;
            }

            for (Int32 i = 0; i < cameraIDs.Length; i++)
            {
                this.SetCameraUsed(cameraIDs[i], isUsed);
            }
        }

        /// <summary>
        /// 获取指定相机是否被使用
        /// </summary>
        /// <param name="cameraID">相机ID(从1开始计数)</param>
        /// <exception cref="ArgumentOutOfRangeException">相机ID超出范围</exception>
        /// <returns>是否被使用</returns>
        public Boolean GetCameraUsed(Byte cameraID)
        {
            if (cameraID < 1 || cameraID > 7)
            {
                throw new ArgumentOutOfRangeException("Camera ID is INVALID!");
            }

            Int32 mask = 1 << (cameraID - 1);
            return (this._cameraMask & mask) == mask;
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 获取整型剩余部分
        /// </summary>
        /// <param name="scaleFactor">比例因子</param>
        /// <returns>剩余部分</returns>
        internal Int16 GetIntLastPart(Single scaleFactor)
        {
            if (this._residual == -1.0F)
            {
                return -1;//0xFFFF
            }
            else
            {
                Byte[] data = new Byte[2];
                data[0] = (Byte)Math.Round(Math.Abs(this._residual / scaleFactor), MidpointRounding.AwayFromZero);
                data[1] = (Byte)(this._cameraMask);

                return C3DBitConverter.ToInt16(data);
            }
        }

        /// <summary>
        /// 获取浮点型剩余部分
        /// </summary>
        /// <param name="scaleFactor">比例因子</param>
        /// <returns>剩余部分</returns>
        internal Single GetFloatLastPart(Single scaleFactor)
        {
            return (Single)this.GetIntLastPart(scaleFactor);
        }
        #endregion

        #region 重载方法
        public override String ToString()
        {
            return String.Format("C3DPoint3DData, [{0}, {1}, {2}]", this._x.ToString(), this._y.ToString(), this._z.ToString());
        }
        #endregion
    }
}