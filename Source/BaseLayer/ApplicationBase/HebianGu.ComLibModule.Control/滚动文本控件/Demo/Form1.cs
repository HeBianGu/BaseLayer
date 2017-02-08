using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace HebianGu.ComLibModule.ControlHelper
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RadioButton chkRToL;
		private System.Windows.Forms.RadioButton chkBounce;
		private System.Windows.Forms.RadioButton chkLToR;
		private System.Windows.Forms.TextBox txtScroll;
		private ScrollingText scrollingText1;		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.chkRToL = new System.Windows.Forms.RadioButton();
            this.chkBounce = new System.Windows.Forms.RadioButton();
            this.chkLToR = new System.Windows.Forms.RadioButton();
            this.txtScroll = new System.Windows.Forms.TextBox();
            this.scrollingText1 = new ScrollingText();
            this.SuspendLayout();
            // 
            // chkRToL
            // 
            this.chkRToL.Location = new System.Drawing.Point(307, 77);
            this.chkRToL.Name = "chkRToL";
            this.chkRToL.Size = new System.Drawing.Size(166, 34);
            this.chkRToL.TabIndex = 1;
            this.chkRToL.Text = "从右向左";
            this.chkRToL.CheckedChanged += new System.EventHandler(this.chkRToL_CheckedChanged);
            // 
            // chkBounce
            // 
            this.chkBounce.Location = new System.Drawing.Point(448, 77);
            this.chkBounce.Name = "chkBounce";
            this.chkBounce.Size = new System.Drawing.Size(167, 34);
            this.chkBounce.TabIndex = 2;
            this.chkBounce.Text = "相反";
            this.chkBounce.CheckedChanged += new System.EventHandler(this.chkBounce_CheckedChanged);
            // 
            // chkLToR
            // 
            this.chkLToR.Location = new System.Drawing.Point(563, 77);
            this.chkLToR.Name = "chkLToR";
            this.chkLToR.Size = new System.Drawing.Size(166, 34);
            this.chkLToR.TabIndex = 3;
            this.chkLToR.Text = "从左向右";
            this.chkLToR.CheckedChanged += new System.EventHandler(this.chkLToR_CheckedChanged);
            // 
            // txtScroll
            // 
            this.txtScroll.Location = new System.Drawing.Point(0, 77);
            this.txtScroll.Name = "txtScroll";
            this.txtScroll.Size = new System.Drawing.Size(295, 25);
            this.txtScroll.TabIndex = 4;
            this.txtScroll.Text = "天空飘过五个字";
            this.txtScroll.TextChanged += new System.EventHandler(this.txtScroll_TextChanged);
            // 
            // scrollingText1
            // 
            this.scrollingText1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollingText1.BackgroundBrush = null;
            this.scrollingText1.BorderColor = System.Drawing.Color.Black;
            this.scrollingText1.Cursor = System.Windows.Forms.Cursors.Default;
            this.scrollingText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scrollingText1.ForegroundBrush = null;
            this.scrollingText1.Location = new System.Drawing.Point(13, 12);
            this.scrollingText1.Name = "scrollingText1";
            this.scrollingText1.ScrollDirection = ScrollDirection.Bouncing;
            this.scrollingText1.ScrollText = "天空飘过五个字";
            this.scrollingText1.ShowBorder = true;
            this.scrollingText1.Size = new System.Drawing.Size(495, 55);
            this.scrollingText1.StopScrollOnMouseOver = true;
            this.scrollingText1.TabIndex = 5;
            this.scrollingText1.Text = "scrollingText1";
            this.scrollingText1.TextScrollDistance = 2;
            this.scrollingText1.TextScrollSpeed = 25;
            this.scrollingText1.VerticleTextPosition = VerticleTextPosition.Center;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(524, 181);
            this.Controls.Add(this.scrollingText1);
            this.Controls.Add(this.txtScroll);
            this.Controls.Add(this.chkLToR);
            this.Controls.Add(this.chkBounce);
            this.Controls.Add(this.chkRToL);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void Form1_Load(object sender, System.EventArgs e)
		{
			scrollingText1.BackgroundBrush = 
				new LinearGradientBrush(this.scrollingText1.ClientRectangle, 
				Color.Red, Color.Blue, 
				LinearGradientMode.Horizontal);

//			scrollingText1.ForegroundBrush = 
//				new LinearGradientBrush(this.scrollingText1.ClientRectangle, 
//				Color.Red, Color.Blue, 
//				LinearGradientMode.Horizontal);
			
			scrollingText1.ForeColor = Color.Yellow;			
		
			scrollingText1.ScrollText = txtScroll.Text;

            chkRToL.Checked = true;
		}		

		private void scrollingText1_TextClicked(object sender, System.EventArgs args)
		{
			MessageBox.Show("BOOM");
		}

		private void chkRToL_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkRToL.Checked) scrollingText1.ScrollDirection = ScrollDirection.RightToLeft;
		}

		private void chkBounce_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkBounce.Checked) scrollingText1.ScrollDirection = ScrollDirection.Bouncing;
		}

		private void chkLToR_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkLToR.Checked) scrollingText1.ScrollDirection = ScrollDirection.LeftToRight;
		}

		private void txtScroll_TextChanged(object sender, System.EventArgs e)
		{
			scrollingText1.ScrollText = txtScroll.Text;
		}	
	}
}