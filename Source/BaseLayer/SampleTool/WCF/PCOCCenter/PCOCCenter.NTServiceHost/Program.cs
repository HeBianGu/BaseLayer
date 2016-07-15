﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration.Install;

namespace OPT.PCOCCenter.NTServiceHost
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //// 运行服务 
            //if (args.Length == 0)
            //{
            //    ServiceBase[] ServicesToRun;
            //    ServicesToRun = new ServiceBase[] 
            //    { 
            //        new NTService() 
            //    };
            //    ServiceBase.Run(ServicesToRun);
            //}
            //// 安装服务
            //else if (args[0].ToLower() == "/i" || args[0].ToLower() == "-i") 
            //{ 
            //try
            //{
            //    string[] cmdline = { };
            //    string serviceFileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //    TransactedInstaller transactedInstaller = new TransactedInstaller();
            //    AssemblyInstaller assemblyInstaller = new AssemblyInstaller(serviceFileName, cmdline);
            //    transactedInstaller.Installers.Add(assemblyInstaller);
            //    transactedInstaller.Install(new System.Collections.Hashtable());

            //}
            //catch (Exception ex)
            //{
            //    string msg = ex.Message;
            //}
            //}
            //// 删除服务 
            //else if (args[0].ToLower() == "/u" || args[0].ToLower() == "-u")
            //{
            try
            {
                string[] cmdline = { };
                string serviceFileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
                TransactedInstaller transactedInstaller = new TransactedInstaller();
                AssemblyInstaller assemblyInstaller = new AssemblyInstaller(serviceFileName, cmdline);
                transactedInstaller.Installers.Add(assemblyInstaller);
                transactedInstaller.Uninstall(null);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            //} 
        }
    }
}
