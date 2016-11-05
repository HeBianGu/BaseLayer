using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HebianGu.ComLibModule.TimerHelper
{

    /// <summary> 定时任务引擎 </summary>
    public partial class TimerEngine
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static TimerEngine t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static TimerEngine Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new TimerEngine();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        protected TimerEngine()
        {

        }
        #endregion - 单例模式 End -
    }

    public partial class TimerEngine
    {
        Timer _time;

        protected Timer Time
        {
             get { return _time; }
        }

        double interval = 1000;

        public double Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        /// <summary> 开始任务 </summary>
        public void Start()
        {
            _time = new Timer();
            _time.Interval = this.interval;
            _time.Elapsed += _time_Elapsed;
            _time.Start();
        }

        protected virtual void _time_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Todo ：触发任务 
            _act.Invoke();
        }


        /// <summary> 停止计时 </summary>
        public void Stop()
        {
            _time.Stop();
        }


        /// <summary> 注册任务 </summary>
        public void Register(Action act)
        {
            this.Act += act;
        }


        Action _act;

        public event Action Act
        {
            add { _act += value; }
            remove { _act -= value; }
        }

    }
}
