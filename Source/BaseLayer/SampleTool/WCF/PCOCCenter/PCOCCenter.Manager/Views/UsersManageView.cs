using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace OPT.PCOCCenter.Manager.Views
{
    public partial class UsersManageView : UserControl
    {
        MainForm mainForm = null;
        public DataTable dtRoles { get; set; }
        public DataTable dtUserGroups { get; set; }
        public DataTable dtUsers { get; set; }

        public UsersManageView(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            this.SizeChanged += new EventHandler(CustomersView_SizeChanged);
            this.gridControl.MouseDown +=new MouseEventHandler(gridControl_MouseDown);

            InitUsersManageGrid();
        }

        private void CustomersView_SizeChanged(object sender, EventArgs e)
        {
            Size size = this.Size;
            size.Width -= (this.gridControl.Location.X + 7);
            size.Height -= (this.gridControl.Location.Y + 5);
            this.gridControl.Size = size;
        }

        void InitUsersManageGrid()
        {
            try
            {
                dtRoles = OPT.PCOCCenter.Client.Client.GetRoles();
                dtUserGroups = OPT.PCOCCenter.Client.Client.GetUserGroups();
                dtUsers = OPT.PCOCCenter.Client.Client.GetUsers();
                gridControl.DataSource = dtUsers;
                gridView.OptionsBehavior.ReadOnly = true;

                gridView.Columns["ID"].Caption = "编号"; gridView.Columns["ID"].Visible = false;
                gridView.Columns["UserName"].Caption = "用户名";
                gridView.Columns["Password"].Caption = "密码"; gridView.Columns["Password"].Visible = false;
                gridView.Columns["UserGroup"].Caption = "用户组";
                gridView.Columns["Roles"].Caption = "校验权限";
                gridView.Columns["Telephone"].Caption = "电话";
                gridView.Columns["MobilePhone"].Caption = "手机";
                gridView.Columns["Email"].Caption = "邮箱";
                gridView.Columns["QQ"].Caption = "QQ号码";
                gridView.Columns["Address"].Caption = "地址";
                gridView.Columns["Memo"].Caption = "备注";
            }
            catch (System.Exception ex)
            {
            }
        }

        private void gridControl_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = gridView.CalcHitInfo(e.Location);

            if (e.Button == MouseButtons.Left)
            {
                switch (info.HitTest)
                {
                    case DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.ColumnButton:      //行列交叉处
                        break;
                    case DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowIndicator:      //行头
                        if ((ModifierKeys & Keys.Control) != Keys.Control) //Ctrl键
                        {
                        }
                        break;
                    case DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.Column:      //列头
                        break;
                    case DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell:      //单元格
                        if ((ModifierKeys & Keys.Control) != Keys.Control) //Ctrl键
                        {
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                //右键菜单
                switch (info.HitTest)
                {
                    case DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.ColumnButton:      //行列交叉处
                    case DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowIndicator:      //行头
                    case DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell:           //单元格
                        this.popupMenu_Grid.ShowPopup(MousePosition);
                        break;
                    default:
                        break;
                }
            }
        }

        void AddUser()
        {
            AddUserForm userForm = new AddUserForm(dtUsers, dtUserGroups);
            userForm.InitUserGroup();
            userForm.InitRoles(dtRoles);
            if (userForm.ShowDialog() == DialogResult.OK)
            {
                // 添加用户信息到服务器
                string userInfo = string.Empty;
                userInfo = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};", 
                    userForm.UserName, userForm.Password, userForm.UserGroupID, userForm.Roles,
                    userForm.Telephone, userForm.MobilePhone, userForm.Email, userForm.QQ, userForm.Address, userForm.Memo);

                int nRet = OPT.PCOCCenter.Client.Client.AddUser(userInfo);
                if(nRet == 1) InitUsersManageGrid();
            }
        }

        void DeleteUser()
        {
            int[] rows = this.gridView.GetSelectedRows();
            DevExpress.XtraGrid.Columns.GridColumn columnID = this.gridView.Columns["ID"];
            DevExpress.XtraGrid.Columns.GridColumn columnUserName = this.gridView.Columns["UserName"];
            string userIDs = string.Empty;
            string userNames = string.Empty;
            foreach (int row in rows)
            {
                string userID = this.gridView.GetRowCellValue(row, columnID).ToString();
                string userName = this.gridView.GetRowCellValue(row, columnUserName).ToString();

                userIDs += string.Format("'{0}',",userID);
                userNames += string.Format("'{0}',", userName);
            }

            userIDs = userIDs.TrimEnd(',');
            userNames = userNames.TrimEnd(',');

            string message = string.Format("确认删除用户[{0}]", userNames);
            if (MessageBox.Show(message, "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int nRet = OPT.PCOCCenter.Client.Client.DeleteUser(userIDs);
                if (nRet == 1) InitUsersManageGrid();
            }           
        }

        void EditUser()
        {
            int[] rows = this.gridView.GetSelectedRows();
            if(rows.Count()<=0) return;

            DevExpress.XtraGrid.Columns.GridColumn columnID = this.gridView.Columns["ID"];
            DevExpress.XtraGrid.Columns.GridColumn columnUserName = this.gridView.Columns["UserName"];
            DevExpress.XtraGrid.Columns.GridColumn columnPassword = this.gridView.Columns["Password"];
            DevExpress.XtraGrid.Columns.GridColumn columnUserGroup = this.gridView.Columns["UserGroup"];
            DevExpress.XtraGrid.Columns.GridColumn columnRoles = this.gridView.Columns["Roles"];
            DevExpress.XtraGrid.Columns.GridColumn columnTelephone = this.gridView.Columns["Telephone"];
            DevExpress.XtraGrid.Columns.GridColumn columnMobilePhone = this.gridView.Columns["MobilePhone"];
            DevExpress.XtraGrid.Columns.GridColumn columnEmail = this.gridView.Columns["Email"];
            DevExpress.XtraGrid.Columns.GridColumn columnQQ = this.gridView.Columns["QQ"];
            DevExpress.XtraGrid.Columns.GridColumn columnAddress = this.gridView.Columns["Address"];
            DevExpress.XtraGrid.Columns.GridColumn columnMemo = this.gridView.Columns["Memo"];

            int row = rows[0];

            AddUserForm userForm = new AddUserForm(dtUsers, dtUserGroups);
            userForm.Text = "修改用户信息";
            string userID = this.gridView.GetRowCellValue(row, columnID).ToString();
            userForm.UserName = this.gridView.GetRowCellValue(row, columnUserName).ToString();
            userForm.Password = this.gridView.GetRowCellValue(row, columnPassword).ToString();
            userForm.UserGroupID = GetUserGroupIDFromGroupName(this.gridView.GetRowCellValue(row, columnUserGroup).ToString());
            userForm.Roles = this.gridView.GetRowCellValue(row, columnRoles).ToString();
            userForm.Telephone = this.gridView.GetRowCellValue(row, columnTelephone).ToString();
            userForm.MobilePhone = this.gridView.GetRowCellValue(row, columnMobilePhone).ToString();
            userForm.Email = this.gridView.GetRowCellValue(row, columnEmail).ToString();
            userForm.QQ = this.gridView.GetRowCellValue(row, columnQQ).ToString();
            userForm.Address = this.gridView.GetRowCellValue(row, columnAddress).ToString();
            userForm.Memo = this.gridView.GetRowCellValue(row, columnMemo).ToString();

            userForm.InitUserGroup();
            userForm.InitRoles(dtRoles);
            userForm.UpdateDatatoUI();

            if (userForm.ShowDialog() == DialogResult.OK)
            {
                // 添加用户信息到服务器
                string userInfo = string.Empty;
                userInfo = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};",
                    userForm.UserName, userForm.Password, userForm.UserGroupID, userForm.Roles,
                    userForm.Telephone, userForm.MobilePhone, userForm.Email, userForm.QQ, userForm.Address, userForm.Memo);

                int nRet = OPT.PCOCCenter.Client.Client.EditUser(userID, userInfo);
                if (nRet == 1) InitUsersManageGrid();
            }
        }

        string GetUserGroupIDFromGroupName(string rGroupName)
        {
            foreach (DataRow dtRow in dtUserGroups.Rows)
            {
                string groupUID = dtRow["ID"].ToString();
                string groupName = dtRow["GroupName"].ToString();

                if (rGroupName == groupName)
                {
                    return groupUID;
                }
            }

            return "";
        }

        void UserGroupManage()
        {
            UserGroupForm userGroupForm = new UserGroupForm(mainForm, dtUserGroups, dtRoles);
            if (userGroupForm.ShowDialog() == DialogResult.OK)
            {
                InitUsersManageGrid();
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddUser();
        }

        public void DeleteUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteUser();
        }

        public void EditUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditUser();
        }

        public void UserGroupManage_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserGroupManage();
        }

    }
}
