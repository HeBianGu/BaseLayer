using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace OPT.PEOffice6.BaseLayer.Base
{
    public class BxElementSiteT<T> : BxElementBase, IBxElementSite
        where T : BxElementValue
    {
        #region  members
        protected T _value;
        #endregion

        #region  properties
        public T Value { get { return _value; } }
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
        public BxElementSiteT(IBxElement container, IBxElementCarrier carrier)
            : base(container, carrier)
        { }
        public BxElementSiteT(IBxElementEx container)
            : base(container, container.Carrier)
        { }
        #endregion

        #region IBxElementSite 成员
        public virtual void ReferTo(IBxElementValue val)
        {
            if (!(val is T))
                throw new Exception("object referred must be type of " + typeof(T).Name);
            if (object.ReferenceEquals(null, _value))
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
