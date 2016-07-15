using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxArray<T> : BxReferableElementBase, IBxContainer,
        IBxPersistStorageNode, IBxElementInit, IList<T>, IList, IBxElementInit_ForOldCode
       where T : BxElementSiteBase1, new()
    {
        protected BxSUICPregnant _childSiteSUICPregnant = BxSUICPregnant.Invalid;
        List<T> _childs = new List<T>();

        protected BxSUICPregnant ChildSiteSUICPregnant
        {
            get
            {
                if (_childSiteSUICPregnant == BxSUICPregnant.Invalid)
                {
                    _childSiteSUICPregnant = null;

                    BxElementSiteBase1 owner = FirstParentSite as BxElementSiteBase1;
                    if (owner != null)
                    {
                        BxXmlUIItem ownerStaticUIConfig = owner.StaticUIConfig;
                        if ((ownerStaticUIConfig != null)/* && !string.IsNullOrEmpty(ownerStaticUIConfig.SubArrayUIItemID)*/)
                        {
                            if(string.IsNullOrEmpty(ownerStaticUIConfig.SubArrayUIItemID))
                                _childSiteSUICPregnant = new BxSUICPregnant(ownerStaticUIConfig.FullID, ownerStaticUIConfig.SUICProvider);
                            else
                            _childSiteSUICPregnant = new BxSUICPregnant(ownerStaticUIConfig.SubArrayUIItemFullID, ownerStaticUIConfig.SUICProvider);
                        }
                    }
                }
                return _childSiteSUICPregnant;
            }
        }

        public BxArray()
        {
        }

        public override IBxElementSiteEx Owner
        {
            set { base.Owner = value; InitChildSiteSUICPregnant(); }
            get { return base.Owner; }
        }

        public override void AddRefer(IBxElementReferer site)
        {
            base.AddRefer(site);

            if ((Owner == null) && (Referers.Length == 1))
            {
                InitChildSiteSUICPregnant();
            }
        }

        void InitChildSiteSUICPregnant()
        {
            _childSiteSUICPregnant = BxSUICPregnant.Invalid;
            BxSUICPregnant suicp = ChildSiteSUICPregnant;

            foreach (IBxElementSite one in ChildSites)
            {
                if (one is IBxElementSiteInit)
                {
                    (one as IBxElementSiteInit).ResetCarrier(_carrier);
                    (one as IBxElementSiteInit).InitSUICPregnant(suicp);
                }
            }
        }

        #region IBxElementInit
        public override void ResetCarrier(IBxElementCarrier carrier)
        {
            if (_carrier == carrier)
                return;
            _carrier = carrier;

            _childSiteSUICPregnant = BxSUICPregnant.Invalid;
            BxSUICPregnant suicp = ChildSiteSUICPregnant;
            foreach (IBxElementSite one in ChildSites)
            {
                if (one is IBxElementSiteInit)
                {
                    (one as IBxElementSiteInit).ResetCarrier(carrier);
                    (one as IBxElementSiteInit).InitSUICPregnant(suicp);
                }
            }
        }
        #endregion

        public IEnumerable<T> ElementsEx
        {
            get { return _childs; }
        }
        //public T this[int index]
        //{
        //    get { return _childs[index]; }
        //}

        #region IBxContainer 成员
        public IEnumerable<IBxElementSite> ChildSites
        {
            get { return _childs; }
        }
        public int Count
        {
            get { return _childs.Count; }
        }
        public IBxElementSite GetAt(int index)
        {
            return _childs[index];
        }
        public virtual void Append()
        {
            _childs.Add(CreateElement());
            OnModified();
        }

        public virtual void AppendRange(int size)
        {
            _childs.AddRange(CreateElements(size));
            OnModified();
        }
        public void Insert(int index)
        {
            _childs.Insert(index, CreateElement());
            OnModified();
        }
        public virtual void InsertRange(Int32 index, Int32 size)
        {
            _childs.InsertRange(index, CreateElements(size));
            OnModified();
        }
        public void Remove(int index)
        {
            _childs.RemoveAt(index);
            OnModified();
        }
        public virtual void RemoveRange(int index, int size)
        {
            _childs.RemoveRange(index, size);
            OnModified();
        }
        public void RemoveAll()
        {
            _childs.RemoveRange(0, _childs.Count);
            OnModified();
        }
        #endregion

        public T AppendEx()
        {
            T val = CreateElement();
            _childs.Add(val);
            OnModified();
            return val;
        }

        protected T CreateElement()
        {
            T ele = new T();
            ele.InitContainer(this);
            ele.ResetCarrier(_carrier);
            ele.InitSUICPregnant(ChildSiteSUICPregnant);
            return ele;
        }
        protected T[] CreateElements(int size)
        {
            T[] eles = new T[size];
            for (int i = 0; i < eles.Length; i++)
            {
                eles[i] = new T();
                eles[i].InitContainer(this);
                eles[i].ResetCarrier(_carrier);
                eles[i].InitSUICPregnant(ChildSiteSUICPregnant);
            }
            return eles;
        }

        protected void InitSubElement(T ele)
        {
            ele.InitContainer(this);
            ele.ResetCarrier(_carrier);
            ele.InitSUICPregnant(ChildSiteSUICPregnant);
        }

        #region IBxPersistStorageNode 成员
        public override void SaveStorageNode(IBxStorageNode node)
        {
            IEnumerable<IBxElementSite> elements = ChildSites;
            int index = 0;
            string sNodeName;
            foreach (IBxElementSite one in elements)
            {
                if (one is IBxPersistStorageNode)
                {
                    sNodeName = BxStorageLable.nodeArrayElement + index.ToString();
                    (one as IBxPersistStorageNode).SaveStorageNode(node.CreateChildNode(sNodeName));
                    index++;
                }
            }
            node.SetElement(BxStorageLable.elementCount, index.ToString());
        }
        public override void LoadStorageNode(IBxStorageNode node)
        {
            int count = Convert.ToInt32(node.GetElementValue(BxStorageLable.elementCount));
            RemoveAll();
            AppendRange(count);

            IEnumerable<IBxElementSite> elements = ChildSites;
            IEnumerable<IBxStorageNode> childs = node.ChildNodes;
            using (IEnumerator<IBxStorageNode> itor = childs.GetEnumerator())
            {
                itor.Reset();
                foreach (IBxElementSite one in elements)
                {
                    if (one is IBxPersistStorageNode)
                    {
                        if (!itor.MoveNext())
                            break;
                        (one as IBxPersistStorageNode).LoadStorageNode(itor.Current);
                    }
                }
            }
        }
        #endregion


        #region IList<T>
        public int IndexOf(T item)
        {
            return _childs.IndexOf(item);
        }
        public void Insert(int index, T item)
        {
            InitSubElement(item);
            _childs.Insert(index, item);
            OnModified();
        }
        public void RemoveAt(int index)
        {
            _childs.RemoveAt(index);
            OnModified();
        }
        public T this[int index]
        {
            get { return _childs[index]; }
            set
            {
                InitSubElement(value);
                _childs[index] = value;
            }
        }
        public void Add(T item)
        {
            InitSubElement(item);
            _childs.Add(item);
            OnModified();
        }
        public void Clear()
        {
            _childs.Clear();
            OnModified();
        }
        public bool Contains(T item)
        {
            return _childs.Contains(item);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            _childs.CopyTo(array, arrayIndex);
        }
        public bool IsReadOnly
        {
            get { return false; }
        }
        public bool Remove(T item)
        {
            if (_childs.Remove(item))
            {
                OnModified();
                return true;
            }
            return false;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return _childs.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _childs.GetEnumerator();
        }
        #endregion


        int IList.Add(object value)
        {
            int n = (_childs as IList).Add(value);
            if (n > -1)
                OnModified();
            return n;
        }
        void IList.Clear()
        {
            (_childs as IList).Clear();
            OnModified();
        }
        bool IList.Contains(object value)
        {
            return (_childs as IList).Contains(value);
        }
        int IList.IndexOf(object value)
        {
            return (_childs as IList).IndexOf(value);
        }
        void IList.Insert(int index, object value)
        {
            if (!(value is T))
                throw new Exception();
            Insert(index, value as T);
            OnModified();
        }
        bool IList.IsFixedSize
        {
            get { return false; }
        }
        bool IList.IsReadOnly
        {
            get { return false; }
        }
        void IList.Remove(object value)
        {
            (_childs as IList).Remove(value);
            OnModified();
        }
        void IList.RemoveAt(int index)
        {
            (_childs as IList).RemoveAt(index);
            OnModified();
        }
        object IList.this[int index]
        {
            get { return this[index]; }
            set
            {
                if (!(value is T))
                    throw new Exception();
                this[index] = value as T;
            }
        }
        void ICollection.CopyTo(Array array, int index)
        {
            (_childs as IList).CopyTo(array, index);
        }
        int ICollection.Count
        {
            get { return Count; }
        }
        bool ICollection.IsSynchronized
        {
            get { return false; }
        }
        object ICollection.SyncRoot
        {
            get { return null; }
        }

        protected virtual void OnInit()
        {
        }
        bool inited = false;
        public void OldInit()
        {
            if (inited)
                return;
            inited = true;
            OnInit();
            foreach (IBxElementSite one in ChildSites)
            {
                if (one.Element is IBxElementInit_ForOldCode)
                    (one.Element as IBxElementInit_ForOldCode).OldInit();
            }
        }
    }

}
