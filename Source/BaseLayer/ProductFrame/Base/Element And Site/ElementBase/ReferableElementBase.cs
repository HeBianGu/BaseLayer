using System;
using System.Collections.Generic;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public interface IBxElementOwner
    {
        IBxElementSiteEx Owner { get; set; }
    }

    public abstract class BxReferableElementBase : BxElementEvent,
        IBxReferableElement, 
        IBxPersistStorageNode, 
        IBxElementOwner,
        IBxElementInit, 
        IBxModifyManage
    {
        List<IBxElementReferer> _referers;
        IBxElementSiteEx _owner;
        protected IBxElementCarrier _carrier;

        public BxReferableElementBase() { _owner = null; _referers = null; }

        protected IBxElementSite FirstParentSite
        {
            get
            {
                if (_owner != null)
                   return _owner;
                else if (_referers != null)
                {
                    if (_referers.Count > 0)
                        return _referers[0];
                }
                return null;
            }
        }

        #region IBxElementEx 成员
        public IBxElementSite[] ParentSites
        {
            get
            {
                int pos = (_owner == null) ? 0 : 1;
                int count = (_referers == null) ? pos : (pos + _referers.Count);
                IBxElementSite[] sites = new IBxElementSite[count];
                if (_owner != null)
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
        //public IBxElementCarrier Carrier
        //{
        //    get
        //    {
        //        if (_carrier == null)
        //        {
        //            if (_owner != null)
        //                _carrier = _owner.Carrier;
        //            else if (_referers != null)
        //            {
        //                if (_referers.Count > 0)
        //                    _carrier = _referers[0].Carrier;
        //            }
        //        }
        //        return _carrier;
        //    }
        //    set { _carrier = value; }
        //}
        #endregion

        #region IBxReferableElement 成员
        public virtual void AddRefer(IBxElementReferer site)
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
        public virtual IBxElementSiteEx Owner
        {
            set { _owner = value; }
            get { return _owner; }
        }
        #endregion

        #region IBxPersistStorageNode 成员
        public abstract void SaveStorageNode(IBxStorageNode node);
        public abstract void LoadStorageNode(IBxStorageNode node);
        #endregion

        protected virtual void OnModified()
        {
            if (_carrier != null)
                _carrier.AddModified(this);
        }

        #region IBxElementInit 成员
        public virtual void ResetCarrier(IBxElementCarrier carrier)
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
}
