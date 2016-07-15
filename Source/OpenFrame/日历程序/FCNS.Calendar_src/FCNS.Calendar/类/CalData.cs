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
        /// ��ȡ�������Ƿ������񾯱�
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
        /// ��ȡ�������Ƿ�Ϊ��������
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
        /// �����Ƿ������
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
        /// ��ȡ�������������
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
        /// ��ȡ����������ʼ��ʱ��
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
        /// ��ȡ��������������
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
        /// ��ȡ���������������ʱ��
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
    /// ʵ����һ������
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
        /// ��⵱ǰ�����ֶ��Ƿ�����ĳʱ���
        /// </summary>
        /// <param name="date">Ҫ����ʱ���</param>
        /// <returns>true Ϊ����</returns>
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
        /// ��ȡ�������Ƿ������񾯱�
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
        /// ��ȡ�������Ƿ�Ϊ��������
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
        /// �����Ƿ������
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
        /// ��ȡ�������������
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
        /// ��ȡ��������������
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
        /// ��ȡ�����õ�ǰʵ���Ƿ���־{·��Ϊ����Ŀ¼��journalĿ¼,�ļ���Ϊ'yyyyMMddHHmm'+'����'}
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
        /// ��ȡ����������ʼ��ʱ��
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
        /// ��ȡ���������������ʱ��
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
    /// ʵ����һ���������
    /// </summary>
    public class CalData
    {
        string calendarName;
        /// <summary>
        /// ����������Ŀ����������'CalTask'ʵ��
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
        /// ��ȡ����������Ŀ�������ɫ
        /// </summary>
        public System.Drawing.Color ColorBorder
        {
            get
            {
                return colorBorder;
            }
        }
        /// <summary>
        /// ��ȡ����������Ŀ���ı���ɫ
        /// </summary>
        public System.Drawing.Color ColorText
        {
            get
            {
                return colorText;
            }
        }
        /// <summary>
        /// ��ȡ����������Ŀδѡ��״̬�ı�����ɫ
        /// </summary>
        public System.Drawing.Color ColorBgUnselected
        {
            get
            {
                return colorBgUnselected;
            }
        }
        /// <summary>
        /// ��ȡ����������Ŀѡ��״̬���󱳾���ɫ
        /// </summary>
        public System.Drawing.Color ColorBgSelectedLeft
        {
            get
            {
                return colorBgSelectedLeft;
            }
        }
        /// <summary>
        /// ��ȡ����������Ŀѡ��״̬���ұ�����ɫ
        /// </summary>
        public System.Drawing.Color ColorBgSelectedRight
        {
            get
            {
                return colorBgSelectedRight;
            }
        }
        /// <summary>
        /// ��ȡ����������������Ŀ�������Ƿ�ɼ�
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
        /// ��ȡ����������������Ŀ�Ƿ�״̬��ѡ��
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
        /// ��������������Ŀ���� ics �ļ�����
        /// </summary>
        /// <param name="file">ics �ļ�·��</param>
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
        /// ��ȡ����������Ŀ���� ics �ļ�����
        /// </summary>
        /// <param name="file">ics �ļ�·��</param>
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
        /// ��ȡ����������������Ŀ������
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
