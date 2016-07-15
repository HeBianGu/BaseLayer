using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxArray<T> : BxReferableElementBase, IBxContainer,
        IBxPersistStorageNode, IBxElementInit, IList<T>, IList
       where T : BxElementSiteBase1, new()
    {
        protected BxXmlUIItem _staticItem = null;
        List<T> _childs = new List<T>();

        protected BxXmlUIItem StaticXmlItem
        {
            get
            {
                if (_staticItem == null)
                {
                    BxElementSiteBase1 owner = Owner as BxElementSiteBase1;
                    if (owner != null)
                    {
                        BxXmlUIItem ownerXmlItem = owner.GetStaticXmlUIItem();
                        if (ownerXmlItem != null)
                            _staticItem = ownerXmlItem.SubArrayUIItem;
                    }
                }
                return _staticItem;
            }
        }

        public BxArray()
        {
        }

        #region IBxElementInit
        public virtual void InitCarrier(IBxElementCarrier carrier)
        {
            if (carrier == null)
                throw new NullReferenceException();
            if (_carrier == carrier)
                return;
            bool bFirstInit = (_carrier == null);
            _carrier = carrier;

            if (_childs.Count > 0)
            {
                foreach (T one in ChildSites)
                {
                    if (bFirstInit)
                        one.InitContainer(this);
                    one.InitCarrier(_carrier);
                }
            }
            if (bFirstInit)
            {
                BxXmlUIItem xmlItem = StaticXmlItem;
                if (xmlItem != null)
                {
                    _childs.ForEach(x => x.InitStaticUIConfig(xmlItem));
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
        }

        public virtual void AppendRange(int size)
        {
            _childs.AddRange(CreateElements(size));
        }
        public void Insert(int index)
        {
            _childs.Insert(index, CreateElement());
        }
        public virtual void InsertRange(Int32 index, Int32 size)
        {
            _childs.InsertRange(index, CreateElements(size));
        }
        public void Remove(int index)
        {
            _childs.RemoveAt(index);
        }
        public virtual void RemoveRange(int index, int size)
        {
            _childs.RemoveRange(index, size);
        }
        public void RemoveAll()
        {
            _childs.RemoveRange(0, _childs.Count);
        }
        #endregion

        public T AppendEx()
        {
            T val = CreateElement();
            _childs.Add(val);
            return val;
        }

        protected T CreateElement()
        {
            T ele = new T();
            ele.InitContainer(this);
            ele.InitCarrier(_carrier);
            ele.InitStaticUIConfig(_staticItem);
            return ele;
        }
        protected T[] CreateElements(int size)
        {
            T[] eles = new T[size];
            for (int i = 0; i < eles.Length; i++)
            {
                eles[i] = new T();
                eles[i].InitContainer(this);
                eles[i].InitCarrier(_carrier);
                eles[i].InitStaticUIConfig(_staticItem);
            }
            return eles;
        }

        protected void InitSubElement(T ele)
        {
            ele.InitContainer(this);
            ele.InitCarrier(_carrier);
            ele.InitStaticUIConfig(_staticItem);
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
        }
        public void RemoveAt(int index)
        {
            _childs.RemoveAt(index);
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
        }
        public void Clear()
        {
            _childs.Clear();
        }
        public bool Contains(T item)
        {
            return _childs.Contains(item);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
             _childs.CopyTo(array,arrayIndex);
        }
        public bool IsReadOnly
        {
            get { return false; }
        }
        public bool Remove(T item)
        {
            return _childs.Remove(item);
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
            return (_childs as IList).Add(value);
        }
        void IList.Clear()
        {
            (_childs as IList).Clear();
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
        }
        void IList.RemoveAt(int index)
        {
            (_childs as IList).RemoveAt(index);
        }
        object IList.this[int index]
        {
            get{return this[index];}
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
    }

}
