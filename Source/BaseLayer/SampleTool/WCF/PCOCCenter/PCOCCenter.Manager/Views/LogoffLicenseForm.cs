using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OPT.PCOCCenter.Manager.Views
{
    public partial class LogoffLicenseForm : Form
    {
        MainForm mainForm = null;
        string logoffCode = string.Empty;

        public LogoffLicenseForm(MainForm mainForm, string logoffCode)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.logoffCode = logoffCode;
            
            textBox.Text += (Utils.Utils.Translate("<!--请妥善保存好此文件，下次申请许可时需要用到此文件！-->")+"\r\n");
            textBox.Text += ("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n");
            textBox.Text += ("<!--OPT License Request Key-->\r\n");
            textBox.Text += ("<HostInfo>\r\n");
            string hostID = string.Format("<HostID>{0}</HostID>\r\n", mainForm.serverInfoView.serverHostID);
            textBox.Text += (hostID);
            string hostKEY = string.Format("<HostKey>{0}</HostKey>\r\n", mainForm.serverInfoView.serverKEYID);
            textBox.Text += (hostKEY);
            string hostMAC = string.Format("<HostMAC>{0}</HostMAC>\r\n", mainForm.serverInfoView.serverHostMAC);
            textBox.Text += (hostMAC);
            string hostHDSN = string.Format("<HostHDSN>{0}</HostHDSN>\r\n", mainForm.serverInfoView.serverHostHDSN);
            textBox.Text += (hostHDSN);
            string hostCPUID = string.Format("<HostCPUID>{0}</HostCPUID>\r\n", mainForm.serverInfoView.serverHostCPUID);
            textBox.Text += (hostCPUID);
            string logoffLicenseID = string.Format("<LogoffLicenseID>{0}</LogoffLicenseID>\r\n", logoffCode);
            textBox.Text += (logoffLicenseID);
            textBox.Text += ("</HostInfo>");
       }

        private void btnSaveLogoff_Click(object sender, EventArgs e)
        {
            SaveLogoffLicenseFile();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void SaveLogoffLicenseFile()
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
                        string hostID = string.Format("<HostID>{0}</HostID>", mainForm.serverInfoView.serverHostID);
                        sw.WriteLine(hostID);
                        string hostKEY = string.Format("<HostKey>{0}</HostKey>", mainForm.serverInfoView.serverKEYID);
                        sw.WriteLine(hostKEY);
                        string hostMAC = string.Format("<HostMAC>{0}</HostMAC>", mainForm.serverInfoView.serverHostMAC);
                        sw.WriteLine(hostMAC);
                        string hostHDSN = string.Format("<HostHDSN>{0}</HostHDSN>", mainForm.serverInfoView.serverHostHDSN);
                        sw.WriteLine(hostHDSN);
                        string hostCPUID = string.Format("<HostCPUID>{0}</HostCPUID>", mainForm.serverInfoView.serverHostCPUID);
                        sw.WriteLine(hostCPUID);
                        string logoffLicenseID = string.Format("<LogoffLicenseID>{0}</LogoffLicenseID>", logoffCode);
                        sw.WriteLine(logoffLicenseID);
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
