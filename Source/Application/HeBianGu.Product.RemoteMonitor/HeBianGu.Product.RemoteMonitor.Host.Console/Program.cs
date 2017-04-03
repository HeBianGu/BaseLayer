using HebianGu.ComLibModule.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.RemoteMonitor.Host.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            LogProvider.Instance.RunLog("准备启动服务！");

            LogProvider.Instance.RunLog(WcfConfiger.Instance.IP);

            LogProvider.Instance.RunLog(WcfConfiger.Instance.Port);

            try
            {
                Dictionary<Type, Type> dic = WcfServiceFactory.Instance.BuildRemoveMonitorService();

               

                 //dic = WcfServiceFactory.Instance.BuildWorkScreamService();

                foreach (var d in dic)
                {
                    LogProvider.Instance.RunLog(d.Key.Name);
                }

                WcfProviderEngine.Instance.AddService(dic);

                LogProvider.Instance.RunLog("启动成功，按任意键退出！");

            }
            catch (Exception ex)
            {
                LogProvider.Instance.ErrLog(ex);
            }
            LogProvider.Instance.RunLog("按任意键退出！");
            Console.Read();
        }
    }
}
