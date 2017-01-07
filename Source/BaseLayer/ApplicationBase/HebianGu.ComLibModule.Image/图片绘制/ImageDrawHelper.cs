#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/1/6 15:33:48
 * 文件名：ImageDrawHelper
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ImageHelper
{
    /// <summary> 图片绘制类 </summary>
    class ImageDrawHelper
    {

        /// <summary> 绘制月历日期 </summary>
        public void DrawCalender(Graphics g, int year, int month, int day)
        {
            DateTime dtN = DateTime.Parse(year.ToString(CultureInfo.InvariantCulture) + "-" +
                                month.ToString(CultureInfo.InvariantCulture) + "-" +
                                day.ToString(CultureInfo.InvariantCulture));

            int firstDayofWeek = (int)dtN.DayOfWeek;

            int endMonthDay = (int)DateTime.DaysInMonth(year, month);

            //记录日期信息
            string[,] dateArray = new string[7, 6];

            DateTime dtNow = DateTime.Parse(DateTime.Now.ToShortDateString());

            var font = new Font("", 14);
            var solidBrushWeekdays = new SolidBrush(Color.Gray);
            var solidBrushWeekend = new SolidBrush(Color.Chocolate);
            var solidBrushHoliday = new SolidBrush(Color.BurlyWood);
            int num = 1;

            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (j == 0 && i < firstDayofWeek) //定义当月第一天的星期的位置
                    {
                        continue;
                    }
                    else
                    {
                        if (num > endMonthDay) //定义当月最后一天的位置
                        {
                            break;
                        }
                        string cMonth = null;
                        string cDay = null;
                        string cHoliday = null;
                        string ccHoliday = null;

                        if (i > 0 && i < 6)
                        {
                            DateTime dt = DateTime.Parse(year + "-" + month + "-" + num);
                            TimeSpan ts = dt - dtNow;
                            dateArray[i, j] = dt.ToShortDateString();

                            if (ts.Days == 0)
                            {
                                g.DrawEllipse(new Pen(Color.Chocolate, 3), (15 + 80 * i), (85 + 60 * j), 30, 15);
                            }

                            cMonth = ChineseDate.GetMonth(dt);
                            cDay = ChineseDate.GetDay(dt);
                            cHoliday = ChineseDate.GetHoliday(dt);
                            ccHoliday = ChineseDate.GetChinaHoliday(dt);

                            if (cHoliday != null)
                            {
                                //绘阳历节日
                                g.DrawString(cHoliday.Length > 3 ? cHoliday.Substring(0, 3) : cHoliday, new Font("", 9),
                                             solidBrushHoliday, (40 + 80 * i), (90 + 60 * j));
                            }
                            //绘农历
                            if (ccHoliday != "")
                            {
                                g.DrawString(ccHoliday, new Font("", 10), solidBrushWeekdays, (25 + 80 * i),
                                                                         (115 + 60 * j));
                            }
                            else
                            {
                                g.DrawString(cDay == "初一" ? cMonth : cDay, new Font("", 10), solidBrushWeekdays, (25 + 80 * i),
                                                                         (115 + 60 * j));
                            }


                            //绘日期
                            g.DrawString(num.ToString(CultureInfo.InvariantCulture), font, solidBrushWeekdays,
                                         (15 + 80 * i), (80 + 60 * j));

                        }
                        else
                        {
                            var dt = DateTime.Parse(year + "-" + month + "-" + num);
                            var ts = dt - dtNow;
                            dateArray[i, j] = dt.ToShortDateString();
                            if (ts.Days == 0)
                            {
                                g.DrawEllipse(new Pen(Color.Chocolate, 3), (15 + 80 * i), (85 + 60 * j), 30, 15);
                            }

                            cMonth = ChineseDate.GetMonth(dt);
                            cDay = ChineseDate.GetDay(dt);
                            cHoliday = ChineseDate.GetHoliday(dt);
                            ccHoliday = ChineseDate.GetChinaHoliday(dt);

                            if (cHoliday != null)
                            {
                                //绘阳历节日
                                g.DrawString(cHoliday.Length > 3 ? cHoliday.Substring(0, 3) : cHoliday, new Font("", 9),
                                             solidBrushHoliday, (40 + 80 * i), (90 + 60 * j));
                            }
                            //绘农历
                            if (ccHoliday != "")
                            {
                                g.DrawString(ccHoliday, new Font("", 10), solidBrushWeekend, (25 + 80 * i),
                                         (115 + 60 * j));
                            }
                            else
                            {
                                g.DrawString(cDay == "初一" ? cMonth : cDay, new Font("", 10), solidBrushWeekend, (25 + 80 * i),
                                         (115 + 60 * j));
                            }

                            //绘日期
                            g.DrawString(num.ToString(CultureInfo.InvariantCulture), font, solidBrushWeekend,
                                         (15 + 80 * i), (80 + 60 * j));
                        }

                        num++;

                    }

                }
            }
        }

    }

    /// <summary> 中国农历 </summary>
    public static class ChineseDate
    {
        private static ChineseLunisolarCalendar china = new ChineseLunisolarCalendar();
        private static Hashtable gHoliday = new Hashtable();
        private static Hashtable nHoliday = new Hashtable();

        private static string[] JQ = {
                                         "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑"
                                         , "立秋", "处暑", "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
                                     };

        private static int[] JQData = {
                                          0, 21208, 42467, 63836, 85337, 107014, 128867, 150921, 173149, 195551, 218072,
                                          240693, 263343, 285989, 308563, 331033, 353350, 375494, 397447, 419210, 440795,
                                          462224, 483532, 504758
                                      };

        static ChineseDate()
        {
            //公历节日
            gHoliday.Add("0101", "元旦");
            gHoliday.Add("0214", "情人节");
            gHoliday.Add("0305", "雷锋日");
            gHoliday.Add("0308", "妇女节");
            gHoliday.Add("0312", "植树节");
            gHoliday.Add("0315", "消权日");
            gHoliday.Add("0401", "愚人节");
            gHoliday.Add("0501", "劳动节");
            gHoliday.Add("0504", "青年节");
            gHoliday.Add("0601", "儿童节");
            gHoliday.Add("0701", "建党节");
            gHoliday.Add("0801", "建军节");
            gHoliday.Add("0910", "教师节");
            gHoliday.Add("1001", "国庆节");
            gHoliday.Add("1224", "平安夜");
            gHoliday.Add("1225", "圣诞节");

            //农历节日
            nHoliday.Add("0101", "春节");
            nHoliday.Add("0115", "元宵节");
            nHoliday.Add("0505", "端午节");
            nHoliday.Add("0815", "中秋节");
            nHoliday.Add("0909", "重阳节");
            nHoliday.Add("1208", "腊八节");
        }

        /// <summary> 获取农历 </summary>
        public static string GetChinaDate(DateTime dt)
        {
            if (dt > china.MaxSupportedDateTime || dt < china.MinSupportedDateTime)
            {
                //日期范围：1901 年 2 月 19 日 - 2101 年 1 月 28 日
                throw new Exception(string.Format("日期超出范围！必须在{0}到{1}之间！",
                                                  china.MinSupportedDateTime.ToString("yyyy-MM-dd"),
                                                  china.MaxSupportedDateTime.ToString("yyyy-MM-dd")));
            }
            string str = string.Format("{0} {1}{2}", GetYear(dt), GetMonth(dt), GetDay(dt));
            string strJQ = GetSolarTerm(dt);
            if (strJQ != "")
            {
                str += " (" + strJQ + ")";
            }
            string strHoliday = GetHoliday(dt);
            if (strHoliday != "")
            {
                str += " " + strHoliday;
            }
            string strChinaHoliday = GetChinaHoliday(dt);
            if (strChinaHoliday != "")
            {
                str += " " + strChinaHoliday;
            }

            return str;
        }

        /// <summary> 获取农历年份 </summary>
        public static string GetYear(DateTime dt)
        {
            int yearIndex = china.GetSexagenaryYear(dt);
            string yearTG = " 甲乙丙丁戊己庚辛壬癸";
            string yearDZ = " 子丑寅卯辰巳午未申酉戌亥";
            string yearSX = " 鼠牛虎兔龙蛇马羊猴鸡狗猪";
            int year = china.GetYear(dt);
            int yTG = china.GetCelestialStem(yearIndex);
            int yDZ = china.GetTerrestrialBranch(yearIndex);

            string str = string.Format("[{1}]{2}{3}{0}", year, yearSX[yDZ], yearTG[yTG], yearDZ[yDZ]);
            return str;
        }

        /// <summary> 获取农历月份 </summary>
        public static string GetMonth(DateTime dt)
        {
            int year = china.GetYear(dt);
            int iMonth = china.GetMonth(dt);
            int leapMonth = china.GetLeapMonth(year);
            bool isLeapMonth = iMonth == leapMonth;
            if (leapMonth != 0 && iMonth >= leapMonth)
            {
                iMonth--;
            }

            string szText = "正二三四五六七八九十";
            string strMonth = isLeapMonth ? "闰" : "";
            if (iMonth <= 10)
            {
                strMonth += szText.Substring(iMonth - 1, 1);
            }
            else if (iMonth == 11)
            {
                strMonth += "十一";
            }
            else
            {
                strMonth += "腊";
            }
            return strMonth + "月";
        }

        /// <summary> 获取农历日期 </summary>
        public static string GetDay(DateTime dt)
        {
            int iDay = china.GetDayOfMonth(dt);
            string szText1 = "初十廿三";
            string szText2 = "一二三四五六七八九十";
            string strDay;
            if (iDay == 20)
            {
                strDay = "二十";
            }
            else if (iDay == 30)
            {
                strDay = "三十";
            }
            else
            {
                strDay = szText1.Substring((iDay - 1) / 10, 1);
                strDay = strDay + szText2.Substring((iDay - 1) % 10, 1);
            }
            return strDay;
        }

        /// <summary> 获取节气 </summary>
        public static string GetSolarTerm(DateTime dt)
        {
            DateTime dtBase = new DateTime(1900, 1, 6, 2, 5, 0);
            DateTime dtNew;
            double num;
            int y;
            string strReturn = "";

            y = dt.Year;
            for (int i = 1; i <= 24; i++)
            {
                num = 525948.76 * (y - 1900) + JQData[i - 1];
                dtNew = dtBase.AddMinutes(num);
                if (dtNew.DayOfYear == dt.DayOfYear)
                {
                    strReturn = JQ[i - 1];
                }
            }

            return strReturn;
        }

        /// <summary> 获取公历节日 </summary>
        public static string GetHoliday(DateTime dt)
        {
            string strReturn = "";
            object g = gHoliday[dt.Month.ToString("00") + dt.Day.ToString("00")];
            if (g != null)
            {
                strReturn = g.ToString();
            }

            return strReturn;
        }

        /// <summary> 获取农历节日 </summary>
        public static string GetChinaHoliday(DateTime dt)
        {
            string strReturn = "";
            int year = china.GetYear(dt);
            int iMonth = china.GetMonth(dt);
            int leapMonth = china.GetLeapMonth(year);
            int iDay = china.GetDayOfMonth(dt);
            if (china.GetDayOfYear(dt) == china.GetDaysInYear(year))
            {
                strReturn = "除夕";
            }
            else if (leapMonth != iMonth)
            {
                if (leapMonth != 0 && iMonth >= leapMonth)
                {
                    iMonth--;
                }
                object n = nHoliday[iMonth.ToString("00") + iDay.ToString("00")];
                if (n != null)
                {
                    if (strReturn == "")
                    {
                        strReturn = n.ToString();
                    }
                    else
                    {
                        strReturn += " " + n.ToString();
                    }
                }
            }

            return strReturn;
        }
    }
}
