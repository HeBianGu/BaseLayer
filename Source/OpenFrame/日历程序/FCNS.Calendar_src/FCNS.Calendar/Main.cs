using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using FCNS.Controls;
using System.Threading;

namespace FCNS.Calendar
{
    public partial class Main : Form
    {
        public static Script.SoftConfig appConfig;
        //public static SoftConfig appConfig;
        /// <summary>
        /// 下一个要打开的窗体;true为Main,false为MiniForm
        /// </summary>
        public static bool lastShown = true;
        public static string appPath = AppDomain.CurrentDomain.BaseDirectory;
        int countDay, countWeek, countMonth, temp;
        MiniForm miniForm;
        System.Windows.Forms.Timer taskAlert;
         List<CalTask> tasksOfTemp;
        public delegate void TwoThread();
        public static List<CalData> tasksAndJournals;//全局调用,设置控件
        FCNS.Controls.ListView listView;
        CalendarTask taskDetail;
        FCNS.Controls.Tree.TreeViewAdv treeView;
        RichTextBoxPlus journalDetail;
        #region 主窗体
        public Main()
        {
            InitializeComponent();

            appConfig = new Script.SoftConfig(appPath + "config.xml");
            //appConfig = new SoftConfig(Path + "config.xml");
            //加载数据
            tasksAndJournals = new List<CalData>();
            tasksOfTemp = new List<CalTask>();
            countDay = countWeek = countMonth = temp = 0;
            while (DateTime.Today.AddDays(temp).DayOfWeek != DayOfWeek.Sunday)
            {
                temp += 1;
            }
            bool b = (bool)appConfig.Get("SetPre-TaskDone", (bool)false);

            foreach (string files in System.IO.Directory.GetFiles(appPath, "*.ics"))
            {
                CalData cd = new CalData();
                cd.LoadICS(files);

                foreach (CalTask ct in cd.DataList)
                {
                    if (b && ct.start.CompareTo(DateTime.Today) == -1)
                    {
                        ct.Done = true;
                    }
                    CountEvent(ct, 1, true);
                }

                tasksAndJournals.Add(cd);
            }

            if ((int)appConfig.Get("EditMode", (int)0) == 0)
            {
                便签模式ToolStripMenuItem.Enabled = false;
                日志模式ToolStripMenuItem.Enabled = true;
                便签模式ToolStripMenuItem.Checked = true;
                日志模式ToolStripMenuItem.Checked = false;
                TaskControl();
            }
            else
            {
                便签模式ToolStripMenuItem.Enabled = true;
                日志模式ToolStripMenuItem.Enabled = false;
                便签模式ToolStripMenuItem.Checked = false;
                日志模式ToolStripMenuItem.Checked = true;
                JournalControl();
            }

            if ((bool)appConfig.Get("Alert", (bool)false))
            {
                tsslAlert.Text = "报警功能已开启... ";
                taskAlert.Start();
            }

            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            appConfig.OnChange += new Script.SoftConfig.KeyHandler(OnChangeConfig);

            switch ((MonthCalendarStyle)appConfig.Get("CalendarStyle", (int)MonthCalendarStyle.Week))
            {
                case MonthCalendarStyle.Day:
                    按日查看ToolStripMenuItem.PerformClick();
                    break;
                case MonthCalendarStyle.Month:
                    按月查看ToolStripMenuItem.PerformClick();
                    break;
                default:
                    按周查看ToolStripMenuItem.PerformClick();
                    break;
            }

            if ((bool)appConfig.Get("DefaultMiniForm", (bool)false))
            {
                this.WindowState = FormWindowState.Minimized;
            }

            miniForm = new MiniForm();
            miniForm.HideMiniForm += new EventHandler(miniForm_HideMiniForm);
            miniForm.RequestMiniFormData += new MiniForm.rData(miniForm_RequestMiniFormData);

            taskAlert = new System.Windows.Forms.Timer();
            taskAlert.Interval = Convert.ToInt32((decimal)appConfig.Get("AlertTime", (decimal)60) * 1000);
            taskAlert.Tick += new EventHandler(taskAlert_Tick);

            notifyIcon1.Visible = (bool)appConfig.Get("NotifyIcon", (bool)false);

            System.Globalization.DateTimeFormatInfo dtfi = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;
            monthCalendar.FirstDayOfWeek = (uint)appConfig.Get("FirstDayOfWeek", (uint)dtfi.FirstDayOfWeek);
            monthCalendar.DaysByWeek = (int)appConfig.Get("DaysByWeek", (int)7);
            monthCalendar.Height = (int)appConfig.Get("MonthCalendarHeight", (int)180);

            foreach (string s in Enum.GetNames(typeof(SpecialTasks)))
            {
                tsddbSpecial.DropDownItems.Add(s);
            }

            Script.Common c = new FCNS.Script.Common();
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                if ((bool)appConfig.Get("MinimumMiniForm", (bool)true))
                {
                    ShowMiniForm();
                }
                else if (notifyIcon1.Visible)
                {
                    WinApi.ShowWindow(this.Handle, WinApi.WindowShowStyle.Hide);
                }
            }
            else
            {
                miniForm.Hide();
                this.BringToFront();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            退出ToolStripMenuItem.PerformClick();
        }

        #endregion

        #region 其它
        //初始化便签控件
        void TaskControl()
        {
            新任务ToolStripMenuItem.Enabled = false;
            新月历ToolStripMenuItem.Enabled = false;

            listView = new FCNS.Controls.ListView();
            listView.Dock = DockStyle.Top;
           listView.Title = "日历任务类型栏目";
           listView.Height = 150;

            FCNS.Controls.ListViewItem item1;
            foreach (CalData cd in tasksAndJournals)
            {
                item1 = new FCNS.Controls.ListViewItem();
                item1.Checked = true;
                item1.Text = cd.CalendarName;
                item1.Color = cd.ColorBorder;
                item1.Tag = cd;//alert:改善,看能否取消
                listView.Items.Add(item1);
            }
            if (listView.Items.Count < 1)
            {
                CalData cd = new CalData();
                cd.CalendarName = "普通";
                item1 = new FCNS.Controls.ListViewItem();
                item1.Text = cd.CalendarName;
                item1.Color = cd.ColorBorder;
                item1.Checked = true;
                item1.Tag = cd;
                listView.Items.Add(item1);
                tasksAndJournals.Add(cd);
            }
            listView.Title = "日历任务类型栏目";

            taskDetail = new CalendarTask();

            taskDetail.Dock = DockStyle.Fill;
            taskDetail.ContextMenuStrip = MenuStripEvent;
            taskDetail.FirstDayOfWeek = (uint)appConfig.Get("FirstDayOfWeek", (uint)1);
            taskDetail.DaysByWeek = (int)appConfig.Get("DaysByWeek", (int)7);
            taskDetail.Date = monthCalendar.Date = DateTime.Now;
            taskDetail.TimeFormat = (int)appConfig.Get("TimeFormat", (int)1);
            taskDetail.Alpha = (int)appConfig.Get("AlphaEvent", (int)200);
            taskDetail.ShowTitle = (bool)appConfig.Get("ShowTitle", (bool)true);
            if (taskDetail.ShowTitle)
            {
                taskDetail.TitleLength = int.Parse(appConfig.Get("TitleLength", (decimal)15).ToString());
            }
            else
            {
                taskDetail.TitleLength = 0;
            }
            taskDetail.StartHour = (int)appConfig.Get("StartHour", (int)8);
            taskDetail.EndHour = (int)appConfig.Get("EndHour", (int)18);
            taskDetail.HoursViewed = (uint)appConfig.Get("HoursViewed", (uint)12);

            taskDetail.CalDataChanged += new FCNS.Calendar.CalendarTask.CalHandler(taskDetail_CalDataChanged);
            taskDetail.SearchKey += new FCNS.Calendar.CalendarTask.GetUrl(taskDetail_SearchKey);
            taskDetail.CalendarChanged += new System.EventHandler(taskDetail_CalendarChanged);
            taskDetail.StyleChanged += new System.EventHandler(taskDetail_StyleChanged);
            taskDetail.DateChanged += new System.EventHandler(taskDetail_DateChanged);
            taskDetail.KeyDown += new System.Windows.Forms.KeyEventHandler(taskDetail_KeyDown);           

          listView.OnSelectedChanged += new System.EventHandler(listView_OnSelectedChanged);
        listView.OnAfterLabelEdit += new FCNS.Controls.ItemHandler(listView_OnAfterLabelEdit);
          listView.OnCheckedChanged += new FCNS.Controls.ItemHandler(listView_OnCheckedChanged);

          splitContainer1.Panel1.Controls.Add(listView);
          splitContainer1.Panel2.Controls.Add(taskDetail);
            listView.UpdateList();
        }
        //初始化日志控件
        void JournalControl()
        {
            新任务ToolStripMenuItem.Enabled = true;
            新月历ToolStripMenuItem.Enabled = true;

            treeView = new FCNS.Controls.Tree.TreeViewAdv();
            treeView.Dock = DockStyle.Top;
            treeView.Height = 200;
            treeView.ContextMenuStrip = MenuStripJournal;

            if (!Directory.Exists(appPath + "journal"))
            {
                Directory.CreateDirectory(appPath + "journal");
            }
            foreach (string s in Directory.GetFiles(appPath + "journal"))
            {
                //treeView.Nodes.Add(Path.GetFileNameWithoutExtension(s));
            }

            journalDetail = new RichTextBoxPlus();
            journalDetail.ContextMenuStrip = MenuStripJournal;
            journalDetail.Dock = DockStyle.Fill;

            splitContainer1.Panel1.Controls.Add(treeView);
            splitContainer1.Panel2.Controls.Add(journalDetail);
        }

        /// <summary>
        /// 统计事件数量
        /// </summary>
        /// <param name="task"></param>
        /// <param name="oper">-1 不操作 0 更改时间 1 新增 2 删除</param>
        void CountEvent(CalTask task, int oper)
        {
            CountEvent(task, oper, false);
        }
        void CountEvent(CalTask task, int oper, bool noWallpaper)
        {
            int i = DateTime.Today.Day + temp - task.start.Day;

            switch (oper)
            {
                case 0:
                    if (task.start.ToString("yyyyMMdd") != DateTime.Today.ToString("yyyyMMdd"))
                    {
                        if (tasksOfTemp.Contains(task))
                        {
                            tasksOfTemp.Remove(task);
                            countDay -= 1;
                        }
                    }
                    else
                    {
                        if (!tasksOfTemp.Contains(task))
                        {
                            tasksOfTemp.Add(task);
                            countDay += 1;
                        }
                    }
                    break;
                case 1:
                    if (task.start.ToString("yyyyMMdd") == DateTime.Today.ToString("yyyyMMdd"))
                    {
                        countDay += 1;
                        countWeek += 1;
                        countMonth += 1;

                        tasksOfTemp.Add(task);
                    }
                    else if (i < 7 && i > 0)
                    {
                        countWeek += 1;
                        countMonth += 1;
                    }
                    else if (task.start.ToString("yyyyMM") == DateTime.Today.ToString("yyyyMM"))
                    {
                        countMonth += 1;
                    }
                    break;
                case 2:
                    if (task.start.ToString("yyyyMMdd") == DateTime.Today.ToString("yyyyMMdd"))
                    {
                        countDay -= 1;
                        countWeek -= 1;
                        countMonth -= 1;

                        tasksOfTemp.Remove(task);
                    }
                    else if (i < 7 && i > 0)
                    {
                        countWeek -= 1;
                        countMonth -= 1;
                    }
                    else if (task.start.ToString("yyyyMM") == DateTime.Today.ToString("yyyyMM"))
                    {
                        countMonth -= 1;
                    }
                    break;
            }
            tsslCountEvent.Text = "本日 " + countDay.ToString() + " 件;本周 " + countWeek.ToString() + " 件;本月 " + countMonth.ToString() + " 件";

            if (!noWallpaper)
            {
                if ((int)appConfig.Get("TaskWallpaper", (int)1) == 1)
                {
                    Windows.Desktop d = new FCNS.Windows.Desktop();
                    Bitmap b = new Bitmap(taskDetail.Width, taskDetail.Height);
                    taskDetail.DrawToBitmap(b, new Rectangle(0, 0, taskDetail.Width, taskDetail.Height));
                    b.Save(appPath + "Wallpaper.bmp");
                    d.DesktopImage(b, true);
                    b.Dispose();
                }
            }
        }

        /// <summary>
        /// 显示迷你窗口
        /// </summary>
        void ShowMiniForm()
        {
            if (notifyIcon1.Visible)
            {
                WinApi.ShowWindow(this.Handle, WinApi.WindowShowStyle.Hide);
            }
            miniForm_RequestMiniFormData((bool)Main.appConfig.Get("ShowUndoneTask", (bool)false));
            lastShown = false;
            miniForm.Show();
        }
        void miniForm_RequestMiniFormData(bool undoneTasks)
        {
            tasksOfTemp.Clear();
            foreach (CalData cd in tasksAndJournals)
            {
                if (undoneTasks)
                {
                    foreach (CalTask c in cd.DataList)
                    {
                        if (!c.Done)
                        {
                            tasksOfTemp.Add(c);
                        }
                    }
                }
                else
                {
                    foreach (CalTask c in cd.DataList)
                    {
                        if (c.start.Day == DateTime.Today.Day)
                        {
                            tasksOfTemp.Add(c);
                        }
                    }
                }
            }
            miniForm.SetMiniFormData = tasksOfTemp;
        }
        void miniForm_HideMiniForm(object sender, EventArgs e)
        {
            WinApi.ShowWindow(this.Handle, WinApi.WindowShowStyle.Show);
            WinApi.SetForegroundWindow(this.Handle);
            lastShown = true;
        }
        /// <summary>
        /// 当配置文件内容有更改时发生
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void OnChangeConfig(string key, object value)
        {
            switch (key)
            {
                case "CalendarStyle":
                    if (便签模式ToolStripMenuItem.Checked)
                    {
                        taskDetail.Style = (MonthCalendarStyle)value;
                    }
                    //else
                    //{
                    //    AddJournal();
                    //}

                    switch ((MonthCalendarStyle)value)
                    {
                        case MonthCalendarStyle.Week:
                            monthCalendar.Style = MonthCalendarStyle.Week;
                            break;
                        case MonthCalendarStyle.Month:
                            monthCalendar.Style = MonthCalendarStyle.Month;
                            break;
                        default:
                            monthCalendar.Style = MonthCalendarStyle.Day;
                            break;
                    }
                    break;
                case "EditMode":
                    if ((int)value == 0)
                    {
                        treeView.Dispose();
                        journalDetail.Dispose();
                        TaskControl();
                    }
                    else
                    {
                        listView.Dispose();
                        taskDetail.Dispose();
                        JournalControl();
                    }
                    break;
                case "TaskWallpaper":
                    try
                    {
                        if ((int)value == 1)
                        {
                            CountEvent(tasksOfTemp[0], -1, false);
                        }
                    }
                    catch
                    {
                    }
                    break;
                case "AlertTime":
                    taskAlert.Stop();
                    taskAlert.Interval = Convert.ToInt32((decimal)appConfig.Get("AlertTime", (decimal)60) * 1000);
                    taskAlert.Start();
                    break;
                case "ShowTitle":
                    taskDetail.ShowTitle = (bool)value;
                    break;
                case "InterceptTitle":
                    taskDetail.TitleLength = (bool)value ? int.Parse(appConfig.Get("TitleLength", (decimal)15).ToString()) : 0;
                    break;
                case "TitleLength":
                    taskDetail.TitleLength = int.Parse(appConfig.Get("TitleLength", (decimal)15).ToString());
                    break;
                case "Alert":
                    if ((bool)value)
                    {
                        taskAlert.Start();
                        tsslAlert.Text = "报警功能已开启... ";
                    }
                    else
                    {
                        taskAlert.Stop();
                        tsslAlert.Text = "报警功能已关闭... ";
                    }
                    break;
                case "NotifyIcon":
                    notifyIcon1.Visible = (bool)value;
                    break;
                case "FirstDayOfWeek":
                    taskDetail.FirstDayOfWeek = monthCalendar.FirstDayOfWeek = (uint)value;
                    break;
                case "DaysByWeek":
                    taskDetail.DaysByWeek = monthCalendar.DaysByWeek = (int)value;
                    break;
                case "StartHour":
                    taskDetail.StartHour = (int)value;
                    break;
                case "EndHour":
                    taskDetail.EndHour = (int)value;
                    break;
                case "TimeFormat":
                    taskDetail.TimeFormat = (int)value;
                    break;

                case "HoursViewed":
                    taskDetail.HoursViewed = (uint)value;
                    break;
                case "AlphaEvent":
                    taskDetail.Alpha = (int)value;
                    break;
                case "AutoRun":
                    if ((bool)value)
                    {
                        Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run", "FCNS.Calendar", appPath + "FCNS.Calendar.exe", Microsoft.Win32.RegistryValueKind.String);
                    }
                    else
                    {
                        Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run", "FCNS.Calendar", "", Microsoft.Win32.RegistryValueKind.String);
                    }
                    break;
                case "SetPre-TaskDone":
                    if ((bool)value)
                    {
                        foreach (FCNS.Controls.ListViewItem i in listView.Items)
                        {
                            foreach (CalTask c in ((CalData)i.Tag).DataList)
                            {
                                if (c.start.CompareTo(DateTime.Today) == -1)
                                {
                                    c.Done = true;
                                }
                            }
                        }
                    }
                    break;
            }
            if (便签模式ToolStripMenuItem.Checked)
            {
                taskDetail.UpdateCalendar(true);
            }
        }

        void taskAlert_Tick(object sender, EventArgs e)
        {
            bool b = false;
            foreach (CalTask task in tasksOfTemp)
            {
                if (task.Alert)
                {
                    b = true;
                    if (task.start.CompareTo(DateTime.Now) != 1)
                    {
                        task.Alert = false;
                        ((CalData)listView.Selected.Tag).SaveICS(System.IO.Path.Combine(appPath, String.Format("{0}.ics", ((CalData)listView.Selected.Tag).CalendarName)));//防止特殊任务(关机之类)导致文件不保存
                        if (task.end.CompareTo(DateTime.Now) == 1)//防止重启软件后以前时间过长的事件报警
                        {
                            if (task.Special)
                            {
                                switch ((SpecialTasks)Enum.Parse(typeof(SpecialTasks), task.Title))
                                {
                                    case SpecialTasks.注销:
                                        Windows.Power.ExitWindows(FCNS.Windows.RestartOptions.LogOff, true);
                                        break;
                                    case SpecialTasks.待机:
                                        Windows.Power.ExitWindows(FCNS.Windows.RestartOptions.Suspend, true);
                                        break;
                                    case SpecialTasks.重启:
                                        Windows.Power.ExitWindows(FCNS.Windows.RestartOptions.Reboot, true);
                                        break;
                                    case SpecialTasks.关机:
                                        Windows.Power.ExitWindows(FCNS.Windows.RestartOptions.ShutDown, true);
                                        break;
                                    case SpecialTasks.弹出消息窗口:
                                        MessageBox.Show(task.Summary);
                                        break;
                                    case SpecialTasks.运行指定程序:
                                        System.Diagnostics.Process.Start(task.Summary);
                                        break;
                                    case SpecialTasks.关闭指定程序:
                                        foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
                                        {
                                            if (p.ProcessName == System.IO.Path.GetFileNameWithoutExtension(task.Summary))
                                            {
                                                p.Kill();
                                            }
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                AlertMessage m = new AlertMessage(task.Title, task.Start + " 到 " + task.End, task.Summary, true);
                                m.Show();
                            }
                            task.Done = true;
                        }
                        if (!lastShown)
                        {
                            ShowMiniForm();
                        }
                        break;
                    }
                }
            }
            if (!b)//如果没有任务就停止
            {
                taskAlert.Stop();
            }
        }
        #endregion

        #region 控件
        private void listView_OnAfterLabelEdit(FCNS.Controls.ListViewItem item)
        {
            if (item != null)
            {
                    CalData cd = (CalData)item.Tag;
                    cd.CalendarName = item.Text;
            }
        }

        private void listView_OnCheckedChanged(FCNS.Controls.ListViewItem item)
        {
                    if (item != listView.Selected)
                    {
                        CalData cd = (CalData)item.Tag;
                        cd.Visible = item.Checked;
                        taskDetail.UpdateCalendar(false);
                    }
                    else
                    {
                        item.Checked = true;
                    }
            }

        private void listView_OnSelectedChanged(object sender, EventArgs e)
        {
                FCNS.Controls.ListViewItem item = listView.Selected;
                if (item != null)
                {
                    foreach (CalData aux in tasksAndJournals)
                    {
                        aux.Selected = false;
                    }
                    CalData cd = (CalData)item.Tag;
                    cd.Selected = true;
                    cd.Visible = true;
                    item.Checked = true;
                    taskDetail.UpdateCalendar(false);
                }
        }

        private void splitterLeft_SplitterMoved(object sender, SplitterEventArgs e)
        {
            appConfig.Set("MonthCalendarHeight", (int)monthCalendar.Height, true, true);
        }

        private void taskDetail_StyleChanged(object sender, EventArgs e)
        {
            按日查看ToolStripMenuItem.PerformClick();
        }

        private void taskDetail_CalendarChanged(object sender, EventArgs e)
        {
            foreach (CalData aux in tasksAndJournals)
            {
                if (aux.Selected)
                {
                    foreach (FCNS.Controls.ListViewItem item in listView.Items)
                    {
                        if (aux == item.Tag)
                        {
                            listView.Selected = item;
                        }
                    }
                }
            }
        }

        private void taskDetail_DateChanged(object sender, EventArgs e)
        {
            monthCalendar.Date = taskDetail.Date;
        }

        private void taskDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                删除任务ToolStripMenuItem.PerformClick();
            }
        }

        private void taskDetail_SearchKey(string keyString)
        {
            if (!string.IsNullOrEmpty(keyString))
            {
                bool b = true;
                try
                {
                    for (int i = 0; i < webBrowser1.Document.All.Count; i++)
                    {
                        if (webBrowser1.Document.All[i].TagName.ToLower().Equals("input"))
                        {
                            switch (webBrowser1.Document.All[i].Name)
                            {
                                case "q":
                                    webBrowser1.Document.All[i].InnerText = keyString;
                                    break;
                                case "sa":
                                    b = false;
                                    webBrowser1.Document.All[i].InvokeMember("Click");
                                    break;
                            }
                        }
                    }
                }
                catch
                {
                    webBrowser1.Navigate("http://www.free-city.cn/soft/ads.html");
                }
                if (b)
                {
                    webBrowser1.Navigate("http://www.free-city.cn/soft/ads.html");
                }
            }
        }

        private void taskDetail_CalDataChanged(CalTask task, int oper)
        {
            if (oper == 1)
            {
                CountEvent(task, oper, true);
            }
            else
            {
                CountEvent(task, oper, false);
            }
        }

        private void monthCalendar2_DateChanged(object sender, EventArgs e)
        {
            if (便签模式ToolStripMenuItem.Checked)
            {
                taskDetail.Date = monthCalendar.Date;
            }
            else
            {
                //AddJournal();
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                显示ToolStripMenuItem.PerformClick();
            }
            else
            {
                MenuStripNotify.Show();
            }
        }

        #endregion

        #region 菜单
        private void 新任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskEdit t = new TaskEdit();
            if (t.ShowDialog() == DialogResult.OK)
            {
                if (MessageBox.Show("是否保存任务?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ((CalData)listView.Selected.Tag).DataList.Add(t.Task);
                    CountEvent(t.Task, 1);
                    //if (t.TaskLoopCount.Count > 0)
                    //{
                    //    DateTime dt=t.Task.end.Subtract(t.Task.start).ToString();
                    //    foreach (DateTime d in t.TaskLoopCount)
                    //    {
                    //        t.Task.Start =d.ToString("yyyyMMddHHmmss");
                    //        t.Task.End = t.Task.end.Add(TimeSpan.Parse(dt));
                    //        ((CalData)listView.Selected.Tag).DataList.Add(t.Task);
                    //    }
                    //}
                    taskDetail.UpdateCalendar(true);
                }
            }
        }

        private void 新月历ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool finded = true;
            int cont = 1;
            string text = "";
            while (finded)
            {
                finded = false;
                if (cont == 1)
                {
                    //如果不设置标题,当存在两个空白标题时程序会挂掉
                    text = "新事件类型";
                }
                else
                {
                    text = String.Format("{0} {1}", "新事件类型", cont);
                }
                foreach (CalData aux2 in tasksAndJournals)
                {
                    if (text == aux2.CalendarName)
                    {
                        finded = true;
                        cont++;
                    }
                }
            }
            CalData cd= new CalData();
            cd.CalendarName = text;
            FCNS.Controls.ListViewItem item1 = new FCNS.Controls.ListViewItem();
            item1.Text = cd.CalendarName;
            item1.Checked = true;
            item1.Color = cd.ColorBorder;
            item1.Tag = (CalData)cd;
            listView.Items.Add(item1);
            listView.Selected = item1;
            tasksAndJournals.Add(cd);
            listView.EditLabel();
        }

        private void 导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "FCNS.Calendar (*.ics)|*.ics";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    CalData cd = new CalData();
                    cd.LoadICS(open.FileName);
                    FCNS.Controls.ListViewItem item1 = new FCNS.Controls.ListViewItem();
                    item1.Text = cd.CalendarName;
                    item1.Checked = true;
                    item1.Color = cd.ColorBorder;
                    item1.Tag = (CalData)cd;
                    listView.Items.Add(item1);
                    tasksAndJournals.Add(cd);
                    listView.Selected = item1;
                }
            }
            taskDetail.UpdateCalendar(false);
        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FCNS.Controls.ListViewItem item = listView.Selected;
            if (item != null)
            {
                CalData cd = (CalData)item.Tag;
                using (SaveFileDialog save = new SaveFileDialog())
                {
                    save.InitialDirectory = appPath + "bak";
                    save.Filter = "Calendarios (*.ics)|*.ics";
                    save.FileName = String.Format("{0}.ics", cd.CalendarName);
                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        cd.SaveICS(save.FileName);
                    }
                }
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            appConfig.Set("Maximized", (bool)(this.WindowState == System.Windows.Forms.FormWindowState.Maximized));
            appConfig.Set("Left", (int)this.Left);
            appConfig.Set("Top", (int)this.Top);
            appConfig.Set("Width", (int)this.ClientSize.Width);
            appConfig.Set("Height", (int)this.ClientSize.Height);

            退出ToolStripMenuItem1.PerformClick();
        }
      //
        private void 参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Preferences p = new Preferences();
            p.ShowDialog();
        }

        private void 配置特殊任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpecialTask s = new SpecialTask();
            s.NewSpecialTask += new SpecialTask.AddTask(s_NewSpecialTask);
            s.Show();
        }
        void s_NewSpecialTask(CalTask task)
        {
            ((CalData)listView.Selected.Tag).DataList.Add(task);
            CountEvent(task, 1);
            taskDetail.UpdateCalendar(false);
        }

        private void 便签模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        便签模式ToolStripMenuItem.Enabled = false;
        日志模式ToolStripMenuItem.Enabled = true;
        便签模式ToolStripMenuItem.Checked = true;
        日志模式ToolStripMenuItem.Checked = false;
        appConfig.Set("EditMode", (int)0,true,true);
        }

        private void 日志模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
         便签模式ToolStripMenuItem.Enabled = true;
         日志模式ToolStripMenuItem.Enabled = false;
         便签模式ToolStripMenuItem.Checked = false;
         日志模式ToolStripMenuItem.Checked = true;
         appConfig.Set("EditMode", (int)1, true, true);
        }
        //
        private void 今天ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            monthCalendar.Date = System.DateTime.Today;
            taskDetail.Date = monthCalendar.Date;
        }

        private void 按日查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbDay.Checked = true;
            tsbWeek.Checked = false;
            tsbMonth.Checked = false;
            appConfig.Set("CalendarStyle", (int)MonthCalendarStyle.Day, true, true);
        }

        private void 按周查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbDay.Checked = false;
            tsbWeek.Checked = true;
            tsbMonth.Checked = false;
            appConfig.Set("CalendarStyle", (int)MonthCalendarStyle.Week, true, true);
        }

        private void 按月查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbDay.Checked = false;
            tsbWeek.Checked = false;
            tsbMonth.Checked = true;
            appConfig.Set("CalendarStyle", (int)MonthCalendarStyle.Month,true,true);
        }

        private void 向后ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tsbDay.Checked)
            {
                monthCalendar.Date = monthCalendar.Date.AddDays(1);
                taskDetail.Date = monthCalendar.Date;
                return;
            }
            if (tsbWeek.Checked)
            {
                monthCalendar.Date = monthCalendar.Date.AddDays(7);
                taskDetail.Date = monthCalendar.Date;
                return;
            }
            if (tsbMonth.Checked)
            {
                monthCalendar.Date = monthCalendar.Date.AddMonths(1);
                taskDetail.Date = monthCalendar.Date;
                return;
            }
        }

        private void 向前ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tsbDay.Checked)
            {
                monthCalendar.Date = monthCalendar.Date.AddDays(-1);
                taskDetail.Date = monthCalendar.Date;
                return;
            }
            if (tsbWeek.Checked)
            {
                monthCalendar.Date = monthCalendar.Date.AddDays(-7);
                taskDetail.Date = monthCalendar.Date;
                return;
            }
            if (tsbMonth.Checked)
            {
                monthCalendar.Date = monthCalendar.Date.AddMonths(-1);
                taskDetail.Date = monthCalendar.Date;
                return;
            }
        }
        //
        private void 访问主页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.free-city.cn");
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.ShowDialog();
        }

        private void 检查更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (File.Exists(appPath + "FCNS.UpdateSoft.exe"))
            //{
            //    //xmlConfig.Save(appPath + "config.xml");
            //    //xmlConfig.SelectSingleNode("Config/升级/最后登陆").InnerText = DateTime.Now.DayOfYear.ToString();

            //    System.Diagnostics.Process.Start(appPath + "FCNS.UpdateSoft.exe", "FCNS.Calendar");
            //}
            //else
            //{
            //    MessageBox.Show("升级组件丢失,请重新安装软件.");
            System.Diagnostics.Process.Start("http://www.free-city.cn");
            //}
        }

        #endregion

        #region 工具栏
        private void tsbNewCalendar_Click(object sender, EventArgs e)
        {
            if (便签模式ToolStripMenuItem.Checked)
            {
                新月历ToolStripMenuItem.PerformClick();
            }
            else
            {
                新建文件夹ToolStripMenuItem.PerformClick();
            }
        }

        private void tsbListView_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            tsbListView.Checked = !splitContainer1.Panel1Collapsed;
        }

        private void tsbPrev_Click(object sender, EventArgs e)
        {
            向前ToolStripMenuItem.PerformClick();
        }

        private void tsbDay_Click(object sender, EventArgs e)
        {
            按日查看ToolStripMenuItem.PerformClick();
        }

        private void tsbWeek_Click(object sender, EventArgs e)
        {
            按周查看ToolStripMenuItem.PerformClick();
        }

        private void tsbMonth_Click(object sender, EventArgs e)
        {
            按月查看ToolStripMenuItem.PerformClick();
        }

        private void tsbNext_Click(object sender, EventArgs e)
        {
            向后ToolStripMenuItem.PerformClick();
        }

        private void tsbZoomLess_Click(object sender, EventArgs e)
        {
            taskDetail.HoursViewed--;
            appConfig.Set("HoursViewed", (uint)taskDetail.HoursViewed);
        }

        private void tsbZoomMore_Click(object sender, EventArgs e)
        {
            taskDetail.HoursViewed++;
            appConfig.Set("HoursViewed", (uint)taskDetail.HoursViewed);
        }

        private void tsddbSpecial_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            tasksOfTemp.Clear();
            tscbSearch.Items.Clear();
            foreach (CalData aux in tasksAndJournals)
            {
                foreach (CalTask task in aux.DataList)
                {
                    if (task.Special && task.Title == e.ClickedItem.Text)
                    {
                        tasksOfTemp.Add(task);
                        tscbSearch.Items.Add(task.Title);
                    }
                }
            }
        }

        private void tsbSearch_Click(object sender, EventArgs e)
        {
            if (textBoxSearch.Text.Trim() != "")
            {
                tasksOfTemp.Clear();
                tscbSearch.Items.Clear();
                foreach (CalData aux in tasksAndJournals)
                {
                    foreach (CalTask task in aux.DataList)
                    {
                        if (task.Title.Contains(textBoxSearch.Text.Trim()))
                        {
                            tasksOfTemp.Add(task);
                            tscbSearch.Items.Add(task.Title);
                        }
                    }
                }
            }
        }

        private void tscbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaskEdit t = new TaskEdit();
            t.Task = tasksOfTemp[tscbSearch.SelectedIndex];
            t.ShowDialog();
        }
        #endregion

        #region 右键菜单
        #region 日志
        private void 新建文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if(Directory.Exists(appPath+"journal/"+
        }

        private void 新建页ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 日历
        private void MenuStripEvent_Opening(object sender, CancelEventArgs e)
        {
            报警ToolStripMenuItem.Checked = false;
            查看ToolStripMenuItem.Enabled = 删除任务ToolStripMenuItem.Enabled = 移动任务ToolStripMenuItem.Enabled = 高级编辑ToolStripMenuItem.Enabled = 报警ToolStripMenuItem.Enabled = 已完成ToolStripMenuItem.Enabled = true;

            if (taskDetail.Selected != null)
            {
                移动任务ToolStripMenuItem.DropDownItems.Clear();
                foreach (FCNS.Controls.ListViewItem item in listView.Items)
                {
                    if (!((CalData)item.Tag).DataList.Contains(taskDetail.Selected.calTask))
                    {
                        ToolStripMenuItem i = new ToolStripMenuItem(item.Text);
                        i.Tag = item.Tag;
                        移动任务ToolStripMenuItem.DropDownItems.Add(i);
                    }
                }
                报警ToolStripMenuItem.Checked = taskDetail.Selected.calTask.Alert;
                已完成ToolStripMenuItem.Checked = taskDetail.Selected.calTask.Done;
            }
            else
            {
              查看ToolStripMenuItem.Enabled=  删除任务ToolStripMenuItem.Enabled = 移动任务ToolStripMenuItem.Enabled = 高级编辑ToolStripMenuItem.Enabled = 报警ToolStripMenuItem.Enabled = 已完成ToolStripMenuItem.Enabled = false;
            }
        }

        private void 清空并移除本栏目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要清空已选择的栏目所有内容吗?", "此操作不可逆", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (listView.Items.Count > 1)
                {
                        FCNS.Controls.ListViewItem item = listView.Selected;
                        if (listView.Selected != null)
                        {
                            int pos = listView.Items.IndexOf(item);
                            CalData cd = (CalData)item.Tag;
                            tasksAndJournals.Remove(cd);
                            listView.Items.Remove(item);
                            if (pos >= (listView.Items.Count - 1))
                            {
                                pos = listView.Items.Count - 1;
                            }
                            listView.Selected = (FCNS.Controls.ListViewItem)listView.Items[pos];
                            taskDetail.UpdateCalendar(true);

                            countDay = countWeek = countMonth = 0;
                            foreach (FCNS.Controls.ListViewItem it in listView.Items)
                            {
                                foreach (CalTask ct in ((CalData)it.Tag).DataList)
                                {
                                    CountEvent(ct, 1, true);
                                }
                            }
                            CountEvent(new CalTask(), -1, false);
                        }
                }
            }
        }

        private void 查看任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
                MessageBox.Show(taskDetail.Selected.calTask.Summary, taskDetail.Selected.calTask.Title);
        }

        private void 新增任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            新任务ToolStripMenuItem.PerformClick();
        }

        private void 删除任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
                foreach (CalData item in tasksAndJournals)
                {
                    if (item.DataList.Contains(taskDetail.Selected.calTask))
                    {
                        item.DataList.Remove(taskDetail.Selected.calTask);
                        CountEvent(taskDetail.Selected.calTask, 2, true);
                        taskDetail.UpdateCalendar(true);
                        CountEvent(new CalTask(), -1, false);
                        break;
                    }
                }
        }

        private void 移动任务ToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ((CalData)e.ClickedItem.Tag).DataList.Add(taskDetail.Selected.calTask);
            ((CalData)listView.Selected.Tag).DataList.Remove(taskDetail.Selected.calTask);
            taskDetail.UpdateCalendar(true);
        }

        private void 高级编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (taskDetail.Selected.calTask.Special)
                {
                }
                else
                {
                    TaskEdit t = new TaskEdit();
                    t.Task = taskDetail.Selected.calTask;
                    t.ShowDialog();
                    taskDetail.UpdateCalendar(false);
                    //{
                    //    if (MessageBox.Show("是否保存任务?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //    {
                    //        taskDetail.Selected.calTask.Alert = t.Task.Alert;
                    //        taskDetail.Selected.calTask.Start = t.Task.start.ToString("yyyyMMddHHmmss");
                    //        taskDetail.Selected.calTask.End = t.Task.end.ToString("yyyyMMddHHmmss");
                    //        taskDetail.Selected.calTask.Title = t.Task.Title;
                    //        taskDetail.Selected.calTask.Summary = t.Task.Summary;
                    //        //任务循环未做
                    //        taskDetail.UpdateCalendar(true);
                    //    }
                    //}
                }
        }

        private void 报警ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taskDetail.Selected.calTask.Alert = 报警ToolStripMenuItem.Checked;
        }

        private void 已完成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taskDetail.Selected.calTask.Done = 已完成ToolStripMenuItem.Checked;
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lastShown)
            {
                WinApi.ShowWindow(this.Handle, WinApi.WindowShowStyle.Show);
                WinApi.SetForegroundWindow(this.Handle);
            }
            else
            {
                ShowMiniForm();
            }
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //appConfig.OnChange += new Script.SoftConfig.KeyHandler(OnChangeConfig);
            //Config.OnChange += new SoftConfig.KeyHandler(OnChangeConfig);

            foreach (string files in System.IO.Directory.GetFiles(appPath, "*.ics"))
            {
                System.IO.File.Delete(files);
            }

            foreach (CalData aux2 in tasksAndJournals)
            {
                aux2.SaveICS(System.IO.Path.Combine(appPath, String.Format("{0}.ics", aux2.CalendarName)));
            }
            Application.Exit();
        }
#endregion

       

        
        #endregion

      

       

    }
}
