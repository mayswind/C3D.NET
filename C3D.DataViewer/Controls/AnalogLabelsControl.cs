using System;
using System.Windows.Forms;

using C3D.DataViewer.Helper;

namespace C3D.DataViewer.Controls
{
    public partial class AnalogLabelsControl : UserControl
    {
        public AnalogLabelsControl(C3DFile file)
        {
            InitializeComponent();
            this.LoadData(file);
        }

        private void LoadData(C3DFile file)
        {
            if (file == null)
            {
                return;
            }

            C3DParameterCache cache = C3DParameterCache.CreateCache(file);
            UInt16 analogChannelCount = cache.AnalogChannelCount;
            Int16[] offset = cache.AnalogZeroOffset;
            Single[] scale = cache.AnalogChannelScale;

            String[] labels = C3DParameterHelper.LoadFromParameterArray<String>(file.Parameters["ANALOG", "LABELS"], analogChannelCount);
            String[] descriptions = C3DParameterHelper.LoadFromParameterArray<String>(file.Parameters["ANALOG", "DESCRIPTIONS"], analogChannelCount);
            String[] units = C3DParameterHelper.LoadFromParameterArray<String>(file.Parameters["ANALOG", "UNITS"], analogChannelCount);

            for (Int32 i = 0; i < analogChannelCount; i++)
            {
                this.lvItems.Items.Add(new ListViewItem(new String[] {
                    (i + 1).ToString(),
                    (labels != null && labels.Length > i ? labels[i].TrimEnd() : ""),
                    (descriptions != null && descriptions.Length > i ? descriptions[i].TrimEnd() : ""),
                    (offset != null && offset.Length > i ? offset[i].ToString() : ""),
                    (scale != null && scale.Length > i ? scale[i].ToString() : ""),
                    (units != null && units.Length > i ? units[i].TrimEnd() : "")}));
            }
        }
    }
}