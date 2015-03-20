namespace C3D.DataViewer.Controls
{
    partial class Point3DControl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tpResidual = new System.Windows.Forms.TabPage();
            this.chartResidual = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuHZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHZoomReset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLine = new System.Windows.Forms.ToolStripSeparator();
            this.mnuVZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVZoomReset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLine2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowResidual = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowMarker = new System.Windows.Forms.ToolStripMenuItem();
            this.tpPointZ = new System.Windows.Forms.TabPage();
            this.chartZ = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tpPointY = new System.Windows.Forms.TabPage();
            this.chartY = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tpPointX = new System.Windows.Forms.TabPage();
            this.chartX = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tpData = new System.Windows.Forms.TabPage();
            this.lvItems = new System.Windows.Forms.ListView();
            this.chFrame = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPointX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPointY = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPointZ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chResidual = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMask = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tcMain = new System.Windows.Forms.TabControl();
            this.gestureHandler1 = new C3D.DataViewer.Gesture.MouseGestureHandler();
            this.gestureHandler2 = new C3D.DataViewer.Gesture.MouseGestureHandler();
            this.gestureHandler3 = new C3D.DataViewer.Gesture.MouseGestureHandler();
            this.gestureHandler4 = new C3D.DataViewer.Gesture.MouseGestureHandler();
            this.mnuContextForData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tpResidual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartResidual)).BeginInit();
            this.mnuContext.SuspendLayout();
            this.mnuContextForData.SuspendLayout();
            this.tpPointZ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartZ)).BeginInit();
            this.tpPointY.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartY)).BeginInit();
            this.tpPointX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartX)).BeginInit();
            this.tpData.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpResidual
            // 
            this.tpResidual.Controls.Add(this.chartResidual);
            this.tpResidual.Location = new System.Drawing.Point(4, 22);
            this.tpResidual.Name = "tpResidual";
            this.tpResidual.Padding = new System.Windows.Forms.Padding(3);
            this.tpResidual.Size = new System.Drawing.Size(632, 454);
            this.tpResidual.TabIndex = 4;
            this.tpResidual.Text = "Residual";
            this.tpResidual.UseVisualStyleBackColor = true;
            // 
            // chartResidual
            // 
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45;
            chartArea1.AxisX.LabelStyle.Format = "D";
            chartArea1.AxisX.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.IsInterlaced = true;
            chartArea1.AxisY.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45 | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea1.AxisY.LabelStyle.Format = "F2";
            chartArea1.AxisY.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.Minimum = -1D;
            chartArea1.AxisY.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;
            chartArea1.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft;
            chartArea1.BackSecondaryColor = System.Drawing.Color.Silver;
            chartArea1.BorderColor = System.Drawing.Color.Gray;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 99F;
            chartArea1.Position.Width = 99F;
            chartArea1.Position.X = 0.5F;
            chartArea1.Position.Y = 0.5F;
            this.chartResidual.ChartAreas.Add(chartArea1);
            this.chartResidual.ContextMenuStrip = this.mnuContext;
            this.chartResidual.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chartResidual.Legends.Add(legend1);
            this.chartResidual.Location = new System.Drawing.Point(3, 3);
            this.chartResidual.Name = "chartResidual";
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(36)))), ((int)(((byte)(107)))));
            series1.CustomProperties = "PixelPointWidth=1";
            series1.IsVisibleInLegend = false;
            series1.LabelToolTip = "Frame: #VALX\\nResidual: #VAL";
            series1.Legend = "Legend1";
            series1.MarkerBorderColor = System.Drawing.Color.Black;
            series1.MarkerColor = System.Drawing.Color.White;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series1.Name = "Residual";
            series1.ToolTip = "Frame: #VALX\\nResidual: #VAL";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            this.chartResidual.Series.Add(series1);
            this.chartResidual.Size = new System.Drawing.Size(626, 448);
            this.chartResidual.TabIndex = 2;
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
            this.mnuShowResidual,
            this.mnuShowMarker});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(282, 214);
            // 
            // mnuHZoomIn
            // 
            this.mnuHZoomIn.Name = "mnuHZoomIn";
            this.mnuHZoomIn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.mnuHZoomIn.Size = new System.Drawing.Size(281, 22);
            this.mnuHZoomIn.Text = "Zoom In Horizontally";
            this.mnuHZoomIn.Click += new System.EventHandler(this.mnuHZoomIn_Click);
            // 
            // mnuHZoomOut
            // 
            this.mnuHZoomOut.Name = "mnuHZoomOut";
            this.mnuHZoomOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuHZoomOut.Size = new System.Drawing.Size(281, 22);
            this.mnuHZoomOut.Text = "Zoom Out Horizontally";
            this.mnuHZoomOut.Click += new System.EventHandler(this.mnuHZoomOut_Click);
            // 
            // mnuHZoomReset
            // 
            this.mnuHZoomReset.Name = "mnuHZoomReset";
            this.mnuHZoomReset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mnuHZoomReset.Size = new System.Drawing.Size(281, 22);
            this.mnuHZoomReset.Text = "Zoom Reset Horizontally";
            this.mnuHZoomReset.Click += new System.EventHandler(this.mnuHZoomReset_Click);
            // 
            // mnuLine
            // 
            this.mnuLine.Name = "mnuLine";
            this.mnuLine.Size = new System.Drawing.Size(278, 6);
            // 
            // mnuVZoomIn
            // 
            this.mnuVZoomIn.Name = "mnuVZoomIn";
            this.mnuVZoomIn.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.mnuVZoomIn.Size = new System.Drawing.Size(281, 22);
            this.mnuVZoomIn.Text = "Zoom In Vertically";
            this.mnuVZoomIn.Click += new System.EventHandler(this.mnuVZoomIn_Click);
            // 
            // mnuVZoomOut
            // 
            this.mnuVZoomOut.Name = "mnuVZoomOut";
            this.mnuVZoomOut.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.mnuVZoomOut.Size = new System.Drawing.Size(281, 22);
            this.mnuVZoomOut.Text = "Zoom Out Vertically";
            this.mnuVZoomOut.Click += new System.EventHandler(this.mnuVZoomOut_Click);
            // 
            // mnuVZoomReset
            // 
            this.mnuVZoomReset.Name = "mnuVZoomReset";
            this.mnuVZoomReset.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.mnuVZoomReset.Size = new System.Drawing.Size(281, 22);
            this.mnuVZoomReset.Text = "Zoom Reset Vertically";
            this.mnuVZoomReset.Click += new System.EventHandler(this.mnuVZoomReset_Click);
            // 
            // mnuLine2
            // 
            this.mnuLine2.Name = "mnuLine2";
            this.mnuLine2.Size = new System.Drawing.Size(278, 6);
            // 
            // mnuShowResidual
            // 
            this.mnuShowResidual.CheckOnClick = true;
            this.mnuShowResidual.Name = "mnuShowResidual";
            this.mnuShowResidual.Size = new System.Drawing.Size(281, 22);
            this.mnuShowResidual.Text = "Show Residual Point";
            this.mnuShowResidual.Click += new System.EventHandler(this.mnuShowResidual_Click);
            // 
            // mnuShowMarker
            // 
            this.mnuShowMarker.CheckOnClick = true;
            this.mnuShowMarker.Name = "mnuShowMarker";
            this.mnuShowMarker.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.mnuShowMarker.Size = new System.Drawing.Size(281, 22);
            this.mnuShowMarker.Text = "Show Marker";
            this.mnuShowMarker.Click += new System.EventHandler(this.mnuShowMarker_Click);
            // 
            // tpPointZ
            // 
            this.tpPointZ.Controls.Add(this.chartZ);
            this.tpPointZ.Location = new System.Drawing.Point(4, 22);
            this.tpPointZ.Name = "tpPointZ";
            this.tpPointZ.Padding = new System.Windows.Forms.Padding(3);
            this.tpPointZ.Size = new System.Drawing.Size(632, 454);
            this.tpPointZ.TabIndex = 2;
            this.tpPointZ.Text = "PointZ";
            this.tpPointZ.UseVisualStyleBackColor = true;
            // 
            // chartZ
            // 
            chartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisX.IsStartedFromZero = false;
            chartArea2.AxisX.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45;
            chartArea2.AxisX.LabelStyle.Format = "D";
            chartArea2.AxisX.LineColor = System.Drawing.Color.Gray;
            chartArea2.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;
            chartArea2.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisY.IsInterlaced = true;
            chartArea2.AxisY.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45 | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea2.AxisY.LabelStyle.Format = "F2";
            chartArea2.AxisY.LineColor = System.Drawing.Color.Gray;
            chartArea2.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.AxisY.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;
            chartArea2.AxisY2.Maximum = 1D;
            chartArea2.AxisY2.Minimum = 0D;
            chartArea2.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft;
            chartArea2.BackSecondaryColor = System.Drawing.Color.Silver;
            chartArea2.BorderColor = System.Drawing.Color.Gray;
            chartArea2.Name = "ChartArea1";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 99F;
            chartArea2.Position.Width = 99F;
            chartArea2.Position.X = 0.5F;
            chartArea2.Position.Y = 0.5F;
            this.chartZ.ChartAreas.Add(chartArea2);
            this.chartZ.ContextMenuStrip = this.mnuContext;
            this.chartZ.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.chartZ.Legends.Add(legend2);
            this.chartZ.Location = new System.Drawing.Point(3, 3);
            this.chartZ.Name = "chartZ";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(36)))), ((int)(((byte)(107)))));
            series2.IsVisibleInLegend = false;
            series2.LabelToolTip = "Frame: #VALX\\nAxis-Z: #VAL";
            series2.Legend = "Legend1";
            series2.MarkerBorderColor = System.Drawing.Color.Black;
            series2.MarkerColor = System.Drawing.Color.White;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series2.Name = "PointZ";
            series2.ToolTip = "Frame: #VALX\\nAxis-Z: #VAL";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            this.chartZ.Series.Add(series2);
            this.chartZ.Size = new System.Drawing.Size(626, 448);
            this.chartZ.TabIndex = 1;
            // 
            // tpPointY
            // 
            this.tpPointY.Controls.Add(this.chartY);
            this.tpPointY.Location = new System.Drawing.Point(4, 22);
            this.tpPointY.Name = "tpPointY";
            this.tpPointY.Padding = new System.Windows.Forms.Padding(3);
            this.tpPointY.Size = new System.Drawing.Size(632, 454);
            this.tpPointY.TabIndex = 1;
            this.tpPointY.Text = "PointY";
            this.tpPointY.UseVisualStyleBackColor = true;
            // 
            // chartY
            // 
            chartArea3.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea3.AxisX.IsStartedFromZero = false;
            chartArea3.AxisX.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45;
            chartArea3.AxisX.LabelStyle.Format = "D";
            chartArea3.AxisX.LineColor = System.Drawing.Color.Gray;
            chartArea3.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea3.AxisX.MajorGrid.Enabled = false;
            chartArea3.AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea3.AxisX.Minimum = 0D;
            chartArea3.AxisX.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;
            chartArea3.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea3.AxisY.IsInterlaced = true;
            chartArea3.AxisY.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45 | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea3.AxisY.LabelStyle.Format = "F2";
            chartArea3.AxisY.LineColor = System.Drawing.Color.Gray;
            chartArea3.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea3.AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea3.AxisY.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;
            chartArea3.AxisY2.Maximum = 1D;
            chartArea3.AxisY2.Minimum = 0D;
            chartArea3.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft;
            chartArea3.BackSecondaryColor = System.Drawing.Color.Silver;
            chartArea3.BorderColor = System.Drawing.Color.Gray;
            chartArea3.Name = "ChartArea1";
            chartArea3.Position.Auto = false;
            chartArea3.Position.Height = 99F;
            chartArea3.Position.Width = 99F;
            chartArea3.Position.X = 0.5F;
            chartArea3.Position.Y = 0.5F;
            this.chartY.ChartAreas.Add(chartArea3);
            this.chartY.ContextMenuStrip = this.mnuContext;
            this.chartY.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Enabled = false;
            legend3.Name = "Legend1";
            this.chartY.Legends.Add(legend3);
            this.chartY.Location = new System.Drawing.Point(3, 3);
            this.chartY.Name = "chartY";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(36)))), ((int)(((byte)(107)))));
            series3.IsVisibleInLegend = false;
            series3.LabelToolTip = "Frame: #VALX\\nAxis-Y: #VAL";
            series3.Legend = "Legend1";
            series3.MarkerBorderColor = System.Drawing.Color.Black;
            series3.MarkerColor = System.Drawing.Color.White;
            series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series3.Name = "PointY";
            series3.ToolTip = "Frame: #VALX\\nAxis-Y: #VAL";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            this.chartY.Series.Add(series3);
            this.chartY.Size = new System.Drawing.Size(626, 448);
            this.chartY.TabIndex = 1;
            // 
            // tpPointX
            // 
            this.tpPointX.Controls.Add(this.chartX);
            this.tpPointX.Location = new System.Drawing.Point(4, 22);
            this.tpPointX.Name = "tpPointX";
            this.tpPointX.Padding = new System.Windows.Forms.Padding(3);
            this.tpPointX.Size = new System.Drawing.Size(632, 454);
            this.tpPointX.TabIndex = 0;
            this.tpPointX.Text = "PointX";
            this.tpPointX.UseVisualStyleBackColor = true;
            // 
            // chartX
            // 
            chartArea4.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea4.AxisX.IsLabelAutoFit = false;
            chartArea4.AxisX.IsStartedFromZero = false;
            chartArea4.AxisX.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45;
            chartArea4.AxisX.LabelStyle.Format = "D";
            chartArea4.AxisX.LineColor = System.Drawing.Color.Gray;
            chartArea4.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea4.AxisX.MajorGrid.Enabled = false;
            chartArea4.AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea4.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea4.AxisX.Minimum = 0D;
            chartArea4.AxisX.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;
            chartArea4.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea4.AxisY.IsInterlaced = true;
            chartArea4.AxisY.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45 | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea4.AxisY.LabelStyle.Format = "F2";
            chartArea4.AxisY.LineColor = System.Drawing.Color.Gray;
            chartArea4.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea4.AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea4.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea4.AxisY.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;
            chartArea4.AxisY2.Maximum = 1D;
            chartArea4.AxisY2.Minimum = 0D;
            chartArea4.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea4.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft;
            chartArea4.BackSecondaryColor = System.Drawing.Color.Silver;
            chartArea4.BorderColor = System.Drawing.Color.Gray;
            chartArea4.Name = "ChartArea1";
            chartArea4.Position.Auto = false;
            chartArea4.Position.Height = 99F;
            chartArea4.Position.Width = 99F;
            chartArea4.Position.X = 0.5F;
            chartArea4.Position.Y = 0.5F;
            this.chartX.ChartAreas.Add(chartArea4);
            this.chartX.ContextMenuStrip = this.mnuContext;
            this.chartX.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Enabled = false;
            legend4.Name = "Legend1";
            this.chartX.Legends.Add(legend4);
            this.chartX.Location = new System.Drawing.Point(3, 3);
            this.chartX.Name = "chartX";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(36)))), ((int)(((byte)(107)))));
            series4.IsVisibleInLegend = false;
            series4.LabelToolTip = "Frame: #VALX\\nAxis-X: #VAL";
            series4.Legend = "Legend1";
            series4.MarkerBorderColor = System.Drawing.Color.Black;
            series4.MarkerColor = System.Drawing.Color.White;
            series4.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series4.Name = "PointX";
            series4.ToolTip = "Frame: #VALX\\nAxis-X: #VAL";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series4.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            this.chartX.Series.Add(series4);
            this.chartX.Size = new System.Drawing.Size(626, 448);
            this.chartX.TabIndex = 0;
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
            this.chFrame,
            this.chPointX,
            this.chPointY,
            this.chPointZ,
            this.chResidual,
            this.chMask});
            this.lvItems.ContextMenuStrip = this.mnuContextForData;
            this.lvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvItems.FullRowSelect = true;
            this.lvItems.GridLines = true;
            this.lvItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvItems.Location = new System.Drawing.Point(3, 3);
            this.lvItems.MultiSelect = false;
            this.lvItems.Name = "lvItems";
            this.lvItems.Size = new System.Drawing.Size(626, 448);
            this.lvItems.TabIndex = 3;
            this.lvItems.UseCompatibleStateImageBehavior = false;
            this.lvItems.View = System.Windows.Forms.View.Details;
            // 
            // chFrame
            // 
            this.chFrame.Text = "Frame";
            // 
            // chPointX
            // 
            this.chPointX.Text = "X";
            this.chPointX.Width = 100;
            // 
            // chPointY
            // 
            this.chPointY.Text = "Y";
            this.chPointY.Width = 100;
            // 
            // chPointZ
            // 
            this.chPointZ.Text = "Z";
            this.chPointZ.Width = 100;
            // 
            // chResidual
            // 
            this.chResidual.Text = "Residual";
            this.chResidual.Width = 100;
            // 
            // chMask
            // 
            this.chMask.Text = "Mask";
            this.chMask.Width = 80;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpData);
            this.tcMain.Controls.Add(this.tpPointX);
            this.tcMain.Controls.Add(this.tpPointY);
            this.tcMain.Controls.Add(this.tpPointZ);
            this.tcMain.Controls.Add(this.tpResidual);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(640, 480);
            this.tcMain.TabIndex = 0;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // gestureHandler1
            // 
            this.gestureHandler1.AutoRegister = true;
            this.gestureHandler1.BaseControl = this.chartX;
            this.gestureHandler1.SupportButton = System.Windows.Forms.MouseButtons.Left;
            this.gestureHandler1.OnMouseGestureToLeft += new C3D.DataViewer.Gesture.MouseGestureToLeft(this.gesturehandler_OnMouseGestureToLeftOrRight);
            this.gestureHandler1.OnMouseGestureToRight += new C3D.DataViewer.Gesture.MouseGestureToRight(this.gesturehandler_OnMouseGestureToLeftOrRight);
            this.gestureHandler1.OnMouseGestureToTop += new C3D.DataViewer.Gesture.MouseGestureToTop(this.gesturehandler_OnMouseGestureToTopOrBottom);
            this.gestureHandler1.OnMouseGestureToBottom += new C3D.DataViewer.Gesture.MouseGestureToBottom(this.gesturehandler_OnMouseGestureToTopOrBottom);
            this.gestureHandler1.OnMouseGestureUp += new System.Windows.Forms.MouseEventHandler(this.gesturehandler_OnMouseGestureUp);
            // 
            // gestureHandler2
            // 
            this.gestureHandler2.AutoRegister = true;
            this.gestureHandler2.BaseControl = this.chartY;
            this.gestureHandler2.SupportButton = System.Windows.Forms.MouseButtons.Left;
            this.gestureHandler2.OnMouseGestureToLeft += new C3D.DataViewer.Gesture.MouseGestureToLeft(this.gesturehandler_OnMouseGestureToLeftOrRight);
            this.gestureHandler2.OnMouseGestureToRight += new C3D.DataViewer.Gesture.MouseGestureToRight(this.gesturehandler_OnMouseGestureToLeftOrRight);
            this.gestureHandler2.OnMouseGestureToTop += new C3D.DataViewer.Gesture.MouseGestureToTop(this.gesturehandler_OnMouseGestureToTopOrBottom);
            this.gestureHandler2.OnMouseGestureToBottom += new C3D.DataViewer.Gesture.MouseGestureToBottom(this.gesturehandler_OnMouseGestureToTopOrBottom);
            this.gestureHandler2.OnMouseGestureUp += new System.Windows.Forms.MouseEventHandler(this.gesturehandler_OnMouseGestureUp);
            // 
            // gestureHandler3
            // 
            this.gestureHandler3.AutoRegister = true;
            this.gestureHandler3.BaseControl = this.chartZ;
            this.gestureHandler3.SupportButton = System.Windows.Forms.MouseButtons.Left;
            this.gestureHandler3.OnMouseGestureToLeft += new C3D.DataViewer.Gesture.MouseGestureToLeft(this.gesturehandler_OnMouseGestureToLeftOrRight);
            this.gestureHandler3.OnMouseGestureToRight += new C3D.DataViewer.Gesture.MouseGestureToRight(this.gesturehandler_OnMouseGestureToLeftOrRight);
            this.gestureHandler3.OnMouseGestureToTop += new C3D.DataViewer.Gesture.MouseGestureToTop(this.gesturehandler_OnMouseGestureToTopOrBottom);
            this.gestureHandler3.OnMouseGestureToBottom += new C3D.DataViewer.Gesture.MouseGestureToBottom(this.gesturehandler_OnMouseGestureToTopOrBottom);
            this.gestureHandler3.OnMouseGestureUp += new System.Windows.Forms.MouseEventHandler(this.gesturehandler_OnMouseGestureUp);
            // 
            // gestureHandler4
            // 
            this.gestureHandler4.AutoRegister = true;
            this.gestureHandler4.BaseControl = this.chartResidual;
            this.gestureHandler4.SupportButton = System.Windows.Forms.MouseButtons.Left;
            this.gestureHandler4.OnMouseGestureToLeft += new C3D.DataViewer.Gesture.MouseGestureToLeft(this.gesturehandler_OnMouseGestureToLeftOrRight);
            this.gestureHandler4.OnMouseGestureToRight += new C3D.DataViewer.Gesture.MouseGestureToRight(this.gesturehandler_OnMouseGestureToLeftOrRight);
            this.gestureHandler4.OnMouseGestureToTop += new C3D.DataViewer.Gesture.MouseGestureToTop(this.gesturehandler_OnMouseGestureToTopOrBottom);
            this.gestureHandler4.OnMouseGestureToBottom += new C3D.DataViewer.Gesture.MouseGestureToBottom(this.gesturehandler_OnMouseGestureToTopOrBottom);
            this.gestureHandler4.OnMouseGestureUp += new System.Windows.Forms.MouseEventHandler(this.gesturehandler_OnMouseGestureUp);
            // 
            // mnuContextForData
            // 
            this.mnuContextForData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopy});
            this.mnuContextForData.Name = "mnuContextForData";
            this.mnuContextForData.Size = new System.Drawing.Size(153, 48);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(152, 22);
            this.mnuCopy.Text = "Copy All Data";
            this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // Point3DControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcMain);
            this.DoubleBuffered = true;
            this.Name = "Point3DControl";
            this.Size = new System.Drawing.Size(640, 480);
            this.tpResidual.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartResidual)).EndInit();
            this.mnuContext.ResumeLayout(false);
            this.tpPointZ.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartZ)).EndInit();
            this.tpPointY.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartY)).EndInit();
            this.tpPointX.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartX)).EndInit();
            this.tpData.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.mnuContextForData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tpResidual;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartResidual;
        private System.Windows.Forms.TabPage tpPointZ;
        private System.Windows.Forms.TabPage tpPointY;
        private System.Windows.Forms.TabPage tpPointX;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartX;
        private System.Windows.Forms.TabPage tpData;
        private System.Windows.Forms.ListView lvItems;
        private System.Windows.Forms.ColumnHeader chFrame;
        private System.Windows.Forms.ColumnHeader chPointX;
        private System.Windows.Forms.ColumnHeader chPointY;
        private System.Windows.Forms.ColumnHeader chPointZ;
        private System.Windows.Forms.ColumnHeader chResidual;
        private System.Windows.Forms.ColumnHeader chMask;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.ContextMenuStrip mnuContext;
        private System.Windows.Forms.ToolStripMenuItem mnuVZoomIn;
        private System.Windows.Forms.ToolStripMenuItem mnuVZoomOut;
        private System.Windows.Forms.ToolStripSeparator mnuLine;
        private System.Windows.Forms.ToolStripMenuItem mnuHZoomIn;
        private System.Windows.Forms.ToolStripMenuItem mnuHZoomOut;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartY;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartZ;
        private System.Windows.Forms.ToolStripSeparator mnuLine2;
        private System.Windows.Forms.ToolStripMenuItem mnuShowMarker;
        private System.Windows.Forms.ToolStripMenuItem mnuHZoomReset;
        private System.Windows.Forms.ToolStripMenuItem mnuVZoomReset;
        private Gesture.MouseGestureHandler gestureHandler1;
        private Gesture.MouseGestureHandler gestureHandler2;
        private Gesture.MouseGestureHandler gestureHandler3;
        private Gesture.MouseGestureHandler gestureHandler4;
        private System.Windows.Forms.ToolStripMenuItem mnuShowResidual;
        private System.Windows.Forms.ContextMenuStrip mnuContextForData;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
    }
}
