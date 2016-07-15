using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public abstract class BxElementSiteBase1 : BxElementEvent,
        IBxElementSiteEx, IBxPersistStorageNode, IBxElementSiteInit, IBxElementSiteVertionType
    {
        protected IBxCompound _container;
        protected BxUIConfigFromSuic _config;
        protected IBxStaticUIConfigPregnant _suicPregnant;
        protected IBxElementCarrier _carrier = null;
        protected string _svt = BxSiteVerType.Normal;


        public BxUIConfigFromSuic Config { get { return GetUIConfigItemEx(); } }
        protected virtual bool IsRefer { get { return this is IBxElementReferer; } }
        public IBxStaticUIConfigPregnant SUICPregnant { get { return _suicPregnant; } }
        public BxXmlUIItem StaticUIConfig
        {
            get
            {
                if (_suicPregnant != null)
                    return _suicPregnant.StaticUIConfig;
                return null;
            }
        }


        public BxElementSiteBase1()
        {
            _container = null;
            _config = null;
            _suicPregnant = null;
        }
        //public BxElementSiteBase1(IBxCompound container, IBxElementCarrier carrier)
        //{
        //    _container = container;
        //    _carrier = carrier;
        //    if (_carrier != null)
        //        _idInCarrier = _carrier.ManageElement(this);
        //    _config = null;
        //}

        public void CreateEmptyConfig(string id)
        {
            _config = new BxUIConfigFromSuic(id);
        }

        public void SetConfig(BxUIConfigFromSuic config)
        {
            _config = config;
        }

        #region method
        //public void InitConfigID(string xmlFileID, Int32 itemID)
        //{
        //    _xmlFileID = xmlFileID;
        //    _configItemID = itemID;
        //    if (_config != null)
        //    {
        //        _staticItem = new BxXmlUIItem(_configItemID.Value, _xmlFileID, Carrier.UIConfigProvider);
        //        _config.XmlItem = _staticItem;
        //    }
        //    else
        //    {
        //        _staticItem = null;
        //    }
        //}

        public BxUIConfigFromSuic GetUIConfigItemEx()
        {
            if ((_config == null) && (_suicPregnant != null) && !string.IsNullOrEmpty(_suicPregnant.ConfigID))
            {
                _config = new BxUIConfigFromSuic(_carrier, _suicPregnant);
            }
            return _config;
        }

        //public BxXmlUIItem GetStaticXmlUIItem()
        //{
        //    if ((_staticItem == null) && (_xmlFileID != null) && (_configItemID != null))
        //    {
        //        _staticItem = new BxXmlUIItem(_configItemID.Value, _xmlFileID, Carrier.UIConfigProvider);
        //    }
        //    return _staticItem;
        //}
        #endregion

        #region IBxElementSite 成员
        public virtual IBxCompound Container { get { return _container; } }
        public abstract IBxElement Element { get; }
        public IBxUIConfig UIConfig { get { return GetUIConfigItemEx(); } }
        #endregion

        #region IBxElementSiteInit 成员
        public virtual void InitContainer(IBxCompound container)
        {
            _container = container;
        }
        public virtual void ResetCarrier(IBxElementCarrier carrier)
        {
            //由父节点负责搞定，这里不再需要
            //if (_suicPregnant != null)
            //     _suicPregnant.InitSUICProvider(suicProvider);

            if (_carrier == carrier)
                return;
            _carrier = carrier;

            if (_config != null)
                _config.ResetCarrier(carrier);

            if (Element is IBxElementInit)
                (Element as IBxElementInit).ResetCarrier(carrier);
        }
        public virtual void InitSUICPregnant(IBxStaticUIConfigPregnant suicPregnant)
        {
            _suicPregnant = suicPregnant;
            if (_config != null)
                _config.ResetSUICPregnant(suicPregnant);
        }
        #endregion

        public string VertionType { get; set; }
        public string Version { get; set; }


        #region IBxPersistStorageNode 成员
        public virtual void SaveStorageNode(IBxStorageNode node)
        {
            if ((_config != null) && _config.NeedSave())
            {
                IBxStorageNode cfgNode = node.CreateChildNode(BxStorageLable.nodeConfig);
                _config.SaveStorageNode(cfgNode);

                IBxStorageNode valueNode = node.CreateChildNode(BxStorageLable.nodeValue);
                SaveElement(valueNode);
            }
            else
            {
                SaveElement(node);
            }
        }
        public virtual void LoadStorageNode(IBxStorageNode node)
        {
            IBxStorageNode cfgNode = node.GetChildNode(BxStorageLable.nodeConfig);
            if (cfgNode != null)
            {
                GetUIConfigItemEx().LoadStorageNode(cfgNode);

                IBxStorageNode valueNode = node.GetChildNode(BxStorageLable.nodeValue);
                LoadElement(valueNode);
            }
            else
            {
                LoadElement(node);
            }
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
                if (element is IBxPersistStorageNode)
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
    }


}
