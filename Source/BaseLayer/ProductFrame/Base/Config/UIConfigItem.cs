using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Xml;
using System.IO;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    //public static class BLUIConfigFieldKeys
    //{
    //    static public string id = "id";
    //    static public string name = "name";
    //    static public string id = "show";
    //    static public string id = "showTitle";
    //    static public string id = "id";
    //    static public string id = "id"; 
    //}
        

    public class BLXmlConfigItem : IBLConfig, IBLUIConfig
    {
        protected Int32 _id = -1;
        protected string _name = null;
        protected UInt32 _flag = 0;
        protected UInt32 _validFlag = 0;
        protected Int32 _controlType = -1;
        protected IBxUnit _unit = null;
        protected IBLUIConfigColumn _column = null;
        protected IBLUIConfigMultiColumn _multiColumn = null;
        
        // protected BLRatio _ratio = null;

        public BLXmlConfigItem() { }
        public BLXmlConfigItem Copy()
        {
            BLXmlConfigItem item = this.MemberwiseClone() as BLXmlConfigItem;
            return item;
        }

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
        public object this[string key]
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

        public IBxUnit Unit { set { _unit = value; } get { return _unit; } }
        public Int32 ControlType { set { _controlType = value; } get { return _controlType; } }
        public IBLUIConfigColumn Column { set { _column = value; } get { return _column; } }
        public IBLUIConfigMultiColumn MultiColumn { set { _multiColumn = value; } get { return _multiColumn; } }
        //public BLRatio Ratio { set { _ratio = value; } get { return _ratio; } }
        #endregion

        #region SubClass
        public delegate T Dlgt_GetValue<T>(BLXmlConfigItem cfg);
        public delegate void Dlgt_SetValue<in T>(BLXmlConfigItem cfg, T val);
        public abstract class _ItemBase
        {
            public string key;
            public abstract object GetValue(BLXmlConfigItem cfg);
            public abstract void SetValue(BLXmlConfigItem cfg, object val);
            public abstract bool HasValue(BLXmlConfigItem cfg);
        }
        public class _Item<T> : _ItemBase
        {
            Dlgt_GetValue<T> dlgt_GetValue;
            Dlgt_SetValue<T> dlgt_SetValue;
            public _Item(string newKey, Dlgt_GetValue<T> newGet, Dlgt_SetValue<T> newSet)
            { key = newKey; dlgt_GetValue = newGet; dlgt_SetValue = newSet; }
            public override object GetValue(BLXmlConfigItem cfg) { return dlgt_GetValue(cfg); }
            public override void SetValue(BLXmlConfigItem cfg, object val) { dlgt_SetValue(cfg, (T)val); }
            public override bool HasValue(BLXmlConfigItem cfg) { return dlgt_GetValue(cfg) != null; }
            public T GetValueEx(BLXmlConfigItem cfg) { return dlgt_GetValue(cfg); }
            public void SetValueEx(BLXmlConfigItem cfg, T val) { dlgt_SetValue(cfg, val); }
        }
        //protected class _FieldItem<T> : _ItemBase
        //{
        //    FieldInfo _field;
        //    public _FieldItem(string newKey, FieldInfo field) { key = newKey; _field = field; }
        //    public override object GetValue(BLXmlConfigItem cfg) { return _field.GetValue(cfg); }
        //    public override void SetValue(BLXmlConfigItem cfg, object val) { _field.SetValue(cfg, val); }
        //    public override bool HasValue(BLXmlConfigItem cfg) { return (_field.GetValue(cfg) != null); }

        //}
        public class _FlagItem : _ItemBase
        {
            UInt32 _flagMask;
            public _FlagItem(string newKey, UInt32 mask) { key = newKey; _flagMask = mask; }

            public override object GetValue(BLXmlConfigItem cfg)
            {
                if ((cfg._validFlag & _flagMask) == 0)
                    return null;
                return ((cfg._flag & _flagMask) != 0);
            }
            public override void SetValue(BLXmlConfigItem cfg, object val)
            {
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
            public override bool HasValue(BLXmlConfigItem cfg) { return ((cfg._validFlag & _flagMask) != 0); }
            public void SetValueEx(BLXmlConfigItem cfg, bool? val)
            {
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
            public bool? GetValueEx(BLXmlConfigItem cfg)
            {
                if ((cfg._validFlag & _flagMask) == 0)
                    return null;
                return ((cfg._flag & _flagMask) != 0);
            }
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
        static readonly public _Item<string> s_itemName = new _Item<string>("name", S_Get_Name, S_Set_Name);
        static readonly public _FlagItem s_itemShow = new _FlagItem("show", s_flagMask_Show);
        static readonly public _FlagItem s_itemShowTitle = new _FlagItem("showTitle", s_flagMask_ShowTitle);
        static readonly public _FlagItem s_itemExpand = new _FlagItem("expand", s_flagMask_Expand);
        static readonly public _FlagItem s_itemUserHide = new _FlagItem("userHide", s_flagMask_UserHide);
        static readonly public _FlagItem s_itemReadOnly = new _FlagItem("readOnly", s_flagMask_ReadOnly);
        static readonly public _FlagItem s_itemValueReadOnly = new _FlagItem("valueReadOnly", s_flagMask_ValueReadOnly);
        static readonly public _FlagItem s_itemFold = new _FlagItem("fold", s_flagMask_Fold);
        static readonly public _Item<Int32> s_itemControlType = new _Item<Int32>("controlType", x => x._controlType, (x, s) => x._controlType = s);
        static readonly public _Item<IBxUnit> s_itemUnit = new _Item<IBxUnit>("unit", x => x._unit, (x, s) => x._unit = s);
        static readonly public _Item<IBLUIConfigColumn> s_itemColumn = new _Item<IBLUIConfigColumn>("column", x => x._column, (x, s) => x._column = s);
        static readonly public _Item<IBLUIConfigMultiColumn> s_itemMultiColumn = new _Item<IBLUIConfigMultiColumn>("multiColumn", x => x._multiColumn, (x, s) => x._multiColumn = s);
        //static readonly public _Item<BLRatio> s_itemRatio = new _Item<BLRatio>("ratio", x => x._ratio, (x, s) => x._ratio = s);

        static protected string S_Get_Name(BLXmlConfigItem cfg) { return cfg._name; }
        static protected void S_Set_Name(BLXmlConfigItem cfg, string val) { cfg._name = val; }


        static BLXmlConfigItem()
        {
            //下面注释掉的两条语句和未注释掉的等价。
            //new _Item<string>("name", delegate(BLXmlConfigItem x){return x.name;},
            //    delegate(BLXmlConfigItem x, string val){ x.name = val;}) ,
            // new _Item<string>("name", x=> x.name,(x,s)=>x.name = s),
            s_itemName = new _Item<string>("name", S_Get_Name, S_Set_Name);
            s_items = new _ItemBase[] 
                { 
                    s_itemName, s_itemID,
                s_itemShow, s_itemShowTitle, s_itemExpand, s_itemUserHide, 
                s_itemReadOnly, s_itemValueReadOnly, s_itemFold,
                s_itemControlType,s_itemUnit,s_itemColumn,s_itemMultiColumn/*,s_itemRatio*/
            };
        }
        static public bool S_ContainsKey(string key)
        {
            return (Array.Find(s_items, x => x.key == key) != null);
        }
        #endregion
    }
}