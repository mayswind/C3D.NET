using System;
using System.Windows.Forms;

namespace C3D.DataViewer.Controls
{
    public partial class ucPointLabels : UserControl
    {
        public ucPointLabels(C3DFile file)
        {
            InitializeComponent();

            String[] labels = (file.Parameters.ContainsParameter("POINT", "LABELS") ? file.Parameters["POINT", "LABELS"].GetData<String[]>() : null);
            String[] descriptions = (file.Parameters.ContainsParameter("POINT", "DESCRIPTIONS") ? file.Parameters["POINT", "DESCRIPTIONS"].GetData<String[]>() : null);

            if (labels != null && labels.Length > 0)
            {
                for (Int32 i = 0; i < labels.Length; i++)
                {
                    this.lvItems.Items.Add(new ListViewItem(new String[] { (i + 1).ToString(), labels[i].TrimEnd(), (descriptions != null && descriptions.Length > i ? descriptions[i].TrimEnd() : "") }));
                }
            }
        }
    }
}