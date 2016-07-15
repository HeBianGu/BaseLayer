using HebianGu.ComLibModule.TaskEx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HebianGu.ComLibModule.ProcessEx;
using HebianGu.ComLibModule.DateTimeEx;

namespace DemoTaskManager
{
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

        Process p = null;

        /// <summary> 抽象方法，提供子类扩展 </summary>
        public override int RunCaseTaskWork(System.Threading.CancellationToken pToken)
        {
            p = new Process();

            p.RunExe("DemoConsoleExe.exe", "");

            

            return 0;
        }


        public void Stop()
        {
            this.IsRestart = false;
            if(p!=null)
            {
                if(!p.HasExited)
                {
                    p.CloseMainWindow();

                    p.Kill();
                }
            }
        }
    }
}
