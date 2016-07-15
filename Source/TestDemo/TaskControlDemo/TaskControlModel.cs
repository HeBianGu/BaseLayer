using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskControlDemo
{
    public partial class TaskControlModel : UserControl
    {
        public TaskControlModel()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("没有详细信息！");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        public void SetState(string  str)
        {
            this.label4.Text = str;
        }
    }
}
