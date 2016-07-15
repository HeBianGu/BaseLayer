using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxStorageNode : IBxStorageNode
    {
        XmlElement _node;
        IBxStorage _storage;

        public BxStorageNode(XmlElement node, IBxStorage storage)
        {
            _node = node;
            _storage = storage;
        }

        #region IBxStorageNode 成员
        public string Name { get { return _node.Name; } }
        public IBxStorage Storage { get { return _storage; } }
        //public IBxStorageNode ParentNode { get { return _parent; } }
        public IEnumerable<IBxStorageElement> Elements
        {
            get
            {
                XmlAttributeCollection attributes = _node.Attributes;
                BxStorageElement[] eles = new BxStorageElement[attributes.Count];
                int i = 0;
                foreach (XmlAttribute one in attributes)
                {
                    eles[i] = new BxStorageElement(one.Name, one.Value);
                }
                return eles;
            }
        }
        public IEnumerable<IBxStorageNode> ChildNodes
        {
            get
            {
                XmlNodeList nodes = _node.ChildNodes;
                BxStorageNode[] childs = new BxStorageNode[nodes.Count];
                int i = 0;
                foreach (XmlElement one in nodes)
                {
                    childs[i] = new BxStorageNode(one, _storage);
                    i++;
                }
                return childs;
            }
        }
        public IBxStorageNode CreateChildNode(string name)
        {
            XmlElement child = _node.OwnerDocument.CreateElement(name);
            _node.AppendChild(child);
            return new BxStorageNode(child, _storage);
        }
        public IBxStorageNode GetChildNode(string name)
        {
            XmlElement child = _node.SelectSingleNode(name) as XmlElement;
            if (child == null)
                return null;
            return new BxStorageNode(child, _storage);
        }
        public bool HasChildNode(string name)
        {
            XmlElement child = _node.SelectSingleNode(name) as XmlElement;
            return child != null;
        }
        public void RemoveChildNode(string name)
        {
            XmlNode child = _node.SelectSingleNode(name);
            if (child == null)
                return;
            _node.RemoveChild(child);
        }
        public void RemoveChildNode(IBxStorageNode node)
        {
            XmlNode child = null;
            if (node is BxStorageNode)
                child = (node as BxStorageNode)._node;
            else
                child = _node.SelectSingleNode(node.Name);
            if (child == null)
                return;
            _node.RemoveChild(child);
        }
        public void RemoveAllChildNode()
        {
            List<XmlNode> childs = new List<XmlNode>((IEnumerable<XmlNode>)_node.ChildNodes);
            foreach (XmlNode one in childs)
            {
                _node.RemoveChild(one);
            }
        }
        public void SetElement(string name, string value)
        {
            _node.SetAttribute(name, value);
        }
        public IBxStorageElement GetElement(string name)
        {
            if (_node.HasAttribute(name))
                return new BxStorageElement(name, _node.GetAttribute(name));
            return null;
        }
        public string GetElementValue(string name)
        {
            if (_node.HasAttribute(name))
                return _node.GetAttribute(name);
            return null;
        }
        public bool HasElement(string name)
        {
            return _node.HasAttribute(name);
        }
        public void RemoveElement(string name)
        {
            _node.RemoveAttribute(name); ;
        }
        public void RemoveAllElement()
        {
            _node.RemoveAllAttributes();
        }
        #endregion
    }
}
