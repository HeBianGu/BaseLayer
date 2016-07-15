using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace OPT.PEOffice6.BaseLayer.Base
{
    public class BxInt32Site : BxElementSiteT<BxInt32V>
    {
        public BxInt32Site(IBxCompound container, IBxElementCarrier carrier)
            : base(container, carrier)
        { }
        public BxInt32Site(IBxElementEx container)
            : base(container, container.Carrier)
        { }

        public Int32 ValueEx 
        {
            get { return _value.Value; }
            set { _value.Value = value; }
        }
    }

}
