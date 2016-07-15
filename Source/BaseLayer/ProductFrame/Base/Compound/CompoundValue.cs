using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxCompoundValue : BxElementValueBase, IBxCompound, IBxElementInit
    {
        BxCompoundCore _core;
        protected IBxElementCarrier _carrier;

        protected BxCompoundCore Core
        {
            get
            {
                if (_core == null)
                    _InitCore();
                return _core;
            }
        }
        public override IBxElementCarrier Carrier { get { return _carrier; } }

        public BxCompoundValue()
        {
            _carrier = null;
            _InitCore();
            _InitSubElements();
        }
        //public BxCompoundValue(IBxElementSite site)
        //{
        //    _site = site;
        //    _carrier = site.Carrier;
        //    _InitCore();
        //    _InitSubElements();
        //}
        public BxCompoundValue(IBxElementCarrier carrier)
        {
            _carrier = carrier;
            _InitCore();
            _InitSubElements();
        }

        private void _InitCore()
        {
            Type type = this.GetType();
            while (type != typeof(BxCompoundValue))
            {
                object[] attribs = type.GetCustomAttributes(typeof(BxCompoundAttribute), false);
                if (attribs.Length > 0)
                {
                    _core = ((BxCompoundAttribute)attribs[0]).Core;
                    return;
                }
            }
        }
        private void _InitSubElements()
        {
            BxCompoundCore core = Core;
            IEnumerable<FieldInfo> fieldsInfo = core.GetAllFieldsInfo();
            foreach (FieldInfo one in fieldsInfo)
            {
                object field = one.GetValue(this);
                if (field is IBxElementSiteInit)
                {
                    if (_carrier != null)
                        (field as IBxElementSiteInit).InitCarrier(_carrier);
                    (field as IBxElementSiteInit).InitFieldInfo(this, one);
                }
            }
        }

        #region IBxElementInit 成员
        //public void InitSite(IBxElementSiteEx site)
        //{
        //    _site = site;
        //}
        public virtual void InitCarrier(IBxElementCarrier carrier)
        {
            _carrier = carrier;
            BxCompoundCore core = Core;
            IEnumerable<FieldInfo> fieldsInfo = core.GetAllFieldsInfo();
            foreach (FieldInfo one in fieldsInfo)
            {
                object field = one.GetValue(this);
                if (field is IBxElementSiteInit)
                {
                    (field as IBxElementSiteInit).InitCarrier(_carrier);
                }
            }
        }
        #endregion


        #region IBxCompound 成员
        public IEnumerable<IBxElementSite> ChildSites { get { return new Enumerable(this); } }
        #endregion

        public class Enumerable : IEnumerable<IBxElementSite>
        {
            BxCompoundValue _obj;
            public Enumerable(BxCompoundValue obj)
            {
                _obj = obj;
            }
            public IEnumerator<IBxElementSite> GetEnumerator()
            {
                return new Enumerator(_obj);
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(_obj);
            }
        }
        public class Enumerator : IEnumerator<IBxElementSite>
        {
            IEnumerator<FieldInfo> _fieldsInfo;
            BxCompoundValue _obj;
            public Enumerator(BxCompoundValue obj)
            {
                _obj = obj;
                _fieldsInfo = _obj._core.GetAllFieldsInfo().GetEnumerator();
            }
            public IBxElementSite Current
            {
                get { return _fieldsInfo.Current.GetValue(_obj) as IBxElementSite; }
            }
            public void Dispose() { }
            object IEnumerator.Current
            {
                get { return _fieldsInfo.Current.GetValue(_obj); }
            }
            public bool MoveNext()
            {
                return _fieldsInfo.MoveNext();
            }
            public void Reset()
            {
                _fieldsInfo.Reset();
            }
        }

        #region IBxPersistStorageNode 成员
        public override void SaveStorageNode(IBxStorageNode node)
        {
            IEnumerable<IBxElementSite> elements = ChildSites;
            IBxStorageNode subNode;
            foreach (IBxElementSite one in elements)
            {
                if (one is IBxPersistStorageNode)
                {
                    subNode = node.CreateChildNode(BxStorageLable.nodeEle);
                    (one as IBxPersistStorageNode).SaveStorageNode(subNode);
                }
            }
        }
        public override void LoadStorageNode(IBxStorageNode node)
        {
            IEnumerable<IBxElementSite> elements = ChildSites;
            IEnumerable<IBxStorageNode> childs = node.ChildNodes;
            //List<IBxStorageNode> subNodes = new List<IBxStorageNode>();
            //foreach (IBxStorageNode n in childs)
            //{
            //    subNodes.Add(n);
            //}
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
    }

}
