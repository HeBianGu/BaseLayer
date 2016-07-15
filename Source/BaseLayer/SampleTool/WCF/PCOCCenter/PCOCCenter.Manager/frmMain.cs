using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraNavBar;
using OPT.PCOCCenter.Manager.Views;
using System.Collections;

namespace OPT.PCOCCenter.Manager
{
    public partial class MainForm : RibbonForm
    {
        bool initViews;
        public ServerInfoView serverInfoView = null;
        public OnlineUsersView onlineUsersView = null;
        public LicenseManageView licenseManageView = null;
        public UsersManageView usersManageView = null;
        public List<string> licenseAppList = null;

        public MainForm()
        {
            InitializeComponent();
            
            string strServerInfo = string.Format("服务器：{0}", Program.Server);
            this.siStatus.Caption = strServerInfo;

            string strLoginInfo = string.Format("当前用户：{0}", Program.UserName);
            this.siInfo.Caption = strLoginInfo;            

            this.licenseAppList = new List<string>();
            initViews = false;
            InitViews();            
            this.SizeChanged +=new EventHandler(MainForm_SizeChanged);
            this.splitContainerControl.SplitterMoved += new EventHandler(MainForm_SizeChanged);
        }

        void InitViews()
        {
            serverInfoView = new ServerInfoView();
            this.splitContainerControl.Panel2.Controls.Add(serverInfoView);
            onlineUsersView = new OnlineUsersView();
            this.splitContainerControl.Panel2.Controls.Add(onlineUsersView);
            licenseManageView = new LicenseManageView(this);
            this.splitContainerControl.Panel2.Controls.Add(licenseManageView);
            usersManageView = new UsersManageView(this);
            this.splitContainerControl.Panel2.Controls.Add(usersManageView);

            initViews = true;
        }

        void MainForm_SizeChanged(object sender, EventArgs e)
        {
            Size size = this.Size;
            size.Width -= (this.splitContainerControl.Panel1.Width+20);
            size.Height -= 210;
            serverInfoView.Size = size;
            onlineUsersView.Size = size;
            licenseManageView.Size = size;
            usersManageView.Size = size;
        }

        private void iExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            OPT.PCOCCenter.Client.Client.Logout();
            this.Close();
        }

        private void ServerInfo_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ShowView("ServerInfoView");
        }

        private void OnlineUserList_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ShowView("OnlineUsersView");
        }
        
        private void LicenseManage_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ShowView("LicenseManageView");
        }

        private void UserManage_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ShowView("UsersManageView");
        }

        void ShowView(string name)
        {
            if (!initViews) return;
            if (name == serverInfoView.Name)
                serverInfoView.Visible = true;
            else
                serverInfoView.Visible = false;

            if (name == onlineUsersView.Name)
                onlineUsersView.Visible = true;
            else
                onlineUsersView.Visible = false;

            if (name == licenseManageView.Name)
                licenseManageView.Visible = true;
            else
                licenseManageView.Visible = false;

            if (name == usersManageView.Name)
                usersManageView.Visible = true;
            else
                usersManageView.Visible = false;

            RefreshToolbar();
        }

        void RefreshToolbar()
        {
            RemoveViewPageGroup();

            RibbonPageGroup group = new RibbonPageGroup();
            viewRibbonPage.Groups.Add(group);

            if (serverInfoView.Visible == true)
            {
                BarButtonItem itemRefreshServerInfo = new BarButtonItem();
                itemRefreshServerInfo.Caption = "刷新";
                itemRefreshServerInfo.Name = "RefreshServerInfo";
                itemRefreshServerInfo.RibbonStyle = RibbonItemStyles.Large;
                itemRefreshServerInfo.LargeImageIndex = 9;
                itemRefreshServerInfo.ItemClick += new ItemClickEventHandler(serverInfoView.RefreshServerInfo_ItemClick);
                group.ItemLinks.Add(itemRefreshServerInfo);

                serverInfoView.UpdateServerInfo();
            }
            else if (onlineUsersView.Visible == true)
            {
                BarButtonItem itemRefreshOnlineUsers = new BarButtonItem();
                itemRefreshOnlineUsers.Caption = "在线用户";
                itemRefreshOnlineUsers.Name = "itemRefreshOnlineUsers";
                itemRefreshOnlineUsers.LargeImageIndex = 9;
                itemRefreshOnlineUsers.ItemClick += new ItemClickEventHandler(onlineUsersView.RefreshOnlineUsers_ItemClick);
                group.ItemLinks.Add(itemRefreshOnlineUsers);

                BarButtonItem itemShowAllLogin = new BarButtonItem();
                itemShowAllLogin.Caption = "所有登陆信息";
                itemShowAllLogin.Name = "ShowAllLogin";
                itemShowAllLogin.LargeImageIndex = 11;
                itemShowAllLogin.ItemClick += new ItemClickEventHandler(onlineUsersView.ShowAllLogin_ItemClick);
                group.ItemLinks.Add(itemShowAllLogin);
            }
            else if (licenseManageView.Visible == true)
            {
                BarButtonItem itemImportLicense = new BarButtonItem();
                itemImportLicense.Caption = Utils.Utils.Translate("导入许可");
                itemImportLicense.Name = "importLicense";
                itemImportLicense.LargeImageIndex = 10;
                itemImportLicense.ItemClick += new ItemClickEventHandler(licenseManageView.ImportLicense_ItemClick);
                group.ItemLinks.Add(itemImportLicense);

                //BarButtonItem itemLogoffLicense = new BarButtonItem();
                //itemLogoffLicense.Caption = Utils.Utils.Translate("注销许可授权");
                //itemLogoffLicense.Name = "logoffLicense";
                //itemLogoffLicense.LargeImageIndex = 11;
                //itemLogoffLicense.ItemClick += new ItemClickEventHandler(licenseManageView.LogoffLicense_ItemClick);
                //group.ItemLinks.Add(itemLogoffLicense);

                BarButtonItem itemRefreshLicenseInfos = new BarButtonItem();
                itemRefreshLicenseInfos.Caption = Utils.Utils.Translate("刷新");
                itemRefreshLicenseInfos.Name = "RefreshLicenseInfos";
                itemRefreshLicenseInfos.RibbonStyle = RibbonItemStyles.Large;
                itemRefreshLicenseInfos.LargeImageIndex = 9;
                itemRefreshLicenseInfos.ItemClick += new ItemClickEventHandler(licenseManageView.RefreshLicenseInfos_ItemClick);
                group.ItemLinks.Add(itemRefreshLicenseInfos, true);
            }
            else if (usersManageView.Visible == true)
            {
                BarButtonItem itemAddUser = new BarButtonItem();
                itemAddUser.Caption = "添加用户";
                itemAddUser.Name = "AddUser";
                itemAddUser.ImageIndex = 15;
                itemAddUser.ItemClick += new ItemClickEventHandler(usersManageView.AddUser_ItemClick);
                group.ItemLinks.Add(itemAddUser);
                BarButtonItem itemEditUser = new BarButtonItem();
                itemEditUser.Caption = "编辑用户";
                itemEditUser.Name = "EditUser";
                itemEditUser.ImageIndex = 16;
                itemEditUser.ItemClick += new ItemClickEventHandler(usersManageView.EditUser_ItemClick);
                group.ItemLinks.Add(itemEditUser);
                BarButtonItem itemDeleteUser = new BarButtonItem();
                itemDeleteUser.Caption = "删除用户";
                itemDeleteUser.Name = "DelUser";
                itemDeleteUser.ImageIndex = 17;
                itemDeleteUser.ItemClick += new ItemClickEventHandler(usersManageView.DeleteUser_ItemClick);
                group.ItemLinks.Add(itemDeleteUser);

                BarButtonItem itemUserGroupManage = new BarButtonItem();
                itemUserGroupManage.Caption = "用户组管理";
                itemUserGroupManage.Name = "UserGroupManage";
                itemUserGroupManage.LargeImageIndex = 12;
                itemUserGroupManage.ItemClick += new ItemClickEventHandler(usersManageView.UserGroupManage_ItemClick);
                group.ItemLinks.Add(itemUserGroupManage, true);
            }

            group.Text = "操作";
            group.Name = "操作";

            this.ribbonControl.SelectedPage = this.viewRibbonPage;
        }

        /// <summary>
        /// 清空view工具条按钮
        /// </summary>
        public void RemoveViewPageGroup()
        {
            foreach (RibbonPageGroup group in viewRibbonPage.Groups)
            {
                foreach (BarItemLink itemLink in group.ItemLinks)
                {
                    if (itemLink.OwnerItem != null)
                        itemLink.OwnerItem.Dispose();
                }
            }
            viewRibbonPage.Groups.Clear();
        }
    }

}