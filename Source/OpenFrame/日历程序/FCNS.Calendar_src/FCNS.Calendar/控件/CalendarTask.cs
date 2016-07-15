using System;
using System.Reflection;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using FCNS;

namespace FCNS.Calendar
{
    public partial class CalendarTask : Control
    {
        #region 变量
        int titleHeight = 40;//标题高度限制为40px
        int tasksHeight = 100;//任务内容的高度
        Bitmap offBmp = null;//用于绘制背景
        System.DateTime firstDate, lastDate;//上个星期,下个星期
        ToolTip tip=new ToolTip();
        int[] limitsh = new int[8];//存储日期列表的宽度
        ChineseCalendar cCalendar=new ChineseCalendar();
        Pen penTitle=new Pen(Color.Empty);
        Pen penTask=new Pen(Color.Empty);
        SolidBrush sbTitle=new SolidBrush(Color.Empty);
        SolidBrush sbTask=new SolidBrush(Color.Empty);
        Bitmap offBmpBuffer = null;
        int[] limitsV = new int[7];
        bool movingTask = false;
        string timeFormatText = "H:mm";
        string timeFormatTextLeft="H:mm";
        ArrayList dataView=new ArrayList();
        ArrayList  dataViewed=new ArrayList();
        Image shadow00, shadow02, shadow10, shadow12, shadow20, shadow21, shadow22;
        /// <summary>
        /// 已选中的任务编辑框
        /// </summary>
        public EventTask Selected = null;
        /// <summary>
        ///已选择的任务编辑框实例 
        /// </summary>
        OperationMouseMove lastMouse;
        #endregion

        #region 属性
        System.DateTime date = DateTime.Now;
        /// <summary>
        /// 获取或设置要显示的日期
        /// </summary>
        [Category("FCNS"), Description("当前显示的日期")]
        public System.DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                UpdateCalendar(false);
            }
        }

        bool showTitle = true;
        /// <summary>
        /// 获取或设置是否绘制标题
        /// </summary>
        [Category("FCNS"), Description("是否绘制标题")]
        public bool ShowTitle
        {
            get
            {
                return showTitle;
            }
            set
            {
                showTitle = value;
                UpdateCalendar(false);
            }
        }

        int titleLength = 15;
        /// <summary>
        /// 获取或设置要截断作为标题的字符长度
        /// </summary>
        [Category("FCNS"), Description("要截断作为标题的字符长度")]
        public int TitleLength
        {
            get
            {
                return titleLength;
            }
            set
            {
                titleLength = value;
            }
        }

        uint firstDayOfWeek;
        /// <summary>
        /// 获取或设置周的首天日期
        /// </summary>
        [Category("FCNS"), Description("周的首天日期")]
        public uint FirstDayOfWeek
        {
            get
            {
                return firstDayOfWeek;
            }
            set
            {
                firstDayOfWeek = value;
            }
        }

        int daysByWeek = 7;
        /// <summary>
        /// 获取或设置一周的天数为 5 还是 7
        /// </summary>
        [Category("FCNS"), Description("1星期为5天还是7天")]
        public int DaysByWeek
        {
            get
            {
                return daysByWeek;
            }
            set
            {
                int auxValue = value;
                if (auxValue != 5)
                {
                    auxValue = 7;
                }
                daysByWeek = auxValue;
            }
        }

        int startHour = 0;
        /// <summary>
        /// 显示任务的开始时间
        /// </summary>
        [Category("FCNS"), Description("显示任务的开始时间")]
        public int StartHour
        {
            get
            {
                return startHour;
            }
            set
            {
                startHour = value;
            }
        }

        int endHour = 24;
        /// <summary>
        /// 显示任务的结束时间
        /// </summary>
        [Category("FCNS"), Description("显示任务的结束时间")]
        public int EndHour
        {
            get
            {
                return endHour;
            }
            set
            {
                endHour = value;
            }
        }

        int timeFormat = 1;
        /// <summary>
        /// 获取或设置时间的显示格式
        /// </summary>
        [Category("FCNS"), Description("时间格式")]
        public int TimeFormat
        {
            get
            {
                return timeFormat;
            }
            set
            {
                timeFormat = value;
            }
        }

        uint hoursViewed = 12;
        /// <summary>
        /// 获取或设置要显示任务的时间为多少小时
        /// </summary>
        [Category("FCNS"), Description("要显示任务的时间为多少小时")]
        public uint HoursViewed
        {
            get
            {
                return hoursViewed;
            }
            set
            {
                if (value == hoursViewed)
                {
                    return;
                }
                uint auxValue;
                auxValue = value;
                if (auxValue > 24)
                {
                    auxValue = 24;
                }
                if (auxValue < 6)
                {
                    auxValue = 6;
                }
                hoursViewed = auxValue;
            }
        }

        MonthCalendarStyle style = MonthCalendarStyle.Week;
        /// <summary>
        /// 获取或设置月历显示的类型
        /// </summary>
        [Category("FCNS"), Description("月历显示的类型")]
        public MonthCalendarStyle Style
        {
            get
            {
                return style;
            }
            set
            {
                style = value;
                style = value;
            }
        }

        int alpha;
        /// <summary>
        ///  获取或设置月历任务面板的透明度
        /// </summary>
        [Category("FCNS"), Description("月历任务面板的透明度")]
        public int Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = value;
            }
        }
        #endregion

        #region 类
        /// <summary>
        /// 鼠标移动的操作类
        /// </summary>
        class OperationMouseMove
        {
            public System.DateTime start, end;
            public System.TimeSpan duration;
            public System.DateTime mouse, newMouse;
            /// <summary>
            /// 任务类型 0 没有;1 调整大小;2 移动;3 拖动
            /// </summary>
            public int type;
        }

        struct RectangleViewed
        {
            public int y, y2, day;
            public EventTask et;
        }
        /// <summary>
        /// 任务编辑框类
        /// </summary>
        public class EventTask
        {
            public System.DateTime Start;
            public System.DateTime End;
            public CalTask calTask;
            public CalData calData;

        }
        #endregion

        #region 其它函数
        /// <summary>
        /// 代码未添加
        /// </summary>
        /// <param name="e"></param>
        void MouseDownMonth(System.Windows.Forms.MouseEventArgs e)
        {
            // TODO: MouseDownMonth
        }
        /// <summary>
        /// 代码未添加
        /// </summary>
        /// <param name="e"></param>
        void DoubleClickMonth(System.Windows.Forms.MouseEventArgs e)
        {
            // TODO: DoubleClickMonth
        }
      
        /// <summary>
        /// 代码未添加
        /// </summary>
        /// <param name="e"></param>
        void MouseUpMonth(System.Windows.Forms.MouseEventArgs e)
        {
            // TODO: MouseUpMonth
        }

        void MouseMoveWeek(System.Windows.Forms.MouseEventArgs e)
        {
            int Y = e.Y;
            if (lastMouse != null)
            {
                if (e.Button != MouseButtons.Left)
                {
                    return;
                }
                if ((Selected == null) && (getPosDateTime(e.X, Y) != lastMouse.mouse))
                {
                    lastMouse.type = 3;
                }

                System.DateTime dt = getPosDateTime(e.X, Y);
                if (dt != System.DateTime.MinValue)
                {
                    if (lastMouse.newMouse != dt)
                    {
                        lastMouse.newMouse = dt;
                        // 鼠标移动任务编辑框
                        if (lastMouse.type == 2)
                        {
                            movingTask = true;
                            Selected.Start = lastMouse.start;
                            Selected.Start += lastMouse.newMouse - lastMouse.mouse;
                            Selected.End = Selected.Start + lastMouse.duration;
                            Repaint2();
                        }
                        // 鼠标改变任务编辑框大小
                        if (((lastMouse.type == 1) || (lastMouse.type == 4)) && (Selected != null))
                        {
                            movingTask = true;
                            Selected.Start = lastMouse.start;
                            Selected.End = lastMouse.end;
                            Selected.End += lastMouse.newMouse - lastMouse.mouse;
                            if (Selected.End < Selected.Start)
                            {
                                Selected.Start = Selected.End;
                                Selected.End = lastMouse.start;
                            }
                            Repaint2();
                        }
                        if (lastMouse.type == 3)
                        {
                            if (Selected == null)
                            {
                                this.Cursor = System.Windows.Forms.Cursors.HSplit;
                                lastMouse.type = 4;
                                InsertNewEvent();
                            }
                        }
                    }
                }
            }
            else
            {
                int i = dataViewed.Count;
                int cursor = 0;
                RectangleViewed rv;
                while (i != 0)
                {
                    rv = (RectangleViewed)dataViewed[--i];
                    if ((Y >= rv.y) && (Y <= rv.y2) && (e.X >= limitsh[rv.day]) && (e.X <= limitsh[rv.day + 1]))
                    {
                        cursor = 2;

                        if (Y <= (rv.y + 5))
                        {
                            cursor = 1;
                        }
                        if (Y >= (rv.y2 - 5))
                        {
                            cursor = 1;
                        }
                        break;
                    }
                }
                //tooltip
                if (e.Y > 15 && e.Y < titleHeight && e.X > 40)
                {
                    tip.SetToolTip(this, cCalendar.GetChineseDay(firstDate.AddDays((e.X - 40) / ((this.ClientRectangle.Width - 40) / daysByWeek)).ToString("MMdd")));
                    cursor = 3;
                }
                switch (cursor)
                {
                    case 0:
                        this.Cursor = Cursors.Default;
                        break;
                    case 1:
                        this.Cursor =Cursors.HSplit;
                        break;
                    case 2:
                        this.Cursor = Cursors.SizeAll;
                        break;
                    case 3:
                        this.Cursor = Cursors.Hand;
                        break;
                }
            }
        }

        void MouseUpWeek(System.Windows.Forms.MouseEventArgs e)
        {
            movingTask = false;
            offBmpBuffer = null;

            if (lastMouse != null)
            {
                if (lastMouse.type == 4)
                {
                    ChangeNameTask();
                }

                if (Selected != null)
                {
                    if (Selected.calTask.start != Selected.Start | Selected.calTask.end != Selected.End)
                    {
                        Selected.calTask.start = Selected.Start;
                        Selected.calTask.end = Selected.End;
                        CalDataChanged(Selected.calTask, 0);
                    }
                }
                lastMouse = null;
                UpdateCalendar(true);
            }
        }

        void DoubleClickWeek(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (Selected == null)
                {
                    lastMouse.type = 1;
                    if (lastMouse.mouse.AddMinutes(-15).Day == lastMouse.mouse.Day)
                    {
                        lastMouse.mouse = lastMouse.mouse.AddMinutes(-15);
                    }
                    InsertNewEvent();
                    ChangeNameTask();
                    UpdateCalendar(true);
                    CalDataChanged(Selected.calTask, 1);
                }
                else
                {
                    ChangeNameTask();
                }
            }
        }

        void MouseDownWeek(System.Windows.Forms.MouseEventArgs e)
        {
            int Y = e.Y;
            bool selected = false;

            // Resize
            lastMouse = new OperationMouseMove();
            lastMouse.type = 1;
            lastMouse.mouse = getPosDateTime(e.X, Y);
            if (lastMouse.mouse == System.DateTime.MinValue)
            {
                lastMouse = null;
                return;
            }
            else
            {
                System.DateTime aux = date;
                date = new System.DateTime(lastMouse.mouse.Year, lastMouse.mouse.Month, lastMouse.mouse.Day);
                if (aux != date)
                {
                    OnDateChanged();
                }
            }

            Repaint2();

            int i = dataViewed.Count;
            RectangleViewed rv;
            while (i != 0)
            {
                rv = (RectangleViewed)dataViewed[--i];
                if ((Y >= rv.y) && (Y <= rv.y2) && (e.X >= limitsh[rv.day]) && (e.X <= limitsh[rv.day + 1]))
                {
                    Selected = rv.et;
                    foreach (CalData aux in Main.tasksAndJournals)
                    {
                        aux.Selected = false;
                    }
                    rv.et.calData.Selected = true;
                    Repaint2();
                    selected = true;

                    lastMouse.duration = Selected.End - Selected.Start;
                    lastMouse.start = Selected.Start;
                    lastMouse.end = Selected.End;
                    lastMouse.type = 2;

                    if (Y <= (rv.y + 5))
                    {
                        lastMouse.start = Selected.End;
                        lastMouse.end = Selected.Start;
                        lastMouse.type = 1;
                    }
                    if (Y >= (rv.y2 - 5))
                    {
                        lastMouse.type = 1;
                    }
                    break;
                }
            }
            // 如果看不到月历,不要添加代码
            CalData cd1 = null;
            foreach (CalData cd2 in Main.tasksAndJournals)
            {
                if ((cd2.Selected) && (cd2.Visible))
                {
                    cd1 = cd2;
                }
            }
            
            if (cd1 == null)
            {
                return;
            }
            OnCalendarChanged();

            if (selected == false)
            {
                if (e.Clicks == 1)
                {
                    Selected = null;
                    lastMouse.type = 0;
                }
            }
            //google
            if (e.Y > 15 && e.Y < titleHeight && e.X > 40 && tip.Active)
            {
               SearchKey(cCalendar.GetChineseDay(firstDate.AddDays((e.X - 40) / ((this.ClientRectangle.Width - 40) / daysByWeek)).ToString("MMdd")));
            }
        }

        Image GetResourceImage(string name)
        {
            Assembly assembly = System.Reflection.Assembly.GetCallingAssembly();
            System.IO.Stream s = assembly.GetManifestResourceStream("FCNS.Calendar.resources." + name);
            return Image.FromStream(s);
        }
        /// <summary>
        /// 计算时间所在Y坐标,已包含标题的高度
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        int getPosHours(double hour, double minute)
        {
            double pos = hour + (minute / 60);
            return (int)((tasksHeight * pos) / 24)+titleHeight;
        }
        /// <summary>
        /// 转换颜色
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        Color Color2Alpha(Color color)
        {
            return Color.FromArgb(alpha, color.R, color.G, color.B);
        }
        void DrawFillVGradient(Graphics g_, Rectangle rect1, Color color1, Color color2)
        {
            System.Int32 r, g, b;
            for (int i = 0; i < rect1.Height; i++)
            {
                r = color2.R + ((((rect1.Height - 1) - i) * (color1.R - color2.R)) / (rect1.Height - 1));
                g = color2.G + ((((rect1.Height - 1) - i) * (color1.G - color2.G)) / (rect1.Height - 1));
                b = color2.B + ((((rect1.Height - 1) - i) * (color1.B - color2.B)) / (rect1.Height - 1));
                penTitle.Color = Color.FromArgb(255, r, g, b);
                g_.DrawLine(penTitle, rect1.Left, rect1.Top + i, rect1.Right - 1, rect1.Top + i);
            }
        }
        /// <summary>
        /// 计算字符的长度
        /// </summary>
        /// <param name="font1">字符使用的字体</param>
        /// <param name="text">字符内容</param>
        /// <param name="width">绘制字符的方框的宽度</param>
        /// <returns></returns>
        int FontWidth(Font font1, string text, int width)
        {
            int width2 = width + (font1.Height * 2);
            Bitmap auxBmp = new Bitmap(width2, font1.Height);
            Graphics g = Graphics.FromImage(auxBmp);
            g.Clear(Color.White);
            g.DrawString(text, font1, sbTitle, 0, 0);

            int pos = (width2) - 1;
            bool find = false;
            while ((pos > 0) && (!find))
            {
                for (int y = 1; y < font1.Height; y += 3)
                {
                    if (auxBmp.GetPixel(pos, y).R != 255)
                    {
                        find = true;
                        break;
                    }
                }
                pos -= 2;
            }

            g.Dispose();
            auxBmp.Dispose();
            return pos;
        }
        /// <summary>
        /// 获取DateTime.ToString()后的字符
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="type">0 为%d,其它默认</param>
        /// <returns></returns>
        string NameDayType(System.DateTime dt, int type)
        {
            switch (type)
            {
                default:
                    return dt.ToString("yyyy-MM-dd");
            }
        }
        /// <summary>
        /// 获取日期控件宽度
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="width"></param>
        /// <param name="font1"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        int NameDayWidth(System.DateTime dt, int width, Font font1, int max)
        {
            if (max <= 0)
            {
                return 0;
            }
            int op = max;
            string text = NameDayType(dt, max);
            while (FontWidth(font1, text, width) > width)
            {
                op--;
                text = NameDayType(dt, op);
                if (op <= 0) return 0;
            }

            return op;
        }
        #endregion

        public CalendarTask()
        {
            InitializeComponent();
            
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserMouse, true);
            this.SetStyle(ControlStyles.ResizeRedraw, false);
            this.SetStyle(ControlStyles.Opaque, true);

            System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();
            uint firstDayOfWeek = (uint)dtfi.FirstDayOfWeek;
            shadow00 = GetResourceImage("shadow-0-0.png");
            shadow02 = GetResourceImage("shadow-0-2.png");
            shadow10 = GetResourceImage("shadow-1-0.png");
            shadow12 = GetResourceImage("shadow-1-2.png");
            shadow20 = GetResourceImage("shadow-2-0.png");
            shadow21 = GetResourceImage("shadow-2-1.png");
            shadow22 = GetResourceImage("shadow-2-2.png");
        }

        #region 控件刷新
        /// <summary>
        /// 更新控件
        /// </summary>
        /// <param name="forceUpdate">true 为重新统计要显示的任务数量</param>
        public void UpdateCalendar(bool forceUpdate)
        {
            /*先统计要显示的日期数量
           * 绘制任务显示控件的背景和内容(按月和按周两种)
           * 绘制已选择的任务编辑框的阴影效果
           * 刷新控件确认绘制成功,否则再次调用绘制任务*/
            RecalculateDateLimits(forceUpdate);
            Repaint2();
        }
        /// <summary>
        /// 控件重绘
        /// </summary>
        void Repaint2()
        {
            if ((this.ClientRectangle.Width < 1) || (this.ClientRectangle.Height < 1))
            {
                return;
            }
            if ((offBmp == null) || (offBmp.Height != this.ClientRectangle.Height) || (offBmp.Width != this.ClientRectangle.Width))
            {
                if (offBmp != null)
                {
                    offBmp.Dispose();
                }
                offBmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            }
            Graphics g = GenerateBackground();

            if (style != MonthCalendarStyle.Month)
            {
                if (Selected != null)
                {
                    drawTask(g, Selected);
                }
            }

            g.Dispose();
            Paint2();
        }
        /// <summary>
        /// 清空控件内容用 _offBmp 填充
        /// </summary>
        void Paint2()
        {
            if ((offBmp == null) || (offBmp.Height != this.ClientRectangle.Height) || (offBmp.Width != this.ClientRectangle.Width))
            {
                Repaint2();
                return;
            }
            if ((this.ClientRectangle.Width < 1) || (this.ClientRectangle.Height < 1))
            {
                return;
            }
            if (!this.IsDisposed)//取消这里会导致控件无法刷新,不加验证在切换模式的时候会报错
            {
                Graphics drawingSurface = Graphics.FromHwnd(this.Handle);
                drawingSurface.DrawImage(offBmp, 0, 0);
                drawingSurface.Dispose();
            }
        }
        /// <summary>
        /// 用白色填充控件背景
        /// </summary>
        /// <returns></returns>
        Graphics GenerateBackground()
        {
            if ((movingTask) && (offBmpBuffer != null))
            {
                if (offBmp != null)
                {
                    offBmp.Dispose();
                }
                offBmp = (Bitmap)offBmpBuffer.Clone(new Rectangle(0, 0, offBmpBuffer.Width, offBmpBuffer.Height), offBmpBuffer.PixelFormat);
                return Graphics.FromImage(offBmp);
            }
            if ((offBmp == null) || (offBmp.Height != this.ClientRectangle.Height) || (offBmp.Width != this.ClientRectangle.Width))
            {
                if (offBmp != null) offBmp.Dispose();
                {
                    offBmp = new Bitmap(this.ClientRectangle.Width, tasksHeight);
                }
            }

            Graphics g = Graphics.FromImage(offBmp);
            g.Clear(Color.White);

            if (style == MonthCalendarStyle.Month)
            {
                PaintStyleMonth(g);
            }
            else
            {
                PaintStyleWeek(g);
            }
            if (movingTask)
            {
                offBmpBuffer = (Bitmap)offBmp.Clone(new Rectangle(0, 0, offBmp.Width, offBmp.Height), offBmp.PixelFormat);
            }
            return g;
        }
        /// <summary>
        /// 计算控件要显示的数据
        /// </summary>
        /// <param name="forceUpdate">true 为强制更新控件界面</param>
        void RecalculateDateLimits(bool forceUpdate)
        {
            System.DateTime first, last;
            switch (style)
            {
                case MonthCalendarStyle.Week:
                    first = date;
                    for (int i = 0; i < 7; i++)
                    {
                        first = date.AddDays(-i);
                        if (daysByWeek == 5)
                        {
                            if (first.DayOfWeek == DayOfWeek.Monday)
                            {
                                break;
                            }
                        }
                        else
                        {
                            if ((uint)first.DayOfWeek == firstDayOfWeek)
                            {
                                break;
                            }
                        }
                    }
                    last = first.AddDays(daysByWeek - 1);
                    break;
                case MonthCalendarStyle.Month://标题不需要此部分
                    first = date;
                    first = first.AddDays(-(date.Day - 1));
                    last = first;
                    last = last.AddDays(System.DateTime.DaysInMonth(last.Year, last.Month) - 1);
                    while ((uint)first.DayOfWeek != firstDayOfWeek)
                    {
                        first = first.AddDays(-1);
                    }
                    while ((uint)last.AddDays(1).DayOfWeek != firstDayOfWeek)
                    {
                        last = last.AddDays(1);
                    }
                    break;
                default:
                    first = last = date;
                    break;
            }
            last = last.AddDays(1).AddSeconds(-1);

            if (forceUpdate || ((last != lastDate) || (first != firstDate)))
            {
                firstDate = first;
                lastDate = last;
                RefreshData();
            }
        }
        /// <summary>
        /// 统计要绘制的数据
        /// </summary>
        void RefreshData()
        {
            //标题不需要此部分
            EventTask aux;
            EventTask Selected2 = Selected;
            dataView.Clear();
            foreach (CalData c in Main.tasksAndJournals)
            {
                if (!c.Visible)
                {
                    continue;
                }
                foreach (CalTask ct in c.DataList)
                {
                    if (ct.RRule == null)
                    {
                        if ((ct.end > firstDate) && (ct.start < lastDate))
                        {
                            aux = new EventTask();
                            aux.Start = ct.start;
                            aux.End = ct.end;
                            aux.calTask = ct;
                            aux.calData = c;
                            if (Selected != null)
                            {
                                if ((Selected.Start == aux.Start) && (Selected.End == aux.End) && (Selected.calTask == aux.calTask) && (Selected.calData == aux.calData))
                                {
                                    Selected = aux;
                                }
                            }
                            dataView.Add(aux);
                        }
                    }
                    else
                    {
                        System.DateTime end = new System.DateTime(firstDate.Year, firstDate.Month, firstDate.Day);
                        System.DateTime start;
                        System.TimeSpan duration = ct.end - ct.start;
                        end -= duration;
                        end = new System.DateTime(end.Year, end.Month, end.Day);
                        do
                        {
                            if (ct.IsInDate(new System.DateTime(end.Year, end.Month, end.Day, ct.start.Hour, ct.start.Minute, ct.start.Second)))
                            {
                                start = new System.DateTime(end.Year, end.Month, end.Day, ct.start.Hour, ct.start.Minute, ct.start.Second);
                                end = start + duration;
                                if ((end > firstDate) && (start < lastDate))
                                {
                                    aux = new EventTask();
                                    aux.Start = start;
                                    aux.End = end;
                                    aux.calTask = ct;
                                    aux.calData = c;
                                    if (Selected != null)
                                    {
                                        if ((Selected.Start == aux.Start) && (Selected.End == aux.End) && (Selected.calTask == aux.calTask) && (Selected.calData == aux.calData))
                                        {
                                            Selected = aux;
                                        }
                                    }
                                    dataView.Add(aux);
                                }
                            }
                            end = end.AddDays(1);
                        }
                        while (end < lastDate);
                    }
                }
            }

            if (Selected == Selected2)
            {
                Selected = null;
            }
            SortData();
        }
        /// <summary>
        /// 排列数据
        /// </summary>
        void SortData()
        {
            //标题不需要此部分
            object aux = null;
            for (int i = 0; i < dataView.Count; i++)
            {
                for (int j = 0; j < dataView.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (((EventTask)dataView[i]).Start < ((EventTask)dataView[j]).Start)
                    {
                        aux = dataView[i];
                        dataView[i] = dataView[j];
                        dataView[j] = aux;
                    }
                }
            }
        }
        #endregion

        #region 绘制函数
        /// <summary>
        /// 获取鼠标点击处的日期时间段
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        System.DateTime getPosDateTime(double x, double y)
        {
            System.DateTime aux = System.DateTime.MinValue;
            if (x < limitsh[0])
            {
                return aux;
            }
            if (x >= this.ClientRectangle.Width)
            {
                return aux;
            }
            if (y < 0)
            {
                return aux;
            }
            if (y >= this.Height)
            {
                return aux;
            }
            int minutes = (15 * (int)System.Math.Round(((((y-titleHeight) * (24 * 60)) / tasksHeight)) / 15));
            aux = new System.DateTime(firstDate.Year, firstDate.Month, firstDate.Day);
            aux = aux.AddMinutes(minutes);
            int i = 0;
            while (aux <= lastDate)
            {
                if ((x >= limitsh[i]) && (x <= limitsh[i + 1]))
                {
                    break;
                }
                    i++;
                aux = aux.AddDays(1);
            }
            return aux;
        }
        /// <summary>
        /// 新增日历任务
        /// </summary>
        void InsertNewEvent()
        {
            //搜索新增任务属于那个任务栏
            CalData cd1 = null;
            foreach (CalData cd2 in Main.tasksAndJournals)
            {
                if (cd2.Selected)
                {
                    cd1 = cd2;
                }
            }
            if (cd1 == null)
            {
                return;
            }
            Selected = new EventTask();
            Selected.Start = lastMouse.mouse;
            Selected.End = lastMouse.mouse;
            CalTask ct = new CalTask();
            ct.start = lastMouse.mouse;
            ct.end = lastMouse.mouse;
            ct.RRule = null;
            cd1.DataList.Add(ct);
            Selected.calTask = ct;
            Selected.calData = cd1;
            lastMouse.start = Selected.Start;
            lastMouse.end = Selected.End;
            Selected.Start = lastMouse.start;
            Selected.End = Selected.Start + lastMouse.duration;
            Repaint2();
        }
        /// <summary>
        /// 激活已选择的任务编辑框的编辑状态
        /// </summary>
        void ChangeNameTask()
        {
            lastMouse = null;
            textBox1.Text = Selected.calTask.Summary;
            textBox1.SelectAll();
            int day = 0;
            while (firstDate.AddDays(day).DayOfWeek != Selected.Start.DayOfWeek)
            {
                day++;
            }
            textBox1.Left = limitsh[day];
            textBox1.Width = limitsh[day + 1] - limitsh[day] + 1;
            textBox1.Top = getPosHours(Selected.Start.Hour, Selected.Start.Minute)+15;
            if (Selected.Start.DayOfWeek != Selected.End.DayOfWeek)
            {
                textBox1.Height = getPosHours(23, 59) - textBox1.Top ;
            }
            else
            {
                textBox1.Height = getPosHours(Selected.End.Hour, Selected.End.Minute) - textBox1.Top ;
            }

            if (textBox1.Height > 30)
            {
                textBox1.Height = textBox1.Height - 15;
            }
            else
            {
                //if (textBox1.Height < getPosHours(0, 30)-titleHeight)
                //{
                    textBox1.Height =15;
                    textBox1.Top -= 15;
                //}
            }
            textBox1.BackColor = Color.FromArgb(255, Selected.calData.ColorBgUnselected.R, Selected.calData.ColorBgUnselected.G, Selected.calData.ColorBgUnselected.B);
            textBox1.ForeColor = Selected.calData.ColorBorder;
            textBox1.Visible = true;
            textBox1.Focus();

            /*			Rectangle cloneRect = new Rectangle(textBox1.Left, textBox1.Top + scrollValue, textBox1.Width, textBox1.Height);
            Bitmap cloneBitmap = _offBmp.Clone(cloneRect, _offBmp.PixelFormat);
            textBox1.BackgroundImage = cloneBitmap;
            Graphics g = textBox1.CreateGraphics();
            g.DrawImage(cloneBitmap, 0, scrollValue);
        Graphics drawingSurface = Graphics.FromHwnd(Handle);
            drawingSurface.DrawImage(cloneBitmap, 0, 0);
            drawingSurface.Dispose();*/
        }
        /// <summary>
        /// 改变任务编辑框为不可见并更新任务内容
        /// </summary>
        void IsChangeText()
        {
            if (textBox1.Visible)
            {
                //if (Selected != null)
                //{
                Selected.calTask.Summary = textBox1.Text;
                //}
                textBox1.Visible = false;
                Repaint2();
            }
        }
        /// <summary>
        /// 绘制文本
        /// </summary>
        /// <param name="g">要绘制文本的 graphics 实例</param>
        /// <param name="font1">绘制文本使用的字体</param>
        /// <param name="brush">绘制文本使用的画笔</param>
        /// <param name="text">文本</param>
        /// <param name="x">要绘制文本的x坐标</param>
        /// <param name="y">要绘制文本的y坐标</param>
        /// <param name="width">要绘制文本的宽度</param>
        /// <param name="height">要绘制文本的高度</param>
        void PaintText(Graphics g, Font font1, SolidBrush brush, string text, int x, int y, int width, int height)
        {
            int height2 = font1.Height;
            while ((height2 + font1.Height) < height) height2 += font1.Height;
            RectangleF drawRect = new RectangleF(x, y, width, height2);
            g.DrawString(text, font1, brush, drawRect);
        }
        /// <summary>
        /// 绘制日期和其它文本内容
        /// </summary>
        /// <param name="g"></param>
        void PaintDays(Graphics g)
        {
            Font font1 = new Font("宋体", 9, FontStyle.Bold);
            StringFormat align1 = new StringFormat();
            align1.Alignment = StringAlignment.Center;
            sbTitle.Color = Color.FromArgb(255, 0, 0, 0);
            g.DrawString(date.Year.ToString(), font1, sbTitle, limitsh[0] / 2, (16 - font1.Height) / 2, align1);

            System.DateTime dt = firstDate;
            int i = 0;

            int max = 4;
            while (dt < lastDate)
            {
                max = NameDayWidth(dt, limitsh[i + 1] - (limitsh[i] + 10), font1, max);
                i++;
                dt = dt.AddDays(1);
            }

            SolidBrush sb2 = new SolidBrush(Color.Blue);
            dt = firstDate;
            i = 0;
            while (dt < lastDate)
            {
                //填充今日日期区域的颜色
                if (dt.ToString("yyyyMMdd") == System.DateTime.Now.ToString("yyyyMMdd"))
                {
                    Rectangle rect1 = new Rectangle(limitsh[i] + 1, 0, limitsh[i + 1] - limitsh[i] - 1, 8);
                    DrawFillVGradient(g, rect1, Color.FromArgb(255, 208, 226, 245), Color.FromArgb(255, 104, 169, 234));
                    rect1 = new Rectangle(limitsh[i] + 1, 7, limitsh[i + 1] - limitsh[i] - 1, 9);
                    DrawFillVGradient(g, rect1, Color.FromArgb(255, 104, 169, 234), Color.FromArgb(255, 187, 252, 255));
                    penTitle.Color = Color.FromArgb(255, 102, 147, 192);
                    g.DrawLine(penTitle, limitsh[i] + 1, 15, limitsh[i + 1], 15);
                    g.DrawLine(penTitle, limitsh[i + 1], 0, limitsh[i + 1], 15);

                    sbTitle.Color = Color.FromArgb(255, 236, 243, 252);
                    g.FillRectangle(sbTitle, limitsh[i] + 1, 16, limitsh[i + 1] - limitsh[i] - 1, titleHeight-21);
                    sbTitle.Color = Color.FromArgb(255, 0, 0, 0);
                }
                g.DrawString(NameDayType(dt, max), font1, sbTitle, (limitsh[i] + limitsh[i + 1]) / 2, (16 - font1.Height) / 2, align1);
                if (style == MonthCalendarStyle.Week && cCalendar.GetChineseDay(dt.ToString("MMdd")).Length > 11)
                {
                    g.DrawString(cCalendar.GetChineseDay(dt.ToString("MMdd")).Substring(0, 8) + "...", font1, sb2, (limitsh[i] + limitsh[i + 1]) / 2, 20, align1);
                }
                else
                {
                    g.DrawString(cCalendar.GetChineseDay(dt.ToString("MMdd")), font1, sb2,(limitsh[i] + limitsh[i + 1]) / 2, 20, align1);
                }
                i++;
                dt = dt.AddDays(1);
            }
            sb2.Dispose();
            align1.Dispose();
            font1.Dispose();
        }
        /// <summary>
        /// 绘制时间行线和时间文本
        /// </summary>
        /// <param name="g"></param>
        void PaintHours(Graphics g)
        {
            int x;
            Color colorLight = Color.FromArgb(255, 229, 229, 229);
            Color colorDark = Color.FromArgb(255, 204, 204, 204);
          
            int totalMediumHours = 24 * 2;
            for (int i = 1; i < totalMediumHours; i++)
            {
                if ((i % 2) == 0)
                {
                    penTask.Color = colorDark;
                }
                else
                {
                    penTask.Color = colorLight;
                }
                x = (tasksHeight * i) / totalMediumHours + titleHeight;
                g.DrawLine(penTask, 40, x, this.ClientRectangle.Width, x);
            }
            StringFormat align1 = new StringFormat();
            align1.Alignment = StringAlignment.Far;
            sbTask.Color = Color.FromArgb(255, 0, 0, 0);

            for (int i = 1; i <= 23; i++)
            {
                System.DateTime a = new System.DateTime(1, 1, 1, i, 0, 0);
                g.DrawString(a.ToString(timeFormatTextLeft), this.Font, sbTask, 38, ((tasksHeight * i) / 24) - this.Font.Height/2 + titleHeight, align1);
            }
            align1.Dispose();
        }
        /// <summary>
        /// 按周绘制控件边框和线条
        /// </summary>
        /// <param name="g"></param>
        void PaintStyleWeek(Graphics g)
        {
            //标题
            int x;
            int max = (style == MonthCalendarStyle.Day) ? 1 : daysByWeek;

            limitsh[0] = 40;
            limitsh[max] = this.ClientRectangle.Width - 1;
            for (int i = 1; i < max; i++)
            {
                limitsh[i] = (((limitsh[daysByWeek] - 40) * i) / daysByWeek) + 40;
            }
            //标题日期背景占15px
            for (int i = 1; i <= 6; i++)
            {
                x = 255 - (i * 3);
                penTitle.Color = Color.FromArgb(255, x, x, x);
                g.DrawLine(penTitle, 0, i, this.ClientRectangle.Width, i);
                g.DrawLine(penTitle, 0, 14 - i, this.ClientRectangle.Width, 14 - i);
            }
            penTitle.Color = Color.FromArgb(255, 234, 234, 234);
            g.DrawLine(penTitle, 0, 7, this.ClientRectangle.Width, 7);

            penTitle.Color = Color.FromArgb(255, 203, 203, 203);
            for (int i = 0; i <= max; i++)
            {
                g.DrawLine(penTitle, limitsh[i], 0, limitsh[i], this.ClientRectangle.Height);
            }

            penTitle.Color = Color.FromArgb(255, 203, 203, 203);
            g.DrawLine(penTitle, 0, 15, this.ClientRectangle.Width, 15);

            penTitle.Color = Color.FromArgb(255, 110, 110, 110);
            g.DrawLine(penTitle, 0, titleHeight - 1, this.ClientRectangle.Width, titleHeight - 1);
            g.DrawLine(penTitle, 0, titleHeight - 5, this.ClientRectangle.Width, titleHeight - 5);
            penTitle.Color = Color.FromArgb(255, 206, 206, 206);
            for (int i = 2; i <= 4; i++)
            {
                g.DrawLine(penTitle, 0, titleHeight - i, this.ClientRectangle.Width, titleHeight - i);
            }
            PaintDays(g);

            //内容
            PaintHours(g);
            sbTask.Color = Color.FromArgb(30, 0, 0, 0);
            g.FillRectangle(sbTask, 0, 0, this.ClientRectangle.Width, getPosHours(startHour, 0));
            g.FillRectangle(sbTask, 0, getPosHours(endHour, 0), this.ClientRectangle.Width, this.ClientRectangle.Height - getPosHours(endHour, 0));
            PaintTasks(g);
        }
        /// <summary>
        /// 绘制任务=>查找任务(按周查看)
        /// </summary>
        /// <param name="g"></param>
        void PaintTasks(Graphics g)
        {
            dataViewed.Clear();
            foreach (EventTask et in dataView)
            {
                if (Selected != et)
                {
                    drawTask(g, et);
                }
            }
        }
        /// <summary>
        /// 绘制任务=>绘制任务编辑框(按周查看)
        /// </summary>
        /// <param name="g"></param>
        /// <param name="et"></param>
        void drawTask(Graphics g, EventTask et)
        {
            System.DateTime end = et.End;
            if ((end.Hour == 0) && (end.Minute == 0))
            {
                if (et.Start != end)
                {
                    end = end.AddSeconds(-1);
                }
                else
                {
                    end = end.AddMinutes(30);
                }
            }

            if (et.Start.Day != end.Day)
            {
                int i = -1;
                System.DateTime aux;
                System.DateTime start;
                System.DateTime aux2;
                aux = new System.DateTime(end.Year, end.Month, end.Day, 0, 0, 0);
                drawPartTask(g, et, aux, end, false, true);
                start = end.AddDays(i);
                while (start.Day != et.Start.Day)
                {
                    aux = new System.DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
                    aux2 = new System.DateTime(start.Year, start.Month, start.Day, 23, 59, 59);
                    drawPartTask(g, et, aux, aux2, false, false);
                    i--;
                    start = end.AddDays(i);
                }
                aux = new System.DateTime(et.Start.Year, et.Start.Month, et.Start.Day, 23, 59, 59);
                drawPartTask(g, et, et.Start, aux, true, false);
            }
            else
            {
                drawPartTask(g, et, et.Start, end, true, true);
            }
        }
        /// <summary>
        /// 绘制任务编辑框
        /// </summary>
        /// <param name="g">要绘制任务编辑框的 graphics 实例</param>
        /// <param name="et">任务编辑框类的实例</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="drawBegin"></param>
        /// <param name="drawEnd"></param>
        void drawPartTask(Graphics g, EventTask et, System.DateTime start, System.DateTime end, bool drawBegin, bool drawEnd)
        {
            if ((end < firstDate) || (start > lastDate))
            {
                return;
            }
            int x = getPosHours(start.Hour, start.Minute);
            int x2 = getPosHours(end.Hour, end.Minute);
            if ((x == 0) && (x2 == 0))
            {
                return;
            }
            if ((x2 - x) < getPosHours(0, 30))
            {
                x2 = getPosHours(start.Hour, start.Minute + 30) ;
            }
            if (x2 < x)
            {
                x2 = x + getPosHours(0, 30) + 1;
            }
            if (!drawBegin)
            {
                x -= 15;
            }
            if (!drawEnd)
            {
                x2 += 7;
            }
            int day = 0;
            while (firstDate.AddDays(day).DayOfWeek != start.DayOfWeek)
            {
                day++;
            }

            RectangleViewed rect;
            rect.day = day;
            rect.y = x;
            rect.y2 = x2;
            rect.et = et;
            dataViewed.Add(rect);

            // 当任务编辑框移动的时候绘制背景阴影
            if (offBmpBuffer == null)
            {
                // 如果只是选中控件,就只绘制背景阴影
                if ((et.calData.Selected) && (et == Selected))
                {
                    Rectangle destRect1;
                    System.Drawing.Imaging.ImageAttributes ia = new System.Drawing.Imaging.ImageAttributes();
                    ia.SetWrapMode(WrapMode.Tile);
                    destRect1 = new Rectangle(limitsh[day] - 5, x + 5, shadow00.Width, shadow00.Height);
                    g.DrawImage(shadow00, destRect1, 0, 0, shadow00.Width, shadow00.Height, GraphicsUnit.Pixel, ia);
                    destRect1 = new Rectangle(limitsh[day + 1] + 1, x + 5, shadow02.Width, shadow02.Height);
                    g.DrawImage(shadow02, destRect1, 0, 0, shadow02.Width, shadow02.Height, GraphicsUnit.Pixel, ia);
                    destRect1 = new Rectangle(limitsh[day] - 5, x2 - 6, shadow20.Width, shadow20.Height);
                    g.DrawImage(shadow20, destRect1, 0, 0, shadow20.Width, shadow20.Height, GraphicsUnit.Pixel, ia);
                    destRect1 = new Rectangle(limitsh[day + 1] - 9, x2 - 6, shadow22.Width, shadow22.Height);
                    g.DrawImage(shadow22, destRect1, 0, 0, shadow22.Width, shadow22.Height, GraphicsUnit.Pixel, ia);
                    destRect1 = new Rectangle(limitsh[day] - 5, x + 5 + shadow00.Height, shadow10.Width, x2 - 6 - (x + 5 + shadow00.Height));
                    g.DrawImage(shadow10, destRect1, 0, 0, shadow10.Width, shadow10.Height, GraphicsUnit.Pixel, ia);
                    destRect1 = new Rectangle(limitsh[day + 1] + 1, x + 5 + shadow00.Height, shadow12.Width, x2 - 6 - (x + 5 + shadow00.Height));
                    g.DrawImage(shadow12, destRect1, 0, 0, shadow12.Width, shadow12.Height, GraphicsUnit.Pixel, ia);
                    destRect1 = new Rectangle(limitsh[day] - 5 + shadow20.Width, x2, limitsh[day + 1] - 9 - (limitsh[day] - 5 + shadow20.Width), shadow21.Height);
                    g.DrawImage(shadow21, destRect1, 0, 0, shadow21.Width, shadow21.Height, GraphicsUnit.Pixel, ia);
                    ia.Dispose();
                }
            }

            PaintTasksPref(g, et, limitsh[day] + 1, x + 1, limitsh[day + 1] - limitsh[day] - 1, x2 - 1);

            int alpha2 = 255;
            if (offBmpBuffer != null)
            {
                alpha2 = alpha;
            }
            else
            {
                alpha2 = 255;
            }

            // 相同颜色的选择与不选择部分
            Color color = et.calData.ColorBorder;
            color = Color.FromArgb(alpha2, color.R, color.G, color.B);
            penTask.Color = color;

            //边框
            g.DrawLine(penTask, limitsh[day], x + 6, limitsh[day], x2 - 7);
            g.DrawLine(penTask, limitsh[day + 1], x + 6, limitsh[day + 1], x2 - 7);
            g.DrawLine(penTask, limitsh[day] + 6, x, limitsh[day + 1] - 6, x);
            g.DrawLine(penTask, limitsh[day] + 6, x2 - 1, limitsh[day + 1] - 6, x2 - 1);
            //右下部分
            g.DrawLine(penTask, limitsh[day + 1] - 5, x2 - 2, limitsh[day + 1] - 4, x2 - 2);
            g.DrawLine(penTask, limitsh[day + 1] - 2, x2 - 4, limitsh[day + 1] - 3, x2 - 3);
            g.DrawLine(penTask, limitsh[day + 1] - 1, x2 - 6, limitsh[day + 1] - 1, x2 - 5);
            //左下部分
            g.DrawLine(penTask, limitsh[day] + 4, x2 - 2, limitsh[day] + 5, x2 - 2);
            g.DrawLine(penTask, limitsh[day] + 3, x2 - 3, limitsh[day] + 2, x2 - 4);
            g.DrawLine(penTask, limitsh[day] + 1, x2 - 6, limitsh[day] + 1, x2 - 5);
            //左上部分
            g.DrawLine(penTask, limitsh[day] + 4, x + 1, limitsh[day] + 5, x + 1);
            g.DrawLine(penTask, limitsh[day] + 3, x + 2, limitsh[day] + 2, x + 3);
            g.DrawLine(penTask, limitsh[day] + 1, x + 4, limitsh[day] + 1, x + 5);
            //Arriba Derecha
            g.DrawLine(penTask, limitsh[day + 1] - 5, x + 1, limitsh[day + 1] - 4, x + 1);
            g.DrawLine(penTask, limitsh[day + 1] - 3, x + 2, limitsh[day + 1] - 2, x + 3);
            g.DrawLine(penTask, limitsh[day + 1] - 1, x + 4, limitsh[day + 1] - 1, x + 5);

            sbTask.Color = color;
            StringFormat align1 = new StringFormat();
            Font font1 = new Font(this.Font.FontFamily, 9, FontStyle.Bold);
            //如果任务编辑框高度足够就绘制上面和下面的时间,否则只绘制下面的时间
            string s = et.calTask.Summary;
            if (showTitle)
            {
                if (et.calTask.Title == "" && titleLength != 0)
                {
                    if (et.calTask.Summary.Length > titleLength)
                    {
                        s = et.calTask.Summary.Substring(0, titleLength);
                    }
                }
                else
                {
                    s = et.calTask.Title;
                }
            }

            if ((x2 - x - 1) > 30)
            {
                if (et.calData.Selected)
                {
                    g.DrawLine(penTask, limitsh[day] + 6, x + 1, limitsh[day + 1] - 6, x + 1);
                    g.DrawLine(penTask, limitsh[day] + 4, x + 2, limitsh[day + 1] - 4, x + 2);
                    g.DrawLine(penTask, limitsh[day] + 3, x + 3, limitsh[day + 1] - 3, x + 3);
                    g.DrawLine(penTask, limitsh[day] + 2, x + 4, limitsh[day + 1] - 2, x + 4);
                    g.DrawLine(penTask, limitsh[day] + 2, x + 5, limitsh[day + 1] - 2, x + 5);
                    color = et.calData.ColorBorder;
                    color = Color.FromArgb(alpha2, color.R, color.G, color.B);
                    sbTask.Color = color;
                    g.FillRectangle(sbTask, limitsh[day] + 1, x + 6, limitsh[day + 1] - limitsh[day] - 1, 9);
                    sbTask.Color = et.calData.ColorText;
                }
                if (et.calTask.Done)
                {
                    g.DrawString("√ "+et.Start.ToString(timeFormatText), font1, sbTask, limitsh[day] + 5, x, align1);
                }
                else
                {
                    g.DrawString(et.Start.ToString(timeFormatText), font1, sbTask, limitsh[day] + 5, x, align1);
                }
                    if ((Selected == et) && (lastMouse != null) && drawEnd)
                {
                    align1.Alignment = StringAlignment.Far;
                        g.DrawString(et.End.ToString(timeFormatText), font1, sbTask, limitsh[day + 1] - 3, x2 - (font1.Height + 3), align1);
                    }
                if (style == MonthCalendarStyle.Day)
                {
                    PaintText(g, font1, sbTask,et.calTask.Title+" => "+et.calTask.Summary, limitsh[day] + 5, x + 17, limitsh[day + 1] - limitsh[day] - 8, x2 - x - 16);
                }
                else
                {
                    PaintText(g, font1, sbTask, s, limitsh[day] + 5, x + 17, limitsh[day + 1] - limitsh[day] - 8, x2 - x - 16);
                }
            }
            else
            {
                if (et.calData.Selected)
                {
                    sbTask.Color = et.calData.ColorText;
                }
                if (style == MonthCalendarStyle.Day)
                {
                    PaintText(g, font1, sbTask, et.calTask.Title + " => " + et.calTask.Summary, limitsh[day] + 5, x + 2, limitsh[day + 1] - limitsh[day] - 8, x2 - x - 1);
                }
                else
                {
                    PaintText(g, font1, sbTask, s, limitsh[day] + 5, x + 2, limitsh[day + 1] - limitsh[day] - 8, x2 - x - 1);
                }
                if ((Selected == et) && (lastMouse != null) && (lastMouse.type == 1) && drawEnd)
                {
                    align1.Alignment = StringAlignment.Far;
                    g.DrawString(et.End.ToString(timeFormatText), font1, sbTask, limitsh[day + 1] - 3, x2 - (font1.Height + 3), align1);
                }
            }
            align1.Dispose();
            font1.Dispose();
        }
        /// <summary>
        /// 绘制任务文本前先绘制任务框
        /// </summary>
        /// <param name="g"></param>
        /// <param name="et"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="ancho"></param>
        /// <param name="alto"></param>
        void PaintTasksPref(Graphics g, EventTask et, int x1, int y1, int ancho, int alto)
        {
            int red, green, blue, red2, green2, blue2;
            int i;

            red = et.calData.ColorBgSelectedLeft.R;
            green = et.calData.ColorBgSelectedLeft.G;
            blue = et.calData.ColorBgSelectedLeft.B;
            red2 = et.calData.ColorBgSelectedRight.R - red;
            green2 = et.calData.ColorBgSelectedRight.G - green;
            blue2 = et.calData.ColorBgSelectedRight.B - blue;

            int alpha2 = 255;
            if (offBmpBuffer != null)
            {
                alpha2 = alpha;
            }
            else
            {
                alpha2 = 255;
            }

            if (et.calData.Selected)
            {
                if ((alto - y1 + 1) > 30)
                {
                    i = 0;
                    y1 = y1 + 14;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1, x1 + i, alto - 6);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1, x1 + i, alto - 4);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1, x1 + i, alto - 3);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1, x1 + i, alto - 2);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1, x1 + i, alto - 2);
                    i = ancho - 5;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1, x1 + i, alto - 2);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1, x1 + i, alto - 2);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1, x1 + i, alto - 3);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1, x1 + i, alto - 4);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1, x1 + i, alto - 6);
                }
                else
                {
                    i = 0;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1 + 5, x1 + i, alto - 6);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1 + 3, x1 + i, alto - 4);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1 + 2, x1 + i, alto - 3);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1 + 1, x1 + i, alto - 2);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1 + 1, x1 + i, alto - 2);
                    i = ancho - 5;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1 + 1, x1 + i, alto - 2);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1 + 1, x1 + i, alto - 2);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1 + 2, x1 + i, alto - 3);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1 + 3, x1 + i, alto - 4);
                    i++;
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1 + 5, x1 + i, alto - 6);
                }
                for (i = 5; i < (ancho - 5); i++)
                {
                    penTask.Color = Color.FromArgb(alpha2, (red + ((i * red2) / ancho)), (green + ((i * green2) / ancho)), (blue + ((i * blue2) / ancho)));
                    g.DrawLine(penTask, x1 + i, y1, x1 + i, alto - 1);
                }
            }
            else
            {
                penTask.Color = Color2Alpha(et.calData.ColorBgUnselected);
                sbTask.Color = Color2Alpha(et.calData.ColorBgUnselected);
                i = 0;
                g.DrawLine(penTask, x1 + i, y1 + 5, x1 + i, alto - 6);
                i++;
                g.DrawLine(penTask, x1 + i, y1 + 3, x1 + i, alto - 4);
                i++;
                g.DrawLine(penTask, x1 + i, y1 + 2, x1 + i, alto - 3);
                i++;
                g.DrawLine(penTask, x1 + i, y1 + 1, x1 + i, alto - 2);
                i++;
                g.DrawLine(penTask, x1 + i, y1 + 1, x1 + i, alto - 2);
                i = ancho - 5;
                g.DrawLine(penTask, x1 + i, y1 + 1, x1 + i, alto - 2);
                i++;
                g.DrawLine(penTask, x1 + i, y1 + 1, x1 + i, alto - 2);
                i++;
                g.DrawLine(penTask, x1 + i, y1 + 2, x1 + i, alto - 3);
                i++;
                g.DrawLine(penTask, x1 + i, y1 + 3, x1 + i, alto - 4);
                i++;
                g.DrawLine(penTask, x1 + i, y1 + 5, x1 + i, alto - 6);
                g.FillRectangle(sbTask, x1 + 5, y1, ancho - 10, alto - y1);
            }
        }
    
        /// <summary>
        /// 按月绘制控件
        /// </summary>
        /// <param name="g"></param>
        void PaintStyleMonth(Graphics g)
        {
            Font font1 = new Font("宋体", 9, FontStyle.Bold);
            StringFormat align1 = new StringFormat();
            align1.Alignment = StringAlignment.Center;
            sbTitle.Color = Color.FromArgb(255, 0, 0, 0);
            g.DrawString(date.ToString("MMMM yyyy"), font1, sbTitle, this.ClientRectangle.Width / 2, (16 - font1.Height) / 2, align1);

            //内容
            int rows = ((lastDate - firstDate).Days / 7) + 1;
            penTask.Color = Color.FromArgb(255, 204, 204, 204);
            for (int i = 0; i <= 7; i++)
            {
                limitsh[i] = (this.ClientRectangle.Width * i) / 7;
            }
            for (int i = 0; i <= rows; i++)
            {
                limitsV[i] = titleHeight + (((tasksHeight - titleHeight) * i) / rows);
            }
            for (int i = 1; i <= 6; i++)
            {
                g.DrawLine(penTask, limitsh[i], limitsV[0], limitsh[i], tasksHeight);
            }
            for (int i = 0; i < rows + 1; i++)
            {
                g.DrawLine(penTask, 0, limitsV[i], this.ClientRectangle.Width, limitsV[i]);
            }


            int col, row;
            System.DateTime dt = firstDate;
            col = row = 0;
            int status = 0;
            Color colorLight = Color.FromArgb(255, 198, 198, 198);
            Color colorDark = Color.FromArgb(255, 64, 64, 64);
            Color colorBlack = Color.FromArgb(255, 0, 0, 0);
            Color color = colorLight;
            StringFormat alignRight = new StringFormat();
            alignRight.Alignment = StringAlignment.Far;
            while (dt < lastDate)
            {
                if (dt.ToString("yyyyMMdd") == date.ToString("yyyyMMdd"))
                {
                    sbTask.Color = Color.FromArgb(255, 247, 247, 247);
                    g.FillRectangle(sbTask, limitsh[col] + 1, limitsV[row] + 1, limitsh[col + 1] - limitsh[col] - 1, limitsV[row + 1] - limitsV[row] - 1);
                }
                if (dt.ToString("yyyyMMdd") == System.DateTime.Now.ToString("yyyyMMdd"))
                {
                    sbTask.Color = Color.FromArgb(255, 170, 230, 250);
                    g.FillRectangle(sbTask, limitsh[col] + 1, limitsV[row] + 1, limitsh[col + 1] - limitsh[col] - 1, limitsV[row + 1] - limitsV[row] - 1);
                }

                if (dt.Day == 1)
                {
                    if (status == 1)
                    {
                        color = colorLight;
                        status = 2;
                    }
                    if (status == 0)
                    {
                        color = colorDark;
                        status = 1;
                    }
                }
                sbTask.Color = color;
                g.DrawString(dt.Day.ToString(), this.Font, sbTask, limitsh[col + 1] - 4, limitsV[row] + 2, alignRight);

                col++;
                if (col == 7)
                {
                    col = 0;
                    row++;
                }
                dt = dt.AddDays(1);
            }

            sbTask.Color = colorBlack;
            dt = firstDate;
            alignRight.Alignment = StringAlignment.Center;

            for (int i = 0; i <= 6; i++)
            {
                g.DrawString(dt.ToString("dddd"), font1, sbTask, (limitsh[i] + limitsh[i + 1]) / 2, limitsV[0] - font1.Height, alignRight);
                dt = dt.AddDays(1);
            }

            align1.Dispose();
            alignRight.Dispose();
            font1.Dispose();

            PaintTasksMonth(g);
        }
        /// <summary>
        /// 绘制日历任务=>查找任务文本(按月查看)
        /// </summary>
        /// <param name="g"></param>
        void PaintTasksMonth(Graphics g)
        {
            dataViewed.Clear();

            int col = 0;
            int row = 0;
            int line = 0;
            System.DateTime dt = firstDate;
            while (dt < lastDate)
            {
                line = 0;
                foreach (EventTask et in dataView)
                {
                    if (dt.ToString("yyyyMMdd") == et.calTask.start.ToString("yyyyMMdd"))
                    {
                        drawTaskMonth(g, et, col, row, line);
                        line++;
                    }
                }
                col++;
                if (col == 7)
                {
                    col = 0;
                    row++;
                }
                dt = dt.AddDays(1);
            }
        }
        /// <summary>
        /// 绘制日历任务=>填充任务文本(按月查看)
        /// </summary>
        /// <param name="g"></param>
        /// <param name="et"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="line"></param>
        void drawTaskMonth(Graphics g, EventTask et, int col, int row, int line)
        {
            Font font1 = new Font(this.Font.FontFamily, 9, FontStyle.Bold);
            sbTask.Color = et.calData.ColorBorder;
            int y = limitsV[row] + 5 + (line * 13) + 11;
            if (y < limitsV[row + 1] - 11)
            {
                if (et.calTask.Title != "")
                {
                    PaintText(g, font1, sbTask, et.Start.ToString(timeFormatText) + " " + et.calTask.Title, limitsh[col] + 5, y, limitsh[col + 1] - limitsh[col] - 5, 11);
                }
                else
                {
                    string s = et.calTask.Summary;
                    if (et.calTask.Summary.Length > titleLength)
                    {
                        PaintText(g, font1, sbTask, et.Start.ToString(timeFormatText) + " " + et.calTask.Summary.Substring(0, titleLength), limitsh[col] + 5, y, limitsh[col + 1] - limitsh[col] - 5, 11);
                    }
                    else
                    {
                        PaintText(g, font1, sbTask, et.Start.ToString(timeFormatText) + " " + et.calTask.Summary, limitsh[col] + 5, y, limitsh[col + 1] - limitsh[col] - 5, 11);
                    }
                }
            }
            font1.Dispose();
        }
        #endregion
    
        #region 事件
        private void CalendarTask_Resize(object sender, EventArgs e)
        {
            tasksHeight = this.ClientRectangle.Height-titleHeight;
            Repaint2();
        }

        private void CalendarTask_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(offBmp, 0, 0);
        }

        private void CalendarTask_MouseMove(object sender, MouseEventArgs e)
        {
            if (style != MonthCalendarStyle.Month)
            {
                MouseMoveWeek(e);
            }
        }

        private void CalendarTask_MouseDown(object sender, MouseEventArgs e)
        {
            IsChangeText();
            if (style == MonthCalendarStyle.Month)
            {
                MouseDownMonth(e);
            }
            else
            {
                MouseDownWeek(e);
            }

            if (e.Clicks == 2)
            {
                if (style == MonthCalendarStyle.Month)
                {
                    DoubleClickMonth(e);
                }
                else
                {
                    DoubleClickWeek(e);
                }
            }
        }

        private void CalendarTask_MouseUp(object sender, MouseEventArgs e)
        {
            if (style == MonthCalendarStyle.Month)
            {
                MouseUpMonth(e);
            }
            else
            {
                MouseUpWeek(e);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    textBox1.Text = Selected.calTask.Summary;
                    IsChangeText();
                    e.Handled = true;
                    break;
                case Keys.Enter:
                    IsChangeText();
                    e.Handled = true;
                    break;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            IsChangeText();
            CalDataChanged(Selected.calTask, -1);
        }

        //自定义事件
        /// <summary>
        /// 当月历显示类型改变后引发任务
        /// </summary>
        /// 
        protected virtual void OnStyleChanged()
        {
            if (StyleChanged != null)
            {
                StyleChanged(this, System.EventArgs.Empty);
            }
        }
        [Category("FCNS"), Description("显示类型改变时引发")]
        new public event EventHandler StyleChanged;

        protected virtual void OnCalendarChanged()
        {
            if (CalendarChanged != null) CalendarChanged(this, System.EventArgs.Empty);
        }
        [Category("FCNS"), Description("任务栏目索引改变时引发")]
        public event EventHandler CalendarChanged;

        protected virtual void OnDateChanged()
        {
            if (DateChanged != null) DateChanged(this, System.EventArgs.Empty);
        }
        /// <summary>
        /// 当日期改变时引发
        /// </summary>
        [Category("FCNS"), Description("当前日期改变时引发")]
        public event EventHandler DateChanged;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oper">0 时间改变;1 新增;2 删除</param>
        public delegate void CalHandler(CalTask task, int oper);
        /// <summary>
        /// 当有任务框属性改变时引发(注意刷新会影响墙纸)
        /// </summary>
        [Category("FCNS"), Description("任务内容改变时引发")]
        public event CalHandler CalDataChanged;

        public delegate void GetUrl(string keyString);
        [Category("FCNS"), Description("Google 搜索标题字符")]
        public event GetUrl SearchKey;
        #endregion

    }
}
