using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.PEOffice6.BaseLayer.Base
{
    public interface IBUUnits
    {
        IEnumerable<IBUUnitCategory> Categories { get; }
        IBUUnitCategory Parse(string sCategoryID);
        IBUUnitCategory this[int nIndex] { get; }
    }

    public interface IBUUnitCategory
    {
        string ID { get; }
        string Name { get; }
        IEnumerable<IBUUnit> Units { get; }
        IBUParsedFormula GetFormula(IBUUnit src, IBUUnit trg);
        //IBUParsedFormula GetFormula(int srcIndex, int trgIndex);
        IBUUnit Parse(string sUnit);
        IBUUnit this[int nIndex] { get; }
    }

    public interface IBUUnit
    {
        string ID { get; }
        string Name { get; }
        IBUUnitCategory Category { get; }
        int Index { get; }
    }

    public interface IBUParsedItem
    {
        double Calc(double var);
    }
    public interface IBUParsedFormula : IBUParsedItem
    {
    }


}
