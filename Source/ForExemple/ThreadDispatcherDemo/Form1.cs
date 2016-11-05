using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadDispatcherDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // Todo ：直接在异步中修改，会报错 
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Thread thread = new Thread(l =>
                {
                    while (true)
                    {
                        Thread.Sleep(1000);

                        // Todo ：直接修改主线程值 
                        this.textBox1.Text = DateTime.Now.ToString();
                    }

                });

                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // Todo ：异步用主线程修改 
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Thread thread = new Thread(l =>
                {
                    while (true)
                    {
                        Thread.Sleep(1000);

                        Action act = () =>
                           {
                               // Todo ：直接修改主线程值 
                               this.textBox1.Text = DateTime.Now.ToString();

                               //----WPF---added by wonsoft.cn---  
                               //this.Dispatcher.Invoke(d, i);  
                           };

                        this.Invoke(act);


                    }

                });

                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
