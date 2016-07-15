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

    public class BxXmlFile : IBxUIConfigFile
    {
        string _fileID;
        string _filePath;
        XmlDocument _xmlDoc = null;

        public string ID { get { return _fileID; } }
        public string FilePath { get { return _filePath; } }

        public BxXmlFile() { _fileID = null; _filePath = null; }
        public BxXmlFile(string fileID, string filePath) { _fileID = fileID; _filePath = filePath; }

        void Init()
        {
            try
            {
                if (_xmlDoc == null)
                {
                    _xmlDoc = new XmlDocument();
                    _xmlDoc.Load(_filePath);
                }
            }
            catch (System.Exception)
            {
                _xmlDoc = null;
            }
        }

        #region IBxUIConfigFile 成员
        public XmlElement GetUIItem(string uiItemID)
        {
            if (string.IsNullOrEmpty(uiItemID))
                return null;
            Init();
            return _xmlDoc.DocumentElement.SelectSingleNode(string.Format(".//UIItem[@id={0}]", uiItemID)) as XmlElement;
        }
        public XmlElement GetUIColumn(string uiColumnID)
        {
            Init();
            return _xmlDoc.DocumentElement.SelectSingleNode(string.Format(".//UIColumn[@id={0}]", uiColumnID)) as XmlElement;
        }
        #endregion

    }

    public class BxXmlFiles
    {
        //IBxSystemPath _systemPath;
        // CultureInfo _ci;
        List<FileInfo> _fileBuffer = new List<FileInfo>();
        BxRegistries _registries = null;

        public BxXmlFiles() { _registries = BxSystemInfo.Instance.Regisries; }
        public BxXmlFiles(BxRegistries registries)
        {
            _registries = registries;
        }

        //public CultureInfo CultureInfo { get { return _ci; } }

        #region public method
        public FileInfo GetXmlFile(string uiConfigID)
        {
            FileInfo fileInfo = _fileBuffer.Find(x => x.FileID == uiConfigID);
            if (fileInfo == null)
            {
                string uiConfigPath = _registries.GetUIConfigFilePath(uiConfigID);
                if (string.IsNullOrEmpty(uiConfigPath))
                    return null;

                BxXmlFile file = new BxXmlFile(uiConfigID, uiConfigPath);
                fileInfo = new FileInfo(uiConfigID, file);
                _fileBuffer.Add(fileInfo);
            }
            return fileInfo;
        }
        #endregion

        //BxXmlFile GetGeneralXmlFile(string fileID)
        //{
        //    BxXmlFile file = null;
        //    try
        //    {
        //        XmlDocument doc = new XmlDocument();
        //        doc.Load(Path.Combine(_systemPath.GeneralConfigPath, @"ConfigInfo.xml"));
        //        XmlElement node = doc.DocumentElement.SelectSingleNode(string.Format(".//file[@id='{0}']", fileID)) as XmlElement;
        //        if (node == null)
        //            return null;

        //        string sPath = node.GetAttribute("relativePath");
        //        sPath = Path.Combine(_systemPath.GeneralConfigPath, sPath, @"UIConfig");

        //        string sFilePathName = Path.Combine(sPath, _ci.Name + ".xml");
        //        if (!new System.IO.FileInfo(sFilePathName).Exists)
        //        {
        //            sFilePathName = Path.Combine(sPath, _ci.ThreeLetterWindowsLanguageName + ".xml");
        //            if (!new System.IO.FileInfo(sFilePathName).Exists)
        //            {
        //                sFilePathName = Path.Combine(sPath, _ci.TwoLetterISOLanguageName + ".xml");
        //            }
        //        }

        //        file = new BxXmlFile(fileID, sFilePathName);
        //    }
        //    catch (System.Exception)
        //    {
        //    }
        //    return file;
        //}
        //BxXmlFile GetModuleXmlFile(string fileID)
        //{
        //    BxXmlFile file = null;
        //    try
        //    {
        //        //XmlDocument doc = new XmlDocument();
        //        //doc.Load(Path.Combine(_systemPath.ModulesConfigPath, @"ModulesInfo.xml"));
        //        //XmlElement node = doc.DocumentElement.SelectSingleNode(string.Format(".//file[@id='{0}']", fileID)) as XmlElement;
        //        //if (node == null)
        //        //    return null;

        //        //string sPath = node.GetAttribute("relativePath");
        //        //sPath = Path.Combine(_systemPath.ModulesConfigPath, sPath, @"UIConfig");

        //        //string sFilePathName = Path.Combine(sPath, _ci.Name + ".xml");
        //        //if (!new System.IO.FileInfo(sFilePathName).Exists)
        //        //{
        //        //    sFilePathName = Path.Combine(sPath, _ci.ThreeLetterWindowsLanguageName + ".xml");
        //        //    if (!new System.IO.FileInfo(sFilePathName).Exists)
        //        //    {
        //        //        sFilePathName = Path.Combine(sPath, _ci.TwoLetterISOLanguageName + ".xml");
        //        //    }
        //        //}

        //        string sFilePathName = Path.Combine(BxPathInfo.ModuleConfigPath(fileID, _ci), "UIConfig.xml");
        //        file = new BxXmlFile(fileID, sFilePathName);
        //    }
        //    catch (System.Exception)
        //    {
        //    }
        //    return file;
        //}

        #region  SubClass
        public class FileInfo
        {
            string _fileID;
            BxXmlFile _file;
            public string FileID { get { return _fileID; } }
            public BxXmlFile File { get { return _file; } }
            public FileInfo(string fileID) { _fileID = fileID; _file = null; }
            public FileInfo(string fileID, BxXmlFile file) { _fileID = fileID; _file = file; }
        }
        #endregion
    }

    //public class BxXmlFilesEx : BxXmlFiles
    //{
    //    List<ItemInfo> _itemBuffer = new List<ItemInfo>();
    //    public FileItemInfo FindXmlFileInOrder(int itemID, string[] fileIDInOrder)
    //    {
    //        ItemInfo itemInfo = _itemBuffer.Find(x => x.ItemID == itemID);
    //        if (itemInfo == null)
    //        {
    //            itemInfo = new ItemInfo(itemID);
    //            _itemBuffer.Add(itemInfo);
    //        }

    //        FileItemInfo fileItemInfo = null;
    //        foreach (string fileID in fileIDInOrder)
    //        {
    //            fileItemInfo = itemInfo.GetFileItemInfo(fileID);
    //            if (fileItemInfo == null)
    //            {
    //                FileInfo fileInfo = GetXmlFile(fileID);
    //                if (fileInfo.File == null)
    //                {
    //                    itemInfo.AddFileItemInfo(fileInfo, null);
    //                }
    //                else
    //                {
    //                    XmlElement node = fileInfo.File.GetUIItem(itemID);
    //                    itemInfo.AddFileItemInfo(fileInfo, node);
    //                    if (node != null)
    //                        return fileItemInfo;
    //                }
    //            }
    //            else
    //            {
    //                if (fileItemInfo.ItemXmlNode != null)
    //                    return fileItemInfo;
    //            }
    //        }
    //        return fileItemInfo;
    //    }

    //    #region  SubClass
    //    public class FileItemInfo
    //    {
    //        FileInfo _fileInfo;
    //        XmlElement _itemXmlNode;
    //        public FileInfo FileInfo { get { return _fileInfo; } }
    //        public XmlElement ItemXmlNode { get { return _itemXmlNode; } }
    //        public FileItemInfo(FileInfo fileInfo) { _fileInfo = fileInfo; _itemXmlNode = null; }
    //        public FileItemInfo(FileInfo fileInfo, XmlElement itemXmlNode) { _fileInfo = fileInfo; _itemXmlNode = itemXmlNode; }
    //    }
    //    public class ItemInfo
    //    {
    //        Int32 _itemID;
    //        List<FileItemInfo> _itemInfos;
    //        public Int32 ItemID { get { return _itemID; } }
    //        public ItemInfo(Int32 id) { _itemID = id; }
    //        public FileItemInfo GetFileItemInfo(string fileID)
    //        {
    //            return _itemInfos.Find(x => x.FileInfo.FileID == fileID);
    //        }
    //        public void AddFileItemInfo(FileInfo fileInfo, XmlElement itemXmlNode)
    //        {
    //            if (_itemInfos == null)
    //                _itemInfos = new List<FileItemInfo>(2);
    //            _itemInfos.Add(new FileItemInfo(fileInfo, itemXmlNode));
    //        }
    //    }
    //    #endregion
    //}

    public class BxUIConfigProvider : BxXmlFiles, IBxUIConfigProvider
    {

        public BxUIConfigProvider() { }

        #region IBxUIConfigProvider 成员
        public IBxUIConfigFile GetUIConfigFile(string uiConfigID)
        {
            FileInfo info = GetXmlFile(uiConfigID);
            return (info == null) ? null : info.File;
        }

        public bool FindUIConfigItem(string itemID, string uiConfigID, out XmlElement node, out IBxUIConfigFile file)
        {
            FileInfo info = GetXmlFile(uiConfigID);
            if (info != null)
            {
                file = info.File;
                if (file != null)
                {
                    node = file.GetUIItem(itemID);
                    return true;
                }
            }
            file = null;
            node = null;
            return false;
        }

        public bool FindUIConfigItem(string fullID, out XmlElement node, out IBxUIConfigFile file)
        {
            string itemID, fileID;
            BxUIConfigID.Split(fullID, out fileID, out itemID);
            return FindUIConfigItem(itemID, fileID, out node, out file);
        }


        #endregion
    }


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






    /// <summary>
    /// PD : property delegate
    /// </summary>
    public class BxPD
    {
        delegate T GetT<out T>();
        delegate void SetT<in T>(T val);

        delegate Int32 GetInt();
        delegate void SetInt(Int32 val);

        delegate bool GetBool();
        delegate void SetBool(bool val);
    }





    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class BxPDAtribute : Attribute
    {
    }
}
