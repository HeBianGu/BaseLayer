using System;
using System.Collections.Generic;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public abstract class BxElementBase : BxElementEvent,
        IBxElement, IBxUIValueEx, IBxPersistString, IBxPersistStorageNode, IBxElementOwner,
        IBxElementInit, IBxModifyManage
    {
        #region  members
        IBxElementSiteEx _owner = null;
        protected bool _valid = false;
        protected IBxElementCarrier _carrier;
        bool _isDefault = false;
        #endregion

        public IBxElementSite[] ParentSites { get { return new IBxElementSite[] { _owner }; } }
        public IBxElementSiteEx Owner { get { return _owner; } set { _owner = value; } }
        public virtual bool Valid
        {
            get { return _valid; }
            set { _valid = value; }
        }
        public bool IsDefault
        {
            get { return _isDefault; }
            set
            {
                _isDefault = value;
            }
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
            node.SetElement(BxStorageLable.elementValueDefault, _isDefault.ToString());
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

            sVal = node.GetElementValue(BxStorageLable.elementValueDefault);
            if (string.IsNullOrEmpty(sVal))
            {
                _isDefault = false;
            }
            else
            {
                bool.TryParse(sVal, out _isDefault);
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
        public virtual string GetUIValue(int? decimalDigits)
        {
            return GetUIValue();
        }
        public virtual bool IsLegalValue(string val)
        {
            return true;
        }

        #endregion

        protected virtual void OnModified()
        {
            if (_carrier != null)
                _carrier.AddModified(this);
        }

        #region IBxElementInit 成员
        public void ResetCarrier(IBxElementCarrier carrier)
        {
            _carrier = carrier;
        }
        #endregion

        #region IBxModifyManage 成员
        public void ResetModifyFlag()
        {
            if (_carrier != null)
                _carrier.RemoveModified(this);
        }
        #endregion
    }

    public abstract class BxElementT<T> : BxElementBase
    {
        #region  members
        T _value = default(T);
        #endregion

        #region  properties
        public T Value
        {
            get { return _value; }
            set
            {
                _valid = true;
                _value = value;
                IsDefault = false;
                OnModified();
            }
        }
        #endregion

        public void InitValue(T val)
        {
            _value = val;
            _valid = true;
        }

        #region  constructor
        public BxElementT() { }
        public BxElementT(IBxElementSiteEx owner) : base(owner) { }
        public BxElementT(T val) { _value = val; Valid = true; }
        #endregion

        public override string SaveToString()
        {
            if (!Valid)
                return null;
            return _value.ToString();
        }
    }


}
