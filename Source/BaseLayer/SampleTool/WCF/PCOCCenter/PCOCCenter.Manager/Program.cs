using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;

namespace OPT.PCOCCenter.Manager
{
    static class Program
    {
        public static string Server { get; set; }
        public static string UserName { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

            if (OPT.PCOCCenter.Client.Client.Login("PCOCCenter", "PCOCCenter.Manager", "Ver6.0.0.1", "PCOCCenter") == 1)
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