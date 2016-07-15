using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace OPT.Product.Base
{
    public interface IBxSharedValueArea
    {
        string SaveValue(IBxReferableElement val);
        void LoadValue(IBxReferableElement val, string id);
        void RequestRestoreRefer(IBxElementReferer referer, string id);
    }

        

    public interface IBxStorage
    {
        IBxStorageNode RootNode { get; }
        IBxSharedValueArea SVA { get; }
    }

    public interface IBxStorageNode
    {
        string Name { get; }
        IBxStorage Storage { get; }
        //IBxStorageNode ParentNode { get; }

        IEnumerable<IBxStorageElement> Elements { get; }
        IEnumerable<IBxStorageNode> ChildNodes { get; }

        IBxStorageNode CreateChildNode(string name);
        IBxStorageNode GetChildNode(string name);
        bool HasChildNode(string name);
        void RemoveChildNode(string name);
        void RemoveChildNode(IBxStorageNode node);
        void RemoveAllChildNode();

        void SetElement(string name, string value);
        IBxStorageElement GetElement(string name);
        string GetElementValue(string name);
        bool HasElement(string name);
        void RemoveElement(string name);
        void RemoveAllElement();
    }

    public interface IBxStorageElement
    {
        string Name { get; }
        string Value { get; set; }
    }


    public static class BxStorageExtendMethod
    {
        public static void EMSaveXml(this IBxPersistStorageNode persist, XmlElement xmlNode)
        {
            BxStorage stg = new BxStorage();
            persist.SaveStorageNode(stg.RootNode);
            stg.SaveXml(xmlNode);
        }
        public static void EMLoadXml(this IBxPersistStorageNode persist, XmlElement xmlNode)
        {
            BxStorage stg = new BxStorage();
            stg.LoadXml(xmlNode);
            persist.LoadStorageNode(stg.RootNode);
        }
    }

}
