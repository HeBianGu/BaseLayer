using HebianGu.ObjectBase.Logger;
using System;

namespace HebianGu.ComLibModule.WindowServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Todo ：注册日志 
            LogProviderHandler.Instance.RunLog += (l,k) => LogFactory.Instance.GetLogService<Log4netEngine>().RunLog(l);
            LogProviderHandler.Instance.ErrLog += (l, k) => LogFactory.Instance.GetLogService<Log4netEngine>().ErrorLog(l);
            LogProviderHandler.Instance.ErrExLog += (l, k) => LogFactory.Instance.GetLogService<Log4netEngine>().ErrorLog(l);

            while (true)
            {
                ConsoleKeyInfo ss = Console.ReadKey();

                
                // Todo ：安装服务 
                if (ss.KeyChar == 'i')
                {
                    InstallDemo();

                }
                // Todo ：卸载服务 
                else if (ss.KeyChar == 'u')
                {
                    UninstallDemo();

                }
                // Todo ：启动服务 
                else if (ss.KeyChar == 's')
                {
                    StartDemo();

                }
                // Todo ：停止服务 
                else if (ss.KeyChar == 'e')
                {
                    StopDemo();
                }
                else
                {
                    break;
                }
            }

            Console.Read();
        }

        static string ss = @"D:\WorkArea\DevTest\BaseLayer\Source\ForExemple\WindowsServiceDemo\bin\Debug\WindowsServiceDemo.exe";

        static string nn = "WindowServiceTestDemo";
        static void InstallDemo()
        {
            WindowServerProvider.Instance.InstallService(ss);
        }
        static void StartDemo()
        {
            WindowServerProvider.Instance.StartServiceOfName(nn);
        }
        static void StopDemo()
        {
            WindowServerProvider.Instance.StopServiceOfName(nn);
        }
        static void UninstallDemo()
        {
            WindowServerProvider.Instance.UnInstallService(ss);
        }

    }
}
