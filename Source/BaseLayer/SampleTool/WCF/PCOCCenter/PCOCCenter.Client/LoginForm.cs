using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Repository;
using OPT.PCOCCenter.Utils;
using DevExpress.XtraEditors;
using OPT.PEOfficeCenter.Utils;

namespace OPT.PCOCCenter.Client
{
    public partial class LoginForm : Form
    {
        public static string ConfigFile = "ClientConfig.xml";

        // 服务器（ip:port[192.168.168.254:22888]）
        public string Server { get; set; }

        // 登陆用户名
        public string UserName { get; set; }

        // 登陆密码
        public string Password { get; set; }

        // 是否保存登陆密码
        public string SavePassword { get; set; }

        // 登陆信息配置文件
        string xmlFile = string.Empty;

        public LoginForm()
        {
            InitializeComponent();
            xmlFile = ConfigFile;
        }

        // 是否保存密码，点击时更新vGridControl状态
        void edtSavePassword_EditValueChanged(object sender, EventArgs e)
        {
            CheckEdit editCheck = sender as CheckEdit;

            VGridRows rows = this.vGridControl.Rows;
            if (rows != null)
            {
                BaseRow row = rows["itemSavePassword"];
                if (row != null) row.Properties.Value = editCheck.EditValue;
            }
        }

        // 初始化登陆窗口
        private void LoginForm_Load(object sender, EventArgs e)
        {
            VGridRows rows = this.vGridControl.Rows;
            if (rows != null)
            {
                int nIndex = 1;
                string historyServer = string.Empty;
                while (true)
                {
                    historyServer = XML.Read(xmlFile, "Server", "server" + nIndex);
                    if (historyServer=="" || historyServer==string.Empty)
                    {
                        break;
                    }
                    else
                    {
                        if (comboBoxServer.Items.Contains(historyServer) == false)
                            comboBoxServer.Items.Add(historyServer);
                    }
                    nIndex++;
                }
                
                
                Server = XML.Read(xmlFile, "Server", "server");
                UserName = XML.Read(xmlFile, "UserName", "");
                Password = XML.Read(xmlFile, "Password", "");
                SavePassword = XML.Read(xmlFile, "SavePassword", "");

                if (comboBoxServer != null)
                {
                    if (comboBoxServer.Items.Contains(Server) == false)
                        comboBoxServer.Items.Insert(0,Server);
                }

                BaseRow row = rows["itemServer"];
                if (row != null) row.Properties.Value = Server;
                row = rows["itemUserName"];
                if (row != null) row.Properties.Value = UserName;
                row = rows["itemPassword"];
                if (row != null) row.Properties.Value = Password;
                row = rows["itemSavePassword"];
                if (row != null)
                {
                    bool savePassword = true;
                    bool.TryParse(SavePassword, out savePassword);
                    row.Properties.Value = savePassword;
                }
            }
        }

        // 连接服务器
        private void btnConnect_Click(object sender, EventArgs e)
        {
            VGridRows rows = this.vGridControl.Rows;
            if (rows != null)
            {
                BaseRow row = rows["itemServer"];
                if (row != null && row.Properties.Value!=null) Server = row.Properties.Value.ToString();
                row = rows["itemUserName"];
                if (row != null && row.Properties.Value != null) UserName = row.Properties.Value.ToString();
                row = rows["itemPassword"];
                if (row != null && row.Properties.Value != null) Password = row.Properties.Value.ToString();
                row = rows["itemSavePassword"];
                if (row != null && row.Properties.Value != null) SavePassword = row.Properties.Value.ToString();
                
                XML.Clear(xmlFile);
                XML.Update(xmlFile, "Server", "server", Server);
                int nIndex = 1;
                for (int i = 1; i < comboBoxServer.Items.Count + 1; i++)
                {
                    if (comboBoxServer.Items[i - 1].ToString() != Server)
                    {
                        XML.Update(xmlFile, "Server", "server" + nIndex, comboBoxServer.Items[i - 1].ToString());
                        nIndex++;
                    }
                }
                XML.Update(xmlFile, "UserName", "", UserName);
                XML.Update(xmlFile, "Password", "", Password);
                XML.Update(xmlFile, "SavePassword", "", SavePassword);
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        // 取消登陆窗口
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
