using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public abstract class BxElementSiteBase1 : BxElementEvent,
        IBxElementSiteEx, IBxPersistStorageNode, IBxElementSiteInit
    {
        protected IBxCompound _container;
        protected IBxElementCarrier _carrier;
        protected Int32 _idInCarrier = -1;
        BxUIConfigItemEx _config;
        protected BxXmlUIItem _staticItem = null;
        string _xmlFileID = null;
        Int32? _configItemID = null;

        public BxUIConfigItemEx Config { get { return GetUIConfigItemEx(); } }
        protected virtual bool IsRefer { get { return this is IBxElementReferer; } }

        public BxElementSiteBase1()
        {
            _container = null;
            _carrier = null;
            if (_carrier != null)
                _idInCarrier = _carrier.ManageElement(this);
            _config = null;
        }
        public BxElementSiteBase1(IBxCompound container, IBxElementCarrier carrier)
        {
            _container = container;
            _carrier = carrier;
            if (_carrier != null)
                _idInCarrier = _carrier.ManageElement(this);
            _config = null;
        }

        public void CreateEmptyConfig(int id)
        {
            _config = new BxUIConfigItemEx();
            _config.ID = id;
        }

        #region method
        public void InitConfigID(string xmlFileID, Int32 itemID)
        {
            _xmlFileID = xmlFileID;
            _configItemID = itemID;
            if (_config != null)
            {
                _staticItem = new BxXmlUIItem(_configItemID.Value, _xmlFileID, Carrier.UIConfigProvider);
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
                    _staticItem = new BxXmlUIItem(_configItemID.Value, _xmlFileID, Carrier.UIConfigProvider);
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
                    if ((Carrier != null) && (_xmlFileID != null) && (_configItemID != null))
                    {
                        _staticItem = new BxXmlUIItem(_configItemID.Value, _xmlFileID, Carrier.UIConfigProvider);
                        _config = new BxUIConfigItemEx(_staticItem);
                    }
                }
            }
            return _config;
        }

        public BxXmlUIItem GetStaticXmlUIItem()
        {
            if ( (_staticItem == null) && (_xmlFileID != null) && (_configItemID != null))
            {
                _staticItem = new BxXmlUIItem(_configItemID.Value, _xmlFileID, Carrier.UIConfigProvider);
            }
            return _staticItem;
        }
        #endregion

        #region IBxElementSite 成员
        public virtual IBxCompound Container { get { return _container; } }
        public abstract IBxElement Element { get; }
        public IBxUIConfig UIConfig { get { return GetUIConfigItemEx(); } }
        #endregion

        #region IBxElementSiteEx 成员
        public IBxElementCarrier Carrier
        {
            get
            {
                if (_carrier == null && _container != null)
                    _carrier = (_container as IBxElementEx).Carrier;
                return _carrier;
            }
            set { _carrier = value; }

        }
        public int IDInCarrier { get { return _idInCarrier; } }
        #endregion

        #region IBxElementSiteInit 成员
        public virtual void InitCarrier(IBxElementCarrier carrier)
        {
            _carrier = carrier;
            if (_staticItem != null)
                _staticItem.InitProvider(_carrier.UIConfigProvider);

            if (Element is IBxElementInit)
                (Element as IBxElementInit).InitCarrier(carrier);
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

            SaveElement(node);
        }
        public virtual void LoadStorageNode(IBxStorageNode node)
        {
            IBxStorageNode cfgNode = node.GetChildNode(BxStorageLable.nodeConfig);
            if (cfgNode == null)
                return;
            GetUIConfigItemEx().LoadStorageNode(cfgNode);

            LoadElement(node);
        }
        #endregion

        protected virtual void SaveElement(IBxStorageNode node)
        {
            IBxElement element = Element;
            bool needShare = (element is IBxReferableElement) && (IsRefer || (element as IBxReferableElement).HasReferer);

            if (needShare)
            {
                string id = node.Storage.SVA.SaveValue(element as IBxReferableElement);
                node.SetElement(BxStorageLable.elementValueID, id);
            }
            else
            {
                (element as IBxPersistStorageNode).SaveStorageNode(node);
            }
        }
        protected virtual void LoadElement(IBxStorageNode node)
        {
            string id = node.GetElementValue(BxStorageLable.elementValueID);
            if (string.IsNullOrEmpty(id))
            {
                (Element as IBxPersistStorageNode).LoadStorageNode(node);
            }
            else
            {
                if (IsRefer)
                {
                    node.Storage.SVA.RequestRestoreRefer(this as IBxElementReferer, id);
                }
                else
                {
                    node.Storage.SVA.LoadValue(Element as IBxReferableElement, id);
                }
            }
        }

        void InitCarrier()
        {
            if (_container != null)
                _carrier = (_container as IBxElementEx).Carrier;
        }

    }


}
