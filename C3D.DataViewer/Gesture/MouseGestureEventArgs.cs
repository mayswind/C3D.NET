using System;

namespace C3D.DataViewer.Gesture
{
    /// <summary>
    /// 鼠标手势事件数据
    /// </summary>
    public class MouseGestureEventArgs : EventArgs
    {
        /// <summary>
        /// 差量值
        /// </summary>
        public Int32 Delta { get; set; }

        /// <summary>
        /// 初始化新的鼠标手势事件数据
        /// </summary>
        /// <param name="delta">变化差异</param>
        public MouseGestureEventArgs(Int32 delta)
        {
            this.Delta = delta;
        }
    }
}