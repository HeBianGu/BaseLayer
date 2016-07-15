using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors;
using OPT.Product.Base;
using OPT.PCOCCenter.Utils;
using OPT.PEOfficeCenter.Utils;

namespace OPT.PCOCCenter.TaskProxy
{
    public partial class PublishTaskForm : Form
    {
        List<TaskInfo> ListTaskInfos = null;
        DataTable dtSimulationHost = null;
        RepositoryItemComboBox comboBoxHostList = null;
        string localHostStr = "(localhost)";
        string localHostIP = "127.0.0.1";

        public PublishTaskForm(ref List<TaskInfo> listTaskInfos)
        {
            InitializeComponent();
            ListTaskInfos = listTaskInfos;
        }

        private void PublishTaskForm_Load(object sender, EventArgs e)
        {
            InitSimulationHostsComboBox();

            foreach (TaskInfo taskInfo in ListTaskInfos)
            {
                EditorRow itemTaskInfo = new EditorRow();
                itemTaskInfo.Name = taskInfo.Name;
                itemTaskInfo.Properties.Caption = taskInfo.Name;
                itemTaskInfo.Properties.RowEdit = comboBoxHostList;
                itemTaskInfo.Properties.Value = localHostStr;

                vGridTaskInfos.Rows.Add(itemTaskInfo);
            }
        }

        private void InitSimulationHostsComboBox()
        {
            comboBoxHostList = new RepositoryItemComboBox();
            comboBoxHostList.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;


            // 获取模拟器主机列表
            dtSimulationHost = OPT.PCOCCenter.Client.Client.GetSimulationHostList();
            comboBoxHostList.Items.Add(localHostStr);
            if (dtSimulationHost != null)
            {
                for (int i = 0; i < dtSimulationHost.Rows.Count; i++)
                {
                    DataRow dtRow = dtSimulationHost.Rows[i];

                    string hostIP = dtRow["HostIP"].ToString();
                    string hostName = dtRow["HostName"].ToString();

                    string hostItem = string.Format("{0}[{1}]", hostIP, hostName);
                    comboBoxHostList.Items.Add(hostItem);
                }
            }
        }

        private string GetWorkerIP(string simulateHost)
        {
            string WorkerIP = string.Empty;

            if (simulateHost == localHostStr)
            {
                WorkerIP = localHostIP;
            }
            else
            {
                int pos = simulateHost.IndexOf("[");
                if(pos>0) WorkerIP = simulateHost.Substring(0, pos);
            }

            return WorkerIP;
        }

        string GetSimulatorPath(string workerIP)
        {
            string simulatorPath = string.Empty;

            if (workerIP == localHostIP)
            {
                // 本地运算，直接读取Eclipse的配置文件
                string xmlFile = System.IO.Path.Combine(BxSystemInfo.Instance.SystemPath.PEOffice6Documents, "TaskProxy");
                if (!System.IO.Directory.Exists(xmlFile))
                    System.IO.Directory.CreateDirectory(xmlFile);
                xmlFile += @"\Config.xml";

                simulatorPath = XML.Read(xmlFile, "EclipsePath", "");
            }
            else
            {
                foreach (DataRow dtRow in dtSimulationHost.Rows)
                {
                    if (dtRow["HostIP"].ToString() == workerIP)
                    {
                        simulatorPath = dtRow["SimulationPath"].ToString();
                        break;
                    }
                }
            }

            return simulatorPath;
        }

        string GetSimulatorLicensePath(string workerIP)
        {
            string simulatorLicensePath = string.Empty;

            if (workerIP == localHostIP)
            {
                // 本地运算，直接读取Eclipse的配置文件
                string xmlFile = System.IO.Path.Combine(BxSystemInfo.Instance.SystemPath.PEOffice6Documents, "TaskProxy");
                if (!System.IO.Directory.Exists(xmlFile))
                    System.IO.Directory.CreateDirectory(xmlFile);
                xmlFile += @"\Config.xml";

                simulatorLicensePath = XML.Read(xmlFile, "LicensePath", "");
            }
            else
            {
                foreach (DataRow dtRow in dtSimulationHost.Rows)
                {
                    if (dtRow["HostIP"].ToString() == workerIP)
                    {
                        simulatorLicensePath = dtRow["LicensePath"].ToString();
                        break;
                    }
                }
            }

            return simulatorLicensePath;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int row = 0;
            foreach (TaskInfo taskInfo in ListTaskInfos)
            {
                EditorRow editRow = null;

                if (row < vGridTaskInfos.Rows.Count)
                    editRow = vGridTaskInfos.Rows[row] as EditorRow;

                if (editRow!=null)
                    taskInfo.WorkerIP = GetWorkerIP(editRow.Properties.Value.ToString());

                taskInfo.SimulatorPath = GetSimulatorPath(taskInfo.WorkerIP);
                taskInfo.SimulatorLicensePath = GetSimulatorLicensePath(taskInfo.WorkerIP);

                row++;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnLocalHostSetting_Click(object sender, EventArgs e)
        {
            SimulationSettingForm settingForm = new SimulationSettingForm();
            settingForm.ShowDialog();
        }

        private void radioHostGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup radioGroup = sender as RadioGroup;

            if (radioGroup.Text == "localHost")
            {
                // 选择本机
                foreach (EditorRow editRow in vGridTaskInfos.Rows)
                {
                    editRow.Properties.Value = localHostStr;
                }
            }
            else if (radioGroup.Text == "autoHost")
            {
                // 选择自动
                string simulateHost = localHostStr;

                if (comboBoxHostList.Items.Count>=2)
                    simulateHost = comboBoxHostList.Items[1].ToString();

                foreach (EditorRow editRow in vGridTaskInfos.Rows)
                {
                    editRow.Properties.Value = simulateHost;
                }
            }
        }
    }
}
