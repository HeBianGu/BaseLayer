using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FCNS.Calendar
{
    public partial class SpecialTask : Form
    {
        public SpecialTask()
        {
            InitializeComponent();

                comboBoxTaskType.Items.AddRange(Enum.GetNames(typeof(SpecialTasks)));
           
        }

        public delegate void AddTask(CalTask task);
        public event AddTask NewSpecialTask;

        private void buttonPath_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    labelPath.Text = ofd.FileName;
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxTaskType.Text != "")
            {
                CalTask task = new CalTask();
                task.Special = true;
                task.Start = task.End = dateTimePicker1.Value.ToString("yyyyMMddHHmmss");
                task.Title = comboBoxTaskType.Text;
                task.Alert = true;
                switch ((SpecialTasks)Enum.Parse(typeof(SpecialTasks), comboBoxTaskType.Text))
                {
                    //case  SpecialTasks.待机:
                    //    break;
                    //     case  SpecialTasks.重启:
                    //    break;
                    //     case  SpecialTasks.关机:
                    //    break;
                    case SpecialTasks.弹出消息窗口:
                        task.Summary = textBoxMessage.Text;
                        break;
                    case SpecialTasks.运行指定程序:
                        task.Summary = labelPath.Text;
                        break;
                    case SpecialTasks.关闭指定程序:
                        task.Summary = labelPath.Text;
                        break;
                }
                    NewSpecialTask(task);
            }
        }
    }
}
