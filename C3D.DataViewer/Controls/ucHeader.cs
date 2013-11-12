using System;
using System.Windows.Forms;

namespace C3D.DataViewer.Controls
{
    public partial class ucHeader : UserControl
    {
        public ucHeader(C3DFile file)
        {
            InitializeComponent();

            this.lvItems.Items.Add(new ListViewItem(new String[] { "Point Count", file.Header.PointCount.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "First Frame Index", file.Header.FirstFrameIndex.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Last Frame Index", file.Header.LastFrameIndex.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Scale Factor", file.Header.ScaleFactor.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Frame Rate", file.Header.FrameRate.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Analog Measurement Count", file.Header.AnalogMeasurementCount.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Analog Samples Per Frame", file.Header.AnalogSamplesPerFrame.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Max Interpolation Gaps", file.Header.MaxInterpolationGaps.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Has Lable Range Data", file.Header.HasLableRangeData.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Header Events Count", file.Header.HeaderEventsCount.ToString() }));
            this.lvItems.Items.Add(new ListViewItem(new String[] { "Supports 4-Chars Event Label", file.Header.IsSupport4CharsLabel.ToString() }));
        }
    }
}