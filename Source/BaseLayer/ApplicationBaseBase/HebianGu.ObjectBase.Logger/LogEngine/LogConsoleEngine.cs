using HebianGu.ObjectBase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.Logger
{

    /// <summary> 控制台写日志 </summary>
    public class LogConsoleEngine : ILogInterface
    {

        public void ErrorLog(Exception ex)
        {
            Console.WriteLine("错误信息：" + ex);
        }

        public void ErrorLog(string message)
        {
            Console.WriteLine("错误信息：" + message);
        }

        public void RunLog(string message)
        {
            Console.WriteLine("运行信息：" + message);
        }
    }
}
