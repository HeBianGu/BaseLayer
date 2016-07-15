namespace OPT.PEOfficeCenter.LicenseManager.Views
{
    partial class AskforLicenseView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTitle = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.editHostKey = new DevExpress.XtraEditors.TextEdit();
            this.btnLoadKeyFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoadModuleInfo = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.comboCustomer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboLicenseType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnGenLicenseFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddRow = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.edtExpiryDateAll = new DevExpress.XtraEditors.TextEdit();
            this.edtLicenseCountAll = new DevExpress.XtraEditors.TextEdit();
            this.comboAllLicenseType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnUpdateAllLicenseCount = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdateAllExpiryDate = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.editHostKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboLicenseType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtExpiryDateAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLicenseCountAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboAllLicenseType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(3, 3);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(84, 25);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "申请许可";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 112);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(84, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "客户机特征码：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(3, 161);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(108, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "模块许可配置信息：";
            // 
            // editHostKey
            // 
            this.editHostKey.Location = new System.Drawing.Point(107, 110);
            this.editHostKey.Name = "editHostKey";
            this.editHostKey.Size = new System.Drawing.Size(454, 20);
            this.editHostKey.TabIndex = 3;
            // 
            // btnLoadKeyFile
            // 
            this.btnLoadKeyFile.Location = new System.Drawing.Point(582, 109);
            this.btnLoadKeyFile.Name = "btnLoadKeyFile";
            this.btnLoadKeyFile.Size = new System.Drawing.Size(94, 23);
            this.btnLoadKeyFile.TabIndex = 7;
            this.btnLoadKeyFile.Text = "加载特征文件...";
            this.btnLoadKeyFile.Click += new System.EventHandler(this.btnLoadKeyFile_Click);
            // 
            // btnLoadModuleInfo
            // 
            this.btnLoadModuleInfo.Location = new System.Drawing.Point(582, 152);
            this.btnLoadModuleInfo.Name = "btnLoadModuleInfo";
            this.btnLoadModuleInfo.Size = new System.Drawing.Size(94, 23);
            this.btnLoadModuleInfo.TabIndex = 7;
            this.btnLoadModuleInfo.Text = "加载配置文件...";
            this.btnLoadModuleInfo.Click += new System.EventHandler(this.btnLoadModuleInfo_Click);
            // 
            // gridControl
            // 
            this.gridControl.Location = new System.Drawing.Point(3, 190);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(702, 291);
            this.gridControl.TabIndex = 8;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(3, 55);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "客户名称：";
            // 
            // comboCustomer
            // 
            this.comboCustomer.Location = new System.Drawing.Point(107, 52);
            this.comboCustomer.Name = "comboCustomer";
            this.comboCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboCustomer.Size = new System.Drawing.Size(336, 20);
            this.comboCustomer.TabIndex = 3;
            // 
            // comboLicenseType
            // 
            this.comboLicenseType.Location = new System.Drawing.Point(527, 52);
            this.comboLicenseType.Name = "comboLicenseType";
            this.comboLicenseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboLicenseType.Size = new System.Drawing.Size(149, 20);
            this.comboLicenseType.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(461, 55);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "许可类型：";
            // 
            // btnGenLicenseFile
            // 
            this.btnGenLicenseFile.Location = new System.Drawing.Point(713, 90);
            this.btnGenLicenseFile.Name = "btnGenLicenseFile";
            this.btnGenLicenseFile.Size = new System.Drawing.Size(118, 85);
            this.btnGenLicenseFile.TabIndex = 8;
            this.btnGenLicenseFile.Text = "生成许可文件...";
            this.btnGenLicenseFile.Click += new System.EventHandler(this.btnGenLicenseFile_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(117, 161);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(27, 23);
            this.btnAddRow.TabIndex = 7;
            this.btnAddRow.Text = "+";
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(150, 161);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(27, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "-";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // edtExpiryDateAll
            // 
            this.edtExpiryDateAll.Location = new System.Drawing.Point(359, 163);
            this.edtExpiryDateAll.Name = "edtExpiryDateAll";
            this.edtExpiryDateAll.Size = new System.Drawing.Size(96, 20);
            this.edtExpiryDateAll.TabIndex = 6;
            // 
            // edtLicenseCountAll
            // 
            this.edtLicenseCountAll.Location = new System.Drawing.Point(227, 162);
            this.edtLicenseCountAll.Name = "edtLicenseCountAll";
            this.edtLicenseCountAll.Size = new System.Drawing.Size(63, 20);
            this.edtLicenseCountAll.TabIndex = 6;
            // 
            // comboAllLicenseType
            // 
            this.comboAllLicenseType.Location = new System.Drawing.Point(494, 162);
            this.comboAllLicenseType.Name = "comboAllLicenseType";
            this.comboAllLicenseType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboAllLicenseType.Size = new System.Drawing.Size(67, 20);
            this.comboAllLicenseType.TabIndex = 3;
            this.comboAllLicenseType.SelectedIndexChanged += new System.EventHandler(this.comboAllLicenseType_SelectedIndexChanged);
            // 
            // btnUpdateAllLicenseCount
            // 
            this.btnUpdateAllLicenseCount.Location = new System.Drawing.Point(296, 161);
            this.btnUpdateAllLicenseCount.Name = "btnUpdateAllLicenseCount";
            this.btnUpdateAllLicenseCount.Size = new System.Drawing.Size(24, 23);
            this.btnUpdateAllLicenseCount.TabIndex = 7;
            this.btnUpdateAllLicenseCount.Text = "↓";
            this.btnUpdateAllLicenseCount.Click += new System.EventHandler(this.btnUpdateAllLicenseCount_Click);
            // 
            // btnUpdateAllExpiryDate
            // 
            this.btnUpdateAllExpiryDate.Location = new System.Drawing.Point(461, 161);
            this.btnUpdateAllExpiryDate.Name = "btnUpdateAllExpiryDate";
            this.btnUpdateAllExpiryDate.Size = new System.Drawing.Size(27, 23);
            this.btnUpdateAllExpiryDate.TabIndex = 7;
            this.btnUpdateAllExpiryDate.Text = "↓";
            this.btnUpdateAllExpiryDate.Click += new System.EventHandler(this.btnUpdateAllExpiryDate_Click);
            // 
            // AskforLicenseView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdateAllExpiryDate);
            this.Controls.Add(this.btnUpdateAllLicenseCount);
            this.Controls.Add(this.btnAddRow);
            this.Controls.Add(this.btnLoadModuleInfo);
            this.Controls.Add(this.btnGenLicenseFile);
            this.Controls.Add(this.btnLoadKeyFile);
            this.Controls.Add(this.edtLicenseCountAll);
            this.Controls.Add(this.edtExpiryDateAll);
            this.Controls.Add(this.editHostKey);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.comboAllLicenseType);
            this.Controls.Add(this.comboLicenseType);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.comboCustomer);
            this.Name = "AskforLicenseView";
            this.Size = new System.Drawing.Size(916, 559);
            this.Load += new System.EventHandler(this.AskforLicenseView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.editHostKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboLicenseType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtExpiryDateAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLicenseCountAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboAllLicenseType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelTitle;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit editHostKey;
        private DevExpress.XtraEditors.SimpleButton btnLoadKeyFile;
        private DevExpress.XtraEditors.SimpleButton btnLoadModuleInfo;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit comboCustomer;
        private DevExpress.XtraEditors.ComboBoxEdit comboLicenseType;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnGenLicenseFile;
        private DevExpress.XtraEditors.SimpleButton btnAddRow;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.TextEdit edtExpiryDateAll;
        private DevExpress.XtraEditors.TextEdit edtLicenseCountAll;
        private DevExpress.XtraEditors.ComboBoxEdit comboAllLicenseType;
        private DevExpress.XtraEditors.SimpleButton btnUpdateAllLicenseCount;
        private DevExpress.XtraEditors.SimpleButton btnUpdateAllExpiryDate;
    }
}
