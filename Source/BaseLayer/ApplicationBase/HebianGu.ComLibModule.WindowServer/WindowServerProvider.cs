using HebianGu.ObjectBase.Logger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.WindowServer
{
    /// <summary> window服务常用操作 </summary>
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

        /// <summary> 安装服务 </summary>
        public void InstallService(string filePath, string[] options = null)
        {
            try
            {
                Install(false, filePath, options);

                LogProviderHandler.Instance.OnRunLog(MethodInfo.GetCurrentMethod().Name.ToTatol("Success"));
            }
            catch (Exception ex)
            {
                LogProviderHandler.Instance.OnErrLog(ex.ToExc());
            }
        }

        static void Install(bool uninstall, string filePath, string[] args)
        {
            using (AssemblyInstaller inst = new AssemblyInstaller(filePath, args))
            {
                IDictionary state = new Hashtable();

                inst.UseNewContext = true;
                try
                {
                    if (uninstall)
                    {
                        inst.Uninstall(state);
                    }
                    else
                    {
                        inst.Install(state);
                        inst.Commit(state);
                    }
                }
                catch
                {
                    try
                    {
                        inst.Rollback(state);
                    }
                    catch
                    {

                    }
                    throw;
                }
            }
        }

        /// <summary> 卸载服务 </summary>
        public void UnInstallService(string filePath, string[] options = null)
        {
            try
            {
                Install(true, filePath, options);

                LogProviderHandler.Instance.OnRunLog(MethodInfo.GetCurrentMethod().Name.ToTatol("Success"));

            }
            catch (Exception ex)
            {
                LogProviderHandler.Instance.OnErrLog(ex.ToExc());
            }

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

                        LogProviderHandler.Instance.OnRunLog(MethodInfo.GetCurrentMethod().Name.ToTatol("Success"));

                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                LogProviderHandler.Instance.OnErrLog(ex.ToExc());

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

                        LogProviderHandler.Instance.OnRunLog(MethodInfo.GetCurrentMethod().Name.ToTatol("Success"));

                    }
                }
            }
            catch (Exception ex)
            {
                LogProviderHandler.Instance.OnErrLog(ex.ToExc());

            }
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
            catch (Exception ex)
            {
                LogProviderHandler.Instance.OnErrLog(ex.ToExc());
            }


            return result;
        }

    }


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
}
