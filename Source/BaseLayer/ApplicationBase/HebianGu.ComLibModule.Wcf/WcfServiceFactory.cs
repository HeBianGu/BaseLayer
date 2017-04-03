using HebianGu.ComLibModule.Wcf.Service;
using HebianGu.ComLibModule.Wcf.Service.Interface;
using HebianGu.ComLibModule.Wcf.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Wcf
{
    public class WcfServiceFactory
    {
        private WcfServiceFactory()
        {

        }

        public static WcfServiceFactory Instance = new WcfServiceFactory();

        /// <summary> 构建自动流服务 </summary>
        public Dictionary<Type, Type> BuildWorkScreamService()
        {
            Dictionary<Type, Type> serviceTypes = new Dictionary<Type, Type>();

            serviceTypes.Add(typeof(IFileTransferService), typeof(FileTransferService));

            serviceTypes.Add(typeof(IWorkScreamService), typeof(WorkScreamService));

            return serviceTypes;
        }

        /// <summary> 构建远程控制器服务 </summary>
        public Dictionary<Type, Type> BuildRemoveMonitorService()
        {
            Dictionary<Type, Type> serviceTypes = new Dictionary<Type, Type>();

            serviceTypes.Add(typeof(IExcuteService), typeof(ExcuteService));

            serviceTypes.Add(typeof(IImageSenderService), typeof(ImageSenderService));

            serviceTypes.Add(typeof(IStreamPrivider), typeof(StreamPrivider));

            return serviceTypes;
        }


    }
}
