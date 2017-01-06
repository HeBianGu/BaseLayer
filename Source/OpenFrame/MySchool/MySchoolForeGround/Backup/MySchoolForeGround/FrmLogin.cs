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
            DialogResult result = MessageBox.Show("确定要退出本系统吗？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private bool ValidateInput()
        {

            if (txtLoginID.Text.Trim() == "")
            {
                MessageBox.Show("请输入用户名", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLoginID.Focus();
                return false;
            }
            else if (txtLoginPassWord.Text.Trim() == "")
            {
                MessageBox.Show("请输入密码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("欢迎" + userInfoEntity.UserLoginName + "登入", "登入", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Visible = false;
                        frmMain.Show();
                        
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        MessageBox.Show("欢迎" + userInfoEntity.UserLoginName + "登入", "登入", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Visible = false;
                        frmMain.Show();
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        MessageBox.Show("欢迎" + userInfoEntity.UserLoginName + "登入", "登入", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Visible = false;
                        frmMain.Show();
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                this.Text = "用户登入-教员登入";
            }
        }

        private void rboClassTeacher_CheckedChanged(object sender, EventArgs e)
        {
            if (rboClassTeacher.Checked == true)
            {
                this.Text = "用户登入-班主任登入";
            }
        }

        private void rboStudent_CheckedChanged(object sender, EventArgs e)
        {
            if (rboStudent.Checked == true)
            {
                this.Text = "用户登入-学生登入";
            }
        }

       
    }
}