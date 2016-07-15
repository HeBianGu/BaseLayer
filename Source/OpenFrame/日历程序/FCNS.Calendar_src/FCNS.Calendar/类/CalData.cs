using System;
using System.IO;
using System.Collections;
using System.Reflection;

namespace FCNS.Calendar
{
    public class TaskAndJournal
    {
        string fileName = string.Empty;
        public System.DateTime start = DateTime.Now;
        public System.DateTime end = DateTime.Now;
        bool alert = false;
        bool special = false;
        bool done = false;
        string title = string.Empty;
        string summary = string.Empty;

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }
        /// <summary>
        /// 获取或设置是否开启任务警报
        /// </summary>
        public bool Alert
        {
            get
            {
                return alert;
            }
            set
            {
                alert = value;
            }
        }
        /// <summary>
        /// 获取或设置是否为特殊任务
        /// </summary>
        public bool Special
        {
            get
            {
                return special;
            }
            set
            {
                special = value;
            }
        }
        /// <summary>
        /// 任务是否已完成
        /// </summary>
        public bool Done
        {
            get
            {
                return done;
            }
            set
            {
                done = value;
            }
        }
        /// <summary>
        /// 获取或设置任务标题
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        /// <summary>
        /// 获取或设置任务开始的时间
        /// </summary>
        public string Start
        {
            get
            {
                return start.ToString();
            }
            set
            {
                start = DateTime.ParseExact(value, "yyyyMMddHHmmss", null);
            }
        }
        /// <summary>
        /// 获取或设置任务内容
        /// </summary>
        public String Summary
        {
            get
            {
                return summary;
            }
            set
            {
                summary = value;
            }
        }
        /// <summary>
        /// 获取或设置任务结束的时间
        /// </summary>
        public String End
        {
            get
            {
                if (end.Year == 1)
                {
                    return "";
                }
                return end.ToString();
            }
            set
            {
                //				end = System.DateTime.ParseExact(value.Remove(8,1), "yyyyMMddHHmmss", null);
                end = DateTime.ParseExact(value, "yyyyMMddHHmmss", null);
            }
        }

    }

    public enum FrequenceOfTask
    {
        Daily, Weekly, Monthly, Yearly
    }
    public class Rule
    {
        public FrequenceOfTask Type;
        public int Interval;
        public System.DateTime Until;

        public override string ToString()
        {
            return String.Format("Type: {0} Interval: {1} Until: {2}", Type, Interval, Until);
        }

        public void Parse(string value)
        {
            foreach (string aux in value.Split(';'))
            {
                switch (aux.Substring(0, aux.IndexOf('=')).ToLower())
                {
                    case "freq":
                        switch (aux.Substring(aux.IndexOf('=') + 1).ToLower())
                        {
                            case "daily":
                                Type = FrequenceOfTask.Daily;
                                break;
                            case "weekly":
                                Type = FrequenceOfTask.Weekly;
                                break;
                            case "monthly":
                                Type = FrequenceOfTask.Monthly;
                                break;
                            case "yearly":
                                Type = FrequenceOfTask.Yearly;
                                break;
                            default:
                                Console.WriteLine("Not implemented in Rule.Parse: {0}", aux);
                                break;
                        }
                        break;
                    case "interval":
                        Interval = int.Parse(aux.Substring(aux.IndexOf('=') + 1).ToLower());
                        break;
                    case "until":
                        Until = DateTime.ParseExact(aux.Substring(aux.IndexOf('=') + 1), "yyyyMMddHHmmss", null);
                        break;
                    default:
                        Console.WriteLine("Not implemented in Rule.Parse: {0}", aux);
                        break;
                }
            }
        }
    }
    /// <summary>
    /// 实例化一个任务
    /// </summary>
    public class CalTask
    {
        public System.DateTime start = DateTime.Now;
        public System.DateTime end = DateTime.Now;
        bool alert = false;
        bool special = false;
        bool done = false;
        string title = string.Empty;
        string summary = string.Empty;
        bool journal = false;
        Rule rrule = null;

        /// <summary>
        /// 检测当前任务字段是否属于某时间段
        /// </summary>
        /// <param name="date">要检测的时间段</param>
        /// <returns>true 为存在</returns>
        public bool IsInDate(System.DateTime date)
        {
            if (start.ToString("yyyyMMdd") == date.ToString("yyyyMMdd"))
            {
                return true;
            }
            if (rrule == null)
            {
                return false;
            }
            if ((start < date) && (date < rrule.Until))
            {
                switch (rrule.Type)
                {
                    case FrequenceOfTask.Daily:
                        return true;
                    case FrequenceOfTask.Weekly:
                        if (start.DayOfWeek == date.DayOfWeek)
                            return true;
                        break;
                    case FrequenceOfTask.Monthly:
                        if (start.Day == date.Day)
                            return true;
                        break;
                    case FrequenceOfTask.Yearly:
                        if ((start.Month == date.Month) && (start.Day == date.Day))
                            return true;
                        break;
                }
            }
            return false;
        }
        /// <summary>
        /// 获取或设置是否开启任务警报
        /// </summary>
        public bool Alert
        {
            get
            {
                return alert;
            }
            set
            {
                alert = value;
            }
        }
        /// <summary>
        /// 获取或设置是否为特殊任务
        /// </summary>
        public bool Special
        {
            get
            {
                return special;
            }
            set
            {
                special = value;
            }
        }
        /// <summary>
        /// 任务是否已完成
        /// </summary>
        public bool Done
        {
            get
            {
                return done;
            }
            set
            {
                done = value;
            }
        }
        /// <summary>
        /// 获取或设置任务标题
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        /// <summary>
        /// 获取或设置任务内容
        /// </summary>
        public String Summary
        {
            get
            {
                return summary;
            }
            set
            {
                summary = value;
            }
        }
        /// <summary>
        /// 获取或设置当前实例是否日志{路径为程序目录的journal目录,文件名为'yyyyMMddHHmm'+'标题'}
        /// </summary>
        public bool Journal
        {
            get
            {
                return journal;
            }
            set
            {
                journal = value;
            }
        }
        public Rule RRule
        {
            get
            {
                return rrule;
            }
            set
            {
                rrule = value;
            }
        }
        /// <summary>
        /// 获取或设置任务开始的时间
        /// </summary>
        public string Start
        {
            get
            {
                return start.ToString();
            }
            set
            {
                start = DateTime.ParseExact(value, "yyyyMMddHHmmss", null);
            }
        }
        /// <summary>
        /// 获取或设置任务结束的时间
        /// </summary>
        public String End
        {
            get
            {
                if (end.Year == 1)
                {
                    return "";
                }
                return end.ToString();
            }
            set
            {
                //				end = System.DateTime.ParseExact(value.Remove(8,1), "yyyyMMddHHmmss", null);
                end = DateTime.ParseExact(value, "yyyyMMddHHmmss", null);
            }
        }
    }
    /// <summary>
    /// 实例化一个任务类别
    /// </summary>
    public class CalData
    {
        string calendarName;
        /// <summary>
        /// 日历任务栏目包含的所有'CalTask'实例
        /// </summary>
        public ArrayList DataList;
        bool visible = true;
        bool selected = false;
        static int i = 0;
        System.Drawing.Color colorBorder, colorText, colorBgUnselected, colorBgSelectedLeft, colorBgSelectedRight;
   
        public CalData()
        {
            DataList = new ArrayList();

            switch (i)
            {
                case 0: // Azul
                    colorBorder = System.Drawing.Color.FromArgb(255, 0, 81, 212);
                    colorText = System.Drawing.Color.FromArgb(255, 255, 255, 255);
                    colorBgUnselected = System.Drawing.Color.FromArgb(200, 141, 181, 242);
                    colorBgSelectedLeft = System.Drawing.Color.FromArgb(255, 28, 104, 224);
                    colorBgSelectedRight = System.Drawing.Color.FromArgb(255, 90, 149, 245);
                    break;
                case 1: // Naranja
                    colorBorder = System.Drawing.Color.FromArgb(255, 245, 118, 0);
                    colorText = System.Drawing.Color.FromArgb(255, 255, 255, 255);
                    colorBgUnselected = System.Drawing.Color.FromArgb(200, 245, 211, 180);
                    colorBgSelectedLeft = System.Drawing.Color.FromArgb(255, 247, 129, 17);
                    colorBgSelectedRight = System.Drawing.Color.FromArgb(255, 255, 164, 79);
                    break;
                case 2: // Rosa
                    colorBorder = System.Drawing.Color.FromArgb(255, 176, 39, 174);
                    colorText = System.Drawing.Color.FromArgb(255, 255, 255, 255);
                    colorBgUnselected = System.Drawing.Color.FromArgb(200, 232, 171, 232);
                    colorBgSelectedLeft = System.Drawing.Color.FromArgb(255, 186, 67, 184);
                    colorBgSelectedRight = System.Drawing.Color.FromArgb(255, 196, 90, 196);
                    break;
                case 3: // Rojo
                    colorBorder = System.Drawing.Color.FromArgb(255, 230, 23, 23);
                    colorText = System.Drawing.Color.FromArgb(255, 255, 255, 255);
                    colorBgUnselected = System.Drawing.Color.FromArgb(200, 251, 171, 171);
                    colorBgSelectedLeft = System.Drawing.Color.FromArgb(255, 244, 44, 44);
                    colorBgSelectedRight = System.Drawing.Color.FromArgb(255, 250, 97, 97);
                    break;
                case 5: // Violeta
                    colorBorder = System.Drawing.Color.FromArgb(255, 73, 43, 161);
                    colorText = System.Drawing.Color.FromArgb(255, 255, 255, 255);
                    colorBgUnselected = System.Drawing.Color.FromArgb(200, 195, 183, 233);
                    colorBgSelectedLeft = System.Drawing.Color.FromArgb(255, 92, 64, 176);
                    colorBgSelectedRight = System.Drawing.Color.FromArgb(255, 171, 149, 237);
                    break;
                case 4: // Verde
                default:
                    colorBorder = System.Drawing.Color.FromArgb(255, 44, 161, 11);
                    colorText = System.Drawing.Color.FromArgb(255, 255, 255, 255);
                    colorBgUnselected = System.Drawing.Color.FromArgb(200, 158, 241, 136);
                    colorBgSelectedLeft = System.Drawing.Color.FromArgb(255, 68, 173, 38);
                    colorBgSelectedRight = System.Drawing.Color.FromArgb(255, 113, 194, 91);
                    break;
            }

            i = ((i + 1) % 6);
        }
        /// <summary>
        /// 获取日历任务栏目的填充颜色
        /// </summary>
        public System.Drawing.Color ColorBorder
        {
            get
            {
                return colorBorder;
            }
        }
        /// <summary>
        /// 获取日历任务栏目的文本颜色
        /// </summary>
        public System.Drawing.Color ColorText
        {
            get
            {
                return colorText;
            }
        }
        /// <summary>
        /// 获取日历任务栏目未选择状态的背景颜色
        /// </summary>
        public System.Drawing.Color ColorBgUnselected
        {
            get
            {
                return colorBgUnselected;
            }
        }
        /// <summary>
        /// 获取日历任务栏目选择状态的左背景颜色
        /// </summary>
        public System.Drawing.Color ColorBgSelectedLeft
        {
            get
            {
                return colorBgSelectedLeft;
            }
        }
        /// <summary>
        /// 获取日历任务栏目选择状态的右背景颜色
        /// </summary>
        public System.Drawing.Color ColorBgSelectedRight
        {
            get
            {
                return colorBgSelectedRight;
            }
        }
        /// <summary>
        /// 获取或设置日历任务栏目的内容是否可见
        /// </summary>
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }
        /// <summary>
        /// 获取或设置日历任务栏目是否状态已选择
        /// </summary>
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
            }
        }
        /// <summary>
        /// 保存日历任务栏目所属 ics 文件内容
        /// </summary>
        /// <param name="file">ics 文件路径</param>
        public void SaveICS(string file)
        {
            using (TextWriter streamWriter = new StreamWriter(file))
            {
                streamWriter.WriteLine("CALNAME:{0}", calendarName);
                string freq = "WEEKLY";
                foreach (CalTask ct in DataList)
                {
                    streamWriter.WriteLine("BEGIN:EVENT");
                    streamWriter.WriteLine(String.Format("DTSTART;TZID:{0}", ct.start.ToString("yyyyMMddHHmmss")));
                    streamWriter.WriteLine(String.Format("DTEND;TZID:{0}", ct.end.ToString("yyyyMMddHHmmss")));
                    streamWriter.WriteLine(String.Format("SPECIAL:{0}", ct.Special));
                    streamWriter.WriteLine(String.Format("ALERT:{0}", ct.Alert));
                    streamWriter.WriteLine(String.Format("TITLE:{0}", ct.Title));
                    streamWriter.WriteLine(String.Format("DONE:{0}", ct.Done));
                    streamWriter.WriteLine(String.Format("SUMMARY:{0}", ct.Summary));
                    streamWriter.WriteLine(String.Format("JOURNAL:{0}", ct.Journal));
                    if (ct.RRule != null)
                    {
                        switch (ct.RRule.Type)
                        {
                            case FrequenceOfTask.Daily:
                                freq = "DAILY";
                                break;
                            case FrequenceOfTask.Weekly:
                                freq = "WEEKLY";
                                break;
                            case FrequenceOfTask.Monthly:
                                freq = "MONTHLY";
                                break;
                            case FrequenceOfTask.Yearly:
                                freq = "YEARLY";
                                break;
                        }
                        streamWriter.WriteLine(String.Format("RRULE:FREQ={0};INTERVAL=1;UNTIL={1}T{2}Z", freq, ct.RRule.Until.ToUniversalTime().ToString("yyyyMMdd"), ct.RRule.Until.ToUniversalTime().ToString("HHmmss")));
                    }
                    streamWriter.WriteLine("END:EVENT");
                }
            }
        }
        /// <summary>
        /// 读取日历任务栏目所属 ics 文件内容
        /// </summary>
        /// <param name="file">ics 文件路径</param>
        public void LoadICS(string file)
        {
            StreamReader sr = new StreamReader(file);
            string curLine;
            char character;
            CalTask ct = new CalTask();
            bool dentro = false;
            System.IO.FileInfo a = new System.IO.FileInfo(file);
            calendarName = a.Name;

            while (true)
            {
                curLine = sr.ReadLine();
                if (curLine == null)
                {
                    break;
                }
                character = ':';
                if (curLine.IndexOf(';') > 0)
                {
                    if (curLine.IndexOf(':') > curLine.IndexOf(';'))
                    {
                        character = ';';
                    }
                }
                if (curLine.IndexOf(character) <= 0)
                {
                    continue;
                }
                switch (curLine.Substring(0, curLine.IndexOf(character)).ToLower())
                {
                    case "calname":
                        calendarName = curLine.Substring(curLine.IndexOf(':') + 1);
                        break;
                    case "begin":
                        ct = new CalTask();
                        dentro = true;
                        break;
                    case "dtstart":
                        ct.Start = curLine.Substring(curLine.IndexOf(':') + 1);
                        break;
                    case "dtend":
                        ct.End = curLine.Substring(curLine.IndexOf(':') + 1);
                        break;
                    case "special":
                        ct.Special = bool.Parse(curLine.Substring(curLine.IndexOf(':') + 1));
                        break;
                    case "alert":
                        ct.Alert = bool.Parse(curLine.Substring(curLine.IndexOf(':') + 1));
                        break;
                    case "done":
                        ct.Done = bool.Parse(curLine.Substring(curLine.IndexOf(':') + 1));
                        break;
                    case "title":
                        ct.Title = curLine.Substring(curLine.IndexOf(':') + 1);
                        break;
                    case "summary":
                        ct.Summary = curLine.Substring(curLine.IndexOf(':') + 1);
                        break;
                    case "journal":
                        ct.Journal = bool.Parse(curLine.Substring(curLine.IndexOf(':') + 1));
                        break;
                    case "rrule":
                        ct.RRule = new Rule();
                        ct.RRule.Parse(curLine.Substring(curLine.IndexOf(':') + 1));
                        break;
                    case "end":
                        if (dentro)
                        {
                            DataList.Add(ct);
                            dentro = false;
                            ct = new CalTask();
                        }
                        break;
                }
            }
            sr.Close();
        }
        /// <summary>
        /// 获取或设置日历任务栏目的名称
        /// </summary>
        public string CalendarName
        {
            get
            {
                return calendarName;
            }
            set
            {
                calendarName = value;
            }
        }
    }
}
