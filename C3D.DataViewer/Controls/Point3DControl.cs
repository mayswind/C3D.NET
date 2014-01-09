using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using C3D.DataViewer.Gesture;
using C3D.DataViewer.Helper;
using C3D.DataViewer.Status;

namespace C3D.DataViewer.Controls
{
    public partial class Point3DControl : UserControl
    {
        private static Int32 _currentTab = 0;

        private Dictionary<Int32, Dictionary<Int32, Single>> _points = null;
        private Dictionary<Int32, Chart> _charts = null;
        private Dictionary<Int32, ChartScaleStatus> _status = null;
        private Dictionary<Int32, MouseGestureHandler> _gestureHandlers = null;

        public Point3DControl(C3DFile file, Int32 pid)
        {
            InitializeComponent();

            this.tcMain.SelectedIndex = _currentTab;
            this._points = new Dictionary<Int32, Dictionary<Int32, Single>>();
            this._charts = new Dictionary<Int32, Chart>();
            this._status = new Dictionary<Int32, ChartScaleStatus>();
            this._gestureHandlers = new Dictionary<Int32, MouseGestureHandler>();

            this.LoadData(file, pid, false);
        }

        #region 读取数据
        private void LoadData(C3DFile file, Int32 pid, Boolean showResidual)
        {
            if (file == null)
            {
                return;
            }

            #region 初始化
            C3DParameterCache cache = C3DParameterCache.CreateCache(file);
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
            #endregion

            #region 列表内容填充
            for (Int32 i = 0; i < file.AllFrames.Count; i++)
            {
                Int32 index = firstFrameIndex + i;
                C3DPoint3DData point3D = file.AllFrames[i].Point3Ds[pid];

                if (showResidual || point3D.Residual > -1)
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
            #endregion

            #region 设置缩放状态
            for (Int32 i = 1; i <= 3; i++)
            {
                this._status[i].Maxs[0] = (this._status[i].Maxs[0] == this._status[i].Mins[0] ? this._status[i].Maxs[0] + 1 : this._status[i].Maxs[0]);
                this._status[i].Maxs[1] = (this._status[i].Maxs[1] == this._status[i].Mins[1] ? this._status[i].Maxs[1] + 1 : this._status[i].Maxs[1]);
            }

            this._status[4].Mins[1] = -1.0F;
            this._status[4].Maxs[1] = Math.Max(this._status[4].Maxs[1], 1.0F);
            #endregion

            #region 显示数据
            for (Int32 i = 1; i <= 4; i++)
            {
                ChartBindingHelper.BindDataToChart<Int32, Single>(this._charts[i], this._points[i], this._status[i].Mins[0], this._status[i].Maxs[0], this._status[i].Mins[1], this._status[i].Maxs[1]);
            }

            this.ShowStripLine(events, cache.FrameRate);
            #endregion
        }

        private void ShowStripLine(C3DHeaderEvent[] events, Single frameRate)
        {
            if (events == null || events.Length == 0)
            {
                return;
            }

            for (Int32 i = 0; i < events.Length; i++)
            {
                if (events[i] != null && events[i].IsDisplay)
                {
                    ChartBindingHelper.SetStripLineToChart(this.chartX, events[i].EventTime * frameRate, events[i].EventName);
                    ChartBindingHelper.SetStripLineToChart(this.chartY, events[i].EventTime * frameRate, events[i].EventName);
                    ChartBindingHelper.SetStripLineToChart(this.chartZ, events[i].EventTime * frameRate, events[i].EventName);
                    ChartBindingHelper.SetStripLineToChart(this.chartResidual, events[i].EventTime * frameRate, events[i].EventName);
                }
            }
        }
        #endregion

        #region 界面事件
        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentTab = this.tcMain.SelectedIndex;
        }
        #endregion

        #region 缩放相关
        private void gesturehandler_OnMouseGestureUp(object sender, MouseEventArgs e)
        {
            ChartZoomHelper.SetChartMouseUp(this._charts[_currentTab]);
        }

        private void gesturehandler_OnMouseGestureToLeftOrRight(object sender, MouseGestureEventArgs e)
        {
            ChartZoomHelper.SetChartMouseMoveLeftOrRight(this._charts[_currentTab], e.Delta);
        }

        private void gesturehandler_OnMouseGestureToTopOrBottom(object sender, MouseGestureEventArgs e)
        {
            ChartZoomHelper.SetChartMouseMoveTopOrBottom(this._charts[_currentTab], e.Delta);
        }

        private void mnuHZoomIn_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this._charts[_currentTab], this._status[_currentTab], 0, -1);
        }

        private void mnuHZoomOut_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this._charts[_currentTab], this._status[_currentTab], 0, 1);
        }

        private void mnuHZoomReset_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this._charts[_currentTab], this._status[_currentTab], 0, 0);
        }

        private void mnuVZoomIn_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this._charts[_currentTab], this._status[_currentTab], 1, -1);
        }

        private void mnuVZoomOut_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this._charts[_currentTab], this._status[_currentTab], 1, 1);
        }

        private void mnuVZoomReset_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this._charts[_currentTab], this._status[_currentTab], 1, 0);
        }

        private void mnuShowMarker_Click(object sender, EventArgs e)
        {
            this._charts[1].Series[0].ChartType = (this.mnuShowMarker.Checked ? SeriesChartType.Line : SeriesChartType.FastLine);
            this._charts[2].Series[0].ChartType = (this.mnuShowMarker.Checked ? SeriesChartType.Line : SeriesChartType.FastLine);
            this._charts[3].Series[0].ChartType = (this.mnuShowMarker.Checked ? SeriesChartType.Line : SeriesChartType.FastLine);
            this._charts[4].Series[0].MarkerStyle = (this.mnuShowMarker.Checked ? MarkerStyle.Square : MarkerStyle.None);
        }
        #endregion
    }
}