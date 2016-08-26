using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ProcessHelper
{
    /// <summary> 执行外部exe </summary>
    class ExeProcessEngine : ProcessEngine
    {

        private string _args;

        public string Args
        {
            get { return _args; }
            set { _args = value; }
        }

        private FileInfo _exeFile;

        public FileInfo ExeFile
        {
            get { return _exeFile; }
            set { _exeFile = value; }
        }

        private string _workDirctory;
        /// <summary> exe要运行的工作区域 相当于在这个目录下双击 </summary>
        public string WorkDirctory
        {
            get { return _workDirctory; }
            set { _workDirctory = value; }
        }


        public override Process CreateProcess()
        {
            Process process = new Process();
            ProcessStartInfo psI = new ProcessStartInfo(_exeFile.Name, _args);
            psI.WorkingDirectory = _workDirctory;
            process.StartInfo = psI;
            psI.UseShellExecute = false;
            psI.CreateNoWindow = true;
            process.StartInfo = psI;
            return process;
        }

       
        public override void DoEndOfEngine()
        {
           
        }

       
        public override void DoStopOfEngine()
        {
        }

        public override void SetLog(IProcessEngineLog log)
        {
            throw new NotImplementedException();
        }


    }


}
