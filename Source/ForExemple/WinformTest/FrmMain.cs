using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinformTest
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        //普通异常测试
        private void btnTest1_Click(object sender, EventArgs e)
        {
            throw new Exception("啊..我这行代码异常了...");
        }

        //多线程异常测试
        private void btnTest2_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(() => { throw new Exception("啊哦，异常错误。"); });
            th.IsBackground = true;
            th.Start();
        }
    }
}
