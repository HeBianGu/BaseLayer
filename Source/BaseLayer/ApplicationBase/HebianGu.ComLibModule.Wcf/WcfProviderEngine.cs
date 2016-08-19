﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Wcf
{

    /// <summary> WCF服务引擎 </summary>
    public partial class WcfProviderEngine
    {
        private WcfProviderEngine()
        {

        }

        public static WcfProviderEngine Instance = new WcfProviderEngine();

        List<ServiceHost> serviceHosts = new List<ServiceHost>();

    }

    public partial class WcfProviderEngine
    {
        /// <summary> 根据类型字典和端口注册服务 </summary>
        public void AddService(Dictionary<Type, Type> serviceTypes)
        {
            string endpointAddress = string.Empty;

            string tName = string.Empty;

            foreach (var item in serviceTypes)
            {
                tName = item.Key.Name.Substring(1);

                endpointAddress = string.Format(WcfConfiger.RomoteFormat, WcfConfiger.Instance.IP, WcfConfiger.Instance.Port, tName);

                ServiceHost host = new ServiceHost(item.Value, new Uri(endpointAddress));

                WSHttpBinding wsHttpBinding = new WSHttpBinding();
                wsHttpBinding.MaxBufferPoolSize = int.MaxValue;
                wsHttpBinding.MaxReceivedMessageSize = int.MaxValue;
                wsHttpBinding.ReceiveTimeout = new TimeSpan(1, 0, 0);
                wsHttpBinding.Security = new System.ServiceModel.WSHttpSecurity();
                wsHttpBinding.Security.Mode = SecurityMode.None;
                host.AddServiceEndpoint(item.Key, wsHttpBinding, string.Empty);

                ServiceMetadataBehavior behavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>();

                if (behavior == null)
                {
                    behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    host.Description.Behaviors.Add(behavior);
                }
                else
                {
                    behavior.HttpGetEnabled = true;
                }

                DataContractSerializerOperationBehavior dataContractBehavior = host.Description.Behaviors.Find<DataContractSerializerOperationBehavior>()
                            as DataContractSerializerOperationBehavior;

                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }

                host.Open();
                serviceHosts.Add(host);
            }
        }
    }
}
