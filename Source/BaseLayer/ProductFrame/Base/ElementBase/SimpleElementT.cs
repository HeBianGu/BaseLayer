using System;
using System.Collections.Generic;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public abstract class BxSimpleElementT<T> : BxElementSiteBase, IBxUIValue, IBxPersistString,
        IBxElement
    {
        #region  members
        protected T _value = default(T);
        protected bool _valid = false;
        #endregion

        #region  properties
        public T Value { get { return _value; } set { _valid = true; _value = value; } }
        public override IBxElement Element { get { return this; } }
        public IBxElementSite[] ParentSites { get { return new IBxElementSite[]{this}; } }
        public bool Valid
        {
            get { return _valid; }
            set { _valid = value; }
        }
        #endregion

        #region  constructor
        public BxSimpleElementT(): base()
        { }
        public BxSimpleElementT(BxCompoundValue container, IBxElementCarrier carrier)
            : base(container, carrier)
        { }
        public BxSimpleElementT(BxCompoundValue container)
            : base(container, container.Carrier)
        { }
        #endregion

        #region IBxPersistStorageNode 成员
        public override void SaveStorageNode(IBxStorageNode node)
        {
            base.SaveStorageNode(node);
            if (!_valid)
                return;
            node.SetElement(BxStorageLable.elementValue, SaveToString());
        }
        public override void LoadStorageNode(IBxStorageNode node)
        {
            base.LoadStorageNode(node);
            string sVal = node.GetElementValue(BxStorageLable.elementValue);
            if (string.IsNullOrEmpty(sVal))
            {
                _valid = false;
            }
            else
            {
                LoadFromString(sVal);
            }
        }
        #endregion

        #region IBxPersistString 成员
        public virtual string SaveToString()
        {
            return _value.ToString();
        }
        public abstract bool LoadFromString(string s);
        #endregion

        #region IBxUIValue 成员
        public virtual string GetUIValue()
        {
            return SaveToString();
        }
        public virtual bool SetUIValue(string val)
        {
            return LoadFromString(val);
        }
        #endregion
    }


}
