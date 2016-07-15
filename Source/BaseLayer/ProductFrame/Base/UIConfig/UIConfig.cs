using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxFlag
    {
        UInt32[] _flags;

        public BxFlag() { _flags = new UInt32[1]; }
        public BxFlag(int capacity) { _flags = new UInt32[capacity]; }

        public int Capacity
        {
            get { return (_flags == null) ? 0 : _flags.Length; }
            set { _flags = new UInt32[value]; }
        }
        bool this[int index]
        {
            get
            {
                int remainder;
                int quotient = Math.DivRem(index, 32, out remainder);
                UInt32 mask = (UInt32)1 << remainder;
                return (_flags[quotient] & mask) != 0;
            }
            set
            {
                int remainder;
                int quotient = Math.DivRem(index, 32, out remainder);
                UInt32 mask = (UInt32)1 << remainder;
                if (value)
                    _flags[quotient] |= mask;
                else
                    _flags[quotient] &= ~mask;
            }
        }

        public const int ID = 0;
        public const int Name = 1;
        public const int ControlType = 2;
        public const int Unit = 3;
        public const int ColumnName = 4;
        public const int Icon = 5;
        public const int Range = 6;
        public const int MenuWidth = 7;
        public const int HelpString = 8;
    }



    public class BxUIConfig : IBxUIConfig, IBxPersistStorageNode, IBxModifyManage
    {
        #region members
        protected IBxElementCarrier _carrier = null;

        protected string _id = null;
        protected string _name = null;
        protected BxUIConfigItemsFlag _flag = new BxUIConfigItemsFlag();
        protected Int32? _controlType = null;
        protected IBxUnit _unit = null;
        protected Int32 _decimalDigits = -1;
        protected string _column = null;
        protected string _columnID = null;
        protected string _icon = null;
        protected IBxRange _range = null;
        protected string _menuWidth = null;
        protected string _helpString = null;
        protected BxMutiColumns _subColumns = null;

        //protected 
        #endregion

        #region functions
        public void ResetCarrier(IBxElementCarrier carrier)
        {
            _carrier = carrier;
        }
        public virtual bool NeedSave()
        {
            if (!string.IsNullOrEmpty(_id))
                return true;
            if (_flag.NeedSave)
                return true;
            if (!string.IsNullOrEmpty(_name))
                return true;
            if (_controlType != null)
                return true;
            if (_unit != null)
                return true;
            if (!string.IsNullOrEmpty(_column))
                return true;
            if (!string.IsNullOrEmpty(_icon))
                return true;
            if (_range != null)
                return true;
            if (!string.IsNullOrEmpty(_menuWidth))
                return true;
            if (!string.IsNullOrEmpty(_helpString))
                return true;

            if (_subColumns != null)
            {
                //if(_subColumns.NeedSave)
                return true;
            }

            return false;
        }
        protected void OnModified()
        {
            if (_carrier != null)
                _carrier.AddModified(this);
        }
        public virtual Int32? ControlTypeEx
        {
            get { return _controlType; }
            set { _controlType = value; OnModified(); }
        }
        public virtual BxMutiColumns SubColumnsEx
        {
            get { return _subColumns; }
            set { _subColumns = value; }
        }
        public void InitDefaultUnit(string fullID)
        {
            IBxDefaultUnit du = BxSystemInfo.Instance.DefaultUnitCenter.GetDefaultUnit(fullID);
            if (du != null)
            {
                _unit = du.DefaultUnit;
                OnModified();
            }
        }
        #endregion

        #region IBxUIConfig 成员
        public virtual int ID { get { return BxUIConfigID.GetItemIDInt(_id); } }
        public virtual string FullID { get { return _id; } set { _id = value; } }
        public virtual string Name { get { return _name; } set { _name = value; } }

        public virtual bool? Show
        {
            get { return _flag.Show; }
            set { _flag.Show = value; OnModified(); }
        }
        public virtual bool? ShowTitle
        {
            get { return _flag.ShowTitle; }
            set { _flag.ShowTitle = value; OnModified(); }
        }
        public virtual bool? Expand
        {
            get { return _flag.Expand; }
            set { _flag.Expand = value; OnModified(); }
        }
        public virtual bool? UserHide
        {
            get { return _flag.UserHide; }
            set { _flag.UserHide = value; OnModified(); }
        }
        public virtual bool? ReadOnly
        {
            get { return _flag.ReadOnly; }
            set { _flag.ReadOnly = value; OnModified(); }
        }
        public virtual bool? ValueReadOnly
        {
            get { return _flag.ValueReadOnly; }
            set { _flag.ValueReadOnly = value; OnModified(); }
        }
        public virtual bool? Fold
        {
            get { return _flag.Fold; }
            set { _flag.Fold = value; OnModified(); }
        }

        public virtual Int32 ControlType
        {
            get { return _controlType.HasValue ? _controlType.Value : -1; }
            set { _controlType = value; OnModified(); }
        }
        public virtual IBxUnit Unit { get { return _unit; } set { _unit = value; OnModified(); } }
        public int DecimalDigits { get { return _decimalDigits; } set { _decimalDigits = value; OnModified(); } }
        public virtual string ColumnName { get { return _column; } set { _column = value; OnModified(); } }
        public virtual string ColumnID { get { return _columnID; } set { _columnID = value; OnModified(); } }
        public virtual string Icon { get { return _icon; } set { _icon = value; OnModified(); } }
        public virtual IBxRange Range
        {
            get { return _range; }
            //set {  _range = value; }
        }
        public virtual string MenuWidth
        {
            get { return _menuWidth; }
            set { _menuWidth = value; OnModified(); }
        }
        public virtual string HelpString
        {
            get { return _helpString; }
            set { _helpString = value; OnModified(); }
        }
        public virtual IBxSubColumns SubColumns
        {
            get { return SubColumnsEx; }
        }
        #endregion

        #region IBxPersistStorageNode 成员
        public virtual void SaveStorageNode(IBxStorageNode node)
        {
            if (!string.IsNullOrEmpty(_id))
                node.SetElement(BxXmlConfigField.id, _id);
            if (!string.IsNullOrEmpty(_name))
                node.SetElement(BxXmlConfigField.name, Name);
            if (_flag != null)
                node.SetElement(BxXmlConfigField.flag, _flag.SaveToString());
            if (_controlType != null)
                node.SetElement(BxXmlConfigField.controlType, string.Format("{0}", ControlType));

            if (Unit != null)
            {
                node.SetElement(BxXmlConfigField.unitCate, Unit.Category.Code);
                node.SetElement(BxXmlConfigField.unit, Unit.Code);
            }

            if (_decimalDigits != -1)
                node.SetElement(BxXmlConfigField.decimalDigits, string.Format("{0}", _decimalDigits));


            if (!string.IsNullOrEmpty(_column))
                node.SetElement(BxXmlConfigField.uiColumn, _column);

            if (!string.IsNullOrEmpty(_column))
                node.SetElement(BxXmlConfigField.uiColumnID, _columnID);

            if (!string.IsNullOrEmpty(_icon))
                node.SetElement(BxXmlConfigField.icon, _icon);

            //差Range

            if (!string.IsNullOrEmpty(_menuWidth))
                node.SetElement(BxXmlConfigField.menuWidth, _menuWidth);

            if (!string.IsNullOrEmpty(_helpString))
                node.SetElement(BxXmlConfigField.HelpString, _helpString);

            if (_subColumns != null)
            {
                _subColumns.SaveStorageNode(node.CreateChildNode("subc"));
            }
        }
        public virtual void LoadStorageNode(IBxStorageNode node)
        {
            if (node.HasElement(BxXmlConfigField.id))
                _id = node.GetElementValue(BxXmlConfigField.id);

            if (node.HasElement(BxXmlConfigField.name))
                Name = node.GetElementValue(BxXmlConfigField.name);
            if (node.HasElement(BxXmlConfigField.flag))
            {
                _flag.LoadFromString(node.GetElementValue(BxXmlConfigField.flag));
            }
            if (node.HasElement(BxXmlConfigField.controlType))
                ControlType = Convert.ToInt32(node.GetElementValue(BxXmlConfigField.controlType));

            if (node.HasElement(BxXmlConfigField.unitCate))
            {
                string s1 = node.GetElementValue(BxXmlConfigField.unitCate);
                string s2 = node.GetElementValue(BxXmlConfigField.unit);
                Unit = BxSystemInfo.Instance.UnitsCenter.EMFind(s1, s2);
            }
             if (node.HasElement(BxXmlConfigField.decimalDigits))
                _decimalDigits = Convert.ToInt32(node.GetElementValue(BxXmlConfigField.decimalDigits));

            if (node.HasElement(BxXmlConfigField.uiColumn))
                _column = node.GetElementValue(BxXmlConfigField.uiColumn);

            if (node.HasElement(BxXmlConfigField.uiColumnID))
                _columnID = node.GetElementValue(BxXmlConfigField.uiColumnID);

            if (node.HasElement(BxXmlConfigField.icon))
                _icon = node.GetElementValue(BxXmlConfigField.icon);

            //差Range

            if (node.HasElement(BxXmlConfigField.menuWidth))
                _menuWidth = node.GetElementValue(BxXmlConfigField.menuWidth);
            if (node.HasElement(BxXmlConfigField.HelpString))
                _helpString = node.GetElementValue(BxXmlConfigField.HelpString);

            IBxStorageNode subc = node.GetChildNode("subc");
            if (subc != null)
            {
                if (_subColumns != null)
                    _subColumns.LoadStorageNode(subc);
            }
        }
        #endregion

        #region IBxModifyManage 成员
        public void ResetModifyFlag()
        {
            if (_carrier != null)
                _carrier.RemoveModified(this);
        }
        #endregion
    }
}
