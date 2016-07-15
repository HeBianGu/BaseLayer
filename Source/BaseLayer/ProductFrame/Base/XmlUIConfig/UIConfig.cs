using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxUIConfigItem : IBxPersistStorageNode
    {
        #region members
        protected string _id = null;
        protected string _name = null;
        protected BxUIItemFlag _flag = null;
        protected Int32? _controlType = null;
        protected IBxUnit _unit = null;
        protected string _column = null;
        //protected BxXmlUITable _subItemTable = null;
        protected UInt16? _widthRatio = null;
        protected Int32? _centerColumn = null;
        protected string _icon = null;
        protected string _tip = null;

        #endregion

        protected void InitFlag()
        {
            _flag = new BxUIItemFlag();
        }

        #region IBxUIConfig 成员
        public string ID { get { return BxUIConfigID.GetItemID(_id); } }
        public string FullID { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public bool? Show
        {
            get { return (_flag == null) ? null : _flag.Show; }
            set
            {
                if (_flag == null)
                {
                    if (value.HasValue)
                        InitFlag();
                    else
                        return;
                }
                _flag.Show = value;
            }
        }
        public bool? ShowTitle
        {
            get { return (_flag == null) ? null : _flag.ShowTitle; }
            set
            {
                if (_flag == null)
                {
                    if (value.HasValue)
                        InitFlag();
                    else
                        return;
                }
                _flag.ShowTitle = value;
            }
        }
        public bool? Expand
        {
            get { return (_flag == null) ? null : _flag.Expand; }
            set
            {
                if (_flag == null)
                {
                    if (value.HasValue)
                        InitFlag();
                    else
                        return;
                }
                _flag.Expand = value;
            }
        }
        public bool? UserHide
        {
            get { return (_flag == null) ? null : _flag.UserHide; }
            set
            {
                if (_flag == null)
                {
                    if (value.HasValue)
                        InitFlag();
                    else
                        return;
                }
                _flag.UserHide = value;
            }
        }
        public bool? ReadOnly
        {
            get { return (_flag == null) ? null : _flag.ReadOnly; }
            set
            {
                if (_flag == null)
                {
                    if (value.HasValue)
                        InitFlag();
                    else
                        return;
                }
                _flag.ReadOnly = value;
            }
        }
        public bool? ValueReadOnly
        {
            get { return (_flag == null) ? null : _flag.ValueReadOnly; }
            set
            {
                if (_flag == null)
                {
                    if (value.HasValue)
                        InitFlag();
                    else
                        return;
                }
                _flag.ValueReadOnly = value;
            }
        }
        public bool? Fold
        {
            get { return (_flag == null) ? null : _flag.Fold; }
            set
            {
                if (_flag == null)
                {
                    if (value.HasValue)
                        InitFlag();
                    else
                        return;
                }
                _flag.Fold = value;
            }
        }
        public Int32 ControlType
        {
            get { return _controlType.HasValue ? _controlType.Value : -1; }
            set { _controlType = value; }
        }
        public IBxUnit Unit { get { return _unit; } set { _unit = value; } }
        public string ColumnName { get { return _column; } set { _column = value; } }
        public UInt16 WidthRation
        {
            get
            {
                if ((_widthRatio == null) || !_widthRatio.HasValue)
                    throw new Exception("have no value!");
                return _widthRatio.Value;
            }
            set { _widthRatio = value; }
        }
        public IBxSubColumns SubColumns
        {
            get { return null; }
        }
        public string Icon { get { return _icon; } set { _icon = value; } }
        public string Tip { get { return _tip; } set { _tip = value; } }

        #endregion

        public Int32? ControlTypeEx { get { return _controlType; } set { _controlType = value; } }
        public UInt16? WidthRationEx
        {
            get { return _widthRatio; }
            set { _widthRatio = value; }
        }
        public Int32? CenterColumn
        {
            get { return _centerColumn; }
            set { _centerColumn = value; }
        }

        #region IBxPersistStorageNode 成员
        public void SaveStorageNode(IBxStorageNode node)
        {
            if (Name != null)
                node.SetElement(BxXmlConfigField.name, Name);
            if (_flag != null)
                node.SetElement(BxXmlConfigField.flag, _flag.SaveToString());
            if (_controlType != null)
                node.SetElement(BxXmlConfigField.controlType, string.Format("{0}", ControlType));

            if (Unit != null)
            {
                node.SetElement(BxXmlConfigField.unitCate, Unit.Category.ID);
                node.SetElement(BxXmlConfigField.unit, Unit.ID);
            }
        }
        public void LoadStorageNode(IBxStorageNode node)
        {
            if (node.HasElement(BxXmlConfigField.name))
                Name = node.GetElementValue(BxXmlConfigField.name);
            if (node.HasElement(BxXmlConfigField.flag))
            {
                if (_flag == null)
                    InitFlag();
                _flag.LoadFromString(node.GetElementValue(BxXmlConfigField.flag));
            }
            if (node.HasElement(BxXmlConfigField.controlType))
                ControlType = Convert.ToInt32(node.GetElementValue(BxXmlConfigField.controlType));

            if (node.HasElement(BxXmlConfigField.unitCate))
            {
                string s1 = node.GetElementValue(BxXmlConfigField.unitCate);
                string s2 = node.GetElementValue(BxXmlConfigField.unit);
                Unit = BxSystemInfo.Instance.UnitsCenter.Parse(s1).Parse(s2);
            }
        }
        #endregion
    }

    public class BxUIConfigItemEx : IBxUIConfig, IBxPersistStorageNode, IBxModifyManage
    {
        #region members
        protected IBxElementCarrier _carrier = null;
        protected BxUIConfigItem _dynItem = null;
        string _id = null;
        protected IBxStaticUIConfigPregnant _suicPregnant;
        protected Int32 _curUnitIndex = 0;
        protected BxSubColumns _subColumns = BxSubColumns.Invalid;
        #endregion


        // public BxXmlUIItem XmlItem { set { _staticItem = value; } }
        public BxXmlUIItem StaticUIConfig
        {
            get
            {
                if (_suicPregnant != null)
                    return _suicPregnant.StaticUIConfig;
                return null;
            }
        }

        public void ResetCarrier(IBxElementCarrier carrier)
        {
            _carrier = carrier;
        }

        public void ResetSUICPregnant(IBxStaticUIConfigPregnant suicPregnant)
        {
            _suicPregnant = suicPregnant;
        }

        public BxUIConfigItemEx() { _suicPregnant = null; }
        public BxUIConfigItemEx(string id) { _suicPregnant = null; _id = id; }
        public BxUIConfigItemEx(IBxElementCarrier carrier, IBxStaticUIConfigPregnant suicPregnant)
        {
            _carrier = carrier;
            _suicPregnant = suicPregnant;
        }
        public BxUIConfigItemEx(IBxStaticUIConfigPregnant suicPregnant) { _suicPregnant = suicPregnant; }

        protected void InitDynItem()
        {
            if (_dynItem == null)
                _dynItem = new BxUIConfigItem();
        }

        #region IBxUIConfig 成员
        public int ID
        {
            get
            {
                return BxUIConfigID.GetItemIDInt(FullID);
            }
            // set { InitDynItem(); _dynItem.ID = value; }
        }
        public string FullID
        {
            get
            {
                if (_id == null)
                {
                    if (_suicPregnant != null)
                        _id = _suicPregnant.ConfigID;
                }
                return _id;
            }
            // set { InitDynItem(); _dynItem.ID = value; }
        }

        public string Name
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.Name != null))
                    return _dynItem.Name;
                return (StaticUIConfig == null) ? null : StaticUIConfig.Name;
            }
            set { InitDynItem(); _dynItem.Name = value; OnModified(); }
        }
        public bool? Show
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.Show != null))
                    return _dynItem.Show;
                return (StaticUIConfig == null) ? null : StaticUIConfig.Show;
            }
            set { InitDynItem(); _dynItem.Show = value; OnModified(); }
        }
        public bool? ShowTitle
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.ShowTitle != null))
                    return _dynItem.ShowTitle;
                return (StaticUIConfig == null) ? null : StaticUIConfig.ShowTitle;
            }
            set { InitDynItem(); _dynItem.ShowTitle = value; OnModified(); }
        }
        public bool? Expand
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.Expand != null))
                    return _dynItem.Expand;
                return (StaticUIConfig == null) ? null : StaticUIConfig.Expand;
            }
            set { InitDynItem(); _dynItem.Expand = value; OnModified(); }
        }
        public bool? UserHide
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.UserHide != null))
                    return _dynItem.UserHide;
                return (StaticUIConfig == null) ? null : StaticUIConfig.UserHide;
            }
            set { InitDynItem(); _dynItem.UserHide = value; OnModified(); }
        }
        public bool? ReadOnly
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.ReadOnly != null))
                    return _dynItem.ReadOnly;
                return (StaticUIConfig == null) ? null : StaticUIConfig.ReadOnly;
            }
            set { InitDynItem(); _dynItem.ReadOnly = value; OnModified(); }
        }
        public bool? ValueReadOnly
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.ValueReadOnly != null))
                    return _dynItem.ValueReadOnly;
                return (StaticUIConfig == null) ? null : StaticUIConfig.ValueReadOnly;
            }
            set { InitDynItem(); _dynItem.ValueReadOnly = value; OnModified(); }
        }
        public bool? Fold
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.Fold != null))
                    return _dynItem.Fold;
                return (StaticUIConfig == null) ? null : StaticUIConfig.Fold;
            }
            set { InitDynItem(); _dynItem.Fold = value; OnModified(); }
        }

        public Int32 ControlType
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.ControlTypeEx != null))
                    return _dynItem.ControlType;
                if ((StaticUIConfig != null) && StaticUIConfig.ControlType.HasValue)
                    return StaticUIConfig.ControlType.Value;
                return -1;
            }
            set { InitDynItem(); _dynItem.ControlType = value; OnModified(); }
        }
        public IBxUnit Unit
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.Unit != null))
                    return _dynItem.Unit;
                if ((StaticUIConfig != null) && StaticUIConfig.Unit != null)
                    return StaticUIConfig.Unit.GetUnit(_curUnitIndex);
                return null;
            }
            set
            {
                InitDynItem();
                _dynItem.Unit = value;
                OnModified();
            }
        }
        public string ColumnName
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.ColumnName != null))
                    return _dynItem.ColumnName;
                return (StaticUIConfig == null) ? null : StaticUIConfig.UIColumnName;
            }
            set { InitDynItem(); _dynItem.ColumnName = value; OnModified(); }
        }
        //public UInt16 WidthRation
        //{
        //    get
        //    {
        //        if ((_dynItem != null) && _dynItem.WidthRationEx.HasValue)
        //            return _dynItem.WidthRationEx.Value;
        //        if (!_staticItem.WidthRation.HasValue)
        //            throw new Exception("have no value!");
        //        return _staticItem.WidthRation.Value;
        //    }
        //    set { InitDynItem(); _dynItem.WidthRationEx = value; }
        //}


        public string Icon
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.Icon != null))
                    return _dynItem.Icon;
                return (StaticUIConfig == null) ? null : StaticUIConfig.Icon;
            }
            set { InitDynItem(); _dynItem.Icon = value; OnModified(); }
        }

        public IBxRange Range
        {
            get
            {
                return StaticUIConfig.Range;
            }
        }

        public string MenuWidth
        {
            get
            {
                return StaticUIConfig.MenuWidth;
            }
        }


        public string HelpString
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.Tip != null))
                    return _dynItem.Tip;
                return (StaticUIConfig == null) ? null : StaticUIConfig.Tip;
            }
            set { InitDynItem(); _dynItem.Tip = value; OnModified(); }
        }

        public IBxSubColumns SubColumns
        {
            get
            {
                if (_subColumns == BxSubColumns.Invalid)
                {
                    _subColumns = null;

                    BxXmlUIItem suic = StaticUIConfig;
                    if ((suic != null) && (suic.SubColums != null))
                        _subColumns = new BxSubColumns(suic.SubColums);
                }
                return _subColumns;
            }
        }
        #endregion

        public Int32? ControlTypeEx
        {
            get
            {
                if (_dynItem.ControlTypeEx != null)
                    return _dynItem.ControlTypeEx.Value;
                return (StaticUIConfig == null) ? null : StaticUIConfig.ControlType;
            }
            set { InitDynItem(); _dynItem.ControlTypeEx = value; OnModified(); }
        }

        public void SwitchUnit(Int32 index)
        {
            if (StaticUIConfig.Unit == null)
                throw new Exception("no unit to switch!");

            if (_dynItem.Unit != null)
            {
                //如果当前的单位类别和目标单位类别相同时，不作任何操作
                if (_dynItem.Unit.Category.ID != StaticUIConfig.Unit.GetUnitCate(index))
                    _dynItem.Unit = null;
            }
            _curUnitIndex = index;
            OnModified();
        }
        public bool NeedSave()
        {
            return _dynItem != null;
        }

        #region IBxPersistStorageNode 成员
        public void SaveStorageNode(IBxStorageNode node)
        {
            if (_dynItem != null)
                _dynItem.SaveStorageNode(node);
        }
        public void LoadStorageNode(IBxStorageNode node)
        {
            _dynItem = new BxUIConfigItem();
            _dynItem.LoadStorageNode(node);
        }
        #endregion


        void OnModified()
        {
            if (_carrier != null)
                _carrier.AddModified(this);
        }

        #region IBxModifyManage 成员
        public void ResetModifyFlag()
        {
            if (_carrier != null)
                _carrier.RemoveModified(this);
        }
        #endregion


        public int DecimalDigits
        {
            get
            {
                return StaticUIConfig.DecimalDigits;
            }
            set
            {
            }
        }

        public string ColumnID
        {
            get
            {
                return (StaticUIConfig == null) ? null : StaticUIConfig.UIColumnID;
            }
        }
    }


}
