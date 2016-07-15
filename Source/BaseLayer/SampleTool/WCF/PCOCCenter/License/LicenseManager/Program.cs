using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using OPT.PEOfficeCenter.Utils;

namespace OPT.PEOfficeCenter.LicenseManager
{
    static class Program
    {

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
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

            LoginForm loginForm = new LoginForm();

            DialogResult ret = loginForm.ShowDialog();

            if (ret == DialogResult.OK)
                Application.Run(new MainForm());
            else
            {
                if (ret != DialogResult.Cancel)
                    MessageBox.Show("用户名或密码错误，请确认后再登录！", "提示");
            }
        }
    }
}