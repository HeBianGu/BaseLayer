using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace OPT.PEOffice6.BaseLayer.Base
{
    public class BxInt32EG : BxElementGenerousT<BxInt32V>
    {
        public BxInt32EG(IBxElementEx container)
            : base(container, container.Carrier)
        { }
    }
}
