using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OPT.PCOCCenter.Service.Interface;

namespace OPT.PCOCCenter.Client
{
    public class SimulationHost
    {

        // 注册模拟器主机
        public static int RegSimulationHost(string hostName, string hostIP, string simulationType, string licensePath, string simulationPath, string hostKey)
        {
            int nRet = 0;

            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(Client.customBinding, Client.RemoteAddress))
                {
                    Client.SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();

                    string sRet = proxy.RegSimulationHost(Client.LoginID.ToString(), Client.gUserName, hostName, hostIP, simulationType, licensePath, simulationPath, hostKey);

                    if (sRet == "success")
                        nRet = 1;
                }
            }
            catch (System.Exception ex)
            {
            }

            return nRet;

        }

        // 删除模拟器主机
        public static int DeleteSimulationHost(string simulationHostIDs)
        {
            int nRet = 0;

            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(Client.customBinding, Client.RemoteAddress))
                {
                    Client.SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();

                    string sRet = proxy.DeleteSimulationHost(Client.LoginID.ToString(), simulationHostIDs);

                    if (sRet != "0")
                        nRet = 1;
                }
            }
            catch (System.Exception ex)
            {
            }

            return nRet;

        }

        // 获取模拟器主机列表
        public static System.Data.DataTable GetSimulationHostList(string simulatonType = "")
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(Client.customBinding, Client.RemoteAddress))
                {
                    Client.SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();

                    return proxy.GetSimulationHostList(Client.LoginID.ToString(), simulatonType);
                }
            }
            catch (System.Exception ex)
            {
            }

            return null;
        }

        // 获取模拟器主机可执行文件路径
        public static string GetSimulationHostFilePath(string simulationHostID)
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(Client.customBinding, Client.RemoteAddress))
                {
                    Client.SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();

                    return proxy.GetSimulationHostFilePath(Client.LoginID.ToString(), simulationHostID);
                }
            }
            catch (System.Exception ex)
            {
            }

            return "";

        }

        // 获取模拟器主机许可文件路径
        public static string GetSimulationHostLicensePath(string simulationHostID)
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(Client.customBinding, Client.RemoteAddress))
                {
                    Client.SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();

                    return proxy.GetSimulationHostLicensePath(Client.LoginID.ToString(), simulationHostID);
                }
            }
            catch (System.Exception ex)
            {
            }

            return "";

        }
    }
}
