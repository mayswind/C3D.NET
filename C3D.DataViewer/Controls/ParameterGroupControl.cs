using System;
using System.Windows.Forms;

namespace C3D.DataViewer.Controls
{
    public partial class ParameterGroupControl : UserControl
    {
        public ParameterGroupControl(C3DFile file)
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

            if (file.Parameters != null && file.Parameters.Count > 0)
            {
                foreach (C3DParameterGroup group in file.Parameters)
                {
                    this.lvItems.Items.Add(new ListViewItem(new String[] { group.ID.ToString(), group.Name, group.Description }));
                }
            }
        }
    }
}