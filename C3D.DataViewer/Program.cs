using System;
using System.Windows.Forms;

namespace C3D.DataViewer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain((args != null && args.Length == 1) ? args[0] : String.Empty));
        }
    }
}