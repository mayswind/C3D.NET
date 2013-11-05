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
            Int16[] offset = (file.Parameters.ContainsParameter("ANALOG", "OFFSET") ? file.Parameters["ANALOG", "OFFSET"].GetData<Int16[]>() : null);
            Single[] scale = (file.Parameters.ContainsParameter("ANALOG", "SCALE") ? file.Parameters["ANALOG", "SCALE"].GetData<Single[]>() : null);
            String[] units = null;
            var rawunits = (file.Parameters.ContainsParameter("ANALOG", "UNITS") ? file.Parameters["ANALOG", "UNITS"].GetData() : null);

            if (rawunits != null && rawunits is String[])
            {
                units = (String[])rawunits;
            }
            else if (rawunits != null && labels != null)
            {
                units = new String[labels.Length];
                String unit = rawunits.ToString();

                for (Int32 i = 0; i < units.Length; i++)
                {
                    units[i] = unit;
                }
            }

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
    }
}