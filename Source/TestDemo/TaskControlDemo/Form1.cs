using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskControlDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Load+=Form1_Load;
        }

        private void Form1_Load(object sender,EventArgs e)
        {
            for (int i = 1; i < 50; i++)
            {
                

                if(i%4==0)
                {
                    TaskControlModel model = new TaskControlModel();
                    model.SetState("空闲");
                    model.BackColor = Color.Yellow;
                    this.flowLayoutPanel1.Controls.Add(model);
                }

                else if (i % 5 == 0)
                {
                    TaskControlModel model = new TaskControlModel();
                    model.SetState("报警");
                    model.BackColor = Color.Red;
                    this.flowLayoutPanel1.Controls.Add(model);
                }

                else if (i % 3 == 0)
                {
                    TaskControlModel model = new TaskControlModel();
                    model.SetState("关机");
                    model.BackColor = Color.LightGray;
                    this.flowLayoutPanel1.Controls.Add(model);
                }

                else if (i % 7 == 0)
                {
                    TaskControlModel model = new TaskControlModel();
                    model.SetState("开机");
                    model.BackColor = Color.White;
                    this.flowLayoutPanel1.Controls.Add(model);
                }
                else
                {
                    TaskControlModel model = new TaskControlModel();
                    model.SetState("运行");
                    model.BackColor = Color.Green;
                    this.flowLayoutPanel1.Controls.Add(model);
                }
                
            }
        }
    }
}
