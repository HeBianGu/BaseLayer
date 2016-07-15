namespace OPT.PCOCCenter.TaskProxy
{
    partial class SimulationSettingForm
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
            this.labelHostName = new DevExpress.XtraEditors.LabelControl();
            this.labelHostIP = new DevExpress.XtraEditors.LabelControl();
            this.labelEclipsePath = new DevExpress.XtraEditors.LabelControl();
            this.edtHostName = new DevExpress.XtraEditors.TextEdit();
            this.comboHostIP = new DevExpress.XtraEditors.ComboBoxEdit();
            this.edtEclipsePath = new DevExpress.XtraEditors.TextEdit();
            this.btnExplorerEclipse = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.edtLicensePath = new DevExpress.XtraEditors.TextEdit();
            this.btnExplorerLicense = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.edtHostName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboHostIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEclipsePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLicensePath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHostName
            // 
            this.labelHostName.Location = new System.Drawing.Point(30, 43);
            this.labelHostName.Name = "labelHostName";
            this.labelHostName.Size = new System.Drawing.Size(48, 14);
            this.labelHostName.TabIndex = 0;
            this.labelHostName.Text = "主机名称";
            // 
            // labelHostIP
            // 
            this.labelHostIP.Location = new System.Drawing.Point(30, 82);
            this.labelHostIP.Name = "labelHostIP";
            this.labelHostIP.Size = new System.Drawing.Size(35, 14);
            this.labelHostIP.TabIndex = 0;
            this.labelHostIP.Text = "主机IP";
            // 
            // labelEclipsePath
            // 
            this.labelEclipsePath.Location = new System.Drawing.Point(30, 155);
            this.labelEclipsePath.Name = "labelEclipsePath";
            this.labelEclipsePath.Size = new System.Drawing.Size(129, 14);
            this.labelEclipsePath.TabIndex = 0;
            this.labelEclipsePath.Text = "Eclipse路径(eclipse.exe)";
            // 
            // edtHostName
            // 
            this.edtHostName.Location = new System.Drawing.Point(192, 37);
            this.edtHostName.Name = "edtHostName";
            this.edtHostName.Size = new System.Drawing.Size(161, 20);
            this.edtHostName.TabIndex = 1;
            // 
            // comboHostIP
            // 
            this.comboHostIP.Location = new System.Drawing.Point(192, 76);
            this.comboHostIP.Name = "comboHostIP";
            this.comboHostIP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboHostIP.Size = new System.Drawing.Size(161, 20);
            this.comboHostIP.TabIndex = 2;
            // 
            // edtEclipsePath
            // 
            this.edtEclipsePath.Location = new System.Drawing.Point(192, 152);
            this.edtEclipsePath.Name = "edtEclipsePath";
            this.edtEclipsePath.Size = new System.Drawing.Size(311, 20);
            this.edtEclipsePath.TabIndex = 1;
            // 
            // btnExplorerEclipse
            // 
            this.btnExplorerEclipse.Location = new System.Drawing.Point(503, 151);
            this.btnExplorerEclipse.Name = "btnExplorerEclipse";
            this.btnExplorerEclipse.Size = new System.Drawing.Size(29, 21);
            this.btnExplorerEclipse.TabIndex = 3;
            this.btnExplorerEclipse.Text = "...";
            this.btnExplorerEclipse.Click += new System.EventHandler(this.btnExplorerEclipse_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(372, 201);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(457, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 118);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(152, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Eclipse授权路径(license.dat)";
            // 
            // edtLicensePath
            // 
            this.edtLicensePath.Location = new System.Drawing.Point(192, 115);
            this.edtLicensePath.Name = "edtLicensePath";
            this.edtLicensePath.Size = new System.Drawing.Size(311, 20);
            this.edtLicensePath.TabIndex = 1;
            // 
            // btnExplorerLicense
            // 
            this.btnExplorerLicense.Location = new System.Drawing.Point(503, 114);
            this.btnExplorerLicense.Name = "btnExplorerLicense";
            this.btnExplorerLicense.Size = new System.Drawing.Size(29, 21);
            this.btnExplorerLicense.TabIndex = 3;
            this.btnExplorerLicense.Text = "...";
            this.btnExplorerLicense.Click += new System.EventHandler(btnExplorerLicense_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 253);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnExplorerLicense);
            this.Controls.Add(this.btnExplorerEclipse);
            this.Controls.Add(this.comboHostIP);
            this.Controls.Add(this.edtLicensePath);
            this.Controls.Add(this.edtEclipsePath);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.edtHostName);
            this.Controls.Add(this.labelEclipsePath);
            this.Controls.Add(this.labelHostIP);
            this.Controls.Add(this.labelHostName);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注册Eclipse主机";
            ((System.ComponentModel.ISupportInitialize)(this.edtHostName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboHostIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEclipsePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLicensePath.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelHostName;
        private DevExpress.XtraEditors.LabelControl labelHostIP;
        private DevExpress.XtraEditors.LabelControl labelEclipsePath;
        private DevExpress.XtraEditors.TextEdit edtHostName;
        private DevExpress.XtraEditors.ComboBoxEdit comboHostIP;
        private DevExpress.XtraEditors.TextEdit edtEclipsePath;
        private DevExpress.XtraEditors.SimpleButton btnExplorerEclipse;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit edtLicensePath;
        private DevExpress.XtraEditors.SimpleButton btnExplorerLicense;
    }
}

