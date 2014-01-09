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

            UInt16 analogChannelCount = (file.Parameters != null && file.Parameters.ContainsParameter("ANALOG", "USED") ? file.Parameters["ANALOG", "USED"].GetData<UInt16>() :
                (UInt16)(file.Header.AnalogSamplesPerFrame != 0 ? file.Header.AnalogMeasurementCount / file.Header.AnalogSamplesPerFrame : 0));

            String[] labels = C3DParameterHelper.LoadFromParameterArray<String>(file.Parameters["ANALOG", "LABELS"], analogChannelCount);
            String[] descriptions = C3DParameterHelper.LoadFromParameterArray<String>(file.Parameters["ANALOG", "DESCRIPTIONS"], analogChannelCount);

            Int16[] offset = C3DParameterHelper.LoadFromParameterArray<Int16>(file.Parameters["ANALOG", "OFFSET"], analogChannelCount);
            Single[] scale = C3DParameterHelper.LoadFromParameterArray<Single>(file.Parameters["ANALOG", "SCALE"], analogChannelCount);
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