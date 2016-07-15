using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using System.IO;

namespace OPT.PCOCCenter.Manager.Views
{
    public partial class ServerInfoView : UserControl
    {
        public string serverHostMAC = string.Empty;
        public string serverHostHDSN = string.Empty;
        public string serverHostCPUID = string.Empty;
        public string serverHostID = string.Empty;
        public string serverKEYID = string.Empty;

        public ServerInfoView()
        {
            InitializeComponent();
            this.SizeChanged += new EventHandler(PEModulesView_SizeChanged);
        }

        private void PEModulesView_SizeChanged(object sender, EventArgs e)
        {
//            this.panelInfo.Size = this.Size;
        }

        public void UpdateServerInfo()
        {
            string serverInfo = OPT.PCOCCenter.Client.Client.GetServerInfo();

            string[] sArray = serverInfo.Split(';');

            if (sArray.Length >= 10)
            {
                string ver = sArray[0].ToString();
                ServerDateTime.Text = sArray[1].ToString();
                ServerRunTimes.Text = sArray[2].ToString();
                ServerIP.Text = sArray[3].ToString();
                ServerPort.Text = sArray[4].ToString();
                OnlineUsers.Text = sArray[5].ToString();
                LicenseApps.Text = sArray[6].ToString();
                LicenseModules.Text = sArray[7].ToString();
                LicenseType.Text = sArray[8].ToString();
                LicenseExpiryDate.Text = sArray[9].ToString();
            }
            if (sArray.Length >= 12)
            {
                if (serverHostID == string.Empty)
                    serverHostID = sArray[10].ToString();

                if (serverKEYID == string.Empty)
                    serverKEYID = sArray[11].ToString();
            }
            if (sArray.Length >= 15)
            {
                serverHostMAC = sArray[12].ToString();
                serverHostHDSN = sArray[13].ToString();
                serverHostCPUID = sArray[14].ToString();
            }
        }

        private void ServerInfoView_Load(object sender, EventArgs e)
        {
            UpdateServerInfo();

            if (serverKEYID != string.Empty)
            {
                this.labelServerID.Text = serverKEYID;
                this.labelHostID.Text = serverHostID;
            }
        }

        public void RefreshServerInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateServerInfo();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(Utils.Utils.Translate("主机ID：")+this.labelHostID.Text + Utils.Utils.Translate("\n产品ID：") + this.labelServerID.Text);
        }

        private void btnWriteFile_Click(object sender, EventArgs e)
        {
            try
            {
                string keyFile = string.Empty;
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = Utils.Utils.Translate("所支持的格式(*.key)|*.key|所有文件(*.*)|*.*");

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        keyFile = dlg.FileName;

                        FileInfo myFile = new FileInfo(keyFile);
                        StreamWriter sw = myFile.CreateText();

                        sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                        sw.WriteLine("<!--OPT License Request Key-->");
                        sw.WriteLine("<HostInfo>");
                        string hostID = string.Format("<HostID>{0}</HostID>", serverHostID);
                        sw.WriteLine(hostID);
                        string hostKEY = string.Format("<HostKey>{0}</HostKey>", serverKEYID);
                        sw.WriteLine(hostKEY);
                        string hostMAC = string.Format("<HostMAC>{0}</HostMAC>", serverHostMAC);
                        sw.WriteLine(hostMAC);
                        string hostHDSN = string.Format("<HostHDSN>{0}</HostHDSN>", serverHostHDSN);
                        sw.WriteLine(hostHDSN);
                        string hostCPUID = string.Format("<HostCPUID>{0}</HostCPUID>", serverHostCPUID);
                        sw.WriteLine(hostCPUID);
                        sw.WriteLine("</HostInfo>");

                        sw.Close();

                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Failed to write key file" + ": " + exp.Message);
            }

        }

    }
}
