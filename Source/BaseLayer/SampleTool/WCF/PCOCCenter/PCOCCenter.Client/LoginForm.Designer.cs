namespace OPT.PCOCCenter.Client
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
            this.labelTitle = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.btnConnect = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.vGridControl = new DevExpress.XtraVerticalGrid.VGridControl();
            this.comboBoxServer = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.edtUserName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.edtPassword = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.edtSavePassword = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.itemServer = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.itemUserName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.itemPassword = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.itemSavePassword = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSavePassword)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Location = new System.Drawing.Point(12, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(106, 14);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "连接PCOC数模平台服务器";
            // 
            // pictureEdit1
            // 
            this.pictureEdit.EditValue = global::OPT.PCOCCenter.Client.Properties.Resources.opendata;
            this.pictureEdit.Location = new System.Drawing.Point(270, -2);
            this.pictureEdit.Name = "pictureEdit";
            this.pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit.Size = new System.Drawing.Size(102, 54);
            this.pictureEdit.TabIndex = 1;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(172, 211);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "连接";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(270, 211);
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
            this.comboBoxServer,
            this.edtUserName,
            this.edtPassword,
            this.edtSavePassword});
            this.vGridControl.RowHeaderWidth = 80;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.itemServer,
            this.itemUserName,
            this.itemPassword,
            this.itemSavePassword});
            this.vGridControl.Size = new System.Drawing.Size(373, 106);
            this.vGridControl.TabIndex = 4;
            // 
            // comboBoxServer
            // 
            this.comboBoxServer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxServer.Name = "comboBoxServer";
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
            // edtSavePassword
            // 
            this.edtSavePassword.Name = "edtSavePassword";
            this.edtSavePassword.EditValueChanged +=new System.EventHandler(edtSavePassword_EditValueChanged);
            // 
            // itemServer
            // 
            this.itemServer.Name = "itemServer";
            this.itemServer.Properties.Caption = "服务器";
            this.itemServer.Properties.RowEdit = this.comboBoxServer;
            this.itemServer.Properties.ToolTip = "PCOC数模平台服务器地址";
            // 
            // itemUserName
            // 
            this.itemUserName.Name = "itemUserName";
            this.itemUserName.Properties.Caption = "用户名";
            this.itemUserName.Properties.RowEdit = this.edtUserName;
            this.itemUserName.Properties.ToolTip = "登陆PCOC数模平台的用户名";
            // 
            // itemPassword
            // 
            this.itemPassword.Name = "itemPassword";
            this.itemPassword.Properties.Caption = "密码";
            this.itemPassword.Properties.RowEdit = this.edtPassword;
            this.itemPassword.Properties.ToolTip = "登陆PCOC数模平台的密码";
            // 
            // itemSavePassword
            // 
            this.itemSavePassword.Name = "itemSavePassword";
            this.itemSavePassword.Properties.Caption = "记住密码";
            this.itemSavePassword.Properties.RowEdit = this.edtSavePassword;
            this.itemSavePassword.Properties.ToolTip = "记住密码后，下次自动连接";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 255);
            this.Controls.Add(this.vGridControl);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.pictureEdit);
            this.Controls.Add(this.labelTitle);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登陆PCOC数模平台";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSavePassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelTitle;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraEditors.SimpleButton btnConnect;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraVerticalGrid.VGridControl vGridControl;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow itemServer;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow itemUserName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow itemPassword;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow itemSavePassword;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox comboBoxServer;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edtUserName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edtPassword;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit edtSavePassword;

    }
}