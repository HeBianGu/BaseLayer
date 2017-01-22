using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //Process.Start("notepad");

            TestCmd();

            //TestRegister();

            Console.Read();
        }

        public static void TestCmd()
        {
            string cmdStr = @"eclrun eclipse D:\TestCase\3T1.DATA";

            CmdProcessEngine cmdProcess = new CmdProcessEngine(cmdStr);

            ProcessLogger log = new ProcessLogger();

            cmdProcess.SetLog(log);

            cmdProcess.Start();

            while(true)
            {
                string exit = Console.ReadLine();

                if (exit == "exit")
                {
                    cmdProcess.Stop();
                }
            }
            
        }

        public static void TestRegister()
        {
            string cmdStr = "notepad";

            RegisterProcessEngine cmdProcess = new RegisterProcessEngine(cmdStr);

            ProcessEngineLog log = new ProcessEngineLog();
            cmdProcess.SetLog(log);
            cmdProcess.Start();

        }

         
    }

    class ProcessEngineLog:IProcessEngineLog
    {

        public void RunLog(string result)
        {
            Console.WriteLine(result);
        }

        public void ErrLog(string errstring)
        {
            Console.WriteLine(errstring);
        }

        public void ErrLog(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
