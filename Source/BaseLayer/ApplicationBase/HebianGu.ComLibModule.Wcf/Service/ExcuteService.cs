using HebianGu.ComLibModule.CMD;
using HebianGu.ComLibModule.Wcf.Service.Entity;
using HebianGu.ComLibModule.Wcf.Service.Interface;
using HebianGu.ComLibModule.WinHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HebianGu.ComLibModule.Wcf.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class ExcuteService : IExcuteService
    {
        /// <summary> 执行Cmd命令 </summary>
        public void ExecuteCmd(string cmdString)
        {
            Process.Start(cmdString);
        }

        /// <summary> 执行Cmd命令 </summary>
        public string ExecuteCmdOutPut(string cmdString)
        {
            return CmdAPI.RunCmdOutPut(cmdString);
        }


    }
}
