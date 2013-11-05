using System;
using System.Drawing;
using System.Windows.Forms;

namespace C3D.DataViewer.Gesture
{
    /// <summary>
    /// 鼠标手势处理器
    /// </summary>
    public class MouseGestureHandler
    {
        #region 字段
        private Control m_control;
        private Boolean m_isRegistered;
        private Boolean m_isCaptured;
        private Int32 m_lastX;
        private Int32 m_lastY;
        private MouseButtons m_supportButton;
        private MouseEventHandler m_mouseDownHandler;
        private MouseEventHandler m_mouseUpHandler;
        private MouseEventHandler m_mouseMoveHandler;
        #endregion

        #region 事件
        public event MouseGestureToLeft OnMouseGestureToLeft;
        public event MouseGestureToRight OnMouseGestureToRight;
        public event MouseGestureToTop OnMouseGestureToTop;
        public event MouseGestureToBottom OnMouseGestureToBottom;
        public event MouseEventHandler OnMouseGestureUp;
        #endregion

        #region 属性
        /// <summary>
        /// 获取事件是否已注册
        /// </summary>
        public Boolean IsRegistered
        {
            get { return this.m_isRegistered; }
        }

        /// <summary>
        /// 获取或设置支持的按键
        /// </summary>
        public MouseButtons SupportButton
        {
            get { return this.m_supportButton; }
            set { this.m_supportButton = value; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 初始化新的鼠标手势处理器
        /// </summary>
        /// <param name="baseCtl">父控件</param>
        public MouseGestureHandler(Control baseCtl)
        {
            this.m_control = baseCtl;
            this.m_supportButton = MouseButtons.Left;

            this.m_mouseDownHandler = new MouseEventHandler(this.control_MouseDown);
            this.m_mouseUpHandler = new MouseEventHandler(this.control_MouseUp);
            this.m_mouseMoveHandler = new MouseEventHandler(this.control_MouseMove);

            this.RegisterHandler();
        }

        public void RegisterHandler()
        {
            if (this.m_control != null && !this.m_isRegistered)
            {
                this.m_isRegistered = true;

                this.m_control.MouseDown += this.m_mouseDownHandler;
                this.m_control.MouseUp += this.m_mouseUpHandler;
                this.m_control.MouseMove += this.m_mouseMoveHandler;
            }
        }

        public void UnRegistreHandler()
        {
            if (this.m_control != null && this.m_isRegistered)
            {
                this.m_control.MouseDown -= this.m_mouseDownHandler;
                this.m_control.MouseUp -= this.m_mouseUpHandler;
                this.m_control.MouseMove -= this.m_mouseMoveHandler;

                this.m_isRegistered = false;
            }
        }
        #endregion

        #region 事件方法
        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.m_isCaptured && e.Button == this.m_supportButton)
            {
                this.m_isCaptured = true;

                Point p = (this.m_control.Parent == null ? e.Location : this.m_control.Parent.PointToClient(this.m_control.PointToScreen(e.Location)));
                this.m_lastX = p.X;
                this.m_lastY = p.Y;
            }
        }

        private void control_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.m_isCaptured && e.Button == this.m_supportButton)
            {
                this.m_isCaptured = false;

                if (this.OnMouseGestureUp != null)
                {
                    this.OnMouseGestureUp(this, e);
                }
            }
        }

        private void control_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.m_isCaptured && e.Button == this.m_supportButton)
            {
                Point p = (this.m_control.Parent == null ? e.Location : this.m_control.Parent.PointToClient(this.m_control.PointToScreen(e.Location)));

                this.DoMouseGesture(p.X, p.Y);
                this.m_lastX = p.X;
                this.m_lastY = p.Y;
            }
        }
        #endregion

        #region 私有方法
        private void DoMouseGesture(Int32 x, Int32 y)
        {
            MouseGesturedDirection direction = MouseGesturedDirection.None;
            Double dx = x - this.m_lastX;
            Double dy = y - this.m_lastY;
            Double delta = Math.Abs(dy / dx);

            if (delta < 0.50)//左右移动 < 30度
            {
                direction = (dx < 0 ? MouseGesturedDirection.Left : MouseGesturedDirection.Right);
            }
            else if (delta > 0.86)//上下移动 > 60度
            {
                direction = (dy < 0 ? MouseGesturedDirection.Top : MouseGesturedDirection.Bottom);
            }

            if (direction == MouseGesturedDirection.Left && this.OnMouseGestureToLeft != null)
            {
                this.OnMouseGestureToLeft(this, new MouseGestureEventArgs((Int32)dx));
            }
            else if (direction == MouseGesturedDirection.Right && this.OnMouseGestureToRight != null)
            {
                this.OnMouseGestureToRight(this, new MouseGestureEventArgs((Int32)dx));
            }
            else if (direction == MouseGesturedDirection.Top && this.OnMouseGestureToTop != null)
            {
                this.OnMouseGestureToTop(this, new MouseGestureEventArgs((Int32)dy));
            }
            else if (direction == MouseGesturedDirection.Bottom && this.OnMouseGestureToBottom != null)
            {
                this.OnMouseGestureToBottom(this, new MouseGestureEventArgs((Int32)dy));
            }
            else if (direction == MouseGesturedDirection.None && dx < 0 && dy < 0 && this.OnMouseGestureToLeft != null && this.OnMouseGestureToTop != null)//左上移动
            {
                this.OnMouseGestureToLeft(this, new MouseGestureEventArgs((Int32)dx));
                this.OnMouseGestureToTop(this, new MouseGestureEventArgs((Int32)dy));
            }
            else if (direction == MouseGesturedDirection.None && dx > 0 && dy > 0 && this.OnMouseGestureToRight != null && this.OnMouseGestureToBottom != null)//右下移动
            {
                this.OnMouseGestureToRight(this, new MouseGestureEventArgs((Int32)dx));
                this.OnMouseGestureToBottom(this, new MouseGestureEventArgs((Int32)dy));
            }
        }
        #endregion
    }
}