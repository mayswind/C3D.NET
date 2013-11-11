using System;
using System.IO;
using System.Windows.Forms;

using C3D.DataViewer.Controls;

namespace C3D.DataViewer
{
    public partial class frmMain : Form
    {
        private String _currentFileName;
        private C3DFile _currentFile;

        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(String filePath)
        {
            InitializeComponent();

            if (!String.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                this.OpenFile(filePath);
            }
        }

        private void frmMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None);
        }

        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            String[] files = e.Data.GetData(DataFormats.FileDrop, false) as String[];
            if (files != null && files.Length > 0)
            {
                this.OpenFile(files[0]);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (this.dlgOpen.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(this.dlgOpen.FileName))
            {
                this.OpenFile(this.dlgOpen.FileName);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.CloseFile();
        }

        private void tvItems_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.scMain.Panel2.Controls.Clear();

            if (this._currentFile == null)
            {
                return;
            }

            String tag = e.Node.Tag as String;

            if (tag.Equals("HEADER"))
            {
                this.scMain.Panel2.Controls.Add(new ucHeader(this._currentFile) { Width = this.scMain.Panel2.Width, Height = this.scMain.Panel2.Height, Dock = DockStyle.Fill });
            }
            else if (tag.Equals("EVENTS"))
            {
                this.scMain.Panel2.Controls.Add(new ucEvents(this._currentFile) { Width = this.scMain.Panel2.Width, Height = this.scMain.Panel2.Height, Dock = DockStyle.Fill });
            }
            else if (tag.Contains("PARAMETERS_GROUP|"))
            {
                String name = tag.Replace("PARAMETERS_GROUP|", "");
                this.scMain.Panel2.Controls.Add(new ucParameter(this._currentFile, 0, name) { Width = this.scMain.Panel2.Width, Height = this.scMain.Panel2.Height, Dock = DockStyle.Fill });
            }
            else if (tag.Contains("PARAMETERS_ITEM|"))
            {
                String name = tag.Replace("PARAMETERS_ITEM|", "");
                this.scMain.Panel2.Controls.Add(new ucParameter(this._currentFile, 1, name) { Width = this.scMain.Panel2.Width, Height = this.scMain.Panel2.Height, Dock = DockStyle.Fill });
            }
            else if (tag.Equals("3D"))
            {
                this.scMain.Panel2.Controls.Add(new ucPointLabels(this._currentFile) { Width = this.scMain.Panel2.Width, Height = this.scMain.Panel2.Height, Dock = DockStyle.Fill });
            }
            else if (tag.Contains("3D|"))
            {
                Int32 id = Int32.Parse(tag.Replace("3D|", ""));
                this.scMain.Panel2.Controls.Add(new uc3DPoint(this._currentFile, id) { Width = this.scMain.Panel2.Width, Height = this.scMain.Panel2.Height, Dock = DockStyle.Fill });
            }
            else if (tag.Equals("ANALOG"))
            {
                this.scMain.Panel2.Controls.Add(new ucAnalogLabels(this._currentFile) { Width = this.scMain.Panel2.Width, Height = this.scMain.Panel2.Height, Dock = DockStyle.Fill });
            }
            else if (tag.Contains("ANALOG|"))
            {
                Int32 id = Int32.Parse(tag.Replace("ANALOG|", ""));
                this.scMain.Panel2.Controls.Add(new ucAnalogSamples(this._currentFile, id) { Width = this.scMain.Panel2.Width, Height = this.scMain.Panel2.Height, Dock = DockStyle.Fill });
            }
        }
        
        private void OpenFile(String filePath)
        {
            this._currentFileName = filePath;
            this._currentFile = C3DFile.LoadFromFile(this._currentFileName);
            this.Text = String.Format("{0} - C3D.NET DataViewer", this._currentFileName);

            this.scMain.Panel2.Controls.Clear();
            this.ShowTreeList();
        }

        private void CloseFile()
        {
            this._currentFileName = null;
            this._currentFile = null;
            this.Text = "C3D.NET DataViewer";

            this.scMain.Panel2.Controls.Clear();
            this.tvItems.Nodes.Clear();
        }

        private void ShowTreeList()
        {
            this.tvItems.Nodes.Clear();

            TreeNode header = TreeListConverter.GetHeaderNode(this._currentFile);
            this.tvItems.Nodes.Add(header);
            this.tvItems.Nodes.Add(TreeListConverter.GetParametersNode(this._currentFile));
            this.tvItems.Nodes.Add(TreeListConverter.Get3DDataNode(this._currentFile));
            this.tvItems.Nodes.Add(TreeListConverter.GetAnalogDataNode(this._currentFile));

            this.tvItems.CollapseAll();
            this.tvItems.SelectedNode = header;
        }
    }
}