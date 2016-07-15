using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OPT.PEOfficeCenter.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using DevExpress.Utils;

namespace OPT.PEOfficeCenter.LicenseManager.Views
{
    public partial class ModuleManageView : UserControl
    {
        public ModuleManageView()
        {
            InitializeComponent();
            this.SizeChanged += new EventHandler(CustomersView_SizeChanged);
            InitOnlineUsersGrid();
        }

        private void CustomersView_SizeChanged(object sender, EventArgs e)
        {
            Size size = this.Size;
            size.Width -= (this.gridControl.Location.X + 7);
            size.Height -= (this.gridControl.Location.Y + 5);
            this.gridControl.Size = size;
        }

        public void UpdateOnlineUsers(bool bAllLogin=false)
        {
            DataTable dtOnlineUsers = null;

            if (dtOnlineUsers != null)
            {
                gridControl.DataSource = dtOnlineUsers;
                gridView.OptionsBehavior.ReadOnly = true;
                DevExpress.XtraGrid.Columns.GridColumn colLoginTime = gridView.Columns["LoginTime"];
                colLoginTime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                DevExpress.XtraGrid.Columns.GridColumn colLogoutTime = gridView.Columns["LogoutTime"];
                colLogoutTime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

                gridView.SortInfo.ClearSorting();
                gridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(colLoginTime, DevExpress.Data.ColumnSortOrder.Descending)});

                gridView.Columns["ID"].Caption = "编号"; gridView.Columns["ID"].Visible = false;
                gridView.Columns["UserName"].Caption = "用户名";
                gridView.Columns["UserIP"].Caption = "用户IP";
                gridView.Columns["LoginTime"].Caption = "登入时间";
                gridView.Columns["AppName"].Caption = "模块名";
                gridView.Columns["ModuleName"].Caption = "功能名"; gridView.Columns["ModuleName"].Visible = false;
                gridView.Columns["ModuleVersion"].Caption = "功能版本"; gridView.Columns["ModuleVersion"].Visible = false;
                gridView.Columns["LogoutTime"].Caption = "登出时间";
                gridView.Columns["Status"].Caption = "状态信息";
            }
        }

        void InitOnlineUsersGrid()
        {
            UpdateOnlineUsers();
        }

        /// <summary>
        /// 刷新在线用户列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefreshOnlineUsers_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateOnlineUsers();
        }

        /// <summary>
        /// 刷新所有登录用户列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShowAllLogin_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool bAllLogin = true;
            UpdateOnlineUsers(bAllLogin);
        }
    }
}
