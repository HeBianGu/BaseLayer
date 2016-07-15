using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxUnitsCenter : IBxUnitsCenter, IBxPersistXmlNode
    {
        protected IBxUnitCategory[] m_cates = null;

        public BxUnitsCenter() { }

        public void LoadUnitConfigFile(string sFilePath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(sFilePath);
                XmlElement root = doc.DocumentElement;
                XmlNodeList cateNodes = root.SelectNodes("Category");
                List<IBxUnitCategory> cates = new List<IBxUnitCategory>();
                BxUnitCategory cate;
                foreach (XmlElement one in cateNodes)
                {
                    cate = new BxUnitCategory();
                    cate.LoadUnitConfigNode(one);
                    cates.Add(cate);
                }
                //cates.Sort((x, y) => string.Compare(x.ID, y.ID));
                m_cates = cates.ToArray();
            }
            catch (System.Exception) { }
        }

        #region IBxUnitsCenter 成员
        public IEnumerable<IBxUnitCategory> Categories { get { return m_cates; } }
        public IBxUnitCategory Parse(string categoryID)
        {
            foreach (IBxUnitCategory one in m_cates)
            {
                if (one.ID == categoryID)
                    return one;
            }
            return null;
        }
        public IBxUnitCategory Find(string code)
        {
            foreach (IBxUnitCategory one in m_cates)
            {
                if (one.Code == code)
                    return one;
            }
            return null;
        }
        #endregion
        public IBxUnitCategory this[int nIndex] { get { return m_cates[nIndex]; } }

        #region IBxPersistXmlNode 成员
        public void SaveXml(XmlElement node)
        {
            throw new NotImplementedException();
        }
        public void LoadXml(XmlElement node)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class BxUnitsProvider : BxUnitsCenter
    {
        protected string _sConfigFilePath = null;
        protected string _sParsedFilePath = null;

        public string ConfigFilePath
        {
            get { return _sConfigFilePath; }
            set { _sConfigFilePath = value; }
        }
        public string ParsedFilePath
        {
            get { return _sParsedFilePath; }
            set { _sParsedFilePath = value; }
        }

        public BxUnitsProvider() { }
        public BxUnitsProvider(string sConfigFilePath, string sParsedFilePath)
        {
            _sConfigFilePath = sConfigFilePath;
            _sParsedFilePath = sParsedFilePath;
        }

        public void Init()
        {
            BCVersion bufferVersion = GetVersion(_sParsedFilePath);
            bool bNeedRebuildBuffer = false;
            if (bufferVersion == null)
                bNeedRebuildBuffer = true;
            else
            {
                BCVersion srcConfigVersion = GetVersion(_sConfigFilePath);
                if (srcConfigVersion > bufferVersion)
                    bNeedRebuildBuffer = true;
            }

            if (bNeedRebuildBuffer)
            {
                LoadUnitConfigFile(_sConfigFilePath);
            }
            else
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(_sParsedFilePath);
                    XmlElement root = doc.DocumentElement;
                    //m_units.Load(root);
                }
                catch (System.Exception) { }
            }
        }
        public static bool GetConfigVersion(string sFilePath, out string majorVer, out string minorVer)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(sFilePath);
                XmlElement root = doc.DocumentElement;
                majorVer = root.GetAttribute("majorVer");
                minorVer = root.GetAttribute("minorVer");
                return true;
            }
            catch (System.Exception) { }
            majorVer = null;
            minorVer = null;
            return false;
        }

        public static BCVersion GetVersion(string xmlFilePath)
        {
            string sVer = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFilePath);
                XmlElement root = doc.DocumentElement;
                sVer = root.GetAttribute("_version_");
            }
            catch (System.Exception) { }
            if (String.IsNullOrEmpty(sVer))
                return null;
            return BCVersion.Parse(sVer);
        }
    }

   
}
