namespace MySchoolBackGround
{
    partial class FrmLoginBG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLoginBG));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLoginID = new System.Windows.Forms.TextBox();
            this.txtLoginPassWord = new System.Windows.Forms.TextBox();
            this.btnLoginExit = new System.Windows.Forms.Button();
            this.btnLoginYes = new System.Windows.Forms.Button();
            this.skinLoginBG = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.rboFirstLeavelManager = new System.Windows.Forms.RadioButton();
            this.rboOrdinaryManager = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "密码:";
            // 
            // txtLoginID
            // 
            this.txtLoginID.Location = new System.Drawing.Point(100, 12);
            this.txtLoginID.Name = "txtLoginID";
            this.txtLoginID.Size = new System.Drawing.Size(243, 21);
            this.txtLoginID.TabIndex = 1;
            // 
            // txtLoginPassWord
            // 
            this.txtLoginPassWord.Location = new System.Drawing.Point(100, 43);
            this.txtLoginPassWord.Name = "txtLoginPassWord";
            this.txtLoginPassWord.PasswordChar = '*';
            this.txtLoginPassWord.Size = new System.Drawing.Size(243, 21);
            this.txtLoginPassWord.TabIndex = 2;
            // 
            // btnLoginExit
            // 
            this.btnLoginExit.Location = new System.Drawing.Point(268, 128);
            this.btnLoginExit.Name = "btnLoginExit";
            this.btnLoginExit.Size = new System.Drawing.Size(75, 22);
            this.btnLoginExit.TabIndex = 5;
            this.btnLoginExit.Text = "退出";
            this.btnLoginExit.UseVisualStyleBackColor = true;
            this.btnLoginExit.Click += new System.EventHandler(this.btnLoginExit_Click);
            // 
            // btnLoginYes
            // 
            this.btnLoginYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnLoginYes.Location = new System.Drawing.Point(158, 128);
            this.btnLoginYes.Name = "btnLoginYes";
            this.btnLoginYes.Size = new System.Drawing.Size(75, 22);
            this.btnLoginYes.TabIndex = 4;
            this.btnLoginYes.Text = "确定";
            this.btnLoginYes.UseVisualStyleBackColor = true;
            this.btnLoginYes.Click += new System.EventHandler(this.btnLoginYes_Click);
            // 
            // skinLoginBG
            // 
            this.skinLoginBG.SerialNumber = "";
            this.skinLoginBG.SkinFile = null;
            // 
            // rboFirstLeavelManager
            // 
            this.rboFirstLeavelManager.AutoSize = true;
            this.rboFirstLeavelManager.Checked = true;
            this.rboFirstLeavelManager.Location = new System.Drawing.Point(74, 18);
            this.rboFirstLeavelManager.Name = "rboFirstLeavelManager";
            this.rboFirstLeavelManager.Size = new System.Drawing.Size(83, 16);
            this.rboFirstLeavelManager.TabIndex = 3;
            this.rboFirstLeavelManager.TabStop = true;
            this.rboFirstLeavelManager.Text = "一级管理员";
            this.rboFirstLeavelManager.UseVisualStyleBackColor = true;
            this.rboFirstLeavelManager.CheckedChanged += new System.EventHandler(this.rboFirstLeavelManager_CheckedChanged);
            // 
            // rboOrdinaryManager
            // 
            this.rboOrdinaryManager.AutoSize = true;
            this.rboOrdinaryManager.Location = new System.Drawing.Point(180, 18);
            this.rboOrdinaryManager.Name = "rboOrdinaryManager";
            this.rboOrdinaryManager.Size = new System.Drawing.Size(83, 16);
            this.rboOrdinaryManager.TabIndex = 3;
            this.rboOrdinaryManager.Text = "普通管理员";
            this.rboOrdinaryManager.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rboFirstLeavelManager);
            this.groupBox1.Controls.Add(this.rboOrdinaryManager);
            this.groupBox1.Location = new System.Drawing.Point(26, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 44);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户类型";
            // 
            // FrmLoginBG
            // 
            this.AcceptButton = this.btnLoginYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 162);
            this.ControlBox = false;
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
            this.Name = "FrmLoginBG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登入-一级管理员";
            this.Load += new System.EventHandler(this.FrmLoginBG_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLoginID;
        private System.Windows.Forms.TextBox txtLoginPassWord;
        private System.Windows.Forms.Button btnLoginExit;
        private System.Windows.Forms.Button btnLoginYes;
        private Sunisoft.IrisSkin.SkinEngine skinLoginBG;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.RadioButton rboOrdinaryManager;
        public System.Windows.Forms.RadioButton rboFirstLeavelManager;
    }
}