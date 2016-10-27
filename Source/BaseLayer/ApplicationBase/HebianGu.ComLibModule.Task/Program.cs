using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.TaskEx
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            for (int i = 0; i < 5; i++)
            {
                //  创建记事本任务
                CaseTaskCore caseTaskCore = new CaseTaskCore();
                caseTaskCore.FilePath = "notepad";
                caseTaskCore.FileName = "notepad";

                //  将记事本添加到任务管理器中
                TaskListManager<CaseTaskCore>.Instance.ContinueLast(caseTaskCore, true);

            }
        }
    }

    public class CaseTaskCore : BaseTaskCore
    {
        string fileName;
        /// <summary> 文件名称 </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        string filePath;
        /// <summary> 文件全路径 包含文件名 </summary>
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        Process p;
        public override int RunCaseTaskWork(System.Threading.CancellationToken pToken)
        {
            Console.WriteLine("开始执行任务：" + this.GetType().Name);

            //  启动进程
            p = Process.Start(this.filePath);

            return 0;
        }

        /// <summary> 停止任务 </summary>
        public void Stop()
        {
            this.IsRestart = false;

            if (p.HasExited)
                return;

            this.p.CloseMainWindow();

            p.Kill();
        }
    }
}
