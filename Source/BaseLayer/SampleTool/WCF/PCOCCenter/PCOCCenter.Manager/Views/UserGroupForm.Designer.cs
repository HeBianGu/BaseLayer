namespace OPT.PCOCCenter.Manager.Views
{
    partial class UserGroupForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.listBoxUserGroups = new DevExpress.XtraEditors.ListBoxControl();
            this.btnAddUserGroup = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteUserGroup = new DevExpress.XtraEditors.SimpleButton();
            this.vGridControl = new DevExpress.XtraVerticalGrid.VGridControl();
            this.row4 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.row5 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.row6 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxUserGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "组属性";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "组名称";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(495, 371);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(78, 29);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(596, 371);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 29);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // listBoxUserGroups
            // 
            this.listBoxUserGroups.Location = new System.Drawing.Point(17, 38);
            this.listBoxUserGroups.Name = "listBoxUserGroups";
            this.listBoxUserGroups.Size = new System.Drawing.Size(196, 316);
            this.listBoxUserGroups.TabIndex = 1;
            this.listBoxUserGroups.SelectedIndexChanged += new System.EventHandler(this.listBoxUserGroups_SelectedIndexChanged);
            // 
            // btnAddUserGroup
            // 
            this.btnAddUserGroup.Location = new System.Drawing.Point(159, 12);
            this.btnAddUserGroup.Name = "btnAddUserGroup";
            this.btnAddUserGroup.Size = new System.Drawing.Size(21, 23);
            this.btnAddUserGroup.TabIndex = 5;
            this.btnAddUserGroup.Text = "+";
            this.btnAddUserGroup.Click += new System.EventHandler(this.btnAddUserGroup_Click);
            // 
            // btnDeleteUserGroup
            // 
            this.btnDeleteUserGroup.Location = new System.Drawing.Point(186, 12);
            this.btnDeleteUserGroup.Name = "btnDeleteUserGroup";
            this.btnDeleteUserGroup.Size = new System.Drawing.Size(20, 23);
            this.btnDeleteUserGroup.TabIndex = 6;
            this.btnDeleteUserGroup.Text = "-";
            this.btnDeleteUserGroup.Click += new System.EventHandler(this.btnDeleteUserGroup_Click);
            // 
            // vGridControl
            // 
            this.vGridControl.Location = new System.Drawing.Point(219, 38);
            this.vGridControl.Name = "vGridControl";
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.row4,
            this.row5,
            this.row6});
            this.vGridControl.Size = new System.Drawing.Size(471, 316);
            this.vGridControl.TabIndex = 7;
            // 
            // row4
            // 
            this.row4.Name = "row4";
            // 
            // row5
            // 
            this.row5.Name = "row5";
            // 
            // row6
            // 
            this.row6.Name = "row6";
            // 
            // UserGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(714, 418);
            this.Controls.Add(this.vGridControl);
            this.Controls.Add(this.btnDeleteUserGroup);
            this.Controls.Add(this.btnAddUserGroup);
            this.Controls.Add(this.listBoxUserGroups);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UserGroupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户组管理";
            this.Load += new System.EventHandler(this.UserGroupForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxUserGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private DevExpress.XtraEditors.ListBoxControl listBoxUserGroups;
        private DevExpress.XtraEditors.SimpleButton btnAddUserGroup;
        private DevExpress.XtraEditors.SimpleButton btnDeleteUserGroup;
        private DevExpress.XtraVerticalGrid.VGridControl vGridControl;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow row4;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow row5;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow row6;
    }
}