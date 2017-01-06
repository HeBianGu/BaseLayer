namespace WinApp
{
    partial class Form1
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnBegin = new System.Windows.Forms.Button();
            this.labComplete = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.btnSelectSavePath = new System.Windows.Forms.Button();
            this.txtResultShow = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbJpg = new System.Windows.Forms.CheckBox();
            this.cbBmp = new System.Windows.Forms.CheckBox();
            this.cbGif = new System.Windows.Forms.CheckBox();
            this.cbPng = new System.Windows.Forms.CheckBox();
            this.btnOpenSavePath = new System.Windows.Forms.Button();
            this.labDownResult = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.labUrl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRetryCount = new System.Windows.Forms.TextBox();
            this.rbGiveUp = new System.Windows.Forms.RadioButton();
            this.rbRetry = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBegin
            // 
            this.btnBegin.Location = new System.Drawing.Point(8, 60);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(51, 23);
            this.btnBegin.TabIndex = 1;
            this.btnBegin.Text = "开始";
            this.btnBegin.UseVisualStyleBackColor = true;
            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
            // 
            // labComplete
            // 
            this.labComplete.AutoSize = true;
            this.labComplete.Location = new System.Drawing.Point(146, 67);
            this.labComplete.Name = "labComplete";
            this.labComplete.Size = new System.Drawing.Size(0, 12);
            this.labComplete.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 707);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(257, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "如发现bug欢迎邮箱告知381126994@qq.com,谢谢";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(66, 60);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(55, 23);
            this.btnStop.TabIndex = 14;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "保存路径";
            // 
            // txtSavePath
            // 
            this.txtSavePath.Enabled = false;
            this.txtSavePath.Location = new System.Drawing.Point(66, 34);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(312, 21);
            this.txtSavePath.TabIndex = 16;
            // 
            // btnSelectSavePath
            // 
            this.btnSelectSavePath.Location = new System.Drawing.Point(384, 34);
            this.btnSelectSavePath.Name = "btnSelectSavePath";
            this.btnSelectSavePath.Size = new System.Drawing.Size(39, 23);
            this.btnSelectSavePath.TabIndex = 17;
            this.btnSelectSavePath.Text = "选择";
            this.btnSelectSavePath.UseVisualStyleBackColor = true;
            this.btnSelectSavePath.Click += new System.EventHandler(this.btnSelectSavePath_Click);
            // 
            // txtResultShow
            // 
            this.txtResultShow.Location = new System.Drawing.Point(3, 97);
            this.txtResultShow.Multiline = true;
            this.txtResultShow.Name = "txtResultShow";
            this.txtResultShow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultShow.Size = new System.Drawing.Size(490, 551);
            this.txtResultShow.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "图片类型";
            // 
            // cbJpg
            // 
            this.cbJpg.AutoSize = true;
            this.cbJpg.Checked = true;
            this.cbJpg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbJpg.Location = new System.Drawing.Point(74, 95);
            this.cbJpg.Name = "cbJpg";
            this.cbJpg.Size = new System.Drawing.Size(72, 16);
            this.cbJpg.TabIndex = 19;
            this.cbJpg.Text = "jpg/jpeg";
            this.cbJpg.UseVisualStyleBackColor = true;
            // 
            // cbBmp
            // 
            this.cbBmp.AutoSize = true;
            this.cbBmp.Location = new System.Drawing.Point(230, 95);
            this.cbBmp.Name = "cbBmp";
            this.cbBmp.Size = new System.Drawing.Size(42, 16);
            this.cbBmp.TabIndex = 20;
            this.cbBmp.Text = "bmp";
            this.cbBmp.UseVisualStyleBackColor = true;
            // 
            // cbGif
            // 
            this.cbGif.AutoSize = true;
            this.cbGif.Location = new System.Drawing.Point(163, 95);
            this.cbGif.Name = "cbGif";
            this.cbGif.Size = new System.Drawing.Size(42, 16);
            this.cbGif.TabIndex = 21;
            this.cbGif.Text = "gif";
            this.cbGif.UseVisualStyleBackColor = true;
            // 
            // cbPng
            // 
            this.cbPng.AutoSize = true;
            this.cbPng.Location = new System.Drawing.Point(289, 96);
            this.cbPng.Name = "cbPng";
            this.cbPng.Size = new System.Drawing.Size(42, 16);
            this.cbPng.TabIndex = 22;
            this.cbPng.Text = "png";
            this.cbPng.UseVisualStyleBackColor = true;
            // 
            // btnOpenSavePath
            // 
            this.btnOpenSavePath.Location = new System.Drawing.Point(429, 34);
            this.btnOpenSavePath.Name = "btnOpenSavePath";
            this.btnOpenSavePath.Size = new System.Drawing.Size(39, 23);
            this.btnOpenSavePath.TabIndex = 23;
            this.btnOpenSavePath.Text = "打开";
            this.btnOpenSavePath.UseVisualStyleBackColor = true;
            this.btnOpenSavePath.Click += new System.EventHandler(this.btnOpenSavePath_Click);
            // 
            // labDownResult
            // 
            this.labDownResult.AutoSize = true;
            this.labDownResult.Location = new System.Drawing.Point(314, 67);
            this.labDownResult.Name = "labDownResult";
            this.labDownResult.Size = new System.Drawing.Size(0, 12);
            this.labDownResult.TabIndex = 25;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(5, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(507, 679);
            this.tabControl1.TabIndex = 26;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cbType);
            this.tabPage1.Controls.Add(this.txtTo);
            this.tabPage1.Controls.Add(this.labUrl);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtFrom);
            this.tabPage1.Controls.Add(this.txtResultShow);
            this.tabPage1.Controls.Add(this.labDownResult);
            this.tabPage1.Controls.Add(this.btnStop);
            this.tabPage1.Controls.Add(this.btnBegin);
            this.tabPage1.Controls.Add(this.labComplete);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(499, 654);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "下载";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "从";
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(8, 15);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(87, 20);
            this.cbType.TabIndex = 26;
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(231, 14);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(42, 21);
            this.txtTo.TabIndex = 28;
            // 
            // labUrl
            // 
            this.labUrl.AutoSize = true;
            this.labUrl.Location = new System.Drawing.Point(303, 22);
            this.labUrl.Name = "labUrl";
            this.labUrl.Size = new System.Drawing.Size(0, 12);
            this.labUrl.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "到";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(279, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "页";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(159, 15);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(42, 21);
            this.txtFrom.TabIndex = 27;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.txtTimeOut);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.btnOpenSavePath);
            this.tabPage2.Controls.Add(this.txtSavePath);
            this.tabPage2.Controls.Add(this.btnSelectSavePath);
            this.tabPage2.Controls.Add(this.cbPng);
            this.tabPage2.Controls.Add(this.cbJpg);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.cbGif);
            this.tabPage2.Controls.Add(this.cbBmp);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(499, 654);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtRetryCount);
            this.groupBox1.Controls.Add(this.rbGiveUp);
            this.groupBox1.Controls.Add(this.rbRetry);
            this.groupBox1.Location = new System.Drawing.Point(14, 201);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 49);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "下载失败时候";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(312, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 31;
            this.label10.Text = "建议别超过3次";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(216, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 30;
            this.label9.Text = "尝试次数";
            // 
            // txtRetryCount
            // 
            this.txtRetryCount.Location = new System.Drawing.Point(275, 19);
            this.txtRetryCount.Name = "txtRetryCount";
            this.txtRetryCount.Size = new System.Drawing.Size(31, 21);
            this.txtRetryCount.TabIndex = 29;
            this.txtRetryCount.Text = "2";
            // 
            // rbGiveUp
            // 
            this.rbGiveUp.AutoSize = true;
            this.rbGiveUp.Location = new System.Drawing.Point(15, 20);
            this.rbGiveUp.Name = "rbGiveUp";
            this.rbGiveUp.Size = new System.Drawing.Size(47, 16);
            this.rbGiveUp.TabIndex = 28;
            this.rbGiveUp.Text = "放弃";
            this.rbGiveUp.UseVisualStyleBackColor = true;
            this.rbGiveUp.CheckedChanged += new System.EventHandler(this.rbGiveUp_CheckedChanged);
            // 
            // rbRetry
            // 
            this.rbRetry.AutoSize = true;
            this.rbRetry.Checked = true;
            this.rbRetry.Location = new System.Drawing.Point(111, 20);
            this.rbRetry.Name = "rbRetry";
            this.rbRetry.Size = new System.Drawing.Size(71, 16);
            this.rbRetry.TabIndex = 27;
            this.rbRetry.TabStop = true;
            this.rbRetry.Text = "自动重试";
            this.rbRetry.UseVisualStyleBackColor = true;
            this.rbRetry.CheckedChanged += new System.EventHandler(this.rbRetry_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(106, 158);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "秒(建议不要超过20)";
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.Location = new System.Drawing.Point(71, 152);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(29, 21);
            this.txtTimeOut.TabIndex = 25;
            this.txtTimeOut.Text = "10";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "下载超时";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 732);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "煎蛋网图片下载";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBegin;
        private System.Windows.Forms.Label labComplete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.Button btnSelectSavePath;
        private System.Windows.Forms.TextBox txtResultShow;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbJpg;
        private System.Windows.Forms.CheckBox cbBmp;
        private System.Windows.Forms.CheckBox cbGif;
        private System.Windows.Forms.CheckBox cbPng;
        private System.Windows.Forms.Button btnOpenSavePath;
        private System.Windows.Forms.Label labDownResult;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label labUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTimeOut;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbGiveUp;
        private System.Windows.Forms.RadioButton rbRetry;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRetryCount;
        private System.Windows.Forms.Label label10;
    }
}

