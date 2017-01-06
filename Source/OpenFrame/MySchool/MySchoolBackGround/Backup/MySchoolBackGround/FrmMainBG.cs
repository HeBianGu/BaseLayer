using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DAO;
using Entity;
using System.Collections;


namespace MySchoolBackGround
{
    public partial class FrmMainBG : Form
    {
        FrmLoginBG frmLogin;
        public FrmMainBG( FrmLoginBG frmLoginBg)
        {
            InitializeComponent();
            frmLogin = frmLoginBg;
            if (frmLogin.rboOrdinaryManager.Checked == true)
            {
                this.btnInquireUser.Enabled = false;
                this.btnEditUser.Enabled = false;
                this.btnDeleteUser.Enabled = false;
                this.chkRegisterPopedom.Visible = false;
                btnDeleteMessage.Enabled = false;
            }
        }
        public FrmMainBG()
        {
            InitializeComponent();
        }

        #region 全局变量
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        ClassNameDao classNameDao = new ClassNameDao();
        IdentityDao identityDao = new IdentityDao();
        SearchTeacherDao searchTeacherDao = new SearchTeacherDao();
        RegiserClassDao regiserClassDao = new RegiserClassDao();
        EditLessonNameDao editLessNameDao = new EditLessonNameDao();
        InsertLessonNameDao insertLessonNameDao = new InsertLessonNameDao();
        SearchLessonNameDao searchLessonNameDao = new SearchLessonNameDao();
        LessonInfoEntity lessInfoEntity = new LessonInfoEntity();
        LessonInfoEntity lessonInfoEntity = new LessonInfoEntity();
        InsertExamInfo insertExamInfo = new InsertExamInfo();
        EditExamInfoDao editExamInfoDao = new EditExamInfoDao();
        judgeEntity judgeEn = new judgeEntity();
        RegiserClassEntity ClassEntity = new RegiserClassEntity();
        SearchExamInfoDao searchExamInfoDao = new SearchExamInfoDao();
        RegisterUserDao registerUser = new RegisterUserDao();
        RegiserUserEntity userEntity = new RegiserUserEntity();
        SearchClassInfoDao searchClassInfoDao = new SearchClassInfoDao();
        EditClassDao editClassDao = new EditClassDao();
        SexInfoDao sexInfoDao = new SexInfoDao();
        SearchUserInfoDao searchUserInfoDao = new SearchUserInfoDao();
        EditUserInfoDao editUserInfoDao = new EditUserInfoDao();
        SearchAllUserInfo searchAllUserInfo = new SearchAllUserInfo();
        RemoveUserDao removeUserDao = new RemoveUserDao();
        MessageBoardEntity messageBoardEntity = new MessageBoardEntity();
        MessageBoardInfoDao messageBoardInfoDao = new MessageBoardInfoDao();
       
        #endregion

        #region 主窗体右上角关闭事件
        private void FrmMainBG_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要退出本系统吗？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        #endregion

        #region 下拉框绑定方法
        private void repalceComboBox(Control con, DataTable dt, String valueMember, String displayMember)
        {
            ComboBox cbo = (ComboBox)con;
            cbo.DataSource = dt;
            cbo.DisplayMember = displayMember;
            cbo.ValueMember = valueMember;
        }
        #endregion

        #region 状态栏显示当前时间
        private void tmrMainBG_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "当前日期:" + DateTime.Now;
        }
        #endregion

        #region 工具栏所有单击事件
        private void tsbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void tsbAbout_Click(object sender, EventArgs e)
        {
            FrmAboutBG frmAboutBG = new FrmAboutBG();
            frmAboutBG.ShowDialog();
        }
        private void tsbUserInquire_Click(object sender, EventArgs e)
        {
            pnlInquireUser.Visible = true;
            pnlRemoveUser.Visible = false;
            pnlRegisterUser.Visible = false;
            pnlEditUser.Visible = false;
            pnlInquireClass.Visible = false;
            pnlRegisterClass.Visible = false;
            pnlEditClass.Visible = false;
            pnlInsertExam.Visible = false;
            pnlInquireExam.Visible = false;
            this.pictureBox.Visible = false;
            this.lblArticle.Visible = false;
            dgvUserRemove.Visible = false;
            pnlBBS.Visible = false;
        }
        private void tsbClassInquire_Click(object sender, EventArgs e)
        {
            pnlInquireClass.Visible = true;
            pnlRemoveUser.Visible = false;
            pnlRegisterClass.Visible = false;
            pnlEditClass.Visible = false;
            pnlRegisterUser.Visible = false;
            pnlInquireUser.Visible = false;
            pnlEditUser.Visible = false;
            pnlInsertExam.Visible = false;
            pnlInquireExam.Visible = false;
            pnlEditExam.Visible = false;
            this.pictureBox.Visible = false;
            this.lblArticle.Visible = false;
            dgvUserRemove.Visible = false;
            pnlBBS.Visible = false;
        }
     
        private void tsbBBS_Click(object sender, EventArgs e)
        {
            pnlBBS.Visible = true;
            pnlRemoveUser.Visible = false;
            pnlRegisterUser.Visible = false;
            pnlInquireUser.Visible = false;
            pnlEditUser.Visible = false;
            pnlRegisterClass.Visible = false;
            pnlInquireClass.Visible = false;
            pnlEditClass.Visible = false;
            pnlInquireExam.Visible = false;
            pnlInsertExam.Visible = false;
            pnlEditExam.Visible = false;
            this.pictureBox.Visible = false;
            this.lblArticle.Visible = false;
            dgvUserRemove.Visible = false;
            ds = messageBoardInfoDao.searchBBSInfo();
            dgvBBSBoard.DataSource = ds.Tables["SearchBBSInfo"];
        }
        #endregion



        #region 窗体load事件
        private void FrmMainBG_Load(object sender, EventArgs e)
        {
            skinFrmMainBG.SkinFile = "SportsBlack.ssk";  
            toolStripStatusLabel1.Text = "当前登入用户:" + Entity.LoginInfoEntity.LoginName;
            listView.Clear();
            listView.LargeImageList = imageListMainBG;
            listView.Items.Add("注册新用户", "注册新用户", 0);
            listView.Items.Add("查询编辑用户", "查询编辑用户", 1);
            listView.Items.Add("学员调动", "学员调动", 1);

            this.chkRegisterPopedom.Enabled = false;
            this.chkRegisterClassTeacher.Enabled = false;

            ds = classNameDao.searchClassName();
            this.cboRegisterClass.DataSource = ds.Tables["classNameInfo"];
            this.cboRegisterClass.ValueMember = "ClassId";
            this.cboRegisterClass.DisplayMember = "ClassName";

          
            this.cboUserClass.DataSource = ds.Tables["classNameInfo"];
            this.cboUserClass.ValueMember = "ClassId";
            this.cboUserClass.DisplayMember = "ClassName";

            this.cboEditrClass.DataSource = ds.Tables["classNameInfo"];
            this.cboEditrClass.ValueMember = "ClassId";
            this.cboEditrClass.DisplayMember = "ClassName";

            ds = identityDao.searchIdentityName();
            this.cboRegisterIdentity.DataSource = ds.Tables["IdentityNameInfo"];
            this.cboRegisterIdentity.ValueMember = "IdentityId";
            this.cboRegisterIdentity.DisplayMember = "IdentityName";

            this.cboEditIdentity.Items.Add("学生");
            this.cboEditIdentity.Items.Add("教师");
            this.cboEditIdentity.Items.Add("管理员");

            ds = searchTeacherDao.searchClassTeacherName();
            this.cboRegisterClassTeacher.DataSource = ds.Tables["classTeacherNameInfo"];
            this.cboRegisterClassTeacher.ValueMember = "ClassTeacherId";
            this.cboRegisterClassTeacher.DisplayMember = "ClassTeacherName";

            this.cboEditClassTeacher.DataSource = ds.Tables["classTeacherNameInfo"];
            this.cboEditClassTeacher.ValueMember = "ClassTeacherId";
            this.cboEditClassTeacher.DisplayMember = "ClassTeacherName";

            ds = searchTeacherDao.searchTeacherName();
            this.cboRegisterTeacher.DataSource = ds.Tables["teacherNameInfo"];
            this.cboRegisterTeacher.ValueMember = "TeacherId";
            this.cboRegisterTeacher.DisplayMember = "TeacherName";

            this.cboEditTeacher.DataSource = ds.Tables["teacherNameInfo"];
            this.cboEditTeacher.ValueMember = "TeacherId";
            this.cboEditTeacher.DisplayMember = "TeacherName";

            ds = searchLessonNameDao.searchLessonName();
            this.cboLessonChoice.DataSource = ds.Tables["LessonNameInfo"];
            this.cboLessonChoice.ValueMember = "LessonId";
            this.cboLessonChoice.DisplayMember = "LessonName";

            this.cboLessonEssayQuestion.DataSource = ds.Tables["LessonNameInfo"];
            this.cboLessonEssayQuestion.ValueMember = "LessonId";
            this.cboLessonEssayQuestion.DisplayMember = "LessonName";

            this.cboInquireCoursesType.DataSource = ds.Tables["LessonNameInfo"];
            this.cboInquireCoursesType.ValueMember = "LessonId";
            this.cboInquireCoursesType.DisplayMember = "LessonName";

            this.cboChoiceLessonDel.DataSource = ds.Tables["LessonNameInfo"];
            this.cboChoiceLessonDel.ValueMember = "LessonId";
            this.cboChoiceLessonDel.DisplayMember = "LessonName";

            this.cboInquireExamType.Items.Add("选择题");
            this.cboInquireExamType.Items.Add("问答题");
            this.cboInquireExamType.SelectedItem = "选择题";

            this.cboInquieClassType.Items.Add("班级名称");
            this.cboInquieClassType.Items.Add("班级人数");
            this.cboInquieClassType.Items.Add("班级编号");
            this.cboInquieClassType.SelectedItem = "班级名称";

            this.cboInquireType.Items.Add("所有");
            this.cboInquireType.Items.Add("姓名");
            this.cboInquireType.Items.Add("性别");
            this.cboInquireType.Items.Add("学生");
            this.cboInquireType.Items.Add("教员");
            this.cboInquireType.Items.Add("班主任");
            this.cboInquireType.Items.Add("管理员");
            this.cboInquireType.SelectedItem = "所有";


          
        }
        #endregion

        #region 窗体"班级管理"按钮
        private void btnClassManage_Click(object sender, EventArgs e)
        {
            listView.Dock = DockStyle.None;
            btnClassManage.SendToBack();
            btnClassManage.Dock = DockStyle.Top;
            btnUserManage.SendToBack();
            btnUserManage.Dock = DockStyle.Top;
            btnExamManage.Dock = DockStyle.Bottom;
            btnBBS.SendToBack();
            btnBBS.Dock = DockStyle.Bottom;
            listView.Dock = DockStyle.Bottom;
            listView.Clear();
            listView.Items.Add("创建新班级", "创建新班级", 2);
            listView.Items.Add("查询编辑班级", "查询编辑班级", 3);
          
        }
        #endregion

        #region 窗体"题库管理"按钮
        private void btnExamManage_Click(object sender, EventArgs e)
        {
            listView.Dock = DockStyle.None;
            btnExamManage.SendToBack();
            btnExamManage.Dock = DockStyle.Top;
            btnClassManage.SendToBack();
            btnClassManage.Dock = DockStyle.Top;
            btnUserManage.SendToBack();
            btnUserManage.Dock = DockStyle.Top;
            btnBBS.Dock = DockStyle.Bottom;
            listView.Dock = DockStyle.Bottom;
            listView.Clear();
            listView.Items.Add("添加新试题", "添加新试题", 4);
            listView.Items.Add("查询编辑题库", "查询编辑题库", 4);
        }
        #endregion

        #region 窗体"留言板"按钮
        private void btnBBS_Click(object sender, EventArgs e)
        {
            listView.Dock = DockStyle.None;
            btnBBS.SendToBack();
            btnBBS.Dock = DockStyle.Top;
            btnExamManage.SendToBack();
            btnExamManage.Dock = DockStyle.Top;
            btnClassManage.SendToBack();
            btnClassManage.Dock = DockStyle.Top;
            btnUserManage.SendToBack();
            btnUserManage.Dock = DockStyle.Top;
            listView.Dock = DockStyle.Bottom;
            listView.Clear();
            listView.Items.Add("进入留言板", "进入留言板", 5);
        }
        #endregion

        #region 窗体"用户管理"按钮
        private void btnUserManage_Click(object sender, EventArgs e)
        {
            listView.Dock = DockStyle.None;
            btnUserManage.Dock = DockStyle.Top;
            btnClassManage.Dock = DockStyle.Bottom;
            btnExamManage.SendToBack();
            btnExamManage.Dock = DockStyle.Bottom;
            btnBBS.SendToBack();
            btnBBS.Dock = DockStyle.Bottom;
            listView.BringToFront();
            listView.Dock = DockStyle.Bottom;
            listView.Clear();
            listView.Items.Add("注册新用户", "注册新用户", 0);
            listView.Items.Add("查询编辑用户", "查询编辑用户", 1);
            listView.Items.Add("学员调动", "学员调动", 1);
        }
        #endregion

        #region 窗体"listView"单击事件
        private void listView_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "" + listView.SelectedItems[0].Text;
                if (str.Equals("注册新用户"))
                {
                    pnlRegisterUser.Visible = true;
                    pnlRemoveUser.Visible = false;
                    pnlInquireUser.Visible = false;
                    pnlEditUser.Visible = false;
                    pnlRegisterClass.Visible = false;
                    pnlInquireClass.Visible = false;
                    pnlEditClass.Visible = false;
                    pnlInquireExam.Visible = false;
                    pnlInsertExam.Visible = false;
                    pnlEditExam.Visible = false;
                    this.pictureBox.Visible = false;
                    this.lblArticle.Visible = false;
                    dgvUserRemove.Visible = false;
                    pnlBBS.Visible = false;
                }
                else if (str.Equals("查询编辑用户"))
                {
                    pnlInquireUser.Visible = true;
                    pnlRemoveUser.Visible = false;
                    pnlRegisterUser.Visible = false;
                    pnlEditUser.Visible = false;
                    pnlInquireClass.Visible = false;
                    pnlRegisterClass.Visible = false;
                    pnlEditClass.Visible = false;
                    pnlInsertExam.Visible = false;
                    pnlInquireExam.Visible = false;
                    this.pictureBox.Visible = false;
                    this.lblArticle.Visible = false;
                    dgvUserRemove.Visible = false;
                    pnlBBS.Visible = false;
                }
                else if (str.Equals("创建新班级"))
                {
                    pnlRegisterClass.Visible = true;
                    pnlRemoveUser.Visible = false;
                    pnlInquireClass.Visible = false;
                    pnlEditClass.Visible = false;
                    pnlRegisterUser.Visible = false;
                    pnlInquireUser.Visible = false;
                    pnlEditUser.Visible = false;
                    pnlInsertExam.Visible = false;
                    pnlInquireExam.Visible = false;
                    pnlEditExam.Visible = false;
                    this.pictureBox.Visible = false;
                    this.lblArticle.Visible = false;
                    dgvUserRemove.Visible = false;
                    pnlBBS.Visible = false;
                }
                else if (str.Equals("查询编辑班级"))
                {
                    pnlInquireClass.Visible = true;
                    pnlRemoveUser.Visible = false;
                    pnlRegisterClass.Visible = false;
                    pnlEditClass.Visible = false;
                    pnlRegisterUser.Visible = false;
                    pnlInquireUser.Visible = false;
                    pnlEditUser.Visible = false;
                    pnlInsertExam.Visible = false;
                    pnlInquireExam.Visible = false;
                    pnlEditExam.Visible = false;
                    this.pictureBox.Visible = false;
                    this.lblArticle.Visible = false;
                    dgvUserRemove.Visible = false;
                    pnlBBS.Visible = false;
                }
                else if (str.Equals("添加新试题"))
                {
                    pnlInsertExam.Visible = true;
                    pnlRemoveUser.Visible = false;
                    pnlInquireExam.Visible = false;
                    pnlEditExam.Visible = false;
                    pnlInquireClass.Visible = false;
                    pnlRegisterClass.Visible = false;
                    pnlEditClass.Visible = false;
                    pnlRegisterUser.Visible = false;
                    pnlInquireUser.Visible = false;
                    pnlEditUser.Visible = false;
                    this.pictureBox.Visible = false;
                    this.lblArticle.Visible = false;
                    dgvUserRemove.Visible = false;
                    pnlBBS.Visible = false;
                }
                else if (str.Equals("查询编辑题库"))
                {
                    pnlInquireExam.Visible = true;
                    pnlRemoveUser.Visible = false;
                    pnlInsertExam.Visible = false;
                    pnlEditExam.Visible = false;
                    pnlInquireClass.Visible = false;
                    pnlRegisterClass.Visible = false;
                    pnlEditClass.Visible = false;
                    pnlRegisterUser.Visible = false;
                    pnlInquireUser.Visible = false;
                    pnlEditUser.Visible = false;
                    this.pictureBox.Visible = false;
                    this.lblArticle.Visible = false;
                    dgvUserRemove.Visible = false;
                    pnlBBS.Visible = false;
                }
                else if (str.Equals("学员调动"))
                {
                    pnlRemoveUser.Visible = true;
                    pnlRegisterUser.Visible = false;
                    pnlInquireUser.Visible = false;
                    pnlEditUser.Visible = false;
                    pnlRegisterClass.Visible = false;
                    pnlInquireClass.Visible = false;
                    pnlEditClass.Visible = false;
                    pnlInquireExam.Visible = false;
                    pnlInsertExam.Visible = false;
                    pnlEditExam.Visible = false;
                    this.pictureBox.Visible = false;
                    this.lblArticle.Visible = false;
                    dgvUserRemove.Visible = false;
                    pnlBBS.Visible = false;
                }
                else if (str.Equals("进入留言板"))
                {  
                    pnlBBS.Visible = true;
                    pnlRemoveUser.Visible = false;
                    pnlRegisterUser.Visible = false;
                    pnlInquireUser.Visible = false;
                    pnlEditUser.Visible = false;
                    pnlRegisterClass.Visible = false;
                    pnlInquireClass.Visible = false;
                    pnlEditClass.Visible = false;
                    pnlInquireExam.Visible = false;
                    pnlInsertExam.Visible = false;
                    pnlEditExam.Visible = false;
                    this.pictureBox.Visible = false;
                    this.lblArticle.Visible = false;
                    dgvUserRemove.Visible = false;
                    ds = messageBoardInfoDao.searchBBSInfo();
                    dgvBBSBoard.DataSource = ds.Tables["SearchBBSInfo"];
                }


            }
            catch (Exception ex)
            {

            }
        }
        #endregion



        #region 返回用户查询按钮
        private void btnEditBackInquireUser_Click(object sender, EventArgs e)
        {
            pnlInquireUser.Visible = true;
            pnlRegisterUser.Visible = false;
            pnlEditUser.Visible = false;
            pnlInsertExam.Visible = false;
            this.pictureBox.Visible = false;
            this.lblArticle.Visible = false;
        }
        #endregion

        #region 用户编辑按钮
        private void btnEditUser_Click_1(object sender, EventArgs e)
        {
            pnlInquireUser.Visible = false;
            pnlRegisterUser.Visible = false;
            pnlEditUser.Visible = true;
            pnlInsertExam.Visible = false;
            this.pictureBox.Visible = false;
            this.lblArticle.Visible = false;
        }
        #endregion

        #region 班级编辑按钮
        private void btnEditClass_Click(object sender, EventArgs e)
        {
            pnlEditClass.Visible = true;
            pnlInquireClass.Visible = false;
            pnlRegisterClass.Visible = false;
            pnlRegisterUser.Visible = false;
            pnlInquireUser.Visible = false;
            pnlEditUser.Visible = false;
            pnlInsertExam.Visible = false;
            this.pictureBox.Visible = false;
            this.lblArticle.Visible = false;
        }
        #endregion

        #region 返回班级查询按钮
        private void btnInquireClassBack_Click(object sender, EventArgs e)
        {
            pnlInquireClass.Visible = true;
            pnlEditClass.Visible = false;
            pnlRegisterClass.Visible = false;
            pnlRegisterUser.Visible = false;
            pnlInquireUser.Visible = false;
            pnlInsertExam.Visible = false;
            pnlEditUser.Visible = false;
            this.pictureBox.Visible = false;
            this.lblArticle.Visible = false;
        }
        #endregion

        #region 题目编辑按钮
        private void btnEditExam_Click(object sender, EventArgs e)
        {
            if (cboInquireExamType.SelectedItem.Equals("选择题"))
            {
                btnEditChoiceYes.Enabled=true;
                btnEditChoiceUp.Enabled=true;
                btnEditChoiceNext.Enabled=true;

                btnEditEssayQuestionYes.Enabled=false;
                btnEditEssayQuestionUp.Enabled=false;
                btnEditEssayQuestionNext.Enabled = false;
            }
            if (cboInquireExamType.SelectedItem.Equals("问答题"))
            {
                btnEditChoiceYes.Enabled = false;
                btnEditChoiceUp.Enabled = false;
                btnEditChoiceNext.Enabled = false;

                btnEditEssayQuestionYes.Enabled = true;
                btnEditEssayQuestionUp.Enabled = true;
                btnEditEssayQuestionNext.Enabled = true;
            }
            pnlEditExam.Visible = true;
            pnlInquireExam.Visible = false;
            pnlInsertExam.Visible = false;
            pnlInquireClass.Visible = false;
            pnlRegisterClass.Visible = false;
            pnlEditClass.Visible = false;
            pnlRegisterUser.Visible = false;
            pnlInquireUser.Visible = false;
            pnlEditUser.Visible = false;
            this.pictureBox.Visible = false;
            this.lblArticle.Visible = false;
        }
        #endregion

        #region 返回成绩查询按钮事件
        private void btnExamBackInquire_Click(object sender, EventArgs e)
        {
            pnlInquireExam.Visible = true;
            pnlEditExam.Visible = false;
            pnlInsertExam.Visible = false;
            pnlInquireClass.Visible = false;
            pnlRegisterClass.Visible = false;
            pnlEditClass.Visible = false;
            pnlRegisterUser.Visible = false;
            pnlInquireUser.Visible = false;
            pnlEditUser.Visible = false;
            this.pictureBox.Visible = false;
            this.lblArticle.Visible = false;
        }
        #endregion



        #region 注册时身份选择事件
        private void cboRegisterIdentity_SelectedValueChanged_1(object sender, EventArgs e)
        {
            string select = this.cboRegisterIdentity.SelectedValue + "";
            if (!select.Equals("System.Data.DataRowView"))
            {
                int selectValue = int.Parse(select);
                if (selectValue == 1)
                {
                    if (this.txtRegisterStuID.Equals(""))
                    {
                        this.txtRegisterStuID.Visible = true;
                        this.txtRegisterStuID.Focus();
                        return;
                    }
                    this.cboRegisterClass.Enabled = true;
                    this.txtRegisterStuID.Enabled = true;
                    this.chkRegisterPopedom.Enabled = false;
                    this.chkRegisterClassTeacher.Enabled = false;
                }
                else if (selectValue == 2)
                {
                    this.lbl10.Visible = false;
                    this.chkRegisterClassTeacher.Enabled = true;
                    this.cboRegisterClass.Enabled = false;
                    this.txtRegisterStuID.Enabled = false;
                    this.chkRegisterPopedom.Enabled = false;
                }
                else if (selectValue == 4)
                {
                    this.lbl10.Visible = false;
                    this.chkRegisterPopedom.Enabled = true;
                    this.cboRegisterClass.Enabled = false;
                    this.txtRegisterStuID.Enabled = false;
                    this.chkRegisterClassTeacher.Enabled = false;
                }
            }
        }
        #endregion

        #region 注册用户按钮单击事件
        private void btnRegisterInsert_Click(object sender, EventArgs e)
        {
            string select = this.cboRegisterIdentity.SelectedValue + "";
            try
            {
                int sex = 0;
                if (rboRegisterMale.Checked == true)
                {
                    sex = 0;//男
                }
                if (rboRegisterFemale.Checked == true)
                {
                    sex = 1;//女
                }
                int message = judgeEn.judge1(txtRegisterLoginID.Text,
                              txtRegisterPassWord.Text, txtRegisterPassWordAG.Text,
                              txtRegisterName.Text, txtRegisterIdCard.Text,
                              txtRegisterAge.Text, txtRegisterPhone.Text,
                              txtRegisterAddress.Text);
                if (message == 0)
                {
                    lbl1.Visible = true;
                    txtRegisterLoginID.Focus();
                    return;
                }
                else if (message == 1)
                {
                    lbl2.Visible = true;
                    txtRegisterPassWord.Focus();
                    return;
                }
                else if (message == 2)
                {
                    lbl3.Visible = true;
                    txtRegisterPassWordAG.Focus();
                    return;
                }
                else if (message == 3)
                {
                    lbl5.Visible = true;
                    txtRegisterName.Focus();
                    return;
                }
                else if (message == 4)
                {
                    lbl6.Visible = true;
                    txtRegisterIdCard.Focus();
                    return;
                }
                else if (message == 5)
                {
                    lbl7.Visible = true;
                    txtRegisterAge.Focus();
                    return;
                }
                else if (message == 6)
                {
                    lbl8.Visible = true;
                    txtRegisterPhone.Focus();
                    return;
                }

                else if (message == 7)
                {
                    lbl11.Visible = true;
                    txtRegisterAddress.Focus();
                    return;
                }

                if (!this.txtRegisterPassWord.Text.Equals(this.txtRegisterPassWordAG.Text))
                {
                    this.lbl12.Visible = true;
                    this.txtRegisterPassWord.Focus();
                    this.txtRegisterPassWord.Text = "";
                    this.txtRegisterPassWordAG.Text = "";
                    return;
                }
                else
                {
                    this.lbl12.Visible = false;
                }

                if (!((this.txtRegisterPhone.Text.Length == 8) || (this.txtRegisterPhone.Text.Length == 11)))
                {
                    this.lbl22.Visible = true;
                    this.txtRegisterPhone.Focus();
                    this.txtRegisterPhone.Text = "";
                    return;
                }
                else
                {
                    this.lbl22.Visible = false;
                }

                if (!(this.txtRegisterIdCard.Text.Length == 18))
                {
                    this.lbl21.Visible = true;
                    this.txtRegisterIdCard.Focus();
                    this.txtRegisterIdCard.Text = "";
                    return;
                }
                else
                {
                    this.lbl21.Visible = false;
                }

                if (!select.Equals("System.Data.DataRowView"))
                {
                    int selectValue = int.Parse(select);
                    userEntity.UserLoginName = this.txtRegisterLoginID.Text;
                    userEntity.UserLoginPwd = this.txtRegisterPassWord.Text;
                    userEntity.UserEnterSchoolTime = this.dtpRegisterWork.Text;
                    userEntity.UserName = this.txtRegisterName.Text;
                    userEntity.UserIdCard = this.txtRegisterIdCard.Text;
                    try
                    {
                        userEntity.UserAge = int.Parse(this.txtRegisterAge.Text);
                        this.lbl13.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        this.lbl13.Visible = true;
                        this.txtRegisterAge.Focus();
                        return;
                    }
                    userEntity.UserBrithday = this.dtpRegisterBrithday.Text;
                    try
                    {
                        userEntity.UserPhone = int.Parse(this.txtRegisterPhone.Text);
                        this.lbl18.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        this.lbl18.Visible = true;
                        this.txtRegisterPhone.Focus();
                        return;
                    }

                    userEntity.UserSex = sex;
                    userEntity.UserClassId = int.Parse(this.cboRegisterClass.SelectedValue + "");
                    try
                    {
                        userEntity.UserId = int.Parse(this.txtRegisterStuID.Text);
                    }
                    catch (Exception ex)
                    {

                    }
                    userEntity.UserAddress = this.txtRegisterAddress.Text;
                    userEntity.UserIdentityId = int.Parse(this.cboRegisterIdentity.SelectedValue + "");
                    userEntity.ClassTeacherIsExist1 = 1;
                    userEntity.ManagerIsExist1 = 1;
                    userEntity.StuIsExist1 = 1;
                    userEntity.TeacherIsExist1 = 1;
                    userEntity.UserRemoveTime = "无";
                    userEntity.UserRemoveClass1 = "无";
                    userEntity.StuInitializationClass1 = "空";
                    if (selectValue == 1)
                    {
                        userEntity.UserLeaveSchoolTime = "在校";
                    }
                    else
                    {
                        userEntity.UserLeaveSchoolTime = "在职";
                    }
                    if (selectValue == 4 && this.chkRegisterPopedom.Checked == true)
                    {
                        this.lbl10.Visible = false;
                        userEntity.UserIdentityId = 5; //一级管理员
                        bool flag = registerUser.insertManagerInfo(userEntity);
                        if (flag == true)
                            MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (selectValue == 4)
                    {
                        this.lbl10.Visible = false;
                        userEntity.UserIdentityId = 4; //普通管理员    
                        bool flag = registerUser.insertManagerInfo(userEntity);
                        if (flag == true)
                            MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (selectValue == 2 && this.chkRegisterClassTeacher.Checked == true)
                    {
                        this.lbl10.Visible = false;
                        userEntity.UserIdentityId = 3; //班主任
                        bool flag = registerUser.insertClassTeacherInfo(userEntity);
                        if (flag == true)
                            MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (selectValue == 2)
                    {
                        this.lbl10.Visible = false;
                        userEntity.UserIdentityId = 2;//普通教员 
                        bool flag = registerUser.insertTeacherInfo(userEntity);
                        if (flag == true)
                            MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else if (selectValue == 1)
                    {
                        userEntity.UserIdentityId = 1;//学生
                        bool flag = registerUser.insertStudentInfo(userEntity);
                        if (flag == true)
                            MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    txtRegisterLoginID.Text = "";
                    txtRegisterPassWord.Text = "";
                    txtRegisterPassWordAG.Text = "";
                    txtRegisterName.Text = "";
                    txtRegisterIdCard.Text = "";
                    txtRegisterAge.Text = "";
                    txtRegisterPhone.Text = "";
                    txtRegisterStuID.Text = "";
                    txtRegisterAddress.Text = "";
                    txtRegisterLoginID.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region 注册用户窗体文本框失去鼠标焦点事件
        private void txtRegisterLoginID_Leave(object sender, EventArgs e)
        {
            if (!txtRegisterLoginID.Text.Equals(""))
            {
                lbl1.Visible = false;
            }
        }

        private void txtRegisterPassWord_Leave(object sender, EventArgs e)
        {
            if (!txtRegisterPassWord.Text.Equals(""))
            {
                lbl2.Visible = false;
            }
        }

        private void txtRegisterPassWordAG_Leave(object sender, EventArgs e)
        {
            if (!txtRegisterPassWordAG.Text.Equals(""))
            {
                lbl3.Visible = false;
            }
        }

        private void txtRegisterName_Leave(object sender, EventArgs e)
        {
            if (!txtRegisterName.Text.Equals(""))
            {
                lbl5.Visible = false;
            }
        }

        private void txtRegisterIdCard_Leave(object sender, EventArgs e)
        {
            if (!txtRegisterIdCard.Text.Equals(""))
            {
                lbl6.Visible = false;
            }
        }

        private void txtRegisterAge_Leave(object sender, EventArgs e)
        {
            if (!txtRegisterAge.Text.Equals(""))
            {
                lbl7.Visible = false;
            }
        }

        private void txtRegisterPhone_Leave(object sender, EventArgs e)
        {
            if (!txtRegisterPhone.Text.Equals(""))
            {
                lbl8.Visible = false;
            }
        }

        private void txtRegisterStuID_Leave(object sender, EventArgs e)
        {
            if (!txtRegisterStuID.Text.Equals(""))
            {
                lbl10.Visible = false;
            }
        }

        private void txtRegisterClassName_Leave(object sender, EventArgs e)
        {
            if (!txtRegisterClassName.Text.Equals(""))
            {
                lbl14.Visible = false;
            }
        }

        private void txtRegisterClassUnm_Leave(object sender, EventArgs e)
        {
            if (!txtRegisterClassUnm.Text.Equals(""))
            {
                lbl15.Visible = false;
            }
        }
        private void txtRegisterAddress_TextChanged(object sender, EventArgs e)
        {
            this.lbl11.Visible = false;
        }
        #endregion

        #region 注册班级按钮单击事件
        private void btnRegisterClassYes_Click(object sender, EventArgs e)
        {
            int message = judgeEn.judge1(txtRegisterClassName.Text, txtRegisterClassUnm.Text);
            if (message == 0)
            {
                lbl14.Visible = true;
                txtRegisterClassName.Focus();
                return;
            }
            else if (message == 1)
            {
                lbl15.Visible = true;
                txtRegisterClassUnm.Focus();
                return;
            }
            ClassEntity.ClassName = this.txtRegisterClassName.Text;
            try
            {
                ClassEntity.ClassStuNum = int.Parse(this.txtRegisterClassUnm.Text);
            }
            catch (Exception ex)
            {
                lbl29.Visible = true;
                txtRegisterClassUnm.Focus();
                return;
            }
            ClassEntity.ClassFinishTime = "开班中";
            ClassEntity.ClassStartTime = this.dtpClassRegisterBegin.Text;
            ClassEntity.TeacherId = int.Parse(this.cboRegisterTeacher.SelectedValue + "");
            ClassEntity.ClassTeacherId = int.Parse(this.cboRegisterClassTeacher.SelectedValue + "");
            ClassEntity.ClassIsExist = 1;
            bool flag = regiserClassDao.insertClassInfo(ClassEntity);
            if (flag == true)
                lbl29.Visible = false;
            MessageBox.Show("添加成功");
            this.txtRegisterClassName.Text = "";
            this.txtRegisterClassUnm.Text = "";
            ds = searchClassInfoDao.searchClassByName(ClassEntity);
            this.dgdInquireClass.DataSource = ds.Tables["SearchClassByName"];
        }
        #endregion



        #region 获取复选框选中选择题答案
        public string requestAnswer(string answer)
        {
            if (this.chkInsertChoiceA.Checked == true)
                answer = answer + "A";
            if (this.chkInsertChoiceB.Checked == true)
                answer = answer + "B";
            if (this.chkInsertChoiceC.Checked == true)
                answer = answer + "C";
            if (this.chkInsertChoiceD.Checked == true)
                answer = answer + "D";
            if (this.chkInsertChoiceE.Checked == true)
                answer = answer + "E";
            if (this.chkEditChoiceA.Checked == true)
                answer = answer + "A";
            if (this.chkEditChoiceB.Checked == true)
                answer = answer + "B";
            if (this.chkEditChoiceC.Checked == true)
                answer = answer + "C";
            if (this.chkEditChoiceD.Checked == true)
                answer = answer + "D";
            if (this.chkEditChoiceE.Checked == true)
                answer = answer + "E";

            return answer;
        }
        #endregion

        #region 修改选择题时检查错误方法
        private bool editMessage(int message)
        {
            bool flag = true;
            if (message == 0)
            {
                lbl26.Visible = true;
                txtEditChoice.Focus();
                flag = false;
            }
            else if (message == 1)
            {
                lbl27.Visible = true;
                txtEditChoiceA.Focus();
                flag = false;
            }
            else if (message == 2)
            {
                lbl27.Visible = true;
                txtEditChoiceB.Focus();
                flag = false;
            }
            else if (message == 3)
            {
                lbl27.Visible = true;
                txtEditChoiceC.Focus();
                flag = false;
            }
            else if (message == 4)
            {
                lbl27.Visible = true;
                txtEditChoiceD.Focus();
                flag = false;
            }
            else if (message == 5)
            {
                lbl27.Visible = true;
                txtEditChoiceE.Focus();
                flag = false;
            }
            return flag;
        }
        #endregion

        #region 加添选择题时检查错误方法
        private bool checkMessage(int message)
        {
            bool flag = true;
            if (message == 0)
            {
                lbl16.Visible = true;
                txtInsertChoiceContent.Focus();
                flag = false;
            }
            else if (message == 1)
            {
                lbl17.Visible = true;
                txtInsertChoiceA.Focus();
                flag = false;
            }
            else if (message == 2)
            {
                lbl17.Visible = true;
                txtInsertChoiceB.Focus();
                flag = false;
            }
            else if (message == 3)
            {
                lbl17.Visible = true;
                txtInsertChoiceC.Focus();
                flag = false;
            }
            else if (message == 4)
            {
                lbl17.Visible = true;
                txtInsertChoiceD.Focus();
                flag = false;
            }
            else if (message == 5)
            {
                lbl17.Visible = true;
                txtInsertChoiceE.Focus();
                flag = false;
            }
            return flag;
        }
        #endregion

        #region 添加选择题按钮单击事件
        private void btnInsertChioceYes_Click(object sender, EventArgs e)
        {
            int message = judgeEn.judge1(txtInsertChoiceContent.Text, txtInsertChoiceA.Text,
                                         txtInsertChoiceB.Text, txtInsertChoiceC.Text,
                                         txtInsertChoiceD.Text, txtInsertChoiceE.Text);

            if (checkMessage(message) == false)
                return;
            lessInfoEntity.LessonId = int.Parse(this.cboLessonChoice.SelectedValue + "");
            lessInfoEntity.ChoiceSubject = this.txtInsertChoiceContent.Text;
            lessInfoEntity.ChoiceContentA = this.txtInsertChoiceA.Text;
            lessInfoEntity.ChoiceContentB = this.txtInsertChoiceB.Text;
            lessInfoEntity.ChoiceContentC = this.txtInsertChoiceC.Text;
            lessInfoEntity.ChoiceContentD = this.txtInsertChoiceD.Text;
            lessInfoEntity.ChoiceContentE = this.txtInsertChoiceE.Text;
            string answer = requestAnswer("");
            this.lbl18.Visible = false;
            if (chkInsertChoiceA.Checked == false && chkInsertChoiceB.Checked == false
                    && chkInsertChoiceC.Checked == false
                    && chkInsertChoiceD.Checked == false && chkInsertChoiceE.Checked == false)
            {
                this.lbl18.Visible = true;
                chkEditChoiceA.Focus();
                return;
            }

            lessInfoEntity.ChoiceRightAnswer = answer;
            lessInfoEntity.ChoiceIsExist = 1;
            bool flag = insertExamInfo.insertChoice(lessInfoEntity);
            if (flag == true)
            {
                MessageBox.Show("添加成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txtInsertChoiceContent.Text = "";
                this.txtInsertChoiceA.Text = "";
                this.txtInsertChoiceB.Text = "";
                this.txtInsertChoiceC.Text = "";
                this.txtInsertChoiceD.Text = "";
                this.txtInsertChoiceE.Text = "";
                chkInsertChoiceA.Checked = false;
                chkInsertChoiceB.Checked = false;
                chkInsertChoiceC.Checked = false;
                chkInsertChoiceD.Checked = false;
                chkInsertChoiceE.Checked = false;
                ds = searchExamInfoDao.searchAllChoice(lessonInfoEntity);
                this.dgvInquireExam.DataSource = ds.Tables["SearchAllChoice"];
            }
        }
        #endregion


      

        #region 修改选择题按钮单击事件
        private void btnEditChoiceYes_Click(object sender, EventArgs e)
        {
            try
            {
                int message = judgeEn.judge1(txtEditChoice.Text, txtEditChoiceA.Text,
                                            txtEditChoiceB.Text, txtEditChoiceC.Text,
                                            txtEditChoiceD.Text, txtEditChoiceE.Text);
                if (editMessage(message) == false)
                    return;
                int i = this.dgvInquireExam.SelectedRows[0].Cells[0].RowIndex;
                lessInfoEntity.ChooseChoiceSubject = this.dgvInquireExam.Rows[i].Cells[0].Value + "";
                lessInfoEntity.ChoiceSubject = this.txtEditChoice.Text;
                lessInfoEntity.ChoiceContentA = this.txtEditChoiceA.Text;
                lessInfoEntity.ChoiceContentB = this.txtEditChoiceB.Text;
                lessInfoEntity.ChoiceContentC = this.txtEditChoiceC.Text;
                lessInfoEntity.ChoiceContentD = this.txtEditChoiceD.Text;
                lessInfoEntity.ChoiceContentE = this.txtEditChoiceE.Text;
                string answer = requestAnswer("");
                lessInfoEntity.ChoiceRightAnswer = answer;
                if (chkEditChoiceA.Checked == false && chkEditChoiceB.Checked == false
                && chkEditChoiceC.Checked == false
                && chkEditChoiceD.Checked == false && chkEditChoiceE.Checked == false)
                {
                    this.lbl28.Visible = true;
                    chkInsertChoiceA.Focus();
                    return;
                }
                bool flag = editExamInfoDao.editChoice(lessInfoEntity);
                if (flag == true)
                {
                    MessageBox.Show("修改成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ds = searchExamInfoDao.searchAllChoice(lessonInfoEntity);
                    this.dgvInquireExam.DataSource = ds.Tables["SearchAllChoice"];
                }
            }
            catch (Exception ex)
            { }
            chkEditChoiceA.Checked = false;
            chkEditChoiceB.Checked = false;
            chkEditChoiceC.Checked = false;
            chkEditChoiceD.Checked = false;
            chkEditChoiceE.Checked = false;
        }
        #endregion

        #region 添加问答题按钮单击事件
        private void btnInsertEssayQuestionYes_Click(object sender, EventArgs e)
        {
            int message = judgeEn.judge1(txtEssayQuestionContent.Text, txtEssayQuestionAnswer.Text);
            if (message == 0)
            {
                lbl19.Visible = true;
                txtEssayQuestionContent.Focus();
                return;
            }
            else if (message == 1)
            {
                lbl20.Visible = true;
                txtEssayQuestionAnswer.Focus();
                return;
            }
            lessInfoEntity.LessonId = int.Parse(this.cboLessonEssayQuestion.SelectedValue + "");
            lessInfoEntity.EssayQuestionSubject = this.txtEssayQuestionContent.Text;
            lessInfoEntity.EssayQuestionAnswer = this.txtEssayQuestionAnswer.Text;
            lessInfoEntity.EssayQuestionIsExist = 1;
            bool flag = insertExamInfo.insertEssayQuestion(lessInfoEntity);
            if (flag == true)
            {
                MessageBox.Show("添加成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txtEssayQuestionContent.Text = "";
                this.txtEssayQuestionAnswer.Text = "";
                this.txtEssayQuestionContent.Focus();
            }
        }
        #endregion

        #region 修改问答题按钮单击事件
        private void btnEditEssayQuestionYes_Click(object sender, EventArgs e)
        {
            int message = judgeEn.judge1(txtEditEssayQuestion.Text, txtEditEssayQuestionAnswer.Text);
            if (message == 0)
            {
                lbl31.Visible = true;
                txtEssayQuestionContent.Focus();
                return;
            }
            else if (message == 1)
            {
                lbl32.Visible = true;
                txtEssayQuestionAnswer.Focus();
                return;
            }
            int i = this.dgvInquireExam.SelectedRows[0].Cells[0].RowIndex;
            lessInfoEntity.ChooseEssayQuestionSubject = this.dgvInquireExam.Rows[i].Cells[0].Value + "";
            lessInfoEntity.EssayQuestionSubject = this.txtEditEssayQuestion.Text;
            lessInfoEntity.EssayQuestionAnswer = this.txtEditEssayQuestionAnswer.Text;
            bool flag = editExamInfoDao.editEssayQuestionInfo(lessInfoEntity);
            if (flag == true)
            {
                MessageBox.Show("修改成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ds = searchExamInfoDao.searchAllEssayQuestion(lessonInfoEntity);
                this.dgvInquireExam.DataSource = ds.Tables["SearchAllEssayQuestion"];
            }
        }
        #endregion

        #region 删除选择题和问答题题目
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int i = this.dgvInquireExam.SelectedRows[0].Cells[0].RowIndex;
            if (this.dgvInquireExam.DataSource == ds.Tables["SearchAllChoice"])
            {
                lessInfoEntity.ChoiceSubject = this.dgvInquireExam.Rows[i].Cells[0].Value + "";
                bool flag = editExamInfoDao.deleteChoice(lessInfoEntity);
                ds = searchExamInfoDao.searchAllChoice(lessonInfoEntity);
                this.dgvInquireExam.DataSource = ds.Tables["SearchAllChoice"];
                if (flag == true)
                    MessageBox.Show("删除成功");
            }
            else if (this.dgvInquireExam.DataSource == ds.Tables["SearchAllEssayQuestion"])
            {
                lessInfoEntity.EssayQuestionSubject = this.dgvInquireExam.Rows[i].Cells[0].Value + "";
                bool flag = editExamInfoDao.deleteEssayQuestionInfo(lessInfoEntity);
                ds = searchExamInfoDao.searchAllEssayQuestion(lessonInfoEntity);
                this.dgvInquireExam.DataSource = ds.Tables["SearchAllEssayQuestion"];
                if (flag == true)
                    MessageBox.Show("删除成功");
            }
        }
        #endregion

        #region 添加选择题窗体文本框失去鼠标焦点事件
        private void txtInsertChoiceContent_Leave(object sender, EventArgs e)
        {
            if (!txtInsertChoiceContent.Text.Equals(""))
            {
                lbl16.Visible = false;
            }
        }

        private void txtInsertChoiceA_Leave(object sender, EventArgs e)
        {
            if (!txtInsertChoiceB.Text.Equals(""))
            {
                lbl17.Visible = false;
            }
        }

        private void txtInsertChoiceB_Leave(object sender, EventArgs e)
        {
            if (!txtInsertChoiceB.Text.Equals(""))
            {
                lbl17.Visible = false;
            }
        }

        private void txtInsertChoiceC_Leave(object sender, EventArgs e)
        {
            if (!txtInsertChoiceC.Text.Equals(""))
            {
                lbl17.Visible = false;
            }
        }

        private void txtInsertChoiceD_Leave(object sender, EventArgs e)
        {
            if (!txtInsertChoiceD.Text.Equals(""))
            {
                lbl17.Visible = false;
            }
        }

        private void txtInsertChoiceE_TextChanged(object sender, EventArgs e)
        {
            if (!txtInsertChoiceE.Text.Equals(""))
            {
                lbl17.Visible = false;
            }
        }
        #endregion

        #region 添加问答题窗体文本框失去鼠标焦点事件
        private void txtEssayQuestionContent_Leave(object sender, EventArgs e)
        {
            if (!txtEssayQuestionContent.Text.Equals(""))
            {
                lbl19.Visible = false;
            }
        }

        private void txtEssayQuestionAnswer_TextChanged(object sender, EventArgs e)
        {
            if (!txtEssayQuestionAnswer.Text.Equals(""))
            {
                lbl20.Visible = false;
            }
        }
        #endregion



        #region 添加科目按钮单击事件
        private void btnInsertLessonYes_Click(object sender, EventArgs e)
        {
            lessInfoEntity.LessonName = this.txtLessonName.Text;
            lessInfoEntity.LessonIsExist = 1;
            if (this.txtLessonName.Text.Equals(""))
            {
                this.lbl23.Visible = true;
                this.txtLessonName.Focus();
                return;
            }
            else
            {
                this.lbl23.Visible = false;
            }
            bool flag = insertLessonNameDao.insertLessonName(lessInfoEntity);
            if (flag == true)
                //this.pnlMessageShowOk.Visible = true;
                //this.pnlInsertExam.Visible = false;
                MessageBox.Show("操作成功");
            this.txtLessonName.Text = "";
            this.txtLessonName.Focus();

            ds = searchLessonNameDao.searchLessonName();
            repalceComboBox(cboLessonChoice, ds.Tables["LessonNameInfo"], "LessonId", "LessonName");

            ds = searchLessonNameDao.searchLessonName();
            repalceComboBox(cboLessonEssayQuestion, ds.Tables["LessonNameInfo"], "LessonId", "LessonName");

            ds = searchLessonNameDao.searchLessonName();
            repalceComboBox(cboInquireCoursesType, ds.Tables["LessonNameInfo"], "LessonId", "LessonName");

            ds = searchLessonNameDao.searchLessonName();
            repalceComboBox(cboChoiceLessonDel, ds.Tables["LessonNameInfo"], "LessonId", "LessonName");

        }
        #endregion

        #region 删除科目按钮单击事件
        private void btnDeleteLessonName_Click(object sender, EventArgs e)
        {
            try
            {
                lessInfoEntity.LessonId = int.Parse(this.cboChoiceLessonDel.SelectedValue + "");
                bool flag = editLessNameDao.deleteLesson(lessInfoEntity);
                if (flag == true)
                    MessageBox.Show("删除成功");
                ds = searchLessonNameDao.searchLessonName();
                repalceComboBox(cboLessonChoice, ds.Tables["LessonNameInfo"], "LessonId", "LessonName");

                ds = searchLessonNameDao.searchLessonName();
                repalceComboBox(cboLessonEssayQuestion, ds.Tables["LessonNameInfo"], "LessonId", "LessonName");

                ds = searchLessonNameDao.searchLessonName();
                repalceComboBox(cboInquireCoursesType, ds.Tables["LessonNameInfo"], "LessonId", "LessonName");

                ds = searchLessonNameDao.searchLessonName();
                repalceComboBox(cboChoiceLessonDel, ds.Tables["LessonNameInfo"], "LessonId", "LessonName");
            }
            catch (Exception ex)
            {
                MessageBox.Show("请选择你要删除的科目");
            }
        }
        #endregion

        #region 修改科目按钮单击事件
        private void btnEditLessonName_Click(object sender, EventArgs e)
        {
            try
            {
                lessInfoEntity.LessonId = int.Parse(this.cboChoiceLessonDel.SelectedValue + "");
                lessInfoEntity.LessonName = this.txtEditLessonName.Text;
                if (this.txtEditLessonName.Text.Equals(""))
                {
                    this.lbl24.Visible = true;
                    this.txtEditLessonName.Focus();
                    return;
                }
                else
                {
                    this.lbl24.Visible = false;
                }
                bool flag = editLessNameDao.editLesson(lessInfoEntity);
                if (flag == true)
                    MessageBox.Show("修改成功");
                ds = searchLessonNameDao.searchLessonName();
                repalceComboBox(cboLessonChoice, ds.Tables["LessonNameInfo"], "LessonId", "LessonName");

                ds = searchLessonNameDao.searchLessonName();
                repalceComboBox(cboLessonEssayQuestion, ds.Tables["LessonNameInfo"], "LessonId", "LessonName");

                ds = searchLessonNameDao.searchLessonName();
                repalceComboBox(cboInquireCoursesType, ds.Tables["LessonNameInfo"], "LessonId", "LessonName");

                ds = searchLessonNameDao.searchLessonName();
                repalceComboBox(cboChoiceLessonDel, ds.Tables["LessonNameInfo"], "LessonId", "LessonName");
            }
            catch (Exception ex)
            {
                MessageBox.Show("请选择你要修改的科目");
            }
            this.txtEditLessonName.Text = "";
            this.txtEditLessonName.Focus();
        }
        #endregion


        #region 查询题库按钮单击时间
        private void btnInquire_Click(object sender, EventArgs e)
        {
            try
            {
                lessonInfoEntity.LessonId = int.Parse(this.cboInquireCoursesType.SelectedValue + "");
                if (this.cboInquireExamType.SelectedItem.Equals("选择题"))
                {
                    this.lbl25.Visible = false;
                    ds = searchExamInfoDao.searchAllChoice(lessonInfoEntity);
                    this.dgvInquireExam.DataSource = ds.Tables["SearchAllChoice"];
                }
                else if (this.cboInquireExamType.SelectedItem.Equals("问答题"))
                {
                    this.lbl25.Visible = false;
                    ds = searchExamInfoDao.searchAllEssayQuestion(lessonInfoEntity);
                    this.dgvInquireExam.DataSource = ds.Tables["SearchAllEssayQuestion"];
                }
            }
            catch (Exception ex)
            {
                this.lbl25.Visible = true;
                this.cboInquireCoursesType.Focus();
                return;
            }
        }
        #endregion

        #region 查询班级按钮单击事件
        private void btnInquireClass_Click(object sender, EventArgs e)
        {
            if (this.cboInquieClassType.Text.Equals(""))
            {
                this.lbl30.Visible = true;
                this.cboInquieClassType.Focus();
                return;
            }
            if (this.cboInquieClassType.SelectedItem.Equals("班级名称"))
            {
                ClassEntity.ClassSearchContent = this.txtInquireClass.Text;
                ds = searchClassInfoDao.searchClassByName(ClassEntity);
                this.dgdInquireClass.DataSource = null;
                this.dgdInquireClass.DataSource = ds.Tables["SearchClassByName"];
                this.lbl30.Visible = false;
            }
            else if (this.cboInquieClassType.SelectedItem.Equals("班级人数"))
            {
                ClassEntity.ClassStuNum = int.Parse(this.txtInquireClass.Text);
                ds = searchClassInfoDao.searchClassByStuNum(ClassEntity);
                this.dgdInquireClass.DataSource = null;
                this.dgdInquireClass.DataSource = ds.Tables["SearchClassByStuNum"];
                this.lbl30.Visible = false;
            }
            else if (this.cboInquieClassType.SelectedItem.Equals("班级编号"))
            {
                ClassEntity.ClassId = int.Parse(this.txtInquireClass.Text);
                ds = searchClassInfoDao.searchClassById(ClassEntity);
                this.dgdInquireClass.DataSource = null;
                this.dgdInquireClass.DataSource = ds.Tables["SearchClassById"];
                this.lbl30.Visible = false;
            }
        }
        #endregion

        #region 题库表网格选中行单击事件
        private void dgvInquireExam_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)this.dgvInquireExam.DataSource;
                int row = e.RowIndex;
                int col = e.ColumnIndex;
                if (this.dgvInquireExam.DataSource == ds.Tables["SearchAllChoice"])
                {
                    this.txtEditChoice.Text = dt.Rows[row][0] + "";
                    this.txtEditChoiceA.Text = dt.Rows[row][1] + "";
                    this.txtEditChoiceB.Text = dt.Rows[row][2] + "";
                    this.txtEditChoiceC.Text = dt.Rows[row][3] + "";
                    this.txtEditChoiceD.Text = dt.Rows[row][4] + "";
                    this.txtEditChoiceE.Text = dt.Rows[row][5] + "";
                    this.tabControl1.TabPages.Clear();
                    this.tabControl1.Controls.Add(this.tabPage1);
                    this.tabControl1.Controls.Add(this.tabPage2);
                    this.txtEditEssayQuestion.Text = "";
                    this.txtEditEssayQuestionAnswer.Text = "";
                }
                else if (this.dgvInquireExam.DataSource == ds.Tables["SearchAllEssayQuestion"])
                {
                    try
                    {
                        this.txtEditEssayQuestion.Text = dt.Rows[row][0] + "";
                        this.txtEditEssayQuestionAnswer.Text = dt.Rows[row][1] + "";
                        this.tabControl1.TabPages.Clear();
                        this.tabControl1.Controls.Add(this.tabPage2);
                        this.tabControl1.Controls.Add(this.tabPage1);
                        this.txtEditChoice.Text = "";
                        this.txtEditChoiceA.Text = "";
                        this.txtEditChoiceB.Text = "";
                        this.txtEditChoiceC.Text = "";
                        this.txtEditChoiceD.Text = "";
                        this.txtEditChoiceE.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("aaa");
                    }
                }
                i = row;
            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region 查看上下选择题和问答题按钮单击事件
        int i = 0;
        int j = 0;
        private void btnEditChoiceUp_Click(object sender, EventArgs e)
        {
            try
            {
                i--;
                if (i < 0)
                    i = 0;
                if (i >= 0 && i < dgvInquireExam.Rows.Count)
                {
                    DataTable dt = (DataTable)this.dgvInquireExam.DataSource;
                    this.txtEditChoice.Text = dt.Rows[i][0] + "";
                    this.txtEditChoiceA.Text = dt.Rows[i][1] + "";
                    this.txtEditChoiceB.Text = dt.Rows[i][2] + "";
                    this.txtEditChoiceC.Text = dt.Rows[i][3] + "";
                    this.txtEditChoiceD.Text = dt.Rows[i][4] + "";
                    this.txtEditChoiceE.Text = dt.Rows[i][5] + "";
                }
            }
            catch (Exception ex)
            { }

        }
        private void btnEditChoiceNext_Click(object sender, EventArgs e)
        {
            try
            {
                i++;
                if (i >= this.dgvInquireExam.Rows.Count)
                    i = this.dgvInquireExam.Rows.Count - 1;
                if (i < this.dgvInquireExam.Rows.Count && i >= 0)
                {
                    DataTable dt = (DataTable)this.dgvInquireExam.DataSource;
                    this.txtEditChoice.Text = dt.Rows[i][0] + "";
                    this.txtEditChoiceA.Text = dt.Rows[i][1] + "";
                    this.txtEditChoiceB.Text = dt.Rows[i][2] + "";
                    this.txtEditChoiceC.Text = dt.Rows[i][3] + "";
                    this.txtEditChoiceD.Text = dt.Rows[i][4] + "";
                    this.txtEditChoiceE.Text = dt.Rows[i][5] + "";
                }
            }
            catch (Exception ex)
            { 
            }
        }
        private void btnEditEssayQuestionUp_Click(object sender, EventArgs e)
        {

            try
            {
                j--;
                if (j < 0)
                    j = 0;
                if (j >= 0 && j < dgvInquireExam.Rows.Count)
                {
                    DataTable dt = (DataTable)this.dgvInquireExam.DataSource;
                    this.txtEditEssayQuestion.Text = dt.Rows[j][0] + "";
                    this.txtEditEssayQuestionAnswer.Text = dt.Rows[j][1] + "";
                }
            }
            catch (Exception ex)
            { }

        }
        private void btnEditEssayQuestionNext_Click(object sender, EventArgs e)
        {
            try
            {
                j++;
                if (j >= this.dgvInquireExam.Rows.Count)
                    j = this.dgvInquireExam.Rows.Count - 1;
                if (j < this.dgvInquireExam.Rows.Count && j >= 0)
                {
                    DataTable dt = (DataTable)this.dgvInquireExam.DataSource;
                    this.txtEditEssayQuestion.Text = dt.Rows[j][0] + "";
                    this.txtEditEssayQuestionAnswer.Text = dt.Rows[j][1] + "";
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region 修改班级按钮单击事件
        private void btnEditClassSave_Click(object sender, EventArgs e)
        {
            int message = judgeEn.judge1(txtEditClass.Text, txtEditClassNum.Text);
            if (message == 0)
            {
                lbl33.Visible = true;
                txtEditClass.Focus();
                return;
            }
            else if (message == 1)
            {
                lbl34.Visible = true;
                txtEditClassNum.Focus();
                return;
            }
            if (this.rboFinishClassNo.Checked == true)
            {
                ClassEntity.ClassFinishTime = "开班中";
            }
            if (this.rboFinishClassYes.Checked == true)
            {
                ClassEntity.ClassFinishTime = dtpEditClassFinish.Text;
            }
            ClassEntity.ClassName = txtEditClass.Text;
            try
            {
                ClassEntity.ClassStuNum = int.Parse(txtEditClassNum.Text);
            }
            catch (Exception ex)
            {
                this.lbl35.Visible = true;
                txtEditClassNum.Focus();
                return;
            }
            ClassEntity.ClassTeacherId = int.Parse(cboEditClassTeacher.SelectedValue + "");
            ClassEntity.TeacherId = int.Parse(cboEditTeacher.SelectedValue + "");
            int i = this.dgdInquireClass.SelectedRows[0].Cells[0].RowIndex;
            if (this.dgdInquireClass.DataSource == ds.Tables["SearchClassByName"])
            {
                ClassEntity.ChooseClassName = this.dgdInquireClass.Rows[i].Cells[0].Value + "";
                bool flag = editClassDao.editClassInfo(ClassEntity);
                if (flag == true)
                    this.lbl35.Visible = false;
                MessageBox.Show("修改成功");
                ds = searchClassInfoDao.searchClassByName(ClassEntity);
                this.dgdInquireClass.DataSource = ds.Tables["SearchClassByName"];
            }
            else if (this.dgdInquireClass.DataSource == ds.Tables["SearchClassByStuNum"])
            {
                ClassEntity.ChooseClassName = this.dgdInquireClass.Rows[i].Cells[1].Value + "";
                bool flag = editClassDao.editClassInfo(ClassEntity);
                if (flag == true)
                    this.lbl35.Visible = false;
                MessageBox.Show("修改成功");
                ds = searchClassInfoDao.searchClassByStuNum(ClassEntity);
                this.dgdInquireClass.DataSource = ds.Tables["SearchClassByStuNum"];
            }
            else if (this.dgdInquireClass.DataSource == ds.Tables["SearchClassById"])
            {
                ClassEntity.ChooseClassName = this.dgdInquireClass.Rows[i].Cells[1].Value + "";
                bool flag = editClassDao.editClassInfo(ClassEntity);
                if (flag == true)
                    this.lbl35.Visible = false;
                MessageBox.Show("修改成功");
                ds = searchClassInfoDao.searchClassById(ClassEntity);
                this.dgdInquireClass.DataSource = ds.Tables["SearchClassById"];
            }
            txtEditClass.Text = "";
            txtEditClassNum.Text = "";
        }
        #endregion

        #region 班级表网格选中行单击事件
        private void dgdInquireClass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)this.dgdInquireClass.DataSource;
                int row = e.RowIndex;
                int col = e.ColumnIndex;
                if (this.dgdInquireClass.DataSource == ds.Tables["SearchClassByName"])
                {
                    this.txtEditClass.Text = dt.Rows[row][0] + "";
                    this.txtEditClassNum.Text = dt.Rows[row][3] + "";

                }
                else if (this.dgdInquireClass.DataSource == ds.Tables["SearchClassByStuNum"])
                {
                    this.txtEditClass.Text = dt.Rows[row][1] + "";
                    this.txtEditClassNum.Text = dt.Rows[row][4] + "";

                }
                else if (this.dgdInquireClass.DataSource == ds.Tables["SearchClassById"])
                {
                    this.txtEditClass.Text = dt.Rows[row][1] + "";
                    this.txtEditClassNum.Text = dt.Rows[row][4] + "";

                }
            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region 删除班级按钮单击事件
        private void btnDeleteClass_Click(object sender, EventArgs e)
        {
            int i = this.dgdInquireClass.SelectedRows[0].Cells[0].RowIndex;
            if (this.dgdInquireClass.DataSource == ds.Tables["SearchClassByName"])
            {
                ClassEntity.ClassName = this.dgdInquireClass.Rows[i].Cells[0].Value + "";
                bool flag = editClassDao.deleteClassByName(ClassEntity);
                if (flag == true)
                {
                    MessageBox.Show("删除成功");
                }
                ds = searchClassInfoDao.searchClassByName(ClassEntity);
                this.dgdInquireClass.DataSource = ds.Tables["SearchClassByName"];
            }
            else if (this.dgdInquireClass.DataSource == ds.Tables["SearchClassByStuNum"])
            {
                ClassEntity.ClassName = this.dgdInquireClass.Rows[i].Cells[1].Value + "";
                bool flag = editClassDao.deleteClassByName(ClassEntity);
                if (flag == true)
                {
                    MessageBox.Show("删除成功");
                }
                ds = searchClassInfoDao.searchClassByStuNum(ClassEntity);
                this.dgdInquireClass.DataSource = ds.Tables["SearchClassByStuNum"];
            }
            else if (this.dgdInquireClass.DataSource == ds.Tables["SearchClassById"])
            {
                ClassEntity.ClassName = this.dgdInquireClass.Rows[i].Cells[1].Value + "";
                bool flag = editClassDao.deleteClassByName(ClassEntity);
                if (flag == true)
                {
                    MessageBox.Show("删除成功");
                }
                ds = searchClassInfoDao.searchClassById(ClassEntity);
                this.dgdInquireClass.DataSource = ds.Tables["SearchClassById"];
            }
        }
        #endregion

        #region 是否结班按钮单击事件
        private void rboFinishClassNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rboFinishClassNo.Checked == true)
            {
                this.dtpEditClassFinish.Enabled = false;
            }
            else
                this.dtpEditClassFinish.Enabled = true;
        }
        #endregion

        #region 修改班级窗体文本框失去鼠标焦点事件
        private void txtEditClass_Leave(object sender, EventArgs e)
        {
            if (!txtEditClass.Text.Equals(""))
                lbl33.Visible = false;
        }
        private void txtEditClassNum_Leave(object sender, EventArgs e)
        {
            if (!txtEditClassNum.Text.Equals(""))
                lbl34.Visible = false;
        }
        #endregion

        #region 人员调动按钮单击事件
        private void btnRemoveYes_Click(object sender, EventArgs e)
        {
          
            if (cboUserName.Enabled == false ||cboUserClassRemove.Enabled == false)
            {
                lbl51.Visible = true;
                cboUserClass.Focus();
                return;
            }
            else
            {
                lbl51.Visible = false;
            }
            userEntity.UserLoginName = cboUserName.SelectedValue + "";
            ds = removeUserDao.searchClassByStuLoginName(userEntity);
            userEntity.StuInitializationClass1 = ds.Tables["SearchClassByStuLoginName"].Rows[0][1] + "";//获取学生原来的班级

            userEntity.UserRemoveTime = dtpRemove.Text;
            userEntity.UserClassId=int.Parse(cboUserClassRemove.SelectedValue+"");//学生转入的班级

            bool flag = removeUserDao.updateStuToClass(userEntity);
            if(flag==true)
            {
                MessageBox.Show("修改成功");
            }

            ds2 = removeUserDao.searchClassByStuLoginName1(userEntity);
            userEntity.UserRemoveClass1 = ds2.Tables["SearchClassByStuLoginName1"].Rows[0][1] + "";//获取转班记录
            removeUserDao.updateStuClassInfo(userEntity);
            ds = removeUserDao.searchStuRemoveInfo();
            dgvUserRemove.DataSource = ds.Tables["SearchStuRemoveInfo"];
        }
        #endregion

        #region 查询所有用户方法
        private void addData(DataTable dt, params DataTable[] fromBLLALL)
        {
            foreach (DataTable fromBLL in fromBLLALL)
            {
                for (int i = 0; i < fromBLL.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();//建立空行
                    dr[0] = fromBLL.Rows[i][0];
                    dr[1] = fromBLL.Rows[i][1];
                    dr[2] = fromBLL.Rows[i][2];
                    dr[3] = fromBLL.Rows[i][3];
                    dr[4] = fromBLL.Rows[i][4];
                    dr[5] = fromBLL.Rows[i][5];
                    dr[6] = fromBLL.Rows[i][6];
                    dr[7] = fromBLL.Rows[i][7];
                    dr[8] = fromBLL.Rows[i][8];
                    dr[9] = fromBLL.Rows[i][9];
                    dr[10] = fromBLL.Rows[i][10];
                    dr[11] = fromBLL.Rows[i][11];
                    dt.Rows.Add(dr);//添加一行
                }
            }
        }
        #endregion

        #region 查询用户按钮单击事件
        private void btnInquireUser_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("姓名");
            dt.Columns.Add("用户名");
            dt.Columns.Add("密码");
            dt.Columns.Add("入职/入校时间");
            dt.Columns.Add("离职/入校时间");
            dt.Columns.Add("身份证号");
            dt.Columns.Add("年龄");
            dt.Columns.Add("出生年月");
            dt.Columns.Add("联系电话");
            dt.Columns.Add("性别");
            dt.Columns.Add("家庭住址");
            dt.Columns.Add("身份");
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();
            DataSet ds3 = new DataSet();
            try
            {
                userEntity.UserName = this.txtInquireContent.Text;
                if (this.cboInquireType.SelectedItem.Equals("管理员") && this.cboInquireTypeIdentity.SelectedItem.Equals("一级管理员"))
                {
                    ds = searchUserInfoDao.searchFirstLeaveManagerByName(userEntity);
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchFirstLeaveManagerByName"];
                }
                else if (this.cboInquireType.SelectedItem.Equals("管理员") && this.cboInquireTypeIdentity.SelectedItem.Equals("普通管理员"))
                {
                    ds = searchUserInfoDao.searchManagerByName(userEntity);
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchManagerByName"];
                }
                else if (this.cboInquireType.SelectedItem.Equals("班主任") && this.cboInquireTypeIdentity.SelectedItem.Equals("姓名"))
                {
                    ds = searchUserInfoDao.searchClassTeacherByName(userEntity);
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchClassTeacherByName"];
                }
                else if (this.cboInquireType.SelectedItem.Equals("班主任") && this.cboInquireTypeIdentity.SelectedItem.Equals("年龄"))
                {
                    userEntity.UserAge = int.Parse(this.txtInquireContent.Text);
                    ds = searchUserInfoDao.searchClassTeacherByAge(userEntity);
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchClassTeacherByAge"];
                }
                else if (this.cboInquireType.SelectedItem.Equals("教员") && this.cboInquireTypeIdentity.SelectedItem.Equals("姓名"))
                {
                    ds = searchUserInfoDao.searchTeacherByName(userEntity);
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchTeacherByName"];
                }
                else if (this.cboInquireType.SelectedItem.Equals("教员") && this.cboInquireTypeIdentity.SelectedItem.Equals("年龄"))
                {
                    userEntity.UserAge = int.Parse(this.txtInquireContent.Text);
                    ds = searchUserInfoDao.searchTeacherByAge(userEntity);
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchTeacherByAge"];
                }
                else if (this.cboInquireType.SelectedItem.Equals("学生") && this.cboInquireTypeIdentity.SelectedItem.Equals("姓名"))
                {
                    ds = searchUserInfoDao.searchStuByName(userEntity);
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchStuByName"];
                }
                else if (this.cboInquireType.SelectedItem.Equals("学生") && this.cboInquireTypeIdentity.SelectedItem.Equals("年龄"))
                {
                    userEntity.UserAge = int.Parse(this.txtInquireContent.Text);
                    ds = searchUserInfoDao.searchStuByAge(userEntity);
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchStuByAge"];
                }
                else if (this.cboInquireType.SelectedItem.Equals("学生") && this.cboInquireTypeIdentity.SelectedItem.Equals("学号"))
                {
                    userEntity.UserId = int.Parse(this.txtInquireContent.Text);
                    ds = searchUserInfoDao.searchStuById(userEntity);
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchStuById"];
                }
                else if (this.cboInquireType.SelectedItem.Equals("所有"))
                {
                   
                    ds = searchAllUserInfo.searchAllManage();
                    ds1 = searchAllUserInfo.searchAllClassTeacher();
                    ds2 = searchAllUserInfo.searchAllTeacher();
                    ds3 = searchAllUserInfo.searchAllStudent();
                    DataTable fromBLL = ds.Tables["SearchAllManage"];
                    DataTable fromBLL1 = ds1.Tables["SearchAllClassTeacher"];
                    DataTable fromBLL2 = ds2.Tables["SearchAllTeacher"];
                    DataTable fromBLL3 = ds3.Tables["SearchAllStudent"];
                    addData(dt, fromBLL,fromBLL1,fromBLL2,fromBLL3);
                    this.dgdUser.DataSource = dt;
                }
                else if (this.cboInquireType.SelectedItem.Equals("姓名"))
                {
                    userEntity.UserName = txtInquireContent.Text;
                    ds = searchAllUserInfo.searchAllManageBlur(userEntity);
                    ds1 = searchAllUserInfo.searchAllClassTeacherBlur(userEntity);
                    ds2 = searchAllUserInfo.searchAllTeacherBlur(userEntity);
                    ds3 = searchAllUserInfo.searchAllStudentBlur(userEntity);
                    DataTable fromBLL = ds.Tables["SearchAllManageBlur"];
                    DataTable fromBLL1 = ds1.Tables["SearchAllClassTeacherBlur"];
                    DataTable fromBLL2 = ds2.Tables["SearchAllTeacherBlur"];
                    DataTable fromBLL3 = ds3.Tables["SearchAllStudentBlur"];
                    addData(dt, fromBLL, fromBLL1, fromBLL2, fromBLL3);
                    this.dgdUser.DataSource = dt;                    
                }
                else if (this.cboInquireType.SelectedItem.Equals("性别") && this.cboInquireTypeIdentity.SelectedItem.Equals("男"))
                {
                    userEntity.UserName = txtInquireContent.Text;
                    ds = searchAllUserInfo.searchMaleManage(userEntity);
                    ds1 = searchAllUserInfo.searchMaleClassTeacher(userEntity);
                    ds2 = searchAllUserInfo.searchMaleTeacher(userEntity);
                    ds3 = searchAllUserInfo.searchMaleStudent(userEntity);
                    DataTable fromBLL = ds.Tables["SearchMaleManage"];
                    DataTable fromBLL1 = ds1.Tables["SearchMaleClassTeacher"];
                    DataTable fromBLL2 = ds2.Tables["SearchMaleTeacher"];
                    DataTable fromBLL3 = ds3.Tables["SearchMaleStudent"];
                    addData(dt, fromBLL, fromBLL1, fromBLL2, fromBLL3);
                    this.dgdUser.DataSource = dt;
                }
                else if (this.cboInquireType.SelectedItem.Equals("性别") && this.cboInquireTypeIdentity.SelectedItem.Equals("女"))
                {
                    userEntity.UserName = txtInquireContent.Text;
                    ds = searchAllUserInfo.searchFemaleManage(userEntity);
                    ds1 = searchAllUserInfo.searchFemaleClassTeacher(userEntity);
                    ds2 = searchAllUserInfo.searchFemaleTeacher(userEntity);
                    ds3 = searchAllUserInfo.searchFemaleStudent(userEntity);
                    DataTable fromBLL = ds.Tables["SearchFemaleManage"];
                    DataTable fromBLL1 = ds1.Tables["SearchFemaleClassTeacher"];
                    DataTable fromBLL2 = ds2.Tables["SearchFemaleTeacher"];
                    DataTable fromBLL3 = ds3.Tables["SearchFemaleStudent"];
                    addData(dt, fromBLL, fromBLL1, fromBLL2, fromBLL3);
                    this.dgdUser.DataSource = dt;
                }
            }
            catch (Exception ex)
            { }
            this.txtInquireContent.Text = "";
        }
        #endregion

        #region 查询用户类型下拉框选取值改变事件
        private void cboInquireType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cboInquireType.SelectedItem.Equals("性别"))
            {
                this.btnEditUser.Enabled = false;
                this.btnDeleteUser.Enabled = false;
                this.txtInquireContent.Enabled = true;
                this.cboInquireTypeIdentity.Enabled = true;
                this.cboInquireTypeIdentity.Items.Clear();
          
                this.cboInquireTypeIdentity.Items.Add("男");
                this.cboInquireTypeIdentity.Items.Add("女");
                this.cboInquireTypeIdentity.SelectedItem = "男";
            }
            else if (this.cboInquireType.SelectedItem.Equals("学生"))
            {
                this.txtInquireContent.Enabled = true;
                this.btnEditUser.Enabled = true;
                this.btnDeleteUser.Enabled = true;
                this.cboInquireTypeIdentity.Enabled = true;
                this.cboInquireTypeIdentity.Items.Clear();
                this.cboInquireTypeIdentity.Items.Add("姓名");
                this.cboInquireTypeIdentity.Items.Add("学号");
                this.cboInquireTypeIdentity.Items.Add("年龄");

                this.cboInquireTypeIdentity.SelectedItem = "姓名";
            }
            else if (this.cboInquireType.SelectedItem.Equals("教员") || this.cboInquireType.SelectedItem.Equals("班主任"))
            {
                this.txtInquireContent.Enabled = true;
                this.btnEditUser.Enabled = true;
                this.btnDeleteUser.Enabled = true;
                this.cboInquireTypeIdentity.Enabled = true;
                this.cboInquireTypeIdentity.Items.Clear();
                this.cboInquireTypeIdentity.Items.Add("姓名");
                this.cboInquireTypeIdentity.Items.Add("年龄");
                this.cboInquireTypeIdentity.SelectedItem = "姓名";
            }
            else if (this.cboInquireType.SelectedItem.Equals("管理员"))
            {
                this.txtInquireContent.Enabled = true;
                this.btnEditUser.Enabled = true;
                this.btnDeleteUser.Enabled = true;
                this.cboInquireTypeIdentity.Enabled = true;
                this.cboInquireTypeIdentity.Items.Clear();
                this.cboInquireTypeIdentity.Items.Add("一级管理员");
                this.cboInquireTypeIdentity.Items.Add("普通管理员");
                this.cboInquireTypeIdentity.SelectedItem = "一级管理员";
            }
            else if (this.cboInquireType.SelectedItem.Equals("所有"))
            {
                this.txtInquireContent.Enabled = false;
                this.btnEditUser.Enabled = false;
                this.btnDeleteUser.Enabled = false;
                this.cboInquireTypeIdentity.Enabled = false;
                this.cboInquireTypeIdentity.Items.Clear();
                this.label12.Text = "请输入要查询的内容:";
            }
            else if (this.cboInquireType.SelectedItem.Equals("姓名"))
            {
                this.txtInquireContent.Enabled = true;
                this.btnEditUser.Enabled = false;
                this.btnDeleteUser.Enabled = false;
                this.label12.Text = "请输入要查询的姓名";
                this.cboInquireTypeIdentity.Enabled = false;
                this.cboInquireTypeIdentity.Items.Clear();
            }
            else
            {
                this.cboInquireTypeIdentity.Enabled = false;

            } 
        }     
       

        private void cboInquireTypeIdentity_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cboInquireTypeIdentity.SelectedItem.Equals("姓名")||
                this.cboInquireTypeIdentity.SelectedItem.Equals("一级管理员")||
                this.cboInquireTypeIdentity.SelectedItem.Equals("普通管理员")||
                this.cboInquireTypeIdentity.SelectedItem.Equals("所有")||
                this.cboInquireTypeIdentity.SelectedItem.Equals("男")||
                this.cboInquireTypeIdentity.SelectedItem.Equals("女"))
            {
                this.label12.Text = "请输入要查询的姓名";
            }
            else if (this.cboInquireTypeIdentity.SelectedItem.Equals("学号"))
            {
                this.label12.Text = "请输入要查询的学号";
            }
            else if (this.cboInquireTypeIdentity.SelectedItem.Equals("年龄"))
            {
                this.label12.Text = "请输入要查询的年龄";
            }

            else 
            {
                this.label12.Text = "请输入要查询的内容:";
            }
        }
        #endregion

        #region 用户查询表网格单击事件
        private void dgdUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string select = this.cboRegisterIdentity.SelectedValue + "";
            int selectValue = int.Parse(select);
            try
            {
                DataTable dt = (DataTable)this.dgdUser.DataSource;
                int row = e.RowIndex;
                int col = e.ColumnIndex;
                if (this.dgdUser.DataSource == ds.Tables["SearchFirstLeaveManagerByName"])//一级管理员姓名
                {
                   
                    this.cboEditrClass.Enabled = false;
                    this.txtEditStuID.Enabled = false;
                    this.txtEditName.Text = dt.Rows[row][0] + "";
                    this.txtEditID.Text = dt.Rows[row][1] + "";
                    this.txtEditPassWord.Text = dt.Rows[row][2] + "";
                    this.dtpEditWork.Text = dt.Rows[row][3] + "";
                    this.txtEditDimission.Text = dt.Rows[row][4] + "";
                    this.txtEditIdCard.Text = dt.Rows[row][5] + "";
                    this.txtEditAge.Text = dt.Rows[row][6] + "";
                    this.dtpEdit.Text = dt.Rows[row][7] + "";
                    this.txtEditPhone.Text = dt.Rows[row][8] + "";
                    if (dt.Rows[row][9].Equals("男"))
                    {
                        rboEditMale.Checked=true;
                    }
                    else
                    {
                        rboEditFemale.Checked = true;
                    }
                    this.txtEditAddress.Text = dt.Rows[row][10] + "";

                    if (dt.Rows[row][11].Equals("一级管理员"))
                    {
                        this.cboEditIdentity.SelectedItem = "管理员";
                        this.chkEditPopedom.Checked = true;
                    }
                    else
                    {
                        this.chkEditPopedom.Checked = false;
                    }
                }
                else if (this.dgdUser.DataSource == ds.Tables["SearchManagerByName"])//普通管理员姓名
                {
                    this.cboEditIdentity.SelectedItem = "管理员";
                    this.chkEditPopedom.Checked = false;

                    this.cboEditrClass.Enabled = false;
                    this.txtEditStuID.Enabled = false;
                    this.txtEditName.Text = dt.Rows[row][0] + "";
                    this.txtEditID.Text = dt.Rows[row][1] + "";
                    this.txtEditPassWord.Text = dt.Rows[row][2] + "";
                    this.dtpEditWork.Text = dt.Rows[row][3] + "";
                    this.txtEditDimission.Text = dt.Rows[row][4] + "";
                    this.txtEditIdCard.Text = dt.Rows[row][5] + "";
                    this.txtEditAge.Text = dt.Rows[row][6] + "";
                    this.dtpEdit.Text = dt.Rows[row][7] + "";
                    this.txtEditPhone.Text = dt.Rows[row][8] + "";
                    if (dt.Rows[row][9].Equals("男"))
                    {
                        rboEditMale.Checked = true;
                    }
                    else
                    {
                        rboEditFemale.Checked = true;
                    }
                    this.txtEditAddress.Text = dt.Rows[row][10] + "";

                }
                else if (this.dgdUser.DataSource == ds.Tables["SearchClassTeacherByName"])//班主任姓名
                {
                    this.cboEditIdentity.SelectedItem = "教师";
                    this.chkEditClassTeacher.Checked = true;

                    this.cboEditrClass.Enabled = false;
                    this.txtEditStuID.Enabled = false;
                    this.txtEditName.Text = dt.Rows[row][0] + "";
                    this.txtEditID.Text = dt.Rows[row][1] + "";
                    this.txtEditPassWord.Text = dt.Rows[row][2] + "";
                    this.dtpEditWork.Text = dt.Rows[row][3] + "";
                    this.txtEditDimission.Text = dt.Rows[row][4] + "";
                    this.txtEditIdCard.Text = dt.Rows[row][5] + "";
                    this.txtEditAge.Text = dt.Rows[row][6] + "";
                    this.dtpEdit.Text = dt.Rows[row][7] + "";
                    this.txtEditPhone.Text = dt.Rows[row][8] + "";
                    if (dt.Rows[row][9].Equals("男"))
                    {
                        rboEditMale.Checked = true;
                    }
                    else
                    {
                        rboEditFemale.Checked = true;
                    }
                    this.txtEditAddress.Text = dt.Rows[row][10] + "";
                    this.cboEditIdentity.Text = dt.Rows[row][11] + "";//身份
                }
                else if (this.dgdUser.DataSource == ds.Tables["SearchClassTeacherByAge"])//班主任年龄
                {
                    this.cboEditIdentity.SelectedItem = "教师";
                    this.chkEditClassTeacher.Checked = true;

                    this.cboEditrClass.Enabled = false;
                    this.txtEditStuID.Enabled = false;
                    this.txtEditName.Text = dt.Rows[row][0] + "";  
                    this.txtEditAge.Text = dt.Rows[row][1] + "";
                    this.txtEditID.Text = dt.Rows[row][2] + "";
                    this.txtEditPassWord.Text = dt.Rows[row][3] + "";
                    this.dtpEditWork.Text = dt.Rows[row][4] + "";
                    this.txtEditDimission.Text = dt.Rows[row][5] + "";
                    this.txtEditIdCard.Text = dt.Rows[row][6] + "";
                    this.dtpEdit.Text = dt.Rows[row][7] + "";
                    this.txtEditPhone.Text = dt.Rows[row][8] + "";
                    if (dt.Rows[row][9].Equals("男"))
                    {
                        rboEditMale.Checked = true;
                    }
                    else
                    {
                        rboEditFemale.Checked = true;
                    }
                    this.txtEditAddress.Text = dt.Rows[row][10] + "";
                    this.cboEditIdentity.Text = dt.Rows[row][11] + "";//身份
                }
                else if (this.dgdUser.DataSource == ds.Tables["SearchTeacherByName"])//教员姓名
                {
                    this.cboEditIdentity.SelectedItem = "教师";
                    this.chkEditClassTeacher.Checked = false;

                    this.cboEditrClass.Enabled = false;
                    this.txtEditStuID.Enabled = false;
                    this.txtEditName.Text = dt.Rows[row][0] + "";
                    this.txtEditID.Text = dt.Rows[row][1] + "";
                    this.txtEditPassWord.Text = dt.Rows[row][2] + "";
                    this.dtpEditWork.Text = dt.Rows[row][3] + "";
                    this.txtEditDimission.Text = dt.Rows[row][4] + "";
                    this.txtEditIdCard.Text = dt.Rows[row][5] + "";
                    this.txtEditAge.Text = dt.Rows[row][6] + "";
                    this.dtpEdit.Text = dt.Rows[row][7] + "";
                    this.txtEditPhone.Text = dt.Rows[row][8] + "";
                    if (dt.Rows[row][9].Equals("男"))
                    {
                        rboEditMale.Checked = true;
                    }
                    else
                    {
                        rboEditFemale.Checked = true;
                    }
                    this.txtEditAddress.Text = dt.Rows[row][10] + "";
                    this.cboEditIdentity.Text = dt.Rows[row][11] + "";//身份
                }
                else if (this.dgdUser.DataSource == ds.Tables["SearchTeacherByAge"])//教员年龄
                {
                    this.cboEditIdentity.SelectedItem = "教师";
                    this.chkEditClassTeacher.Checked = false;

                    this.cboEditrClass.Enabled = false;
                    this.txtEditStuID.Enabled = false;
                    this.txtEditName.Text = dt.Rows[row][0] + "";
                    this.txtEditAge.Text = dt.Rows[row][1] + "";
                    this.txtEditID.Text = dt.Rows[row][2] + "";
                    this.txtEditPassWord.Text = dt.Rows[row][3] + "";
                    this.dtpEditWork.Text = dt.Rows[row][4] + "";
                    this.txtEditDimission.Text = dt.Rows[row][5] + "";
                    this.txtEditIdCard.Text = dt.Rows[row][6] + "";
                    this.dtpEdit.Text = dt.Rows[row][7] + "";
                    this.txtEditPhone.Text = dt.Rows[row][8] + "";
                    if (dt.Rows[row][9].Equals("男"))
                    {
                        rboEditMale.Checked = true;
                    }
                    else
                    {
                        rboEditFemale.Checked = true;
                    }
                    this.txtEditAddress.Text = dt.Rows[row][10] + "";
                    this.cboEditIdentity.Text = dt.Rows[row][11] + "";//身份
                }
                else if (this.dgdUser.DataSource == ds.Tables["SearchStuByName"])//学生姓名
                {
                    this.cboEditIdentity.SelectedItem = "学生";
                    this.chkEditClassTeacher.Enabled = false;
                    this.chkEditPopedom.Enabled=false;

                    this.cboEditrClass.Enabled = true;
                    this.txtEditStuID.Enabled = true;

                    this.txtEditName.Text = dt.Rows[row][0] + "";
                    this.txtEditID.Text = dt.Rows[row][1] + "";
                    this.txtEditPassWord.Text = dt.Rows[row][2] + "";
                    this.dtpEditWork.Text = dt.Rows[row][3] + "";
                    this.txtEditDimission.Text = dt.Rows[row][4] + "";
                    this.txtEditIdCard.Text = dt.Rows[row][5] + "";
                    this.txtEditAge.Text = dt.Rows[row][6] + "";
                    this.dtpEdit.Text = dt.Rows[row][7] + "";
                    this.txtEditPhone.Text = dt.Rows[row][8] + "";
                    if (dt.Rows[row][9].Equals("男"))
                    {
                        rboEditMale.Checked = true;
                    }
                    else
                    {
                        rboEditFemale.Checked = true;
                    }
                    this.cboEditrClass.Text = dt.Rows[row][10] + "";
                    this.txtEditStuID.Text = dt.Rows[row][11] + "";
                    this.txtEditAddress.Text = dt.Rows[row][12] + "";
                    this.cboEditIdentity.Text = dt.Rows[row][13] + "";//身份
                }
                else if (this.dgdUser.DataSource == ds.Tables["SearchStuByAge"])//学生年龄
                {
                    this.cboEditIdentity.SelectedItem = "学生";
                    this.chkEditClassTeacher.Enabled = false;
                    this.chkEditPopedom.Enabled = false;

                    this.cboEditrClass.Enabled = true;
                    this.txtEditStuID.Enabled = true;
                    this.txtEditName.Text = dt.Rows[row][0] + "";
                    this.txtEditAge.Text = dt.Rows[row][1] + "";
                    this.txtEditStuID.Text = dt.Rows[row][2] + "";
                    this.txtEditID.Text = dt.Rows[row][3] + "";
                    this.txtEditPassWord.Text = dt.Rows[row][4] + "";
                    this.dtpEditWork.Text = dt.Rows[row][5] + "";
                    this.txtEditDimission.Text = dt.Rows[row][6] + "";
                    this.txtEditIdCard.Text = dt.Rows[row][7] + "";
                    this.dtpEdit.Text = dt.Rows[row][8] + "";
                    this.txtEditPhone.Text = dt.Rows[row][9] + "";
                    if (dt.Rows[row][10].Equals("男"))
                    {
                        rboEditMale.Checked = true;
                    }
                    else
                    {
                        rboEditFemale.Checked = true;
                    }
                    this.cboEditrClass.Text = dt.Rows[row][11] + "";
                    this.txtEditAddress.Text = dt.Rows[row][12] + "";
                    this.cboEditIdentity.Text = dt.Rows[row][13] + "";//身份
                }
                else if (this.dgdUser.DataSource == ds.Tables["SearchStuById"])//学生学号
                {
                    this.cboEditIdentity.SelectedItem = "学生";
                    this.chkEditClassTeacher.Enabled = false;
                    this.chkEditPopedom.Enabled = false;

                    this.cboEditrClass.Enabled = true;
                    this.txtEditStuID.Enabled = true;
                    this.txtEditName.Text = dt.Rows[row][0] + "";
                    this.txtEditStuID.Text = dt.Rows[row][2] + "";
                    this.txtEditID.Text = dt.Rows[row][3] + "";
                    this.txtEditPassWord.Text = dt.Rows[row][4] + "";
                    this.dtpEditWork.Text = dt.Rows[row][5] + "";
                    this.txtEditDimission.Text = dt.Rows[row][6] + "";
                    this.txtEditIdCard.Text = dt.Rows[row][7] + "";
                    this.txtEditAge.Text = dt.Rows[row][1] + "";
                    this.dtpEdit.Text = dt.Rows[row][8] + "";
                    this.txtEditPhone.Text = dt.Rows[row][9] + "";
                    if (dt.Rows[row][10].Equals("男"))
                    {
                        rboEditMale.Checked = true;
                    }
                    else
                    {
                        rboEditFemale.Checked = true;
                    }
                    this.cboEditrClass.Text = dt.Rows[row][11] + "";
                    this.txtEditAddress.Text = dt.Rows[row][12] + "";
                    this.cboEditIdentity.Text = dt.Rows[row][13] + "";//身份
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region 复选框是否离职按钮事件
        private void chkDimission_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkDimission.Checked == true)
            {
                this.dtpEditDimission.Visible = true;
                this.txtEditDimission.Visible = false;
            }
            else
            {
                this.dtpEditDimission.Visible = false;
                this.txtEditDimission.Visible = true;
            }
        }
        #endregion

        #region 删除用户按钮单击事件
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            int i = this.dgdUser.SelectedRows[0].Cells[0].RowIndex;
            userEntity.UserLoginName= this.dgdUser.Rows[i].Cells[1].Value + "";
            if (this.dgdUser.DataSource == ds.Tables["SearchFirstLeaveManagerByName"])
            {
                bool flag =editUserInfoDao.deleteManagerByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("删除成功");
                }
                 ds = searchUserInfoDao.searchFirstLeaveManagerByName(userEntity);
                 this.dgdUser.DataSource = null;
                 this.dgdUser.DataSource = ds.Tables["SearchFirstLeaveManagerByName"];
            }
            else if (this.dgdUser.DataSource == ds.Tables["SearchManagerByName"])
            {
                bool flag = editUserInfoDao.deleteManagerByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("删除成功");
                }
                ds = searchUserInfoDao.searchManagerByName(userEntity);
                this.dgdUser.DataSource = null;
                this.dgdUser.DataSource = ds.Tables["SearchManagerByName"];
            }

            else if (this.dgdUser.DataSource == ds.Tables["SearchClassTeacherByName"])
            {
                bool flag = editUserInfoDao.deleteClassTeacherByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("删除成功");
                }
                ds = searchUserInfoDao.searchClassTeacherByName(userEntity);
                this.dgdUser.DataSource = null;
                this.dgdUser.DataSource = ds.Tables["SearchClassTeacherByName"];
            }
            else if (this.dgdUser.DataSource == ds.Tables["SearchClassTeacherByAge"])
            {
                userEntity.UserLoginName = this.dgdUser.Rows[i].Cells[2].Value + "";
                bool flag = editUserInfoDao.deleteClassTeacherByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("删除成功");
                }
                ds = searchUserInfoDao.searchClassTeacherByAge(userEntity);
                this.dgdUser.DataSource = null;
                this.dgdUser.DataSource = ds.Tables["SearchClassTeacherByAge"];
            }
            else if (this.dgdUser.DataSource == ds.Tables["SearchTeacherByName"])
            {
                bool flag = editUserInfoDao.deleteTeacherByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("删除成功");
                }
                ds = searchUserInfoDao.searchTeacherByName(userEntity);
                this.dgdUser.DataSource = null;
                this.dgdUser.DataSource = ds.Tables["SearchTeacherByName"];
            }
            else if (this.dgdUser.DataSource == ds.Tables["SearchTeacherByAge"])
            {
                userEntity.UserLoginName = this.dgdUser.Rows[i].Cells[2].Value + "";
                bool flag = editUserInfoDao.deleteTeacherByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("删除成功");
                }
                ds = searchUserInfoDao.searchTeacherByAge(userEntity);
                this.dgdUser.DataSource = null;
                this.dgdUser.DataSource = ds.Tables["SearchTeacherByAge"];
            }
            else if (this.dgdUser.DataSource == ds.Tables["SearchStuByName"])
            {
                bool flag = editUserInfoDao.deleteStuByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("删除成功");
                }
                ds = searchUserInfoDao.searchStuByName(userEntity);
                this.dgdUser.DataSource = null;
                this.dgdUser.DataSource = ds.Tables["SearchStuByName"];
            }
            else if (this.dgdUser.DataSource == ds.Tables["SearchStuByAge"])
            {
                userEntity.UserLoginName = this.dgdUser.Rows[i].Cells[3].Value + "";
                bool flag = editUserInfoDao.deleteStuByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("删除成功");
                }
                ds = searchUserInfoDao.searchStuByAge(userEntity);
                this.dgdUser.DataSource = null;
                this.dgdUser.DataSource = ds.Tables["SearchStuByAge"];
            }
            else if (this.dgdUser.DataSource == ds.Tables["SearchStuById"])
            {
                userEntity.UserLoginName = this.dgdUser.Rows[i].Cells[2].Value + "";
                bool flag = editUserInfoDao.deleteStuByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("删除成功");
                }
                ds = searchUserInfoDao.searchStuById(userEntity);
                this.dgdUser.DataSource = null;
                this.dgdUser.DataSource = ds.Tables["SearchStuById"];
            }
        }   
        #endregion

        #region 修改用户信息按钮单击时间
        private void btnEditSave_Click(object sender, EventArgs e)
        {
           
            this.cboEditrClass.SelectedIndex = 1;
            int message = judgeEn.judge1(txtEditID.Text, txtEditPassWord.Text,
                txtEditName.Text, txtEditAge.Text, txtEditAddress.Text);
            if (message == 0)
            {
                lbl40.Visible = true;
                txtEditID.Focus();
                return;
            }
            else if (message == 1)
            {
                lbl41.Visible = true;
                txtEditPassWord.Focus();
                return;
            }
            else if (message == 2)
            {
                lbl43.Visible = true;
                txtEditName.Focus();
                return;
            }
            else if (message == 3)
            {
                lbl45.Visible = true;
                txtEditAge.Focus();
                return;
            }
            else if (message == 4)
            {
                lbl50.Visible = true;
                txtEditAddress.Focus();
                return;
            }
            if (!txtEditPassWord.Text.Equals(txtEditPassWordAG.Text))
            {
                lbl42.Visible = true;
                txtEditPassWord.Focus();
                return;
            }
            else
            {
                lbl42.Visible = false;
            }
            if (!((this.txtEditPhone.Text.Length == 8) || (this.txtEditPhone.Text.Length == 11)))
            {
                this.lbl47.Visible = true;
                this.txtEditPhone.Focus();
                this.txtEditPhone.Text = "";
                return;
            }
            else
            {
                this.lbl47.Visible = false;
            }

            if (!(this.txtEditIdCard.Text.Length == 18))
            {
                this.lbl44.Visible = true;
                this.txtEditIdCard.Focus();
                this.txtEditIdCard.Text = "";
                return;
            }
            else
            {
                this.lbl44.Visible = false;
            }
           
            if (chkDimission.Checked==true)
            { 
                userEntity.UserLeaveSchoolTime=dtpEditDimission.Text;
            }
            else if (chkDimission.Checked == false)
            {
                userEntity.UserLeaveSchoolTime = txtEditDimission.Text;
            }
            //if (cboEditIdentity.SelectedItem.Equals("学生"))
            //{
            //    userEntity.UserIdentityId = 1;
            //}
            //else if (cboEditIdentity.SelectedItem.Equals("管理员") && chkEditPopedom.Checked == true)
            //{
            //    userEntity.UserIdentityId = 5;
            //}
            //else if (cboEditIdentity.SelectedItem.Equals("教师") && chkEditClassTeacher.Checked == true)
            //{
            //    userEntity.UserIdentityId = 3;
            //}
            //else if (cboEditIdentity.SelectedItem.Equals("教师"))
            //{
            //    userEntity.UserIdentityId = 2;
            //}
            //else if (cboEditIdentity.SelectedItem.Equals("管理员"))
            //{
            //    userEntity.UserIdentityId = 4;
            //}
           
            int sex = 0;
            if (rboEditMale.Checked == true)
            {
                sex = 0;//男
            }
            if (rboEditFemale.Checked == true)
            {
                sex = 1;//女
            }
            userEntity.UserLoginName = txtEditID.Text;
            userEntity.UserLoginPwd = txtEditPassWord.Text;
            userEntity.UserEnterSchoolTime = dtpEditWork.Text;
            userEntity.UserName = txtEditName.Text;
            userEntity.UserAge = int.Parse(txtEditAge.Text);
            userEntity.UserIdCard = txtEditIdCard.Text;
            userEntity.UserPhone = int.Parse(txtEditPhone.Text);
            userEntity.UserClassId = int.Parse(cboEditrClass.SelectedValue+"");
            userEntity.UserAddress = txtEditAddress.Text;
            userEntity.UserSex = sex;
            userEntity.UserBrithday = dtpEdit.Text;
            int i = this.dgdUser.SelectedRows[0].Cells[0].RowIndex;

            if (this.dgdUser.DataSource == ds.Tables["SearchFirstLeaveManagerByName"])
            {
                userEntity.UserIdentityId = 5;
                txtEditPassWordAG.Text = "";
                userEntity.ChooseUserLoginName = this.dgdUser.Rows[i].Cells[1].Value + "";
                bool flag = editUserInfoDao.editManagerByLoginName(userEntity);
                if(flag==true)
                {
                    MessageBox.Show("修改成功");
                    ds = searchUserInfoDao.searchFirstLeaveManagerByName1();
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchFirstLeaveManagerByName1"];
                }
            }
            else if(this.dgdUser.DataSource == ds.Tables["SearchManagerByName"])
            {
                userEntity.UserIdentityId = 4;
                txtEditPassWordAG.Text = "";
                userEntity.ChooseUserLoginName = this.dgdUser.Rows[i].Cells[1].Value + "";
                bool flag = editUserInfoDao.editManagerByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("修改成功");
                    ds = searchUserInfoDao.searchManagerByName1();
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchManagerByName1"];
                }
            }
            else if (this.dgdUser.DataSource == ds.Tables["SearchClassTeacherByName"])
            {
                userEntity.UserIdentityId = 3;
                txtEditPassWordAG.Text = "";
                userEntity.ChooseUserLoginName = this.dgdUser.Rows[i].Cells[1].Value + "";
                bool flag = editUserInfoDao.editClassTeacherByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("修改成功");
                    ds = searchUserInfoDao.searchClassTeacherByName1();
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchClassTeacherByName1"];
                }
            }
            else if (this.dgdUser.DataSource == ds.Tables["SearchClassTeacherByAge"])
            {
               
                userEntity.UserIdentityId = 3;
                txtEditPassWordAG.Text = "";
                userEntity.ChooseUserLoginName = this.dgdUser.Rows[i].Cells[2].Value + "";
                bool flag = editUserInfoDao.editClassTeacherByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("修改成功");
                    ds = searchUserInfoDao.searchClassTeacherByAge1();
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchClassTeacherByAge1"];
                }
            }
            else if (this.dgdUser.DataSource == ds.Tables["SearchTeacherByName"])
            {
                userEntity.UserIdentityId = 2;
                txtEditPassWordAG.Text = "";
                userEntity.ChooseUserLoginName = this.dgdUser.Rows[i].Cells[1].Value + "";
                bool flag = editUserInfoDao.editTeacherByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("修改成功");
                    ds = searchUserInfoDao.searchTeacherByName1();
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchTeacherByName1"];
                }
            }
            else if (this.dgdUser.DataSource == ds.Tables["SearchTeacherByAge"])
            {
                userEntity.UserIdentityId = 2;
                txtEditPassWordAG.Text = "";
                userEntity.ChooseUserLoginName = this.dgdUser.Rows[i].Cells[2].Value + "";
                bool flag = editUserInfoDao.editTeacherByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("修改成功");
                    ds = searchUserInfoDao.searchTeacherByAge1();
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchTeacherByName1"];
                }
            }
            else if (this.dgdUser.DataSource == ds.Tables["SearchStuByName"])
            {
                userEntity.UserIdentityId = 1;
                txtEditPassWordAG.Text = "";
                userEntity.UserId = int.Parse(txtEditStuID.Text);
                userEntity.ChooseUserLoginName = this.dgdUser.Rows[i].Cells[1].Value + "";
                bool flag = editUserInfoDao.editStudentByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("修改成功");
                    ds = searchUserInfoDao.searchStuByName1();
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchStuByName1"];
                }
            }
            else if (this.dgdUser.DataSource == ds.Tables["SearchStuByAge"])
            {
                userEntity.UserIdentityId = 1;
                txtEditPassWordAG.Text = "";
                userEntity.UserId = int.Parse(txtEditStuID.Text);
                userEntity.ChooseUserLoginName = this.dgdUser.Rows[i].Cells[3].Value + "";
                bool flag = editUserInfoDao.editStudentByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("修改成功");
                    ds = searchUserInfoDao.searchStuByAge1();
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchStuByAge1"];
                }
            }

            else if (this.dgdUser.DataSource == ds.Tables["SearchStuById"])
            {
                userEntity.UserIdentityId = 1;
                txtEditPassWordAG.Text = "";
                userEntity.UserId = int.Parse(txtEditStuID.Text);
                userEntity.ChooseUserLoginName = this.dgdUser.Rows[i].Cells[2].Value + "";
                bool flag = editUserInfoDao.editStudentByLoginName(userEntity);
                if (flag == true)
                {
                    MessageBox.Show("修改成功");
                    ds = searchUserInfoDao.searchStuById1();
                    this.dgdUser.DataSource = null;
                    this.dgdUser.DataSource = ds.Tables["SearchStuById1"];
                }
            }


        }
        #endregion

        #region 修改用户组合框选定项改事件
        private void cboEditIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboEditIdentity.SelectedItem.Equals("学生"))
            {
                cboEditrClass.Enabled = true;
                txtEditStuID.Enabled = true;
                chkEditClassTeacher.Checked = false;
                chkEditPopedom.Checked = false;

                chkEditClassTeacher.Enabled = false;
                chkEditPopedom.Enabled = false;
            }
            else if (this.cboEditIdentity.SelectedItem.Equals("教师"))
            {
                chkEditClassTeacher.Enabled = true;

                chkEditPopedom.Checked = false;
                chkEditPopedom.Enabled = false;

                cboEditrClass.Enabled = false;
                txtEditStuID.Enabled = false;
            }
            else if (this.cboEditIdentity.SelectedItem.Equals("管理员"))
            {
                chkEditPopedom.Enabled = true;
                chkEditClassTeacher.Enabled = false;
                chkEditClassTeacher.Checked = false;

                cboEditrClass.Enabled = false;
                txtEditStuID.Enabled = false;
            }
        }
        #endregion

        #region 修改用户窗体文本框鼠标焦点失去事件
        private void txtEditID_Leave(object sender, EventArgs e)
        {
            if (!txtEditID.Equals(""))
                lbl40.Visible = false;
        }

        private void txtEditPassWord_Leave(object sender, EventArgs e)
        {
            if (!txtEditPassWord.Equals(""))
                lbl41.Visible = false;
        }

        private void txtEditName_Leave(object sender, EventArgs e)
        {
            if (!txtEditName.Equals(""))
                lbl43.Visible = false;
        }

        private void txtEditAge_Leave(object sender, EventArgs e)
        {
            if (!txtEditAge.Equals(""))
                lbl45.Visible = false;
        }
        #endregion


        #region 学员调动选择班级
        private void searchRemoveClass()
        {
            ds = classNameDao.searchClassName();
            this.cboUserClassRemove.DataSource = ds.Tables["classNameInfo"];
            this.cboUserClassRemove.ValueMember = "ClassId";
            this.cboUserClassRemove.DisplayMember = "ClassName";
        }
        #endregion

        #region 学员调动选择班级查询学生姓名事件
        private void cboUserClass_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ClassEntity.ClassId = int.Parse(cboUserClass.SelectedValue + "");
            ds = removeUserDao.searchStuName(ClassEntity);
            cboUserName.DataSource = ds.Tables["SearchStuName"];
            cboUserName.ValueMember = "StuLoginName";
            cboUserName.DisplayMember = "StuName";
            cboUserName.Enabled = true;
            cboUserClassRemove.Enabled = false;
        }
        #endregion 

        #region 学员调动选择姓名事件
        private void cboUserName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            searchRemoveClass();
            cboUserClassRemove.Enabled = true;
        }
        #endregion

        #region 查询学员调动
        private void btnInquireUserRemove_Click(object sender, EventArgs e)
        {
            dgvUserRemove.Visible = true;
            ds = removeUserDao.searchStuRemoveInfo();
            dgvUserRemove.DataSource = ds.Tables["SearchStuRemoveInfo"];
        }
        #endregion

        #region 发送留言按钮单击事件
        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (richTextBox.Text.Equals(""))
            {
                label51.Visible = true;
                richTextBox.Focus();
                return;
            }
            else
            {
                label51.Visible = false;
            }
            messageBoardEntity.LeaveMessageTime = DateTime.Now + "";
            messageBoardEntity.LoginName1 = Entity.LoginInfoEntity.LoginName;
            messageBoardEntity.MessageContent1 = richTextBox.Text;
            bool flag = messageBoardInfoDao.insertMessage(messageBoardEntity);
            if (flag == true)
            {
                MessageBox.Show("发送成功");
            }
            ds = messageBoardInfoDao.searchBBSInfo();
            dgvBBSBoard.DataSource = ds.Tables["SearchBBSInfo"];
            richTextBox.Text = "";
            richTextBox.Focus();
        }
        #endregion

        #region 删除留言按钮单击事件
        private void btnDeleteMessage_Click(object sender, EventArgs e)
        {
            int i = dgvBBSBoard.SelectedRows[0].Cells[0].RowIndex;
            messageBoardEntity.MessageContent1  = this.dgvBBSBoard.Rows[i].Cells[2].Value + "";
            bool flag = messageBoardInfoDao.deleteMessage(messageBoardEntity);
            if (flag == true)
            {
                MessageBox.Show("删除成功");
            }
            ds = messageBoardInfoDao.searchBBSInfo();
            dgvBBSBoard.DataSource = ds.Tables["SearchBBSInfo"];
        }
        #endregion





    }
}



    

