using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Wcf
{
    /// <summary> 提供控制台日志输出 </summary>
    public class LogProvider
    {
        public static LogProvider Instance = new LogProvider();

        int Step = 1;

        /// <summary> 输出步骤格式 </summary>
        public string StepString
        {
            get
            {
                return string.Format("步骤 {0} ,", (Step++).ToString());
            }
        }

        /// <summary> 时间间隔 </summary>
        public string TimeSpan
        {
            get
            {
                return string.Format("  时间：{0}  间隔 {1} ,", DateTime.Now.ToString(), (DateTime.Now - CurrentTime).ToString());
            }
        }
        public DateTime CurrentTime { get; set; }

        string runLogFormat = "     正在运行：[{0}]";
        /// <summary> 运行日志格式 </summary>
        string RunLogFormat
        {
            get { return StepString + runLogFormat; }
        }

        string errLogFormat = "     处理异常：[{0}]";
        /// <summary> 错误日志格式 </summary>
        string ErrLogFormat
        {
            get { return StepString + errLogFormat; }
        }

        /// <summary> 写运行日志 </summary>>
        public void RunLog(string str)
        {
            //Console.WriteLine(StepString + TimeSpan);
            string outtemp = string.Format(RunLogFormat, str);
            Console.WriteLine(outtemp);
            this.CurrentTime = DateTime.Now;
        }

        /// <summary> 写错误日志 </summary>
        public void ErrLog(string str)
        {
            //Console.WriteLine(StepString + TimeSpan);
            string outtemp = string.Format(ErrLogFormat, str);
            Console.WriteLine(outtemp);
            this.CurrentTime = DateTime.Now;
        }

        /// <summary> 写错误日志 </summary>
        public void ErrLog(Exception ex)
        {
            //Console.WriteLine(StepString + TimeSpan);
            string outtemp = string.Format(ErrLogFormat, ex.Message);
            Console.WriteLine(outtemp);
            this.CurrentTime = DateTime.Now;
        }
    }
}
