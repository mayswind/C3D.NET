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
    public partial class AnalogSamplesControl : UserControl
    {
        private static Int32 _currentTab = 0;

        private Dictionary<Single, Single> _points = null;
        private ChartScaleStatus _status = null;

        public AnalogSamplesControl(C3DFile file, Int32 cid)
        {
            InitializeComponent();

            this.tcMain.SelectedIndex = _currentTab;

            #region 读取数据
            C3DHeaderEvent[] events = file.Header.GetAllHeaderEvents();
            UInt16 firstFrameIndex = file.Header.FirstFrameIndex;
            UInt16 lastFrameIndex = file.Header.LastFrameIndex;
            UInt16 samplePerFrame = (file.Parameters != null && file.Parameters.ContainsParameter("ANALOG", "RATE") && file.Parameters.ContainsParameter("POINT", "RATE") ?
                (UInt16)(file.Parameters["ANALOG", "RATE"].GetData<Single>() / file.Parameters["POINT", "RATE"].GetData<Single>()) : file.Header.AnalogSamplesPerFrame);

            this._points = new Dictionary<Single, Single>();
            this._status = new ChartScaleStatus(firstFrameIndex, lastFrameIndex, Single.MaxValue, Single.MinValue);

            for (Int32 i = 0; i < samplePerFrame; i++)
            {
                this.lvItems.Columns.Add(new ColumnHeader() { Text = String.Format("SP {0}", (i + 1).ToString()), Width = 70 });
            }

            for (Int32 i = 0; i < file.AllFrames.Count; i++)
            {
                Int32 index = firstFrameIndex + i;
                C3DAnalogSamples point3D = file.AllFrames[i].AnalogSamples[cid];

                for (Int32 j = 0; j < point3D.SampleCount; j++)
                {
                    this._points[index + (Single)(j) / (Single)(point3D.SampleCount)] = point3D[j];
                    this._status.Mins[1] = Math.Min(this._status.Mins[1], point3D[j]);
                    this._status.Maxs[1] = Math.Max(this._status.Maxs[1], point3D[j]);
                }

                String[] item = new String[file.AllFrames[i].AnalogSamples[cid].SampleCount + 1];
                item[0] = (file.Header.FirstFrameIndex + i).ToString();

                for (Int32 j = 1; j < item.Length; j++)
                {
                    item[j] = point3D[j - 1].ToString("F3");
                }

                this.lvItems.Items.Add(new ListViewItem(item));
            }

            this._status.Maxs[0] = (this._status.Maxs[0] == this._status.Mins[0] ? this._status.Maxs[0] + 1 : this._status.Maxs[0]);
            this._status.Maxs[1] = (this._status.Maxs[1] == this._status.Mins[1] ? this._status.Maxs[1] + 1 : this._status.Maxs[1]);
            #endregion

            #region 绑定数据
            for (Int32 i = 1; i <= 1; i++)
            {
                this.DataBind(this.chartView, this._points, this._status.Mins[0], this._status.Maxs[0], this._status.Mins[1], this._status.Maxs[1]);
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

                    this.SetStripLine(this.chartView, events[i].EventTime * frameRate, events[i].EventName);
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

        #region 缩放相关
        private void gesturehandler_OnMouseGestureUp(object sender, MouseEventArgs e)
        {
            ChartZoomHelper.SetChartMouseUp(this.chartView);
        }

        private void gesturehandler_OnMouseGestureToLeftOrRight(object sender, MouseGestureEventArgs e)
        {
            ChartZoomHelper.SetChartMouseMoveLeftOrRight(this.chartView, e.Delta);
        }

        private void gesturehandler_OnMouseGestureToTopOrBottom(object sender, MouseGestureEventArgs e)
        {
            ChartZoomHelper.SetChartMouseMoveTopOrBottom(this.chartView, e.Delta);
        }

        private void mnuHZoomIn_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this.chartView, this._status, 0, -1);
        }

        private void mnuHZoomOut_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this.chartView, this._status, 0, 1);
        }

        private void mnuHZoomReset_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this.chartView, this._status, 0, 0);
        }

        private void mnuVZoomIn_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this.chartView, this._status, 1, -1);
        }

        private void mnuVZoomOut_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this.chartView, this._status, 1, 1);
        }

        private void mnuVZoomReset_Click(object sender, EventArgs e)
        {
            ChartZoomHelper.ZoomChart(this.chartView, this._status, 1, 0);
        }

        private void mnuShowMarker_Click(object sender, EventArgs e)
        {
            this.chartView.Series[0].ChartType = (this.mnuShowMarker.Checked ? SeriesChartType.Line : SeriesChartType.FastLine);
        }
        #endregion

        #region 私有方法
        private void DataBind(Chart chart, Dictionary<Single, Single> data, Single minX, Single maxX, Single minY, Single maxY)
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
        #endregion
    }
}