#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/5 18:54:48  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����XmlMananger
 *
 * ˵����
 * 
 * 
 * �޸��ߣ�           ʱ�䣺               
 * �޸�˵����
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HebianGu.ComLibModule.XML
{
    /// <summary> XML������ </summary>
    public partial class XmlHelper
    {
        private  string _xPath;
        /// <summary> xml�ļ�·�� </summary>
        public  string XmlPath
        {
            get { return _xPath; }
            set { _xPath = value; }
        }

        private  string _configName = "XmlPath";

        /// <summary> �����ļ��ڵ����ƣ���������AppSettings�ڵ��� </summary>
        public  string ConfigName
        {
            get { return _configName; }
            set { _configName = value; GetConfig(); }
        }

        /// <summary> �������ļ���ȡxml·�� </summary>
         void GetConfig()
        {
            if (string.IsNullOrEmpty(_xPath))
            {
                try
                {
                    _xPath = ConfigurationManager.AppSettings[_configName];
                }
                catch { }
            }
        }
         XmlHelper()
        {
            GetConfig();
        }


        /// <summary> ���һ���ӽڵ� </summary>
        /// <param name="xDoc">XmlDocument����</param>
        /// <param name="parentNode">���ڵ�</param>
        /// <param name="xlParameter">Xml����</param>
        private  void AppendChild(XmlDocument xDoc, XmlNode parentNode, params XmlParam[] xlParameter)
        {
            foreach (XmlParam xpar in xlParameter)
            {
                XmlNode newNode = xDoc.CreateNode(XmlNodeType.Element, xpar.Name, null);
                string ns = string.IsNullOrEmpty(xpar.NamespaceOfPrefix) ? "" : newNode.GetNamespaceOfPrefix(xpar.NamespaceOfPrefix);
                foreach (AttributeParameter attp in xpar.Attributes)
                {
                    XmlNode attr = xDoc.CreateNode(XmlNodeType.Attribute, attp.Name, ns == "" ? null : ns);
                    attr.Value = attp.Value;
                    newNode.Attributes.SetNamedItem(attr);
                }
                newNode.InnerText = xpar.InnerText;
                parentNode.AppendChild(newNode);
            }
        }
       
        /// <summary> ���һ���ӽڵ� </summary>
        /// <param name="xDoc">XmlDocument����</param>
        /// <param name="parentNode">���ڵ�</param>
        /// <param name="xlParameter">Xml����</param>
        private  void AddEveryNode(XmlDocument xDoc, XmlNode parentNode, params XmlParam[] paras)
        {
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)
            {
                if (xns.Name == parentNode.Name)
                {
                    AppendChild(xDoc, xns, paras);
                }
                else
                {
                    foreach (XmlNode xn in xns)
                    {
                        if (xn.Name == parentNode.Name)
                        {
                            AppendChild(xDoc, xn, paras);
                        }
                    }
                }
            }
        }

        /// <summary> ���һ��XmlDocument���� </summary>
         XmlDocument GetXmlDom()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(_xPath);
            return xdoc;
        }

        /// <summary> ����һ��XML�ĵ����ɹ����������·����ֱ��ָ����ļ� </summary>
        /// <param name="fileName">�ļ�����·����</param>
        /// <param name="rootNode">���������</param>
        /// <param name="elementName">Ԫ�ؽڵ�����</param>
        /// <param name="xmlParameter">XML����</param>
        public  void CreateXmlFile(string fileName, string rootNode, string elementName, params XmlParam[] xmlParameter)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xn = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(xn);
            XmlNode root = xDoc.CreateElement(rootNode);
            xDoc.AppendChild(root);
            XmlNode ln = xDoc.CreateNode(XmlNodeType.Element, elementName, null);
            AppendChild(xDoc, ln, xmlParameter);
            root.AppendChild(ln);
            try
            {
                xDoc.Save(fileName);
                _xPath = fileName;
            }
            catch
            {
                throw;
            }
        }
        /// <summary> ����һ��XML�ĵ����ɹ����������·����ֱ��ָ����ļ� </summary>
        /// <param name="fileName">�ļ�����·����</param>
        /// <param name="xmlString">xml�ַ���</param>
        public  void CreateXmlFile(string fileName, string xmlString)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.LoadXml(xmlString);
                xDoc.Save(fileName);
                _xPath = fileName;
            }
            catch { throw; }
        }

        /// <summary> ����½ڵ� </summary>
        /// <param name="parentNode">�½ڵ�ĸ��ڵ����</param>
        /// <param name="xmlParameter">Xml��������</param>
        public  void AddNewNode(XmlNode parentNode, params XmlParam[] xmlParameter)
        {
            XmlDocument xDoc = GetXmlDom();
            if (parentNode.Name == xDoc.DocumentElement.Name)
            {
                XmlNode newNode = xDoc.CreateNode(XmlNodeType.Element, xDoc.DocumentElement.ChildNodes[0].Name, null);
                AppendChild(xDoc, newNode, xmlParameter);
                xDoc.DocumentElement.AppendChild(newNode);
            }
            else
            {
                AddEveryNode(xDoc, parentNode, xmlParameter);
            }
            xDoc.Save(_xPath);
        }
        /// <summary> ����½ڵ� </summary>
        /// <param name="xDoc">XmlDocument����</param>
        /// <param name="parentName">�½ڵ�ĸ��ڵ�����</param>
        /// <param name="xmlParameter">XML��������</param>
        public  void AddNewNode(string parentName, params XmlParam[] xmlParameter)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNode parentNode = GetNode(xDoc, parentName);
            if (parentNode == null) return;
            if (parentNode.Name == xDoc.DocumentElement.Name)
            {
                XmlNode newNode = xDoc.CreateNode(XmlNodeType.Element, xDoc.DocumentElement.ChildNodes[0].Name, null);
                AppendChild(xDoc, newNode, xmlParameter);
                xDoc.DocumentElement.AppendChild(newNode);
            }
            else
            {
                AddEveryNode(xDoc, parentNode, xmlParameter);
            }
            xDoc.Save(_xPath);
        }

        /// <summary> ��ӽڵ����� </summary>
        /// <param name="node">�ڵ����</param>
        /// <param name="namespaceOfPrefix">�ýڵ�������ռ�URI</param>
        /// <param name="attributeName">����������</param>
        /// <param name="attributeValue">����ֵ</param>
        public  void AddAttribute(XmlNode node, string namespaceOfPrefix, string attributeName, string attributeValue)
        {
            XmlDocument xDoc = GetXmlDom();
            string ns = string.IsNullOrEmpty(namespaceOfPrefix) ? "" : node.GetNamespaceOfPrefix(namespaceOfPrefix);
            XmlNode xn = xDoc.CreateNode(XmlNodeType.Attribute, attributeName, ns == "" ? null : ns);
            xn.Value = attributeValue;
            node.Attributes.SetNamedItem(xn);
            xDoc.Save(_xPath);
        }
        /// <summary> ��ӽڵ����� </summary>
        /// <param name="node">�ڵ����</param>
        /// <param name="namespaceOfPrefix">�ýڵ�������ռ�URI</param>
        /// <param name="attributeParameters">�ڵ����Բ���</param>
        public  void AddAttribute(XmlNode node, string namespaceOfPrefix, params AttributeParameter[] attributeParameters)
        {
            XmlDocument xDoc = GetXmlDom();
            string ns = string.IsNullOrEmpty(namespaceOfPrefix) ? "" : node.GetNamespaceOfPrefix(namespaceOfPrefix);
            foreach (AttributeParameter attp in attributeParameters)
            {
                XmlNode xn = xDoc.CreateNode(XmlNodeType.Attribute, attp.Name, ns == "" ? null : ns);
                xn.Value = attp.Value;
                node.Attributes.SetNamedItem(xn);
            }
            xDoc.Save(_xPath);
        }
        /// <summary> ��ӽڵ����� </summary>
        /// <param name="nodeName">�ڵ�����</param>
        /// <param name="namespaceOfPrefix">�ýڵ�������ռ�URI</param>
        /// <param name="attributeName">����������</param>
        /// <param name="attributeValue">����ֵ</param>
        public  void AddAttribute(string nodeName, string namespaceOfPrefix, string attributeName, string attributeValue)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList xlst = xDoc.DocumentElement.ChildNodes;
            for (int i = 0; i < xlst.Count; i++)
            {
                XmlNode node = GetNode(xlst[i], nodeName);
                if (node == null) break;
                AddAttribute(node, namespaceOfPrefix, attributeName, attributeValue);
            }
            xDoc.Save(_xPath);
        }
        /// <summary> ��ӽڵ����� </summary>
        /// <param name="nodeName">�ڵ�����</param>
        /// <param name="namespaceOfPrefix">�ýڵ�������ռ�URI</param>
        /// <param name="attributeParameters">�ڵ����Բ���</param>
        public  void AddAttribute(string nodeName, string namespaceOfPrefix, params AttributeParameter[] attributeParameters)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList xlst = xDoc.DocumentElement.ChildNodes;
            for (int i = 0; i < xlst.Count; i++)
            {
                XmlNode node = GetNode(xlst[i], nodeName);
                if (node == null) break;
                AddAttribute(node, namespaceOfPrefix, attributeParameters);
            }
            xDoc.Save(_xPath);
        }

        /// <summary> ��ȡָ���ڵ����ƵĽڵ���� </summary>
        /// <param name="nodeName">�ڵ�����</param>
        /// <returns></returns>
        public  XmlNode GetNode(string nodeName)
        {
            XmlDocument xDoc = GetXmlDom();
            if (xDoc.DocumentElement.Name == nodeName) return (XmlNode)xDoc.DocumentElement;
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // ���������ӽڵ�
            {
                if (xns.Name == nodeName) return xns;
                else
                {
                    XmlNode xn = GetNode(xns, nodeName);
                    if (xn != null) return xn;  /// V1.0.0.3��ӽڵ��ж�
                }
            }
            return null;
        }
        /// <summary> ��ȡָ���ڵ����ƵĽڵ���� </summary>
        /// <param name="node">�ڵ����</param>
        /// <param name="nodeName">�ڵ�����</param>
        /// <returns></returns>
        public  XmlNode GetNode(XmlNode node, string nodeName)
        {
            foreach (XmlNode xn in node)
            {
                if (xn.Name == nodeName) return xn;
                else
                {
                    XmlNode tmp = GetNode(xn, nodeName);
                    if (tmp != null) return tmp;
                }
            }
            return null;
        }
        /// <summary> ��ȡָ���ڵ����ƵĽڵ���� </summary>
        /// <param name="index">�ڵ�����</param>
        /// <param name="nodeName">�ڵ�����</param>
        public  XmlNode GetNode(int index, string nodeName)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            if (nlst.Count <= index) return null;
            if (nlst[index].Name == nodeName) return (XmlNode)nlst[index];
            foreach (XmlNode xn in nlst[index])
            {
                return GetNode(xn, nodeName);
            }
            return null;
        }
        /// <summary> ��ȡָ���ڵ����ƵĽڵ���� </summary>
        /// <param name="node">�ڵ����</param>
        /// <param name="nodeName">�ڵ�����</param>
        /// <param name="innerText">�ڵ�����</param>
        public  XmlNode GetNode(XmlNode node, string nodeName, string innerText)
        {
            foreach (XmlNode xn in node)
            {
                if (xn.Name == nodeName && xn.InnerText == innerText) return xn;
                else
                {
                    XmlNode tmp = GetNode(xn, nodeName, innerText);
                    if (tmp != null) return tmp;
                }
            }
            return null;
        }
        /// <summary> ��ȡָ���ڵ����ƵĽڵ���� </summary>
        /// <param name="nodeName">�ڵ�����</param>
        /// <param name="innerText">�ڵ�����</param>
        public  XmlNode GetNode(string nodeName, string innerText)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // ���������ӽڵ�
            {
                if (xns.Name == nodeName && xns.InnerText == innerText) return xns;
                XmlNode tmp = GetNode(xns, nodeName, innerText);
                if (tmp != null) return tmp;
            }
            return null;
        }
        /// <summary> ��ȡָ���ڵ����ƵĽڵ���� </summary>
        /// <param name="xmlParameter">XML����</param>
        public  XmlNode GetNode(XmlParam xmlParameter)
        {
            return GetNode(xmlParameter.Name, xmlParameter.InnerText);
        }
        /// <summary> ��ȡָ���ڵ����ƵĽڵ���� </summary>
        /// <param name="node">�ڵ����</param>
        /// <param name="xmlParameter">XML����</param>
        public  XmlNode GetNode(XmlNode node, XmlParam xmlParameter)
        {
            return GetNode(node, xmlParameter.Name, node.InnerText);
        }

        /// <summary> �޸Ľڵ������ </summary>
        /// <param name="node">�ڵ�</param>
        /// <param name="xmlParameter">XML��������</param>
        private  void UpdateNode(XmlNode node, XmlParam xmlParameter)
        {
            node.InnerText = xmlParameter.InnerText;
            for (int i = 0; i < xmlParameter.Attributes.Length; i++)
            {
                for (int j = 0; j < node.Attributes.Count; j++)
                {
                    if (node.Attributes[j].Name == xmlParameter.Attributes[i].Name)
                    {
                        node.Attributes[j].Value = xmlParameter.Attributes[i].Value;
                    }
                }
            }
        }
        /// <summary> �޸Ľڵ������ </summary>
        private  void UpdateNode(XmlNode node, string innerText, AttributeParameter[] attributeParameters)
        {
            node.InnerText = innerText;
            for (int i = 0; i < attributeParameters.Length; i++)
            {
                for (int j = 0; j < node.Attributes.Count; j++)
                {
                    if (node.Attributes[j].Name == attributeParameters[i].Name)
                    {
                        node.Attributes[j].Value = attributeParameters[i].Value;
                    }
                }
            }
        }
        /// <summary> �޸Ľڵ������ </summary>
        /// <param name="index">�ڵ�����</param>
        /// <param name="xmlParameter">XML��������</param>
        public  void UpdateNode(int index, XmlParam xmlParameter)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            if (nlst.Count <= index) return;
            if (nlst[index].Name == xmlParameter.Name)
            {
                UpdateNode(nlst[index], xmlParameter);
            }
            else
            {
                foreach (XmlNode xn in nlst[index])
                {
                    XmlNode xnd = GetNode(xn, xmlParameter.Name);
                    if (xnd != null)
                    {
                        UpdateNode(xnd, xmlParameter);
                    }
                }
            }
            xDoc.Save(_xPath);
        }
        /// <summary> �޸Ľڵ������ </summary>
        /// <param name="index">�ڵ�����</param>
        /// <param name="nodeName">�ڵ�����</param>
        /// <param name="newInnerText">�޸ĺ������</param>
        public  void UpdateNode(int index, string nodeName, string newInnerText)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            if (nlst.Count <= index) return;
            if (nlst[index].Name == nodeName)
            {
                nlst[index].InnerText = newInnerText;
            }
            else
            {
                foreach (XmlNode xn in nlst[index])
                {
                    XmlNode xnd = GetNode(xn, nodeName);
                    if (xnd != null)
                    {
                        xnd.InnerText = newInnerText;
                    }
                }
            }
            xDoc.Save(_xPath);
        }
        /// <summary> �޸Ľڵ������ </summary>
        /// <param name="xmlParameter">XmlParameter����</param>
        /// <param name="innerText">�޸ĺ������</param>
        /// <param name="attributeParameters">��Ҫ�޸ĵ�����</param>
        public  void UpdateNode(XmlParam xmlParameter, string innerText, params AttributeParameter[] attributeParameters)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // ���������ӽڵ�
            {
                if (xns.Name == xmlParameter.Name && xns.InnerText == xmlParameter.InnerText)
                {
                    UpdateNode(xns, innerText, attributeParameters);
                    break;
                }
                XmlNode tmp = GetNode(xns, xmlParameter);
                if (tmp != null)
                {
                    UpdateNode(tmp, innerText, attributeParameters);
                    break;
                }
            }
            xDoc.Save(_xPath);
        }

        /// <summary> ɾ���ڵ� </summary>
        /// <param name="index">�ڵ�����</param>
        public  void DeleteNode(int index)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            nlst[index].ParentNode.RemoveChild(nlst[index]);
            xDoc.Save(_xPath);
        }

        /// <summary> ɾ���ڵ� </summary>
        /// <param name="nodeList">��Ҫɾ���Ľڵ����</param>
        public  void DeleteNode(params XmlNode[] nodeList)
        {
            XmlDocument xDoc = GetXmlDom();
            foreach (XmlNode xnl in nodeList)
            {
                foreach (XmlNode xn in xDoc.DocumentElement.ChildNodes)
                {
                    if (xnl.Equals(xn))
                    {
                        xn.ParentNode.RemoveChild(xn);
                        break;
                    }
                }
            }
            xDoc.Save(_xPath);
        }

        /// <summary> ɾ���ڵ� </summary>
        /// <param name="xDoc">XmlDocument����</param>
        /// <param name="nodeName">�ڵ�����</param>
        /// <param name="nodeText">�ڵ�����</param>
        public  void DeleteNode(string nodeName, string nodeText)
        {
            XmlDocument xDoc = GetXmlDom();
            foreach (XmlNode xn in xDoc.DocumentElement.ChildNodes)
            {
                if (xn.Name == nodeName)
                {
                    if (xn.InnerText == nodeText)
                    {
                        xn.ParentNode.RemoveChild(xn);
                        return;
                    }
                }
                else
                {
                    XmlNode node = GetNode(xn, nodeName);
                    if (node != null && node.InnerText == nodeText)
                    {
                        node.ParentNode.ParentNode.RemoveChild(node.ParentNode);
                        return;
                    }
                }
            }
            xDoc.Save(_xPath);
        }

        /// <summary> �޸�����ֵ </summary>
        /// <param name="elem">Ԫ�ض���</param>
        /// <param name="attps">���Բ���</param>
        private  void SetAttribute(XmlElement elem, params AttributeParameter[] attps)
        {
            foreach (AttributeParameter attp in attps)
            {
                elem.SetAttribute(attp.Name, attp.Value);
            }
        }

        /// <summary> �޸�����ֵ </summary>
        /// <param name="xmlParameter">XML����</param>
        /// <param name="attributeParameters">���Բ���</param>
        public  void SetAttribute(XmlParam xmlParameter, params AttributeParameter[] attributeParameters)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // ���������ӽڵ�
            {
                if (xns.Name == xmlParameter.Name && xns.InnerText == xmlParameter.InnerText)
                {
                    SetAttribute((XmlElement)xns, attributeParameters);
                    break;
                }
                XmlNode tmp = GetNode(xns, xmlParameter);
                if (tmp != null)
                {
                    SetAttribute((XmlElement)tmp, attributeParameters);
                    break;
                }
            }
            xDoc.Save(_xPath);
        }
        /// <summary> �޸�����ֵ </summary>
        /// <param name="xmlParameter">XML����</param>
        /// <param name="attributeValue">������ֵ</param>
        public  void SetAttribute(XmlParam xmlParameter, string attributeName, string attributeValue)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // ���������ӽڵ�
            {
                if (xns.Name == xmlParameter.Name && xns.InnerText == xmlParameter.InnerText)
                {
                    ((XmlElement)xns).SetAttribute(attributeName, attributeValue);
                    break;
                }
                XmlNode tmp = GetNode(xns, xmlParameter);
                if (tmp != null)
                {
                    ((XmlElement)tmp).SetAttribute(attributeName, attributeValue);
                    break;
                }
            }
            xDoc.Save(_xPath);
        }

    }
    

    /// <summary> �����˵�� </summary>
    partial class XmlHelper
    {
        #region - Start ����ģʽ -

        /// <summary> ����ģʽ </summary>
        private static XmlHelper t = null;

        /// <summary> ���߳��� </summary>
        private static object localLock = new object();

        /// <summary> ����ָ������ĵ���ʵ�� </summary>
        public static XmlHelper Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new XmlHelper();
                    }
                }
                return t;
            }
        }

        #endregion - ����ģʽ End -

        #region - Start ����ģʽ -

        /// <summary> ����ģʽ </summary>
        static Dictionary<string, XmlHelper> cache = null;

        /// <summary> ͨ�����Ƶõ�����ʵ�� </summary>
        public static XmlHelper InstanceOfName(string path)
        {
            lock (localLock)
            {
                if (cache == null)
                    cache = new Dictionary<string, XmlHelper>();

                if (!cache.ContainsKey(path))
                    cache.Add(path, new XmlHelper());

                return cache[path];
            }

        }

        #endregion - ����ģʽ End -

    }

    /// <summary> �ڵ����Բ��� </summary>
    public class AttributeParameter
    {
        private string _name;
        private string _value;

        public AttributeParameter() { }
        public AttributeParameter(string attributeName, string attributeValue)
        {
            this._name = attributeName;
            this._value = attributeValue;
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        /// <summary>
        /// ����ֵ
        /// </summary>
        public string Value
        {
            get { return this._value; }
            set { this._value = value; }
        }
    }

    /// <summary> XMLHelper���� </summary>
    public class XmlParam
    {
        private string _name;
        private string _innerText;
        private string _namespaceOfPrefix;
        private AttributeParameter[] _attributes;
        public XmlParam() { }
        public XmlParam(string name, params AttributeParameter[] attParas) : this(name, null, null, attParas) { }
        public XmlParam(string name, string innerText, params AttributeParameter[] attParas) : this(name, innerText, null, attParas) { }
        public XmlParam(string name, string innerText, string namespaceOfPrefix, params AttributeParameter[] attParas)
        {
            this._name = name;
            this._innerText = innerText;
            this._namespaceOfPrefix = namespaceOfPrefix;
            this._attributes = attParas;
        }
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        /// <summary>
        /// �ڵ��ı�
        /// </summary>
        public string InnerText
        {
            get { return this._innerText; }
            set { this._innerText = value; }
        }
        /// <summary>
        /// �ڵ�ǰ׺xmlns����(�����ռ�URI)
        /// </summary>
        public string NamespaceOfPrefix
        {
            get { return this._namespaceOfPrefix; }
            set { this._namespaceOfPrefix = value; }
        }
        /// <summary>
        /// �ڵ����Լ�
        /// </summary>
        public AttributeParameter[] Attributes
        {
            get { return this._attributes; }
            set { this._attributes = value; }
        }
    }
}