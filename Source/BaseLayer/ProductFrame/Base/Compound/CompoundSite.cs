using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxCompoundSite<T> : BxElementSiteBase
         where T : BxCompoundValue, new()
    {
        #region  members
        T _value = null;
        #endregion

        public T Value
        {
            get
            {
                if (_value == null)
                {
                    if (_carrier == null)
                    {
                        _value = new T();
                    }
                    else
                    {
                        Type t = typeof(T);
                        ConstructorInfo ci = t.GetConstructor(new Type[] { typeof(IBxElementCarrier) });
                        if (ci != null)
                        {
                            _value = ci.Invoke(new object[] { _carrier }) as T;
                        }
                        else
                        {
                            _value = new T();
                            _value.InitCarrier(_carrier);
                        }
                    }
                    _value.Owner = this;
                }
                return _value;
            }
        }

        public override IBxElement Element { get { return Value; } }
        //public IEnumerable<IBxElementSite> Elements
        //{
        //    get { return _value.ChildSites; }
        //}

        #region  constructor
        public BxCompoundSite()
            : base()
        {
            //_value = new T();
            //_value.Owner = this;
        }
        //public BxCompoundSite(IBxCompound container, IBxElementCarrier carrier)
        //    : base(container, carrier)
        //{
        //    //_value = new T();
        //    //_value.Owner = this;
        //}
        #endregion

        public override void InitCarrier(IBxElementCarrier carrier)
        {
            base.InitCarrier(carrier);
            if (_value != null)
                _value.InitCarrier(carrier);
        }

        #region IBxPersistStorageNode 成员
        public override void SaveStorageNode(IBxStorageNode node)
        {
            base.SaveStorageNode(node);
            if (Value.HasReferer)
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
                Value.LoadStorageNode(node);
            }
            else
            {
                node.Storage.SVA.LoadValue(_value, id);
            }
        }
        #endregion
    }

    public class BxCompoundRefer<T> : BxElementSiteBase, IBxElementReferer
        where T : BxCompoundValue
    {
        #region  members
        protected T _value = null;
        #endregion

        #region  properties
        public T Value { get { return _value; } }
        public override IBxElement Element { get { return _value; } }
        #endregion

        #region  constructor
        public BxCompoundRefer()
            : base()
        { }
        //public BxCompoundRefer(IBxCompound container, IBxElementCarrier carrier)
        //    : base(container, carrier)
        //{
        //}
        #endregion

        #region IBxElementReferSite 成员
        public virtual void ReferTo(IBxReferableElement val)
        {
            if (!(val is T))
                throw new Exception("object referred must be type of " + typeof(T).Name);
            if (object.ReferenceEquals(null, _value))
                _value.BreakRefer(this);
            _value = val as T;
            _value.AddRefer(this);
        }
        #endregion

        //#region IBxCompound 成员
        //public IEnumerable<IBxElement> Elements
        //{
        //    get 
        //    {
        //        if (_value == null)
        //            return null;
        //        return _value.ChildSites; 
        //    }
        //}
        //#endregion

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
