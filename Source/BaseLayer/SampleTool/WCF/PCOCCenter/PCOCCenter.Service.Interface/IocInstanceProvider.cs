using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace OPT.PEOfficeCenter.Service.Interface
{
    public class IocInstanceProvider : IInstanceProvider
    {
        Type _serviceType;
        IContainer _container;

        public IocInstanceProvider(Type serviceType)
        {
            _serviceType = serviceType;
            _container = CNBlogs.Infrastructure.CrossCutting.IoC.
                IoCFactory.Instance.CurrentContainter;
        }

        #region IInstanceProvider Members

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return _container.Resolve(_serviceType);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            if (instance is IDisposable)
                ((IDisposable)instance).Dispose();
        }

        #endregion
    }
}
