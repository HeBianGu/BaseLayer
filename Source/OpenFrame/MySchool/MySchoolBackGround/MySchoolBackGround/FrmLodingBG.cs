using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace MySchoolBackGround
{
    public partial class FrmLodingBG : Form
    {
        public FrmLodingBG()
        {
            InitializeComponent();
            pgbLodingBG.Minimum = 0;
            pgbLodingBG.Maximum = 100;
            pgbLodingBG.Step = 20;
            tmrLodingBG.Enabled = true;
            tmrLodingBG.Interval = 1000;
            this.pgbLodingBG.Value = 0;
        }
        FrmLoginBG frmLoginBG = new FrmLoginBG();
       

        private void FrmLodingBG_Load(object sender, EventArgs e)
        {
            skinLodingBG.SkinFile = "SportsBlack.ssk";
            this.pgbLodingBG.PerformStep();
            double percent = 100 * (this.pgbLodingBG.Value - this.pgbLodingBG.Minimum) / (this.pgbLodingBG.Maximum - this.pgbLodingBG.Minimum);
            this.lblLodingBG.Text = percent.ToString() + "%";
            if (pgbLodingBG.Value >= this.pgbLodingBG.Maximum)
            {
                this.tmrLodingBG.Stop();
                this.Hide();
               
                frmLoginBG.ShowDialog();
            }
        }

       
    }
}