using System;
using System.Collections.Generic;
using System.Xml;
using OPT.Product.BaseInterface;
using System.Globalization;

namespace OPT.Product.Base
{
    class BxSrcUnitFormulas : IBxPersistXmlNode
    {
        int _srcUnitIndex;
        IBxFormula[] _trgUnitFormula;

        public BxSrcUnitFormulas(int srcUnitIndex, int nUnitsCount)
        {
            _srcUnitIndex = srcUnitIndex;
            _trgUnitFormula = new IBxFormula[nUnitsCount];
            for (int i = 0; i < nUnitsCount; i++)
            {
                _trgUnitFormula[i] = null;
            }
        }
        public IBxFormula GetFormula(int nIndex)
        {
            return _trgUnitFormula[nIndex];
        }
        public void SetFormula(int nIndex, IBxFormula formula)
        {
            _trgUnitFormula[nIndex] = formula;
        }

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

    public class CategoryUnits
    {
        protected IBxIndexedUnit[] _units;

        public CategoryUnits(int count)
        {
            _units = new IBxIndexedUnit[count];
        }

        public IEnumerable<IBxIndexedUnit> Units
        {
            get { return _units; }
        }

        public IBxIndexedUnit Parse(string sUnit)
        {
            foreach (IBxIndexedUnit one in _units)
            {
                if (one.ID == sUnit)
                    return one;
            }
            return null;
        }
        public IBxIndexedUnit Find(string code)
        {
            foreach (IBxIndexedUnit one in _units)
            {
                if (one.Code == code)
                    return one;
            }
            return null;
        }
        public IBxIndexedUnit ParseByName(string name)
        {
            foreach (IBxIndexedUnit one in _units)
            {
                if (one.Name == name)
                    return one;
            }
            return null;
        }
        public IBxIndexedUnit this[int nIndex]
        {
            get { return _units[nIndex]; }
            set { _units[nIndex] = value; }
        }

    }

    class BxUnitCategory : IBxUnitCategory, IBxPersistXmlNode
    {
        protected string _id = null;
        protected string _name = null;
        protected string _code = null;

        protected CategoryUnits _units = null;
        protected BxSrcUnitFormulas[] _formulas = null;
        protected int _nDefaultUnitIndex = -1;

        public BxUnitCategory() { }
        public BxUnitCategory(string id, string code)
        {
            _id = id;
            _code = code;
        }

        public void LoadUnitConfigNode(XmlElement cateNode)
        {

            FormulaParser parser = new FormulaParser();
            try
            {
                _id = cateNode.GetAttribute("id");
                _code = cateNode.GetAttribute("code");

                XmlElement unitsNode = (XmlElement)cateNode.SelectSingleNode("Units");
                XmlNodeList unitsNodeList = cateNode.SelectSingleNode("Units").ChildNodes;
                int count = unitsNodeList.Count;
                _units = new CategoryUnits(count);
                _formulas = new BxSrcUnitFormulas[count];
                int nIndex = 0;
                string id = null;
                string code = null;
                int dd = 0;
                foreach (XmlElement one in unitsNodeList)
                {
                    id = one.GetAttribute("id");
                    code = one.GetAttribute("code");
                    if (!Int32.TryParse(one.GetAttribute("decimalDigits"), NumberStyles.Any, CultureInfo.InvariantCulture, out dd))
                    {
                        //throw new Exception("no decimal digis in unit " + id + " !");
                    }

                    _units[nIndex] = new BxUnit(id, code, dd, this, nIndex);
                    _formulas[nIndex] = new BxSrcUnitFormulas(nIndex, count);
                    nIndex++;
                }
            }
            catch (System.Exception) { }

            string srcUnit;
            string trgUnit;
            try
            {
                XmlElement formulasNode = (XmlElement)cateNode.SelectSingleNode("Formulas");
                if (formulasNode == null)
                    return;
                string primaryUnitID = formulasNode.GetAttribute("primaryUnit");
                if (string.IsNullOrEmpty(primaryUnitID))
                    return;
                
                _nDefaultUnitIndex = ParseEx(primaryUnitID).Index;

                XmlNodeList formulaNodeList = formulasNode.ChildNodes;

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
            BxSrcUnitFormulas formulas = _formulas[srcIndex];
            formulas.SetFormula(trgIndex, fml);
        }
        public void SetFormula(string srcUnit, string trgUnit, IBxFormula fml)
        {
            if (srcUnit == trgUnit) return;
            IBxIndexedUnit src = ParseEx(srcUnit);
            IBxIndexedUnit trg = ParseEx(trgUnit);
            if ((src == null) || (trg == null)) return;
            BxSrcUnitFormulas formulas = _formulas[src.Index];
            formulas.SetFormula(trg.Index, fml);
        }

        public IBxIndexedUnit ParseEx(string sUnit)
        {
            return _units.Parse(sUnit);
        }

        public IBxFormula GetFormulaEx(IBxIndexedUnit src, IBxIndexedUnit trg)
        {
            return GetFormula(src.Index, trg.Index);
        }

        #region IBxUnitCategory 成员
        public string ID { get { return _id; } }
        public string Name
        {
            set { _name = value; }
            get
            {
                if (string.IsNullOrEmpty(_name))
                    return ID;
                return _name;
            }
        }
        public string Code { get { return _code; } }
        public IEnumerable<IBxUnit> Units { get { return _units.Units; } }
        public IBxFormula GetFormula(IBxUnit src, IBxUnit trg)
        {
            if ((src.BaseUnitCate != this) || (trg.BaseUnitCate != this))
                throw new Exception();
            Int32 index1 = (src.BaseUnit as IBxIndexedUnit).Index;
            Int32 index2 = (trg.BaseUnit as IBxIndexedUnit).Index;
            return GetFormula(index1, index2);

            //return GetFormula((src as IBxIndexedUnit).Index, (trg as IBxIndexedUnit).Index);
        }
        public IBxUnit Parse(string sUnit)
        {
            return _units.Parse(sUnit);
        }
        public IBxUnit Find(string code)
        {
            return _units.Find(code);
        }
        public IBxUnit ParseByName(string name)
        {
            return _units.ParseByName(name);
        }

        public IBxUnit this[int nIndex] { get { return _units[nIndex]; } }
        #endregion

        #region IBxUnitCategory 成员
        public IBxUnitCategory BaseUnitCate { get { return this; } }
        #endregion

        public IBxFormula GetFormula(int srcIndex, int trgIndex)
        {
            if (srcIndex == trgIndex)
                return ParsedFormula.s_sameUnitFormula;

            BxSrcUnitFormulas formulas = _formulas[srcIndex];
            IBxFormula fml = formulas.GetFormula(trgIndex);
            if (fml == null)
            {
                //取反向的转换关系
                ParsedFormula tempFml = _formulas[trgIndex].GetFormula(srcIndex) as ParsedFormula;
                if (tempFml != null)
                {
                    fml = ParsedFormula.Inverse(tempFml);
                    formulas.SetFormula(trgIndex, fml);
                }
            }
            if (fml == null)
            {
                if ((trgIndex == _nDefaultUnitIndex) || (srcIndex == _nDefaultUnitIndex))
                {
                    throw new Exception(string.Format("缺乏单位{0}和主单位{1}之间的转换关系\n", this[srcIndex].ID, this[trgIndex].ID));
                }
                else
                {
                    IBxFormula fml1 = GetFormula(srcIndex, _nDefaultUnitIndex);
                    IBxFormula fml2 = GetFormula(_nDefaultUnitIndex, trgIndex);
                    fml = new CombinedFormula(fml1, fml2);
                    formulas.SetFormula(trgIndex, fml);
                }
            }
            return fml;
        }
        public IBxUnit this[string unitID]
        {
            get
            {
                foreach (IBxUnit one in Units)
                {
                    if (one.ID == unitID)
                        return one;
                }
                return null;
            }
        }

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

}
