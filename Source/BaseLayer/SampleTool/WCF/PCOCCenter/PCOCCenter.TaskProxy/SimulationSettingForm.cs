using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using OPT.Product.Base;
using OPT.PCOCCenter.Utils;
using OPT.PEOfficeCenter.Utils;

namespace OPT.PCOCCenter.TaskProxy
{
    public partial class SimulationSettingForm : Form
    {
        public SimulationSettingForm()
        {
            InitializeComponent();

            //获得主机名
            edtHostName.Text = "(localhost)";
            edtHostName.Properties.ReadOnly = true;

            //获取IP
            comboHostIP.EditValue = "127.0.0.1";
            comboHostIP.Properties.ReadOnly = true;

            // 本地运算，直接读取Eclipse的配置文件
            string xmlFile = System.IO.Path.Combine(BxSystemInfo.Instance.SystemPath.PEOffice6Documents, "TaskProxy");
            if (!System.IO.Directory.Exists(xmlFile))
                System.IO.Directory.CreateDirectory(xmlFile);
            xmlFile += @"\Config.xml";

            edtLicensePath.Text = XML.Read(xmlFile, "LicensePath", "");
            edtEclipsePath.Text = XML.Read(xmlFile, "EclipsePath", "");
        }

        bool SaveEclipseHostConfig()
        {
            string xmlFile = System.IO.Path.Combine(BxSystemInfo.Instance.SystemPath.PEOffice6Documents, "TaskProxy");
            if (!System.IO.Directory.Exists(xmlFile))
                System.IO.Directory.CreateDirectory(xmlFile);
            xmlFile += @"\Config.xml";

            XML.Update(xmlFile, "EclipsePath", "", edtEclipsePath.Text);
            XML.Update(xmlFile, "LicensePath", "", edtLicensePath.Text);

            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (SaveEclipseHostConfig() == true)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("设置本机Eclipse出错，请确认后再试！", "提示");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnExplorerEclipse_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Filter = "所有支持的格式(*.exe)|*.exe|所有文件(*.*)|*.*";

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {                        
                        edtEclipsePath.Text = dlg.FileName;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Failed to select Eclipse exe file" + ": " + exp.Message);
            }
        }

        private void btnExplorerLicense_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Filter = "所有支持的格式(*.DAT)|*.DAT|所有文件(*.*)|*.*";

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        edtLicensePath.Text = dlg.FileName;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Failed to select Eclipse License file" + ": " + exp.Message);
            }
        }

    }
}
