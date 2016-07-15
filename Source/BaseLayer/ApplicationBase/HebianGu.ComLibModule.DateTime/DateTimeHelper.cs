using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace HebianGu.ComLibModule.DateTimeEx
{
    /// <summary> 日期查找扩展方法 </summary>
    public static partial class DateTimeHelper
    {
        /// <summary> 根据时间返回几个月前,几天前,几小时前,几分钟前,几秒前 </summary>
        public static string DateStringFromNow(this DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60)
            {
                return dt.ToShortDateString();
            }
            else if (span.TotalDays > 30)
            {
                return "1个月前";
            }
            else if (span.TotalDays > 14)
            {
                return "2周前";
            }
            else if (span.TotalDays > 7)
            {
                return "1周前";
            }
            else if (span.TotalDays > 1)
            {
                return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            }
            else if (span.TotalHours > 1)
            {
                return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            }
            else if (span.TotalMinutes > 1)
            {
                return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            }
            else if (span.TotalSeconds >= 1)
            {
                return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
            }
            else
            {
                return "1秒前";
            }
        }

        /// <summary> 计算两个时间的差值,返回的是x天x小时x分钟x秒 </summary>
        public static string DateDiff(this DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            //TimeSpan ts=ts1.Add(ts2).Duration();
            dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            return dateDiff;
        }

        /// <summary> 时间相差值,返回时间差调用时,isTotal为true时,返回的时带小数的天数,否则返回的是整数 </summary>
        public static string DateDiff(this DateTime DateTime1, DateTime DateTime2, bool isTotal)
        {
            TimeSpan ts = DateTime2 - DateTime1;
            if (isTotal)
                //带小数的天数，比如1天12小时结果就是1.5 
                return ts.TotalDays.ToString();
            else
                //整数天数，1天12小时或者1天20小时结果都是1 
                return ts.Days.ToString();
        }

        /// <summary> 日期比较 比较天数 大于天数返回true，小于返回false </summary>
        public static bool CompareDate(string today, string writeDate, int n)
        {
            DateTime Today = Convert.ToDateTime(today);
            DateTime WriteDate = Convert.ToDateTime(writeDate);
            WriteDate = WriteDate.AddDays(n);
            if (Today >= WriteDate)
                return false;
            else
                return true;
        }

        /// <summary> 根据英文的星期几返回中文的星期几 如WhichDay("Sunday"),返回星期日 </summary>
        public static string WhichDay(string enWeek)
        {
            switch (enWeek.Trim())
            {
                case "Sunday":
                    return "星期日";
                case "Monday":
                    return "星期一";
                case "Tuesday":
                    return "星期二";
                case "Wednesday":
                    return "星期三";
                case "Thursday":
                    return "星期四";
                case "Friday":
                    return "星期五";
                case "Saturday":
                    return "星期六";
                default:
                    return enWeek;
            }
        }

        /// <summary> 根据出生年月进行生日提醒 </summary>
        public static string GetBirthdayTip(DateTime birthday)
        {
            DateTime now = DateTime.Now;
            //TimeSpan span = DateTime.Now - birthday;
            int nowMonth = now.Month;
            int birtMonth = birthday.Month;
            if (nowMonth == 12 && birtMonth == 1)
                return string.Format("下月{0}号", birthday.Day);
            if (nowMonth == 1 && birtMonth == 12)
                return string.Format("上月{0}号", birthday.Day);
            int months = now.Month - birthday.Month;
            //int days = now.Day - birthday.Day;
            if (months == 1)
                return string.Format("上月{0}号", birthday.Day);
            else if (months == -1)
                return string.Format("下月{0}号", birthday.Day);
            else if (months == 0)
            {
                if (now.Day == birthday.Day)
                    return "今天";
                return string.Format("本月{0}号", birthday.Day);
            }
            else if (months > 1)
                return string.Format("已过{0}月", months);
            else
                return string.Format("{0}月{1}日", birthday.Month, birthday.Day);
        }

    }

    /// <summary> 日期查找扩展方法 </summary>
    public static partial class DateTimeHelper
    {
        /// <summary> 周日 上周-1 下周+1 本周0 </summary>
        public static string GetSunday(this DateTime dateTime, int? weeks)
        {
            int week = weeks ?? 0;

            return dateTime.AddDays(Convert.ToDouble((0 - Convert.ToInt16(dateTime.DayOfWeek))) + 7 * week).ToShortDateString();
        }

        /// <summary> 周六 上周-1 下周+1 本周0</summary>
        public static string GetSaturday(this DateTime dateTime, int? weeks)
        {
            int week = weeks ?? 0;
            return dateTime.AddDays(Convert.ToDouble((6 - Convert.ToInt16(dateTime.DayOfWeek))) + 7 * week).ToShortDateString();
        }

        /// <summary> 月第一天 上月-1 本月0 下月1 </summary>
        public static string GetFirstDayOfMonth(this DateTime dateTime, int? months)
        {
            int month = months ?? 0;
            return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(month).ToShortDateString();
        }

        /// <summary> 月最后一天 上月0 本月1 下月2 </summary>
        public static string GetLastDayOfMonth(this DateTime dateTime, int? months)
        {
            int month = months ?? 0;
            return DateTime.Parse(dateTime.ToString("yyyy-MM-01")).AddMonths(month).AddDays(-1).ToShortDateString();
        }

        /// <summary> 年度第一天 </summary>
        public static string GetFirstDayOfYear(this DateTime dateTime)
        {
            int year = Convert.ToInt32(dateTime.Year);
            return DateTime.Parse(dateTime.ToString("yyyy-01-01")).AddYears(year).ToShortDateString();
        }

        /// <summary> 年度最后一天 </summary>
        public static string GetLastDayOfYear(this DateTime dateTime)
        {
            int year = Convert.ToInt32(dateTime.Year);
            return DateTime.Parse(dateTime.ToString("yyyy-01-01")).AddYears(year).AddDays(-1).ToShortDateString();
        }

        /// <summary> 季度第一天 上季度-1 下季度+1 </summary>
        public static string GetFirstDayOfQuarter(this DateTime dateTime, int? quarters)
        {
            int quarter = quarters ?? 0;
            return dateTime.AddMonths(quarter * 3 - ((dateTime.Month - 1) % 3)).ToString("yyyy-MM-01");
        }

        /// <summary> 季度最后一天 上季度0 本季度1 下季度2 </summary>
        public static string GetLastDayOfQuarter(this DateTime dateTime, int? quarters)
        {
            int quarter = quarters ?? 0;
            return
                DateTime.Parse(dateTime.AddMonths(quarter * 3 - ((dateTime.Month - 1) % 3)).ToString("yyyy-MM-01")).AddDays(-1).
                    ToShortDateString();
        }

        /// <summary> 中文星期 </summary>
        public static string GetDayOfWeekCN(this DateTime dateTime)
        {
            var day = new[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            return day[Convert.ToInt16(dateTime.DayOfWeek)];
        }

        /// <summary> 获取星期数字形式,周一开始 </summary>
        public static int GetDayOfWeekNum(this DateTime dateTime)
        {
            int day = (Convert.ToInt16(dateTime.DayOfWeek) == 0) ? 7 : Convert.ToInt16(dateTime.DayOfWeek);
            return day;
        }

        /// <summary>  取指定日期是一年中的第几周 </summary> 
        public static int GetWeekofyear(this DateTime dtime)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dtime, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        /// <summary> 取指定日期是一月中的第几天 </summary> 
        public static int GetDayofmonth(this DateTime dtime)
        {
            return CultureInfo.CurrentCulture.Calendar.GetDayOfMonth(dtime);
        }
    }
}
