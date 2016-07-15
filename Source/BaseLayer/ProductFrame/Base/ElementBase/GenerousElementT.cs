using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace OPT.PEOffice6.BaseLayer.Base
{
    public class BxElementGenerousT<T> : BxElementBase
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
        public BxElementGenerousT(IBxElement container, IBxElementCarrier carrier)
            : base(container, carrier)
        { }
        public BxElementGenerousT(IBxElementEx container)
            : base(container, container.Carrier)
        { }
        #endregion

        #region IBxPersistStorageNode 成员
        public override void SaveStorageNode(IBxStorageNode node)
        {
            base.SaveStorageNode(node);
            if (_value.HasReferer)
            {
                string id = node.Storage.SVA.SaveValue(_value);
                node.SetElement(BxStorageLable.elementValueID, id);
            }
            else
            {
                IBxStorageNode valNode = node.CreateChildNode(BxStorageLable.nodeValue);
                _value.SaveStorageNode(valNode);
            }
        }
        public override void LoadStorageNode(IBxStorageNode node)
        {
            base.LoadStorageNode(node);
            string id = node.GetElementValue(BxStorageLable.elementValueID);
            if (string.IsNullOrEmpty(id))
            {
                IBxStorageNode valNode = node.GetChildNode(BxStorageLable.nodeValue);
                _value.LoadStorageNode(valNode);
            }
            else
            {
                node.Storage.SVA.LoadValue(_value, id);
            }
        }
        #endregion
    }


}
