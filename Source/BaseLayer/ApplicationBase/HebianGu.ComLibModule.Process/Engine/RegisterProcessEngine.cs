using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ProcessHelper
{
    /// <summary> 调用注册表引擎 </summary>
    class RegisterProcessEngine : ProcessEngine
    {
        public RegisterProcessEngine(string exeName)
        {
            _exeName = exeName;

            this.Build();
        }
        private string _exeName;
        /// <summary> 注册表中exe的名称 </summary>
        public string ExeName
        {
            get { return _exeName; }
        }

        public override Process CreateProcess()
        {
            //  啟動一個獨立進程   
            Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c ";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;

            return p;
        }


  
    }
}
