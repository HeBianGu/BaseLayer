using HebianGu.ComLibModule.ThreadEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HebianGu.ComLibModule.DateTimeEx;

namespace BaseTester
{
    class Program
    {
        static void Main(string[] args)
        {

            //ProgBackWorkForm p = new ProgBackWorkForm();
            ////p.BackgroundWorker.DoWork+=BackgroundWorker_DoWork;
            //p.Show();

         

            ReadTime();

            Console.Read();

        }

        static void  BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            //MainWindow win = e.Argument as MainWindow;

            int i = 0;
            while (i <= 100)
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                bw.ReportProgress(i++);

                Thread.Sleep(1000);

            }
        }


        static void  ReadTime()
        {
           List<DateTime> times= DateTime.Now.SplitToDateTimes(DateTime.Now.AddDays(5), 2);

            foreach(var v in times)
            {
                Console.WriteLine(v.Date.ToString());
            }
        }
    }
}
