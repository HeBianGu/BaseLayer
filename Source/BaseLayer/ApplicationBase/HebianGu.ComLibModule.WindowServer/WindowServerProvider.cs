using HebianGu.ObjectBase.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.WindowServer
{

    /// <summary> window服务常用操作 </summary>
    partial class WindowServerProvider
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static WindowServerProvider t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static WindowServerProvider Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new WindowServerProvider();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private WindowServerProvider()
        {

        }
        #endregion - 单例模式 End -

    }

    partial class WindowServerProvider
    {

        /// <summary> 服务是否安装 </summary>
        public bool IsInstalledOfName(string serviceName)
        {
            try
            {
                ServiceController[] services = ServiceController.GetServices();


                foreach (ServiceController service in services)
                {
                    if (service.ServiceName == serviceName)
                    {
                        return true;
                    }
                }


                return false;
            }
            catch
            { return false; }
        }

        /// <summary> 启动服务 </summary>
        public bool StartServiceOfName(string serviceName)
        {
            try
            {
                ServiceController[] services = ServiceController.GetServices();


                foreach (ServiceController service in services)
                {
                    if (service.ServiceName == serviceName)
                    {
                        service.Start();


                        service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));

                        return true;
                    }
                }

                return false;
            }
            catch(Exception ex)
            {
                LogProviderHandler.Instance.OnErrLog(ex);

                return false;
            }
        }


        /// <summary> 停止服务 </summary>
        public void StopServiceOfName(string serviceName)
        {
            try
            {
                ServiceController[] services = ServiceController.GetServices();


                foreach (ServiceController service in services)
                {
                    if (service.ServiceName == serviceName)
                    {
                        service.Stop();


                        service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                    }
                }
            }
            catch { }
        }


        /// <summary> 服务是否启动 </summary>
        public bool IsStartOfName(string serviceName)
        {
            bool result = true;


            try
            {
                ServiceController[] services = ServiceController.GetServices();


                foreach (ServiceController service in services)
                {
                    if (service.ServiceName == serviceName)
                    {
                        if ((service.Status == ServiceControllerStatus.Stopped)
                            || (service.Status == ServiceControllerStatus.StopPending))
                        {
                            result = false;
                        }
                    }
                }
            }
            catch { }


            return result;
        }


    }
}
