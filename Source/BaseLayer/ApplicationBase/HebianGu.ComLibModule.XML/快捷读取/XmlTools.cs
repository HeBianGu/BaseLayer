using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
namespace HebianGu.ComLibModule.XML
{
    public class XmlTools
    {
        private static XmlDocument xmlDoc;
         static string filePathStr;
        public  static void  Load(string filePath)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            filePathStr = filePath;
        
        
        }
        /// <summary>
        /// 得到非表单数据项的数据
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <param name="formDoc">传入的XML业务参数集合</param>
        /// <returns></returns>
        public static string GetNodeValueByName(string NodeName, XmlDocument formDoc)
        {
            XmlNodeList nodelist = formDoc.GetElementsByTagName(NodeName);
            string nodeValue = "";
            if (nodelist.Count > 0)
            {
                nodeValue = nodelist[0].InnerText;
            }
            return nodeValue;
        }
        public static string GetNodeValueByName(string NodeName)
        {
            XmlNodeList nodelist = xmlDoc.GetElementsByTagName(NodeName);
            string nodeValue = "";
            if (nodelist.Count > 0)
            {
                nodeValue = nodelist[0].InnerText;
            }
            return nodeValue;
        }
        public static List<string> GetNodeValuesByName(string NodeName)
        {
            List<string> resultList = new List<string>();
            XmlNodeList nodelist = xmlDoc.GetElementsByTagName(NodeName);
            
            if (nodelist.Count > 0)
            {
                
                foreach (XmlNode node in nodelist)
                {
                    resultList.Add(node.FirstChild.InnerText);
                    
                }
            }
            return resultList;
        }
       public static void AddNode(string nodeName,string value)
        {
          XmlNode node= xmlDoc.CreateElement(nodeName);
          node.InnerText = value;
          xmlDoc.DocumentElement.AppendChild(node);
        }
       public static void AddNode(string nodeName, string value,int restartIndex,string restartdateStr)
       {
           XmlNode node = xmlDoc.CreateElement(nodeName);
           node.InnerText = value;
           XmlNode restartIndexnode = xmlDoc.CreateElement("restartIndex");
           restartIndexnode.InnerText = restartIndex.ToString();
           
           XmlNode restartdateStrnode = xmlDoc.CreateElement("restartDate");
           restartdateStrnode.InnerText = restartdateStr;
           node.AppendChild(restartIndexnode);
           node.AppendChild(restartdateStrnode);
           xmlDoc.DocumentElement.AppendChild(node);
       }
       public static void RemoveNode(string nodeName, string value)
       {
           XmlNodeList nodeList = xmlDoc.GetElementsByTagName(nodeName);
           XmlNode currNode=null;
               foreach (XmlNode node in  nodeList)
               {
                   if (node.FirstChild.InnerText.Trim()==value.Trim())
                   {
                       currNode=node;
                       break;
                   }
               }
               if (currNode!=null)
           {
               xmlDoc.DocumentElement.RemoveChild(currNode);
           }
         
       }
       public static void RenameNode(string nodeName, string oldvalue,string newvalue)
       {
           XmlNodeList nodeList = xmlDoc.GetElementsByTagName(nodeName);
           XmlNode currNode = null;
           foreach (XmlNode node in nodeList)
           {
               if (node.FirstChild.InnerText.Trim() == oldvalue.Trim())
               {
                   node.FirstChild.InnerText = newvalue;
                   break;
               }
           }
          

       }
       public static XmlNode GetNode(string nodeName, string value)
       {
           XmlNodeList nodeList = xmlDoc.GetElementsByTagName(nodeName);
           XmlNode currNode = null;
           foreach (XmlNode node in nodeList)
           {
               if (node.FirstChild.InnerText.Trim() == value.Trim())
               {
                   currNode = node;
                   break;
               }
           }
           return currNode;
       }

        /// <summary>
        /// 得到表单数据项的数据，并将数据填充到Dictionary对象中
        /// </summary>
        /// <param name="formDoc">传入的XML业务参数集合</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionaryCollection(string typeName,XmlDocument formDoc)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            XmlNodeList nodelist = formDoc.GetElementsByTagName(typeName);
            if (nodelist.Count > 0)
            {
                for (int i = 0; i < nodelist.Count; i++)
                {
                    if (nodelist[i].HasChildNodes)
                    {
                        string KEY_IMPORT = nodelist[i].ChildNodes[0].InnerText;
                        string KEY_STRING = nodelist[i].ChildNodes[2].InnerText;
                        if (dictionary.ContainsKey(KEY_IMPORT))
                        {
                            continue;
                        }
                        dictionary.Add(KEY_IMPORT, KEY_STRING);
                    }
                }
            }
            else
            {
                dictionary = null;
            }
            return dictionary;
        }


        public static  void SaveDoc()
        {
            xmlDoc.Save(filePathStr);
        }
       

    }
 
}
