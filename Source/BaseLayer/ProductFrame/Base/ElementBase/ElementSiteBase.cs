using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public abstract class BxElementSiteBase : BxElementEvent,
        IBxElementSiteEx, IBxPersistStorageNode, IBxElementSiteInit
    {
        protected IBxElementCarrier _carrier;
        protected IBxCompound _container;
        protected Int32 _idInCarrier;
        BxUIConfigItemEx _config;
        protected BxXmlUIItem _staticItem;
        string _xmlFileID;
        Int32? _configItemID;
        //FieldInfo _info;

        public BxUIConfigItemEx Config { get { return GetUIConfigItemEx(); } }

        public BxElementSiteBase()
        {
            _container = null;
            _carrier = null;
            if (_carrier != null)
                _idInCarrier = _carrier.ManageElement(this);
            _config = null;
        }
        // protected BxElementBase() { _carrier = null; _container = null; _config = null; }
        public BxElementSiteBase(IBxCompound container, IBxElementCarrier carrier)
        {
            _container = container;
            _carrier = carrier;
            if (_carrier != null)
                _idInCarrier = _carrier.ManageElement(this);
            _config = null;
        }

        #region method
        public void InitConfigID(string xmlFileID, Int32 itemID)
        {
            _xmlFileID = xmlFileID;
            _configItemID = itemID;
            if (_config != null)
            {
                _staticItem = new BxXmlUIItem(_configItemID.Value, _xmlFileID, _carrier.UIConfigProvider);
                _config.XmlItem = _staticItem;
            }
            else
            {
                _staticItem = null;
            }
        }
        protected BxXmlUIItem GetXmlUIItem()
        {
            if (_staticItem == null)
            {
                if ((_xmlFileID != null) && (_configItemID != null))
                {
                    _staticItem = new BxXmlUIItem(_configItemID.Value, _xmlFileID, _carrier.UIConfigProvider);
                    if (_config != null)
                        _config.XmlItem = _staticItem;
                }
            }
            return _staticItem;
        }
        public BxUIConfigItemEx GetUIConfigItemEx()
        {
            if (_config == null)
            {
                if (_staticItem != null)
                {
                    _config = new BxUIConfigItemEx(_staticItem);
                }
                else
                {
                    if ((_carrier != null) && (_xmlFileID != null) && (_configItemID != null))
                    {
                        _staticItem = new BxXmlUIItem(_configItemID.Value, _xmlFileID, _carrier.UIConfigProvider);
                        _config = new BxUIConfigItemEx(_staticItem);
                    }
                }
            }
            return _config;
        }
        #endregion

        #region IBxElementSite 成员
        public virtual IBxCompound Container { get { return _container; } }
        public abstract IBxElement Element { get; }
        public IBxUIConfig UIConfig { get { return GetUIConfigItemEx(); } }
        #endregion

        #region IBxElementSiteEx 成员
        public IBxElementCarrier Carrier { get { return _carrier; } }
        public int IDInCarrier { get { return _idInCarrier; } }
        #endregion

        #region IBxElementSiteInit 成员
        public virtual void InitCarrier(IBxElementCarrier carrier)
        {
            _carrier = carrier;
            if (_staticItem != null)
                _staticItem.InitProvider(_carrier.UIConfigProvider);
        }
        public virtual void InitContainer(IBxCompound container)
        {
            _container = container;
        }
        public virtual void InitFieldInfo(IBxCompound container, FieldInfo info)
        {
            InitContainer(container);
            //_info = info;
            object[] attribs = info.GetCustomAttributes(typeof(BxSiteAttribute), false);
            if (attribs.Length > 0)
            {
                BxSiteAttribute sa = attribs[0] as BxSiteAttribute;
                InitConfigID(sa.ConfigFileID, sa.ConfigItemID);
            }
        }
        public virtual void InitStaticUIConfig(BxXmlUIItem staticItem)
        {
            _staticItem = staticItem;
            if (_config != null)
                _config.XmlItem = _staticItem;
        }
        #endregion

        #region IBxPersistStorageNode 成员
        public virtual void SaveStorageNode(IBxStorageNode node)
        {
            if ((_config != null) && _config.NeedSave())
            {
                IBxStorageNode cfgNode = node.CreateChildNode(BxStorageLable.nodeConfig);
                _config.SaveStorageNode(cfgNode);
            }
        }
        public virtual void LoadStorageNode(IBxStorageNode node)
        {
            IBxStorageNode cfgNode = node.GetChildNode(BxStorageLable.nodeConfig);
            if (cfgNode == null)
                return;
            GetUIConfigItemEx().LoadStorageNode(cfgNode);
        }
        #endregion

        //protected virtual void SaveElement(IBxStorageNode node)
        //{
        //}

        //public virtual void LoadElement(IBxStorageNode node)
        //{
        //    IBxStorageNode cfgNode = node.GetChildNode(BxStorageLable.nodeConfig);
        //    if (cfgNode == null)
        //        return;
        //    GetUIConfigItemEx().LoadStorageNode(cfgNode);
        //}
    }


}
