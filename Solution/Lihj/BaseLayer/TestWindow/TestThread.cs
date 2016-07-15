using HebianGu.ComLibModule.ThreadEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWindow
{
    public partial class TestThread : Form
    {
        public TestThread()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProgBackWorkForm p = new ProgBackWorkForm();
            p.BackgroundWorker.DoWork += BackgroundWorker_DoWork;
            p.Show();
        }

        void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
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

                //this.Invoke(new MethodInvoker(()=>this.button1.Text=i.ToString()));

                this.Invoke(new MethodInvoker(() => this.button1.Text = i.ToString()));

                bw.ReportProgress(i++);

                Thread.Sleep(1000);

            }
        }
    }
}
