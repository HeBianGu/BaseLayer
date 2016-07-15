using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public static class BxUnitsManager 
    {
        static public string Conver(double srcValue, int srcDecimalDigits, IBxUnit srcUnit, IBxUnit trgUnit)
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
    }
}
