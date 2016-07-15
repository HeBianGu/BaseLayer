using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace HebianGu.ComLibModule.ThreadEx
{
    /// <summary> 后台进度条窗体 设置BackGroudWork的RunWorkerAsync事件即可 </summary>
    public partial class ProgBackWorkForm : Form
    {

        private BackgroundWorker _BackgroundWorker;

        /// <summary> 进度条窗口的后台进程 </summary>
        public BackgroundWorker BackgroundWorker
        {
            get { return _BackgroundWorker; }
            set { _BackgroundWorker = value; }
        }

        object _argument = null;

        /// <summary> 进程要传递的参数 </summary>
        public object Argument
        {
            get { return _argument; }
            set { _argument = value; }
        }

        public ProgBackWorkForm()
        {
            InitializeComponent();
            // 实例化后台对象
            _BackgroundWorker = new BackgroundWorker();
            // 设置可以通告进度
            _BackgroundWorker.WorkerReportsProgress = true;
            // 设置可以取消
            _BackgroundWorker.WorkerSupportsCancellation = true;

            _BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(UpdateProgress);

            _BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompletedWork);

            this.Load += ProgBackWorkForm_Load;

        }

        /// <summary> 开始执行任务 </summary>
        void ProgBackWorkForm_Load(object sender, EventArgs e)
        {

           

        }

        /// <summary> 更新进度事件 </summary>
        void UpdateProgress(object sender, ProgressChangedEventArgs e)
        {
            //  获取百分比
            int progress = e.ProgressPercentage;

            //  修改进度
            this.progressBar1.Value = progress;

            //  加载到窗体
            this.lb_percent.Text = string.Format("{0}%", progress);
        }

        /// <summary> 完成事件 </summary>
        void CompletedWork(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.lb_ditial.Text = "Error";

            }
            else if (e.Cancelled)
            {

                this.lb_ditial.Text = "Canceled";

            }
            else
            {

                this.lb_ditial.Text = "Completed";

            }

            this.progressBar1.Value = this.progressBar1.Maximum;
        }

        /// <summary> 取消 </summary>
        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            _BackgroundWorker.CancelAsync();
        }

        private void ProgBackWorkForm_Shown(object sender, EventArgs e)
        {
            // 执行任务
            _BackgroundWorker.RunWorkerAsync(_argument);
        }

    }
}
