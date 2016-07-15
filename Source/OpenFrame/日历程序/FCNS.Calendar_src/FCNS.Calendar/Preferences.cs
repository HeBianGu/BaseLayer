using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FCNS.Calendar
{
    public partial class Preferences : Form
    {
        public Preferences()
        {
            InitializeComponent();
        }

        private void Preferences_Load(object sender, EventArgs e)
        {
            //窗体
            checkBoxDefaultMiniForm.Checked = (bool)Main.appConfig.Get("DefaultMiniForm", (bool)false);
            checkBoxMinimumMiniForm.Checked = (bool)Main.appConfig.Get("MinimumMiniForm", (bool)true);
            checkBoxPastTask.Checked = (bool)Main.appConfig.Get("ShowPastTask", (bool)false);
            checkBoxUndoneTask.Checked = (bool)Main.appConfig.Get("ShowUndoneTask", (bool)false);
            //日历
            System.DateTime dt;
            dt = System.DateTime.Now;
            while (dt.DayOfWeek != 0)
            {
                dt = dt.AddDays(1);
            }
            for (int i = 0; i < 7; i++)
            {
                comboBoxFirstDay.Items.Add(dt.ToString("dddd"));
                dt = dt.AddDays(1);
            }
            if (5 == (int)Main.appConfig.Get("DaysByWeek", (int)7))
            {
                comboBoxDaysWeek.SelectedIndex = 0;
                comboBoxFirstDay.Enabled = false;
            }
            else
            {
                comboBoxDaysWeek.SelectedIndex = 1;
                comboBoxFirstDay.Enabled = true;
            }

            comboBoxTimeFormat.Items.Add("1:00pm");
            comboBoxTimeFormat.Items.Add("13:00");
            if (0 == (int)Main.appConfig.Get("TimeFormat", (int)1))
            {
                comboBoxTimeFormat.SelectedIndex = 0;
            }
            else
            {
                comboBoxTimeFormat.SelectedIndex = 1;
            }

            System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();

            comboBoxFirstDay.SelectedIndex = int.Parse((Main.appConfig.Get("FirstDayOfWeek", (uint)dtfi.FirstDayOfWeek)).ToString());
            comboBoxHoursViewed.SelectedIndex = int.Parse((Main.appConfig.Get("HoursViewed", (uint)12)).ToString()) - 6;

            comboBoxEndHour.SelectedIndex = int.Parse((Main.appConfig.Get("EndHour", (int)8)).ToString()) - 12;
            comboBoxStartHour.SelectedIndex = int.Parse((Main.appConfig.Get("StartHour", (int)18)).ToString());

            checkBoxTitle.Checked = (bool)Main.appConfig.Get("ShowTitle", (bool)true);
            checkBoxTitle2.Checked = (bool)Main.appConfig.Get("InterceptTitle", (bool)true);
            numericUpDownTitle.Value = (decimal)Main.appConfig.Get("TitleLength", (decimal)15);
            checkBoxAlert.Checked = (bool)Main.appConfig.Get("Alert", (bool)false);
            linkLabelAlert.Text = (string)Main.appConfig.Get("AlertFile", (string)AppDomain.CurrentDomain.BaseDirectory + "alert.mid");
            numericUpDownAlert.Value = (decimal)Main.appConfig.Get("AlertTime", (decimal)60);
            checkBoxDoneTask.Checked = (bool)Main.appConfig.Get("SetPre-TaskDone", (bool)false);

            trackBar1.Value = (255 - (int)Main.appConfig.Get("AlphaEvent", (int)200));
            //墙纸
            comboBoxWallpaper.SelectedIndex = (int)Main.appConfig.Get("TaskWallpaper", (int)1);
            //其它
            checkBoxNotify.Checked = (bool)Main.appConfig.Get("NotifyIcon", true);

            comboBoxCheckUpdates.Items.AddRange(new object[] {
					("手动"),
						("每周"),
					("每月")});
            comboBoxCheckUpdates.SelectedIndex = (int)Main.appConfig.Get("CheckUpdate", (int)2);

            checkBoxAutoRun.Checked = (bool)Main.appConfig.Get("AutoRun", (bool)false);
        }

        private void Preferences_FormClosing(object sender, FormClosingEventArgs e)
        {
            Main.appConfig.SaveXML();
        }

        #region 窗体设置
        private void checkBoxDefaultMiniForm_CheckedChanged(object sender, EventArgs e)
        {
            Main.appConfig.Set("DefaultMiniForm", (bool)checkBoxDefaultMiniForm.Checked);
        }

        private void checkBoxMinimumMiniForm_CheckedChanged(object sender, EventArgs e)
        {
            Main.appConfig.Set("MinimumMiniForm", (bool)checkBoxMinimumMiniForm.Checked);
        }

        private void checkBoxPastTask_CheckedChanged(object sender, EventArgs e)
        {
            Main.appConfig.Set("ShowPastTask", (bool)checkBoxPastTask.Checked);
        }

        private void checkBoxUndoneTask_CheckedChanged(object sender, EventArgs e)
        {
            Main.appConfig.Set("ShowUndoneTask", (bool)checkBoxUndoneTask.Checked);
        }
        #endregion

        #region 日历
        void comboBoxTimeFormat_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Main.appConfig.Set("TimeFormat", (int)comboBoxTimeFormat.SelectedIndex, true);
        }

        void comboBoxHoursViewed_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Main.appConfig.Set("HoursViewed", (uint)(6 + comboBoxHoursViewed.SelectedIndex), true);
        }

        void comboBoxDaysWeek_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (comboBoxDaysWeek.SelectedIndex == 0)
            {
                comboBoxFirstDay.Enabled = false;
                Main.appConfig.Set("DaysByWeek", (int)5, true);
            }
            else
            {
                comboBoxFirstDay.Enabled = true;
                Main.appConfig.Set("DaysByWeek", (int)7, true);
            }
        }

        void comboBoxFirstDay_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Main.appConfig.Set("FirstDayOfWeek", (uint)comboBoxFirstDay.SelectedIndex, true);
        }

        void comboBoxEndHour_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Main.appConfig.Set("EndHour", (int)(12 + comboBoxEndHour.SelectedIndex), true);
        }

        void comboBoxStartHour_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Main.appConfig.Set("StartHour", (int)comboBoxStartHour.SelectedIndex, true);
        }

        private void checkBoxTitle_CheckedChanged(object sender, EventArgs e)
        {
            Main.appConfig.Set("ShowTitle", (bool)checkBoxTitle.Checked);
        }

        private void checkBoxTitle2_CheckedChanged(object sender, EventArgs e)
        {
            Main.appConfig.Set("InterceptTitle", (bool)checkBoxTitle2.Checked);
        }
        private void numericUpDownTitle_ValueChanged(object sender, EventArgs e)
        {
            Main.appConfig.Set("TitleLength", (decimal)numericUpDownTitle.Value);
        }

        private void checkBoxAlert_CheckedChanged(object sender, EventArgs e)
        {
            Main.appConfig.Set("Alert", (bool)checkBoxAlert.Checked, true);
        }
        private void numericUpDownAlert_ValueChanged(object sender, EventArgs e)
        {
            Main.appConfig.Set("AlertTime", (decimal)numericUpDownAlert.Value, true);
        }

        private void linkLabelAlert_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = System.IO.Path.GetDirectoryName(linkLabelAlert.Text);
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    linkLabelAlert.Text = ofd.FileName;
                    Main.appConfig.Set("AlertFile", (string)linkLabelAlert.Text);
                }
            }
        }

        private void buttonAlert_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabelAlert.Text);
        }

        private void checkBoxDoneTask_CheckedChanged(object sender, EventArgs e)
        {
            Main.appConfig.Set("SetPre-TaskDone", (bool)checkBoxDoneTask.Checked, true);
        }

        void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            Main.appConfig.Set("AlphaEvent", (int)(255 - trackBar1.Value), true);
        }
        #endregion

        #region 墙纸
        private void comboBoxWallpaper_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main.appConfig.Set("TaskWallpaper", (int)comboBoxWallpaper.SelectedIndex);
        }
        #endregion

        #region 其它
        void checkBoxNotify_CheckedChanged(object sender, System.EventArgs e)
        {
            Main.appConfig.Set("NotifyIcon", (bool)checkBoxNotify.Checked, true);
        }

        void comboBoxCheckUpdate_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Main.appConfig.Set("CheckUpdate", (int)comboBoxCheckUpdates.SelectedIndex);
        }

        private void checkBoxAutoRun_CheckedChanged(object sender, EventArgs e)
        {
            Main.appConfig.Set("AutoRun", (bool)checkBoxAutoRun.Checked,true);
        }
        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.free-city.cn/bbs");
        }
    }
}
