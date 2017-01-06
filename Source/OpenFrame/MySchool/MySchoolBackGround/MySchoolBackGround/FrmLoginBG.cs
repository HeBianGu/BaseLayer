using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DAO;
using Entity;

namespace MySchoolBackGround
{
    public partial class FrmLoginBG : Form
    {
        public FrmLoginBG()
        {
            InitializeComponent();      
        }
       
        private void FrmLoginBG_Load(object sender, EventArgs e)
        {
            skinLoginBG.SkinFile = "SportsBlack.ssk";
           
        }

        private void btnLoginExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("ȷ��Ҫ�˳���ϵͳ��", "�˳�", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private bool ValidateInput()
        {

            if (txtLoginID.Text.Trim() == "")
            {
                MessageBox.Show("�������û���", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);            
                txtLoginID.Focus();
                return false;
            }
            else if (txtLoginPassWord.Text.Trim() == "")
            {
                MessageBox.Show("����������", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLoginPassWord.Focus();               
                return false;
            }
            else
            {
                return true;
            }

        }

        private void btnLoginYes_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                RegiserUserEntity userEntity = new RegiserUserEntity();
                LoginDao loginDao = new LoginDao();
   
                if (this.rboFirstLeavelManager.Checked == true)
                {
                    FrmMainBG frmMain = new FrmMainBG(this);
                    userEntity.UserLoginName = txtLoginID.Text;
                    userEntity.UserLoginPwd = txtLoginPassWord.Text;
                    userEntity.UserIdentityId = 5;
                    //bool flag = loginDao.searchIsLoginInfo(userEntity);

                    bool flag = true;

                    if (flag == true)
                    {
                      Entity.LoginInfoEntity.LoginName = userEntity.UserLoginName  ;
                        MessageBox.Show("��ӭ" + userEntity.UserLoginName + "����","����",MessageBoxButtons.OK,MessageBoxIcon.Information); 
                        this.Visible = false;
                        frmMain.Show();
                    }
                    else
                    {
                        MessageBox.Show("�û������������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtLoginID.Focus();
                        this.txtLoginID.Text = "";
                        this.txtLoginPassWord.Text = "";
                    }
                }
                else if (this.rboOrdinaryManager.Checked==true)
                {
                    FrmMainBG frmMain = new FrmMainBG(this);
                    userEntity.UserLoginName = txtLoginID.Text;
                    userEntity.UserLoginPwd = txtLoginPassWord.Text;
                    userEntity.UserIdentityId = 4;
                    bool flag = loginDao.searchIsLoginInfo(userEntity);
                    if (flag == true)
                    {
                       Entity.LoginInfoEntity.LoginName  =userEntity.UserLoginName ;
                        MessageBox.Show("��ӭ" + userEntity.UserLoginName + "����", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Visible = false;
                        frmMain.Show();
                    }
                    else
                    {
                        MessageBox.Show("�û������������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txtLoginID.Focus();
                        this.txtLoginID.Text = "";
                        this.txtLoginPassWord.Text = "";
                    }
                }
              
                
            }
        }

        private void rboFirstLeavelManager_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rboFirstLeavelManager.Checked==true)
            {
                this.Text = "�û�����- һ������Ա";
            }
            if (this.rboOrdinaryManager.Checked==true)
            {
                this.Text = "�û�����- ��ͨ����Ա";
            }
        }       
    }
}