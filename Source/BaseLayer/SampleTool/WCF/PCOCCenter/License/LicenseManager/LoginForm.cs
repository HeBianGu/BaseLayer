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
using OPT.PEOfficeCenter.Utils;
using DevExpress.XtraEditors;

namespace OPT.PEOfficeCenter.LicenseManager
{
    public partial class LoginForm : Form
    {
        public static string ConfigFile = "Config.xml";

        // 登录用户名
        public string UserName { get; set; }

        // 登录密码
        public string Password { get; set; }

        // 登录信息配置文件
        string xmlFile = string.Empty;

        public LoginForm()
        {
            InitializeComponent();
            xmlFile = ConfigFile;
        }

        // 初始化登录窗口
        private void LoginForm_Load(object sender, EventArgs e)
        {
            VGridRows rows = this.vGridControl.Rows;
            if (rows != null)
            {
                UserName = XML.Read(xmlFile, "UserName", "");
                Password = XML.Read(xmlFile, "Password", "");

                BaseRow row = rows["itemUserName"];
                if (row != null) row.Properties.Value = UserName;
                row = rows["itemPassword"];
                if (row != null) row.Properties.Value = Password;
            }
        }

        // 连接服务器
        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        // 取消登录窗口
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
