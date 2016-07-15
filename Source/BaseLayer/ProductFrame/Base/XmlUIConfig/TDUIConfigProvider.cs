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

    public class TDUIConfigFile : IBxUIConfigFile
    {
        IBxUIConfigProvider _baseProvider;
        string _priorityModule;
        string[] _replacedModule;
        IBxUIConfigFile[] _buffer;

        public TDUIConfigFile(IBxUIConfigProvider baseProvider) { _baseProvider = baseProvider; }
        public void Init(string priorityModule, params string[] replacedModules)
        {
            _priorityModule = priorityModule;
            _replacedModule = replacedModules;

            _buffer = new IBxUIConfigFile[replacedModules.Length + 1];
            _buffer[0] = _baseProvider.GetUIConfigFile(priorityModule);
            int index = 1;
            foreach (string one in _replacedModule)
            {
                _buffer[index] = _baseProvider.GetUIConfigFile(one);
                index++;
            }
        }

        public IBxUIConfigFile Buffer
        {
            get { return _buffer[0]; }
        }

        #region IBxUIConfigFile 成员
        public string ID
        {
            get {  return _priorityModule; }
        }
        public XmlElement GetUIItem(string uiItemID)
        {
            XmlElement node = null;
            foreach (IBxUIConfigFile one in _buffer)
            {
                if (one != null)
                {
                    node = one.GetUIItem(uiItemID);
                    if (node != null)
                        return node;
                }
            }
            return node;
        }
        public XmlElement GetUIColumn(string uiColumnID)
        {
            XmlElement node = null;
            foreach (IBxUIConfigFile one in _buffer)
            {
                if (one != null)
                {
                    node = one.GetUIColumn(uiColumnID);
                    if (node != null)
                        return node;
                }
            }
            return node;
        }
        #endregion
    }

    public class TDUIConfigProvider : IBxUIConfigProvider
    {
        IBxUIConfigProvider _baseProvider;
        TDUIConfigFile _buffer;
        string _priorityModule;

        public TDUIConfigProvider() { _baseProvider = BxSystemInfo.Instance.UIConfigProvider; }
        public TDUIConfigProvider(IBxUIConfigProvider baseProvider) { _baseProvider = baseProvider; }

        public void Init(string yourModule, params string[] replacedModules)
        {
            _priorityModule = yourModule;
            _buffer = new TDUIConfigFile(_baseProvider);
            _buffer.Init(yourModule, replacedModules);
        }

        #region IBxUIConfigProvider 成员
        public IBxUIConfigFile GetUIConfigFile(string fileID)
        {
            return _buffer;
        }
        public bool FindUIConfigItem(string itemID, string fileID, out XmlElement node, out IBxUIConfigFile file)
        {
            if ((fileID != _priorityModule) || (_buffer.Buffer == null))
                return _baseProvider.FindUIConfigItem(itemID, fileID, out node, out file);

            XmlElement temp = _buffer.Buffer.GetUIItem(itemID);
            if (temp != null)
            {
                node = temp;
                file = _buffer;
                return true;
            }
            return _baseProvider.FindUIConfigItem(itemID, fileID, out node, out file);
        }

        public bool FindUIConfigItem(string fullID, out XmlElement node, out IBxUIConfigFile file)
        {
            string itemID, fileID;
            int index = string.IsNullOrEmpty(fullID) ? -1 : fullID.IndexOf(',');
            if (index < 0)
            {
                fileID = null;
                itemID = null;
            }
            else
            {
                fileID = fullID.Substring(0, index);
                itemID = fullID.Substring(index + 1, fullID.Length - index - 1);
            }

            return FindUIConfigItem(itemID, fileID, out node, out file);
        }
        #endregion
    }


}
