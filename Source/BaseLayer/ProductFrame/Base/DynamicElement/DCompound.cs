using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;
using System.IO;
namespace OPT.Product.Base
{

    public class BxDCompound : BxElementEvent, IBxCompound
    {
        protected List<IBxElementSite> _children = new List<IBxElementSite>();
        IBxElementSite _parentSite = null;

        public void AddChild(IBxElementSite child)
        {
            _children.Add(child);
            if (child is IBxElementSiteInit)
            {
                (child as IBxElementSiteInit).InitContainer(this);
            }
        }
        public void Insert(int index, IBxElementSite child)
        {
            _children.Insert(index, child);
            if (child is IBxElementSiteInit)
            {
                (child as IBxElementSiteInit).InitContainer(this);
            }
        }
        public bool RemoveChild(IBxElementSite child)
        {
            return _children.Remove(child);
        }
        public void RemoveChildAt(int index)
        {
            _children.RemoveAt(index);
        }
        public virtual void Clear()
        {
            _children.Clear();
        }

        #region IBxElement 成员
        public IBxElementSite[] ParentSites
        {
            get { return new IBxElementSite[] { _parentSite }; }
        }
        #endregion

        #region IBxCompound 成员
        public IEnumerable<IBxElementSite> ChildSites
        {
            get { return _children; }
        }
        #endregion

    }


    public class BxDefaultUnit : BxElementT<string>
    {
        string _unitCate;

        public string UnitCate { get { return _unitCate; } }
        public string Unit { get { return Value; } }
        public bool SetUnit(string unitCate, string unit) { _unitCate = unitCate; Value = unit; return true; }

        public BxDefaultUnit() : base() { _unitCate = null; }
        public BxDefaultUnit(string unitCate, string unit) : base(unit) { _unitCate = unitCate; }

        public override string SaveToString()
        {
            if (!Valid)
                return null;
            return UnitCate + "," + Unit;
        }
        public override bool LoadFromString(string s)
        {
            string[] parts = s.Split(new char[] { ',' });
            if (parts.Length != 2)
            {
                Valid = false;
                return false;
            }
            return SetUnit(parts[0], parts[1]);
        }

        public override string GetUIValue()
        {
            return Unit.ToString();
        }

        public override bool SetUIValue(string val)
        {
            Value = val;
            return true;
        }
    }

    public class BxDefaultUnitS : BxElementSiteT<BxDefaultUnit>
    {
        public BxDefaultUnitS() : base() { }
        public BxDefaultUnitS(string unitCate, string unit) : base() { Value.SetUnit(unitCate, unit); }

        public string ID { get { return Config.FullID; } }
        public string Name { get { return Config.Name; } set { Config.Name = value; } }
        //public string ColumnName { get { return Config.ColumnName; } set { Config.ColumnName = value; ; } }

        public void Init(string id)
        {
            CreateEmptyConfig(id);
            Config.ControlType = 2;
            Config.Show = true;
        }

        public string UnitCate { get { return Value.UnitCate; } }
        public string Unit { get { return Value.Unit; } }
        public bool SetUnit(string unitCate, string unit) { return Value.SetUnit(unitCate, unit); }

        public void SaveXmlnode(XmlElement node)
        {
            node.SetAttribute("id", ID);
            node.SetAttribute("name", Name);
            node.SetAttribute("unitCate", UnitCate);
            node.SetAttribute("unit", Unit);
        }
        public void LoadXmlnode(XmlElement node)
        {
            string id = node.GetAttribute("id");
            Init(id);

            Name = node.GetAttribute("name");
            SetUnit(node.GetAttribute("unitCate"), node.GetAttribute("unit"));
        }
    }

    public class BxDefaultUnitsGroup : BxDCompound, IBxContainer
    {
        public void AddUnit(BxDefaultUnitS child)
        {
            child.InitContainer(this);
            AddChild(child);
        }

        public void AddUnit(string id, string name, /*string GroupID, */string unitCate, string unit)
        {
            BxDefaultUnitS site = new BxDefaultUnitS(unitCate, unit);
            site.Init(id);
            site.Config.Name = name;
            //site.Config.ColumnName = GroupID;
            site.InitContainer(this);
            AddChild(site);
        }

        public BxDefaultUnitS GetUnit(string id)
        {
            foreach (IBxElementSite one in ChildSites)
            {
                if (one.UIConfig.FullID == id)
                    return (BxDefaultUnitS)one;
            }
            return null;
        }

        public void SaveXmlnode(XmlElement node)
        {
            XmlElement childNode = null;
            foreach (BxDefaultUnitS one in ChildSites)
            {
                childNode = node.OwnerDocument.CreateElement("Item");
                node.AppendChild(childNode);
                one.SaveXmlnode(childNode);
            }
        }
        public void LoadXmlnode(XmlElement node)
        {
            Clear();
            BxDefaultUnitS child = null;
            foreach (XmlElement one in node.ChildNodes)
            {
                child = new BxDefaultUnitS();
                child.LoadXmlnode(one);
                AddUnit(child);
            }
        }

        #region IBxContainer 成员
        public int Count
        {
            get { return _children.Count; }
        }
        public IBxElementSite GetAt(int index)
        {
            return _children[index];
        }
        public void Append()
        {
        }
        public void AppendRange(int size)
        {
        }
        public void Insert(int index)
        {
        }
        public void InsertRange(int index, int size)
        {
        }
        public void Remove(int index)
        {
        }
        public void RemoveRange(int index, int size)
        {
        }
        public void RemoveAll()
        {
        }
        #endregion
    }

    public class BxDefaultUnitsGroupS : BxElementSiteBase1
    {
        #region  members
        BxDefaultUnitsGroup _value = null;
        #endregion

        #region  properties
        public BxDefaultUnitsGroup Value
        {
            get
            {
                if (_value == null)
                {
                    _value = new BxDefaultUnitsGroup();
                }
                return _value;
            }
        }

        public string ID { get { return Config.FullID; } }

        #endregion

        public void Init(string id)
        {
            SetConfig(S_UIConfig);
            //Config.ControlType = 201;
            //Config.Show = true;
        }

        #region IBxElementSite 成员
        public override IBxElement Element { get { return Value; } }
        //public string Name { get { return Config.Name; } set { Config.Name = value; } }
        #endregion


        public void SaveXmlnode(XmlElement node)
        {
            Value.SaveXmlnode(node);
            node.SetAttribute("id", ID);
        }
        public void LoadXmlnode(XmlElement node)
        {
            Value.LoadXmlnode(node);
            string id = node.GetAttribute("id");
            Init(id);

            //Environment.
        }

        public static BxUIConfigFromSuic s_uiConfig = null;

        public static BxUIConfigFromSuic S_UIConfig
        {
            get
            {
                if (s_uiConfig == null)
                {
                    s_uiConfig = new BxUIConfigFromSuic(new BxSUICPregnant("DefaultUnit,100010"));
                }
                return s_uiConfig;
            }
        }
    }


    public class BxDefaultUnitsProvider
    {
        string _name = null;
        List<BxDefaultUnitsGroupS> _groups = new List<BxDefaultUnitsGroupS>();

        public List<BxDefaultUnitsGroupS> Groups { get { return _groups; } }
        public string Name { get { return _name; } set { _name = Name; } }

        public void Clear() { _groups.Clear(); }
        public void AddGroup(BxDefaultUnitsGroupS group)
        {
            _groups.Add(group);
        }
        public BxDefaultUnitsGroupS GetGroup(string id)
        {
            return _groups.Find(x => x.ID == id);
        }

        public void SaveXmlnode(XmlElement node)
        {
            XmlElement childNode = null;
            foreach (BxDefaultUnitsGroupS one in Groups)
            {
                childNode = node.OwnerDocument.CreateElement("Group");
                node.AppendChild(childNode);
                one.SaveXmlnode(childNode);
            }
            node.SetAttribute("name", Name);
        }
        public void LoadXmlnode(XmlElement node)
        {
            Clear();
            BxDefaultUnitsGroupS child = null;
            foreach (XmlElement one in node.ChildNodes)
            {
                child = new BxDefaultUnitsGroupS();
                child.LoadXmlnode(one);
                AddGroup(child);
            }
            Name = node.GetAttribute("name");
        }

        public void SaveXmlfile(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);
            SaveXmlnode(root);
        }
        public void LoadXmlfile(string filePath)
        {
            Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlElement root = doc.DocumentElement;
            LoadXmlnode(root);
        }

        public static string GetProviderName(string filePath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlElement root = doc.DocumentElement;
                return root.GetAttribute("name");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }


    public class BxProviderInfo
    {
        public string Name { get; set; }
        public string FilePath { get; set; }

        BxDefaultUnitsProvider _provider = null;
        public BxDefaultUnitsProvider Provider
        {
            get
            {
                if (_provider == null)
                {
                    _provider = new BxDefaultUnitsProvider();
                    _provider.LoadXmlfile(FilePath);
                }
                return _provider;
            }
        }

        public BxProviderInfo(string path)
        {
            FilePath = path;
            Name = BxDefaultUnitsProvider.GetProviderName(path);
        }
    }

    public class BxDefaultUnitsManager
    {
        string _path;
        List<BxProviderInfo> _providers;

        public BxDefaultUnitsManager(string path)
        {
            _path = path;
            _providers = new List<BxProviderInfo>();

            BxProviderInfo temp = null;
            string[] FileNames = System.IO.Directory.GetFiles(_path);
            foreach (string one in FileNames)
            {
                temp = new BxProviderInfo(Path.Combine(_path, one));
                if (!string.IsNullOrEmpty(temp.Name))
                {
                    _providers.Add(temp);
                }
            }
        }

        public string[] AllProviderNames
        {
            get
            {
                List<string> files = new List<string>();
                foreach (BxProviderInfo one in _providers)
                {
                    files.Add(one.Name);
                }
                return files.ToArray();
            }
        }

        public BxDefaultUnitsProvider GetProvider(string name)
        {
            foreach (BxProviderInfo one in _providers)
            {
                if (one.Name == name)
                {
                    return one.Provider;
                }
            }
            return null;
        }

        public BxProviderInfo[] AllProviderInfo
        {
            get
            {
                return _providers.ToArray();
            }
        }


        //public void Clear() { _providers.Clear(); }


    }
}
