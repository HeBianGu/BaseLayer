using System;
using System.Collections;
using System.Xml;

namespace OPT.PEOfficeCenter.Utils
{
    /// <summary>
    /// XML 的摘要说明
    /// </summary>
    public class XML
    {
        public XML()
        {
        }

        /// <summary>
        /// 创建一个带有根节点的Xml文件
        /// </summary>
        /// <param name="FileName">Xml文件名称</param>
        /// <param name="rootName">根节点名称</param>
        /// <param name="Encode">编码方式:gb2312，UTF-8等常见的</param>
        /// <param name="DirPath">保存的目录路径</param>
        /// <returns></returns>
        public static bool CreateXmlDocument(string xmlPath, string rootName, string Encode)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmldecl;
                xmldecl = xmlDoc.CreateXmlDeclaration("1.0", Encode, null);
                xmlDoc.AppendChild(xmldecl);
                XmlElement xmlelem = xmlDoc.CreateElement("", rootName, "");
                xmlDoc.AppendChild(xmlelem);
                xmlDoc.Save(xmlPath);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// 加载xml文件
        /// </summary>
        /// <param name="xmlPath">xml文件路径</param>
        /// <returns></returns>
        public static bool LoadXml(ref XmlDocument xmlDoc, string xmlPath, string rootName="xml")
        {
            if (xmlPath == string.Empty || xmlPath == "")
                return false;

            try
            {
                //判断xml文件是否存在
                if (!System.IO.File.Exists(xmlPath))
                {
                    CreateXmlDocument(xmlPath, rootName, "UTF-8");
                }

                //加载xml文件
                xmlDoc.Load(xmlPath);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        public static XmlNode AddNode(ref XmlDocument xmlDoc, string nodePath)
        {
            XmlNode parentNode = xmlDoc.DocumentElement;
            nodePath += "/";
            int nPos = nodePath.IndexOf("/");

            if (nPos == 0)
            {
                // 添加根节点
                nodePath = nodePath.Substring(nPos + 1, nodePath.Length - nPos - 1);
                nPos = nodePath.IndexOf("/");
                if (nPos > 0)
                {
                    string node = nodePath.Substring(0, nPos);

                    if (parentNode.Name != node)
                    {
                        XmlElement xmlNode = xmlDoc.CreateElement("", node, "");
                        xmlDoc.AppendChild(xmlNode);
                        parentNode = xmlNode;
                    }
                }
            }

            // 循环添加普通节点
            while (nPos > 0)
            {
                string node = nodePath.Substring(0, nPos);
                XmlNode xmlNode = parentNode.SelectSingleNode(node);
                if (xmlNode == null)
                {
                    xmlNode = xmlDoc.CreateElement(node);
                    parentNode.AppendChild(xmlNode);
                }
                parentNode = xmlNode;

                nodePath = nodePath.Substring(nPos + 1, nodePath.Length - nPos - 1);
                nPos = nodePath.IndexOf("/");
            }

            return parentNode;
        }

        public static XmlNodeList GetNodeList(XmlNode xn, string node)
        {
            try
            {
                if (xn != null)
                {
                    return xn.ChildNodes;
                }
            }
            catch { }

            return null;
        }

        public static XmlNodeList GetNodeList(string path, string node)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                if (LoadXml(ref xmlDoc, path))
                {
                    XmlNode xn = xmlDoc.SelectSingleNode(node);
                    if (xn == null && node.Substring(0, 1) != "/")
                    {
                        XmlNode rootNode = xmlDoc.DocumentElement;
                        xn = rootNode.SelectSingleNode(node);
                    }

                    if(xn!=null)
                    {
                        return xn.ChildNodes;
                    }
                }
            }
            catch { }

            return null;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="xmlNode">xmlNode</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Read(xmlNode, "/Node", "")
         * XmlHelper.Read(xmlNode, "/Node/Element[@Attribute='Name']", "Attribute")
         ************************************************/
        public static string Read(XmlNode xn, string node, string attribute, string defaultvalue = "")
        {
            string value = defaultvalue;
            try
            {
                if (xn != null)
                {
                    value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
                }
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Read(path, "/Node", "")
         * XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
         ************************************************/
        public static string Read(string path, string node, string attribute, string defaultvalue="")
        {
            string value = defaultvalue;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                if (LoadXml(ref xmlDoc, path))
                {
                    XmlNode xn = xmlDoc.SelectSingleNode(node);
                    if (xn == null && node.Substring(0, 1) != "/")
                    {
                        XmlNode rootNode = xmlDoc.DocumentElement;
                        xn = rootNode.SelectSingleNode(node);
                    }

                    value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
                }
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "Element", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Element", "Attribute", "Value")
         * XmlHelper.Insert(path, "/Node", "", "Attribute", "Value")
         ************************************************/
        public static void Insert(string path, string node, string element, string attribute, string value)
        {
            Insert(path, "xml", node, element, attribute, value);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "Element", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Element", "Attribute", "Value")
         * XmlHelper.Insert(path, "/Node", "", "Attribute", "Value")
         ************************************************/
        public static void Insert(string path, string root, string node, string element, string attribute, string value)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                if (LoadXml(ref xmlDoc, path, root))
                {
                    XmlNodeList xns = xmlDoc.SelectNodes(node);
                    XmlNode xn = null;
                    if (xns != null) xn = xns.Item(xns.Count - 1);
                    if (xn == null)  xn = AddNode(ref xmlDoc, node);

                    if (element.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        if (!attribute.Equals(""))
                        {
                            xe.SetAttribute(attribute, value);
                        }
                        else
                        {
                            xe.InnerText = value;
                        }
                    }
                    else
                    {
                        XmlElement xe = xmlDoc.CreateElement(element);
                        if (attribute.Equals(""))
                            xe.InnerText = value;
                        else
                            xe.SetAttribute(attribute, value);
                        xn.AppendChild(xe);
                    }
                    xmlDoc.Save(path);
                }
            }
            catch { }
        }
        
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Attribute", "Value")
         ************************************************/
        public static void Update(string path, string node, string attribute, string value)
        {
            Update(path, "xml", node, attribute, value);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Attribute", "Value")
         ************************************************/
        public static void Update(string path, string root, string node, string attribute, string value)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                if (LoadXml(ref xmlDoc, path, root))
                {
                    XmlNodeList xns =  xmlDoc.SelectNodes(node);
                    XmlNode xn = null;
                    if(xns!=null) xn = xns.Item(xns.Count - 1);
                    if (xn == null) xn = AddNode(ref xmlDoc, node);

                    XmlElement xe = (XmlElement)xn;
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);
                    xmlDoc.Save(path);
                }
            }
            catch { }
        }


        /// <summary>
        /// 清空所有数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Clear(path)
         ************************************************/
        public static void Clear(string path)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                if (LoadXml(ref xmlDoc, path))
                {
                    xmlDoc.RemoveAll();
                    CreateXmlDocument(path, "xml", "UTF-8");
                    xmlDoc.Save(path);
                }
            }
            catch { }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Delete(path, "/Node", "")
         * XmlHelper.Delete(path, "/Node", "Attribute")
         ************************************************/
        public static void Delete(string path, string node, string attribute)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                if (LoadXml(ref xmlDoc, path))
                {
                    XmlNode xn = xmlDoc.SelectSingleNode(node);
                    if (xn==null && node.Substring(0, 1) != "/")
                    {
                        XmlNode rootNode = xmlDoc.DocumentElement;
                        xn = rootNode.SelectSingleNode(node);
                    }
                    
                    XmlElement xe = (XmlElement)xn;
                    if (attribute.Equals(""))
                        xn.ParentNode.RemoveChild(xn);
                    else
                        xe.RemoveAttribute(attribute);
                    xmlDoc.Save(path);
                }
            }
            catch { }
        }
    }
}