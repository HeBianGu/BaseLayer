using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Define
{
    public abstract class LogPackaging
    {
        private static log4net.ILog
            log = log4net.LogManager.GetLogger("mdc");

        private static LogPackaging
            logPackaging = null;

        /// <summary> 获取日志包装器 </summary>
        public static LogPackaging
            GetLog()
        {
            if (logPackaging == null)
            {
                logPackaging = new LogPackaging_();
            }
            return logPackaging;
        }

        /// <summary> 写日志 </summary>
        /// <param name="message">消息</param>
        public void
            WriteLog(object message)
        {
            WriteLog(null, message);
        }

        /// <summary> 写日志 </summary>
        /// <param name="sender">触发源</param>
        /// <param name="message">日志</param>
        public void
            WriteLog(object sender, object message)
        {
            GetILog(sender).Info(message);
        }

        /// <summary> 写异常 </summary>
        /// <param name="e">异常</param>
        public void
            WriteErr(Exception e)
        {
            WriteErr(null, e);
        }

        /// <summary> 写异常 </summary>
        /// <param name="sender">触发源</param>
        /// <param name="e">异常</param>
        public void
            WriteErr(object sender, Exception e)
        {
            GetILog(sender).Error(e.Message, e);

        }

        private ILog
            GetILog(Object o)
        {
            if (o == null)
                return log;

            Type t = null;
            if (o is Type)
            {
                t = o as Type;
            }
            else
            {
                t = o.GetType();
            }
            return LogManager.GetLogger(t);
        }

        class LogPackaging_ : LogPackaging { }
    }
}
