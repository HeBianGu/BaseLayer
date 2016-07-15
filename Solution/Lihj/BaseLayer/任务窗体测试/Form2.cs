using HebianGu.ComLibModule.TaskEx;
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

namespace 任务窗体测试
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        TaskManager<Worker> manager = null;
        private void button1_Click(object sender, EventArgs e)
        {
            manager = new TaskManager<Worker>(RunningTask, AllOver);

            Worker worker = new Worker();

            Worker worker1 = new Worker();

            Worker worker2 = new Worker();

            Worker worker3 = new Worker();

            manager.ContinueLast(worker);

            manager.ContinueLast(worker1);

            manager.ContinueLast(worker2);

            manager.ContinueLast(worker3);

            manager.Start();

            manager.Wait();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            manager.Stop();

            MessageBox.Show("已终止任务，终止时处理的任务是" + manager.RunTask.Id);
        }

        void RunningTask(Task<int> lastTask, Task<int> perTask)
        {
          
            if (lastTask.IsFaulted)
            {
                MessageBox.Show(lastTask.Id + "任务报错" + lastTask.Result);
            }
            if (lastTask.IsCompleted)
            {
                MessageBox.Show(lastTask.Id + "任务完成" + lastTask.Result);
            }
            if (lastTask.IsCanceled)
            {
                MessageBox.Show(lastTask.Id + "任务取消" + lastTask.Result);
            }

            MessageBox.Show("正在运行任务" + perTask.Id);
        }

        int AllOver(Task<int> lastTask)
        {
            if (manager.IsComplete)
            {
                string formatstr = "完成的任务{0},取消的任务{1},报错的任务{2},总数{3}";
                MessageBox.Show(string.Format(formatstr, manager.CompleteTask.Count.ToString(), manager.CancelTask.Count.ToString(), manager.FaultTask.Count.ToString(), manager.TotalCount.ToString()));
            }
            return TaskResultID.CompleteParam;
        }
    }

    public class Worker : IWorkInterface
    {

        public int RunWork(System.Threading.CancellationToken pToken)
        {
            int i = 50;
            while (i > 0)
            {
                i--;
                Thread.Sleep(100);
            }
            return i;
        }
    }
}
