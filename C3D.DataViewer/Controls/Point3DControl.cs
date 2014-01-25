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
        private static Boolean _showResidual = false;
        private static Boolean _showMarker = false;

        private C3DFile _file = null;
        private Int32 _pid = 0;

        private Chart[] _charts = null;
        private Dictionary<Int32, Single>[] _points = null;
        private ChartScaleStatus[] _status = null;

        public Point3DControl(C3DFile file, Int32 pid)
        {
            InitializeComponent();

            this.tcMain.SelectedIndex = _currentTab;
            this.mnuShowResidual.Checked = _showResidual;
            this.mnuShowMarker.Checked = _showMarker;

            this._charts = new Chart[4] { this.chartX, this.chartY, this.chartZ, this.chartResidual };
            this._points = new Dictionary<Int32, Single>[4];
            this._status = new ChartScaleStatus[4];

            this._file = file;
            this._pid = pid;
            this.LoadData(true);
        }

        #region 读取数据
        private void LoadData(Boolean isFirstLoad)
        {
            if (this._file == null)
            {
                return;
            }

            C3DParameterCache cache = C3DParameterCache.CreateCache(this._file);
            C3DHeaderEvent[] events = this._file.Header.GetAllHeaderEvents();

            UInt16 firstFrameIndex = this._file.Header.FirstFrameIndex;
            UInt16 lastFrameIndex = this._file.Header.LastFrameIndex;

            #region 第一次初始化
            if (isFirstLoad)
            {
                for (Int32 i = 0; i < 4; i++)
                {
                    this._status[i] = new ChartScaleStatus(firstFrameIndex, lastFrameIndex, Single.MaxValue, Single.MinValue);
                }

                this.ShowStripLine(events, cache.FrameRate);
                this.SetMarker();
            }
            #endregion

            #region 列表内容填充
            for (Int32 i = 0; i < 4; i++)
            {
                this._points[i] = new Dictionary<Int32, Single>();

                this._status[i].Mins[1] = Single.MaxValue;
                this._status[i].Maxs[1] = Single.MinValue;

                this._status[i].Maxs[0] = (this._status[i].Maxs[0] == this._status[i].Mins[0] ? this._status[i].Maxs[0] + 1 : this._status[i].Maxs[0]);
                this._status[i].Maxs[1] = (this._status[i].Maxs[1] == this._status[i].Mins[1] ? this._status[i].Maxs[1] + 1 : this._status[i].Maxs[1]);
            }
            
            this._status[3].Mins[1] = -1.0F;
            this._status[3].Maxs[1] = Math.Max(this._status[3].Maxs[1], 1.0F);

            for (Int32 i = 0; i < this._file.AllFrames.Count; i++)
            {
                Int32 index = firstFrameIndex + i;
                C3DPoint3DData point3D = this._file.AllFrames[i].Point3Ds[this._pid];

                if (_showResidual || point3D.Residual > -1)
                {
                    this._points[0][index] = point3D.X;
                    this._points[1][index] = point3D.Y;
                    this._points[2][index] = point3D.Z;

                    for (Int32 j = 0; j < 3; j++)
                    {
                        this._status[j].Mins[1] = Math.Min(this._status[j].Mins[1], this._points[j][index]);
                        this._status[j].Maxs[1] = Math.Max(this._status[j].Maxs[1], this._points[j][index]);
                    }
                }

                this._points[3][index] = point3D.Residual;
                this._status[3].Maxs[1] = Math.Max(this._status[3].Maxs[1], point3D.Residual);

                if (isFirstLoad)
                {
                    this.lvItems.Items.Add(new ListViewItem(new String[] {
                        (index).ToString(), point3D.X.ToString("F3"), point3D.Y.ToString("F3"), point3D.Z.ToString("F3"),
                        point3D.Residual.ToString("F3"), point3D.CameraMaskInfo }));
                }
            }
            #endregion

            #region 绑定数据
            for (Int32 i = 0; i < 4; i++)
            {
                ChartBindingHelper.BindDataToChart<Int32, Single>(this._charts[i], this._points[i], this._status[i].Mins[0], this._status[i].Maxs[0], this._status[i].Mins[1], this._status[i].Maxs[1]);
            }
            #endregion
        }

        private void ShowStripLine(C3DHeaderEvent[] events, Single frameRate)
        {
            if (events == null || events.Length == 0)
            {
                return;
            }

            for (Int32 j = 0; j < events.Length; j++)
            {
                if (events[j] == null || !events[j].IsDisplay)
                {
                    continue;
                }

                Double offset = events[j].EventTime * frameRate;
                String name = events[j].EventName;

                for (Int32 i = 0; i < 4; i++)
                {
                    ChartBindingHelper.SetStripLineToChart(this._charts[i], offset, name);
                }
            }
        }

        private void SetMarker()
        {
            for (Int32 i = 0; i < 3; i++)
            {
                this._charts[i].Series[0].ChartType = (_showMarker ? SeriesChartType.Line : SeriesChartType.FastLine);//XYZ
            }

            this._charts[3].Series[0].MarkerStyle = (_showMarker ? MarkerStyle.Square : MarkerStyle.None);//Residual
        }

        private Chart GetCurrentChart()
        {
            return this._charts[_currentTab - 1];
        }

        private ChartScaleStatus GetCurrentStatus()
        {
            return this._status[_currentTab - 1];
        }
        #endregion

        #region 界面事件
        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentTab = this.tcMain.SelectedIndex;
        }

        private void gesturehandler_OnMouseGestureUp(object sender, MouseEventArgs e)
        {
            ChartZoomHelper.SetChartMouseUp(this.GetCurrentChart());
        }

        private void gesturehandler_OnMouseGestureToLeftOrRight(object sender, MouseGestureEventArgs e)
        {
            ChartZoomHelper.SetChartMouseMoveLeftOrRight(this.GetCurrentChart(), e.Delta);
        }

        private void gesturehandler_OnMouseGestureToTopOrBottom(object sender, MouseGestureEventArgs e)
        {
            ChartZoomHelper.SetChartMouseMoveTopOrBottom(this.GetCurrentChart(), e.Delta);
        }
        #endregion

        #region 菜单事件
        private void mnuHZoomIn_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this.GetCurrentChart(), this.GetCurrentStatus(), 0, -1);
        }

        private void mnuHZoomOut_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this.GetCurrentChart(), this.GetCurrentStatus(), 0, 1);
        }

        private void mnuHZoomReset_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this.GetCurrentChart(), this.GetCurrentStatus(), 0, 0);
        }

        private void mnuVZoomIn_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this.GetCurrentChart(), this.GetCurrentStatus(), 1, -1);
        }

        private void mnuVZoomOut_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this.GetCurrentChart(), this.GetCurrentStatus(), 1, 1);
        }

        private void mnuVZoomReset_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this.GetCurrentChart(), this.GetCurrentStatus(), 1, 0);
        }

        private void mnuShowResidual_Click(object sender, EventArgs e)
        {
            _showResidual = this.mnuShowResidual.Checked;
            this.LoadData(false);
        }

        private void mnuShowMarker_Click(object sender, EventArgs e)
        {
            _showMarker = this.mnuShowMarker.Checked;
            this.SetMarker();
        }
        #endregion
    }
}