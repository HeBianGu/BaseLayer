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

    public class BxDDProvider : IBxDDProvider
    {
        string _id = null;
        List<BxDefaultDataItem> _units = new List<BxDefaultDataItem>();

        public void LoadSystemXml(string xmlFilePath)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(xmlFilePath);
            }
            catch (Exception)
            {
                return;
            }

            XmlNodeList nodes = doc.SelectNodes("//Unit");
            _units.Clear();
            _units.Capacity = nodes.Count;
            foreach (XmlElement one in nodes)
            {
                _units.Add(new BxDefaultDataItem(one));
            }
        }

        public void LoadUserXml(string xmlFilePath)
        {
            _units.Clear();
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(xmlFilePath);
            }
            catch (Exception)
            {
                return;
            }

            XmlNodeList nodes = doc.SelectNodes("//Unit");
            BxDefaultDataItem temp;
            foreach (XmlElement one in nodes)
            {
                temp = new BxDefaultDataItem(one);
                _units.Add(temp);
            }
        }

        public BxDefaultDataItem Find(string id)
        {
            return _units.Find(x => x.ID == id);
        }

        #region IBxDDProvider 成员
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public IBxDefaultData Get(string id)
        {
            return _units.Find(x => x.ID == id);
        }
        public BxDefaultDataItem GetEx(string id)
        {
            return _units.Find(x => x.ID == id);
        }
        #endregion
    }



}
