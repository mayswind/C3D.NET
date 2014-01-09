namespace C3D.DataViewer.Controls
{
    partial class AnalogSamplesControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpData = new System.Windows.Forms.TabPage();
            this.lvItems = new System.Windows.Forms.ListView();
            this.chFrame = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpView = new System.Windows.Forms.TabPage();
            this.chartView = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuHZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHZoomReset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLine = new System.Windows.Forms.ToolStripSeparator();
            this.mnuVZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVZoomReset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLine2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowMarker = new System.Windows.Forms.ToolStripMenuItem();
            this.gestureHandler = new C3D.DataViewer.Gesture.MouseGestureHandler();
            this.tcMain.SuspendLayout();
            this.tpData.SuspendLayout();
            this.tpView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartView)).BeginInit();
            this.mnuContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpData);
            this.tcMain.Controls.Add(this.tpView);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(640, 480);
            this.tcMain.TabIndex = 1;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tpData
            // 
            this.tpData.Controls.Add(this.lvItems);
            this.tpData.Location = new System.Drawing.Point(4, 22);
            this.tpData.Name = "tpData";
            this.tpData.Padding = new System.Windows.Forms.Padding(3);
            this.tpData.Size = new System.Drawing.Size(632, 454);
            this.tpData.TabIndex = 3;
            this.tpData.Text = "Data";
            this.tpData.UseVisualStyleBackColor = true;
            // 
            // lvItems
            // 
            this.lvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFrame});
            this.lvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvItems.FullRowSelect = true;
            this.lvItems.GridLines = true;
            this.lvItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvItems.Location = new System.Drawing.Point(3, 3);
            this.lvItems.MultiSelect = false;
            this.lvItems.Name = "lvItems";
            this.lvItems.Size = new System.Drawing.Size(626, 448);
            this.lvItems.TabIndex = 4;
            this.lvItems.UseCompatibleStateImageBehavior = false;
            this.lvItems.View = System.Windows.Forms.View.Details;
            // 
            // chFrame
            // 
            this.chFrame.Text = "Frame";
            // 
            // tpView
            // 
            this.tpView.Controls.Add(this.chartView);
            this.tpView.Location = new System.Drawing.Point(4, 22);
            this.tpView.Name = "tpView";
            this.tpView.Padding = new System.Windows.Forms.Padding(3);
            this.tpView.Size = new System.Drawing.Size(632, 454);
            this.tpView.TabIndex = 4;
            this.tpView.Text = "View";
            this.tpView.UseVisualStyleBackColor = true;
            // 
            // chartView
            // 
            chartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.IsStartedFromZero = false;
            chartArea2.AxisX.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45;
            chartArea2.AxisX.LabelStyle.Format = "D";
            chartArea2.AxisX.LineColor = System.Drawing.Color.Gray;
            chartArea2.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisY.IsInterlaced = true;
            chartArea2.AxisY.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45 | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea2.AxisY.LabelStyle.Format = "F2";
            chartArea2.AxisY.LineColor = System.Drawing.Color.Gray;
            chartArea2.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.AxisY2.Maximum = 1D;
            chartArea2.AxisY2.Minimum = 0D;
            chartArea2.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft;
            chartArea2.BackSecondaryColor = System.Drawing.Color.Silver;
            chartArea2.BorderColor = System.Drawing.Color.Gray;
            chartArea2.Name = "ChartArea1";
            this.chartView.ChartAreas.Add(chartArea2);
            this.chartView.ContextMenuStrip = this.mnuContext;
            this.chartView.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.chartView.Legends.Add(legend2);
            this.chartView.Location = new System.Drawing.Point(3, 3);
            this.chartView.Name = "chartView";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(36)))), ((int)(((byte)(107)))));
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.MarkerColor = System.Drawing.Color.Black;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series2.Name = "PointX";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            this.chartView.Series.Add(series2);
            this.chartView.Size = new System.Drawing.Size(626, 448);
            this.chartView.TabIndex = 1;
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHZoomIn,
            this.mnuHZoomOut,
            this.mnuHZoomReset,
            this.mnuLine,
            this.mnuVZoomIn,
            this.mnuVZoomOut,
            this.mnuVZoomReset,
            this.mnuLine2,
            this.mnuShowMarker});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(220, 170);
            // 
            // mnuHZoomIn
            // 
            this.mnuHZoomIn.Name = "mnuHZoomIn";
            this.mnuHZoomIn.Size = new System.Drawing.Size(219, 22);
            this.mnuHZoomIn.Text = "Zoom In Horizontally";
            this.mnuHZoomIn.Click += new System.EventHandler(this.mnuHZoomIn_Click);
            // 
            // mnuHZoomOut
            // 
            this.mnuHZoomOut.Name = "mnuHZoomOut";
            this.mnuHZoomOut.Size = new System.Drawing.Size(219, 22);
            this.mnuHZoomOut.Text = "Zoom Out Horizontally";
            this.mnuHZoomOut.Click += new System.EventHandler(this.mnuHZoomOut_Click);
            // 
            // mnuHZoomReset
            // 
            this.mnuHZoomReset.Name = "mnuHZoomReset";
            this.mnuHZoomReset.Size = new System.Drawing.Size(219, 22);
            this.mnuHZoomReset.Text = "Zoom Reset Horizontally";
            this.mnuHZoomReset.Click += new System.EventHandler(this.mnuHZoomReset_Click);
            // 
            // mnuLine
            // 
            this.mnuLine.Name = "mnuLine";
            this.mnuLine.Size = new System.Drawing.Size(216, 6);
            // 
            // mnuVZoomIn
            // 
            this.mnuVZoomIn.Name = "mnuVZoomIn";
            this.mnuVZoomIn.Size = new System.Drawing.Size(219, 22);
            this.mnuVZoomIn.Text = "Zoom In Vertically";
            this.mnuVZoomIn.Click += new System.EventHandler(this.mnuVZoomIn_Click);
            // 
            // mnuVZoomOut
            // 
            this.mnuVZoomOut.Name = "mnuVZoomOut";
            this.mnuVZoomOut.Size = new System.Drawing.Size(219, 22);
            this.mnuVZoomOut.Text = "Zoom Out Vertically";
            this.mnuVZoomOut.Click += new System.EventHandler(this.mnuVZoomOut_Click);
            // 
            // mnuVZoomReset
            // 
            this.mnuVZoomReset.Name = "mnuVZoomReset";
            this.mnuVZoomReset.Size = new System.Drawing.Size(219, 22);
            this.mnuVZoomReset.Text = "Zoom Reset Vertically";
            this.mnuVZoomReset.Click += new System.EventHandler(this.mnuVZoomReset_Click);
            // 
            // mnuLine2
            // 
            this.mnuLine2.Name = "mnuLine2";
            this.mnuLine2.Size = new System.Drawing.Size(216, 6);
            // 
            // mnuShowMarker
            // 
            this.mnuShowMarker.CheckOnClick = true;
            this.mnuShowMarker.Name = "mnuShowMarker";
            this.mnuShowMarker.Size = new System.Drawing.Size(219, 22);
            this.mnuShowMarker.Text = "Show Marker";
            this.mnuShowMarker.Click += new System.EventHandler(this.mnuShowMarker_Click);
            // 
            // gestureHandler
            // 
            this.gestureHandler.AutoRegister = true;
            this.gestureHandler.BaseControl = this.chartView;
            this.gestureHandler.SupportButton = System.Windows.Forms.MouseButtons.Left;
            this.gestureHandler.OnMouseGestureToLeft += new C3D.DataViewer.Gesture.MouseGestureToLeft(this.gesturehandler_OnMouseGestureToLeftOrRight);
            this.gestureHandler.OnMouseGestureToRight += new C3D.DataViewer.Gesture.MouseGestureToRight(this.gesturehandler_OnMouseGestureToLeftOrRight);
            this.gestureHandler.OnMouseGestureToTop += new C3D.DataViewer.Gesture.MouseGestureToTop(this.gesturehandler_OnMouseGestureToTopOrBottom);
            this.gestureHandler.OnMouseGestureToBottom += new C3D.DataViewer.Gesture.MouseGestureToBottom(this.gesturehandler_OnMouseGestureToTopOrBottom);
            this.gestureHandler.OnMouseGestureUp += new System.Windows.Forms.MouseEventHandler(this.gesturehandler_OnMouseGestureUp);
            // 
            // AnalogSamplesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcMain);
            this.Name = "AnalogSamplesControl";
            this.Size = new System.Drawing.Size(640, 480);
            this.tcMain.ResumeLayout(false);
            this.tpData.ResumeLayout(false);
            this.tpView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartView)).EndInit();
            this.mnuContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpData;
        private System.Windows.Forms.ListView lvItems;
        private System.Windows.Forms.ColumnHeader chFrame;
        private System.Windows.Forms.TabPage tpView;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartView;
        private System.Windows.Forms.ContextMenuStrip mnuContext;
        private System.Windows.Forms.ToolStripMenuItem mnuHZoomIn;
        private System.Windows.Forms.ToolStripMenuItem mnuHZoomOut;
        private System.Windows.Forms.ToolStripMenuItem mnuHZoomReset;
        private System.Windows.Forms.ToolStripSeparator mnuLine;
        private System.Windows.Forms.ToolStripMenuItem mnuVZoomIn;
        private System.Windows.Forms.ToolStripMenuItem mnuVZoomOut;
        private System.Windows.Forms.ToolStripMenuItem mnuVZoomReset;
        private System.Windows.Forms.ToolStripSeparator mnuLine2;
        private System.Windows.Forms.ToolStripMenuItem mnuShowMarker;
        private Gesture.MouseGestureHandler gestureHandler;
    }
}
