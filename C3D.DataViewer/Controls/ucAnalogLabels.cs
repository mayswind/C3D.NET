using System;
using System.Windows.Forms;

namespace C3D.DataViewer.Controls
{
    public partial class ucAnalogLabels : UserControl
    {
        public ucAnalogLabels(C3DFile file)
        {
            InitializeComponent();

            UInt16 analogChannelCount = (file.Parameters.ContainsParameter("ANALOG", "USED") ? file.Parameters["ANALOG", "USED"].GetData<UInt16>() :
                (UInt16)(file.Header.AnalogSamplesPerFrame != 0 ? file.Header.AnalogMeasurementCount / file.Header.AnalogSamplesPerFrame : 0));

            String[] labels = this.LoadFromArray<String>(file.Parameters["ANALOG", "LABELS"], analogChannelCount);
            String[] descriptions = this.LoadFromArray<String>(file.Parameters["ANALOG", "DESCRIPTIONS"], analogChannelCount);

            Int16[] offset = this.LoadFromArray<Int16>(file.Parameters["ANALOG", "OFFSET"], analogChannelCount);
            Single[] scale = this.LoadFromArray<Single>(file.Parameters["ANALOG", "SCALE"], analogChannelCount);
            String[] units = this.LoadFromArray<String>(file.Parameters["ANALOG", "UNITS"], analogChannelCount);

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

        private T[] LoadFromArray<T>(C3DParameter parameter, Int32 size)
        {
            Object raw = (parameter != null ? parameter.GetData() : null);
            T[] ret = null;

            if (raw != null && raw is T[])
            {
                ret = raw as T[];
            }
            else if (raw != null && raw is T && size > 0)
            {
                ret = new T[size];
                T unit = (T)raw;

                for (Int32 i = 0; i < ret.Length; i++)
                {
                    ret[i] = unit;
                }
            }

            return ret;
        }
    }
}