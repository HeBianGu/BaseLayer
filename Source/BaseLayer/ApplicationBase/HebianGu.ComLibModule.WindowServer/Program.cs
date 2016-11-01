using HebianGu.ObjectBase.Logger;
using System;

namespace HebianGu.ComLibModule.WindowServer
{
    class Program
    {
        static void Main(string[] args)
        {


            // Todo ：注册日志 

            LogProviderHandler.Instance.RunLog += l => LogFactory.Instance.GetLogService<Log4netEngine>().RunLog(l);
            LogProviderHandler.Instance.ErrLog += l => LogFactory.Instance.GetLogService<Log4netEngine>().ErrorLog(l);
            LogProviderHandler.Instance.ErrExLog += l => LogFactory.Instance.GetLogService<Log4netEngine>().ErrorLog(l);

            //ServiceController[] services = ServiceController.GetServices();

            //services.ToList().ForEach(l=>Console.WriteLine(l.ServiceName));

            //Console.Read();

            string sName="BaiduYunUtility";

            //if(WindowServerProvider.Instance.IsInstalledOfName(sName))
            //{
            //    Console.WriteLine(sName+"已经安装！");
            //}
            //else
            //{
            //    Console.WriteLine(sName + "未安装！");
            //}

           if(WindowServerProvider.Instance.StartServiceOfName(sName))
           {
               Console.WriteLine(sName+"启动成功！");
           }
           else
           {
               Console.WriteLine(sName + "启动失败！");
           }

            Console.Read();
        }


    }
}
