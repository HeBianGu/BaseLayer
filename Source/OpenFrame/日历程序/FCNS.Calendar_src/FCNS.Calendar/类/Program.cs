using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace FCNS.Calendar
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
            //Application.Run(new Form1());
            Mutex mt = null;
            try
            {
                mt = Mutex.OpenExisting("FCNS.Calendar");
            }
            catch (WaitHandleCannotBeOpenedException)
            {

            }

            if (mt == null)
            {
                mt = new Mutex(true, "FCNS.Calendar");
                Application.Run(new Main());
                mt.ReleaseMutex();
            }
            else
            {
                mt.Close();
                MessageBox.Show("程序已运行");
                Application.Exit();
            }
        }
    }
}