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

    public class BxDefaultDataItem : IBxDefaultData
    {
        string _id = null;
        BxUnitDouble _data = new BxUnitDouble();
        string _value = null;

        public BxDefaultDataItem() { }
        public BxDefaultDataItem(string id) { _id = id; }
        public BxDefaultDataItem(string id, BxUnitDouble data) { _id = id; _data = data; }
        public BxDefaultDataItem(XmlElement node)
        {
            LoadXmlNode(node);
        }

        public void LoadXmlNode(XmlElement node)
        {
            string s = node.GetAttribute("id");
            if (!string.IsNullOrEmpty(s))
                _id = s;

            string data = node.GetAttribute("data");
            string cate = node.GetAttribute("unitCate");
            if (string.IsNullOrEmpty(data) || string.IsNullOrEmpty(cate))
            {
                _value = data;
                _data = null;
            }
            else
            {
                string unit = node.GetAttribute("unit");
                _data.SetUIValue(data, cate, unit);
                _value = _data.SaveToString();
            }
        }

        #region IBxDefaultUnit 成员
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        //用来取有单位的默认数据
        public BxUnitDouble DefaultData
        {
            get { return _data; }
            set { _data = value; }
        }
        //用来取无单位的默认数据
        public string Value
        {
            get { return _value; }
        }
        #endregion
    }

}
