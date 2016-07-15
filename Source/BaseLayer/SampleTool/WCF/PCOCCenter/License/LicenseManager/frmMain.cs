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
using OPT.PEOfficeCenter.LicenseManager.Views;
using System.Collections;

namespace OPT.PEOfficeCenter.LicenseManager
{
    public partial class MainForm : RibbonForm
    {
        bool initViews;
        AskforLicenseView AskforLicenseView = null;
        LicenseListView LicenseListView = null;
        ModuleManageView ModuleManageView = null;
        CustomerManageView CustomerManageView = null;
        public List<string> licenseAppList = null;

        public MainForm()
        {
            InitializeComponent();
            
            this.siStatus.Caption = "";

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
            AskforLicenseView = new AskforLicenseView();
            this.splitContainerControl.Panel2.Controls.Add(AskforLicenseView);
            LicenseListView = new LicenseListView(this);
            this.splitContainerControl.Panel2.Controls.Add(LicenseListView);
            ModuleManageView = new ModuleManageView();
            this.splitContainerControl.Panel2.Controls.Add(ModuleManageView);
            CustomerManageView = new CustomerManageView(this);
            this.splitContainerControl.Panel2.Controls.Add(CustomerManageView);

            initViews = true;
        }

        void MainForm_SizeChanged(object sender, EventArgs e)
        {
            Size size = this.Size;
            size.Width -= (this.splitContainerControl.Panel1.Width+20);
            size.Height -= 210;
            AskforLicenseView.Size = size;
            LicenseListView.Size = size;
            ModuleManageView.Size = size;
            CustomerManageView.Size = size;
        }

        private void iExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void AskforLicense_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ShowView("AskforLicenseView");
        }
        
        private void LicenseList_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ShowView("LicenseListView");
        }

        private void OnlineUserList_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ShowView("ModuleManageView");
        }

        private void UserManage_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ShowView("CustomerManageView");
        }

        void ShowView(string name)
        {
            if (!initViews) return;
            if (name == AskforLicenseView.Name)
                AskforLicenseView.Visible = true;
            else
                AskforLicenseView.Visible = false;

            if (name == LicenseListView.Name)
                LicenseListView.Visible = true;
            else
                LicenseListView.Visible = false;

            if (name == ModuleManageView.Name)
                ModuleManageView.Visible = true;
            else
                ModuleManageView.Visible = false;

            if (name == CustomerManageView.Name)
                CustomerManageView.Visible = true;
            else
                CustomerManageView.Visible = false;

            RefreshToolbar();
        }

        void RefreshToolbar()
        {
            RemoveViewPageGroup();

            RibbonPageGroup group = new RibbonPageGroup();
            viewRibbonPage.Groups.Add(group);

            if (AskforLicenseView.Visible == true)
            {
                BarButtonItem itemRefreshAskforLicense = new BarButtonItem();
                itemRefreshAskforLicense.Caption = "刷新";
                itemRefreshAskforLicense.Name = "RefreshAskforLicense";
                itemRefreshAskforLicense.RibbonStyle = RibbonItemStyles.Large;
                itemRefreshAskforLicense.LargeImageIndex = 9;
                itemRefreshAskforLicense.ItemClick += new ItemClickEventHandler(AskforLicenseView.RefreshAskforLicense_ItemClick);
                group.ItemLinks.Add(itemRefreshAskforLicense);

                AskforLicenseView.UpdateAskforLicense();
            }
            else if (LicenseListView.Visible == true)
            {
                BarButtonItem itemImportLicense = new BarButtonItem();
                itemImportLicense.Caption = "导入许可";
                itemImportLicense.Name = "importLicense";
                itemImportLicense.LargeImageIndex = 10;
                itemImportLicense.ItemClick += new ItemClickEventHandler(LicenseListView.ImportLicense_ItemClick);
                group.ItemLinks.Add(itemImportLicense);
            }
            else if (ModuleManageView.Visible == true)
            {
                BarButtonItem itemRefreshOnlineUsers = new BarButtonItem();
                itemRefreshOnlineUsers.Caption = "在线用户";
                itemRefreshOnlineUsers.Name = "itemRefreshOnlineUsers";
                itemRefreshOnlineUsers.LargeImageIndex = 9;
                itemRefreshOnlineUsers.ItemClick += new ItemClickEventHandler(ModuleManageView.RefreshOnlineUsers_ItemClick);
                group.ItemLinks.Add(itemRefreshOnlineUsers);

                BarButtonItem itemShowAllLogin = new BarButtonItem();
                itemShowAllLogin.Caption = "所有登录信息";
                itemShowAllLogin.Name = "ShowAllLogin";
                itemShowAllLogin.LargeImageIndex = 11;
                itemShowAllLogin.ItemClick += new ItemClickEventHandler(ModuleManageView.ShowAllLogin_ItemClick);
                group.ItemLinks.Add(itemShowAllLogin);
            }
            else if (CustomerManageView.Visible == true)
            {
                BarButtonItem itemAddUser = new BarButtonItem();
                itemAddUser.Caption = "添加用户";
                itemAddUser.Name = "AddUser";
                itemAddUser.ImageIndex = 15;
                itemAddUser.ItemClick += new ItemClickEventHandler(CustomerManageView.AddUser_ItemClick);
                group.ItemLinks.Add(itemAddUser);
                BarButtonItem itemEditUser = new BarButtonItem();
                itemEditUser.Caption = "编辑用户";
                itemEditUser.Name = "EditUser";
                itemEditUser.ImageIndex = 16;
                itemEditUser.ItemClick += new ItemClickEventHandler(CustomerManageView.EditUser_ItemClick);
                group.ItemLinks.Add(itemEditUser);
                BarButtonItem itemDeleteUser = new BarButtonItem();
                itemDeleteUser.Caption = "删除用户";
                itemDeleteUser.Name = "DelUser";
                itemDeleteUser.ImageIndex = 17;
                itemDeleteUser.ItemClick += new ItemClickEventHandler(CustomerManageView.DeleteUser_ItemClick);
                group.ItemLinks.Add(itemDeleteUser);

                BarButtonItem itemUserGroupManage = new BarButtonItem();
                itemUserGroupManage.Caption = "用户组管理";
                itemUserGroupManage.Name = "UserGroupManage";
                itemUserGroupManage.LargeImageIndex = 12;
                itemUserGroupManage.ItemClick += new ItemClickEventHandler(CustomerManageView.UserGroupManage_ItemClick);
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