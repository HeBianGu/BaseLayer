using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Xml;
using System.IO;

namespace OPT.Product.BaseInterface
{
#if false

    //public static class BLUIConfigFieldKeys
    //{
    //    static public string id = "id";
    //    static public string name = "name";
    //    static public string id = "show";
    //    static public string id = "showTitle";
    //    static public string id = "id";
    //    static public string id = "id"; 
    //}


    public static class BxDelagateProperty
    {
        public static T CreateDelegateSet<T>(object obj, string propertyName) where T : class
        {
            PropertyInfo info = obj.GetType().GetProperty(propertyName);
            if (info == null)
                throw new Exception("no property");
            return Delegate.CreateDelegate(typeof(T), obj, info.GetGetMethod()) as T;
        }
        public static T CreateDelegateGet<T>(object obj, string propertyName) where T : class
        {
            PropertyInfo info = obj.GetType().GetProperty(propertyName);
            if (info == null)
                throw new Exception("no property");
            return Delegate.CreateDelegate(typeof(T), obj, info.GetSetMethod()) as T;
        }
    }

    public class BxUIConfig : IBLConfig/*, IBxUIConfig*/
    {
        protected Int32 _id = -1;
        protected string _name = null;
        protected UInt32 _flag = 0;
        protected UInt32 _validFlag = 0;
        protected Int32 _controlType = -1;
        protected IBxUnit _unit = null;
        protected string _columnName = null;

        public BxUIConfig() { }
        protected virtual void Init() { }
        protected virtual void InitUnit() { }
        protected virtual void InitColumn() { }


        #region IBLConfig
        public IEnumerable<BLConfigItem> KeyValues
        {
            get
            {
                List<BLConfigItem> temp = new List<BLConfigItem>(s_items.Length);
                int index = 0;
                object val = null;
                foreach (_ItemBase one in s_items)
                {
                    val = one.GetValue(this);
                    if (val == null)
                        continue;
                    temp[index] = new BLConfigItem(one.key, val);
                    index++;
                }
                return temp.ToArray();
            }
        }
        public IEnumerable<string> Keys
        {
            get
            {
                List<string> tempKeys = new List<string>(s_items.Length);
                Array.ForEach(s_items, x => { if (x.HasValue(this)) tempKeys.Add(x.key); });
                return tempKeys.ToArray();
            }
        }
        public IEnumerable Values
        {
            get
            {
                ArrayList temp = new ArrayList(s_items.Length);
                object val = null;
                Array.ForEach(s_items, x => { val = x.GetValue(this); if (val != null) temp.Add(val); });
                return temp.ToArray();
            }
        }
        public virtual object this[string key]
        {
            get
            {
                _ItemBase item = Array.Find(s_items, x => x.key == key);
                if (item == null) return null;
                return item.GetValue(this);
            }
            set
            {
                _ItemBase item = Array.Find(s_items, x => x.key == key);
                if (item == null) throw new Exception("not have such key " + key);
                item.SetValue(this, value);
            }
        }
        public void Add(string key, object value) { throw new NotImplementedException(); }
        public bool ContainsKey(string key)
        {
            return (Array.Find(s_items, x => x.key == key) != null);
        }
        public bool Remove(string key)
        {
            _ItemBase item = Array.Find(s_items, x => x.key == key);
            if (item == null)
                return false;
            item.SetValue(this, null);
            return true;
        }
        public bool TryGetValue(string key, out object value)
        {
            _ItemBase item = Array.Find(s_items, x => x.key == key);
            if (item == null)
            {
                value = null;
                return false;
            }
            value = item.GetValue(this);
            return (value != null);
        }
        #endregion

        #region SubClass
        public delegate T Dlgt_GetValue<T>(BxUIConfig cfg);
        public delegate void Dlgt_SetValue<in T>(BxUIConfig cfg, T val);
        public abstract class _ItemBase
        {
            public string key;
            public abstract object GetValue(BxUIConfig cfg);
            public abstract void SetValue(BxUIConfig cfg, object val);
            public abstract bool HasValue(BxUIConfig cfg);
        }
        public class _ItemInit<T> : _ItemBase
        {
            Dlgt_GetValue<T> dlgt_GetValue;
            Dlgt_SetValue<T> dlgt_SetValue;
            public _ItemInit(string newKey, Dlgt_GetValue<T> newGet, Dlgt_SetValue<T> newSet)
            { key = newKey; dlgt_GetValue = newGet; dlgt_SetValue = newSet; }
            public override object GetValue(BxUIConfig cfg) { cfg.Init(); return dlgt_GetValue(cfg); }
            public override void SetValue(BxUIConfig cfg, object val) { cfg.Init(); dlgt_SetValue(cfg, (T)val); }
            public override bool HasValue(BxUIConfig cfg) { cfg.Init(); return dlgt_GetValue(cfg) != null; }
            public T GetValueEx(BxUIConfig cfg) { cfg.Init(); return dlgt_GetValue(cfg); }
            public void SetValueEx(BxUIConfig cfg, T val) { cfg.Init(); dlgt_SetValue(cfg, val); }
        }
        public class _FlagItem : _ItemBase
        {
            UInt32 _flagMask;
            public _FlagItem(string newKey, UInt32 mask) { key = newKey; _flagMask = mask; }

            public override object GetValue(BxUIConfig cfg)
            {
                cfg.Init();
                if ((cfg._validFlag & _flagMask) == 0)
                    return null;
                return ((cfg._flag & _flagMask) != 0);
            }
            public override void SetValue(BxUIConfig cfg, object val)
            {
                cfg.Init();
                if (val == null)
                    cfg._validFlag &= ~_flagMask;
                else
                {
                    if (!(val is bool))
                        throw new Exception(string.Format("item %s is only support bool type.\n", key));
                    if ((bool)val)
                        cfg._flag |= _flagMask;
                    else
                        cfg._flag &= ~_flagMask;
                    cfg._validFlag |= _flagMask;
                }
            }
            public override bool HasValue(BxUIConfig cfg)
            {
                cfg.Init();
                return ((cfg._validFlag & _flagMask) != 0);
            }
            public void SetValueEx(BxUIConfig cfg, bool? val)
            {
                cfg.Init();
                if (val.HasValue)
                {
                    cfg._validFlag |= _flagMask;
                    if (val.Value)
                        cfg._flag |= _flagMask;
                    else
                        cfg._flag &= ~_flagMask;
                }
                else
                {
                    cfg._validFlag &= ~_flagMask;
                }
            }
            public bool? GetValueEx(BxUIConfig cfg)
            {
                cfg.Init();
                if ((cfg._validFlag & _flagMask) == 0)
                    return null;
                return ((cfg._flag & _flagMask) != 0);
            }
        }
        public class _Item<T> : _ItemBase
        {
            Dlgt_GetValue<T> dlgt_GetValue;
            Dlgt_SetValue<T> dlgt_SetValue;
            public _Item(string newKey, Dlgt_GetValue<T> newGet, Dlgt_SetValue<T> newSet)
            { key = newKey; dlgt_GetValue = newGet; dlgt_SetValue = newSet; }

            public override object GetValue(BxUIConfig cfg) { return dlgt_GetValue(cfg); }
            public override void SetValue(BxUIConfig cfg, object val) { dlgt_SetValue(cfg, (T)val); }
            public override bool HasValue(BxUIConfig cfg) { return dlgt_GetValue(cfg) != null; }
            public T GetValueEx(BxUIConfig cfg) { return dlgt_GetValue(cfg); }
            public void SetValueEx(BxUIConfig cfg, T val) { dlgt_SetValue(cfg, val); }
        }
        #endregion

        #region static
        static public UInt32 s_flagMask_Valid = ((UInt32)1) << 31;
        static public UInt32 s_flagMask_Show = 0x0001;
        static public UInt32 s_flagMask_ShowTitle = 0x0002;
        static public UInt32 s_flagMask_Expand = 0x0004;
        static public UInt32 s_flagMask_UserHide = 0x0080;
        static public UInt32 s_flagMask_ReadOnly = 0x0010;
        static public UInt32 s_flagMask_ValueReadOnly = 0x0020;
        static public UInt32 s_flagMask_Fold = 0x0040;

        static public _ItemBase[] s_items;
        static readonly public _Item<Int32> s_itemID = new _Item<Int32>("id", x => x._id, (x, s) => x._id = s);
        static readonly public _ItemInit<string> s_itemName = new _ItemInit<string>("name", S_Get_Name, S_Set_Name);
        //-->begin flag
        static readonly public _FlagItem s_itemShow = new _FlagItem("show", s_flagMask_Show);
        static readonly public _FlagItem s_itemShowTitle = new _FlagItem("showTitle", s_flagMask_ShowTitle);
        static readonly public _FlagItem s_itemExpand = new _FlagItem("expand", s_flagMask_Expand);
        static readonly public _FlagItem s_itemUserHide = new _FlagItem("userHide", s_flagMask_UserHide);
        static readonly public _FlagItem s_itemReadOnly = new _FlagItem("readOnly", s_flagMask_ReadOnly);
        static readonly public _FlagItem s_itemValueReadOnly = new _FlagItem("valueReadOnly", s_flagMask_ValueReadOnly);
        static readonly public _FlagItem s_itemFold = new _FlagItem("fold", s_flagMask_Fold);
        //<--end of flag
        static readonly public _ItemInit<Int32> s_itemControlType = new _ItemInit<Int32>("controlType", x => x._controlType, (x, s) => x._controlType = s);
        static readonly public _ItemInit<IBxUnit> s_itemUnit = new _ItemInit<IBxUnit>("unit", x => { x.InitUnit(); return x._unit; }, (x, s) => x._unit = s);
        static readonly public _ItemInit<string> s_itemColumn =
            new _ItemInit<string>("column", x => { x.InitColumn(); return x._columnName; }, (x, s) => x._columnName = s);
        //static readonly public _Item<IBLUIConfigMultiColumn> s_itemMultiColumn = new _Item<IBLUIConfigMultiColumn>("multiColumn", x => x._multiColumn, (x, s) => x._multiColumn = s);
        //static readonly public _Item<BLRatio> s_itemRatio = new _Item<BLRatio>("ratio", x => x._ratio, (x, s) => x._ratio = s);

        static protected string S_Get_Name(BxUIConfig cfg) { return cfg._name; }
        static protected void S_Set_Name(BxUIConfig cfg, string val) { cfg._name = val; }

        static protected void S_SetFlagValue(BxUIConfig cfg, UInt32 flagMask, bool? val)
        {
            if (val.HasValue)
            {
                cfg._validFlag |= flagMask;
                if (val.Value)
                    cfg._flag |= flagMask;
                else
                    cfg._flag &= ~flagMask;
            }
            else
            {
                cfg._validFlag &= ~flagMask;
            }
        }
        static protected bool? S_GetFlagValue(BxUIConfig cfg, UInt32 flagMask)
        {
            cfg.Init();
            if ((cfg._validFlag & flagMask) == 0)
                return null;
            return ((cfg._flag & flagMask) != 0);
        }

        static BxUIConfig()
        {
            //下面注释掉的两条语句和未注释掉的等价。
            //new _Item<string>("name", delegate(BxUIConfig x){return x.name;},
            //    delegate(BxUIConfig x, string val){ x.name = val;}) ,
            // new _Item<string>("name", x=> x.name,(x,s)=>x.name = s),
            // s_itemName = new _Item<string>("name", S_Get_Name, S_Set_Name);
            s_items = new _ItemBase[] 
                { 
                    s_itemName, s_itemID,
                s_itemShow, s_itemShowTitle, s_itemExpand, s_itemUserHide, 
                s_itemReadOnly, s_itemValueReadOnly, s_itemFold,
                s_itemControlType,s_itemUnit,s_itemColumn/*,s_itemMultiColumn*//*,s_itemRatio*/
            };
        }
        static public bool S_ContainsKey(string key)
        {
            return (Array.Find(s_items, x => x.key == key) != null);
        }
        #endregion




    }

    public class BxCustomUIConfig : BxUIConfig, IBxUIConfig
    {
        #region IBLUIConfig 成员
        public Int32 ID { set { _id = value; } get { return _id; } }
        public string Name { set { S_Set_Name(this, value); } get { return S_Get_Name(this); } }
        public bool? Show { set { s_itemShow.SetValueEx(this, value); } get { return s_itemShow.GetValueEx(this); } }
        public bool? ShowTitle { set { s_itemShowTitle.SetValueEx(this, value); } get { return s_itemShowTitle.GetValueEx(this); } }
        public bool? Expand { set { s_itemExpand.SetValueEx(this, value); } get { return s_itemExpand.GetValueEx(this); } }
        public bool? UserHide { set { s_itemUserHide.SetValueEx(this, value); } get { return s_itemUserHide.GetValueEx(this); } }
        public bool? ReadOnly { set { s_itemReadOnly.SetValueEx(this, value); } get { return s_itemReadOnly.GetValueEx(this); } }
        public bool? ValueReadOnly { set { s_itemValueReadOnly.SetValueEx(this, value); } get { return s_itemValueReadOnly.GetValueEx(this); } }
        public bool? Fold { set { s_itemFold.SetValueEx(this, value); } get { return s_itemFold.GetValueEx(this); } }
        public Int32 ControlType { set { s_itemControlType.SetValueEx(this, value); } get { return s_itemControlType.GetValueEx(this); } }
        public IBxUnit Unit { set { s_itemUnit.SetValueEx(this, value); } get { return s_itemUnit.GetValueEx(this); } }
        public string ColumnName { set { s_itemColumn.SetValueEx(this, value); } get { return s_itemColumn.GetValueEx(this); } }
        #endregion
    }

    public enum BxEUnitType
    {
        Invalid = -1,
        Normal = 0,
        V = 1,
        M = 2
    }


    public class BxXmlUIConfig : BxUIConfig, IBxUIConfig
    {
        public class BxUnit
        {
            bool _isNormal;
            int _index = 0;
            string[] _unitCate;
            string[] _unit;
            public BxUnit(string unitCate, string unit)
            {
                _isNormal = true;
                _unitCate = new string[] { unitCate };
                _unit = new string[] { unit };
            }
            public BxUnit(string unitCateV, string unitV, string unitCateM, string unitM)
            {
                _isNormal = false;
                _unitCate = new string[] { unitCateV, unitCateM };
                _unit = new string[] { unitV, unitM };
            }
            public string CurUnitCate { get { return _unitCate[_index]; } }
            public string CurUnit { get { return _unit[_index]; } }
            public void SetUnitType(bool VOrM)
            {
                if (_isNormal)
                    return;
                _index = VOrM ? 0 : 1;
            }
        }

        protected IBxUIConfigProvider _provider = null;
        protected bool _inited = false;
        protected BxUnit _unitBox = null;
        protected Int32 _tableID;

        public BxXmlUIConfig() { }
        public BxXmlUIConfig(IBxUIConfigProvider provider) { _provider = provider; }

        protected BxXmlUIConfig Copy()
        {
            BxXmlUIConfig item = this.MemberwiseClone() as BxXmlUIConfig;
            return item;
        }
        protected override void Init()
        {
            if (_inited)
                return;
            _inited = true;
            XmlElement node = null;

            string s = node.GetAttribute(s_itemID.key);
            if (!string.IsNullOrEmpty(s))
                _id = S_ParseInt(s);

            s = node.GetAttribute(s_itemName.key);
            if (!string.IsNullOrEmpty(s))
                _name = s;

            s = node.GetAttribute(s_itemShow.key);
            if (!string.IsNullOrEmpty(s))
                S_SetFlagValue(this, s_flagMask_Show, S_ParseBool(s));

            s = node.GetAttribute(s_itemShowTitle.key);
            if (!string.IsNullOrEmpty(s))
                S_SetFlagValue(this, s_flagMask_ShowTitle, S_ParseBool(s));

            s = node.GetAttribute(s_itemExpand.key);
            if (!string.IsNullOrEmpty(s))
                S_SetFlagValue(this, s_flagMask_Expand, S_ParseBool(s));

            s = node.GetAttribute(s_itemUserHide.key);
            if (!string.IsNullOrEmpty(s))
                S_SetFlagValue(this, s_flagMask_UserHide, S_ParseBool(s));

            s = node.GetAttribute(s_itemReadOnly.key);
            if (!string.IsNullOrEmpty(s))
                S_SetFlagValue(this, s_flagMask_ReadOnly, S_ParseBool(s));

            s = node.GetAttribute(s_itemValueReadOnly.key);
            if (!string.IsNullOrEmpty(s))
                S_SetFlagValue(this, s_flagMask_ValueReadOnly, S_ParseBool(s));

            s = node.GetAttribute(s_itemFold.key);
            if (!string.IsNullOrEmpty(s))
                S_SetFlagValue(this, s_flagMask_Fold, S_ParseBool(s));

            s = node.GetAttribute(s_itemControlType.key);
            if (!string.IsNullOrEmpty(s))
                _controlType = S_ParseInt(s);

            s = node.GetAttribute("unitCate");
            if (!string.IsNullOrEmpty(s))
            {
                string unit = node.GetAttribute("unit");
                _unitBox = new BxUnit(s, unit);
            }
            else
            {
                string unitCateV = node.GetAttribute("unitCateV");
                string unitV = node.GetAttribute("unitV");
                string unitCateM = node.GetAttribute("unitCateM");
                string unitM = node.GetAttribute("unitM");
                _unitBox = new BxUnit(unitCateV, unitV, unitCateM, unitM);
            }

            s = node.GetAttribute(s_itemColumn.key);
            if (!string.IsNullOrEmpty(s))
                _tableID = S_ParseInt(s);
        }
        protected override void InitUnit()
        {
            if (_unit != null)
                return;
            if ((_unitBox == null) || string.IsNullOrEmpty(_unitBox.CurUnitCate) || string.IsNullOrEmpty(_unitBox.CurUnit))
                return;

        }

        #region IBLUIConfig 成员
        //public Int32 ID { set { _id = value; } get { return _id; } }
        public Int32 ID { get { return _id; } }
        public string Name { get { return S_Get_Name(this); } }
        public bool? Show { get { return s_itemShow.GetValueEx(this); } }
        public bool? ShowTitle { get { return s_itemShowTitle.GetValueEx(this); } }
        public bool? Expand { get { return s_itemExpand.GetValueEx(this); } }
        public bool? UserHide { set { throw new Exception(); } get { return s_itemUserHide.GetValueEx(this); } }
        public bool? ReadOnly { get { return s_itemReadOnly.GetValueEx(this); } }
        public bool? ValueReadOnly { get { return s_itemValueReadOnly.GetValueEx(this); } }
        public bool? Fold { get { return s_itemFold.GetValueEx(this); } }
        public Int32 ControlType { get { return s_itemControlType.GetValueEx(this); } }
        public IBxUnit Unit { set { throw new Exception(); } get { return s_itemUnit.GetValueEx(this); } }
        public string ColumnName { get { return s_itemColumn.GetValueEx(this); } }
        #endregion

        #region IBLConfig
        public override object this[string key]
        {
            get { return base[key]; }
            set { throw new Exception("Can not write BxXmlUIConfig!"); }
        }
        #endregion

        static protected Int32 S_ParseInt(string val)
        {
            Int32 result;
            if (Int32.TryParse(val, out result))
                return result;
            return -1;
        }
        static protected bool? S_ParseBool(string val)
        {
            bool result;
            if (bool.TryParse(val, out result))
                return result;
            return null;
        }

    }
#endif

}
