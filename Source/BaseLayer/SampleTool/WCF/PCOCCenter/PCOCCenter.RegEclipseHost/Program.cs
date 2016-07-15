using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OPT.PCOCCenter.RegEclipseHost
{
    static class Program
    {
        public static string Server { get; set; }
        public static string UserName { get; set; }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (OPT.PCOCCenter.Client.Client.Login("PCOCCenter", "PCOCCenter.RegEclipseHost", "Ver6.0.0.1", "PCOCCenter") == 1)
            {
                Server = OPT.PCOCCenter.Client.Client.gServer;
                UserName = OPT.PCOCCenter.Client.Client.gUserName;
                Application.Run(new MainForm());
                OPT.PCOCCenter.Client.Client.Logout();
            }
            else
            {
                if (OPT.PCOCCenter.Client.Client.ErrorInfo != null)
                    MessageBox.Show(OPT.PCOCCenter.Client.Client.ErrorInfo, "提示");
            }

        }
    }
}
