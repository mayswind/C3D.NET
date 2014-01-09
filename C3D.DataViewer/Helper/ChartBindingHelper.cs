using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace C3D.DataViewer.Helper
{
    /// <summary>
    /// 图表数据绑定辅助类
    /// </summary>
    internal static class ChartBindingHelper
    {
        /// <summary>
        /// 绑定数据到图表中
        /// </summary>
        /// <typeparam name="T1">X坐标数据类型</typeparam>
        /// <typeparam name="T2">Y坐标数据类型</typeparam>
        /// <param name="chart">图表控件</param>
        /// <param name="data">数据集合</param>
        /// <param name="minX">X轴最小值</param>
        /// <param name="maxX">X轴最大值</param>
        /// <param name="minY">Y轴最小值</param>
        /// <param name="maxY">Y轴最大值</param>
        internal static void BindDataToChart<T1, T2>(Chart chartCtl, Dictionary<T1, T2> data, Single minX, Single maxX, Single minY, Single maxY)
        {
            chartCtl.Series[0].Points.DataBind(data, "Key", "Value", "");
            chartCtl.ChartAreas[0].AxisX.Minimum = minX;
            chartCtl.ChartAreas[0].AxisX.Maximum = maxX;
            chartCtl.ChartAreas[0].AxisY.Minimum = minY;
            chartCtl.ChartAreas[0].AxisY.Maximum = maxY;
        }

        /// <summary>
        /// 设置图表分割线
        /// </summary>
        /// <param name="chart">图表控件</param>
        /// <param name="offset">偏移</param>
        /// <param name="name">名称</param>
        internal static void SetStripLineToChart(Chart chartCtl, Double offset, String name)
        {
            StripLine line = new StripLine();
            line.Interval = 0;
            line.IntervalOffset = offset;
            line.Text = name;
            line.BorderColor = Color.Black;
            line.BorderDashStyle = ChartDashStyle.Dash;
            line.BorderWidth = 1;

            chartCtl.ChartAreas[0].AxisX.StripLines.Add(line);
        }
    }
}