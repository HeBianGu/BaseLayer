namespace OPT.PCOCCenter.Manager.Views
{
    partial class LogoffLicenseForm
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.btnSaveLogoff = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(12, 12);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(514, 227);
            this.textBox.TabIndex = 0;
            // 
            // btnSaveLogoff
            // 
            this.btnSaveLogoff.Location = new System.Drawing.Point(321, 266);
            this.btnSaveLogoff.Name = "btnSaveLogoff";
            this.btnSaveLogoff.Size = new System.Drawing.Size(91, 31);
            this.btnSaveLogoff.TabIndex = 1;
            this.btnSaveLogoff.Text = Utils.Utils.Translate("保存");
            this.btnSaveLogoff.UseVisualStyleBackColor = true;
            this.btnSaveLogoff.Click += new System.EventHandler(this.btnSaveLogoff_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(429, 266);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(91, 31);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = Utils.Utils.Translate("关闭");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // LogoffLicenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(538, 325);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSaveLogoff);
            this.Controls.Add(this.textBox);
            this.Name = "LogoffLicenseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = Utils.Utils.Translate("注销信息");
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button btnSaveLogoff;
        private System.Windows.Forms.Button btnClose;
    }
}