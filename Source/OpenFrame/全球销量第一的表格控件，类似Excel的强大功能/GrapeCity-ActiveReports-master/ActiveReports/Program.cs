using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AppLoadingMT;
namespace GrapeCity.ActiveReports.Samples.EndUserDesigner
{
    static class Program
    {
        /// <summary>
        ///The main entry point for the application. 
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //	show the splash form
            Splasher.Show();

            //	process application startup
            DoStartup(args);

            //	if the form is still shown...
            Splasher.Close();
        }

        static void DoStartup(string[] args)
        {
            //	do whatever you need to do
            EndUserDesigner f = new EndUserDesigner();
            Application.Run(f);
        }


    }
}
