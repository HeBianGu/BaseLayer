using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using OPT.PCOCCenter.Service;
using OPT.PCOCCenter.Service.Interface;
using OPT.PCOCCenter.Utils;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using OPT.PEOfficeCenter.Utils;

namespace OPT.PCOCCenter.Hosting
{
    class Program
    {
        static List<ServiceHost> serviceHosts = new List<ServiceHost>();

        static string ConfigFile = "CenterConfig.xml";

        static void StartWCFService(Dictionary<Type, Type> serviceTypes, string servicePort)
        {
            servicePort = XML.Read(ConfigFile, "Service", "Port", servicePort);

            string endpointAddress = string.Empty;
            string tName = string.Empty;

            foreach (var item in serviceTypes)
            {
                tName = item.Key.Name.Substring(1);
                endpointAddress = string.Format("http://localhost:{0}/{1}", servicePort, tName);
                ServiceHost host = new ServiceHost(item.Value, new Uri(endpointAddress));

                CustomBinding customBinding = new CustomBinding("GZipHttpBinding");
                customBinding.ReceiveTimeout = new TimeSpan(1, 0, 0);
                host.AddServiceEndpoint(item.Key, customBinding, string.Empty);

                ServiceMetadataBehavior behavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
                {
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
                }

                {
                    System.ServiceModel.Description.DataContractSerializerOperationBehavior dataContractBehavior =
                                host.Description.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior>()
                                as System.ServiceModel.Description.DataContractSerializerOperationBehavior;
                    if (dataContractBehavior != null)
                    {
                        dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                    }
                }

                host.Opened += delegate
                {
                    Console.WriteLine("{0}已经启动，按任意键终止服务！", tName);
                };

                host.Open();
                serviceHosts.Add(host);
            }

            Console.Read();
            if (serviceHosts != null)
            {
                foreach (ServiceHost t in serviceHosts)
                {
                    if (t != null)
                        t.Close();
                }
            }
        }
        
        static void Main(string[] args)
        {
            CenterService centerService = new CenterService();
            centerService.InitCenterService(AppDomain.CurrentDomain.BaseDirectory + "//" + ConfigFile);

            Dictionary<Type, Type> serviceTypes = new Dictionary<Type, Type>();
            serviceTypes.Add(typeof(ISimulationAgentService), typeof(SimulationAgentService));
            serviceTypes.Add(typeof(IFileTransferService), typeof(FileTransferService));
            serviceTypes.Add(typeof(ICenterService), typeof(CenterService));
            StartWCFService(serviceTypes, "22888");
        }
    }
}
