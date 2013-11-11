using System;
using System.Windows.Forms;

namespace C3D.DataViewer.Controls
{
    public partial class ucAnalogLabels : UserControl
    {
        public ucAnalogLabels(C3DFile file)
        {
            InitializeComponent();

            String[] labels = (file.Parameters.ContainsParameter("ANALOG", "LABELS") ? file.Parameters["ANALOG", "LABELS"].GetData<String[]>() : null);
            String[] descriptions = (file.Parameters.ContainsParameter("ANALOG", "DESCRIPTIONS") ? file.Parameters["ANALOG", "DESCRIPTIONS"].GetData<String[]>() : null);

            Int16[] offset = this.LoadFromArray<Int16>(file.Parameters["ANALOG", "OFFSET"], (labels != null ? labels.Length : 0));
            Single[] scale = this.LoadFromArray<Single>(file.Parameters["ANALOG", "SCALE"], (labels != null ? labels.Length : 0));
            String[] units = this.LoadFromArray<String>(file.Parameters["ANALOG", "UNITS"], (labels != null ? labels.Length : 0));

            if (labels != null && labels.Length > 0)
            {
                for (Int32 i = 0; i < labels.Length; i++)
                {
                    this.lvItems.Items.Add(new ListViewItem(new String[] {
                        (i + 1).ToString(), labels[i].TrimEnd(), (descriptions != null && descriptions.Length > i ? descriptions[i].TrimEnd() : ""),
                        (offset != null && offset.Length > i ? offset[i].ToString() : ""),
                        (scale != null && scale.Length > i ? scale[i].ToString() : ""),
                        (units != null && units.Length > i ? units[i].TrimEnd() : "")}));
                }
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