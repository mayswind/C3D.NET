using System;
using System.Windows.Forms;

namespace C3D.DataViewer.Controls
{
    public partial class ucHeader : UserControl
    {
        public ucHeader(C3DFile file)
        {
            InitializeComponent();

            this.lvItems.Items.Add(new ListViewItem(new String[] { "File Format", file.CreateProcessorType.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Number Format", file.Header.ScaleFactor > 0 ? "INT" : "REAL" }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Point Count", file.Header.PointCount.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Analog Measurement Count", file.Header.AnalogMeasurementCount.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "First Frame Index", file.Header.FirstFrameIndex.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Last Frame Index", file.Header.LastFrameIndex.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Max Interpolation Gaps", file.Header.MaxInterpolationGaps.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Scale Factor", file.Header.ScaleFactor.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Analog Samples Per Frame", file.Header.AnalogSamplesPerFrame.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Frame Rate", file.Header.FrameRate.ToString() }));
        }
    }
}