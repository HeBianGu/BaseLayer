using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxStorage : IBxStorage, IBxPersistXmlNode
    {
        BxStorageNode _stg = null;
        XmlElement _xmlRoot = null;
        IBxStorageNode _elementsNode = null;
        BxSVA _sva = null;

        public BxStorage()
        {
            New();
        }

        public void SaveXml(XmlElement node)
        {
            Save();

            XmlElement stgNode = node.OwnerDocument.CreateElement(BxSSL.nodeStg);
            node.AppendChild(stgNode);
            string xml = _xmlRoot.InnerXml;
            node.InnerXml = xml;
        }

        public void LoadXml(XmlElement node)
        {
            Load(node);
        }
        public void SaveToXmlFile(string filePath)
        {
            Save();
            _xmlRoot.OwnerDocument.Save(filePath);
        }
        public void LoadFromXmlFile(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            LoadXml(doc.DocumentElement as XmlElement);
        }

        void Save()
        {
            if (_sva != null)
                _sva.Save();
        }
        protected void New()
        {
            XmlDocument doc = new XmlDocument();
            _xmlRoot = doc.CreateElement("root");
            doc.AppendChild(_xmlRoot);
            XmlElement stg = doc.CreateElement(BxSSL.nodeStg);
            _xmlRoot.AppendChild(stg);
            _stg = new BxStorageNode(stg, this);

            _sva = new BxSVA();
            _sva.New(_stg.CreateChildNode(BxSSL.nodeSVA));

            _elementsNode = _stg.CreateChildNode(BxSSL.nodeEles);

        }
        protected void Load(XmlElement node)
        {
            _xmlRoot = node;
            _stg = new BxStorageNode(_xmlRoot.FirstChild as XmlElement, this);

            IBxStorageNode svaNode = _stg.GetChildNode(BxSSL.nodeSVA);
            if (svaNode != null)
            {
                _sva = new BxSVA();
                _sva.Load(svaNode);
            }
            _elementsNode = _stg.GetChildNode(BxSSL.nodeEles);
        }

        #region IBLStorage 成员
        public IBxStorageNode RootNode { get { return _elementsNode; } }
        public IBxSharedValueArea SVA
        {
            get
            {
                return _sva;
            }
        }
        #endregion

    }


}
