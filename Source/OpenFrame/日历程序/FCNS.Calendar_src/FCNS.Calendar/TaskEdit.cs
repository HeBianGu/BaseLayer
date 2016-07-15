using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FCNS.Calendar
{
    public partial class TaskEdit : Form
    {
        public TaskEdit()
        {
            InitializeComponent();
        }

        DateTime temp;
        CalTask task=null;
        /// <summary>
        /// 获取或设置要编辑的任务
        /// </summary>
        public CalTask Task
        {
            get
            {
                return task;
            }
            set
            {
                task = value;
            }
        }

        public List<DateTime> TaskLoopCount
        {
            get
            {
                List<DateTime> s = new List<DateTime>();
                //for (int i = 0; i < listBoxTask.Items.Count; i++)
                //{
                //    s.Add(listBoxTask.GetItemText(listBoxTask.Items[i]));
                //}
                return s;
            }
            set
            {
                    //listBoxTask.Items.AddRange(value);
            }
        }

        private void TaskDetail_Load(object sender, EventArgs e)
        {
            if (task == null)
            {
                task = new CalTask();
            }
            else
            {
                temp = task.end;
                temp.Subtract(task.start);
            }
            textBoxTitle.Text = task.Title;
            checkBoxAlert.Checked = task.Alert;
            dateTimePickerStart.Value = task.start;
            dateTimePickerEnd.Value = task.end;
            richTextBoxSummary.Text = task.Summary;
        }

        private void TaskDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            //重复任务相同 DateTime 的清除
            //if (listBoxTask.Items.Count != 0)
            //{
            //    for (int i = 0; i < listBoxTask.Items.Count; i++)
            //    {

            //    }
            //}
            if (MessageBox.Show("是否保存任务?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                task.Title = textBoxTitle.Text;
                task.Alert = checkBoxAlert.Checked;
                task.Start = dateTimePickerStart.Value.ToString("yyyyMMddHHmmss");
                if (dateTimePickerEnd.Value.CompareTo(dateTimePickerStart.Value) != 1)
                {
                    dateTimePickerEnd.Value = dateTimePickerEnd.Value.Add(TimeSpan.Parse(temp.ToShortTimeString()));
                }
                task.End = dateTimePickerEnd.Value.ToString("yyyyMMddHHmmss");
                task.Summary = richTextBoxSummary.Text;
            }
            //this.DialogResult = DialogResult.OK;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            listBoxTask.Items.Add(dateTimePickerLoop.Value);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxTask.SelectedItem != null)
            {
                listBoxTask.Items.Remove(listBoxTask.SelectedItem);
            }
        }

        private void buttonAddWeek_Click(object sender, EventArgs e)
        {
            DateTime d = dateTimePickerLoop.Value;
            for (int i = 1; i < 8; i++)
            {
                d=d.AddDays(1);
                listBoxTask.Items.Add(d);
            }
        }
    }
}
