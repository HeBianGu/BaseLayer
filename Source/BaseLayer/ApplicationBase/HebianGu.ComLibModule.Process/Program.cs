using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ProcessHelper
{
    class Program
    {
        static void Main(string[] args)
        {

            TestCmd();

            Console.Read();
        }

        public static void TestCmd()
        {
            string cmdStr = @"eclrun eclipse D:\TestCase\3T1.DATA";

            //string cmdStr = "notepad";


            CmdProcessEngine cmdProcess = new CmdProcessEngine(cmdStr);

            ProcessLogger log = new ProcessLogger();
            cmdProcess.SetLog(log);
            cmdProcess.Start();
        }

        public static void TestRegister()
        {
            string cmdStr = "cmd";

            RegisterProcessEngine cmdProcess = new RegisterProcessEngine(cmdStr);

            cmdProcess.Start();
        }
    }
}
