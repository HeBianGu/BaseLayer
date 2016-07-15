using System;
using System.Collections.Generic;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    //public interface IBxUnit
    //{
    //    string ID { get; }
    //    string Name { get; }
    //    IBxUnitCategory Category { get; }
    //    int Index { get; }
    //}

    //public interface IBxUnitCategory
    //{
    //    string ID { get; }
    //    string Name { get; }
    //    IEnumerable<IBxUnit> Units { get; }
    //    IBxFormula GetFormula(IBxUnit src, IBxUnit trg);
    //    //IBxFormula GetFormula(int srcIndex, int trgIndex);
    //    IBxUnit Parse(string sUnit);
    //    IBxUnit this[int nIndex] { get; }
    //}



    class BUUnit : IBxUnit
    {
        protected string m_id;
        protected string _name;
        protected string _code;
        protected int _dd = 0;
        protected IBxUnitCategory m_cate;
        protected int m_nIndex = -1;

        public BUUnit()
        {
            _name = null;
            m_cate = null;
            _code = null;
        }
        public BUUnit(string id, string name, string code,int dd, IBxUnitCategory cate, int nIndex)
        {
            m_id = id;
            _name = string.IsNullOrEmpty(name) ? null : name;
            _code = string.IsNullOrEmpty(code) ? null : code;
            m_cate = cate;
            _dd = dd;
            m_nIndex = nIndex;
        }
        #region IBxUnit 成员
        public string ID { get { return m_id; } }
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                    return ID;
                return _name;
            }
        }
        public string Code { get { return _code; } }
        public Int32 DecimalDigits { get { return _dd; } }
        public IBxUnitCategory Category { get { return m_cate; } }
        public int Index { get { return m_nIndex; } }
        #endregion
    }

    class _SrcUnitFormulas : IBLPersistXmlNode
    {
        int m_srcUnitIndex;
        IBxFormula[] m_trgUnitFormula;

        public _SrcUnitFormulas(int srcUnitIndex, int nUnitsCount)
        {
            m_srcUnitIndex = srcUnitIndex;
            m_trgUnitFormula = new IBxFormula[nUnitsCount];
            for (int i = 0; i < nUnitsCount; i++)
            {
                m_trgUnitFormula[i] = null;
            }
        }
        public IBxFormula GetFormula(int nIndex)
        {
            return m_trgUnitFormula[nIndex];
        }
        public void SetFormula(int nIndex, IBxFormula formula)
        {
            m_trgUnitFormula[nIndex] = formula;
        }

        //#region IBCPersistXml 成员
        //public void Save(XmlElement node, BCPersistHelp help)
        //{
        //    node.SetAttribute("srcUnitIndex", m_srcUnitIndex.ToString());
        //    node.SetAttribute("count", m_trgUnitFormula.Length.ToString());
        //    XmlElement childNode = null;
        //    int nIndex = 0;
        //    foreach (IBxFormula one in m_trgUnitFormula)
        //    {
        //        childNode = node.EMAddChild("Fml-" + nIndex.ToString());
        //        one.Save(childNode, help);
        //        nIndex++;
        //    }
        //}
        //public void Load(XmlElement node, BCPersistHelp help)
        //{
        //    string s = node.GetAttribute("srcUnitIndex");
        //    m_srcUnitIndex = Convert.ToInt32(s);
        //    s = node.GetAttribute("count");
        //    int count = Convert.ToInt32(s);
        //    m_trgUnitFormula = new IBxFormula[count];
        //    XmlNodeList nodeList = node.ChildNodes;
        //    int nIndex = -1;
        //    foreach (XmlElement one in nodeList)
        //    {
        //        s = one.Name.Substring(4);
        //        nIndex = Convert.ToInt32(s);
        //        m_trgUnitFormula[nIndex] = (IBxFormula)help.DynamicLoadObject(one);
        //    }
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

    class UnitCategory : IBxUnitCategory, IBLPersistXmlNode
    {
        protected string m_id = null;
        protected string _name = null;
        protected string _code = null;

        protected IBxUnit[] m_units = null;
        protected _SrcUnitFormulas[] m_formulas = null;
        protected int m_nDefaultUnitIndex = -1;

        public UnitCategory() { }
        public UnitCategory(string name)
        {
            _name = name;
        }

        public void LoadUnitConfigNode(XmlElement cateNode)
        {
            FormulaParser parser = new FormulaParser();
            try
            {
                m_id = cateNode.GetAttribute("id");
                _name = cateNode.GetAttribute("name");
                _code = cateNode.GetAttribute("code");

                XmlElement unitsNode = (XmlElement)cateNode.SelectSingleNode("Units");
                string defaultUnit = unitsNode.GetAttribute("primaryUnit");

                //获取所有的单位
                XmlNodeList unitsNodeList = cateNode.SelectSingleNode("Units").ChildNodes;
                List<IBxUnit> units = new List<IBxUnit>();
                List<string> fmls = new List<string>();
                int nIndex = 0;
                string id = null;
                string name = null;
                string code = null;
                int dd = 0;
                foreach (XmlElement one in unitsNodeList)
                {
                    id = one.GetAttribute("id");
                    if (id == defaultUnit)
                        m_nDefaultUnitIndex = nIndex;
                    name = one.GetAttribute("name");
                    if (string.IsNullOrEmpty(name))
                        name = id;
                    name = UnitNameConvert.Convert(name);
                    code = one.GetAttribute("code");
                    if (!Int32.TryParse(one.GetAttribute("decimalDigits"), out dd))
                        throw new Exception("no decimal digis in unit " + id + " !");

                    units.Add(new BUUnit(id, name, code, dd,this, nIndex));
                    fmls.Add(one.GetAttribute("defaultUnit"));
                    nIndex++;
                }
                m_units = units.ToArray();
                //end

                //生成默认转换公式
                int nCounts = m_units.Length;
                m_formulas = new _SrcUnitFormulas[nCounts];
                nIndex = 0;
                _SrcUnitFormulas temp = null;
                foreach (IBxUnit one in m_units)
                {
                    temp = new _SrcUnitFormulas(nIndex, nCounts);
                    m_formulas[nIndex] = temp;
                    if (nIndex != m_nDefaultUnitIndex)
                    {
                        parser.Formula = fmls[nIndex];
                        parser.Variant = one.ID;
                        temp.SetFormula(m_nDefaultUnitIndex, new ParsedFormula(parser.Parse()));
                    }
                    nIndex++;
                }
                //end
            }
            catch (System.Exception) { }

            try
            {
                XmlElement formulasNode = (XmlElement)cateNode.SelectSingleNode("Formulas");
                XmlNodeList formulaNodeList = formulasNode.ChildNodes;
                string srcUnit;
                string trgUnit;
                foreach (XmlElement one in formulaNodeList)
                {
                    srcUnit = one.GetAttribute("src");
                    trgUnit = one.GetAttribute("trg");
                    parser.Formula = one.GetAttribute("convertion");
                    parser.Variant = srcUnit;
                    SetFormula(srcUnit, trgUnit, new ParsedFormula(parser.Parse()));
                }
            }
            catch (System.Exception) { }
        }

        public void SetFormula(int srcIndex, int trgIndex, IBxFormula fml)
        {
            if (srcIndex == trgIndex) return;
            _SrcUnitFormulas formulas = m_formulas[srcIndex];
            formulas.SetFormula(trgIndex, fml);
        }
        public void SetFormula(string srcUnit, string trgUnit, IBxFormula fml)
        {
            if (srcUnit == trgUnit) return;
            IBxUnit src = Parse(srcUnit);
            IBxUnit trg = Parse(trgUnit);
            if ((src == null) || (trg == null)) return;
            _SrcUnitFormulas formulas = m_formulas[src.Index];
            formulas.SetFormula(trg.Index, fml);
        }

        #region IBxUnitCategory 成员
        public string ID { get { return m_id; } }
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                    return ID;
                return _name;
            }
        }
        public string Code { get { return _code; } }
        public IEnumerable<IBxUnit> Units { get { return m_units; } }
        public IBxFormula GetFormula(IBxUnit src, IBxUnit trg)
        {
            return GetFormula(src.Index, trg.Index);
        }
        public IBxUnit Parse(string sUnit)
        {
            foreach (IBxUnit one in m_units)
            {
                if (one.ID == sUnit)
                    return one;
            }
            return null;
        }
        public IBxUnit Find(string code)
        {
            foreach (IBxUnit one in m_units)
            {
                if (one.Code == code)
                    return one;
            }
            return null;
        }
        public IBxUnit this[int nIndex] { get { return m_units[nIndex]; } }
        #endregion

        public IBxFormula GetFormula(int srcIndex, int trgIndex)
        {
            if (srcIndex == trgIndex)
                return ParsedFormula.s_sameUnitFormula;

            _SrcUnitFormulas formulas = m_formulas[srcIndex];
            IBxFormula fml = formulas.GetFormula(trgIndex);
            if (fml == null)
            {
                if (trgIndex == m_nDefaultUnitIndex)
                    throw new Exception(string.Format("not find the convertion between {0} to default unit {1} \n", this[srcIndex].ID, this[m_nDefaultUnitIndex].ID));

                if (srcIndex == m_nDefaultUnitIndex)
                {
                    IBxFormula temp = GetFormula(trgIndex, srcIndex);
                    if (!(temp is ParsedFormula))
                        throw new Exception(string.Format("It must be a ParsedFormula between {0] to defaultUnit {1}\n", this[trgIndex].ID, this[srcIndex].ID));
                    fml = ParsedFormula.Inverse((ParsedFormula)temp);
                    formulas.SetFormula(trgIndex, fml);
                }
                else
                {
                    IBxFormula fml1 = GetFormula(srcIndex, m_nDefaultUnitIndex);
                    IBxFormula fml2 = GetFormula(m_nDefaultUnitIndex, trgIndex);
                    fml = new CombinedFormula(fml1, fml2);
                    formulas.SetFormula(trgIndex, fml);
                }
            }
            return fml;
        }
        public IBxUnit this[string unit]
        {
            get
            {
                foreach (IBxUnit one in m_units)
                {
                    if (one.ID == unit)
                        return one;
                }
                return null;
            }
        }

        //#region IBCPersistXml 成员
        //public void Save(XmlElement node, BCPersistHelp help)
        //{
        //    help.SaveString(node, "id", m_id);
        //    help.SaveString(node, "name", _name);
        //    XmlElement unitsNode = node.EMAddChild("Units");
        //    help.SaveArray(unitsNode, m_units);
        //    XmlElement fmlsNode = node.EMAddChild("Formulars");
        //    help.SaveArray(fmlsNode, m_formulas);
        //}
        //public void Load(XmlElement node, BCPersistHelp help)
        //{
        //    m_id = help.ReadString(node, "id");
        //    _name = help.ReadString(node, "name");
        //    XmlElement unitsNode = (XmlElement)node.SelectSingleNode("Units");
        //    XmlElement fmlsNode = (XmlElement)node.SelectSingleNode("Formulars");
        //    help.LoadArray(unitsNode, out m_units);
        //    help.LoadArray(fmlsNode, out m_formulas);
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

    public class UnitNameConvert
    {
        public const char srcSuperScript = '^';
        public static string Convert(string s)
        {
            int pos = s.IndexOf(srcSuperScript);
            if (pos < 0)
                return s;

            string s1 = s.Substring(0, pos);
            char superScript = s[pos + 1];
            if (superScript == '-')
            {
                s1 += string.Format("<sup>-{0}</sup>", s[pos + 2]);
                if ((pos + 3) >= s.Length)
                    return s1;
                return s1 += Convert(s.Substring(pos + 3));
            }
            else
            {
                s1 += string.Format("<sup>{0}</sup>", superScript);
                if ((pos + 2) >= s.Length)
                    return s1;
                return s1 += Convert(s.Substring(pos + 2));
            }

        }
    }
}
