namespace HebianGu.ComLibModule.ThreadEx
{
    partial class ProgBackWorkForm
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bt_Cancel = new System.Windows.Forms.Button();
            this.lb_ditial = new System.Windows.Forms.Label();
            this.lb_percent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 43);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(530, 31);
            this.progressBar1.TabIndex = 0;
            // 
            // bt_Cancel
            // 
            this.bt_Cancel.Location = new System.Drawing.Point(447, 87);
            this.bt_Cancel.Name = "bt_Cancel";
            this.bt_Cancel.Size = new System.Drawing.Size(75, 23);
            this.bt_Cancel.TabIndex = 1;
            this.bt_Cancel.Text = "Cancel";
            this.bt_Cancel.UseVisualStyleBackColor = true;
            this.bt_Cancel.Click += new System.EventHandler(this.bt_Cancel_Click);
            // 
            // lb_ditial
            // 
            this.lb_ditial.AutoSize = true;
            this.lb_ditial.Location = new System.Drawing.Point(22, 19);
            this.lb_ditial.Name = "lb_ditial";
            this.lb_ditial.Size = new System.Drawing.Size(53, 12);
            this.lb_ditial.TabIndex = 2;
            this.lb_ditial.Text = "处理进度";
            // 
            // lb_percent
            // 
            this.lb_percent.AutoSize = true;
            this.lb_percent.Location = new System.Drawing.Point(238, 92);
            this.lb_percent.Name = "lb_percent";
            this.lb_percent.Size = new System.Drawing.Size(53, 12);
            this.lb_percent.TabIndex = 2;
            this.lb_percent.Text = "处理进度";
            // 
            // ProgBackWorkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 125);
            this.Controls.Add(this.lb_percent);
            this.Controls.Add(this.lb_ditial);
            this.Controls.Add(this.bt_Cancel);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ProgBackWorkForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "处理进度...";
            this.Shown += new System.EventHandler(this.ProgBackWorkForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button bt_Cancel;
        private System.Windows.Forms.Label lb_ditial;
        private System.Windows.Forms.Label lb_percent;
    }
}