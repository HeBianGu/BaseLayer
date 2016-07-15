using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxElementCarrier : IBxElementCarrier
    {
        BxCompoundCore _core;
        IBxSystemInfo _systemInfo = null;

        public IBxSystemInfo SystemInfo
        {
            get
            {
                if (_systemInfo == null)
                    return BxSystemInfo.Instance;
                return _systemInfo;
            }
        }

        public BxElementCarrier() 
        {
            _core = new BxCompoundCore(this.GetType());
        }
        public BxElementCarrier(IBxSystemInfo systemInfo)
        {
            _systemInfo = systemInfo;
        }


        #region IBxElementCarrier 成员
        public IBxUIConfigProvider UIConfigProvider
        {
            get { return SystemInfo.UIConfigProvider; }
        }
        public int ManageElement(IBxElementSite element)
        {
            return -1;
        }
        public void RemoveElement(IBxElementSite element)
        {
            //TODO :RemoveElement
        }
        public IBxElementSite GetElement(int id)
        {
            //TODO:GetElement
            return null;
        }
        #endregion
    }
}
