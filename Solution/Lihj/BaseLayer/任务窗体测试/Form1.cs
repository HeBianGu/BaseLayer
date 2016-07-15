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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CancellationTokenSource cts = new CancellationTokenSource();
        Task parent = null;
        private void button1_Click(object sender, EventArgs e)
        {

            parent = new Task(() =>
            {
                //   1、 取消任务类
                var cts = new CancellationTokenSource();
                var taskFactory = new TaskFactory<string>(
                    cts.Token,
                    TaskCreationOptions.AttachedToParent, // 2、 优先级
                    TaskContinuationOptions.ExecuteSynchronously, // 3、 创建的任务指定行为
                    TaskScheduler.Current);

                //  4、 创建并启动3个子任务

                var childTasks = new[] { 
                      taskFactory.StartNew(() => Sum(cts.Token, 100)), 
                      taskFactory.StartNew(() => Sum(cts.Token, 200)), 
                      taskFactory.StartNew(() => Sum(cts.Token, Int32.MaxValue))  // 这个会抛异常

              };


                //  设置任何子任务抛出异常就取消其余子任务

                for (int i = 0; i < childTasks.Length; i++)
                {
                    childTasks[i].ContinueWith(l => cts.Cancel(), TaskContinuationOptions.OnlyOnFaulted);
                }


                // 所有子任务完成后，从未出错/未取消的任务获取返回的最大值 
                // 然后将最大值传给另一个任务来显示最大结果

                taskFactory.ContinueWhenAll(childTasks,
                    completedTasks => completedTasks.Where(t => !t.IsFaulted && !t.IsCanceled).Max(t => t.Result),
                    CancellationToken.None).ContinueWith(t => MessageBox.Show("The maxinum is: " + t.Result),
                    TaskContinuationOptions.ExecuteSynchronously).Wait(); // Wait用于测试

            });


            // 子任务完成后，也显示任何未处理的异常

            parent.ContinueWith(p =>
            {
                // 用StringBuilder输出所有
                StringBuilder sb = new StringBuilder("The following exception(s) occurred:" + Environment.NewLine);
                foreach (var l in p.Exception.Flatten().InnerExceptions)
                {
                    sb.AppendLine("   " + l.GetType().ToString());
                }

                Console.WriteLine(sb.ToString());
            }, TaskContinuationOptions.OnlyOnFaulted);


            // 启动父任务

            parent.Start();


        }

        private  string Sum(CancellationToken ct, Int32 n)
        {
            Int32 sum = 0;

            while (true)
            {
                if (n < 0)
                    break;
                ct.ThrowIfCancellationRequested();
                n--;
                sum += n;
            }
            return sum.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //在之后的某个时间，取消CancellationTokenSource 以取消Task

            
            cts.Cancel();//这是个异步请求，Task可能已经完成了。我是双核机器，Task没有完成过 16 


            if(parent.IsCanceled)
            {
                MessageBox.Show("任务已取消！");
            }
            else
            {
                MessageBox.Show("任务取消失败！");
            }
        }
    }
}
