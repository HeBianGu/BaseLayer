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

    public class BxDefaultUnitItem : IBxDefaultUnit
    {
        string _id = null;
        IBxUnit _unit = null;

        public BxDefaultUnitItem() { }
        public BxDefaultUnitItem(string id) { _id = id; }
        public BxDefaultUnitItem(string id, IBxUnit unit) { _id = id; _unit = unit; }
        public BxDefaultUnitItem(XmlElement node)
        {
            LoadXmlNode(node);
        }

        public void LoadXmlNode(XmlElement node)
        {
            string s = node.GetAttribute("id");
            if (!string.IsNullOrEmpty(s))
                _id = s;

            string cate = node.GetAttribute("unitCate");
            string unit = node.GetAttribute("unit");
            _unit = BxSystemInfo.Instance.UnitsCenter.EMParse(cate, unit);
        }

        #region IBxDefaultUnit 成员
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public IBxUnit DefaultUnit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        #endregion
    }

}
