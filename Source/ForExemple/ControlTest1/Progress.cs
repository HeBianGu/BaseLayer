using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlTest1
{
    public partial class Progress : Form
    {
        public Progress()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.splashScreenManager1.ShowWaitForm();
            System.Threading.Thread.Sleep(3000);
            this.splashScreenManager1.CloseWaitForm();
        }
    }
}
