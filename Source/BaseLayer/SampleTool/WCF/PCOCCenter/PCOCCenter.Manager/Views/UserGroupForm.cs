using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraVerticalGrid.Events;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;

namespace OPT.PCOCCenter.Manager.Views
{
    public class UserGroup
    {
        public int nStatus; // 0--exist; 1--insert; -1--delete; 2--update;
        public string UID;
        public string GroupName;
        public string IPRanges;
        public string AllowApps;
        public string Roles;
    }

    public partial class UserGroupForm : Form
    {
        MainForm mainForm = null;
        DataTable dtUserGroups { get; set; }
        DataTable dtRoles { get; set; }

        List<UserGroup> userGroupList = new List<UserGroup>();

        RepositoryItemTextEdit textEdit = null;
        RepositoryItemCheckedComboBoxEdit comboBoxEdit = null;

        public UserGroupForm(MainForm mainForm, DataTable dtUserGroups, DataTable dtRoles)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            this.vGridControl.OptionsView.MinRowAutoHeight = 22;
            this.vGridControl.RowHeaderWidth = 120;
            this.vGridControl.RecordWidth = this.vGridControl.Width-this.vGridControl.RowHeaderWidth-1;
            this.vGridControl.FocusedRowChanged+=new DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventHandler(vGridControl_FocusedRowChanged);

            comboBoxEdit = new RepositoryItemCheckedComboBoxEdit();
            comboBoxEdit.EditValueChanged+=new EventHandler(comboBoxEdit_EditValueChanged);

            textEdit = new RepositoryItemTextEdit();
            textEdit.EditValueChanged+=new EventHandler(textEdit_EditValueChanged);

            this.dtUserGroups = dtUserGroups;
            this.dtRoles = dtRoles;
        }

        private void UserGroupForm_Load(object sender, EventArgs e)
        {
            listBoxUserGroups.Items.Clear();
            foreach (DataRow dtRow in dtUserGroups.Rows)
            {
                string groupUID = dtRow["ID"].ToString();
                string groupName = dtRow["GroupName"].ToString();

                UserGroup userGroup = new UserGroup();
                userGroup.nStatus = 0;
                userGroup.UID = groupUID;
                userGroup.GroupName = groupName;
                userGroupList.Add(userGroup);
                listBoxUserGroups.Items.Add(groupName);
            }

        }

        UserGroup GetUserGroupFromGroupName(string GroupName)
        {
            foreach (UserGroup userGroup in userGroupList)
            {
                if (userGroup.GroupName == GroupName)
                    return userGroup;
            }

            return null;
        }

        public void textEdit_EditValueChanged(object sender, EventArgs e)
        {
            UserGroup userGroup = GetUserGroupFromGroupName(listBoxUserGroups.SelectedItem.ToString());
            if (userGroup == null) return;

            int row = this.vGridControl.FocusedRow.Index;

           if (row == 1)
            {
                if (userGroup.nStatus == 0) userGroup.nStatus = 2;
                // 用户组名
                userGroup.GroupName = (sender as TextEdit).Text;
                if (listBoxUserGroups.SelectedIndex >= 0)
                    listBoxUserGroups.Items[listBoxUserGroups.SelectedIndex] = userGroup.GroupName;
            }
            else if (row == 2)
            {
                // IP范围
                if (userGroup.nStatus == 0) userGroup.nStatus = 2;
                userGroup.IPRanges = (sender as TextEdit).Text;
            }
        }

        public void comboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            UserGroup userGroup = GetUserGroupFromGroupName(listBoxUserGroups.SelectedItem.ToString());
            int row = this.vGridControl.FocusedRow.Index;
            
            if (row == 3)
            {
                if (userGroup.nStatus==0) userGroup.nStatus = 2;
                userGroup.AllowApps = "";
                // 模块列表
                foreach (CheckedListBoxItem item in comboBoxEdit.GetItems())
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        userGroup.AllowApps += item.Value.ToString();
                        userGroup.AllowApps += ", ";
                    }
                }
                userGroup.AllowApps = userGroup.AllowApps.TrimEnd(' ');
                userGroup.AllowApps = userGroup.AllowApps.TrimEnd(',');
            }
            else if (row == 4)
            {
                // 权限列表
                if (userGroup.nStatus == 0) userGroup.nStatus = 2;
                userGroup.Roles = "";

                foreach (CheckedListBoxItem item in comboBoxEdit.GetItems())
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        userGroup.Roles += item.Description.ToString();
                        userGroup.Roles += ", ";
                    }
                }
                userGroup.Roles = userGroup.Roles.TrimEnd(' ');
                userGroup.Roles = userGroup.Roles.TrimEnd(',');
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (UserGroup userGroup in userGroupList)
            {
                if (userGroup.nStatus == 0)
                    continue;
                else if (userGroup.nStatus == -1)
                {
                    // 删除用户组
                    OPT.PCOCCenter.Client.Client.DeleteUserGroup(userGroup.UID);
                }
                else if (userGroup.nStatus == 1)
                {
                    // 添加用户组
                    OPT.PCOCCenter.Client.Client.AddUserGroup(userGroup.GroupName, userGroup.IPRanges, userGroup.AllowApps, userGroup.Roles);
                }
                else if (userGroup.nStatus == 2)
                {
                    // 更新用户组
                    OPT.PCOCCenter.Client.Client.UpdateUserGroup(userGroup.UID, userGroup.GroupName, userGroup.IPRanges, userGroup.AllowApps, userGroup.Roles);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnAddUserGroup_Click(object sender, EventArgs e)
        {
            string groupName = "新建用户组";

            foreach (string userGroup in listBoxUserGroups.Items)
            {
                if (userGroup == groupName)
                {
                    MessageBox.Show("用户组已经存在，请确认后再试！", "提示");
                    return;
                }
            }

            UserGroup userGroupInfo = new UserGroup();
            userGroupInfo.nStatus = 1;
            userGroupInfo.UID = null;
            userGroupInfo.GroupName = groupName;
            userGroupList.Add(userGroupInfo);

            int index = listBoxUserGroups.Items.Add(groupName);
            listBoxUserGroups.SetSelected(index, true);
        }

        private void btnDeleteUserGroup_Click(object sender, EventArgs e)
        {
            int index = listBoxUserGroups.SelectedIndex;
            if (index >= 0)
            {
                string msg = string.Format("确定删除用户组[{0}]?", listBoxUserGroups.SelectedItem.ToString());
                if (MessageBox.Show(msg, "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    UserGroup userGroup = GetUserGroupFromGroupName(listBoxUserGroups.SelectedItem.ToString());
                    userGroup.nStatus = -1;
                    listBoxUserGroups.Items.RemoveAt(index);
                }
            }
            else
            {
                MessageBox.Show("先选择用户组", "提示");
            }
        }

        public void vGridControl_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.Row!=null && e.Row.Index == 0)
                this.vGridControl.FocusNext();

            if (this.vGridControl.FocusedRow == null)
                return;

            int row = this.vGridControl.FocusedRow.Index;

            UserGroup userGroup = null;

            if (listBoxUserGroups.SelectedItem!=null)
                userGroup = GetUserGroupFromGroupName(listBoxUserGroups.SelectedItem.ToString());

            if (row == 3)
            {
                // 模块列表
                this.comboBoxEdit.Items.Clear();

                foreach (string appName in mainForm.licenseAppList)
                {
                    int index = this.comboBoxEdit.Items.Add(appName);
                    if (userGroup!=null)
                    {
                        if (userGroup.AllowApps != null && userGroup.AllowApps.IndexOf(appName) >= 0)
                            this.comboBoxEdit.Items[index].CheckState = CheckState.Checked;
                        else
                            this.comboBoxEdit.Items[index].CheckState = CheckState.Unchecked;
                    }
                }
            }
            else if (row == 4)
            {
                // 权限列表
                this.comboBoxEdit.Items.Clear();

                foreach (DataRow dtRow in dtRoles.Rows)
                {
                    string roleItem = dtRow["RoleItem"].ToString();
                    string roleName = dtRow["Memo"].ToString();
                    int index = this.comboBoxEdit.Items.Add(roleName);
                    this.comboBoxEdit.Items[index].Description = roleName;
                    if (userGroup != null)
                    {
                        if (userGroup.Roles!=null && userGroup.Roles.IndexOf(roleName) >= 0)
                            this.comboBoxEdit.Items[index].CheckState = CheckState.Checked;
                        else
                            this.comboBoxEdit.Items[index].CheckState = CheckState.Unchecked;
                    }
                }
            }
        }

        /// <summary>
        /// 切换用户组，刷新组信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxUserGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxUserGroups.SelectedItem == null) return;

            List<string> colNames = new List<string>();
            colNames.Add("属性");
            colNames.Add("值");

            List<string> PropertyNames = new List<string>();
            PropertyNames.Add("组名称");
            PropertyNames.Add("IP范围");
            PropertyNames.Add("模块列表");
            PropertyNames.Add("检查权限");


            List<string> PropertyValues = new List<string>();
            foreach (DataRow dtRow in dtUserGroups.Rows)
            {
                string groupName = dtRow["GroupName"].ToString();
                if (groupName == listBoxUserGroups.SelectedItem.ToString())
                {
                    PropertyValues.Add(dtRow["GroupName"].ToString());
                    PropertyValues.Add(dtRow["IPRanges"].ToString());
                    PropertyValues.Add(dtRow["AllowApps"].ToString());
                    PropertyValues.Add(dtRow["Roles"].ToString());
                    break;
                }
            }

            UserGroup userGroup = GetUserGroupFromGroupName(listBoxUserGroups.SelectedItem.ToString());

            //// 设置组属性数据
            VGridRows rows = this.vGridControl.Rows;
            if (rows != null)
            {
                rows.Clear();
                DevExpress.XtraVerticalGrid.Rows.EditorRow rowTitle = new DevExpress.XtraVerticalGrid.Rows.EditorRow("PropertyTitle");
                rows.Add(rowTitle);
                rowTitle.Properties.Caption = "属性";
                rowTitle.Properties.Value = "值";

                for (int i = 0; i < PropertyNames.Count; i++)
                {
                    DevExpress.XtraVerticalGrid.Rows.EditorRow row = new DevExpress.XtraVerticalGrid.Rows.EditorRow(PropertyNames[i]);
                    rows.Add(row);
                    row.Properties.Caption = PropertyNames[i];
                    if(i==2||i==3)
                        row.Properties.RowEdit = this.comboBoxEdit;
                    else
                        row.Properties.RowEdit = this.textEdit;

                    if (i < PropertyValues.Count)
                    {
                        row.Properties.Value = PropertyValues[i];

                        switch (i)
                        {
                            case 1:
                                if (userGroup.nStatus == 2)
                                    row.Properties.Value = userGroup.IPRanges;
                                else
                                    userGroup.IPRanges = PropertyValues[i];
                                break;
                            case 2:
                                if (userGroup.nStatus == 2)
                                    row.Properties.Value = userGroup.AllowApps;
                                else
                                    userGroup.AllowApps = PropertyValues[i];
                                break;
                            case 3:
                                if (userGroup.nStatus == 2)
                                    row.Properties.Value = userGroup.Roles;
                                else
                                    userGroup.Roles = PropertyValues[i];
                                break;
                        }
                    }
                }
            }

        }
   
    }
}
