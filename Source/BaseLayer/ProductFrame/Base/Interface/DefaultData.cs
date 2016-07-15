using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace OPT.Product.Base
{
    public interface IBxDefaultData
    {
        string ID { get; }
        //用来取有单位的默认数据
        BxUnitDouble DefaultData { get; }
        //用来取无单位的默认数据
        string Value { get; }
    }

    public interface IBxDDProvider
    {
        string ID { get; }
        IBxDefaultData Get(string id);
    }

    public interface IBxDDCenter
    {
        IBxDDProvider GetProvider(string moduleID);
        IBxDefaultData GetDefaultData(string fullID);
        IBxDefaultData GetDefaultData(string moduleID, string id);
    }
}
