using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using System.Globalization;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{

    public class BxStaticUIConfigProvider : IBxStaticUIConfigProvider
    {
        IBxUIConfigProvider _uiConfigProvider;
        Dictionary<string, BxXmlUIItem> _map = new Dictionary<string, BxXmlUIItem>();

        public BxStaticUIConfigProvider(IBxUIConfigProvider uiConfigProvider)
        {
            _uiConfigProvider = uiConfigProvider;

            //string Name = BxSystemInfo.Instance.SUICProvider.GetStaticUIConfig("TestModule,00001").Name;
        }

        #region IBxStaticUIConfigProvider 成员
        public BxXmlUIItem GetStaticUIConfig(string id)
        {
            BxXmlUIItem staticUIConfig;
            if (_map.TryGetValue(id, out staticUIConfig))
                return staticUIConfig;

            IBxUIConfigFile file;
            XmlElement xmlNode;
            if (_uiConfigProvider.FindUIConfigItem(BxUIConfigID.GetItemID(id), BxUIConfigID.GetFileID(id), out xmlNode, out file))
            {
                staticUIConfig = new BxXmlUIItem(id ,this,_uiConfigProvider);
                _map.Add(id, staticUIConfig);
                return staticUIConfig;
            }
            return null;
        }
        #endregion
    }

}
