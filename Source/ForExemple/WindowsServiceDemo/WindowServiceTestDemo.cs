using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceDemo
{
    public partial class WindowServiceDemo : ServiceBase
    {
        public WindowServiceDemo()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Trace.Write("服务已经启动！");
        }

        protected override void OnStop()
        {
            Trace.Write("服务已经关闭！");
        }
    }
}
