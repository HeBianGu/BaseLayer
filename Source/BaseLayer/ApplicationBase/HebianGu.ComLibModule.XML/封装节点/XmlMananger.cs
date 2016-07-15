#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/5 18:54:48  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：XmlMananger
 *
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
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
    /// <summary> XML操作类 </summary>
    public class XmlMananger
    {
        private static string _xPath;
        /// <summary> xml文件路径 </summary>
        public static string XmlPath
        {
            get { return _xPath; }
            set { _xPath = value; }
        }

        private static string _configName = "XmlPath";

        /// <summary> 配置文件节点名称，请设置在AppSettings节点下 </summary>
        public static string ConfigName
        {
            get { return _configName; }
            set { _configName = value; GetConfig(); }
        }

        /// <summary> 从配置文件读取xml路径 </summary>
        static void GetConfig()
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
        static XmlMananger() { GetConfig(); }


        /// <summary> 添加一个子节点 </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="parentNode">父节点</param>
        /// <param name="xlParameter">Xml参数</param>
        private static void AppendChild(XmlDocument xDoc, XmlNode parentNode, params XmlParam[] xlParameter)
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
       
        /// <summary> 添加一个子节点 </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="parentNode">父节点</param>
        /// <param name="xlParameter">Xml参数</param>
        private static void AddEveryNode(XmlDocument xDoc, XmlNode parentNode, params XmlParam[] paras)
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

        /// <summary> 获得一个XmlDocument对象 </summary>
        private static XmlDocument GetXmlDom()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(_xPath);
            return xdoc;
        }

        /// <summary> 创建一个XML文档，成功创建后操作路径将直接指向该文件 </summary>
        /// <param name="fileName">文件物理路径名</param>
        /// <param name="rootNode">根结点名称</param>
        /// <param name="elementName">元素节点名称</param>
        /// <param name="xmlParameter">XML参数</param>
        public static void CreateXmlFile(string fileName, string rootNode, string elementName, params XmlParam[] xmlParameter)
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
        /// <summary> 创建一个XML文档，成功创建后操作路径将直接指向该文件 </summary>
        /// <param name="fileName">文件物理路径名</param>
        /// <param name="xmlString">xml字符串</param>
        public static void CreateXmlFile(string fileName, string xmlString)
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

        /// <summary> 添加新节点 </summary>
        /// <param name="parentNode">新节点的父节点对象</param>
        /// <param name="xmlParameter">Xml参数对象</param>
        public static void AddNewNode(XmlNode parentNode, params XmlParam[] xmlParameter)
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
        /// <summary> 添加新节点 </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="parentName">新节点的父节点名称</param>
        /// <param name="xmlParameter">XML参数对象</param>
        public static void AddNewNode(string parentName, params XmlParam[] xmlParameter)
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

        /// <summary> 添加节点属性 </summary>
        /// <param name="node">节点对象</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attributeName">新属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void AddAttribute(XmlNode node, string namespaceOfPrefix, string attributeName, string attributeValue)
        {
            XmlDocument xDoc = GetXmlDom();
            string ns = string.IsNullOrEmpty(namespaceOfPrefix) ? "" : node.GetNamespaceOfPrefix(namespaceOfPrefix);
            XmlNode xn = xDoc.CreateNode(XmlNodeType.Attribute, attributeName, ns == "" ? null : ns);
            xn.Value = attributeValue;
            node.Attributes.SetNamedItem(xn);
            xDoc.Save(_xPath);
        }
        /// <summary> 添加节点属性 </summary>
        /// <param name="node">节点对象</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attributeParameters">节点属性参数</param>
        public static void AddAttribute(XmlNode node, string namespaceOfPrefix, params AttributeParameter[] attributeParameters)
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
        /// <summary> 添加节点属性 </summary>
        /// <param name="nodeName">节点名称</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attributeName">新属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void AddAttribute(string nodeName, string namespaceOfPrefix, string attributeName, string attributeValue)
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
        /// <summary> 添加节点属性 </summary>
        /// <param name="nodeName">节点名称</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attributeParameters">节点属性参数</param>
        public static void AddAttribute(string nodeName, string namespaceOfPrefix, params AttributeParameter[] attributeParameters)
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

        /// <summary> 获取指定节点名称的节点对象 </summary>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static XmlNode GetNode(string nodeName)
        {
            XmlDocument xDoc = GetXmlDom();
            if (xDoc.DocumentElement.Name == nodeName) return (XmlNode)xDoc.DocumentElement;
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // 遍历所有子节点
            {
                if (xns.Name == nodeName) return xns;
                else
                {
                    XmlNode xn = GetNode(xns, nodeName);
                    if (xn != null) return xn;  /// V1.0.0.3添加节点判断
                }
            }
            return null;
        }
        /// <summary> 获取指定节点名称的节点对象 </summary>
        /// <param name="node">节点对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static XmlNode GetNode(XmlNode node, string nodeName)
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
        /// <summary> 获取指定节点名称的节点对象 </summary>
        /// <param name="index">节点索引</param>
        /// <param name="nodeName">节点名称</param>
        public static XmlNode GetNode(int index, string nodeName)
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
        /// <summary> 获取指定节点名称的节点对象 </summary>
        /// <param name="node">节点对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="innerText">节点内容</param>
        public static XmlNode GetNode(XmlNode node, string nodeName, string innerText)
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
        /// <summary> 获取指定节点名称的节点对象 </summary>
        /// <param name="nodeName">节点名称</param>
        /// <param name="innerText">节点内容</param>
        public static XmlNode GetNode(string nodeName, string innerText)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // 遍历所有子节点
            {
                if (xns.Name == nodeName && xns.InnerText == innerText) return xns;
                XmlNode tmp = GetNode(xns, nodeName, innerText);
                if (tmp != null) return tmp;
            }
            return null;
        }
        /// <summary> 获取指定节点名称的节点对象 </summary>
        /// <param name="xmlParameter">XML参数</param>
        public static XmlNode GetNode(XmlParam xmlParameter)
        {
            return GetNode(xmlParameter.Name, xmlParameter.InnerText);
        }
        /// <summary> 获取指定节点名称的节点对象 </summary>
        /// <param name="node">节点对象</param>
        /// <param name="xmlParameter">XML参数</param>
        public static XmlNode GetNode(XmlNode node, XmlParam xmlParameter)
        {
            return GetNode(node, xmlParameter.Name, node.InnerText);
        }

        /// <summary> 修改节点的内容 </summary>
        /// <param name="node">节点</param>
        /// <param name="xmlParameter">XML参数对象</param>
        private static void UpdateNode(XmlNode node, XmlParam xmlParameter)
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
        /// <summary> 修改节点的内容 </summary>
        private static void UpdateNode(XmlNode node, string innerText, AttributeParameter[] attributeParameters)
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
        /// <summary> 修改节点的内容 </summary>
        /// <param name="index">节点索引</param>
        /// <param name="xmlParameter">XML参数对象</param>
        public static void UpdateNode(int index, XmlParam xmlParameter)
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
        /// <summary> 修改节点的内容 </summary>
        /// <param name="index">节点索引</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="newInnerText">修改后的内容</param>
        public static void UpdateNode(int index, string nodeName, string newInnerText)
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
        /// <summary> 修改节点的内容 </summary>
        /// <param name="xmlParameter">XmlParameter对象</param>
        /// <param name="innerText">修改后的内容</param>
        /// <param name="attributeParameters">需要修改的属性</param>
        public static void UpdateNode(XmlParam xmlParameter, string innerText, params AttributeParameter[] attributeParameters)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // 遍历所有子节点
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

        /// <summary> 删除节点 </summary>
        /// <param name="index">节点索引</param>
        public static void DeleteNode(int index)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            nlst[index].ParentNode.RemoveChild(nlst[index]);
            xDoc.Save(_xPath);
        }

        /// <summary> 删除节点 </summary>
        /// <param name="nodeList">需要删除的节点对象</param>
        public static void DeleteNode(params XmlNode[] nodeList)
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

        /// <summary> 删除节点 </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeText">节点内容</param>
        public static void DeleteNode(string nodeName, string nodeText)
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

        /// <summary> 修改属性值 </summary>
        /// <param name="elem">元素对象</param>
        /// <param name="attps">属性参数</param>
        private static void SetAttribute(XmlElement elem, params AttributeParameter[] attps)
        {
            foreach (AttributeParameter attp in attps)
            {
                elem.SetAttribute(attp.Name, attp.Value);
            }
        }

        /// <summary> 修改属性值 </summary>
        /// <param name="xmlParameter">XML参数</param>
        /// <param name="attributeParameters">属性参数</param>
        public static void SetAttribute(XmlParam xmlParameter, params AttributeParameter[] attributeParameters)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // 遍历所有子节点
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
        /// <summary> 修改属性值 </summary>
        /// <param name="xmlParameter">XML参数</param>
        /// <param name="attributeValue">新属性值</param>
        public static void SetAttribute(XmlParam xmlParameter, string attributeName, string attributeValue)
        {
            XmlDocument xDoc = GetXmlDom();
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // 遍历所有子节点
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
}