using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DAO;
using Entity;

namespace MySchoolForeGround
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        
        
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            skinLogin.SkinFile = "SportsBlack.ssk";
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
            UserInfoEntity userInfoEntity = new UserInfoEntity();
            LoginDao loginDao = new LoginDao();

            userInfoEntity.UserLoginName = txtLoginID.Text;
            userInfoEntity.UserLoginPwd = txtLoginPassWord.Text;

          

            if (ValidateInput())
            {
                if (this.rboStudent.Checked == true)
                {
                    FrmMain frmMain = new FrmMain(this);
                    userInfoEntity.StuIsExist1 = 1;
                    bool flag = loginDao.LoginStuInfo(userInfoEntity);
                    if (flag == true)
                    {
                        Entity.LoginInfoEntity.LoginName = userInfoEntity.UserLoginName;
                        MessageBox.Show("��ӭ" + userInfoEntity.UserLoginName + "����", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Visible = false;
                        frmMain.Show();
                        
                    }
                    else
                    {
                        MessageBox.Show("�û������������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLoginID.Text="";
                        txtLoginPassWord.Text="";
                        txtLoginID.Focus();
                    }
                }
                else if (rboTeacher.Checked == true)
                { 
                    FrmMain frmMain=new FrmMain(this);
                    userInfoEntity.TeacherIsExist1 = 1;
                    bool flag = loginDao.LoginTeacherInfo(userInfoEntity);
                    if (flag == true)
                    {
                        Entity.LoginInfoEntity.LoginName = userInfoEntity.UserLoginName;
                        MessageBox.Show("��ӭ" + userInfoEntity.UserLoginName + "����", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Visible = false;
                        frmMain.Show();
                    }
                    else
                    {
                        MessageBox.Show("�û������������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLoginID.Text = "";
                        txtLoginPassWord.Text = "";
                        txtLoginID.Focus();
                    }
                }
                else if (rboClassTeacher.Checked == true)
                {
                    FrmMain frmMain = new FrmMain(this);
                    userInfoEntity.ClassTeacherIsExist1 = 1;
                    bool flag = loginDao.LoginClassTeacherInfo(userInfoEntity);
                    if (flag == true)
                    {
                        Entity.LoginInfoEntity.LoginName = userInfoEntity.UserLoginName;
                        MessageBox.Show("��ӭ" + userInfoEntity.UserLoginName + "����", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Visible = false;
                        frmMain.Show();
                    }
                    else
                    {
                        MessageBox.Show("�û������������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLoginID.Text = "";
                        txtLoginPassWord.Text = "";
                        txtLoginID.Focus();
                    }
                }

            }
        }
        private void rboTeacher_CheckedChanged(object sender, EventArgs e)
        {
            if (rboTeacher.Checked == true)
            {
                this.Text = "�û�����-��Ա����";
            }
        }

        private void rboClassTeacher_CheckedChanged(object sender, EventArgs e)
        {
            if (rboClassTeacher.Checked == true)
            {
                this.Text = "�û�����-�����ε���";
            }
        }

        private void rboStudent_CheckedChanged(object sender, EventArgs e)
        {
            if (rboStudent.Checked == true)
            {
                this.Text = "�û�����-ѧ������";
            }
        }

       
    }
}