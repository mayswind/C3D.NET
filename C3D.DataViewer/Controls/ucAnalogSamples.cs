using System;
using System.Windows.Forms;

namespace C3D.DataViewer.Controls
{
    public partial class ucAnalogSamples : UserControl
    {
        private static Int32 _currentTab = 0;

        public ucAnalogSamples(C3DFile file, Int32 cid)
        {
            InitializeComponent();

            this.tcMain.SelectedIndex = _currentTab;

            Int16 samplePerFrame = (file.Parameters != null && file.Parameters["ANALOG", "RATE"] != null && file.Parameters["POINT", "RATE"] != null ?
                (Int16)(file.Parameters["ANALOG", "RATE"].GetData<Single>() / file.Parameters["POINT", "RATE"].GetData<Single>()) : file.Header.AnalogSamplesPerFrame);

            for (Int32 i = 0; i < samplePerFrame; i++)
            {
                this.lvItems.Columns.Add(new ColumnHeader() { Text = String.Format("SP {0}", (i + 1).ToString()), Width = 70 });
            }

            for (Int32 i = 0; i < file.AllFrames.Count; i++)
            {
                String[] item = new String[file.AllFrames[i].AnalogSamples[cid].SampleCount + 1];
                item[0] = (file.Header.FirstFrameIndex + i).ToString();

                for (Int32 j = 1; j < item.Length; j++)
                {
                    item[j] = file.AllFrames[i].AnalogSamples[cid][j - 1].ToString("F3");
                }

                this.lvItems.Items.Add(new ListViewItem(item));
            }
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentTab = this.tcMain.SelectedIndex;
        }
    }
}