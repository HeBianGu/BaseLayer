using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ProcessHelper
{
    /// <summary> 执行cmd的命令 </summary>
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
            Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c " + _commandString;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.CreateNoWindow = false;
            return p;
        }

        [Description("引擎正常退出")]
        public override void DoEndOfEngine()
        {
            base.DoEndOfEngine();
        }

        [Description("引擎停止退出")]
        public override void DoStopOfEngine()
        {
            base.DoStopOfEngine();
        }
    }
}
