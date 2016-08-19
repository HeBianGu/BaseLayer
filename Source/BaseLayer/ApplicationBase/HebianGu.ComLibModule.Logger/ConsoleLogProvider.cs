using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Logger
{
    /// <summary> 提供控制台日志输出 </summary>
    public class ConsoleLogProvider
    {
        public static ConsoleLogProvider Instance = new ConsoleLogProvider();

        int Step = 1;

        /// <summary> 输出步骤格式 </summary>
        public string StepString
        {
            get
            {
                return string.Format("步骤 {0} ,", (Step++).ToString());
            }
        }

        string runLogFormat = "正在运行：{0}";
        /// <summary> 运行日志格式 </summary>
        string RunLogFormat
        {
            get { return StepString + runLogFormat; }
        }

        string errLogFormat = "处理异常：{0}";
        /// <summary> 错误日志格式 </summary>
        string ErrLogFormat
        {
            get { return StepString + errLogFormat; }
        }

        /// <summary> 写运行日志 </summary>>
        public void RunLog(string str)
        {
            string outtemp = string.Format(RunLogFormat, str);
            Console.WriteLine(outtemp);
        }

        /// <summary> 写错误日志 </summary>
        public void ErrLog(string str)
        {
            string outtemp = string.Format(ErrLogFormat, str);
            Console.WriteLine(outtemp);
        }

        /// <summary> 写错误日志 </summary>
        public void ErrLog(Exception ex)
        {
            string outtemp = string.Format(ErrLogFormat, ex.Message);
            Console.WriteLine(outtemp);
        }
    }
}
