using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HebianGu.ComLibModule.ControlHelper
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            // Todo ：颜色拾取控件 
            Application.Run(new MainForm());

            //// Todo ：滚动文件控件 
            //Application.Run(new Form1());
        }
    }
}
