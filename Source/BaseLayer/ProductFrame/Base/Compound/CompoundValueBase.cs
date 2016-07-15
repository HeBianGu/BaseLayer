using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace OPT.PEOffice6.BaseLayer.Base
{
    public class BxCompoundValueBase : BxElementValueBase, IBxPersistStorageNode
    {
        BxCompoundCore _core;
        protected BxCompoundCore Core
        {
            get
            {
                if (_core == null)
                    _InitCore();
                return _core;
            }
        }
        public BxCompoundValueBase()
        {
            _InitCore();
            _InitSubElements();
        }

        private void _InitCore()
        {
            Type type = this.GetType();
            while (type != typeof(BxCompoundValueBase))
            {
                object[] attribs = type.GetCustomAttributes(typeof(BxCompoundAttribute), false);
                if (attribs.Length > 0)
                {
                    _core = ((BxCompoundAttribute)attribs[0]).Core;
                    return;
                }
            }
        }
        private void _InitSubElements()
        {
            BxCompoundCore core = Core;
            IEnumerable<FieldInfo> fieldsInfo = core.GetAllFieldsInfo();
            foreach (FieldInfo one in fieldsInfo)
            {
                object field = one.GetValue(this);
                if (field is IBxElementInit)
                {
                    (field as IBxElementInit).BindToParent(this, _carrier);
                }
            }
        }

        protected IBxElementCarrier _carrier = null;

        public virtual void InitCarrier(IBxElementCarrier carrier)
        {
            _carrier = carrier;
        }


        #region IBxPersistStorageNode 成员

        public void SaveStorageNode(IBxStorageNode node)
        {
            throw new NotImplementedException();
        }

        public void LoadStorageNode(IBxStorageNode node)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
