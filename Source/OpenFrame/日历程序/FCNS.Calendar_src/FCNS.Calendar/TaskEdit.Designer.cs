namespace FCNS.Calendar
{
    partial class TaskEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskEdit));
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.richTextBoxSummary = new System.Windows.Forms.RichTextBox();
            this.labelSummary = new System.Windows.Forms.Label();
            this.checkBoxAlert = new System.Windows.Forms.CheckBox();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.labelStart = new System.Windows.Forms.Label();
            this.labelEnd = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxTask = new System.Windows.Forms.ListBox();
            this.dateTimePickerLoop = new System.Windows.Forms.DateTimePicker();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonAddWeek = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(6, 14);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(59, 12);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "标    题:";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(72, 10);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(200, 21);
            this.textBoxTitle.TabIndex = 1;
            // 
            // richTextBoxSummary
            // 
            this.richTextBoxSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxSummary.Location = new System.Drawing.Point(72, 96);
            this.richTextBoxSummary.Name = "richTextBoxSummary";
            this.richTextBoxSummary.Size = new System.Drawing.Size(264, 244);
            this.richTextBoxSummary.TabIndex = 2;
            this.richTextBoxSummary.Text = "";
            // 
            // labelSummary
            // 
            this.labelSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSummary.AutoSize = true;
            this.labelSummary.Location = new System.Drawing.Point(6, 96);
            this.labelSummary.Name = "labelSummary";
            this.labelSummary.Size = new System.Drawing.Size(59, 12);
            this.labelSummary.TabIndex = 3;
            this.labelSummary.Text = "内    容:";
            // 
            // checkBoxAlert
            // 
            this.checkBoxAlert.AutoSize = true;
            this.checkBoxAlert.Location = new System.Drawing.Point(288, 12);
            this.checkBoxAlert.Name = "checkBoxAlert";
            this.checkBoxAlert.Size = new System.Drawing.Size(48, 16);
            this.checkBoxAlert.TabIndex = 4;
            this.checkBoxAlert.Text = "报警";
            this.checkBoxAlert.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Location = new System.Drawing.Point(72, 37);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(200, 21);
            this.dateTimePickerStart.TabIndex = 5;
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(72, 69);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(200, 21);
            this.dateTimePickerEnd.TabIndex = 6;
            // 
            // labelStart
            // 
            this.labelStart.AutoSize = true;
            this.labelStart.Location = new System.Drawing.Point(6, 41);
            this.labelStart.Name = "labelStart";
            this.labelStart.Size = new System.Drawing.Size(59, 12);
            this.labelStart.TabIndex = 7;
            this.labelStart.Text = "开始时间:";
            // 
            // labelEnd
            // 
            this.labelEnd.AutoSize = true;
            this.labelEnd.Location = new System.Drawing.Point(6, 73);
            this.labelEnd.Name = "labelEnd";
            this.labelEnd.Size = new System.Drawing.Size(59, 12);
            this.labelEnd.TabIndex = 8;
            this.labelEnd.Text = "结束时间:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonDelete);
            this.groupBox1.Controls.Add(this.buttonAddWeek);
            this.groupBox1.Controls.Add(this.buttonAdd);
            this.groupBox1.Controls.Add(this.dateTimePickerLoop);
            this.groupBox1.Controls.Add(this.listBoxTask);
            this.groupBox1.Location = new System.Drawing.Point(342, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 330);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "任务循环:未开通";
            // 
            // listBoxTask
            // 
            this.listBoxTask.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBoxTask.FormattingEnabled = true;
            this.listBoxTask.ItemHeight = 12;
            this.listBoxTask.Location = new System.Drawing.Point(3, 107);
            this.listBoxTask.Name = "listBoxTask";
            this.listBoxTask.Size = new System.Drawing.Size(153, 220);
            this.listBoxTask.TabIndex = 0;
            // 
            // dateTimePickerLoop
            // 
            this.dateTimePickerLoop.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerLoop.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerLoop.Location = new System.Drawing.Point(10, 20);
            this.dateTimePickerLoop.Name = "dateTimePickerLoop";
            this.dateTimePickerLoop.Size = new System.Drawing.Size(139, 21);
            this.dateTimePickerLoop.TabIndex = 1;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(10, 47);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(43, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "增加";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonAddWeek
            // 
            this.buttonAddWeek.Location = new System.Drawing.Point(63, 47);
            this.buttonAddWeek.Name = "buttonAddWeek";
            this.buttonAddWeek.Size = new System.Drawing.Size(86, 23);
            this.buttonAddWeek.TabIndex = 3;
            this.buttonAddWeek.Text = "增加一个星期";
            this.buttonAddWeek.UseVisualStyleBackColor = true;
            this.buttonAddWeek.Click += new System.EventHandler(this.buttonAddWeek_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(10, 77);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(43, 23);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "删除";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // TaskDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 347);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelEnd);
            this.Controls.Add(this.labelStart);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.checkBoxAlert);
            this.Controls.Add(this.labelSummary);
            this.Controls.Add(this.richTextBoxSummary);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TaskDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "任务编辑框";
            this.Load += new System.EventHandler(this.TaskDetail_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskDetail_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.RichTextBox richTextBoxSummary;
        private System.Windows.Forms.Label labelSummary;
        private System.Windows.Forms.CheckBox checkBoxAlert;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label labelEnd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxTask;
        private System.Windows.Forms.Button buttonAddWeek;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.DateTimePicker dateTimePickerLoop;
        private System.Windows.Forms.Button buttonDelete;
    }
}