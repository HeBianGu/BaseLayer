using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace OPT.Product.BaseInterface
{
    // public interface IBSObjectSaver

    public interface IBLSiteValueManager
    {
        Int32 SaveValue(IBLElementSite site);
        bool RestoreValue(Int32 id, IBLElementSite site);
    }

    public interface IBLStorage
    {
        IBLStorageNode RootNode { get; }
        IBLSiteValueManager SVM { get; }
    }

    public interface IBLStorageNode
    {
        string Name { get; }
        IBLStorage Storage { get; }
        IBLStorageNode ParentNode { get; }

        IEnumerable<IBLStorageElement> Elements { get; }
        IEnumerable<IBLStorageNode> ChildNodes { get; }

        IBLStorageNode CreateChildNode(string name);
        IBLStorageNode GetChildNode(string name);
        bool HasChildNode(string name);
        void RemoveChildNode(string name);
        void RemoveChildNode(IBLStorageNode node);
        void RemoveAllChildNode();

        void SetElement(string name, string value);
        IBLStorageElement GetElement(string name);
        string GetElementValue(string name);
        bool HasElement(string name);
        void RemoveElement(string name);
        void RemoveAllElement();
    }

    public interface IBLStorageElement
    {
        string Name { get; }
        string Value { get; set; }
    }

}
