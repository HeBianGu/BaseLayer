using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxReferSiteT<T> : BxElementSiteBase, IBxElementReferer
        where T : BxSingleElement
    {
        #region  members
        protected T _value;
        #endregion

        #region  properties
        public T Value { get { return _value; } }
        public override IBxElement Element { get { return _value; } }
        public bool Valid
        {
            get { return object.ReferenceEquals(null, _value) ? false : _value.Valid; }
            set
            {
                if (object.ReferenceEquals(null, _value))
                    throw new Exception("value is Null!");
                _value.Valid = value;
            }
        }
        #endregion

        #region  constructor
        public BxReferSiteT(): base()
        { }
        //public BxReferSiteT(BxCompoundValue container)
        //    : base(container, container.Carrier)
        //{ }
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
        }
        #endregion

        #region IBxPersistStorageNode 成员
        public override void SaveStorageNode(IBxStorageNode node)
        {
            base.SaveStorageNode(node);
            if (_value == null)
                return;
            string id = node.Storage.SVA.SaveValue(_value);
            node.SetElement(BxStorageLable.elementValueID, id);
        }
        public override void LoadStorageNode(IBxStorageNode node)
        {
            base.LoadStorageNode(node);
            string sID = node.GetElementValue(BxStorageLable.elementValueID);
            if (string.IsNullOrEmpty(sID))
                return;
            node.Storage.SVA.RequestRestoreRefer(this, sID);
        }
        #endregion
    }


}
