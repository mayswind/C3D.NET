using System;

namespace C3D.DataViewer.Gesture
{
    /// <summary>
    /// 鼠标向左手势委托
    /// </summary>
    public delegate void MouseGestureToLeft(Object sender, MouseGestureEventArgs e);

    /// <summary>
    /// 鼠标向右手势委托
    /// </summary>
    public delegate void MouseGestureToRight(Object sender, MouseGestureEventArgs e);

    /// <summary>
    /// 鼠标向上手势委托
    /// </summary>
    public delegate void MouseGestureToTop(Object sender, MouseGestureEventArgs e);

    /// <summary>
    /// 鼠标向下手势委托
    /// </summary>
    public delegate void MouseGestureToBottom(Object sender, MouseGestureEventArgs e);
}