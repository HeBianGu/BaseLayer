using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MySchoolForeGround
{
    public partial class FrmLoding : Form
    {
        public FrmLoding()
        {
            InitializeComponent();
            pgbLoding.Minimum = 0;
            pgbLoding.Maximum = 100;
            pgbLoding.Step = 20;
            tmrLoding.Enabled = true;
            tmrLoding.Interval = 1000;
            this.pgbLoding.Value = 0;
        }
        FrmLogin frmLogin = new FrmLogin();
      

        private void FrmLoding_Load(object sender, EventArgs e)
        {
            skinLoding.SkinFile = "SportsBlack.ssk";
            this.pgbLoding.PerformStep();
            double percent = 100 * (this.pgbLoding.Value - this.pgbLoding.Minimum) / (this.pgbLoding.Maximum - this.pgbLoding.Minimum);
            this.lblLoding.Text = percent.ToString() + "%";
            if (pgbLoding.Value >= this.pgbLoding.Maximum)
            {
                this.tmrLoding.Stop();
                this.Hide();
                frmLogin.ShowDialog();
            }
        }
    }
}