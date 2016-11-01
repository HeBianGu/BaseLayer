using HebianGu.ObjectBase.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.Logger
{
    /// <summary> 写日志文件 </summary>
    public class Log4netEngine : ILogInterface
    {
        public void ErrorLog(Exception ex)
        {
            Log4netProvider.Logger.Error(ex);
        }

        public void ErrorLog(string message)
        {
            Log4netProvider.Logger.Error(message);
        }

        public void RunLog(string message)
        {
            Log4netProvider.Logger.Debug(message);
        }
    }
}
