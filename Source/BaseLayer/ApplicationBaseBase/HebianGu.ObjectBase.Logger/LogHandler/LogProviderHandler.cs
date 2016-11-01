using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.Logger
{

    /// <summary> 注册事件写日志 (优势可以注册取消多个，静态注册) </summary>
    public partial class LogProviderHandler
    {

        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static LogProviderHandler t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static LogProviderHandler Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new LogProviderHandler();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private LogProviderHandler()
        {

        }
        #endregion - 单例模式 End -

    }

    partial class LogProviderHandler
    {
        /// <summary> 注册日志 p1=运行日志 p2=错误日志 p3=错误日志 </summary>
        public void RegisterLogComponent(Action<string> runlog, Action<string> errlog, Action<Exception> errExlog)
        {
            this.RunLog += runlog;
            this.ErrLog += errlog;
            this.ErrExLog += errExlog;
        }


        /// <summary> 清理日志 </summary>
        public void Clear()
        {

        }


        Action<string> _runLog;

        /// <summary> 运行日志委托 </summary>
        public event Action<string> RunLog
        {
            add
            {
                _runLog += value;
            }
            remove
            {
                if (_runLog != null)
                    _runLog -= value;
            }
        }

        Action<string> _errLog;

        /// <summary> 错误日志委托 </summary>
        public event Action<string> ErrLog
        {
            add
            {
                _errLog += value;
            }
            remove
            {
                if (_errLog != null)
                    _errLog -= value;
            }
        }

        Action<Exception> _errExLog;

        /// <summary> 错误日志委托 </summary>
        public event Action<Exception> ErrExLog
        {
            add
            {
                _errExLog += value;
            }
            remove
            {
                if (_errExLog != null)
                    _errExLog -= value;
            }
        }


        /// <summary> 此方法的说明 </summary>
        public void OnRunLog(string message)
        {
            if (this._runLog == null) return;

            this._runLog(message);
        }

        /// <summary> 此方法的说明 </summary>
        public void OnErrLog(string message)
        {
            if (this._errLog == null) return;

            this._errLog(message);
        }
        /// <summary> 此方法的说明 </summary>
        public void OnErrLog(Exception message)
        {
            if (this._errExLog == null) return;

            this._errExLog(message);
        }
    }
}
