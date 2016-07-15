using HebianGu.ComLibModule.TaskEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoTaskManager
{
    public partial class TaskList : Form
    {
        public TaskList()
        {
            InitializeComponent();


        }

        private void TaskList_Load(object sender, EventArgs e)
        {
            LoadData();

            InitData();
        }

        //  任务列表
        TaskListManager<CaseTaskCore> taskList = null;

        CaseTaskCore _case = null;

        void InitData()
        {

            this.bindingSource1.DataSource = taskList.TaskList.ToList();
            //  绑定数据
            this.tv_list.DataSource = this.bindingSource1;
            //this.tv_list.KeyFieldName = "Id";
            //this.tv_list.ParentFieldName = "Pid";
            this.tv_list.ExpandAll();

        }
        void LoadData()
        {
            //  初始化任务列表
            taskList = TaskListManager<CaseTaskCore>.Instance;

            CaseTaskCore c = new CaseTaskCore();
            taskList.ContinueLast(c);
            CaseTaskCore c1 = new CaseTaskCore();
            taskList.ContinueLast(c1);
            CaseTaskCore c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);
            c2 = new CaseTaskCore();
            taskList.ContinueLast(c2);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            //if (_case != null)
            //{
            //    if (_case.TaskCore.Status == TaskStatus.WaitingToRun)
            //    {
            //        _case.TaskCore.Start();
            //    }
            //    else
            //    {
            //        var find = taskList.TaskList.Find(_case);
            //        if (find != null)
            //        {
            //            find.Next.Value.TaskCore.Start();
            //        }
            //    }

            //}
            taskList.Start();

            this.timer1.Start();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.tv_list.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.tv_list.Refresh();

            if(this.taskList.IsComplete)
            {
                this.timer1.Stop();
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            _case = taskList.Stop();

        }


        private void btn_clear_Click(object sender, EventArgs e)
        {
            TaskListManager<CaseTaskCore>.Instance.ClearTask();

            this.bindingSource1.DataSource = TaskListManager<CaseTaskCore>.Instance.TaskList.ToList();
        }

        private void btn_continue_Click(object sender, EventArgs e)
        {
            TaskListManager<CaseTaskCore>.Instance.Continue();
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            TaskListManager<CaseTaskCore>.Instance.Pause();
        }

        private void tv_list_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            //if (e.Column.FieldName != "Status")
            //    return;
            if(e==null||e.Node==null)
            {
                return;
            }

            switch (e.Node.GetValue("Status").ToString())
            {
                case "准备":
                    e.Appearance.BackColor = Color.Green;
                    break;
                case "等待":
                    e.Appearance.BackColor = Color.LightSkyBlue;
                    break;
                case "暂停":
                    e.Appearance.BackColor = Color.LightGray;
                    break;
                case "尚未开始执行":
                    e.Appearance.BackColor = Color.LightSkyBlue;
                    break;
                case "运行":
                    e.Appearance.BackColor = Color.Green;
                    break;
                case "等待子任务":
                    e.Appearance.BackColor = Color.LightSkyBlue;
                    break;
                case "完成":
                    e.Appearance.BackColor = Color.White;
                    break;
                case "取消":
                    e.Appearance.BackColor = Color.Yellow;
                    break;
                case "异常":
                    e.Appearance.BackColor = Color.Red;
                    break;
                default:
                    break;
            }
        }

        private void btn_stopnow_Click(object sender, EventArgs e)
        {
            TaskListManager<CaseTaskCore>.Instance.RunTask.Stop();
        }
    }
}
