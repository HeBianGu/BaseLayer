using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.TimerHelper
{
    /// <summary> 每天的时分秒触发 </summary>
    public class DayTimerEngine : TimerEngine
    {
        DateTime _startTime;


        /// <summary> 创建天的时间格式 </summary>
        public static DateTime BuildDay(int h, int m, int s)
        {
            return DateTime.MinValue.Date.AddHours(h).AddMinutes(m).AddSeconds(s);
        }


        /// <summary> 每天的起始监测时间（只关注时间部分 不关注日期） </summary> 
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        DateTime _endTime;
        /// <summary> 每天的结束监测时间（只关注时间部分 不关注日期） </summary> 
        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        protected override void _time_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TimeSpan start = DateTime.Now.TimeOfDay - _startTime.TimeOfDay;

            TimeSpan end = DateTime.Now.TimeOfDay - _endTime.TimeOfDay;

            // Todo ：小于当前起始时间 
            if (start.TotalSeconds < 0)
            {
                this.Time.Interval = start.TotalSeconds;

            }
            // Todo ：介于两者之间 
            else if (start.TotalSeconds >= 0 && end.TotalSeconds <= 0)
            {
                this.Time.Interval = this.Interval;
            }
            // Todo ：大于起始时间 
            else if (end.TotalSeconds > 0)
            {
                TimeSpan outspan = _endTime.AddDays(1).Date - _endTime;

                outspan = outspan + _startTime.TimeOfDay;

                this.Time.Interval = outspan.TotalSeconds;
            }


            base._time_Elapsed(sender, e);
        }
    }
}
