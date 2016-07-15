/*1.任务统计 显示一个星期的内容
 * 2.日期时钟
 * 3.显示今日所有任务(可以隐藏过期任务)
 * 4.显示所有未完成的任务
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FCNS.Calendar
{
    public partial class MiniForm : Form
    {
        public MiniForm()
        {
            InitializeComponent();
        }

        public event EventHandler HideMiniForm;
        System.Timers.Timer dateTimer;
        string weekString;//标题显示星期

        private int WM_SYSCOMMAND = 0x112;
        private long SC_MAXIMIZE = 0xF030;
        private long SC_MINIMIZE = 0xF020;
        private long SC_CLOSE = 0xF060;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt64() == SC_MAXIMIZE)
                {
                    HideMiniForm(this, EventArgs.Empty);
                    this.Hide();
                    return;
                }
                if (m.WParam.ToInt64() == SC_MINIMIZE)
                {
                    this.Hide();
                    return;
                }

                if (m.WParam.ToInt64() == SC_CLOSE)
                {
                    HideMiniForm(this, EventArgs.Empty);
                    this.Hide();
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void MiniForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);

            dateTimer = new System.Timers.Timer(60000);
            dateTimer.Elapsed += new System.Timers.ElapsedEventHandler(dateTimer_Elapsed);
            dateTimer.Start();
            switch (DateTime.Today.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    weekString = "星期五";
                    break;
                case DayOfWeek.Monday:
                    weekString = "星期一";
                    break;
                case DayOfWeek.Saturday:
                    weekString = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    weekString = "星期日";
                    break;
                case DayOfWeek.Thursday:
                    weekString = "星期四";
                    break;
                case DayOfWeek.Tuesday:
                    weekString = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    weekString = "星期三";
                    break;
            }
            this.Text = weekString + DateTime.Now.ToString(" MM-dd HH:mm");
            weekString = string.Empty;

            checkBoxPastTask.Checked = (bool)Main.appConfig.Get("ShowPastTask", (bool)false);
            checkBoxUndoneTask.Checked = (bool)Main.appConfig.Get("ShowUndoneTask", (bool)false);
        }

        void dateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Text = weekString + DateTime.Now.ToString(" MM-dd HH:mm");
        }

        void RefreshData()
        {
            try
            {
                listViewTask.Items.Clear();
                foreach (CalTask t in tasks)
                {
                    if (checkBoxPastTask.Checked && t.start.CompareTo(DateTime.Now) == -1)
                    {
                        continue;
                    }

                    ListViewItem i = new ListViewItem(t.start.ToString("MM-dd HH:mm"));
                    i.Tag = t;
                    if (t.Title == "")
                    {
                        if (15 < t.Summary.Length)
                        {
                            i.SubItems.Add(t.Summary.Substring(0, 15));
                        }
                        else
                        {
                            i.SubItems.Add(t.Summary);
                        }
                    }
                    else
                    {
                        i.SubItems.Add(t.Title);
                    }

                    if (t.Alert)
                    {
                        i.SubItems.Add("是");
                    }
                    else
                    {
                        i.SubItems.Add("否");
                    }

                    if (t.Done)
                    {
                        i.SubItems.Add("是");
                    }
                    else
                    {
                        i.SubItems.Add("否");
                    }
                    listViewTask.Items.Add(i);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.StackTrace.ToString());
            }
        }

        List<CalTask> tasks = new List<CalTask>();
        /// <summary>
        /// 设置迷你窗口显示的数据
        /// </summary>
        public List<CalTask> SetMiniFormData
        {
            set
            {
                tasks = value;
                RefreshData();
            }
        }

        #region 事件
        private void checkBoxPastTask_CheckedChanged(object sender, EventArgs e)
        {
            Main.appConfig.Set("ShowPastTask", (bool)checkBoxPastTask.Checked, false, true);
            RefreshData();
        }

        private void checkBoxUndoneTask_CheckedChanged(object sender, EventArgs e)
        {
            Main.appConfig.Set("ShowUndoneTask", (bool)checkBoxUndoneTask.Checked, false, true);
            RequestMiniFormData(checkBoxUndoneTask.Checked);
        }

        private void listViewTask_DoubleClick(object sender, EventArgs e)
        {
            if (listViewTask.SelectedItems != null)
            {
                AlertMessage a = new AlertMessage(((CalTask)listViewTask.SelectedItems[0].Tag).Title, ((CalTask)listViewTask.SelectedItems[0].Tag).Start + " 到 " + ((CalTask)listViewTask.SelectedItems[0].Tag).End, ((CalTask)listViewTask.SelectedItems[0].Tag).Summary, false);
                a.ShowDialog();
            }
        }

        public delegate void rData(bool undoneTasks);
        public event rData RequestMiniFormData;
        #endregion

        private void 查看详情ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewTask.SelectedItems != null)
            {
                listViewTask_DoubleClick(sender, e);
            }
        }

        private void 标记为已完成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewTask.SelectedItems != null)
            {
                ((CalTask)listViewTask.SelectedItems[0].Tag).Done = !((CalTask)listViewTask.SelectedItems[0].Tag).Done;
                listViewTask.Items.Remove(listViewTask.SelectedItems[0]);
            }
        }

        private void 切换报警状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewTask.SelectedItems != null)
            {
                bool b = ((CalTask)listViewTask.SelectedItems[0].Tag).Alert;
                ((CalTask)listViewTask.SelectedItems[0].Tag).Alert = !b;
                if (!b)
                {
                    listViewTask.SelectedItems[0].SubItems[2].Text = "是";
                }
                else
                {
                    listViewTask.SelectedItems[0].SubItems[2].Text = "否";
                }
            }
        }
    }
}
