using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.Product.BaseInterface
{
    public interface IBxUnitsCenter
    {
        IEnumerable<IBxUnitCategory> Categories { get; }
        IBxUnitCategory Parse(string categoryID);
        IBxUnitCategory Find(string categoryCode);
        //IBxUnitCategory this[int nIndex] { get; }


    }

    public static class BxUnitsCenterExtendMethod
    {
        public static IBxUnit EMParse(this IBxUnitsCenter center, string unitCate, string unit)
        {
            IBxUnitCategory cate = center.Parse(unitCate);
            if (cate == null)
                return null;
            return cate.Parse(unit);
        }

        public static IBxUnit EMFind(this IBxUnitsCenter center, string unitCateCode, string unitCode)
        {
            IBxUnitCategory cate = center.Find(unitCateCode);
            if (cate == null)
                return null;
            return cate.Find(unitCode);
        }
    }

    public interface IBxUnitCategoryBase
    {
        string ID { get; }
        string Name { get; }
        string Code { get; }
        IEnumerable<IBxUnit> Units { get; }
        IBxFormula GetFormula(IBxUnit src, IBxUnit trg);
        //IBxFormula GetFormula(int srcIndex, int trgIndex);
        IBxUnit Parse(string unitID);
        IBxUnit Find(string unitCode);
        IBxUnit ParseByName(string name);

        //IBxUnit this[int nIndex] { get; }
    }

    public interface IBxUnitCategory : IBxUnitCategoryBase
    {
        IBxUnitCategory BaseUnitCate { get; }
    }

    public interface IBxUnitBase
    {
        string ID { get; }
        string Name { get; }
        string Code { get; }
        //小数位数
        Int32 DecimalDigits { get; }
        IBxUnitCategory Category { get; }
    }

    public interface IBxUnit : IBxUnitBase
    {
        IBxUnit BaseUnit { get; }
        IBxUnitCategory BaseUnitCate { get; }
    }

    public interface IBxIndexedUnit : IBxUnit
    {
        int Index { get; }
    }

    //public interface IBxSubsetUnit : IBxUnit
    //{
    //    IBxUnit BaseUnit { get; }
    //}

    public interface IBxUnitConvert
    {
        double Calc(double var);
    }
    public interface IBxFormula : IBxUnitConvert
    {
    }


    public static class BxUnitConvert
    {
        static public string ConverToEx(double srcValue, int srcDecimalDigits, IBxUnit srcUnit, IBxUnit trgUnit)
        {
            IBxUnitCategory cate = srcUnit.Category;
            int ddAdopt = trgUnit.DecimalDigits - srcUnit.DecimalDigits;
            int trgDD = srcDecimalDigits + ddAdopt;
            if (trgDD < 0)
                trgDD = 0;
            double trgValue = cate.GetFormula(srcUnit, trgUnit).Calc(srcValue);
            string sFromat = "{0:F" + trgDD.ToString() + "}";
            string s = string.Format(sFromat, trgValue);
            return s;
        }

        static public string ConverToEx(double srcValue, IBxUnit srcUnit, IBxUnit trgUnit, IBxUnit decimalDigitsUnit, int decimalDigits)
        {
            IBxUnitCategory cate = srcUnit.Category;
            int ddAdopt = trgUnit.DecimalDigits - decimalDigitsUnit.DecimalDigits;
            int trgDD = decimalDigits + ddAdopt;
            if (trgDD < 0)
                trgDD = 0;
            double trgValue = cate.GetFormula(srcUnit, trgUnit).Calc(srcValue);
            string sFromat = "{0:F" + trgDD.ToString() + "}";
            string s = string.Format(sFromat, trgValue);
            return s;
        }

        static public double EMConverTo(this IBxUnit srcUnit, double srcValue, IBxUnit trgUnit)
        {
            IBxUnitCategory cate = srcUnit.Category;
            return cate.GetFormula(srcUnit, trgUnit).Calc(srcValue);
        }

        static public double EMConverTo(this IBxUnit srcUnit, double srcValue, string trgUnit)
        {
            IBxUnitCategory cate = srcUnit.Category;
            return cate.GetFormula(srcUnit, cate.Parse(trgUnit)).Calc(srcValue);
        }

        static public double EMConverFrom(this IBxUnit trgUnit, double srcValue, IBxUnit srcUnit)
        {
            IBxUnitCategory cate = trgUnit.Category;
            return cate.GetFormula(srcUnit, trgUnit).Calc(srcValue);
        }

        static public double EMConverFrom(this IBxUnit trgUnit, double srcValue, string srcUnit)
        {
            IBxUnitCategory cate = trgUnit.Category;
            return cate.GetFormula(cate.Parse(srcUnit), trgUnit).Calc(srcValue);
        }
    }

}
