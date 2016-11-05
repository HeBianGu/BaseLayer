namespace CodeImage
{
    partial class FrmExample
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
            this.picD2 = new System.Windows.Forms.PictureBox();
            this.picD1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtD2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtD1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picD2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picD1)).BeginInit();
            this.SuspendLayout();
            // 
            // picD2
            // 
            this.picD2.Location = new System.Drawing.Point(12, 12);
            this.picD2.Name = "picD2";
            this.picD2.Size = new System.Drawing.Size(297, 250);
            this.picD2.TabIndex = 0;
            this.picD2.TabStop = false;
            // 
            // picD1
            // 
            this.picD1.Location = new System.Drawing.Point(348, 12);
            this.picD1.Name = "picD1";
            this.picD1.Size = new System.Drawing.Size(283, 250);
            this.picD1.TabIndex = 0;
            this.picD1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 285);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "输入字符串";
            // 
            // txtD2
            // 
            this.txtD2.Location = new System.Drawing.Point(116, 278);
            this.txtD2.Name = "txtD2";
            this.txtD2.Size = new System.Drawing.Size(100, 25);
            this.txtD2.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(222, 280);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "生成二维码";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnCreateD1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(348, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "输入字符串";
            // 
            // txtD1
            // 
            this.txtD1.Location = new System.Drawing.Point(436, 281);
            this.txtD1.Name = "txtD1";
            this.txtD1.Size = new System.Drawing.Size(100, 25);
            this.txtD1.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(542, 283);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "生成一维码";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnCreateD2_Click);
            // 
            // FrmExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 318);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtD2);
            this.Controls.Add(this.txtD1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.picD1);
            this.Controls.Add(this.picD2);
            this.Name = "FrmExample";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picD2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picD1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picD2;
        private System.Windows.Forms.PictureBox picD1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtD2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtD1;
        private System.Windows.Forms.Button button3;
    }
}

