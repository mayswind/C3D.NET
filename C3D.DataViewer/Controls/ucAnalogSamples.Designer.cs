namespace C3D.DataViewer.Controls
{
    partial class ucAnalogSamples
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45;
            chartArea1.AxisX.LabelStyle.Format = "D";
            chartArea1.AxisX.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.IsInterlaced = true;
            chartArea1.AxisY.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45 | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea1.AxisY.LabelStyle.Format = "F2";
            chartArea1.AxisY.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY2.Maximum = 1D;
            chartArea1.AxisY2.Minimum = 0D;
            chartArea1.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft;
            chartArea1.BackSecondaryColor = System.Drawing.Color.Silver;
            chartArea1.BorderColor = System.Drawing.Color.Gray;
            chartArea1.Name = "ChartArea1";
            this.chartView.ChartAreas.Add(chartArea1);
            this.chartView.ContextMenuStrip = this.mnuContext;
            this.chartView.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chartView.Legends.Add(legend1);
            this.chartView.Location = new System.Drawing.Point(3, 3);
            this.chartView.Name = "chartView";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(36)))), ((int)(((byte)(107)))));
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.MarkerColor = System.Drawing.Color.Black;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series1.Name = "PointX";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            this.chartView.Series.Add(series1);
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
            this.mnuContext.Size = new System.Drawing.Size(220, 192);
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
            // ucAnalogSamples
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcMain);
            this.Name = "ucAnalogSamples";
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
    }
}
