using System;
using System.Collections.Generic;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public abstract class BxElementValueBase : BxElementEvent,
        IBxReferableElement, IBxPersistStorageNode
    {
        List<IBxElementReferer> _referers;
        IBxElementSiteEx _owner;

        public BxElementValueBase() { _owner = null; _referers = null; }

        #region IBxElement 成员
        public IBxElementSite[] ParentSites 
        {
            get
            {
                int pos = (_owner == null) ?0:1;
                int count = (_referers == null) ?pos:(pos+_referers.Count);
                IBxElementSite[] sites = new IBxElementSite[count];
                if(_owner != null)
                    sites[0] = _owner;
                //_referers.CopyTo(sites,pos);
                if (_referers != null)
                {
                    foreach (IBxElementSite one in _referers)
                    {
                        sites[pos] = one;
                    }
                }
                return sites;
            } 
        }
        #endregion

        #region IBxReferableElement 成员
        public void AddRefer(IBxElementReferer site)
        {
            if (_referers == null)
            {
                _referers = new List<IBxElementReferer>(1);
                _referers.Add(site);
            }
            else
            {
                if (_referers.IndexOf(site) > -1)
                    return;
                _referers.Add(site);
            }
        }
        public void BreakRefer(IBxElementReferer site)
        {
            if (_referers != null)
                _referers.Remove(site);
        }
        public bool HasReferer
        {
            get { return (_referers == null) ? false : (_referers.Count > 0); }
        }
        public IBxElementReferer[] Referers
        {
            get { return (_referers == null) ? new IBxElementReferer[0] : _referers.ToArray(); }
        }
        public IBxElementSiteEx Owner
        {
            set { _owner = value; }
            get { return _owner; }
        }
        #endregion

        #region IBxPersistStorageNode 成员
        public abstract void SaveStorageNode(IBxStorageNode node);
        public abstract void LoadStorageNode(IBxStorageNode node);
        #endregion

        //#region IBxUIValue 成员
        //public abstract string GetUIValue();
        //public abstract bool SetUIValue(string val);
        //#endregion


        public virtual IBxElementCarrier Carrier
        {
            get { throw new NotImplementedException(); }
        }
    }

    public abstract class BxSingleElement : BxElementValueBase, IBxPersistString, IBxUIValue
    {
        bool _valid;
        public virtual bool Valid { get { return _valid; } set { _valid = value; } }

        public BxSingleElement() { _valid = false; }

        #region IBxPersistStorageNode 成员
        public override void SaveStorageNode(IBxStorageNode node)
        {
            node.SetElement(BxStorageLable.elementValue, SaveToString());
        }
        public override void LoadStorageNode(IBxStorageNode node)
        {
            LoadFromString(node.GetElementValue(BxStorageLable.elementValue));
        }
        #endregion

        #region IBxUIValue 成员
        public virtual string GetUIValue()
        {
            return SaveToString();
        }
        public virtual bool SetUIValue(string val)
        {
            if (LoadFromString(val))
            {
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }
        #endregion

        #region IBxPersistString 成员
        public abstract string SaveToString();
        public abstract bool LoadFromString(string s);
        #endregion
    }

    public abstract class BxSingleElementT<T> : BxSingleElement
    /* where T : struct*/
    {
        T _value;

        public BxSingleElementT() { _value = default(T); }
        public BxSingleElementT(T val) { _value = val; Valid = true; }

        public virtual T Value
        {
            get
            {
                if (!Valid)
                    throw new Exception("invalid value!");
                return _value;
            }
            set { Valid = true; _value = value; }
        }


        #region IBxPersistString 成员
        public override string SaveToString()
        {
            return _value.ToString();
        }
        #endregion
    }

    


}
