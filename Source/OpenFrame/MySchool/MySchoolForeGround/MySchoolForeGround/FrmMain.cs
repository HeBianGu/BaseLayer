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

namespace MySchoolForeGround
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        FrmLogin Frmlog;

        public FrmMain(FrmLogin log)
        {
            InitializeComponent();
            Frmlog = log;
            if (Frmlog.rboStudent.Checked == true)
            {
                txtName.Enabled = false;
                txtAge.Enabled = false;
                dtpBrithday.Enabled = false;
                txtIdCard.Enabled = false;
                txtPhone.Enabled = false;
                txtClassName.Enabled = false;
                txtStuID.Enabled = false;
                groupBox3.Enabled = false;
                txtAddress.Enabled = false;
                dtpEnterSchoolTime.Enabled = false;
                btnEditEssayNext.Enabled = false;
                btnInquireMark.Enabled = false;
             
                button2.Enabled = false;
            }
            else if (Frmlog.rboTeacher.Checked == true)
            {
                txtClassName.Enabled = false;
                txtStuID.Enabled = false;
                btnBigenExam.Enabled = false;
            }
            else if (Frmlog.rboClassTeacher.Checked == true)
            {
                txtClassName.Enabled = false;
                txtStuID.Enabled = false;
                button2.Enabled = false;
                btnBigenExam.Enabled = false;
            }
        }
        #region 全局变量
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        UserInfoEntity userInfoEntity = new UserInfoEntity();
        SearchClassInfoDao searchClassInfoDao = new SearchClassInfoDao();
        SearchLessonNameDao searchLessonNameDao = new SearchLessonNameDao();
        SearchUserInfoDao searchUserInfoDao = new SearchUserInfoDao();
        EditUserInfoDao editUserInfoDao = new EditUserInfoDao();
        judgeEntity judgeEn = new judgeEntity();
        LessonInfoEntity lessonInfoEntity = new LessonInfoEntity();
        LoginInfoEntity loginInfoEntity = new LoginInfoEntity();
        SearchExamInfo searchExamInfo = new SearchExamInfo();
        ArrayList arr = new ArrayList();
        StuMarkEntity stuMarkEntity = new StuMarkEntity();
        StuMarkInfoDao stuMarkInfoDao = new StuMarkInfoDao();
        ClassInfoEntity classInfoEntity = new ClassInfoEntity();
        QuestionOperateEntity questionEntity = new QuestionOperateEntity();
        QuestionOperateDAO questionOperateDAO = new QuestionOperateDAO();
        MessageBoardEntity messageBoardEntity = new MessageBoardEntity();
        MessageBoardInfoDao messageBoardInfoDao = new MessageBoardInfoDao();

        string Pwd = "";
        int minute;                        
        int second;                         
        int remainSeconds = 360;
        int num = 5;
        //string[] answer = new string[5];
        
        #endregion

        #region 主窗体Load事件
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "当前登入用户:"+Entity.LoginInfoEntity.LoginName;
            skinMain.SkinFile = "SportsBlack.ssk";
            listView.Clear();
            listView.LargeImageList = imageListMain;
            listView.Items.Add("查询编辑用户", "查询编辑用户", 0);

            ds = searchLessonNameDao.searchLessonName();
            cboChoiceExamType.DataSource = ds.Tables["LessonNameInfo"];
            cboChoiceExamType.ValueMember = "LessonId";
            cboChoiceExamType.DisplayMember = "LessonName";

            cboLessonId.DataSource = ds.Tables["LessonNameInfo"];
            cboLessonId.ValueMember = "LessonId";
            cboLessonId.DisplayMember = "LessonName";

            cboInquireLesson.DataSource = ds.Tables["LessonNameInfo"];
            cboInquireLesson.ValueMember = "LessonId";
            cboInquireLesson.DisplayMember = "LessonName";

            ds = stuMarkInfoDao.searchAllClass();
            cboInquireClass.DataSource = ds.Tables["SearchAllClass"];
            cboInquireClass.ValueMember = "ClassId";
            cboInquireClass.DisplayMember = "ClassName";
       
            userInfoEntity.UserLoginName = Entity.LoginInfoEntity.LoginName;
            ds = stuMarkInfoDao.searchTeacherClass(userInfoEntity);
            cboTeacherClass.DataSource = ds.Tables["SearchTeacherClass"];
            cboTeacherClass.ValueMember = "ClassId";
            cboTeacherClass.DisplayMember = "ClassName";
        }
        #endregion

        #region 窗体"listView"单击事件
        private void btnExamOnLine_Click(object sender, EventArgs e)
        {
            listView.Dock = DockStyle.None;
            btnExamOnLine.Dock = DockStyle.Top;
            btnUserManage.SendToBack();
            btnUserManage.Dock = DockStyle.Top;
            btnBBS.Dock = DockStyle.Bottom;
            listView.Dock = DockStyle.Bottom;
            listView.Clear();
            listView.Items.Add("参加考试", "参加考试", 1);
            listView.Items.Add("查看成绩", "查看成绩", 2);
            listView.Items.Add("批改问答题", "批改问答题", 3);
        }

        private void btnBBS_Click(object sender, EventArgs e)
        {
            listView.Dock = DockStyle.None;
            btnBBS.SendToBack();
            btnBBS.Dock = DockStyle.Top;
            btnExamOnLine.SendToBack();
            btnExamOnLine.Dock = DockStyle.Top;
            btnUserManage.SendToBack();
            btnUserManage.Dock = DockStyle.Top;
            listView.Dock = DockStyle.Bottom;
            listView.Clear();
            listView.Items.Add("进入留言板", "进入留言板", 5);
        }

        private void btnUserManage_Click(object sender, EventArgs e)
        {
            listView.Dock = DockStyle.None;
            btnUserManage.Dock = DockStyle.Top;
            btnExamOnLine.Dock = DockStyle.Bottom;
            btnBBS.SendToBack();
            btnBBS.Dock = DockStyle.Bottom;
            listView.BringToFront();
            listView.Dock = DockStyle.Bottom;
            listView.Clear();
            listView.Items.Add("查询编辑用户", "查询编辑用户", 0);
        }
        #endregion

        #region  查询户单击事件
        private void listView_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "" + listView.SelectedItems[0].Text;
                if (str.Equals("查询编辑用户"))
                {
                    pnlInquireEditUser.Visible = true;
                    pnlExam.Visible = false;
                    pnlBeginExam.Visible = false;
                    pnlInquireUserMark.Visible = false;
                    pnlMessageShow.Visible = false;
                    spConBBS.Visible = false;
                    pnlExamMark.Visible = false;
                    pnlEssayQuestion.Visible = false;
                    pnlEditEssayQuestion.Visible = false;
                    pnlEssayQuestion.Visible = false;
                    userInfoEntity.UserLoginName = Entity.LoginInfoEntity.LoginName;
                    if (Frmlog.rboStudent.Checked == true)
                    {
                        ds = searchUserInfoDao.searchStuInfo(userInfoEntity);
                        Pwd = ds.Tables["SearchStuInfo"].Rows[0][0] + "";
                        dtpEnterSchoolTime.Text = ds.Tables["SearchStuInfo"].Rows[0][1] + "";
                        txtName.Text = ds.Tables["SearchStuInfo"].Rows[0][2] + "";
                        txtIdCard.Text = ds.Tables["SearchStuInfo"].Rows[0][3] + "";
                        txtAge.Text = ds.Tables["SearchStuInfo"].Rows[0][4] + "";
                        dtpBrithday.Text = ds.Tables["SearchStuInfo"].Rows[0][5] + "";
                        txtPhone.Text = ds.Tables["SearchStuInfo"].Rows[0][6] + "";
                        txtClassName.Text = ds.Tables["SearchStuInfo"].Rows[0][8] + "";
                        txtStuID.Text = ds.Tables["SearchStuInfo"].Rows[0][9] + "";
                        txtAddress.Text = ds.Tables["SearchStuInfo"].Rows[0][10] + "";
                        if (ds.Tables["SearchStuInfo"].Rows[0][7].ToString().Equals("男"))
                        {
                            rborMale.Checked = true;
                        }
                        else
                            rboFemale.Checked = true;
                    }
                    else if (Frmlog.rboTeacher.Checked == true )
                    {
                        ds = searchUserInfoDao.searchTeacherInfo(userInfoEntity);
                        Pwd = ds.Tables["SearchTeacherInfo"].Rows[0][0] + "";
                        dtpEnterSchoolTime.Text = ds.Tables["SearchTeacherInfo"].Rows[0][1] + "";
                        txtName.Text = ds.Tables["SearchTeacherInfo"].Rows[0][2] + "";
                        txtIdCard.Text = ds.Tables["SearchTeacherInfo"].Rows[0][3] + "";
                        txtAge.Text = ds.Tables["SearchTeacherInfo"].Rows[0][4] + "";
                        dtpBrithday.Text = ds.Tables["SearchTeacherInfo"].Rows[0][5] + "";
                        txtPhone.Text = ds.Tables["SearchTeacherInfo"].Rows[0][6] + "";
                        txtAddress.Text = ds.Tables["SearchTeacherInfo"].Rows[0][8] + "";
                        if (ds.Tables["SearchTeacherInfo"].Rows[0][7].ToString().Equals("男"))
                        {
                            rborMale.Checked = true;
                        }
                        else
                            rboFemale.Checked = true;
                    }
                    else if(Frmlog.rboClassTeacher.Checked == true)
                    {
                        ds = searchUserInfoDao.searchClassTeacherInfo(userInfoEntity);
                        Pwd = ds.Tables["SearchClassTeacherInfo"].Rows[0][0] + "";
                        dtpEnterSchoolTime.Text = ds.Tables["SearchClassTeacherInfo"].Rows[0][1] + "";
                        txtName.Text = ds.Tables["SearchClassTeacherInfo"].Rows[0][2] + "";
                        txtIdCard.Text = ds.Tables["SearchClassTeacherInfo"].Rows[0][3] + "";
                        txtAge.Text = ds.Tables["SearchClassTeacherInfo"].Rows[0][4] + "";
                        dtpBrithday.Text = ds.Tables["SearchClassTeacherInfo"].Rows[0][5] + "";
                        txtPhone.Text = ds.Tables["SearchClassTeacherInfo"].Rows[0][6] + "";
                        txtAddress.Text = ds.Tables["SearchClassTeacherInfo"].Rows[0][8] + "";
                        if (ds.Tables["SearchClassTeacherInfo"].Rows[0][7].ToString().Equals("男"))
                        {
                            rborMale.Checked = true;
                        }
                        else
                            rboFemale.Checked = true;
                    }
                }
                else if (str.Equals("参加考试"))
                {
                    pnlExamMark.Visible = false;
                    pnlExam.Visible = true;
                    pnlInquireEditUser.Visible=false;
                    pnlBeginExam.Visible = false;
                    pnlInquireUserMark.Visible = false;
                    pnlMessageShow.Visible = false;
                    spConBBS.Visible = false;
                    pnlEssayQuestion.Visible = false;
                    pnlEditEssayQuestion.Visible = false;
                    pnlEssayQuestion.Visible = false;
                }
                else if (str.Equals("进入留言板"))
                {
                    pnlExamMark.Visible = false;
                    spConBBS.Visible = true;
                    this.richTextBox.WordWrap = true;
                    pnlExam.Visible = false;
                    pnlInquireEditUser.Visible = false;
                    pnlBeginExam.Visible = false;
                    pnlInquireUserMark.Visible = false;
                    pnlMessageShow.Visible = false;
                    pnlEssayQuestion.Visible = false;
                    pnlEditEssayQuestion.Visible = false;
                    pnlEssayQuestion.Visible = false;
                    ds = messageBoardInfoDao.searchBBSInfo();
                    dgvBBSBoard.DataSource = ds.Tables["SearchBBSInfo"];
                }
                else if (str.Equals("批改问答题"))
                {
                    pnlEssayQuestion.Visible = true;
                    pnlExamMark.Visible = false;
                    pnlExam.Visible = false;
                    pnlInquireEditUser.Visible = false;
                    pnlBeginExam.Visible = false;
                    pnlInquireUserMark.Visible = false;
                    pnlMessageShow.Visible = false;
                    spConBBS.Visible = false;
                    pnlEditEssayQuestion.Visible = false;
                   
                }
                else if (str.Equals("查看成绩"))
                {
                    pnlInquireUserMark.Visible = true;
                    pnlEssayQuestion.Visible = false;
                    pnlExamMark.Visible = false;
                    pnlExam.Visible = false;
                    pnlInquireEditUser.Visible = false;
                    pnlBeginExam.Visible = false;
                    pnlMessageShow.Visible = false;
                    spConBBS.Visible = false;
                    pnlEditEssayQuestion.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 开始考试按钮单击事件
        string[] sAnswer = new string[20];//学生选择题答案
        string[] rAnswer = new string[20];//选择题正确答案
        string[] eAnswer = new string[20];//学生问答题答案
        private void btnBigenExam_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    sAnswer[i] = "";
                    rAnswer[i] = "";
                    eAnswer[i] = "";
                }
                pnlExamMark.Visible = false;
                tmrExam.Enabled = true;
                pnlBeginExam.Visible = true;
                pnlExam.Visible = false;
                pnlInquireEditUser.Visible = false;
                pnlInquireUserMark.Visible = false;
                pnlMessageShow.Visible = false;
                spConBBS.Visible = false;
                pnlEditEssayQuestion.Visible = false;
                lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
                ds = searchExamInfo.searchAllChoice(lessonInfoEntity);
                ds1 = searchExamInfo.searchAllEssayQuestion(lessonInfoEntity);

                for (int i = 0; i < num; i++)
                    arr.Add("-2");
                Random ran = new Random();
                for (int i = 0; i < num; i++)
                {
                    int x = ran.Next(ds.Tables["SearchAllChoice"].Rows.Count);
                    if (arr.IndexOf(x) == -1)
                    {
                        arr[i] = x;
                    }
                    else
                    {
                        i--;
                    }
                }
                txtChoiceContent.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[0].ToString())][0].ToString();
                txtChoiceA.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[0].ToString())][1].ToString();
                txtChoiceB.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[0].ToString())][2].ToString();
                txtChoiceC.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[0].ToString())][3].ToString();
                txtChoiceD.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[0].ToString())][4].ToString();
                txtChoiceE.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[0].ToString())][5].ToString();
                Entity.QuestionOperateEntity questionOperateEntity = new QuestionOperateEntity();
                for (int i = 0; i < arr.Count; i++)
                {
                    lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
                    questionEntity.StuEssayQuestionAnswer = "";
                    rAnswer[i] = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[i].ToString())][6].ToString();
                    questionOperateEntity.EssayQuestionSubject = ds1.Tables["SearchAllEssayQuestion"].Rows[int.Parse(arr[i].ToString())][0].ToString();
                    questionOperateEntity.EssayQuestionAnswer = ds1.Tables["SearchAllEssayQuestion"].Rows[int.Parse(arr[i].ToString())][1].ToString();
                    questionOperateEntity.StuLoginName = LoginInfoEntity.LoginName;
                    questionOperateDAO.insertQuestionOperate(questionOperateEntity,lessonInfoEntity);
                }
                txtEssayQuestionContent.Text = ds1.Tables["SearchAllEssayQuestion"].Rows[int.Parse(arr[0].ToString())][0].ToString();
                DataSet questionDs;
                questionDs = questionOperateDAO.searchQuestionOperate(questionEntity);
                txtEssayQuestionAnswer.Text = questionDs.Tables["SearchQuestionOperate"].Rows[0][0] + "";
            }
            catch (Exception ex)
            { }
            lblChoice.Text = "选择题" + 1 + "";
            lblEssayQuestion.Text = "问答题" + 1 + "";
        }
        #endregion

        #region 修改用户检查值为空错误方法
        private bool editUser(int message)
        {
            bool flag = true;
            if (message == 0)
            {
                lbl1.Visible = true;
                txtName.Focus();
                flag = false;
            }
            else if (message == 1)
            {
                lbl2.Visible = true;
                txtIdCard.Focus();
                flag = false;
            }
            else if (message == 2)
            {
                lbl3.Visible = true;
                txtAge.Focus();
                flag = false;
            }
            else if (message == 3)
            {
                lbl4.Visible = true;
                txtPhone.Focus();
                flag = false;
            }
            else if (message == 4)
            {
                lbl5.Visible = true;
                txtAddress.Focus();
                flag = false;
            }
            return flag;
        }
        #endregion

        #region 修改用户按钮单击事件
        private void btnInquireEditSave_Click(object sender, EventArgs e)
        {
           
            if (!txtNewPwd.Text.Equals("") && txtNewPwd.Text.Equals(txtNewPwdAg.Text)
                && txtOldPwd.Text.Equals(Pwd))
            {
                userInfoEntity.UserLoginPwd = txtNewPwd.Text;
            }
            else
            {
                userInfoEntity.UserLoginPwd = Pwd;
            }
            if (Frmlog.rboStudent.Checked == true)
            {
                if (!txtOldPwd.Text.Equals(Pwd))
                {
                    lbl6.Visible = true;
                    txtOldPwd.Focus();
                    return;
                }
                else
                {
                    lbl6.Visible = false;
                }
                if (txtNewPwd.Text.Equals(""))
                {
                    lbl7.Visible = true;
                    txtNewPwd.Focus();
                    return;
                }
                else
                {
                    lbl7.Visible = false;
                }
                if (!txtNewPwd.Text.Equals(txtNewPwdAg.Text))
                {
                    lbl8.Visible = true;
                    txtNewPwd.Focus();
                    txtNewPwd.Text = "";
                    txtNewPwdAg.Text = "";
                    return;
                  
                }
                else
                {
                    lbl8.Visible = false;
                }
          
                bool flag = editUserInfoDao.editStudent(userInfoEntity);
                {
                    if(flag==true)
                    MessageBox.Show("修改成功");
                   
                }
            }

            int sex = 0;
            if (rborMale.Checked == true)
            {
                sex = 0;//男
            }
            if (rboFemale.Checked == true)
            {
                sex = 1;//女
            }
            int message = judgeEn.judge1(txtName.Text,
                             txtIdCard.Text,txtAge.Text, 
                             txtPhone.Text, txtAddress.Text);
            if (editUser(message) == false)
                return;
            if (!(this.txtIdCard.Text.Length == 18))
            {
                this.lbl2.Visible = true;
                this.txtIdCard.Focus();
                this.txtIdCard.Text = "";
                return;
            }
            else
            {
                this.lbl2.Visible = false;
            }
            if (!((this.txtPhone.Text.Length == 8) || (this.txtPhone.Text.Length == 11)))
            {
                this.lbl4.Visible = true;
                this.txtPhone.Focus();
                this.txtPhone.Text = "";
                return;
            }
            else
            {
                this.lbl4.Visible = false;
            }
           userInfoEntity.UserEnterSchoolTime= dtpEnterSchoolTime.Text;
           userInfoEntity.UserName= txtName.Text ;
           if (int.Parse(txtAge.Text) < 18)
           {
               lbl3.Visible = true;
               txtAge.Focus();
               return;
           }
           else
           {
               lbl3.Visible = false;
           }

           try
           {
               userInfoEntity.UserAge = int.Parse(txtAge.Text);
           }
           catch (Exception ex)
           {
               lbl3.Visible = true;
           }

           userInfoEntity.UserBrithday= dtpBrithday.Text ;
           userInfoEntity.UserPhone= int.Parse(txtPhone.Text);
           userInfoEntity.UserAddress= txtAddress.Text;
           userInfoEntity.UserSex = sex;
           userInfoEntity.UserIdCard = txtIdCard.Text;
            if (Frmlog.rboTeacher.Checked == true)
            {
                if (!txtNewPwd.Text.Equals("") && txtOldPwd.Text.Equals(""))
                {
                    lbl6.Visible = true;
                    txtOldPwd.Focus();
                    return;
                }
                else
                {
                    lbl6.Visible = false;
                }

                if (!txtOldPwd.Text.Equals("") && txtNewPwd.Text.Equals("") && txtNewPwdAg.Text.Equals(""))
                {
                    lbl8.Visible = true;
                    txtNewPwd.Focus();
                    return;
                }
                else
                {
                    lbl8.Visible = false;
                }
               bool flag = editUserInfoDao.editTeacher(userInfoEntity);
               if (flag == true)
               {
                   MessageBox.Show("修改成功");
               }
            }
            else if (Frmlog.rboClassTeacher.Checked == true)
            {
                if (!txtNewPwd.Text.Equals("") && txtOldPwd.Text.Equals(""))
                {
                    lbl6.Visible = true;
                    txtOldPwd.Focus();
                    return;
                }
                else
                {
                    lbl6.Visible = false;
                }
                if (!txtOldPwd.Text.Equals("") && txtNewPwd.Text.Equals("") && txtNewPwdAg.Text.Equals(""))
                {
                    lbl8.Visible = true;
                    txtNewPwd.Focus();
                    return;
                }
                else
                {
                    lbl8.Visible = false;
                }
                bool flag = editUserInfoDao.editClassTeacher(userInfoEntity);
                if (flag == true)
                {
                    MessageBox.Show("修改成功");
                }
            }
            txtOldPwd.Text = "";
            txtNewPwd.Text = "";
            txtNewPwdAg.Text = "";
        }
        #endregion 

        #region 关闭窗体事件
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要退出本系统吗？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void tsbExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region 改变字体单击事件
        private void lblChangeFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowEffects = true;
            fontDialog.ShowColor = true;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SelectionFont = fontDialog.Font;
                richTextBox.SelectionColor = fontDialog.Color;
            }
        }
        #endregion
       
       
        #region 开始考试选择题和问答题下一题按钮单击事件
        int n = 1;
        int x = 1;
        private void btnChoiceNext_Click(object sender, EventArgs e)
        {
            try
            {
                n++;
                if (n > 5)
                {
                    n = 5;
                }
              
              if (n <=5 && n>0)
                {
                    lblChoice.Text = "选择题" + (n);
                    txtChoiceContent.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[n-1].ToString())][0].ToString();
                    txtChoiceA.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[n-1].ToString())][1].ToString();
                    txtChoiceB.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[n-1].ToString())][2].ToString();
                    txtChoiceC.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[n-1].ToString())][3].ToString();
                    txtChoiceD.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[n-1].ToString())][4].ToString();
                    txtChoiceE.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[n-1].ToString())][5].ToString();
                  
                }
            }
            catch (Exception ex)
            { }
            if (lblChoice.Text.Equals("选择题" + 5))
            {
                btnChoiceNext.Enabled = false;
            }
            chkChoiceAnswerA.Checked = false;
            chkChoiceAnswerB.Checked = false;
            chkChoiceAnswerC.Checked = false;
            chkChoiceAnswerD.Checked = false;
            chkChoiceAnswerE.Checked = false;
        }
       
        private void btnEssayQuestionNext_Click(object sender, EventArgs e)
        {
            try
            {
                
                x++;
                if (x > 5)
                {
                    x = 5;
                    btnEssayQuestionNext.Enabled = false;
                }
                if (x <= 5 && x > 0)
                {
                    //把上一题学生回答的答案入库 
                    lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
                    questionEntity.StuEssayQuestionAnswer = txtEssayQuestionAnswer.Text;
                    questionEntity.EssayQuestionSubject = txtEssayQuestionContent.Text;
                    questionEntity.StuLoginName = Entity.LoginInfoEntity.LoginName;
                    questionOperateDAO.updateQuestionOperate(questionEntity,lessonInfoEntity);
                    //将下一题显示在文本框内
                    lblEssayQuestion.Text = "问答题" + (x);
                    txtEssayQuestionContent.Text = ds1.Tables["SearchAllEssayQuestion"].Rows[int.Parse(arr[x-1].ToString())][0].ToString();
                    questionEntity.EssayQuestionSubject = txtEssayQuestionContent.Text;
                    //查询下一题答案
                    ds = questionOperateDAO.searchQuestionOperate(questionEntity);
                    if (ds.Tables["SearchQuestionOperate"].Rows.Count != 0)
                    {
                        txtEssayQuestionAnswer.Text = ds.Tables["SearchQuestionOperate"].Rows[0][0] + "";
                    }
                    else
                    {
                        txtEssayQuestionAnswer.Text = "";
                    }
                    txtEssayQuestionAnswer.Focus();
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region 开始考试选择题和问答题上一题按钮单击事件
        private void btnChoiceUp_Click(object sender, EventArgs e)
        {
            try
            {
                n--;
                if (n < 0)
                    n = 1;
                if (n > 0 && n < 5)
                {
                    btnChoiceNext.Enabled = true;
                    lblChoice.Text = "选择题" + (n);
                    txtChoiceContent.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[n - 1].ToString())][0].ToString();
                    txtChoiceA.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[n - 1].ToString())][1].ToString();
                    txtChoiceB.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[n - 1].ToString())][2].ToString();
                    txtChoiceC.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[n - 1].ToString())][3].ToString();
                    txtChoiceD.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[n - 1].ToString())][4].ToString();
                    txtChoiceE.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[n - 1].ToString())][5].ToString();
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void btnEssayQuestionUp_Click(object sender, EventArgs e)
        {
            try
            {
                x--;
                if (x < 0)
                    x = 1;
                if (x > 0 && x < 5)
                {
                    btnEssayQuestionNext.Enabled = true;
                    lblEssayQuestion.Text = "问答题" + (x);
                    txtEssayQuestionContent.Text = ds1.Tables["SearchAllEssayQuestion"].Rows[int.Parse(arr[x - 1].ToString())][0].ToString();
                    questionEntity.EssayQuestionSubject = txtEssayQuestionContent.Text;
                    questionEntity.StuLoginName = Entity.LoginInfoEntity.LoginName;
                    ds = questionOperateDAO.searchQuestionOperate(questionEntity);
                    txtEssayQuestionAnswer.Text = ds.Tables["SearchQuestionOperate"].Rows[0][0] + "";
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 考试计时事件
        private void tmrExam_Tick_1(object sender, EventArgs e)
        {
            if (remainSeconds > 0)
            {
                minute = remainSeconds / 60;
                second = remainSeconds % 60;
                lblExamTime.Text = string.Format("{0:00}:{1:00}", minute, second);
                remainSeconds--;
            }
            else
            {
                tmrExam.Stop();
                lblExamTime.Text = "00:00";
                btnBigenExam.Enabled = false;
                pnlExamMark.Visible = true;
                pnlInquireEditUser.Visible = false;
                pnlExam.Visible = false;
                pnlBeginExam.Visible = false;
                pnlInquireUserMark.Visible = false;
                pnlMessageShow.Visible = false;
                spConBBS.Visible = false;
             
                insertStuChoiceMark();
            }
        }
        #endregion

        #region 将选择题答案储存到数组中#
        private void chkChoiceAnswerE_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string answer = "";
                if (chkChoiceAnswerA.Checked == true)
                    answer += "A";
                if (chkChoiceAnswerB.Checked == true)
                    answer += "B";
                if (chkChoiceAnswerC.Checked == true)
                    answer += "C";
                if (chkChoiceAnswerD.Checked == true)
                    answer += "D";
                if (chkChoiceAnswerE.Checked == true)
                    answer += "E";
                sAnswer[n - 1] = answer;
                a1.Text = sAnswer[0] + "";
                a2.Text = sAnswer[1] + "";
                a3.Text = sAnswer[2] + "";
                a4.Text = sAnswer[3] + "";
                a5.Text = sAnswer[4] + "";
            }
            catch (Exception ex)
            { }

        }
        #endregion

        #region 将学生选择题成绩入库
        private void insertStuChoiceMark()
        {
            int sum = 0;
            for (int i = 0; i < arr.Count; i++)//比较学生选择答案和正确答案
            {
                if (sAnswer[i].Equals(rAnswer[i]))
                    sum += 3;
            }
            this.lblStuMark.Text = sum + "";
            userInfoEntity.UserLoginName = Entity.LoginInfoEntity.LoginName;
            stuMarkEntity.StuChoiceMark = int.Parse(this.lblStuMark.Text);
            lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
            stuMarkEntity.StuEssatQuestionMark = 0;
            stuMarkEntity.StuMark = stuMarkEntity.StuChoiceMark + stuMarkEntity.StuEssatQuestionMark;
            stuMarkInfoDao.insertStuChoiceMark(lessonInfoEntity, userInfoEntity, stuMarkEntity);
        }
        #endregion

        #region 考试结束按钮单击事件
        private void btnExamFinish_Click(object sender, EventArgs e)
        {  
            pnlExamMark.Visible = true;
            pnlInquireEditUser.Visible = false;
            pnlExam.Visible = false;
            pnlBeginExam.Visible = false;
            pnlInquireUserMark.Visible = false;
            pnlMessageShow.Visible = false;
            spConBBS.Visible = false;

            insertStuChoiceMark();
        }
        #endregion

        #region 检查答题卡选择题方法
        private void AA(int i)
        {
          string str = sAnswer[i];
         
            foreach (char c in str.ToCharArray())
            {
                switch (c)
                {
                    case 'A': chkCardChoiceA.Checked = true; break;
                    case 'B': chkCardChoiceB.Checked = true; break;
                    case 'C': chkCardChoiceC.Checked = true; break;
                    case 'D': chkCardChoiceD.Checked = true; break;
                    case 'E': chkCardChoiceE.Checked = true; break;
                }
            }
        }
        #endregion

        #region 检查答题卡选择题答案先清空方法
        private void chkCardChoiceClear()
        {
            chkCardChoiceA.Checked = false;
            chkCardChoiceB.Checked = false;
            chkCardChoiceC.Checked = false;
            chkCardChoiceD.Checked = false;
            chkCardChoiceE.Checked = false;
        }
        #endregion

        #region 查看答题卡事件
        private void btnAnswerCardEn_Click(object sender, EventArgs e)//完成问答答题卡修改
        {
            questionEntity.EssayQuestionSubject = txtEssayQuestion.Text;
            questionEntity.StuEssayQuestionAnswer = txtCardEssayAnswer.Text;
            bool flag = questionOperateDAO.updateQuestion(questionEntity);
            if (flag == true)
            {
                MessageBox.Show("修改成功");
            }
            AnswerCard();
            pnlAnswerCardE.Visible = false;
        }

        public string requestAnswer(string answer1)
        {
            if (this.chkCardChoiceA.Checked == true)
                answer1 = answer1 + "A";
            if (this.chkCardChoiceB.Checked == true)
                answer1 = answer1 + "B";
            if (this.chkCardChoiceC.Checked == true)
                answer1 = answer1 + "C";
            if (this.chkCardChoiceD.Checked == true)
                answer1 = answer1 + "D";
            if (this.chkCardChoiceE.Checked == true)
                answer1 = answer1 + "E";
            return answer1;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            pnlAnswerCardChoice.Visible = false;
            string answer2 = requestAnswer("");
            a1.Text = answer2;
            sAnswer[0] = a1.Text;
            AnswerCard();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            pnlAnswerCardChoice.Visible = false;
            string answer2 = requestAnswer("");
            a2.Text = answer2;
            sAnswer[1] = a2.Text;
            AnswerCard();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            pnlAnswerCardChoice.Visible = false;
            string answer2 = requestAnswer("");
            a3.Text = answer2;
            sAnswer[2] = a3.Text;
            AnswerCard();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            pnlAnswerCardChoice.Visible = false;
            string answer2 = requestAnswer("");
            a4.Text = answer2;
            sAnswer[3] = a4.Text;
            AnswerCard();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            pnlAnswerCardChoice.Visible = false;
            string answer2 = requestAnswer("");
            a5.Text = answer2;
            sAnswer[4] = a5.Text;
            AnswerCard();
        }

        private void lklChoice1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//答题卡选择题1
        {
            chkCardChoiceClear();
            pnlAnswerCardChoice.Visible = true;
            lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
            ds = searchExamInfo.searchAllChoice(lessonInfoEntity);
            txtChoiceCard.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[0].ToString())][0].ToString();
            txtCardChoiceA.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[0].ToString())][1].ToString();
            txtCardChoiceB.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[0].ToString())][2].ToString();
            txtCardChoiceC.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[0].ToString())][3].ToString();
            txtCardChoiceD.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[0].ToString())][4].ToString();
            txtCardChoiceE.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[0].ToString())][5].ToString();
            btn1.Visible = true;
            btn2.Visible = false;
            btn3.Visible = false;
            btn4.Visible = false;
            btn5.Visible = false;
        }
        private void lklChoice2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//答题卡选择题2
        {
            chkCardChoiceClear();
            pnlAnswerCardChoice.Visible = true;
            lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
            ds = searchExamInfo.searchAllChoice(lessonInfoEntity);
            txtChoiceCard.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[1].ToString())][0].ToString();
            txtCardChoiceA.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[1].ToString())][1].ToString();
            txtCardChoiceB.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[1].ToString())][2].ToString();
            txtCardChoiceC.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[1].ToString())][3].ToString();
            txtCardChoiceD.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[1].ToString())][4].ToString();
            txtCardChoiceE.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[1].ToString())][5].ToString();
            btn1.Visible = false;
            btn2.Visible = true;
            btn3.Visible = false;
            btn4.Visible = false;
            btn5.Visible = false;
        }
        private void lklChoice3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//答题卡选择题3
        {
            chkCardChoiceClear();
            pnlAnswerCardChoice.Visible = true;
            lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
            ds = searchExamInfo.searchAllChoice(lessonInfoEntity);
            txtChoiceCard.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[2].ToString())][0].ToString();
            txtCardChoiceA.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[2].ToString())][1].ToString();
            txtCardChoiceB.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[2].ToString())][2].ToString();
            txtCardChoiceC.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[2].ToString())][3].ToString();
            txtCardChoiceD.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[2].ToString())][4].ToString();
            txtCardChoiceE.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[2].ToString())][5].ToString();
            btn1.Visible = false;
            btn2.Visible = false;
            btn3.Visible = true;
            btn4.Visible = false;
            btn5.Visible = false;
        }
        private void lklChoice4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//答题卡选择题4
        {
            chkCardChoiceClear();
            pnlAnswerCardChoice.Visible = true;
            lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
            ds = searchExamInfo.searchAllChoice(lessonInfoEntity);
            txtChoiceCard.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[3].ToString())][0].ToString();
            txtCardChoiceA.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[3].ToString())][1].ToString();
            txtCardChoiceB.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[3].ToString())][2].ToString();
            txtCardChoiceC.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[3].ToString())][3].ToString();
            txtCardChoiceD.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[3].ToString())][4].ToString();
            txtCardChoiceE.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[3].ToString())][5].ToString();
            btn1.Visible = false;
            btn2.Visible = false;
            btn3.Visible = false;
            btn4.Visible = true;
            btn5.Visible = false;
        }
        private void lklChoice5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//答题卡选择题5
        {
            chkCardChoiceClear();
            pnlAnswerCardChoice.Visible = true;
            lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
            ds = searchExamInfo.searchAllChoice(lessonInfoEntity);
            txtChoiceCard.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[4].ToString())][0].ToString();
            txtCardChoiceA.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[4].ToString())][1].ToString();
            txtCardChoiceB.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[4].ToString())][2].ToString();
            txtCardChoiceC.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[4].ToString())][3].ToString();
            txtCardChoiceD.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[4].ToString())][4].ToString();
            txtCardChoiceE.Text = ds.Tables["SearchAllChoice"].Rows[int.Parse(arr[4].ToString())][5].ToString();
            btn1.Visible = false;
            btn2.Visible = false;
            btn3.Visible = false;
            btn4.Visible = false;
            btn5.Visible = true;
      
        }
        private void lklEssayQuestion1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//答题卡问答题1
        {
            pnlAnswerCardE.Visible = true;
            questionEntity.StuLoginName = Entity.LoginInfoEntity.LoginName;
            lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
            ds = questionOperateDAO.searchQuestionOperate1(questionEntity,lessonInfoEntity);
            txtEssayQuestion.Text = ds.Tables["SearchQuestionOperate1"].Rows[0][0] + "";
            txtCardEssayAnswer.Text = ds.Tables["SearchQuestionOperate1"].Rows[0][1] + "";
        }

        private void lklEssayQuestion2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//答题卡问答题2
        {
            pnlAnswerCardE.Visible = true;
            questionEntity.StuLoginName = Entity.LoginInfoEntity.LoginName;
            lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
            ds = questionOperateDAO.searchQuestionOperate1(questionEntity,lessonInfoEntity);
            txtEssayQuestion.Text = ds.Tables["SearchQuestionOperate1"].Rows[1][0] + "";
            txtCardEssayAnswer.Text = ds.Tables["SearchQuestionOperate1"].Rows[1][1] + "";
        }

        private void lklEssayQuestion3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//答题卡问答题3
        {
            pnlAnswerCardE.Visible = true;
            questionEntity.StuLoginName = Entity.LoginInfoEntity.LoginName;
            lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
            ds = questionOperateDAO.searchQuestionOperate1(questionEntity,lessonInfoEntity);
            txtEssayQuestion.Text = ds.Tables["SearchQuestionOperate1"].Rows[2][0] + "";
            txtCardEssayAnswer.Text = ds.Tables["SearchQuestionOperate1"].Rows[2][1] + "";
        }

        private void lklEssayQuestion4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//答题卡问答题4
        {
            pnlAnswerCardE.Visible = true;
            questionEntity.StuLoginName = Entity.LoginInfoEntity.LoginName;
            lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
            ds = questionOperateDAO.searchQuestionOperate1(questionEntity,lessonInfoEntity);
            txtEssayQuestion.Text = ds.Tables["SearchQuestionOperate1"].Rows[3][0] + "";
            txtCardEssayAnswer.Text = ds.Tables["SearchQuestionOperate1"].Rows[3][1] + "";
        }

        private void lklEssayQuestion5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//答题卡问答题5
        {
            pnlAnswerCardE.Visible = true;
            questionEntity.StuLoginName = Entity.LoginInfoEntity.LoginName;
            lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
            ds = questionOperateDAO.searchQuestionOperate1(questionEntity,lessonInfoEntity);
            txtEssayQuestion.Text = ds.Tables["SearchQuestionOperate1"].Rows[4][0] + "";
            txtCardEssayAnswer.Text = ds.Tables["SearchQuestionOperate1"].Rows[4][1] + "";
        }
        #endregion


        #region 教师批改问答题选择班级获取学生姓名事件
        private void cboTeacherClass_SelectionChangeCommitted(object sender, EventArgs e)
        {
            classInfoEntity.ClassId = int.Parse(cboTeacherClass.SelectedValue+"");
            ds = stuMarkInfoDao.searchStuNameByClass(classInfoEntity);
            cboStudent.DataSource = ds.Tables["SearchStuNameByClass"];
            cboStudent.ValueMember = "StuLoginName";
            cboStudent.DisplayMember = "StuName";
        }
        #endregion

        #region 教师批改问答题事件
        private void button2_Click(object sender, EventArgs e)
        {
            pnlEditEssayQuestion.Visible = true;
            pnlEssayQuestion.Visible = false;
            pnlExamMark.Visible = false;
            pnlBeginExam.Visible = false;
            pnlExam.Visible = false;
            pnlInquireEditUser.Visible = false;
            pnlInquireUserMark.Visible = false;
            pnlMessageShow.Visible = false;
            spConBBS.Visible = false;

            userInfoEntity.UserLoginName = cboStudent.SelectedValue+"";
            lessonInfoEntity.LessonId = int.Parse(cboLessonId.SelectedValue+"");
            ds = stuMarkInfoDao.searchStuExamQuestion(userInfoEntity,lessonInfoEntity);
            textBox1.Text = ds.Tables["SearchStuExamQuestion"].Rows[0][0] + "";
            txtEssayConent.Text = ds.Tables["SearchStuExamQuestion"].Rows[0][1]+"";
            txtEssayAnswer.Text = ds.Tables["SearchStuExamQuestion"].Rows[0][2]+"";
            txtStuEssayAnswer.Text = ds.Tables["SearchStuExamQuestion"].Rows[0][3] + "";
            txtStuName.Text = ds.Tables["SearchStuExamQuestion"].Rows[0][4] + "";
            label31.Text = "题目" + 1 + "";
        }
        #endregion

        #region 教师批改问答提下一题按钮单击事件 

        private void btnEditEssayNext_Click(object sender, EventArgs e)
        {
            if (txtEssayMark.Text.Equals("") || int.Parse(txtEssayMark.Text) < 0 || int.Parse(txtEssayMark.Text) > 8)
            {
                lbl30.Visible = true;
                txtEssayMark.Focus();
                return;
            }
            else
            {
                lbl30.Visible = false;
            }
            n++;
            if (n > 5)
            {
                n = 5;
                btnEditEssayNext.Enabled = false;
            }
            if (n <= 5 && n > 0)
            {
                label31.Text = "题目" + (n);
                txtEssayConent.Text = ds.Tables["SearchStuExamQuestion"].Rows[n - 1][1] + "";
                txtEssayAnswer.Text = ds.Tables["SearchStuExamQuestion"].Rows[n - 1][2] + "";
                txtStuEssayAnswer.Text = ds.Tables["SearchStuExamQuestion"].Rows[n - 1][3] + "";

                lessonInfoEntity.LessonId = int.Parse(cboLessonId.SelectedValue+"");
                stuMarkEntity.StuEssatQuestionMark = int.Parse(txtEssayMark.Text);
                userInfoEntity.UserLoginName = textBox1.Text;
                stuMarkInfoDao.updatetStuEssayMark(lessonInfoEntity, userInfoEntity, stuMarkEntity); //把分数入库
                txtEssayMark.Text = "";
                txtEssayMark.Focus();
                stuMarkInfoDao.updateStuMark(lessonInfoEntity, userInfoEntity, stuMarkEntity);//计算学生总分
            }
        }
        #endregion

        #region 教师批改问答提上一题按钮单击事件
        private void btnEditEssayUp_Click(object sender, EventArgs e)
        {
            try
            {
                n--;
                if (n <=0)
                    n = 1;
                if (n > 0 && n < 5)
                {
                    lessonInfoEntity.LessonId = int.Parse(cboLessonId.SelectedValue + "");
                    stuMarkEntity.StuEssatQuestionMark = int.Parse(txtEssayMark.Text);
                    userInfoEntity.UserLoginName = textBox1.Text;
                    btnEditEssayNext.Enabled = true;
                    label31.Text = "题目" + (n);
                    txtEssayConent.Text = ds.Tables["SearchStuExamQuestion"].Rows[n - 1][1] + "";
                    txtEssayAnswer.Text = ds.Tables["SearchStuExamQuestion"].Rows[n - 1][2] + "";
                    txtStuEssayAnswer.Text = ds.Tables["SearchStuExamQuestion"].Rows[n - 1][3] + "";
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 答题卡查看答案是否填写方法
        private void AnswerCard()
        {
            questionEntity.StuLoginName = Entity.LoginInfoEntity.LoginName;
            lessonInfoEntity.LessonId = int.Parse(cboChoiceExamType.SelectedValue + "");
            ds = questionOperateDAO.searchQuestionOperate1(questionEntity,lessonInfoEntity);
            if (!(ds.Tables["SearchQuestionOperate1"].Rows[0][1] + "").Equals(""))
            {
                lklEssayQuestion1.LinkColor = Color.Black;
            }
            else
            {
                lklEssayQuestion1.LinkColor = Color.Blue;
            }
            if (!(ds.Tables["SearchQuestionOperate1"].Rows[1][1] + "").Equals(""))
            {
                lklEssayQuestion2.LinkColor = Color.Black;
            }
            else
            {
                lklEssayQuestion2.LinkColor = Color.Blue;
            }
            if (!(ds.Tables["SearchQuestionOperate1"].Rows[2][1] + "").Equals(""))
            {
                lklEssayQuestion3.LinkColor = Color.Black;
            }
            else
            {
                lklEssayQuestion3.LinkColor = Color.Blue;
            }
            if (!(ds.Tables["SearchQuestionOperate1"].Rows[3][1] + "").Equals(""))
            {
                lklEssayQuestion4.LinkColor = Color.Black;
            }
            else
            {
                lklEssayQuestion4.LinkColor = Color.Blue;
            }
            if (!(ds.Tables["SearchQuestionOperate1"].Rows[4][1] + "").Equals(""))
            {
                lklEssayQuestion5.LinkColor = Color.Black;
            }
            else
            {
                lklEssayQuestion5.LinkColor = Color.Blue;
            }

            if (!a1.Text.Equals(""))
            {
                lklChoice1.LinkColor = Color.Black;
            }
            else
            {
                lklChoice1.LinkColor = Color.Blue;
            }
            if (!a2.Text.Equals(""))
            {
                lklChoice2.LinkColor = Color.Black;
            }
            else
            {
                lklChoice2.LinkColor = Color.Blue;
            }

            if (!a3.Text.Equals(""))
            {
                lklChoice3.LinkColor = Color.Black;
            }
            else
            {
                lklChoice3.LinkColor = Color.Blue;
            }
            if (!a4.Text.Equals(""))
            {
                lklChoice4.LinkColor = Color.Black;
            }
            else
            {
                lklChoice4.LinkColor = Color.Blue;
            }
            if (!a5.Text.Equals(""))
            {
                lklChoice5.LinkColor = Color.Black;
            }
            else
            {
                lklChoice5.LinkColor = Color.Blue;
            }



        }
        #endregion

        #region 点击答题卡查看答案是否填写事件
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnswerCard();
        }
        #endregion

        #region 教员批改学生问答题返回选择界面
        private void btnBackEditEssay_Click(object sender, EventArgs e)
        {
            pnlEssayQuestion.Visible = true;
            pnlExamMark.Visible = false;
            pnlExam.Visible = false;
            pnlInquireEditUser.Visible = false;
            pnlBeginExam.Visible = false;
            pnlInquireUserMark.Visible = false;
            pnlMessageShow.Visible = false;
            spConBBS.Visible = false;
            pnlEditEssayQuestion.Visible = false;
        }
        #endregion

        #region 成绩查询按钮单击事件
        private void btnInquireMark_Click(object sender, EventArgs e)
        {
            try
            {
                lessonInfoEntity.LessonId = int.Parse(cboInquireLesson.SelectedValue + "");
                classInfoEntity.ClassId = int.Parse(cboInquireClass.SelectedValue + "");
                userInfoEntity.UserName = txtInquireUserName.Text;
                ds = stuMarkInfoDao.searchStuMark(userInfoEntity, lessonInfoEntity, classInfoEntity);
                dgvStuMark.DataSource = ds.Tables["SearchStuMark"];
            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region 用户发送留言按钮单击事件
        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (richTextBox.Text.Equals(""))
            {
                label40.Visible = true;
                richTextBox.Focus();
                return;
            }
            else
            {
                label40.Visible = false;
            }
            messageBoardEntity.LeaveMessageTime = DateTime.Now+"";
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

        #region 状态栏左下角显示当前时间
        private void timer_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "当前日期:" + DateTime.Now;
        }
        #endregion

        #region 修改基本信息文本框鼠标焦点失去事件
        private void txtAge_Leave(object sender, EventArgs e)
        {
            lbl3.Visible = false;
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (!txtName.Text.Equals(""))
            {
                lbl1.Visible = false;
            }
        }
        #endregion

        private void tsbOnLineExam_Click(object sender, EventArgs e)
        {
            pnlExamMark.Visible = false;
            pnlExam.Visible = true;
            pnlInquireEditUser.Visible = false;
            pnlBeginExam.Visible = false;
            pnlInquireUserMark.Visible = false;
            pnlMessageShow.Visible = false;
            spConBBS.Visible = false;
            pnlEssayQuestion.Visible = false;
            pnlEditEssayQuestion.Visible = false;
            pnlEssayQuestion.Visible = false;
        }

        private void tsbBBS_Click(object sender, EventArgs e)
        {
            pnlExamMark.Visible = false;
            spConBBS.Visible = true;
            this.richTextBox.WordWrap = true;
            pnlExam.Visible = false;
            pnlInquireEditUser.Visible = false;
            pnlBeginExam.Visible = false;
            pnlInquireUserMark.Visible = false;
            pnlMessageShow.Visible = false;
            pnlEssayQuestion.Visible = false;
            pnlEditEssayQuestion.Visible = false;
            pnlEssayQuestion.Visible = false;
            ds = messageBoardInfoDao.searchBBSInfo();
            dgvBBSBoard.DataSource = ds.Tables["SearchBBSInfo"];
        }










    }
}