namespace DemoAssembly
{
    partial class Form1
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_code = new System.Windows.Forms.RichTextBox();
            this.btn_sumit = new System.Windows.Forms.Button();
            this.txt_result = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.btn_sumit);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txt_result);
            this.splitContainer1.Size = new System.Drawing.Size(956, 569);
            this.splitContainer1.SplitterDistance = 357;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_code);
            this.panel1.Location = new System.Drawing.Point(12, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(941, 300);
            this.panel1.TabIndex = 1;
            // 
            // txt_code
            // 
            this.txt_code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_code.Location = new System.Drawing.Point(0, 0);
            this.txt_code.Name = "txt_code";
            this.txt_code.Size = new System.Drawing.Size(941, 300);
            this.txt_code.TabIndex = 0;
            this.txt_code.Text = "";
            // 
            // btn_sumit
            // 
            this.btn_sumit.Location = new System.Drawing.Point(12, 318);
            this.btn_sumit.Name = "btn_sumit";
            this.btn_sumit.Size = new System.Drawing.Size(86, 35);
            this.btn_sumit.TabIndex = 0;
            this.btn_sumit.Text = "运行";
            this.btn_sumit.UseVisualStyleBackColor = true;
            this.btn_sumit.Click += new System.EventHandler(this.btn_sumit_Click);
            // 
            // txt_result
            // 
            this.txt_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_result.Location = new System.Drawing.Point(0, 0);
            this.txt_result.Name = "txt_result";
            this.txt_result.Size = new System.Drawing.Size(956, 208);
            this.txt_result.TabIndex = 0;
            this.txt_result.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 569);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox txt_code;
        private System.Windows.Forms.Button btn_sumit;
        private System.Windows.Forms.RichTextBox txt_result;
    }
}

