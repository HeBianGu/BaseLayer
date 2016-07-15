using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace OPT.Product.Base
{
    /// <summary>
    /// 此接口代表了实现此接口的对象有存储到xml结点的功能。
    /// </summary>
    /// <reremarks>
    /// 底层占用了一些名字以存储内部信息，因此下列名字禁止被作为
    /// 一个xmlnode的名字使用：$stg,$ass,$share 
    /// </reremarks>
    public interface IBxPersistStorageNode
    {
        void SaveStorageNode(IBxStorageNode node);
        void LoadStorageNode(IBxStorageNode node);
    }


    /// <summary>
    /// StorageSystemLable
    /// </summary>
    public static class BxSSL
    {
        public const string nodeStg = "_stg";
        public const string nodeSVA = "_sva";
        public const string nodeEles = "_Eles";
        public const string nodeTS = "TS";
        public const string nodeVS = "VS";
        public const string nodeVItem = "V";
        public const string nodeAss = "Ass";
        public const string nodeItem = "Item";

        public const string elementTypeID = "_tid";
       // public const string elementAssInfo = "ai";
        public const string elementValue = "v";

        //compound child site version type 
        public const string elementSvt = "vt";

        // version
        public const string elementVersion = "ver";
    }

    public static class BxStorageLable
    {
        public const string elementValue = "v";
        public const string elementValueID = "vid";
        public const string elementCount = "ct";
        public const string elementValueDefault = "vd";

        public const string elementUnitCate = "uc";
        public const string elementUnit = "u";

        public const string nodeValue = "V";
        public const string nodeEle = "E";
        public const string nodeConfig = "C";
        public const string nodeArrayElement = "A";
    }

    public interface IBxBufferControl
    {
        void ClearBuffer();
    }
}
