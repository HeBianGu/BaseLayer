using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxCompound : BxReferableElementBase, IBxCompound, IBxElementInit, IBxElementInit_ForOldCode
    {
        BxCompoundCore _core;
        BxCompoundInstanceData _instanceFields = null;
        string _version = "_invalid";

        string Version
        {
            get
            {
                if (_version == "_invalid")
                    _InitCore();
                return _version;
            }
        }

        protected BxCompoundCore Core
        {
            get
            {
                if (_core == null)
                    _InitCore();
                return _core;
            }
        }
        protected BxCompoundInstanceData InstanceFields
        {
            get { return _instanceFields; }
        }

        public BxCompound()
        {
            _InitSubElements();
        }

        private void _InitCore()
        {
            Type type = this.GetType();
            while (type != typeof(BxCompound))
            {
                object[] attribs = type.GetCustomAttributes(typeof(BxCompoundAttribute), false);
                if (attribs.Length > 0)
                {
                    BxCompoundAttribute a = (BxCompoundAttribute)attribs[0];
                    _core = a.Core;
                    _version = a.Version;
                    return;
                }
            }
        }
        private void _InitSubElements()
        {
            BxCompoundCore core = Core;
            IEnumerable<BxCompoundCoreFieldData> fieldsInfo = core.GetAllFieldsInfo();
            foreach (BxCompoundCoreFieldData one in fieldsInfo)
            {
                object field = one.FieldInfo.GetValue(this);
                if (field is IBxElementSiteInit)
                {
                    (field as IBxElementSiteInit).InitContainer(this);
                    (field as IBxElementSiteInit).InitSUICPregnant(one);
                }
                if (field is IBxElementSiteVertionType)
                {
                    (field as IBxElementSiteVertionType).VertionType = one.VersionType;
                    (field as IBxElementSiteVertionType).Version = one.Version;
                }
            }
        }

        #region IBxElementInit 成员
        public override void ResetCarrier(IBxElementCarrier carrier)
        {
            if (_carrier == carrier)
                return;
            _carrier = carrier;
            IBxStaticUIConfigProvider suicProvider = (carrier == null) ? null : carrier.SCICProvider;

            if (suicProvider == null)
            {
                _instanceFields = null;

                IEnumerable<BxCompoundCoreFieldData> fieldsInfo = Core.GetAllFieldsInfo();
                foreach (BxCompoundCoreFieldData one in fieldsInfo)
                {
                    object field = one.FieldInfo.GetValue(this);
                    if (field is IBxElementSiteInit)
                    {
                        (field as IBxElementSiteInit).ResetCarrier(carrier);
                        (field as IBxElementSiteInit).InitSUICPregnant(one);
                    }
                    if (field is IBxElementSiteVertionType)
                    {
                        (field as IBxElementSiteVertionType).VertionType = one.VersionType;
                        (field as IBxElementSiteVertionType).Version = one.Version;
                    }
                }
            }
            else
            {
                if (_instanceFields == null)
                    _instanceFields = new BxCompoundInstanceData(suicProvider, Core);
                else
                    _instanceFields.ResetSUICProvider(suicProvider);

                foreach (BxCompoundFieldData one in InstanceFields.InstanceFields)
                {
                    object field = one.FieldInfo.GetValue(this);
                    if (field is IBxElementSiteInit)
                    {
                        (field as IBxElementSiteInit).ResetCarrier(carrier);
                        (field as IBxElementSiteInit).InitSUICPregnant(one);
                    }
                    if (field is IBxElementSiteVertionType)
                    {
                        (field as IBxElementSiteVertionType).VertionType = one.VersionType;
                    }
                }
            }
        }
        #endregion

        #region 兼容过去的代码
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
        #endregion


        #region IBxCompound 成员
        public IEnumerable<IBxElementSite> ChildSites { get { return new Enumerable(this); } }
        public IEnumerable<IBxElementSite> DeclaredOnlyChildSites { get { return new DeclaredOnlyEnumerable(this); } }
        #endregion


        #region Enumerable
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
            IEnumerator<BxCompoundCoreFieldData> _fieldsInfo;
            BxCompound _obj;
            public Enumerator(BxCompound obj)
            {
                _obj = obj;
                _fieldsInfo = _obj._core.GetAllFieldsInfo().GetEnumerator();
            }
            public IBxElementSite Current
            {
                get { return _fieldsInfo.Current.FieldInfo.GetValue(_obj) as IBxElementSite; }
            }
            public void Dispose() { _fieldsInfo.Dispose(); }
            object IEnumerator.Current
            {
                get { return _fieldsInfo.Current.FieldInfo.GetValue(_obj); }
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

        public class DeclaredOnlyEnumerable : IEnumerable<IBxElementSite>
        {
            BxCompound _obj;
            public DeclaredOnlyEnumerable(BxCompound obj)
            {
                _obj = obj;
            }
            public IEnumerator<IBxElementSite> GetEnumerator()
            {
                return new DeclaredOnlyEnumerator(_obj);
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return new DeclaredOnlyEnumerator(_obj);
            }
        }
        public class DeclaredOnlyEnumerator : IEnumerator<IBxElementSite>
        {
            IEnumerator<BxCompoundCoreFieldData> _fieldsInfo;
            BxCompound _obj;
            public DeclaredOnlyEnumerator(BxCompound obj)
            {
                _obj = obj;
                _fieldsInfo = (IEnumerator<BxCompoundCoreFieldData>)_obj._core.GetFieldsInfo(true).GetEnumerator();
            }
            public IBxElementSite Current
            {
                get { return _fieldsInfo.Current.FieldInfo.GetValue(_obj) as IBxElementSite; }
            }
            public void Dispose() { _fieldsInfo.Dispose(); }
            object IEnumerator.Current
            {
                get { return _fieldsInfo.Current.FieldInfo.GetValue(_obj); }
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
        #endregion

        #region IBxPersistStorageNode 成员
        //public void OnSaveStorageNode(IBxStorageNode node)
        //{
        //    IEnumerable<IBxElementSite> elements = ChildSites;
        //    IBxStorageNode subNode;
        //    foreach (IBxElementSite one in elements)
        //    {
        //        if (one is IBxPersistStorageNode)
        //        {
        //            subNode = node.CreateChildNode(BxStorageLable.nodeEle);
        //            (one as IBxPersistStorageNode).SaveStorageNode(subNode);
        //        }
        //    }
        //}

        //public override void SaveStorageNode(IBxStorageNode node)
        //{
        //    List<BxCompoundCore> list = Core.InheritanceList;
        //    foreach (BxCompoundCore one in list)
        //    {
        //        MethodInfo info = one.CompoundType.GetMethod("OnSaveStorageNode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        //        if (info == null)
        //        {
        //            OnSaveStorageNode(node);
        //        }
        //        else
        //        {
        //            info.Invoke(this, new object[] { node });
        //        }
        //    }
        //}

        public void SaveStorageNodeSameVersion(IBxStorageNode node)
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

        public override void SaveStorageNode(IBxStorageNode node)
        {
            string curVersion = Version;
            if (!string.IsNullOrEmpty(curVersion))
            {
                BxVersionHelp.SetVersion(node, curVersion);
            }
            SaveStorageNodeSameVersion(node);
        }

        public void LoadStorageNodeSameVersion(IBxStorageNode node)
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
        public override void LoadStorageNode(IBxStorageNode node)
        {
            string curVersion = Version;
            string ver = BxVersionHelp.GetVersion(node);
            if (string.IsNullOrEmpty(curVersion) || curVersion == ver)
            {
                LoadStorageNodeSameVersion(node);
                return;
            }

            IEnumerable<IBxElementSite> elements = ChildSites;
            IEnumerable<IBxStorageNode> childs = node.ChildNodes;
            using (IEnumerator<IBxStorageNode> itor = childs.GetEnumerator())
            {
                itor.Reset();
                string svt = null;
                string eleVersion = null;
                foreach (IBxElementSite one in elements)
                {
                    if (one is IBxPersistStorageNode)
                    {
                        if (one is IBxElementSiteVertionType)
                        {
                            svt = (one as IBxElementSiteVertionType).VertionType;
                            eleVersion = (one as IBxElementSiteVertionType).Version;
                        }
                        else
                            svt = null;

                        if (!string.IsNullOrEmpty(svt))
                        {
                            if (svt == BxSiteVerType.Insert && string.Compare(eleVersion,ver) > 0)
                            {
                                continue;
                            }
                            else if (svt == BxSiteVerType.Placeholder)
                            {
                                if (!itor.MoveNext())
                                    break;
                            }
                            else
                            {
                            }
                        }
                        
                        {
                            if (!itor.MoveNext())
                                break;
                            (one as IBxPersistStorageNode).LoadStorageNode(itor.Current);
                        }
                    }
                }
            }
        }
        #endregion
    }

}
