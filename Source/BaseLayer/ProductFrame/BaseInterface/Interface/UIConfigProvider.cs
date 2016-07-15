using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace OPT.Product.BaseInterface
{
    public interface IBxUIConfigFile
    {
        //名字
        string ID { get; }
        XmlElement GetUIItem(string uiItemID);
        XmlElement GetUIColumn(string uiColumnID);
    }

    public interface IBxUIConfigProvider
    {
        IBxUIConfigFile GetUIConfigFile(string fileID);
        bool FindUIConfigItem(string itemID, string fileID, out XmlElement node, out IBxUIConfigFile file);
        bool FindUIConfigItem(string fullID, out XmlElement node, out IBxUIConfigFile file);
    }



    public interface IBxDefaultUnit
    {
        string ID { get; }
        IBxUnit DefaultUnit { get; }
    }

    public interface IBxDUProvider
    {
        string ID { get; }
        IBxDefaultUnit Get(string id);
    }

    public interface IBxDUCenter
    {
        IBxDUProvider GetProvider(string moduleID);
        IBxDefaultUnit GetDefaultUnit(string fullID);
        IBxDefaultUnit GetDefaultUnit(string moduleID, string id);
    }


}