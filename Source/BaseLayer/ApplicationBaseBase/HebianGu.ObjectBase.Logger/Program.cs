using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.Logger
{
    class Program
    {
        static void Main(string[] args)
        {

            string message = "测试测试测试 总共三种写日志的方法目前 LogProviderHandler Log4netEngine LogConsoleEngine";
            
            // Todo ：测试Log4net 

            //  注册日志方法
            LogProviderHandler.Instance.RunLog += l => LogFactory.Instance.GetLogService<Log4netEngine>().RunLog(l);
            LogProviderHandler.Instance.ErrLog += l => LogFactory.Instance.GetLogService<Log4netEngine>().ErrorLog(l);
            LogProviderHandler.Instance.ErrExLog += l => LogFactory.Instance.GetLogService<Log4netEngine>().ErrorLog(l);

            //  写日志方法
            LogProviderHandler.Instance.OnRunLog(message);
            LogProviderHandler.Instance.OnErrLog(message);
            
            // Todo ：测试CosoleLog 
            
            // 注册日志方法
            LogProviderHandler.Instance.RunLog += l => LogFactory.Instance.GetLogService<LogConsoleEngine>().RunLog(l);
            LogProviderHandler.Instance.ErrLog += l => LogFactory.Instance.GetLogService<LogConsoleEngine>().ErrorLog(l);
            LogProviderHandler.Instance.ErrExLog += l => LogFactory.Instance.GetLogService<LogConsoleEngine>().ErrorLog(l);

            //  写日志方法
            LogProviderHandler.Instance.OnRunLog(message);
            LogProviderHandler.Instance.OnErrLog(message);
            
            // Todo ：不用 LogProviderHandler
            LogFactory.Instance.GetLogService<Log4netEngine>().RunLog(message);
            LogFactory.Instance.GetLogService<Log4netEngine>().ErrorLog(message);

            LogFactory.Instance.GetLogService<LogConsoleEngine>().RunLog(message);
            LogFactory.Instance.GetLogService<LogConsoleEngine>().RunLog(message);

            LogConsoleEngine log=new LogConsoleEngine();
            ComLogProvider.Init(log);
            ComLogProvider.Log.RunLog("静态注册日志");

            Log4netEngine log1 = new Log4netEngine();

            log1.RunLog("记录运行日志");

            Console.Read();
        }


    }
}
