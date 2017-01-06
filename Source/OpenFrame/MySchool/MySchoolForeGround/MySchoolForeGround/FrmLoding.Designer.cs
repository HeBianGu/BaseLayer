namespace MySchoolForeGround
{
    partial class FrmLoding
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLoding));
            this.skinLoding = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.tmrLoding = new System.Windows.Forms.Timer(this.components);
            this.pgbLoding = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLoding = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // skinLoding
            // 
            this.skinLoding.SerialNumber = "";
            this.skinLoding.SkinFile = null;
            // 
            // tmrLoding
            // 
            this.tmrLoding.Tick += new System.EventHandler(this.FrmLoding_Load);
            // 
            // pgbLoding
            // 
            this.pgbLoding.Location = new System.Drawing.Point(36, 32);
            this.pgbLoding.Name = "pgbLoding";
            this.pgbLoding.Size = new System.Drawing.Size(300, 20);
            this.pgbLoding.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "正在启动:";
            // 
            // lblLoding
            // 
            this.lblLoding.AutoSize = true;
            this.lblLoding.Location = new System.Drawing.Point(99, 66);
            this.lblLoding.Name = "lblLoding";
            this.lblLoding.Size = new System.Drawing.Size(0, 12);
            this.lblLoding.TabIndex = 1;
            // 
            // FrmLoding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 106);
            this.Controls.Add(this.lblLoding);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pgbLoding);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmLoding";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loding...";
            this.Load += new System.EventHandler(this.FrmLoding_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunisoft.IrisSkin.SkinEngine skinLoding;
        private System.Windows.Forms.Timer tmrLoding;
        private System.Windows.Forms.ProgressBar pgbLoding;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLoding;
    }
}

