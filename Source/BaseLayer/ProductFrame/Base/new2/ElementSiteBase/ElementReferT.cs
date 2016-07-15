using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxElementReferT<T> : BxElementSiteBase1, IBxElementReferer
        where T : class, IBxElementOwner, IBxReferableElement
    {
        #region  members
        protected T _value = null;
        #endregion

        #region  properties
        public T Value { get { return _value; } }

        #endregion

        #region  constructor
        #endregion


        #region IBxElementSite 成员
        public override IBxElement Element { get { return _value; } }
        #endregion

        #region IBxElementReferer 成员
        public virtual void ReferTo(IBxReferableElement val)
        {
            if (!(val is T))
                throw new Exception("object referred must be type of " + typeof(T).Name);
            if (!object.ReferenceEquals(null, _value))
                _value.BreakRefer(this);
            _value = val as T;
            _value.AddRefer(this);

            if (_value is IBxElementInit)
                (_value as IBxElementInit).InitCarrier(_carrier);
        }
        #endregion

        public virtual void ReferToEx(T val)
        {
            if (!object.ReferenceEquals(null, _value))
                _value.BreakRefer(this);
            _value = val;
            _value.AddRefer(this);

            if (_carrier != null)
            {
                if (_value is IBxElementInit)
                    (_value as IBxElementInit).InitCarrier(_carrier);
            }
        }

    }


}
