using System;

namespace C3D.DataViewer.Gesture
{
    /// <summary>
    /// 鼠标手势事件数据
    /// </summary>
    public class MouseGestureEventArgs : EventArgs
    {
        #region 字段
        private Int32 _delta;
        #endregion

        #region 属性
        /// <summary>
        /// 获取鼠标移动差量值
        /// </summary>
        public Int32 Delta
        {
            get { return this._delta; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的鼠标手势事件数据
        /// </summary>
        /// <param name="delta">变化差异</param>
        public MouseGestureEventArgs(Int32 delta)
        {
            this._delta = delta;
        }
        #endregion
    }
}