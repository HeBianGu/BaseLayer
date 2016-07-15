using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace OPT.Product.Base
{

    public static class BxSiteVerType
    {
        //系统保留,请勿保用
        public const string Reserve = "-1";

        public const string Normal = "0";

        //插入
        public const string Insert = "1";
        //占位
        public const string Placeholder = "2";
    
    }


    static class BxVersionHelp
    {
        public static string GetVersionType(IBxStorageNode node)
        {
            return node.GetElementValue(BxSSL.elementSvt);
        }

        public static void SetVersionType(IBxStorageNode node, string svt)
        {
            node.SetElement(BxSSL.elementSvt, svt);
        }

        public static string GetVersion(IBxStorageNode node)
        {
            return node.GetElementValue(BxSSL.elementVersion);
        }

        public static void SetVersion(IBxStorageNode node, string version)
        {
            node.SetElement(BxSSL.elementVersion, version);
        }

    }

}
