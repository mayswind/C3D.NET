using System;
using System.Windows.Forms;

using C3D.DataViewer.Helper;

namespace C3D.DataViewer.Controls
{
    public partial class PointLabelsControl : UserControl
    {
        public PointLabelsControl(C3DFile file)
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

            UInt16 pointCount = (file.Parameters != null && file.Parameters.ContainsParameter("POINT", "USED") ? file.Parameters["POINT", "USED"].GetData<UInt16>() : file.Header.PointCount);

            String[] labels = C3DParameterHelper.LoadFromParameterArray<String>(file.Parameters["POINT", "LABELS"], pointCount);
            String[] descriptions = C3DParameterHelper.LoadFromParameterArray<String>(file.Parameters["POINT", "DESCRIPTIONS"], pointCount);

            for (Int32 i = 0; i < pointCount; i++)
            {
                this.lvItems.Items.Add(new ListViewItem(new String[] {
                    (i + 1).ToString(),
                    (labels != null && labels.Length > i ? labels[i].TrimEnd() : ""),
                    (descriptions != null && descriptions.Length > i ? descriptions[i].TrimEnd() : "")}));
            }
        }
    }
}