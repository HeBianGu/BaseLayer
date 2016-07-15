using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplashScreenManagerDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.splashScreenManager1.ShowWaitForm();
            this.splashScreenManager1.SetWaitFormCaption("正在初始化数据...");
            this.splashScreenManager1.SetWaitFormDescription("25%");
            System.Threading.Thread.Sleep(3000);
            this.splashScreenManager1.SetWaitFormCaption("正在初始化地质模型...");
            this.splashScreenManager1.SetWaitFormDescription("50%");
            System.Threading.Thread.Sleep(3000);
            this.splashScreenManager1.SetWaitFormCaption("正在初始化生产模型...");
            this.splashScreenManager1.SetWaitFormDescription("75%");
            System.Threading.Thread.Sleep(3000);
            this.splashScreenManager1.SetWaitFormCaption("正在初始化岩石模型...");
            this.splashScreenManager1.SetWaitFormDescription("100%");
            System.Threading.Thread.Sleep(3000);
            this.splashScreenManager1.CloseWaitForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //SplashScreen1 ss = new SplashScreen1();
            //SplashScreenManager.ShowDefaultProgressSplashScreen("加载");
            SplashScreenManager.ShowForm(typeof(SplashScreen1));
            //SplashScreenManager.ShowDefaultSplashScreen();
            System.Threading.Thread.Sleep(5000);


            SplashScreenManager.CloseDefaultSplashScreen();
        }
    }
}
