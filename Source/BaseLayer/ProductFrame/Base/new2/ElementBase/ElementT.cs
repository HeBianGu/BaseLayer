using System;
using System.Collections.Generic;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public abstract class BxElementBase : BxElementEvent,
        IBxElement, IBxUIValue, IBxPersistString, IBxPersistStorageNode, IBxElementOwner
    {
        #region  members
        IBxElementSiteEx _owner = null;
        protected bool _valid = false;
        #endregion

        public IBxElementSite[] ParentSites { get { return new IBxElementSite[] { _owner }; } }
        public IBxElementSiteEx Owner { get { return _owner; } set { _owner = value; } }
        public virtual bool Valid
        {
            get { return _valid; }
            set { _valid = value; }
        }

        #region  constructor
        public BxElementBase() { }
        public BxElementBase(IBxElementSiteEx owner) { _owner = owner; }
        #endregion

        #region IBxPersistStorageNode 成员
        public virtual void SaveStorageNode(IBxStorageNode node)
        {
            if (!_valid)
                return;
            node.SetElement(BxStorageLable.elementValue, SaveToString());
        }
        public virtual void LoadStorageNode(IBxStorageNode node)
        {
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
        public abstract string SaveToString();
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

    public abstract class BxElementT<T> : BxElementBase
    {
        #region  members
        protected T _value = default(T);
        #endregion

        #region  properties
        public T Value { get { return _value; } set { _valid = true; _value = value; } }
        #endregion

        #region  constructor
        public BxElementT() { }
        public BxElementT(IBxElementSiteEx owner) : base(owner) { }
        #endregion

        public override string SaveToString()
        {
            return _value.ToString();
        }
    }


}
