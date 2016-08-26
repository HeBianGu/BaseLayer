using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ProcessHelper
{
    class CmdProcessEngine : ProcessEngine
    {
        public CmdProcessEngine(string cmdstr)
        {
            _commandString = cmdstr;

            this.Build();
        }

        private string _commandString;
        /// <summary> 要执行的Cmd命令 </summary>
        public string CmdString
        {
            get { return _commandString; }
        }

        public override Process CreateProcess()
        {
            //  啟動一個獨立進程   
            Process p = new System.Diagnostics.Process();
            //p.StartInfo.FileName = "cmd.exe";
            //p.StartInfo.Arguments = "/c " + _commandString;
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardInput = true;
            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.RedirectStandardError = true;
            //p.StartInfo.CreateNoWindow = true;

            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c " + _commandString;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.CreateNoWindow = false;
            //p.EnableRaisingEvents = true;


            return p;
        }

        [Description("引擎正常退出时的事件")]
        public override void DoEndOfEngine()
        {
            base.DoEndOfEngine();
        }

        [Description("引擎正常退出时的事件")]
        public override void DoStopOfEngine()
        {
            base.DoStopOfEngine();
        }
    }
}
