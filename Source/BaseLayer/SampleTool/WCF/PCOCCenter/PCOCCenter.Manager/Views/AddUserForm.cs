using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace OPT.PCOCCenter.Manager.Views
{
    public partial class AddUserForm : Form
    {
        List<string> UserNameList = new List<string>(); // 已经存在的用户名
        bool bEditUser = false;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserGroupID { get; set; }
        public string Roles { get; set; }
        public string Telephone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string Address { get; set; }
        public string Memo { get; set; }
        public DataTable dtUserGroups { get; set; }


        public AddUserForm(DataTable dtUsers, DataTable dtUserGroups)
        {
            InitializeComponent();
            this.dtUserGroups = dtUserGroups;

            if (dtUsers != null)
            {
                foreach (DataRow dtRow in dtUsers.Rows)
                {
                    string userName = dtRow["UserName"].ToString();
                    UserNameList.Add(userName);
                }
            }
            comboUserGroup.EditValueChanged+=new EventHandler(comboUserGroup_EditValueChanged);
            listRoles.ItemChecking+=new ItemCheckingEventHandler(listRoles_ItemChecking);
        }

        public void listRoles_ItemChecking(object sender, ItemCheckingEventArgs e)
        {
            if (listRoles.Items[e.Index].Value.ToString() == "CheckUserName" && e.NewValue == CheckState.Unchecked)
            {
                e.NewValue = CheckState.Checked;
            }
        }

        public void comboUserGroup_EditValueChanged(object sender, EventArgs e)
        {
            // 从用户组中，更新用户权限
            foreach (CheckedListBoxItem groupItem in comboUserGroup.Properties.Items)
            {
                if(groupItem.CheckState == CheckState.Checked)
                {
                    // 
                    foreach (DataRow dtRow in dtUserGroups.Rows)
                    {
                        string groupUID = dtRow["ID"].ToString();
                        string groupName = dtRow["GroupName"].ToString();
                        string groupRoles = dtRow["Roles"].ToString();

                        if (groupName == groupItem.Description)
                        {
                            foreach (CheckedListBoxItem item in listRoles.Items)
                            {
                                string role = item.Description;
                                if (groupRoles.IndexOf(role) >= 0)
                                {
                                    item.CheckState = CheckState.Checked;
                                }
                            }
                        }
                    }

                }
            }
        }

        public void UpdateDatatoUI()
        {
            bEditUser = true;
            editUserName.Text = UserName;
            editPassword.Text = Password;
            editRePassword.Text = Password;
            comboUserGroup.SetEditValue(UserGroupID);

            foreach(CheckedListBoxItem item in listRoles.Items)
            {
                string role = item.Description;
                if(Roles.IndexOf(role)>=0)
                {
                    item.CheckState = CheckState.Checked;
                }
                else
                {
                    item.CheckState = CheckState.Unchecked;
                }
            }
            editTelephone.Text = Telephone;
            editMobilePhone.Text = MobilePhone;
            editEmail.Text = Email;
            editQQ.Text = QQ;
            editAddress.Text = Address;
            editMemo.Text = Memo;
        }

        public void InitUserGroup()
        {
            comboUserGroup.Properties.Items.Clear();
            foreach (DataRow dtRow in dtUserGroups.Rows)
            {
                string groupUID = dtRow["ID"].ToString();
                string groupName = dtRow["GroupName"].ToString();
                int index = comboUserGroup.Properties.Items.Add(groupUID);
                comboUserGroup.Properties.Items[index].Description = groupName;
            }
            if (comboUserGroup.Properties.Items.Count > 0 && UserGroupID==null)
                comboUserGroup.Properties.Items[0].CheckState = CheckState.Checked;
        }

        public void InitRoles(DataTable dtRoles)
        {
            listRoles.Items.Clear();
            foreach (DataRow dtRow in dtRoles.Rows)
            {
                string roleItem = dtRow["RoleItem"].ToString();
                string roleName = dtRow["Memo"].ToString();
                int index = listRoles.Items.Add(roleItem);
                listRoles.Items[index].Description = roleName;
                if (roleItem == "CheckUserName")
                {
                    listRoles.Items[index].CheckState = CheckState.Checked;
                    listRoles.Items[index].Enabled = false;
                }
                listRoles.SelectionMode = SelectionMode.MultiSimple;                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool VerifyUserInfo()
        {
            if (UserName == null || UserName == string.Empty || UserName == "")
            {
                MessageBox.Show("请输入用户名！");
                return false;
            }

            if (UserGroupID == null || UserGroupID == string.Empty || UserGroupID == "")
            {
                MessageBox.Show("请选择用户组！");
                return false;
            }

            if (Password != editRePassword.Text)
            {
                MessageBox.Show("两次输入密码不一样，请重新输入！");
                return false;
            }

            if (bEditUser == false)
            {
                foreach (string userName in UserNameList)
                {
                    if (UserName == userName)
                    {
                        MessageBox.Show("用户名已经存在，请重新输入！");
                        return false;
                    }
                }
            }

            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UserName = editUserName.Text.ToString();
            Password = editPassword.Text.ToString();
            UserGroupID = comboUserGroup.EditValue.ToString();

            Roles = "";
            foreach(CheckedListBoxItem roleItem in listRoles.Items)
            {
                if (roleItem.CheckState == CheckState.Checked)
                {
                    Roles += roleItem.Description;
                    Roles += ", ";
                }
            }
            Roles = Roles.TrimEnd(' ');
            Roles = Roles.TrimEnd(',');
            Telephone = editTelephone.Text.ToString();
            MobilePhone = editMobilePhone.Text.ToString();
            Email = editEmail.Text.ToString();
            QQ = editQQ.Text.ToString();
            Address = editAddress.Text.ToString();
            Memo = editMemo.Text.ToString();

            if (VerifyUserInfo() == false)   return;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
