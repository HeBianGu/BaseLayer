#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/3/28 10:06:56
 * 文件名：LogTest
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HebianGu.ObjectBase.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibMethods.UnitTester
{
    [TestClass]
    class LogTest
    {

        /// <summary> 此方法的说明 </summary>
        [TestMethod]
        public void RunMethod()
        {
            string message = "测试测试测试 总共三种写日志的方法目前 LogProviderHandler Log4netEngine LogConsoleEngine";

            // Todo ：测试Log4net 

            //  注册日志方法
            LogProviderHandler.Instance.RunLog += (l, e) => LogFactory.Instance.GetLogService<Log4netEngine>().RunLog(l);
            LogProviderHandler.Instance.ErrLog += (l, e) => LogFactory.Instance.GetLogService<Log4netEngine>().ErrorLog(l);
            LogProviderHandler.Instance.ErrExLog += (l, e) => LogFactory.Instance.GetLogService<Log4netEngine>().ErrorLog(l);

            //  写日志方法
            LogProviderHandler.Instance.OnRunLog(message);
            LogProviderHandler.Instance.OnErrLog(message);

            // Todo ：测试CosoleLog 

            // 注册日志方法
            LogProviderHandler.Instance.RunLog += (l, e) => LogFactory.Instance.GetLogService<LogConsoleEngine>().RunLog(l);
            LogProviderHandler.Instance.ErrLog += (l, e) => LogFactory.Instance.GetLogService<LogConsoleEngine>().ErrorLog(l);
            LogProviderHandler.Instance.ErrExLog += (l, e) => LogFactory.Instance.GetLogService<LogConsoleEngine>().ErrorLog(l);

            //  写日志方法
            LogProviderHandler.Instance.OnRunLog(message);
            LogProviderHandler.Instance.OnErrLog(message);

            // Todo ：不用 LogProviderHandler
            LogFactory.Instance.GetLogService<Log4netEngine>().RunLog(message);
            LogFactory.Instance.GetLogService<Log4netEngine>().ErrorLog(message);

            LogFactory.Instance.GetLogService<LogConsoleEngine>().RunLog(message);
            LogFactory.Instance.GetLogService<LogConsoleEngine>().RunLog(message);

            LogConsoleEngine log = new LogConsoleEngine();
            ComLogProvider.Init(log);
            ComLogProvider.Log.RunLog("静态注册日志");

            Log4netEngine log1 = new Log4netEngine();

            log1.RunLog("记录运行日志");

        }

    }
}
