using System;
using System.Text;
using System.Windows.Forms;

namespace C3D.DataViewer.Helper
{
    /// <summary>
    /// ListView扩展类
    /// </summary>
    internal static class ListViewExtension
    {
        /// <summary>
        /// 返回指定ListView表格格式内容
        /// </summary>
        /// <param name="listView">ListView控件</param>
        /// <exception cref="ArgumentNullException">ListView不能为空</exception>
        /// <returns>ListView表格格式内容</returns>
        internal static String GetTableContent(this ListView listView)
        {
            if (listView == null)
            {
                throw new ArgumentNullException("listView");
            }

            StringBuilder sb = new StringBuilder();

            for (Int32 i = 0; i < listView.Columns.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append('\t');
                }

                sb.Append(listView.Columns[i].Text);
            }

            sb.AppendLine();

            for (Int32 i = 0; i < listView.Items.Count; i++)
            {
                for (Int32 j = 0; j < listView.Items[i].SubItems.Count; j++)
                {
                    if (j > 0)
                    {
                        sb.Append('\t');
                    }

                    sb.Append(listView.Items[i].SubItems[j].Text);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}