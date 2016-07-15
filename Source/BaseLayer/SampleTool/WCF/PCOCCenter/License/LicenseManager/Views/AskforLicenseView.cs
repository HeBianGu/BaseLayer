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
using OPT.PEOfficeCenter.Utils;
using System.Xml;
using DevExpress.XtraEditors.Repository;
using System.Diagnostics;

namespace OPT.PEOfficeCenter.LicenseManager.Views
{
    public partial class AskforLicenseView : UserControl
    {
        DataTable dtModuleInfo = new DataTable();
        List<string> colNames = new List<string>();
        Dictionary<string, List<string>> appModuleInfos = new Dictionary<string, List<string>>();
        RepositoryItemComboBox comboApplication = new RepositoryItemComboBox();
        RepositoryItemComboBox comboModuleLicenseType = new RepositoryItemComboBox();
        RepositoryItemDateEdit dateExpireDate = new RepositoryItemDateEdit();
        string hostMAC = string.Empty;
        string hostHDSN = string.Empty;
        string hostCPUID = string.Empty;
        string hostID = string.Empty;
        string logoffLicenseID = "";

        public AskforLicenseView()
        {
            InitializeComponent();
            this.SizeChanged += new EventHandler(AskforLicenseView_SizeChanged);
        }

        private void AskforLicenseView_SizeChanged(object sender, EventArgs e)
        {
            Size size = this.Size;
            size.Width -= (this.gridControl.Location.X + 7);
            size.Height -= (this.gridControl.Location.Y + 5);
            this.gridControl.Size = size;
        }

        public void UpdateAskforLicense()
        {
        }

        private void InitLicenseTypeComboBox()
        {
            comboLicenseType.Properties.Items.Add("Normal");
            comboLicenseType.Properties.Items.Add("Trial");
        }

        private void InitModuleLicenseTypeComboBox()
        {
            comboModuleLicenseType.Items.Add("Normal");
            comboModuleLicenseType.Items.Add("Trial");
            comboModuleLicenseType.Items.Add("free");

            comboAllLicenseType.Properties.Items.Add("Normal");
            comboAllLicenseType.Properties.Items.Add("Trial");
        }

        private void InitCustomerComboBox()
        {

        }

        private void InitApplicationComboBox()
        {
            string moduleInfoConfigFile = "../Config/zh-CN/Registries/ModulesInfo.xml";
            if (!System.IO.File.Exists(moduleInfoConfigFile))
            {
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                moduleInfoConfigFile = appPath+"ModulesInfo.xml";
                if (!System.IO.File.Exists(moduleInfoConfigFile))
                {
                    MessageBox.Show("未找到PEOffice config目录下的模块配置文件ModulesInfo.xml", "提示");
                }
            }

            if (CheckFile(moduleInfoConfigFile, "<FunctionModules>") == false) return;

            XmlNodeList appList = XML.GetNodeList(moduleInfoConfigFile, "ModulesInfo/FunctionModules");

            string appName = "DataEngine";
            string moduleName = appName;
            appModuleInfos[appName] = new List<string>();
            appModuleInfos[appName].Add(moduleName);
            comboApplication.Items.Add(appName);

            appName = "DataManager";
            moduleName = appName;
            appModuleInfos[appName] = new List<string>();
            appModuleInfos[appName].Add(moduleName);
            comboApplication.Items.Add(appName);

            foreach (XmlNode nodeApp in appList)
            {
                appName = XML.Read(nodeApp, "ModuleGroup", "id");

                if (appName == "DataEngine" || appName == "DataManager")
                    continue;

                appModuleInfos[appName] = new List<string>();

                comboApplication.Items.Add(appName);

                XmlNodeList moduleList = XML.GetNodeList(nodeApp, "ModuleGroup/Module");

                if (moduleList != null && moduleList.Count > 0)
                {
                    foreach (XmlNode nodeModule in moduleList)
                    {
                        if (nodeModule != null)
                        {
                            moduleName = XML.Read(nodeModule, "Module", "id");
                            appModuleInfos[appName].Add(moduleName);
                        }
                    }
                }
            }
        }

        private void AskforLicenseView_Load(object sender, EventArgs e)
        {
            InitApplicationComboBox();
            InitModuleLicenseTypeComboBox();
            InitLicenseTypeComboBox();
            InitCustomerComboBox();
            
            colNames.Add("应用程序");
            colNames.Add("授权许可数");
            colNames.Add("截止日期");
            colNames.Add("授权类型");

            // 设置表格数据
            dtModuleInfo.Clear();

            for (int i = 0; i < colNames.Count; i++)
                dtModuleInfo.Columns.Add(colNames[i]);

            gridControl.DataSource = dtModuleInfo;

            gridView.Columns[colNames[0]].ColumnEdit = comboApplication;
            //dateExpireDate.EditMask = "yyyy-MM-dd";
            //dateExpireDate.Mask.UseMaskAsDisplayFormat = true;
            //gridView.Columns[colNames[2]].ColumnEdit = dateExpireDate;
            gridView.Columns[colNames[3]].ColumnEdit = comboModuleLicenseType;
        }

        public void RefreshAskforLicense_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateAskforLicense();
        }

        bool SaveHostKeyFile(string keyFile)
        {
            if( editHostKey.Text == null || editHostKey.Text == "")
            {
                MessageBox.Show("请输入客户机key信息", "提示");
                return false;
            }

            try
            {
                string hostkey = string.Format("{0}", editHostKey.Text);

                FileInfo myFile = new FileInfo(keyFile);
                StreamWriter sw = myFile.CreateText();

                sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                sw.WriteLine("<!--OPT License Request Key-->");
                sw.WriteLine("<HostInfo>");
                string serverHostID = string.Format("<HostID>{0}</HostID>", hostID);
                sw.WriteLine(serverHostID);
                string serverKEYID = string.Format("<HostKey>{0}</HostKey>", hostkey);
                sw.WriteLine(serverKEYID);
                string serverHostMAC = string.Format("<HostMAC>{0}</HostMAC>", hostMAC);
                sw.WriteLine(serverHostMAC);
                string serverHostHDSN = string.Format("<HostHDSN>{0}</HostHDSN>", hostHDSN);
                sw.WriteLine(serverHostHDSN);
                string serverHostCPUID = string.Format("<HostCPUID>{0}</HostCPUID>", hostCPUID);
                sw.WriteLine(serverHostCPUID);
                if (string.IsNullOrEmpty(logoffLicenseID) == false)
                {
                    string logoffLicense = string.Format("<LogoffLicenseID>{0}</LogoffLicenseID>", logoffLicenseID);
                    sw.WriteLine(logoffLicense);
                }
                sw.WriteLine("</HostInfo>");

                sw.Close();

                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
                return false;
            }
        }   

        bool SaveModuleInfoFile(string moduleInfoFile)
        {
            if (dtModuleInfo.Rows.Count <= 0)
            {
                MessageBox.Show("请配置许可信息", "提示");
            }

            string strAppVersion = "V6.0.0.0";

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmldecl;
                xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(xmldecl);
                XmlElement root = xmlDoc.CreateElement("", "LicenseInfo", "");
                xmlDoc.AppendChild(root);
                
                string customer = ""; if (comboCustomer.EditValue != null) customer = comboCustomer.EditValue.ToString();
                XmlElement xe = xmlDoc.CreateElement("Customer"); xe.InnerText = customer;
                root.AppendChild(xe);
                xe = xmlDoc.CreateElement("CreateDate"); xe.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                root.AppendChild(xe);
                xe = xmlDoc.CreateElement("Version"); xe.InnerText = "PEOffice " + strAppVersion;
                root.AppendChild(xe);

                string licenseType = ""; if (comboLicenseType.EditValue != null) licenseType = comboLicenseType.EditValue.ToString();
                xe = xmlDoc.CreateElement("LicenseType"); xe.InnerText = licenseType;
                root.AppendChild(xe);
                
                XmlElement appsRoot = xmlDoc.CreateElement("Applications");
                root.AppendChild(appsRoot);

                string strModuleVersion = "V6.0.0.0";
                foreach (DataRow dtRow in dtModuleInfo.Rows)
                {
                    string appName = dtRow[colNames[0]].ToString();
                    if (appName == null || appName == "") continue;
                    string licenseCount = dtRow[colNames[1]].ToString();
                    if (licenseCount == null || licenseCount == "") continue;
                    string expireDate = dtRow[colNames[2]].ToString();
                    if (expireDate == null || expireDate == "") continue;
                    string licType = dtRow[colNames[3]].ToString();
                    if (licType == null || licType == "") continue;

                    List<string> moduleNames = null;
                    
                    try
                    {
                        moduleNames = appModuleInfos[appName];
                    }
                    catch (System.Exception ex)
                    {
                        continue;
                    }

                    XmlElement appxe = xmlDoc.CreateElement("Application");
                    appxe.SetAttribute("Name", appName);
                    appxe.SetAttribute("Version", strAppVersion);
                    appsRoot.AppendChild(appxe);

                    
                    XmlElement modulesRoot = xmlDoc.CreateElement("Modules");
                    appxe.AppendChild(modulesRoot);

                    if (moduleNames == null) continue;
                    foreach (string moduleName in moduleNames)
                    {
                        XmlElement modulexe = xmlDoc.CreateElement("Module");
                        modulexe.SetAttribute("Name", moduleName);
                        modulexe.SetAttribute("Version", strModuleVersion);
                        modulexe.SetAttribute("LicenseCount", licenseCount);
                        modulexe.SetAttribute("ExpiryDate", expireDate);
                        modulexe.SetAttribute("LicenseType", licType);
                        modulesRoot.AppendChild(modulexe);
                    }
                }
                xmlDoc.Save(moduleInfoFile);

                return true;
            }
            catch (System.Exception ex)
            {
                return false;           	
            }
        }

        bool CheckFile(string file, string strMatchCode)
        {
            bool ret = false;

            try
            {
                // 校验是否为Key文件
                FileInfo myFile = new FileInfo(file);
                StreamReader sr = myFile.OpenText();

                string strHeader = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    if (strHeader.IndexOf("<!--") >= 0)
                    {
                        strHeader = sr.ReadLine();
                        continue;
                    }
                    else
                    {
                        break;
                    }                    
                }

                if (strHeader.IndexOf("<?xml") >= 0)
                {
                    while (!sr.EndOfStream)
                    {
                        string strLine = sr.ReadLine();

                        if (strLine.IndexOf(strMatchCode) >= 0)
                        {
                            ret = true;
                            break;
                        }
                    }
                }

                sr.Close();
            }
            catch (Exception exp)
            {

            }
            
            return ret;
        }

        private void btnGenLicenseFile_Click(object sender, EventArgs e)
        {
                try
                {
                    string licenseFile = string.Empty;
                    using (SaveFileDialog dlg = new SaveFileDialog())
                    {
                        dlg.Filter = "所有支持的格式(*.lic)|*.lic|所有文件(*.*)|*.*";

                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            licenseFile = dlg.FileName;
                            string licenseFileName = Path.GetFileNameWithoutExtension(licenseFile);

                            int nPos = licenseFile.LastIndexOf("\\");
                            string licPath = licenseFile.Substring(0, nPos+1);

                            if (SaveHostKeyFile(licPath + "host.key") && SaveModuleInfoFile(licPath + licenseFileName + @".xml"))
                            {
                                // 生成许可文件
                                Process myProcess = new Process();
                                
                                string fileName = @"LicenseGenerator.exe";

                                string args = "-k\"" + licPath + "host.key\" -m\"" + licPath + licenseFileName + ".xml\"" + " -l\"" + licenseFile + "\"";
                                ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(fileName, args);
                                
                                myProcess.StartInfo = myProcessStartInfo;
                                
                                myProcess.Start();
                                
                                while (!myProcess.HasExited)
                                {
                                    myProcess.WaitForExit();
                                }
                                
                                int returnValue = myProcess.ExitCode;


                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Failed to create license file" + ": " + exp.Message);
                }
        }

        private void btnLoadKeyFile_Click(object sender, EventArgs e)
        {
            try
            {
                string hostKeyFile = string.Empty;
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Filter = "所有支持的格式(*.key)|*.key|所有文件(*.*)|*.*";

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        hostKeyFile = dlg.FileName;

                        // 此处需要校验是否为key文件
                        if (CheckFile(hostKeyFile, "<HostKey>") == false)
                        {
                            string strMsg = string.Format("{0}不是客户机信息文件，请确认后再重试！", hostKeyFile);
                            MessageBox.Show(strMsg, "提示");
                            return;
                        }

                        string hostKey = XML.Read(hostKeyFile, "HostInfo/HostKey", "");
                        hostMAC = XML.Read(hostKeyFile, "HostInfo/HostMAC", "");
                        hostHDSN = XML.Read(hostKeyFile, "HostInfo/HostHDSN", "");
                        hostCPUID = XML.Read(hostKeyFile, "HostInfo/HostCPUID", "");
                        hostID = XML.Read(hostKeyFile, "HostInfo/HostID", "");
                        logoffLicenseID = XML.Read(hostKeyFile, "HostInfo/LogoffLicenseID", "");

                        if (hostKey.Length >= 16)
                        {
                            editHostKey.Text = hostKey;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Failed to open hostkey file" + ": " + exp.Message);
            }
        }

        private void btnUpdateAllLicenseCount_Click(object sender, EventArgs e)
        {
            foreach (DataRow dtRow in dtModuleInfo.Rows)
            {
                dtRow[colNames[1]] = edtLicenseCountAll.Text;
            }
        }

        private void btnUpdateAllExpiryDate_Click(object sender, EventArgs e)
        {
            foreach (DataRow dtRow in dtModuleInfo.Rows)
            {
                dtRow[colNames[2]] = edtExpiryDateAll.Text;
            }
        }

        private void comboAllLicenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow dtRow in dtModuleInfo.Rows)
            {
                if (dtRow[colNames[3]].ToString()!="free")
                dtRow[colNames[3]] = comboAllLicenseType.Text;
            }
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            DataRow dtRow = dtModuleInfo.NewRow();
            dtModuleInfo.Rows.Add(dtRow);

            gridControl.DataSource = dtModuleInfo;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
        }

        private void btnLoadModuleInfo_Click(object sender, EventArgs e)
        {
            try
            {
                string moduleInfoFile = string.Empty;
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Filter = "所有支持的格式(*.xml)|*.xml|所有文件(*.*)|*.*";

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        moduleInfoFile = dlg.FileName;

                        // 此处需要校验是否为key文件
                        if (CheckFile(moduleInfoFile, "<LicenseInfo>") == false)
                        {
                            string strMsg = string.Format("{0}不是模块许可信息文件，请确认后再重试！", moduleInfoFile);
                            MessageBox.Show(strMsg, "提示");
                            return;
                        }

                        string customer = XML.Read(moduleInfoFile, "LicenseInfo/Customer", "");
                        if (customer != null && customer != "")
                        {
                            comboCustomer.EditValue = customer;
                        }

                        string licenseType = XML.Read(moduleInfoFile, "LicenseInfo/LicenseType", "");
                        if (licenseType != null && licenseType != "")
                        {
                            comboLicenseType.EditValue = licenseType;
                        }

                        XmlNodeList appList = XML.GetNodeList(moduleInfoFile, "LicenseInfo/Applications");

                        // 设置表格数据
                        dtModuleInfo.Clear();

                        foreach (XmlNode nodeApp in appList)
                        {
                            int j = 0;

                            XmlNodeList modulesList = XML.GetNodeList(nodeApp, "Application/Modules");

                            if (modulesList != null && modulesList.Count > 0)
                            {
                                XmlNodeList moduleList = XML.GetNodeList(modulesList[0], "Module");
                                if (moduleList != null && moduleList.Count > 0)
                                {
                                    XmlNode nodeModule = moduleList[0];

                                    if (nodeModule != null)
                                    {
                                        DataRow dtRow = dtModuleInfo.NewRow();
                                        dtRow[colNames[0]] = XML.Read(nodeApp, "Application", "Name");
                                        dtRow[colNames[1]] = XML.Read(nodeModule, "Module", "LicenseCount");
                                        dtRow[colNames[2]] = XML.Read(nodeModule, "Module", "ExpiryDate");
                                        dtRow[colNames[3]] = XML.Read(nodeModule, "Module", "LicenseType");

                                        dtModuleInfo.Rows.Add(dtRow);
                                    }
                                }
                            }
                        }

                        gridControl.DataSource = dtModuleInfo;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Failed to open moduleInfo file" + ": " + exp.Message);
            }
        }

    }
}
