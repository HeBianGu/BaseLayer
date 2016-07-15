using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxCompound : BxReferableElementBase, IBxCompound, IBxElementInit
    {
        BxCompoundCore _core;

        protected BxCompoundCore Core
        {
            get
            {
                if (_core == null)
                    _InitCore();
                return _core;
            }
        }

        public BxCompound()
        {
            _carrier = null;
            //_InitCore();
            _InitSubElements();
        }
        public BxCompound(IBxElementCarrier carrier)
        {
            _carrier = carrier;
            _InitSubElements();
            //InitCarrier(carrier);
        }

        private void _InitCore()
        {
            Type type = this.GetType();
            while (type != typeof(BxCompound))
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
                    //if (_carrier != null)
                    //    (field as IBxElementSiteInit).InitCarrier(_carrier);
                    (field as IBxElementSiteInit).InitFieldInfo(this, one);
                }
            }
        }

        #region IBxElementInit 成员
        public virtual void InitCarrier(IBxElementCarrier carrier)
        {
            if(carrier == null)
                throw new NullReferenceException();
            if (_carrier == carrier) 
                return;
            bool bFirstInit = (_carrier == null);
            _carrier = carrier;


            BxCompoundCore core = Core;
            IEnumerable<FieldInfo> fieldsInfo = core.GetAllFieldsInfo();
            IBxElementSiteInit temp = null;
            foreach (FieldInfo one in fieldsInfo)
            {
                object field = one.GetValue(this);
                if (field is IBxElementSiteInit)
                {
                    temp = field as IBxElementSiteInit;
                    if (bFirstInit)
                        temp.InitFieldInfo(this, one);
                    temp.InitCarrier(_carrier);
                }
            }
            if (bFirstInit)
                OnInit();
        }
        #endregion

        protected virtual void OnInit()
        {
        }



        #region IBxCompound 成员
        public IEnumerable<IBxElementSite> ChildSites { get { return new Enumerable(this); } }
        #endregion

        public class Enumerable : IEnumerable<IBxElementSite>
        {
            BxCompound _obj;
            public Enumerable(BxCompound obj)
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
            BxCompound _obj;
            public Enumerator(BxCompound obj)
            {
                _obj = obj;
                _fieldsInfo = _obj._core.GetAllFieldsInfo().GetEnumerator();
            }
            public IBxElementSite Current
            {
                get { return _fieldsInfo.Current.GetValue(_obj) as IBxElementSite; }
            }
            public void Dispose() { _fieldsInfo.Dispose(); }
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
