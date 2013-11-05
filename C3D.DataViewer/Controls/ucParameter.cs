using System;
using System.Windows.Forms;

namespace C3D.DataViewer.Controls
{
    public partial class ucParameter : UserControl
    {
        public ucParameter(C3DFile file, Int32 type, String name)
        {
            InitializeComponent();

            if (type == 0)
            {
                C3DParameterGroup group = file.Parameters.GetGroup(name);
                this.lvItems.Items.Add(new ListViewItem(new String[] { "ID", group.ID.ToString() }));
                this.lvItems.Items.Add(new ListViewItem(new String[] { "Name", group.Name }));
                this.lvItems.Items.Add(new ListViewItem(new String[] { "Description", group.Description }));
                this.lvItems.Items.Add(new ListViewItem(new String[] { "Locked", group.IsLocked.ToString() }));
            }
            else if (type == 1)
            {
                C3DParameter param = file.Parameters[name];
                this.lvItems.Items.Add(new ListViewItem(new String[] { "ID", param.ID.ToString() }));
                this.lvItems.Items.Add(new ListViewItem(new String[] { "Name", param.Name }));
                this.lvItems.Items.Add(new ListViewItem(new String[] { "Description", param.Description }));
                this.lvItems.Items.Add(new ListViewItem(new String[] { "Locked", param.IsLocked.ToString() }));
                this.lvItems.Items.Add(new ListViewItem(new String[] { "Type", param.C3DParameterType.ToString() }));

                Object data = param.InnerData;
                if (data != null && (data.GetType() == typeof(Char) || data.GetType() == typeof(Byte) || data.GetType() == typeof(Int16) || data.GetType() == typeof(Single)))
                {
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimension", "0" }));
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Data", data.ToString() }));
                }
                else if (data != null && (data.GetType() == typeof(String)))
                {
                    String s = data as String;
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimension", "1" }));
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimensions[0]", s.Length.ToString() }));
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Data", s }));
                }
                else if (data != null && (data.GetType() == typeof(Char[]) || data.GetType() == typeof(Byte[]) || data.GetType() == typeof(Int16[]) || data.GetType() == typeof(Single[])))
                {
                    Array array = data as Array;
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimension", "1" }));
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimensions[0]", array.GetLength(0).ToString() }));

                    for (Int32 i = 0; i < array.GetLength(0); i++)
                    {
                        this.lvItems.Items.Add(new ListViewItem(new String[] { "Data[" + i.ToString() + "]", array.GetValue(i).ToString() }));
                    }
                }
                else if (data != null && (data.GetType() == typeof(String[])))
                {
                    String[] array = data as String[];
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimension", "2" }));
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimensions[0]", (array.Length > 0 ? array[0].Length : 0).ToString() }));
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimensions[1]", array.Length.ToString() }));

                    for (Int32 i = 0; i < array.Length; i++)
                    {
                        this.lvItems.Items.Add(new ListViewItem(new String[] { "Data[" + i.ToString() + "]", array[i] }));
                    }
                }
                else if (data != null && (data.GetType() == typeof(Char[,]) || data.GetType() == typeof(Byte[,]) || data.GetType() == typeof(Int16[,]) || data.GetType() == typeof(Single[,])))
                {
                    Array array = data as Array;
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimension", "2" }));
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimensions[0]", array.GetLength(0).ToString() }));
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimensions[1]", array.GetLength(1).ToString() }));

                    for (Int32 i = 0; i < array.GetLength(0); i++)
                    {
                        for (Int32 j = 0; j < array.GetLength(1); j++)
                        {
                            this.lvItems.Items.Add(new ListViewItem(new String[] { "Data[" + i.ToString() + ", " + j.ToString() + "]", array.GetValue(i, j).ToString() }));
                        }
                    }
                }
                else if (data != null && (data.GetType() == typeof(Char[, ,]) || data.GetType() == typeof(Byte[, ,]) || data.GetType() == typeof(Int16[, ,]) || data.GetType() == typeof(Single[, ,])))
                {
                    Array array = data as Array;
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimension", "3" }));
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimensions[0]", array.GetLength(0).ToString() }));
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimensions[1]", array.GetLength(1).ToString() }));
                    this.lvItems.Items.Add(new ListViewItem(new String[] { "Dimensions[2]", array.GetLength(2).ToString() }));

                    for (Int32 i = 0; i < array.GetLength(0); i++)
                    {
                        for (Int32 j = 0; j < array.GetLength(1); j++)
                        {
                            for (Int32 k = 0; k < array.GetLength(2); k++)
                            {
                                this.lvItems.Items.Add(new ListViewItem(new String[] { "Data[" + i.ToString() + ", " + j.ToString() + ", " + k.ToString() + "]", array.GetValue(i, j, k).ToString() }));
                            }
                        }
                    }
                }
            }
        }
    }
}