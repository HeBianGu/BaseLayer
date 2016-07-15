using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxGenerousSiteT<T> : BxElementSiteBase
         where T : BxSingleElement, new()
    {
        #region  members
        protected T _value;
        #endregion

        #region  properties
        public T Value { get { return _value; } }
        #endregion

        #region  constructor
        public BxGenerousSiteT()
            : base(null, null)
        {
            _value = new T();
            _value.Owner = this;
        }
        //public BxGenerousSiteT(BxCompoundValue container)
        //    : base(container, container.Carrier)
        //{
        //    _value = new T();
        //    _value.Owner = this;
        //}
        #endregion

        #region IBxElementSite 成员
        public override IBxElement Element { get { return _value; } }
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
                _value.SaveStorageNode(node);
            }
        }
        public override void LoadStorageNode(IBxStorageNode node)
        {
            base.LoadStorageNode(node);
            string id = node.GetElementValue(BxStorageLable.elementValueID);
            if (string.IsNullOrEmpty(id))
            {
                _value.LoadStorageNode(node);
            }
            else
            {
                node.Storage.SVA.LoadValue(_value, id);
            }
        }
        #endregion
    }


}
