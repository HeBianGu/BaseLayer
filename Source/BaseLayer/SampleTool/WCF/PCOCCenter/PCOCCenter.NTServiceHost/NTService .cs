using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using OPT.PCOCCenter.Service;
using OPT.PCOCCenter.Service.Interface;
using System.ServiceModel.Description;
using OPT.PCOCCenter.Utils;
using OPT.PEOfficeCenter.Utils;

namespace OPT.PCOCCenter.NTServiceHost
{
    public partial class NTService : ServiceBase
    {
        private List<ServiceHost> serviceHosts = new List<ServiceHost>();

        public NTService()
        {
            InitializeComponent();
        }

        static string ConfigFile = "CenterConfig.xml";

        void StartWCFService(Dictionary<Type, Type> serviceTypes, string servicePort)
        {
            servicePort = XML.Read(ConfigFile, "Service", "Port", servicePort);

            string endpointAddress = string.Empty;
            string tName = string.Empty;

            foreach (var item in serviceTypes)
            {
                tName = item.Key.Name.Substring(1);
                endpointAddress = string.Format("http://localhost:{0}/{1}", servicePort, tName);
                ServiceHost host = new ServiceHost(item.Value, new Uri(endpointAddress));

                WSHttpBinding wsHttpBinding = new WSHttpBinding();
                wsHttpBinding.MaxBufferPoolSize = int.MaxValue;
                wsHttpBinding.MaxReceivedMessageSize = int.MaxValue;
                wsHttpBinding.ReceiveTimeout = new TimeSpan(1, 0, 0);
                wsHttpBinding.Security = new System.ServiceModel.WSHttpSecurity();
                wsHttpBinding.Security.Mode = SecurityMode.None;
                host.AddServiceEndpoint(item.Key, wsHttpBinding, string.Empty);

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

                host.Open();
                serviceHosts.Add(host);
            }
        }

        public void StartService()
        {
            CenterService centerService = new CenterService();
            centerService.InitCenterService(AppDomain.CurrentDomain.BaseDirectory + "//" + ConfigFile);

            Dictionary<Type, Type> serviceTypes = new Dictionary<Type, Type>();
            serviceTypes.Add(typeof(IFileTransferService), typeof(FileTransferService));
            serviceTypes.Add(typeof(ICenterService), typeof(CenterService));
            StartWCFService(serviceTypes, "22888");
        }

        protected override void OnStart(string[] args)
        {
            StartService();
        }

        protected override void OnStop()
        {
            if (serviceHosts != null)
            {
                foreach (ServiceHost t in serviceHosts)
                {
                    if (t != null)
                        t.Close();
                }
            }
        }
    }
}
