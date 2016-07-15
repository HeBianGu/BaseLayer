namespace OPT.PEOfficeCenter.LicenseManager
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.labelTitle = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.btnConnect = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.vGridControl = new DevExpress.XtraVerticalGrid.VGridControl();
            this.edtUserName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.edtPassword = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.itemUserName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.itemPassword = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Location = new System.Drawing.Point(12, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(118, 14);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "登录PEOffice许可管理";
            // 
            // pictureEdit
            // 
            this.pictureEdit.EditValue = global::OPT.PEOfficeCenter.LicenseManager.Properties.Resources.opendata;
            this.pictureEdit.Location = new System.Drawing.Point(270, -2);
            this.pictureEdit.Name = "pictureEdit";
            this.pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit.Size = new System.Drawing.Size(102, 54);
            this.pictureEdit.TabIndex = 1;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(172, 176);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "登录";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(270, 176);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // vGridControl
            // 
            this.vGridControl.Appearance.Empty.BackColor = System.Drawing.SystemColors.Control;
            this.vGridControl.Appearance.Empty.Options.UseBackColor = true;
            this.vGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.vGridControl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.vGridControl.LayoutStyle = DevExpress.XtraVerticalGrid.LayoutViewStyle.SingleRecordView;
            this.vGridControl.Location = new System.Drawing.Point(-1, 78);
            this.vGridControl.Name = "vGridControl";
            this.vGridControl.OptionsView.MinRowAutoHeight = 22;
            this.vGridControl.RecordWidth = 120;
            this.vGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.edtUserName,
            this.edtPassword});
            this.vGridControl.RowHeaderWidth = 80;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.itemUserName,
            this.itemPassword});
            this.vGridControl.Size = new System.Drawing.Size(373, 72);
            this.vGridControl.TabIndex = 4;
            // 
            // edtUserName
            // 
            this.edtUserName.AutoHeight = false;
            this.edtUserName.Name = "edtUserName";
            // 
            // edtPassword
            // 
            this.edtPassword.AutoHeight = false;
            this.edtPassword.Name = "edtPassword";
            this.edtPassword.UseSystemPasswordChar = true;
            // 
            // itemUserName
            // 
            this.itemUserName.Name = "itemUserName";
            this.itemUserName.Properties.Caption = "用户名";
            this.itemUserName.Properties.RowEdit = this.edtUserName;
            this.itemUserName.Properties.ToolTip = "登录PEOffice服务器的用户名";
            // 
            // itemPassword
            // 
            this.itemPassword.Name = "itemPassword";
            this.itemPassword.Properties.Caption = "密码";
            this.itemPassword.Properties.RowEdit = this.edtPassword;
            this.itemPassword.Properties.ToolTip = "登录PEOffice服务器的密码";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 229);
            this.Controls.Add(this.vGridControl);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.pictureEdit);
            this.Controls.Add(this.labelTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录许可管理";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelTitle;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraEditors.SimpleButton btnConnect;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraVerticalGrid.VGridControl vGridControl;

        private DevExpress.XtraVerticalGrid.Rows.EditorRow itemUserName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow itemPassword;

        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edtUserName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edtPassword;
    }
}