using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace OPT.PEOffice6.BaseLayer.Base
{
    public abstract class BxElementBase : IBxElementEx, IBxUIConfigableEx, IBxPersistStorageNode
    {
        protected IBxElementCarrier _carrier;
        protected IBxElement _container;
        protected Int32 _idInCarrier;
        BxXmlUIItem _staticItem;
        BxUIConfigItemEx _config;
        string _xmlFileID;
        Int32? _configItemID;

        // protected BxElementBase() { _carrier = null; _container = null; _config = null; }
        public BxElementBase(IBxElement container, IBxElementCarrier carrier)
        {
            _container = container;
            _carrier = carrier;
            if (_carrier != null)
                _idInCarrier = _carrier.ManageElement(this);
            _config = null;
        }

        #region method
        public void InitConfig(BxXmlUIItem staticItem) 
        {
            _staticItem = staticItem;
            if (_config != null)
                _config.XmlItem = _staticItem;
        }
        BxUIConfigItemEx GetUIConfigItemEx()
        {
            if (_config == null)
            {
                if (_staticItem != null)
                {
                    _config = new BxUIConfigItemEx(_staticItem);
                }
                else
                {
                    if ((_xmlFileID != null) && (_configItemID != null))
                    {
                        BxXmlUIItem item = new BxXmlUIItem(_configItemID.Value, _xmlFileID, _carrier.UIConfigProvider);
                        _config = new BxUIConfigItemEx(item);
                    }
                }
            }
            return _config;
        }
        #endregion

        #region IBxElement 成员
        public IBxElementCarrier Carrier { get { return _carrier; } }
        public IBxElement ParentElement { get { return _container; } }
        #endregion

        #region IBxElementEx 成员
        public int IDInCarrier { get { return _idInCarrier; } }
        #endregion

        #region IBxUIConfigable 成员
        public IBxUIConfig UIConfig { get { return GetUIConfigItemEx(); } }
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
        #endregion

        #region IBxPersistStorageNode 成员
        public virtual void SaveStorageNode(IBxStorageNode node)
        {
            if ((_config == null) && _config.NeedSave())
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


    }


}
