using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.IO;
using OPT.PCOCCenter.Service;

namespace OPT.PEOfficeCenter.LicenseManager.Views
{
    public partial class LicenseListView : UserControl
    {
        MainForm mainForm = null;

        public LicenseListView(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            this.SizeChanged += new EventHandler(LicenseFilesView_SizeChanged);

        }

        private void LicenseFilesView_SizeChanged(object sender, EventArgs e)
        {
            Size size = this.Size;
            size.Width -= (this.gridControl.Location.X + 7);
            size.Height -= (this.gridControl.Location.Y + 5);
            this.gridControl.Size = size;
        }

        void InitGrid(List<LicenseInfo> licenseInfos)
        {
            mainForm.licenseAppList.Clear();
            List<string> colNames = new List<string>();
            colNames.Add("授权文件");
            colNames.Add("应用程序");
            colNames.Add("授权许可数");
            colNames.Add("占用许可数");
            colNames.Add("截止日期");

            // 设置表格数据
            DataTable dt = new DataTable();            

            for (int i = 0; i < colNames.Count; i++ )
                dt.Columns.Add(colNames[i]);

            for (int i = 0; i < licenseInfos.Count; i++)
            {
                LicenseInfo oLicenseInfo = licenseInfos[i];
                for (int mIndex = 0; mIndex < oLicenseInfo.ModuleInfos.Count; mIndex++)
                {
                    string appName = oLicenseInfo.ModuleInfos[mIndex].AppName;
                    if (mainForm.licenseAppList.IndexOf(appName) >= 0) continue;

                    DataRow dtRow = dt.NewRow();
                    string LicenseFile = string.Format("{0} -- [{1}]", i + 1, oLicenseInfo.LicenseType);
                    dtRow[colNames[0]] = LicenseFile;
                    dtRow[colNames[1]] = appName;
                    dtRow[colNames[2]] = oLicenseInfo.ModuleInfos[mIndex].LicenseCount;
                    dtRow[colNames[3]] = oLicenseInfo.ModuleInfos[mIndex].LicenseUsed;
                    dtRow[colNames[4]] = oLicenseInfo.ModuleInfos[mIndex].ExpiryDate;
                    
                    dt.Rows.Add(dtRow);
                    mainForm.licenseAppList.Add(appName);
                }                    
            }

            gridControl.DataSource = dt;
            gridView.Columns[colNames[0]].GroupIndex = 0;
            gridView.ExpandAllGroups();

            int j = 0;
        }

        bool CheckLicenseFile(string licenseFile)
        {
            bool ret = false;

            string strLicenseHeader = "OPTLIC";

            // 校验是否为许可文件
            FileInfo myFile = new FileInfo(licenseFile);
            StreamReader sr = myFile.OpenText();

            string licHeader = sr.ReadLine();

            if (licHeader == strLicenseHeader)
            {
                ret = true;
            }

            sr.Close();

            return ret;
        }

        /// <summary>
        /// 导入授权许可文件
        /// 直接传送加密串到服务器，由服务器写为许可文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ImportLicense_ItemClick(object sender, ItemClickEventArgs e)
        {
            ImportLicenseFile();
        }

        public void ImportLicense_OnCompleted(object sender, EventArgs e)
        {
//            ImportLicenseFile();
        }

        public void ImportLicenseFile()
        {
            try
            {

            }
            catch (Exception exp)
            {
                MessageBox.Show("Failed to open license file" + ": " + exp.Message);
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
