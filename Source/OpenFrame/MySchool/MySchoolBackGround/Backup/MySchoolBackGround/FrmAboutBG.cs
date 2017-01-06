using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MySchoolBackGround
{
    public partial class FrmAboutBG : Form
    {
        public FrmAboutBG()
        {
            InitializeComponent();
        }

        private void btnAboutYes_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAboutBG_Load(object sender, EventArgs e)
        {
            skinAboutBG.SkinFile = "SportsBlack.ssk";
        }
    }
}