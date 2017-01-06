namespace MySchoolForeGround
{
    partial class FrmLogin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            //if (disposing && (components != null))
            //{
            //    components.Dispose();
            //}
            //base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rboStudent = new System.Windows.Forms.RadioButton();
            this.rboClassTeacher = new System.Windows.Forms.RadioButton();
            this.rboTeacher = new System.Windows.Forms.RadioButton();
            this.btnLoginYes = new System.Windows.Forms.Button();
            this.btnLoginExit = new System.Windows.Forms.Button();
            this.txtLoginPassWord = new System.Windows.Forms.TextBox();
            this.txtLoginID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.skinLogin = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rboStudent);
            this.groupBox1.Controls.Add(this.rboClassTeacher);
            this.groupBox1.Controls.Add(this.rboTeacher);
            this.groupBox1.Location = new System.Drawing.Point(32, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 44);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户类型";
            // 
            // rboStudent
            // 
            this.rboStudent.AutoSize = true;
            this.rboStudent.Checked = true;
            this.rboStudent.Location = new System.Drawing.Point(74, 18);
            this.rboStudent.Name = "rboStudent";
            this.rboStudent.Size = new System.Drawing.Size(47, 16);
            this.rboStudent.TabIndex = 3;
            this.rboStudent.TabStop = true;
            this.rboStudent.Text = "学生";
            this.rboStudent.UseVisualStyleBackColor = true;
            this.rboStudent.CheckedChanged += new System.EventHandler(this.rboStudent_CheckedChanged);
            // 
            // rboClassTeacher
            // 
            this.rboClassTeacher.AutoSize = true;
            this.rboClassTeacher.Location = new System.Drawing.Point(242, 18);
            this.rboClassTeacher.Name = "rboClassTeacher";
            this.rboClassTeacher.Size = new System.Drawing.Size(59, 16);
            this.rboClassTeacher.TabIndex = 3;
            this.rboClassTeacher.Text = "班主任";
            this.rboClassTeacher.UseVisualStyleBackColor = true;
            this.rboClassTeacher.CheckedChanged += new System.EventHandler(this.rboClassTeacher_CheckedChanged);
            // 
            // rboTeacher
            // 
            this.rboTeacher.AutoSize = true;
            this.rboTeacher.Location = new System.Drawing.Point(160, 18);
            this.rboTeacher.Name = "rboTeacher";
            this.rboTeacher.Size = new System.Drawing.Size(47, 16);
            this.rboTeacher.TabIndex = 3;
            this.rboTeacher.Text = "教员";
            this.rboTeacher.UseVisualStyleBackColor = true;
            this.rboTeacher.CheckedChanged += new System.EventHandler(this.rboTeacher_CheckedChanged);
            // 
            // btnLoginYes
            // 
            this.btnLoginYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnLoginYes.Location = new System.Drawing.Point(164, 129);
            this.btnLoginYes.Name = "btnLoginYes";
            this.btnLoginYes.Size = new System.Drawing.Size(75, 22);
            this.btnLoginYes.TabIndex = 11;
            this.btnLoginYes.Text = "确定";
            this.btnLoginYes.UseVisualStyleBackColor = true;
            this.btnLoginYes.Click += new System.EventHandler(this.btnLoginYes_Click);
            // 
            // btnLoginExit
            // 
            this.btnLoginExit.Location = new System.Drawing.Point(274, 129);
            this.btnLoginExit.Name = "btnLoginExit";
            this.btnLoginExit.Size = new System.Drawing.Size(75, 22);
            this.btnLoginExit.TabIndex = 12;
            this.btnLoginExit.Text = "退出";
            this.btnLoginExit.UseVisualStyleBackColor = true;
            this.btnLoginExit.Click += new System.EventHandler(this.btnLoginExit_Click);
            // 
            // txtLoginPassWord
            // 
            this.txtLoginPassWord.Location = new System.Drawing.Point(106, 44);
            this.txtLoginPassWord.Name = "txtLoginPassWord";
            this.txtLoginPassWord.PasswordChar = '*';
            this.txtLoginPassWord.Size = new System.Drawing.Size(243, 21);
            this.txtLoginPassWord.TabIndex = 9;
            // 
            // txtLoginID
            // 
            this.txtLoginID.Location = new System.Drawing.Point(106, 13);
            this.txtLoginID.Name = "txtLoginID";
            this.txtLoginID.Size = new System.Drawing.Size(243, 21);
            this.txtLoginID.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "密码:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "用户名:";
            // 
            // skinLogin
            // 
            this.skinLogin.SerialNumber = "";
            this.skinLogin.SkinFile = null;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 164);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLoginYes);
            this.Controls.Add(this.btnLoginExit);
            this.Controls.Add(this.txtLoginPassWord);
            this.Controls.Add(this.txtLoginID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登入-学生登入";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLoginYes;
        private System.Windows.Forms.Button btnLoginExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Sunisoft.IrisSkin.SkinEngine skinLogin;
        public System.Windows.Forms.TextBox txtLoginPassWord;
        public System.Windows.Forms.RadioButton rboStudent;
        public System.Windows.Forms.RadioButton rboClassTeacher;
        public System.Windows.Forms.RadioButton rboTeacher;
        public System.Windows.Forms.TextBox txtLoginID;
    }
}