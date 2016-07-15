using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 多线程
{
    public class BackgroundWorkerTest
    {

        public void CreateBackgroundWorker()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork +=bw_DoWork;
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
        }

        public void bw_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        public void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
    }
}
