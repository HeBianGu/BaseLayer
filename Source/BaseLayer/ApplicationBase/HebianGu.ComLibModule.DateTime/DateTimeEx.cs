#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/3 10:26:13
 * 文件名：DateTimeEx
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.DateTimeEx
{
    public static class DateTimeEx
    {

        /// <summary> 比较两个日期天数部分是否相等 </summary>
        public static bool IsEqulByDay(this DateTime ptime, DateTime comTime)
        {
            TimeSpan span = ptime - comTime;
            if (span.TotalDays == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary> 字符串转换成日期 string format = "ddMMMyyyy"; </summary>
        public static DateTime ToDateTime(this string str, string format)
        {
            DateTime ss = DateTime.ParseExact(str, format, CultureInfo.InvariantCulture);

            return ss;
        }


        /// <summary> 根据指定天数截取时间(包含时间和结束事件) </summary>
        public static List<DateTime> SplitDTsByDay(this DateTime start, DateTime end, int daySpan)
        {

            List<DateTime> times = new List<DateTime>();

            if (start > end)
            {
                return null;
            }

            TimeSpan span = end - start;

            double totalDays = span.TotalDays;

            DateTime pTime = start;

            times.Add(pTime);

            while (totalDays > 0)
            {
                pTime = pTime.AddDays(daySpan);

                //  小于最后时间
                span = end - pTime;
                if (span.Days > 0)
                {
                    times.Add(pTime);
                }

                totalDays -= daySpan;
            }
            times.Add(end);

            return times;
        }

        /// <summary> 根据指定月数截取时间(包含时间和结束事件) </summary>
        public static List<DateTime> SplitDTsByMonth(this DateTime start, DateTime end, int monthSpan)
        {

            List<DateTime> times = new List<DateTime>();

            if (start > end)
            {
                return null;
            }

            TimeSpan span = end - start;

            DateTime pTime = start;

            times.Add(pTime);

            while (true)
            {
                pTime = pTime.AddMonths(monthSpan);

                //  小于最后时间
                span = end - pTime;
                if (span.TotalDays > 0 && (end.Month != start.Month))
                {
                    times.Add(pTime);
                }
                else
                {
                    break;
                }

            }
            times.Add(end);

            return times;
        }


    }
}
