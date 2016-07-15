using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    //public class BxReferableElementSiteT<T> : BxElementSiteBase1
    //     where T : BxReferableElementBase, new()
    //{
    //    #region  members
    //    protected T _value;
    //    #endregion

    //    #region  properties
    //    public T Value { get { return _value; } }
    //    #endregion

    //    #region  constructor
    //    public BxReferableElementSiteT()
    //        : base(null, null)
    //    {
    //        _value = new T();
    //        _value.Owner = this;
    //    }
    //    #endregion

    //    #region IBxElementSite 成员
    //    public override IBxElement Element { get { return _value; } }
    //    #endregion
    //}

    public class BxElementSiteT<T> : BxElementSiteBase1
     where T : IBxElementOwner, IBxElement, new()
    {
        #region  members
        T _value;
        #endregion

        #region  properties
        public T Value
        {
            get
            {
                if (_value == null)
                {
                    _value = new T();
                    _value.Owner = this;
                }
                return _value;
            }
        }
        #endregion

        #region  constructor
        public BxElementSiteT()
        {
            //_value = new T();
            //_value.Owner = this;
        }
        #endregion

        #region IBxElementSite 成员
        public override IBxElement Element { get { return Value; } }
        #endregion


    }

}
