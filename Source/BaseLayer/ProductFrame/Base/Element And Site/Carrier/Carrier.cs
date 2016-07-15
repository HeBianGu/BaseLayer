using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using OPT.Product.BaseInterface;
using System.Globalization;

namespace OPT.Product.Base
{
    //public class BxModifyInfo : IBxModifyInfo
    //{
    //    bool _modified = false;

    //    #region IBxModifyInfo 成员
    //    public bool Modified
    //    {
    //        get { return _modified; }
    //        set { _modified = value; }
    //    }
    //    #endregion
    //}

    public class BxCarrier : IBxElementCarrier, IBxPersistStorageNode, IBxPersistXmlNode
    {
        BxModifiedManager _modifyInfo = new BxModifiedManager();

        public BxCarrier()
        {
            InitElements();
        }

        protected void InitElements()
        {
            FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            object temp;
            foreach (FieldInfo one in fields)
            {
                if (one.GetCustomAttributes(typeof(BxCarrierElement), false).Length > -1)
                {
                    temp = one.GetValue(this);
                    if (temp is IBxElementInit)
                        (temp as IBxElementInit).ResetCarrier(this);

                    IBxElementInit_ForOldCode ele = temp as IBxElementInit_ForOldCode;
                    if (ele != null)
                        ele.OldInit();
                }
            }
        }

        #region IBxElementCarrier 成员
        public IBxUIConfigProvider UIConfigProvider
        {
            get { return BxSystemInfo.Instance.UIConfigProvider; }
        }

        public int ManageElement(IBxElementSite element)
        {
            return -1;
        }
        public void RemoveElement(IBxElementSite element)
        {
            //TODO :RemoveElement
        }
        public IBxElementSite GetElement(int id)
        {
            //TODO:GetElement
            return null;
        }
        #endregion

        #region IBxPersistStorageNode 成员
        public void SaveStorageNode(IBxStorageNode node)
        {
            FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            IBxStorageNode subNode;
            foreach (FieldInfo one in fields)
            {
                if (one.GetCustomAttributes(typeof(BxCarrierElement), false).Length > -1)
                {
                    IBxPersistStorageNode ele = one.GetValue(this) as IBxPersistStorageNode;
                    subNode = node.CreateChildNode(BxStorageLable.nodeEle);
                    ele.SaveStorageNode(subNode);
                }
            }
        }
        public void LoadStorageNode(IBxStorageNode node)
        {
            FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            IEnumerable<IBxStorageNode> childs = node.ChildNodes;
            using (IEnumerator<IBxStorageNode> itor = childs.GetEnumerator())
            {
                itor.Reset();
                foreach (FieldInfo one in fields)
                {
                    if (one.GetCustomAttributes(typeof(BxCarrierElement), false).Length > -1)
                    {
                        IBxPersistStorageNode ele = one.GetValue(this) as IBxPersistStorageNode;
                        if (!itor.MoveNext())
                            break;
                        ele.LoadStorageNode(itor.Current);
                    }
                }
            }
        }
        #endregion

        #region IBxPersistXmlNode 成员
        public void SaveXml(System.Xml.XmlElement node)
        {
            BxStorage stg = new BxStorage();
            SaveStorageNode(stg.RootNode);
            stg.SaveXml(node);
        }
        public void LoadXml(System.Xml.XmlElement node)
        {
            BxStorage stg = new BxStorage();
            stg.LoadXml(node);
            LoadStorageNode(stg.RootNode);
        }
        #endregion


        #region IBxElementCarrier 成员
        public virtual IBxStaticUIConfigProvider SCICProvider
        {
            get { return BxSystemInfo.Instance.SUICProvider; }
        }
        public void AddModified(IBxModifyManage one)
        {
            _modifyInfo.Add(one);
        }
        public void RemoveModified(IBxModifyManage one)
        {
            _modifyInfo.Remove(one);
        }
        #endregion

        #region IBxModifyInfo 成员
        public bool Modified { get { return _modifyInfo.Modified; } }
        public void ResetModifyFlag()
        {
            _modifyInfo.Reset();
        }
        #endregion

    }

    public class BxModifiedManager
    {
       // List<IBxModifyManage> _modifiedList = new List<IBxModifyManage>();
        bool _modified = false;

        public bool Modified
        {
           // get { return _modifiedList.Count != 0; }
            get { return _modified; }
        }

        public void Add(IBxModifyManage one)
        {
            _modified = true;
            //if (_modifiedList.Contains(one))
            //    return;
            //_modifiedList.Add(one);
        }

        public void Remove(IBxModifyManage one)
        {
            //if (_modifiedList.Contains(one))
            //    return;
            //_modifiedList.Add(one);
        }

        public void Reset()
        {
            //_modifiedList.ForEach(x => x.ResetModifyFlag());
            //_modifiedList.Clear();
            _modified = false;
        }
    }

}
