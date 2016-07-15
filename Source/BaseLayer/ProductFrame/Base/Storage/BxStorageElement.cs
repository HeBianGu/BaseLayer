using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{

    public class BxStorageElement : IBxStorageElement
    {
        string _name;
        string _value;

        public BxStorageElement() { _name = null; _value = null; }
        public BxStorageElement(string name, string value) { _name = name; _value = value; }

        #region IBxStorageElement 成员
        public string Name { get { return _name; } }
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion
    }

}
