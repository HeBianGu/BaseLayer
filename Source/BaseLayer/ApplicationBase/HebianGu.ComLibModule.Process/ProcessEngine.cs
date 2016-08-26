using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HebianGu.ComLibModule.ObjectBase;
using System.Reflection;
using HebianGu.ComLibModule.EnumEx;

namespace HebianGu.ComLibModule.ProcessHelper
{
    abstract class ProcessEngine
    {

        #region - 定义抽象成员 -

        Process _process;

        private ProcessResult _result;
        /// <summary> 运行结果 </summary>
        public ProcessResult Result
        {
            get { return _result; }
            private set
            {
                _result = value;

                if (_log != null)
                {
                    //  状态改变触发日志

                    DescriptionAttribute ss = _result.GetAttribute<DescriptionAttribute>();

                    _log.RunLog(ss.Description);
                }

            }
        }


        EventHandler _exsitEventHanlder = null;


        IProcessEngineLog _log = null;
        /// <summary> 引擎过程日志 </summary>
        internal IProcessEngineLog Log
        {
            get { return _log; }
        }


        #endregion

        #region - 定义抽象结构 -
        public ProcessEngine()
        {
            _exsitEventHanlder = (object sender, EventArgs e) =>
            {
                EndOfEngine();
            };
        }

        /// <summary> 创建引擎 </summary>
        protected void Build()
        {

            //  创建进程
            _process = this.CreateProcess();

            //  创建结束流程
            _process.EnableRaisingEvents = true;
            _process.Exited += _exsitEventHanlder;

            this.Result = ProcessResult.Ready;
        }

        /// <summary> 进程结束 </summary>
        void EndOfEngine()
        {
            this.Result = ProcessResult.Over;

            this.DoEndOfEngine();

            //  根据子类检验运行结果输出结果
            this.Result = this.CheckResult() ? ProcessResult.Success : ProcessResult.Error;
        } 

        void StopOfEngine()
        {

            this.Result = ProcessResult.Stop;
            this.DoStopOfEngine();
        }


        #endregion

        /// <summary> 启动进程 </summary>
        public void Start()
        {
            if (_process == null) return;//|| _process.HasExited

            _process.Start();

            this.Result = ProcessResult.Running;

            _process.WaitForExit();


        }

        /// <summary> 停止引擎 </summary>
        public void Stop()
        {
            if (_process == null || _process.HasExited) return;

            //  当停止殷勤注销掉触发事件
            _process.Exited -= _exsitEventHanlder;

            this.StopOfEngine();

            _process.Kill();

            _process.Dispose();

        }


        /// <summary> 创建进程的方法 子类提供扩展 </summary>
        public abstract Process CreateProcess();

        /// <summary> 进程结束触发的方法 </summary>
        [Description("引擎正常退出时的事件")]
        public virtual void DoEndOfEngine()
        {
            if (this.Log == null) return;
            DescriptionAttribute attr = MethodInfo.GetCurrentMethod().GetAttribute<DescriptionAttribute>();
            this.Log.RunLog(attr.Description);


        }

        /// <summary> 进程停止触发的方法 </summary>
        [Description("引擎正常退出时的事件")]
        public virtual void DoStopOfEngine()
        {
            if (this.Log == null) return;
            DescriptionAttribute attr = MethodInfo.GetCurrentMethod().GetAttribute<DescriptionAttribute>();
            this.Log.RunLog(attr.Description);
        }

        /// <summary> 设置进程输出日志 </summary>
        public virtual void SetLog(IProcessEngineLog log)
        {
            this._log = log;
        }

        /// <summary> 检查运行结果 </summary>
        public virtual bool CheckResult()
        {
            return true;
        }
    }

    /// <summary> 进程运行结果 </summary>
    enum ProcessResult
    {
        [Description("运行成功")]
        Success = 0,
        [Description("运行出错")]
        Error,
        [Description("正在运行")]
        Running,
        [Description("准备就绪")]
        Ready,
        [Description("运行结束")]
        Over,
        [Description("运行中断")]
        Stop
    }

    /// <summary> 进程写日志的接口 </summary>
    public interface IProcessEngineLog
    {
        void RunLog(string result);

        void ErrLog(string errstring);

        void ErrLog(Exception ex);
    }

}
