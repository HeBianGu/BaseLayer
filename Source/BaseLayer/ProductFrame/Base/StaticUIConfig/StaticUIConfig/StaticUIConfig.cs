using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;
using System.Globalization;


namespace OPT.Product.Base
{

    public class BxXmlUIItem
    {
        #region members
        IBxStaticUIConfigProvider _suicProvider = null;
        protected IBxUIConfigProvider _uicProvider = null;

        protected bool _inited = false;
        protected string _id = null;
        protected string _name = null;
        protected BxUIItemFlag _flag = null;
        protected Int32? _controlType = -1;
        protected BxUnitList _unit = null;
        protected Int32 _decimalDigits = -1;
        protected string _column = null;
        string _uiColumnName = null;

        protected BxSUICSubColums _suicSubColums = null;
        protected string _subArrayUIItemID = null;

        protected string _Icon = null;
        protected BxRange _range = null;
        protected string _menuWidth = null;
        
        protected string _tip = null;

        #endregion

        public IBxStaticUIConfigProvider SUICProvider { get { return _suicProvider; } }
        // public IBxUIConfigProvider UIConfigProvider { get { return _xmlProvider; } }
        // public string XmlFileID { get { return _xmlFileID; } }


        #region construct
        public BxXmlUIItem()
        {
        }
        public BxXmlUIItem(string fullID, IBxStaticUIConfigProvider suicProvider, IBxUIConfigProvider uicProvider)
        {
            _id = fullID;
            _suicProvider = suicProvider;
            _uicProvider = uicProvider;
        }
        #endregion

        #region properties
        public string FullID { get { return _id; } }
        public string FileID { get { return BxUIConfigID.GetFileID(_id); } }
        public string ID { get { return BxUIConfigID.GetItemID(_id); } }
        public string Name { get { Init(); return _name; } }
        public bool? Show { get { Init(); return _flag.Show; } }
        public bool? ShowTitle { get { Init(); return _flag.ShowTitle; } }
        public bool? Expand { get { Init(); return _flag.Expand; } }
        public bool? UserHide { get { Init(); return _flag.UserHide; } }
        public bool? ReadOnly { get { Init(); return _flag.ReadOnly; } }
        public bool? ValueReadOnly { get { Init(); return _flag.ValueReadOnly; } }
        public bool? Fold { get { Init(); return _flag.Fold; } }

        public BxUnitList Unit { get { Init(); return _unit; } }
        public int DecimalDigits { get { Init(); return _decimalDigits; } }
        
        public Int32? ControlType { get { Init(); return _controlType; } }
        public string UIColumnID { get { Init(); return _column; } }
        public string UIColumnName
        {
            get
            {
                if (_uiColumnName == null)
                {
                    IBxUIConfigFile file = _uicProvider.GetUIConfigFile(BxUIConfigID.GetFileID(_id));
                    XmlElement columnNode = file.GetUIColumn(UIColumnID);
                    if (columnNode == null)
                        return null;
                    _uiColumnName = columnNode.GetAttribute(BxXmlConfigField.name);
                }
                return _uiColumnName;
            }
        }

        public BxSUICSubColums SubColums { get { Init(); return _suicSubColums; } }
        public string SubArrayUIItemID
        {
            get { Init(); return _subArrayUIItemID; }
        }
        public string SubArrayUIItemFullID
        {
            get { return BxUIConfigID.Combine(FileID, SubArrayUIItemID); }
        }
        public BxXmlUIItem SubArrayUIItem
        {
            get
            {
                if (string.IsNullOrEmpty(_subArrayUIItemID))
                    return null;
                return _suicProvider.GetStaticUIConfig(_subArrayUIItemID);
            }
        }

        public string Icon { get { Init(); return _Icon; } }
        public BxRange Range { get { Init(); return _range; } }
        public string MenuWidth { get { Init(); return _menuWidth; } }
        public string Tip { get { Init(); return _tip; } }
        #endregion

        #region   methods
        protected void Init()
        {
            if (_inited)
                return;
            _inited = true;

            XmlElement node;
            IBxUIConfigFile _xmlFile;
            _uicProvider.FindUIConfigItem(_id, out node, out _xmlFile);
            if (node == null)
                throw new Exception(string.Format("Can not find item {0} From UIConfig file{1}.", _id, _xmlFile.ID));

            //取itemID
            //_id  = node.GetAttribute(BxXmlConfigField.id);

            //取name
            _name = node.GetAttribute(BxXmlConfigField.name);

            //取标志位
            #region 取标志位
            _flag = new BxUIItemFlag();
            string s;

            s = node.GetAttribute(BxXmlConfigField.show);
            if (!string.IsNullOrEmpty(s))
                _flag.Show = S_ParseBool(s);

            s = node.GetAttribute(BxXmlConfigField.showTitle);
            if (!string.IsNullOrEmpty(s))
                _flag.ShowTitle = S_ParseBool(s);

            s = node.GetAttribute(BxXmlConfigField.expand);
            if (!string.IsNullOrEmpty(s))
                _flag.Expand = S_ParseBool(s);

            s = node.GetAttribute(BxXmlConfigField.userHide);
            if (!string.IsNullOrEmpty(s))
                _flag.UserHide = S_ParseBool(s);

            s = node.GetAttribute(BxXmlConfigField.readOnly);
            if (!string.IsNullOrEmpty(s))
                _flag.ReadOnly = S_ParseBool(s);

            s = node.GetAttribute(BxXmlConfigField.valueReadOnly);
            if (!string.IsNullOrEmpty(s))
                _flag.ValueReadOnly = S_ParseBool(s);

            s = node.GetAttribute(BxXmlConfigField.fold);
            if (!string.IsNullOrEmpty(s))
                _flag.Fold = S_ParseBool(s);

            s = node.GetAttribute(BxXmlConfigField.controlType);
            if (!string.IsNullOrEmpty(s))
                _controlType = S_ParseNullInt(s);
            #endregion

            //取subArrayUIItemID
            _subArrayUIItemID = node.GetAttribute(BxXmlConfigField.arrayElement);

            //取ICOn
            _Icon = node.GetAttribute(BxXmlConfigField.icon);

            double temp;
            double? min = null;
            bool minValid = false;
            double? max = null;
            bool maxValid = false; 

            s = node.GetAttribute(BxXmlConfigField.min);
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
            {
                min = temp;
                minValid = false;
            }
            else
            {
                s = node.GetAttribute(BxXmlConfigField.minEx);
                if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                {
                    min = temp;
                    minValid = true;
                }
            }
           
            s = node.GetAttribute(BxXmlConfigField.max);
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
            {
                max = temp;
                maxValid = false;
            }
            else
            {
                s = node.GetAttribute(BxXmlConfigField.maxEx);
                if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                {
                    max = temp;
                    maxValid = true;
                }
            }
            if (min.HasValue || max.HasValue)
            {
                _range = new BxRange(min,minValid,max,maxValid);
            }

            //取subArrayUIItemID
            _menuWidth = node.GetAttribute(BxXmlConfigField.menuWidth);

            //取HelpString
            _tip = node.GetAttribute(BxXmlConfigField.HelpString);


            //取单位
            #region 取单位
            s = node.GetAttribute(BxXmlConfigField.unitCate);
            if (!string.IsNullOrEmpty(s))
            {
                string unit = node.GetAttribute(BxXmlConfigField.unit);
                _unit = new BxUnitList(s, unit);
            }
            else
            {
                string unitItem = node.GetAttribute(BxXmlConfigField.unitItem);
                if (!string.IsNullOrEmpty(unitItem))
                {
                    XmlNodeList unitItems = node.SelectNodes(BxXmlConfigNodeName.unitItemNode);
                    string[] units = new string[unitItems.Count];
                    string[] unitCates = new string[unitItems.Count];
                    int i = 0;
                    foreach (XmlElement one in unitItems)
                    {
                        unitCates[i] = one.GetAttribute(BxXmlConfigField.unitCate);
                        units[i] = one.GetAttribute(BxXmlConfigField.unit);
                    }
                    _unit = new BxUnitList(unitCates, units);
                }
            }
            #endregion

            //取decimalDigits
            s = node.GetAttribute(BxXmlConfigField.decimalDigits);
            if (!string.IsNullOrEmpty(s))
                _decimalDigits = S_ParseInt(s);

            //取columnID
            _column = node.GetAttribute(BxXmlConfigField.uiColumn);

            //取subColumns
            #region 取subColumns
            s = node.GetAttribute(BxXmlConfigField.subColumns);
            if (!string.IsNullOrEmpty(s))
            {
                Int32 centerSubCol = S_ParseInt(node.GetAttribute(BxXmlConfigField.centerSubCol));
                XmlNodeList child = node.SelectNodes(BxXmlConfigNodeName.subColNode);
                _suicSubColums = new BxSUICSubColums(child.Count, centerSubCol);

                string fileID = BxUIConfigID.GetFileID(_id);
                string id;
                UInt16 ratio;
                int index = 0;
                foreach (XmlElement one in child)
                {
                    id = BxUIConfigID.Combine(fileID, one.GetAttribute(BxXmlConfigField.id));
                    ratio = S_ParseUInt16(one.GetAttribute(BxXmlConfigField.width));
                    _suicSubColums.InitColumn(index, id, _suicProvider, ratio);
                    index++;
                }
            }
            #endregion
        }
        public void InitProvider(IBxStaticUIConfigProvider suicProvider, IBxUIConfigProvider uicProvider)
        {
            _inited = false;
            _suicProvider = suicProvider;
            _uicProvider = uicProvider;
        }
        #endregion

        #region static method
        static protected bool? S_ParseBool(string val)
        {
            bool result;
            if (bool.TryParse(val, out result))
                return result;
            return null;
        }
        static protected Int32 S_ParseInt(string val)
        {
            Int32 result;
            if (Int32.TryParse(val, out result))
                return result;
            return -1;
        }
        static protected UInt16 S_ParseUInt16(string val)
        {
            UInt16 result;
            if (UInt16.TryParse(val, out result))
                return result;
            return UInt16.MaxValue;
        }
        static protected Int32? S_ParseNullInt(string val)
        {
            Int32 result;
            if (Int32.TryParse(val, out result))
                return result;
            return null;
        }
        static protected UInt16? S_ParseNullUInt16(string val)
        {
            UInt16 result;
            if (UInt16.TryParse(val, out result))
                return result;
            return null;
        }
        #endregion

        public static BxXmlUIItem Invalid = new BxXmlUIItem();
    }
}