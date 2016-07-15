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
    public class BLXmlUIConfigGroup : IBLUIConfigGroup
    {
        protected List<BLXmlConfigItem> _items = new List<BLXmlConfigItem>();
        protected List<BLUIConfigColumn> _columns = new List<BLUIConfigColumn>();
        protected XmlElement _xmlNode;
        BLXmlConfigItem _defaultItem = null;

        protected BLXmlUIConfigGroup() { _xmlNode = null; }
        public BLXmlUIConfigGroup(XmlElement xmlNode)
        {
            _xmlNode = xmlNode;
            XmlElement defaultValue = xmlNode.SelectSingleNode("UIConfigGroup/DefaultValue") as XmlElement;
            _defaultItem = CreateItem(defaultValue);
        }

        #region IBLConfigGroup 成员
        public string Name { get; set; }
        public IBLUIConfig GetItemConfig(int configItemID)
        {
            BLXmlConfigItem item = _items.Find(x => x.ID == configItemID);
            if ((item == null) && (_xmlNode != null))
            {
                XmlElement node = _xmlNode.SelectSingleNode(string.Format(".//UIItem[@id={0}]", configItemID)) as XmlElement;
                item = CreateItem(_defaultItem, node);
            }
            return item;
        }
        public IBLUIConfigColumn GetColumn(Int32 id)
        {
            BLUIConfigColumn item = _columns.Find(x => x.ID == id);
            if ((item == null) && (_xmlNode != null))
            {
                XmlElement node = _xmlNode.SelectSingleNode(string.Format(".//UIColumn[@id={0}]", id)) as XmlElement;
                item = new BLUIConfigColumn();
                item.ID = id;
                item.Name = node.GetAttribute("name");
                _columns.Add(item);
            }
            return item;
        }

        #endregion

        BLXmlConfigItem CreateItem(BLXmlConfigItem defaultItem, XmlElement node)
        {
            BLXmlConfigItem item = defaultItem.Copy();
            InitItem(item, node);
            return item;
        }
        BLXmlConfigItem CreateItem(XmlElement node)
        {
            BLXmlConfigItem item = new BLXmlConfigItem();
            InitItem(item, node);
            return item;
        }

        void InitItem(BLXmlConfigItem item, XmlElement node)
        {
            string s = node.GetAttribute(BLXmlConfigItem.s_itemID.key);
            if (!string.IsNullOrEmpty(s))
                item.ID = S_ParseInt(s);

            s = node.GetAttribute(BLXmlConfigItem.s_itemName.key);
            if (!string.IsNullOrEmpty(s))
                item.Name = s;

            s = node.GetAttribute(BLXmlConfigItem.s_itemShow.key);
            if (!string.IsNullOrEmpty(s))
                item.Show = S_ParseBool(s);

            s = node.GetAttribute(BLXmlConfigItem.s_itemShowTitle.key);
            if (!string.IsNullOrEmpty(s))
                item.ShowTitle = S_ParseBool(s);

            s = node.GetAttribute(BLXmlConfigItem.s_itemExpand.key);
            if (!string.IsNullOrEmpty(s))
                item.Expand = S_ParseBool(s);

            s = node.GetAttribute(BLXmlConfigItem.s_itemUserHide.key);
            if (!string.IsNullOrEmpty(s))
                item.UserHide = S_ParseBool(s);

            s = node.GetAttribute(BLXmlConfigItem.s_itemReadOnly.key);
            if (!string.IsNullOrEmpty(s))
                item.ReadOnly = S_ParseBool(s);

            s = node.GetAttribute(BLXmlConfigItem.s_itemValueReadOnly.key);
            if (!string.IsNullOrEmpty(s))
                item.ValueReadOnly = S_ParseBool(s);

            s = node.GetAttribute(BLXmlConfigItem.s_itemFold.key);
            if (!string.IsNullOrEmpty(s))
                item.Fold = S_ParseBool(s);

            s = node.GetAttribute(BLXmlConfigItem.s_itemControlType.key);
            if (!string.IsNullOrEmpty(s))
                item.ControlType = S_ParseInt(s);

            string unitCate = node.GetAttribute("unitCate");
            s = node.GetAttribute(BLXmlConfigItem.s_itemUnit.key);
            item.Unit = S_ParseUnit(unitCate, s);

            s = node.GetAttribute(BLXmlConfigItem.s_itemColumn.key);
            int columnID = S_ParseInt(s);
            if (columnID > 0)
                item.Column = GetColumn(columnID);

            item.MultiColumn = CreateMultiColumn(node);

            //s = node.GetAttribute(BLXmlConfigItem.s_itemRatio.key);
            //item.Ratio = S_ParseRatio(s);
        }

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
        static protected IBxUnit S_ParseUnit(string unitCate, string unit)
        {
            IBxUnitsCenter units = BLSystemInfo.Instance.Units;
            IBxUnitCategory cate = units.Parse(unitCate);
            if (cate != null)
                return cate.Parse(unit);
            return null;
        }
        protected IBLUIConfigMultiColumn CreateMultiColumn(XmlElement node)
        {
            string s = node.GetAttribute(BLXmlConfigItem.s_itemMultiColumn.key);
            Int32 multiColumn = -1;
            if (!Int32.TryParse(s, out multiColumn) || (multiColumn < 2))
                return null;
            XmlNodeList child = node.SelectNodes("Column");
            IBLUIConfig[] columns = new IBLUIConfig[multiColumn];
            UInt16[] ratios = new UInt16[multiColumn];
            int index = -1;
            foreach (XmlElement one in child)
            {
                index++;
                columns[index] = GetItemConfig(Int32.Parse(one.GetAttribute("id")));
                ratios[index] = UInt16.Parse(one.GetAttribute("width"));
            }
            return new BLUIConfigMultiColumn(columns, ratios);
        }
    }
}