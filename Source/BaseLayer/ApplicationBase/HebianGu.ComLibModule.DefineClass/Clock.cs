using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HebianGu.ComLibModule.Define
{
    /// <summary> 闹钟类 </summary>
    public class Clock : IDisposable
    {
        private System.Timers.Timer _time = new System.Timers.Timer(10000);
        private int _hour = 0;
        private int _minute = 0;
        private bool _isPrcMinute = false;

        public event ElapsedEventHandler Alarm;

        public Clock(int Hour, int Minute)
        {
            _hour = Hour;
            _minute = Minute;
            _time.Elapsed += new ElapsedEventHandler(_time_Elapsed);
        }

        public Clock(int Hour, int Minute, ElapsedEventHandler Alarm)
        {
            _hour = Hour;
            _minute = Minute;
            _time.Elapsed += new ElapsedEventHandler(_time_Elapsed);
            this.Alarm += Alarm;
            Start();
        }

        ~Clock()
        {
            Dispose();
        }

        public void Start() { _time.Start(); }

        public void Stop() { _time.Stop(); }

        private void _time_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (e.SignalTime.Hour == _hour && e.SignalTime.Minute == _minute)
            {
                if (!_isPrcMinute)
                {
                    _isPrcMinute = true;
                    if (Alarm != null) Alarm(this, e);
                }
            }
            else
            {
                _isPrcMinute = false;
            }
        }

        public void Dispose()
        {
            _time.Stop();
            _time.Dispose();
        }
    }
}
