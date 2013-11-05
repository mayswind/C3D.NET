using System;

namespace C3D
{
    /// <summary>
    /// C3D头部事件类
    /// </summary>
    public sealed class C3DHeaderEvent
    {
        #region 字段
        private String _eventName;
        private Single _eventTime;
        private Boolean _isDisplay;
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置事件名称
        /// </summary>
        public String EventName
        {
            get { return this._eventName; }
            set { this._eventName = value; }
        }

        /// <summary>
        /// 获取或设置事件时间
        /// </summary>
        public Single EventTime
        {
            get { return this._eventTime; }
            set { this._eventTime = value; }
        }

        /// <summary>
        /// 获取或设置是否显示
        /// </summary>
        public Boolean IsDisplay
        {
            get { return this._isDisplay; }
            set { this._isDisplay = value; }
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 初始化新的C3D头部事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="eventTime">事件时间</param>
        /// <param name="isDisplay">是否显示</param>
        public C3DHeaderEvent(String eventName, Single eventTime, Boolean isDisplay)
        {
            this._eventName = eventName;
            this._eventTime = eventTime;
            this._isDisplay = isDisplay;
        }

        /// <summary>
        /// 初始化空的C3D头部事件
        /// </summary>
        public C3DHeaderEvent()
            : this(String.Empty, 0.0F, false)
        { }
        #endregion

        #region 重载方法
        public override String ToString()
        {
            return String.Format("C3DHeaderEvent, [{0}, {1}, {2}]", this._eventName, this._eventTime.ToString(), this._isDisplay.ToString());
        }
        #endregion
    }
}