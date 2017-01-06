using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MySchoolForeGround
{
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();
        }

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            skinAbout.SkinFile = "SportsBlack.ssk";
        }

        private void btnAboutYes_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}