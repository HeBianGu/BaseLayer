using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxCarrier : IBxElementCarrier
    {
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

        public BxCarrier() 
        {
            InitElements();
        }
        public BxCarrier(IBxSystemInfo systemInfo)
        {
            _systemInfo = systemInfo;
            InitElements();
        }

        protected void InitElements()
        {
            FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
            foreach (FieldInfo one in fields)
            {
                if (one.GetCustomAttributes(typeof(BxCarrierElement), false).Length > -1)
                {
                    IBxElementInit ele = one.GetValue(this) as IBxElementInit;
                    if (ele != null)
                    {
                        ele.InitCarrier(this);
                    }
                }
            }
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
