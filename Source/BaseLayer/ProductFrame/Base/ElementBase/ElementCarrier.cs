using System;

namespace OPT.PEOffice6.BaseLayer.Base
{
    public class BxElementCarrier : IBxElementCarrier
    {
        IBxSystemInfo _systemInfo = null;

        public BxElementCarrier() { }
        public BxElementCarrier(IBxSystemInfo systemInfo)
        {
            _systemInfo = systemInfo;
        }

        #region IBxElementCarrier 成员
        public IBxUIConfigProvider UIConfigProvider
        {
            get { return _systemInfo.UIConfigProvider; }
        }
        public int ManageElement(IBxElementEx element)
        {
            return -1;
        }

        public void RemoveElement(IBxElementEx element)
        {
            //TODO :RemoveElement
        }

        public IBxElementEx GetElement(int id)
        {
            //TODO:GetElement
            return null;
        }
        #endregion
    }
}
