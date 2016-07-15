using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{

    public class BUUnits : IBxUnitsCenter, IBLPersistXmlNode
    {
        protected IBxUnitCategory[] m_cates = null;

        public BUUnits() { }

        public void LoadUnitConfigFile(string sFilePath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(sFilePath);
                XmlElement root = doc.DocumentElement;
                XmlNodeList cateNodes = root.SelectNodes("Category");
                List<IBxUnitCategory> cates = new List<IBxUnitCategory>();
                UnitCategory cate;
                foreach (XmlElement one in cateNodes)
                {
                    cate = new UnitCategory();
                    cate.LoadUnitConfigNode(one);
                    cates.Add(cate);
                }
                cates.Sort((x, y) => string.Compare(x.ID , y.ID));
                m_cates = cates.ToArray();
            }
            catch (System.Exception) { }
        }

        #region IBxUnitsCenter 成员
        public IEnumerable<IBxUnitCategory> Categories { get { return m_cates; } }
        public IBxUnitCategory Parse(string sCategoryID)
        {
            foreach (IBxUnitCategory one in m_cates)
            {
                if (one.ID == sCategoryID)
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
        public IBxUnitCategory this[int nIndex] { get { return m_cates[nIndex]; } }
        #endregion

        //#region IBCPersistXml 成员
        //public void Save(XmlElement node, BCPersistHelp help)
        //{
        //    XmlElement catesNode = node.EMAddChild("_Cates_");
        //    help.SaveArray(catesNode, m_cates);
        //}
        //public void Load(XmlElement node, BCPersistHelp help)
        //{
        //    XmlElement catesNode = (XmlElement)node.SelectSingleNode("_Cates_");
        //    help.LoadArray(catesNode, out m_cates);
        //}
        //#endregion

        #region IBLPersistXmlNode 成员
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

    //static public class BUUnitConvert
    //{
    //    static public double EMConvert(this double srcValue, IBxUnit srcUnit, IBxUnit trgUnit)
    //    {
    //        IBxUnitCategory cate = srcUnit.Category;
    //        return cate.GetFormula(srcUnit, trgUnit).Calc(srcValue);
    //    }

    //    static public double EMConvert(this double srcValue, IBxUnitsCenter units, string srcUnit, string trgUnit, string cate)
    //    {
    //        IBxUnitCategory _cate = units.Parse(cate);
    //        return _cate.GetFormula(_cate.Parse(srcUnit), _cate.Parse(trgUnit)).Calc(srcValue);
    //    }

    //    static public double EMConvert(this IBxUnitsCenter units, double srcValue, string srcUnit, string trgUnit, string cate)
    //    {
    //        IBxUnitCategory _cate = units.Parse(cate);
    //        return _cate.GetFormula(_cate.Parse(srcUnit), _cate.Parse(trgUnit)).Calc(srcValue);
    //    }
    //}

    public class BUUnitsProvider
    {
        protected string m_sConfigFilePath = null;
        protected string m_sParsedFilePath = null;
        protected BUUnits m_units = new BUUnits();
        public IBxUnitsCenter Units { get { return m_units; } }

        public string ConfigFilePath
        {
            get { return m_sConfigFilePath; }
            set { m_sParsedFilePath = value; }
        }
        public string ParsedFilePath
        {
            get { return m_sParsedFilePath; }
            set { m_sParsedFilePath = value; }
        }

        public BUUnitsProvider() { }
        public BUUnitsProvider(string sConfigFilePath, string sParsedFilePath)
        {
            m_sConfigFilePath = sConfigFilePath;
            m_sParsedFilePath = sParsedFilePath;
        }

        public void Init()
        {
            BCVersion bufferVersion = GetVersion(m_sParsedFilePath);
            bool bNeedRebuildBuffer = false;
            if (bufferVersion == null)
                bNeedRebuildBuffer = true;
            else
            {
                BCVersion srcConfigVersion = GetVersion(m_sConfigFilePath);
                if (srcConfigVersion > bufferVersion)
                    bNeedRebuildBuffer = true;
            }

            if (bNeedRebuildBuffer)
            {
                m_units.LoadUnitConfigFile(m_sConfigFilePath);
            }
            else
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(m_sParsedFilePath);
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

    public class BCVersion
    {
        protected int[] m_parts = new int[4];
        public int this[int nIndex] { set { m_parts[nIndex] = value; } get { return m_parts[nIndex]; } }

        public BCVersion() { }
        public BCVersion(int part1, int part2 = 0, int part3 = 0, int part4 = 0)
        {
            m_parts[0] = part1; m_parts[1] = part2;
            m_parts[2] = part3; m_parts[3] = part4;
        }

        public override bool Equals(object obj) { return m_parts.Equals(obj); }
        public override int GetHashCode() { return m_parts.GetHashCode(); }

        static public bool operator ==(BCVersion ver1, BCVersion ver2)
        {
            if (object.ReferenceEquals(ver1, ver2))
                return true;

            for (int i = 0; i < 4; i++)
            {
                if (ver1[i] != ver2[i])
                    return false;
            }
            return true;
        }
        static public bool operator !=(BCVersion ver1, BCVersion ver2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (ver1[i] == ver2[i])
                    return false;
            }
            return true;
        }
        static public bool operator >(BCVersion ver1, BCVersion ver2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (ver1[i] < ver2[i])
                    return false;
            }
            return (ver1[3] > ver2[3]);
        }
        static public bool operator <(BCVersion ver1, BCVersion ver2)
        {

            return (ver2 > ver1);
        }
        static public bool operator >=(BCVersion ver1, BCVersion ver2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (ver1[i] < ver2[i])
                    return false;
            }
            return true;
        }
        static public bool operator <=(BCVersion ver1, BCVersion ver2)
        {
            return (ver2 >= ver1);
        }

        public bool FromString(string s)
        {
            int[] temp = new int[4];

            string[] arrParts = s.Split(".".ToArray(), 4);
            int nIndex = 0;
            int var = 0;
            foreach (string one in arrParts)
            {
                temp[nIndex] = Int32.TryParse(one, out var) ? var : 0;
                nIndex++;
            }
            for (; nIndex < 4; nIndex++)
                temp[nIndex] = 0;
            m_parts = temp;
            return true;
        }
        public override string ToString()
        {
            return m_parts[0].ToString() + "." + m_parts[1].ToString() + "." + m_parts[2].ToString() + "." + m_parts[3].ToString();
        }
        static public BCVersion Parse(string s)
        {
            BCVersion outVar = new BCVersion();
            outVar.FromString(s);
            return outVar.FromString(s) ? outVar : null;
        }
    }

    public class BCLongVersion
    {
        BCVersion[] m_vers = new BCVersion[2];
    }
}
