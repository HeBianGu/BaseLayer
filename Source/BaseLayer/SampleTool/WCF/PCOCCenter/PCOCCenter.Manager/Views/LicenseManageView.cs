using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OPT.PCOCCenter.Service;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.IO;

namespace OPT.PCOCCenter.Manager.Views
{
    public partial class LicenseManageView : UserControl
    {
        MainForm mainForm = null;
        List<string> colNames = new List<string>();

        public LicenseManageView(MainForm mainForm)
        {
            colNames.Add(Utils.Utils.Translate("授权文件"));
            colNames.Add(Utils.Utils.Translate("应用程序"));
            colNames.Add(Utils.Utils.Translate("授权许可数"));
            colNames.Add(Utils.Utils.Translate("占用许可数"));
            colNames.Add(Utils.Utils.Translate("截止日期"));
            colNames.Add(Utils.Utils.Translate("许可文件ID"));
            this.mainForm = mainForm;
            InitializeComponent();
            this.SizeChanged += new EventHandler(LicenseFilesView_SizeChanged);

            List<LicenseInfo> licenseInfos = OPT.PCOCCenter.Client.Client.GetLicenseInfos();
            InitGrid(licenseInfos);
        }

        private void LicenseFilesView_SizeChanged(object sender, EventArgs e)
        {
            Size size = this.Size;
            size.Width -= (this.gridControl.Location.X + 7);
            size.Height -= (this.gridControl.Location.Y + 5);
            this.gridControl.Size = size;
        }

        string InitGrid(List<LicenseInfo> licenseInfos)
        {
            mainForm.licenseAppList.Clear();

            // 设置表格数据
            DataTable dt = new DataTable();            

            for (int i = 0; i < colNames.Count; i++ )
                dt.Columns.Add(colNames[i]);

            string errorInfo = "";
            for (int i = 0; i < licenseInfos.Count; i++)
            {
                LicenseInfo oLicenseInfo = licenseInfos[i];
                if (oLicenseInfo.IsUsed.ToLower() != "true") continue;

                if (string.IsNullOrEmpty(oLicenseInfo.ErrorInfo))
                {
                    for (int mIndex = 0; mIndex < oLicenseInfo.ModuleInfos.Count; mIndex++)
                    {
                        string appName = oLicenseInfo.ModuleInfos[mIndex].AppName;

                        DataRow dtRow = dt.NewRow();
                        string LicenseFile = string.Format("{0} -- [{1}]", i + 1, oLicenseInfo.LicenseType);
                        dtRow[colNames[0]] = LicenseFile;
                        dtRow[colNames[1]] = appName;
                        dtRow[colNames[2]] = oLicenseInfo.ModuleInfos[mIndex].LicenseCount;
                        dtRow[colNames[3]] = oLicenseInfo.ModuleInfos[mIndex].LicenseUsed;
                        dtRow[colNames[4]] = oLicenseInfo.ModuleInfos[mIndex].ExpiryDate;
                        dtRow[colNames[5]] = oLicenseInfo.uuid;

                        dt.Rows.Add(dtRow);
                        if (mainForm.licenseAppList.IndexOf(appName) >= 0) continue;
                        mainForm.licenseAppList.Add(appName);
                    }
                }
                else
                {
                    errorInfo = oLicenseInfo.ErrorInfo;
                }
            }

            gridControl.DataSource = dt;
            gridView.Columns[colNames[0]].GroupIndex = 0;
            gridView.Columns[colNames[5]].Visible = false;
            gridView.ExpandAllGroups();

            int j = 0;

            return errorInfo;
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

        void RefreshLicenseInfos()
        {
            List<LicenseInfo> licenseInfos = OPT.PCOCCenter.Client.Client.GetLicenseInfos();
            InitGrid(licenseInfos);
        }

        /// <summary>
        /// 刷新授权许可信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefreshLicenseInfos_ItemClick(object sender, ItemClickEventArgs e)
        {
            RefreshLicenseInfos();
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

        /// <summary>
        /// 注销授权许可文件
        /// 注销许可文件后返回注销信息码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LogoffLicense_ItemClick(object sender, ItemClickEventArgs e)
        {
            LogoffLicenseFile();
        }

        public void LogoffLicenseFile()
        {
            /*
            string licenseFileID = string.Empty;

            //
            int row = gridView.FocusedRowHandle;
            if (row < 0)
            {
                MessageBox.Show(Utils.Utils.Translate("请先选择一个许可文件，然后注销！"));
                return;
            }

            licenseFileID = gridView.GetRowCellValue(row, colNames[5]).ToString();
            string licFile = gridView.GetRowCellValue(row, colNames[0]).ToString();

            // 确认注销
            if(MessageBox.Show(string.Format("{0} [{1}]?\n\n {2}", Utils.Utils.Translate("注销许可"), licFile, Utils.Utils.Translate("注：许可被注销后不能再次导入！")), Utils.Utils.Translate("提示"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // 删除许可
                // 返回许可信息及主机信息
                string logoffCode = OPT.PCOCCenter.Client.Client.LogoffLicenseFile(licenseFileID);
                RefreshLicenseInfos();

                LogoffLicenseForm logoffForm = new LogoffLicenseForm(mainForm, logoffCode);
                logoffForm.Show();
            }*/
        }

        public void ImportLicenseFile()
        {
            try
            {
                string licenseFile = string.Empty;
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Filter = Utils.Utils.Translate("所有支持的格式(*.lic)|*.lic|所有文件(*.*)|*.*");

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        licenseFile = dlg.FileName;

                        // 此处需要校验是否为许可文件
                        if (CheckLicenseFile(licenseFile) == false)
                        {
                            string strMsg = string.Format(Utils.Utils.Translate("{0}不是许可文件，请确认后再重试！"), licenseFile);
                            MessageBox.Show(strMsg, Utils.Utils.Translate("提示"));
                            return;
                        }

                        string remoteAddress = OPT.PCOCCenter.Client.Client.RemoteFileTransferAddress;
                        string licServerPath = OPT.PCOCCenter.Client.FileTransfer.UploadFile(remoteAddress, licenseFile, "Licenses");

                        if (licServerPath != "")
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            // 上传完成,通知许可服务器更新许可信息，并传回到许可管理器刷新界面
                            OPT.PCOCCenter.Client.Client.AddLicenseToConfig(licServerPath);
                            List<LicenseInfo> licenseInfos = OPT.PCOCCenter.Client.Client.ReloadLicenseFiles(this.ParentForm);
                            if (licenseInfos != null && licenseInfos.Count>0)
                            {
                                string errorInfo = InitGrid(licenseInfos);

                                if (string.IsNullOrEmpty(errorInfo) == true)
                                    MessageBox.Show(Utils.Utils.Translate("许可文件导入成功！"));
                                else
                                    MessageBox.Show(errorInfo);
                            }
                            else
                            {
                                MessageBox.Show(OPT.PCOCCenter.Client.Client.ErrorInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Failed to open license file" + ": " + exp.Message);
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
