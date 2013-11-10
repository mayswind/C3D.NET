using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using C3D.DataViewer.Gesture;

namespace C3D.DataViewer.Controls
{
    public partial class uc3DPoint : UserControl
    {
        private static Int32 _currentTab = 0;

        private Dictionary<Int32, Dictionary<Int32, Single>> _points = null;
        private Dictionary<Int32, Chart> _charts = null;
        private Dictionary<Int32, ChartScaleStatus> _status = null;
        private Dictionary<Int32, MouseGestureHandler> _gestureHandlers = null;

        public uc3DPoint(C3DFile file, Int32 pid)
        {
            InitializeComponent();

            this.tcMain.SelectedIndex = _currentTab;

            this._points = new Dictionary<Int32, Dictionary<Int32, Single>>();
            this._charts = new Dictionary<Int32, Chart>();
            this._status = new Dictionary<Int32, ChartScaleStatus>();
            this._gestureHandlers = new Dictionary<Int32, MouseGestureHandler>();

            #region 读取数据
            C3DHeaderEvent[] events = file.Header.GetAllHeaderEvents();
            UInt16 firstFrameIndex = file.Header.FirstFrameIndex;
            UInt16 lastFrameIndex = file.Header.LastFrameIndex;

            for (Int32 i = 1; i <= 4; i++)
            {
                this._points[i] = new Dictionary<Int32, Single>();
                this._status[i] = new ChartScaleStatus(firstFrameIndex, lastFrameIndex, Single.MaxValue, Single.MinValue);
            }

            this._charts[1] = this.chartX;
            this._charts[2] = this.chartY;
            this._charts[3] = this.chartZ;
            this._charts[4] = this.chartResidual;

            for (Int32 i = 0; i < file.AllFrames.Count; i++)
            {
                Int32 index = firstFrameIndex + i;
                C3DPoint3DData point3D = file.AllFrames[i].Point3Ds[pid];

                if (point3D.Residual > -1)
                {
                    this._points[1][index] = point3D.X;
                    this._points[2][index] = point3D.Y;
                    this._points[3][index] = point3D.Z;

                    for (Int32 j = 1; j <= 3; j++)
                    {
                        this._status[j].Mins[1] = Math.Min(this._status[j].Mins[1], this._points[j][index]);
                        this._status[j].Maxs[1] = Math.Max(this._status[j].Maxs[1], this._points[j][index]);
                    }
                }

                this._points[4][index] = point3D.Residual;
                this._status[4].Maxs[1] = Math.Max(this._status[4].Maxs[1], point3D.Residual);

                this.lvItems.Items.Add(new ListViewItem(new String[] {
                    (index).ToString(), point3D.X.ToString("F3"), point3D.Y.ToString("F3"), point3D.Z.ToString("F3"),
                    point3D.Residual.ToString("F3"), point3D.CameraMaskInfo }));
            }

            this._status[4].Mins[1] = -1.0F;
            this._status[4].Maxs[1] = Math.Max(this._status[4].Maxs[1], 1.0F);
            #endregion

            #region 绑定数据
            for (Int32 i = 1; i <= 4; i++)
            {
                this.DataBind(this._charts[i], this._points[i], this._status[i].Mins[0], this._status[i].Maxs[0], this._status[i].Mins[1], this._status[i].Maxs[1]);

                this._gestureHandlers[i] = new MouseGestureHandler(this._charts[i]);
                this._gestureHandlers[i].OnMouseGestureToLeft += gesturehandler_OnMouseGestureToLeftOrRight;
                this._gestureHandlers[i].OnMouseGestureToRight += gesturehandler_OnMouseGestureToLeftOrRight;
                this._gestureHandlers[i].OnMouseGestureToTop += gesturehandler_OnMouseGestureToTopOrBottom;
                this._gestureHandlers[i].OnMouseGestureToBottom += gesturehandler_OnMouseGestureToTopOrBottom;
                this._gestureHandlers[i].OnMouseGestureUp += gesturehandler_OnMouseGestureUp;
            }

            if (events != null && events.Length > 0)
            {
                Single frameRate = (file.Parameters != null && file.Parameters["POINT", "RATE"] != null ? file.Parameters["POINT", "RATE"].GetData<Single>() : file.Header.FrameRate);

                for (Int32 i = 0; i < events.Length; i++)
                {
                    if (events[i] == null || !events[i].IsDisplay)
                    {
                        continue;
                    }

                    this.SetStripLine(this.chartX, events[i].EventTime * frameRate, events[i].EventName);
                    this.SetStripLine(this.chartY, events[i].EventTime * frameRate, events[i].EventName);
                    this.SetStripLine(this.chartZ, events[i].EventTime * frameRate, events[i].EventName);
                    this.SetStripLine(this.chartResidual, events[i].EventTime * frameRate, events[i].EventName);
                }
            }
            #endregion
        }

        #region 界面事件
        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentTab = this.tcMain.SelectedIndex;
        }
        #endregion

        #region 菜单点击事件
        private void mnuHZoomIn_Click(object sender, EventArgs e)
        {
            this.Zoom(0, -1);
        }

        private void mnuHZoomOut_Click(object sender, EventArgs e)
        {
            this.Zoom(0, 1);
        }

        private void mnuHZoomReset_Click(object sender, EventArgs e)
        {
            this.Zoom(0, 0);
        }

        private void mnuVZoomIn_Click(object sender, EventArgs e)
        {
            this.Zoom(1, -1);
        }

        private void mnuVZoomOut_Click(object sender, EventArgs e)
        {
            this.Zoom(1, 1);
        }

        private void mnuVZoomReset_Click(object sender, EventArgs e)
        {
            this.Zoom(1, 0);
        }

        private void mnuShowMarker_Click(object sender, EventArgs e)
        {
            this._charts[1].Series[0].ChartType = (this.mnuShowMarker.Checked ? SeriesChartType.Line : SeriesChartType.FastLine);
            this._charts[2].Series[0].ChartType = (this.mnuShowMarker.Checked ? SeriesChartType.Line : SeriesChartType.FastLine);
            this._charts[3].Series[0].ChartType = (this.mnuShowMarker.Checked ? SeriesChartType.Line : SeriesChartType.FastLine);
            this._charts[4].Series[0].MarkerStyle = (this.mnuShowMarker.Checked ? MarkerStyle.Square : MarkerStyle.None);
        }
        #endregion

        #region 鼠标手势事件
        private void gesturehandler_OnMouseGestureUp(object sender, MouseEventArgs e)
        {
            this._charts[_currentTab].Cursor = Cursors.Default;
        }

        private void gesturehandler_OnMouseGestureToLeftOrRight(object sender, MouseGestureEventArgs e)
        {
            Axis axis = this._charts[_currentTab].ChartAreas[0].AxisX;
            Double delta = (axis.ScaleView.ViewMaximum - axis.ScaleView.ViewMinimum) / this._charts[_currentTab].Width * e.Delta;

            if (delta > 0 && axis.ScaleView.ViewMinimum - delta < axis.Minimum)//Mouse To Right
            {
                axis.ScaleView.Position += axis.Minimum - axis.ScaleView.ViewMinimum;
            }
            else if (delta < 0 && axis.ScaleView.ViewMaximum - delta > axis.Maximum)//Mouse To Left
            {
                axis.ScaleView.Position += axis.Maximum - axis.ScaleView.ViewMaximum;
            }
            else
            {
                axis.ScaleView.Position -= delta;
            }

            this._charts[_currentTab].Cursor = Cursors.Hand;
        }

        private void gesturehandler_OnMouseGestureToTopOrBottom(object sender, MouseGestureEventArgs e)
        {
            Axis axis = this._charts[_currentTab].ChartAreas[0].AxisY;
            Double delta = (axis.ScaleView.ViewMaximum - axis.ScaleView.ViewMinimum) / this._charts[_currentTab].Height *e.Delta;

            if (delta > 0 && axis.ScaleView.ViewMaximum + delta > axis.Maximum)//Mouse To Down
            {
                axis.ScaleView.Position += axis.Maximum - axis.ScaleView.ViewMaximum;
            }
            else if (delta < 0 && axis.ScaleView.ViewMinimum + delta < axis.Minimum)//Mouse To Up
            {
                axis.ScaleView.Position += axis.Minimum - axis.ScaleView.ViewMinimum;
            }
            else
            {
                axis.ScaleView.Position += delta;
            }

            this._charts[_currentTab].Cursor = Cursors.Hand;
        }
        #endregion

        #region 私有方法
        private void DataBind(Chart chart, Dictionary<Int32, Single> data, Single minX, Single maxX, Single minY, Single maxY)
        {
            chart.Series[0].Points.DataBind(data, "Key", "Value", "");
            chart.ChartAreas[0].AxisX.Minimum = minX;
            chart.ChartAreas[0].AxisX.Maximum = maxX;
            chart.ChartAreas[0].AxisY.Minimum = minY;
            chart.ChartAreas[0].AxisY.Maximum = maxY;
        }

        private void SetStripLine(Chart chart, Double offset, String name)
        {
            StripLine line = new StripLine();
            line.Interval = 0;
            line.IntervalOffset = offset;
            line.Text = name;
            line.BorderColor = Color.Black;
            line.BorderDashStyle = ChartDashStyle.Dash;
            line.BorderWidth = 1;

            chart.ChartAreas[0].AxisX.StripLines.Add(line);
        }

        private void Zoom(Byte type, Int32 scale)
        {
            Axis axis = (type == 0 ? this._charts[_currentTab].ChartAreas[0].AxisX : this._charts[_currentTab].ChartAreas[0].AxisY);

            if (scale == 0)//Zoom Reset
            {
                axis.Maximum = this._status[_currentTab].Maxs[type];
                axis.Minimum = this._status[_currentTab].Mins[type];
                axis.ScaleView.ZoomReset();

                this._status[_currentTab].Scales[type] = scale;
            }
            else
            {
                Double diff = this._status[_currentTab].Maxs[type] - this._status[_currentTab].Mins[type];
                Int32 newScale = this._status[_currentTab].Scales[type] + scale;

                if (newScale > 0)
                {
                    axis.Maximum = this._status[_currentTab].Maxs[type] + diff * 0.1 * Math.Abs(newScale);
                    axis.Minimum = this._status[_currentTab].Mins[type] - diff * 0.1 * Math.Abs(newScale);
                }
                else if (newScale < 0)
                {
                    Double vMax = this._status[_currentTab].Maxs[type] - diff * 0.5 * (1.0 - 1.0 / Math.Pow(2, Math.Abs(newScale)));
                    Double vMin = this._status[_currentTab].Mins[type] + diff * 0.5 * (1.0 - 1.0 / Math.Pow(2, Math.Abs(newScale)));

                    if (vMin >= vMax)
                    {
                        return;
                    }

                    axis.ScaleView.Zoom(vMin, vMax);
                }
                else
                {
                    axis.Maximum = this._status[_currentTab].Maxs[type];
                    axis.Minimum = this._status[_currentTab].Mins[type];

                    axis.ScaleView.ZoomReset();
                }

                this._status[_currentTab].Scales[type] = newScale;
            }
        }
        #endregion
    }
}