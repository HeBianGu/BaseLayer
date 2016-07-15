using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxUIConfigFromSuic : IBxUIConfig, IBxPersistStorageNode, IBxModifyManage
    {
        #region members
        protected IBxElementCarrier _carrier = null;
        protected IBxStaticUIConfigPregnant _suicPregnant = null;
        protected BxUIConfig _dynItem = null;
        #endregion

        #region functions
        public BxUIConfigFromSuic() { }
        public BxUIConfigFromSuic(string fullID)
        {
            FullID = fullID;
            InitDefaultUnit();
        }
        public BxUIConfigFromSuic(IBxStaticUIConfigPregnant suicPregnant)
        {
            _suicPregnant = suicPregnant;
            InitDefaultUnit();
        }
        public BxUIConfigFromSuic(IBxElementCarrier carrier, IBxStaticUIConfigPregnant suicPregnant)
        {
            _carrier = carrier;
            _suicPregnant = suicPregnant;
            InitDefaultUnit();
        }
        public BxXmlUIItem StaticUIConfig
        {
            get
            {
                if (_suicPregnant != null)
                    return _suicPregnant.StaticUIConfig;
                return null;
            }
        }
        protected void InitDynItem()
        {
            if (_dynItem == null)
            {
                _dynItem = new BxUIConfig();
                _dynItem.ResetCarrier(_carrier);

                if ((StaticUIConfig != null) && (StaticUIConfig.SubColums != null))
                    _dynItem.SubColumnsEx = new BxMutiColumns(StaticUIConfig.SubColums);
            }
        }
        protected void InitDefaultUnit()
        {
            string id = FullID;
            if (!string.IsNullOrEmpty(id))
            {
                InitDynItem();
                _dynItem.InitDefaultUnit(id);
            }
        }

        public Int32? ControlTypeEx
        {
            get
            {
                if (_dynItem.ControlTypeEx != null)
                    return _dynItem.ControlTypeEx.Value;
                return (StaticUIConfig == null) ? null : StaticUIConfig.ControlType;
            }
            set { InitDynItem(); _dynItem.ControlTypeEx = value; }
        }
        public bool NeedSave()
        {
            if (_dynItem != null)
                return _dynItem.NeedSave();
            return false;
        }
        public void ResetCarrier(IBxElementCarrier carrier)
        {
            _carrier = carrier;
            if (_dynItem != null)
            {
                _dynItem.ResetCarrier(_carrier);
            }
        }
        public void ResetSUICPregnant(IBxStaticUIConfigPregnant suicPregnant)
        {
            _suicPregnant = suicPregnant;
        }
        #endregion

        #region IBxUIConfig 成员
        public int ID
        {
            get { return BxUIConfigID.GetItemIDInt(FullID); }
        }
        public string FullID
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.FullID != null))
                    return _dynItem.FullID;
                return (_suicPregnant == null) ? null : _suicPregnant.ConfigID;
            }
            set
            {
                InitDynItem();
                _dynItem.FullID = value;

                IBxStaticUIConfigProvider p = (_carrier != null) ? _carrier.SCICProvider : BxSystemInfo.Instance.SUICProvider;
                _suicPregnant = new BxSUICPregnant(value, p);
            }
        }
        public string Name
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.Name != null))
                    return _dynItem.Name;
                return (StaticUIConfig == null) ? null : StaticUIConfig.Name;
            }
            set { InitDynItem(); _dynItem.Name = value; }
        }
        public bool? Show
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.Show != null))
                    return _dynItem.Show;
                return (StaticUIConfig == null) ? null : StaticUIConfig.Show;
            }
            set { InitDynItem(); _dynItem.Show = value; }
        }

        public bool? ShowTitle
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.ShowTitle != null))
                    return _dynItem.ShowTitle;
                return (StaticUIConfig == null) ? null : StaticUIConfig.ShowTitle;
            }
            set { InitDynItem(); _dynItem.ShowTitle = value; }
        }
        public bool? Expand
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.Expand != null))
                    return _dynItem.Expand;
                return (StaticUIConfig == null) ? null : StaticUIConfig.Expand;
            }
            set { InitDynItem(); _dynItem.Expand = value; }
        }
        public bool? UserHide
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.UserHide != null))
                    return _dynItem.UserHide;
                return (StaticUIConfig == null) ? null : StaticUIConfig.UserHide;
            }
            set { InitDynItem(); _dynItem.UserHide = value; }
        }
        public bool? ReadOnly
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.ReadOnly != null))
                    return _dynItem.ReadOnly;
                return (StaticUIConfig == null) ? null : StaticUIConfig.ReadOnly;
            }
            set { InitDynItem(); _dynItem.ReadOnly = value; }
        }
        public bool? ValueReadOnly
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.ValueReadOnly != null))
                    return _dynItem.ValueReadOnly;
                return (StaticUIConfig == null) ? null : StaticUIConfig.ValueReadOnly;
            }
            set { InitDynItem(); _dynItem.ValueReadOnly = value; }
        }
        public bool? Fold
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.Fold != null))
                    return _dynItem.Fold;
                return (StaticUIConfig == null) ? null : StaticUIConfig.Fold;
            }
            set { InitDynItem(); _dynItem.Fold = value; }
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
            set { InitDynItem(); _dynItem.ControlType = value; }
        }
        public IBxUnit Unit
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.Unit != null))
                    return _dynItem.Unit;
                if ((StaticUIConfig != null) && StaticUIConfig.Unit != null)
                    return StaticUIConfig.Unit.GetUnit(0);
                return null;
            }
            set
            {
                InitDynItem();
                _dynItem.Unit = value;
            }
        }
        public int DecimalDigits
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.DecimalDigits != -1))
                    return _dynItem.DecimalDigits;
                if (StaticUIConfig != null)
                    return StaticUIConfig.DecimalDigits;
                return -1;
            }
            set
            {
                InitDynItem();
                _dynItem.DecimalDigits = value;
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
            set { InitDynItem(); _dynItem.ColumnName = value; }
        }
        public string ColumnID
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.ColumnID != null))
                    return _dynItem.ColumnID;
                return (StaticUIConfig == null) ? null : StaticUIConfig.UIColumnID;
            }
            set { InitDynItem(); _dynItem.ColumnID = value; }
        }
        public string Icon
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.Icon != null))
                    return _dynItem.Icon;
                return (StaticUIConfig == null) ? null : StaticUIConfig.Icon;
            }
            set { InitDynItem(); _dynItem.Icon = value; }
        }
        public IBxRange Range
        {
            get { return StaticUIConfig.Range; }
            //set {  _range = value; }
        }
        public string MenuWidth
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.MenuWidth != null))
                    return _dynItem.MenuWidth;
                return (StaticUIConfig == null) ? null : StaticUIConfig.MenuWidth;
            }
            set { InitDynItem(); _dynItem.MenuWidth = value; }
        }
        public string HelpString
        {
            get
            {
                if ((_dynItem != null) && (_dynItem.HelpString != null))
                    return _dynItem.HelpString;
                return (StaticUIConfig == null) ? null : StaticUIConfig.Tip;
            }
            set { InitDynItem(); _dynItem.HelpString = value; }
        }
        public IBxSubColumns SubColumns
        {
            get
            {
                InitDynItem();
                return _dynItem.SubColumns;
            }
        }
        #endregion

        public BxMutiColumns SubColumnsEx
        {
            get
            {
                InitDynItem();
                return _dynItem.SubColumnsEx;
            }
        }


        #region IBxPersistStorageNode 成员
        public void SaveStorageNode(IBxStorageNode node)
        {
            if (_dynItem != null)
                _dynItem.SaveStorageNode(node);
        }
        public void LoadStorageNode(IBxStorageNode node)
        {
            //_dynItem = new BxUIConfig();
            InitDynItem();
            _dynItem.LoadStorageNode(node);
        }
        #endregion

        #region IBxModifyManage 成员
        public void ResetModifyFlag()
        {
            if (_dynItem != null)
                _dynItem.ResetModifyFlag();
        }
        #endregion
    }
}
