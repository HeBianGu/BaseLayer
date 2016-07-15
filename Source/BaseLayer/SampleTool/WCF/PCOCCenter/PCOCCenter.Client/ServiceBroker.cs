using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Channels;
namespace Common
{
    public class ServiceBroker
    {
        public static string ErrorInfo { get; set; }
        
        private static void LogText(string text)
        {
            ErrorInfo = text;
        }

        private static void SetMaxItemsInObjectGraph<T>(ChannelFactory<T> channelFactory)
        {
            foreach (System.ServiceModel.Description.OperationDescription op in channelFactory.Endpoint.Contract.Operations)
            {
                System.ServiceModel.Description.DataContractSerializerOperationBehavior dataContractBehavior =
                            op.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior>()
                            as System.ServiceModel.Description.DataContractSerializerOperationBehavior;
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }
        }

        public static void DisposeService<T>(T channel) where T : class
        {
            ICommunicationObject obj2 = channel as ICommunicationObject;
            if ((obj2 != null) && (obj2.State != CommunicationState.Closed))
            {
                try
                {
                    if (obj2.State != CommunicationState.Faulted)
                    {
                        obj2.Close();
                    }
                    else
                    {
                        obj2.Abort();
                    }
                }
                catch
                {
                }
            }
        }

        public static T FindService<T>(string RemoteAddress)
        {           
            return FindService<T>(RemoteAddress, "GZipHttpBinding");
        }

        public static T FindService<T>(string RemoteAddress, string endPointName)
        {
            try
            {
                CustomBinding customBinding = new CustomBinding(endPointName);

                ChannelFactory<T> channelFactory = new ChannelFactory<T>(customBinding, RemoteAddress);
                SetMaxItemsInObjectGraph(channelFactory);
                
                return channelFactory.CreateChannel();
            }
            catch (System.Exception ex)
            {
                LogText("Exception : " + ex.Message);
                if (ex.InnerException != null) LogText("Inner Exception : " + ex.InnerException.Message);
            }

            return default(T);
        }
    }
 
}
