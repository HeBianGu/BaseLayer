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

    public class BxDUProvider : IBxDUProvider
    {
        string _id = null;
        List<BxDefaultUnitItem> _units = new List<BxDefaultUnitItem>();

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
                _units.Add(new BxDefaultUnitItem(one));
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
            BxDefaultUnitItem temp;
            foreach (XmlElement one in nodes)
            {
                temp = new BxDefaultUnitItem(one);
                _units.Add(temp);
            }
        }

        public BxDefaultUnitItem Find(string id)
        {
            return _units.Find(x => x.ID == id);
        }

        #region IBxDUProvider 成员
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public IBxDefaultUnit Get(string id)
        {
            return _units.Find(x => x.ID == id);
        }
        public BxDefaultUnitItem GetEx(string id)
        {
            return _units.Find(x => x.ID == id);
        }
        #endregion
    }



}
