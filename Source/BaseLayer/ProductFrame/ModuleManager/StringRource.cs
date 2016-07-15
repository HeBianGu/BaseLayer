using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ModuleManager
{
    /// <summary> XML配置文件读取类 </summary>
    class StringRource
    {

        string _resourceFilePath;
        XmlDocument _xmlDoc = null;

        public StringRource() { }
        /// <summary> 初始化资源文件 </summary>
        public StringRource(string resourceFilePath) { _resourceFilePath = resourceFilePath; }

        public string TextByID(string id)
        {
            if (_xmlDoc == null)
            {
                try
                {
                    _xmlDoc = new XmlDocument();
                    _xmlDoc.Load(_resourceFilePath);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            XmlElement node = _xmlDoc.DocumentElement.SelectSingleNode(string.Format(".//Item[@id='{0}']", id)) as XmlElement;
            return node.GetAttribute("text");

            //if (node == null)
            //    return null;
            //if (node.HasAttribute(ci.Name))
            //    return node.GetAttribute(ci.Name);

            //if (node.HasAttribute(ci.ThreeLetterWindowsLanguageName))
            //    return node.GetAttribute(ci.ThreeLetterWindowsLanguageName);

            //return node.GetAttribute(ci.TwoLetterISOLanguageName);
        }
        /// <summary> 创建资源文件 </summary>
        static public StringRource CreateResource(string resourceFilePath)
        {
            return new StringRource(resourceFilePath);
        }


    }
}
