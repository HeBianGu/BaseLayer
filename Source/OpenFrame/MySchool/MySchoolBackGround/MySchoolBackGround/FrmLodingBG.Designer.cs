namespace MySchoolBackGround
{
    partial class FrmLodingBG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLodingBG));
            this.pgbLodingBG = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLodingBG = new System.Windows.Forms.Label();
            this.skinLodingBG = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.tmrLodingBG = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // pgbLodingBG
            // 
            this.pgbLodingBG.Location = new System.Drawing.Point(36, 32);
            this.pgbLodingBG.Name = "pgbLodingBG";
            this.pgbLodingBG.Size = new System.Drawing.Size(300, 20);
            this.pgbLodingBG.TabIndex = 0;
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
            // lblLodingBG
            // 
            this.lblLodingBG.AutoSize = true;
            this.lblLodingBG.Location = new System.Drawing.Point(99, 66);
            this.lblLodingBG.Name = "lblLodingBG";
            this.lblLodingBG.Size = new System.Drawing.Size(0, 12);
            this.lblLodingBG.TabIndex = 1;
            // 
            // skinLodingBG
            // 
            this.skinLodingBG.SerialNumber = "";
            this.skinLodingBG.SkinFile = null;
            // 
            // tmrLodingBG
            // 
            this.tmrLodingBG.Enabled = true;
            this.tmrLodingBG.Interval = 1000;
            this.tmrLodingBG.Tick += new System.EventHandler(this.FrmLodingBG_Load);
            // 
            // FrmLodingBG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 104);
            this.Controls.Add(this.lblLodingBG);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pgbLodingBG);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmLodingBG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loding...";
            this.Load += new System.EventHandler(this.FrmLodingBG_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgbLodingBG;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLodingBG;
        private Sunisoft.IrisSkin.SkinEngine skinLodingBG;
        private System.Windows.Forms.Timer tmrLodingBG;
    }
}