using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace OPT.Product.BaseInterface
{
    public interface IBxPersistString
    {
        string SaveToString();
        bool LoadFromString(string s);
    }

    /// <summary>
    /// 此接口代表了实现此接口的对象有存储到xml结点的功能。
    /// </summary>
    /// <reremarks>
    /// 底层占用了一些名字以存储内部信息，因此下列名字禁止被作为
    /// 一个xmlnode的名字使用：$stg,$ass,$share 
    /// </reremarks>
    public interface IBxPersistXmlNode
    {
        void SaveXml(XmlElement node);
        void LoadXml(XmlElement node);
    }
    public interface IBLPersistString
    {
        string SaveToString();
        bool LoadFromString(string s);
    }

    /// <summary>
    /// 此接口代表了实现此接口的对象有存储到xml结点的功能。
    /// </summary>
    /// <reremarks>
    /// 底层占用了一些名字以存储内部信息，因此下列名字禁止被作为
    /// 一个xmlnode的名字使用：$stg,$ass,$share 
    /// </reremarks>
    public interface IBLPersistXmlNode
    {
        void SaveXml(XmlElement node);
        void LoadXml(XmlElement node);
    }

}
