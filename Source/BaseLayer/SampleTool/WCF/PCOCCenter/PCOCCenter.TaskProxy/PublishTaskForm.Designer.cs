namespace OPT.PCOCCenter.TaskProxy
{
    partial class PublishTaskForm
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
            this.radioHostGroup = new DevExpress.XtraEditors.RadioGroup();
            this.vGridTaskInfos = new DevExpress.XtraVerticalGrid.VGridControl();
            this.btnLocalHostSetting = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.radioHostGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vGridTaskInfos)).BeginInit();
            this.SuspendLayout();
            // 
            // radioHostGroup
            // 
            this.radioHostGroup.AllowDrop = true;
            this.radioHostGroup.EditValue = "localHost";
            this.radioHostGroup.Location = new System.Drawing.Point(12, 15);
            this.radioHostGroup.Name = "radioHostGroup";
            this.radioHostGroup.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioHostGroup.Properties.Appearance.Options.UseBackColor = true;
            this.radioHostGroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioHostGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("localHost", "本机运算"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("autoHost", "自动分配")});
            this.radioHostGroup.Size = new System.Drawing.Size(416, 35);
            this.radioHostGroup.TabIndex = 0;
            this.radioHostGroup.SelectedIndexChanged += new System.EventHandler(this.radioHostGroup_SelectedIndexChanged);
            // 
            // vGridTaskInfos
            // 
            this.vGridTaskInfos.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.vGridTaskInfos.LayoutStyle = DevExpress.XtraVerticalGrid.LayoutViewStyle.SingleRecordView;
            this.vGridTaskInfos.Location = new System.Drawing.Point(12, 93);
            this.vGridTaskInfos.Name = "vGridTaskInfos";
            this.vGridTaskInfos.OptionsView.MinRowAutoHeight = 30;
            this.vGridTaskInfos.RecordWidth = 124;
            this.vGridTaskInfos.RowHeaderWidth = 76;
            this.vGridTaskInfos.Size = new System.Drawing.Size(542, 220);
            this.vGridTaskInfos.TabIndex = 1;
            // 
            // btnLocalHostSetting
            // 
            this.btnLocalHostSetting.Location = new System.Drawing.Point(450, 15);
            this.btnLocalHostSetting.Name = "btnLocalHostSetting";
            this.btnLocalHostSetting.Size = new System.Drawing.Size(104, 35);
            this.btnLocalHostSetting.TabIndex = 2;
            this.btnLocalHostSetting.Text = "本机模拟器设置";
            this.btnLocalHostSetting.Click += new System.EventHandler(this.btnLocalHostSetting_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 73);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "任务列表";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(331, 340);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(427, 340);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PublishTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(566, 392);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnLocalHostSetting);
            this.Controls.Add(this.vGridTaskInfos);
            this.Controls.Add(this.radioHostGroup);
            this.Name = "PublishTaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "运算设置";
            this.Load += new System.EventHandler(this.PublishTaskForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radioHostGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vGridTaskInfos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup radioHostGroup;
        private DevExpress.XtraVerticalGrid.VGridControl vGridTaskInfos;
        private DevExpress.XtraEditors.SimpleButton btnLocalHostSetting;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}