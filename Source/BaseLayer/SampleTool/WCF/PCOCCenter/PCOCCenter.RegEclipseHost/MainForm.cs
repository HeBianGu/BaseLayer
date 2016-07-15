using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace OPT.PCOCCenter.RegEclipseHost
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //获得主机名
            edtHostName.Text = Dns.GetHostName();

            //获取IP
            IPHostEntry ipEntry = Dns.GetHostEntry(edtHostName.Text);

            string defaultIP = string.Empty;
            foreach (IPAddress ipaddr in ipEntry.AddressList)
            {
                string ip = ipaddr.ToString();
                if (ip.IndexOf(':') > 0) continue;

                if (defaultIP == string.Empty) defaultIP = ip;
                comboHostIP.Properties.Items.Add(ip);
            }
            comboHostIP.EditValue = defaultIP;
        }

        bool RegEclipseHost()
        {
            int ret = OPT.PCOCCenter.Client.Client.RegSimulationHost(edtHostName.Text, comboHostIP.EditValue.ToString(), "Eclipse", edtLicensePath.Text, edtEclipsePath.Text, "");
            if (ret == 1)
                return true;
            else
                return false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (RegEclipseHost() == true)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("注册Eclipse主机出错，请确认后再试！", "提示");
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
