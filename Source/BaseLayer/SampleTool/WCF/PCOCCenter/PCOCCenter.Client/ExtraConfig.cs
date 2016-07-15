using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OPT.PCOCCenter.Service.Interface;

namespace OPT.PCOCCenter.Client
{
    public class ExtraConfig
    {

        // 上传配置文件
        public static int AddExtraConfig(string configType, string configName, string configFile)
        {
            int nRet = 0;

            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(Client.customBinding, Client.RemoteAddress))
                {
                    Client.SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();

                    string sRet = proxy.AddExtraConfig(Client.LoginID.ToString(), Client.gUserName, configType, configName, configFile);

                    if (sRet != "0")
                        nRet = 1;
                }
            }
            catch (System.Exception ex)
            {
            }

            return nRet;

        }

        // 删除指定配置文件
        public static int DeleteExtraConfig(string configIDs)
        {
            int nRet = 0;

            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(Client.customBinding, Client.RemoteAddress))
                {
                    Client.SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();

                    string sRet = proxy.DeleteExtraConfig(Client.LoginID.ToString(), configIDs);

                    if (sRet != "0")
                        nRet = 1;
                }
            }
            catch (System.Exception ex)
            {
            }

            return nRet;

        }

        // 获取配置名称列表
        public static System.Data.DataTable GetExtraConfigNameList(string configType)
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(Client.customBinding, Client.RemoteAddress))
                {
                    Client.SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();

                    return proxy.GetExtraConfigNameList(Client.LoginID.ToString(), configType);
                }
            }
            catch (System.Exception ex)
            {
            }

            return null;
        }

        // 获取配置文件
        public static string GetExtraConfigFile(string configID)
        {
            try
            {
                using (ChannelFactory<ICenterService> channelFactory = new ChannelFactory<ICenterService>(Client.customBinding, Client.RemoteAddress))
                {
                    Client.SetMaxItemsInObjectGraph(channelFactory);

                    ICenterService proxy = channelFactory.CreateChannel();

                    return proxy.GetExtraConfigFile(Client.LoginID.ToString(), configID);
                }
            }
            catch (System.Exception ex)
            {
            }

            return "";

        }

    }
}
