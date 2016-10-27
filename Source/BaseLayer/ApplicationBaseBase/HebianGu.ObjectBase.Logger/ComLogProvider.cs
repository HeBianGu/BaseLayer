using HebianGu.ObjectBase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.Logger
{
    /// <summary> 主要提供组件内错误日志信息 </summary>
    public class ComLogProvider : ILogInterface
    {

        public static ILogInterface Log = null;

        /// <summary> 初始化日志信息 </summary>
        public static void Init(ILogInterface log)
        {
            Log = log;
        }

        /// <summary> 写错误日志 </summary>
        public void ErrorLog(Exception ex)
        {
            if (Log == null) return;

            Log.ErrorLog(ex);
        }


        /// <summary> 写错误日志 </summary>
        public void ErrorLog(string message)
        {
            if (Log == null) return;

            Log.ErrorLog(message);
        }
        
        /// <summary> 写运行日志 </summary>
        public void RunLog(string message)
        {
            if (Log == null) return;

            Log.RunLog(message);
        }
    }
}
