using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxSubSetUnitCategory : IBxUnitCategory
    {
        IBxUnitCategory _baseCategory;
        string _name;
        List<IBxUnit> _subsetUnits = null;

        public BxSubSetUnitCategory(IBxUnitCategory baseCategory, string name)
        {
            _baseCategory = baseCategory;
            _name = name;
        }

        public void LoadUnitConfigNode(XmlElement cateNode)
        {
            FormulaParser parser = new FormulaParser();
            try
            {
                XmlNodeList unitNodes = cateNode.ChildNodes;
                _subsetUnits = new List<IBxUnit>(unitNodes.Count);
                string id, name;
                IBxUnit baseUnit;
                int index = 0;
                foreach (XmlElement one in unitNodes)
                {
                    id = one.GetAttribute("id");
                    baseUnit = _baseCategory.Parse(id);

                    name = one.GetAttribute("name");
                    if (string.IsNullOrEmpty(name))
                        name = id;
                    name = UnitNameConvert.RegularizeUnit(name);

                    _subsetUnits.Add(new BxSubSetUnit(baseUnit, this, name, index));
                    index++;
                }
            }
            catch (System.Exception) { }
        }

        #region IBxUnitCategory 成员
        public string ID { get { return _baseCategory.ID; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Code { get { return _baseCategory.Code; } }
        public IEnumerable<IBxUnit> Units { get { return _subsetUnits; } }
        public IBxFormula GetFormula(IBxUnit src, IBxUnit trg)
        {
            if ((src.BaseUnitCate != this.BaseUnitCate) || (trg.BaseUnitCate != this.BaseUnitCate))
                throw new Exception();
            //IBxUnit baseSrcUnit = src.BaseUnit;
           // IBxUnit baseTrgUnit = trg.BaseUnit;
            return _baseCategory.GetFormula(src.BaseUnit, trg.BaseUnit);
        }
        public IBxUnit Parse(string unitID)
        {
            IBxUnit unit = _subsetUnits.Find(x => x.ID == unitID);
            if (unit == null)
                unit = _baseCategory.Parse(unitID);
            return unit;
        }
        public IBxUnit Find(string unitCode)
        {
            IBxUnit unit = _subsetUnits.Find(x => x.Code == unitCode);
            if (unit == null)
                unit = _baseCategory.Find(unitCode);
            return unit;
        }
        public IBxUnit ParseByName(string name)
        {
            IBxUnit unit = _subsetUnits.Find(x => x.Name == name);
            if (unit == null)
                unit = _baseCategory.ParseByName(name);
            return unit;
        }
        #endregion

        #region IBxUnitCategory 成员
        public IBxUnitCategory BaseUnitCate { get { return _baseCategory; } }
        #endregion

    }



}
