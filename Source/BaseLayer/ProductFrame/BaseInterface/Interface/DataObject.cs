using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace OPT.Product.BaseInterface
{
    public enum EBxDOType
    {
        Well,
        MultiWell,
        Unit,
        Layer
    }

    //IV: Independent variables
    public enum EBxDOVType
    {
        Time,
        Day,
        Month,
        Year
    }

    //
    public interface IBxPEObjectField
    {
        string ID { get; }
        string Name(CultureInfo ci);
        Type DataType { get; }
    }

    public static class WellFieldID
    {
        public const string wellID = "wellID";
        public const string wellName = "wellName";
        public const string wellAPI = "wellAPI"; 
    }
     

    public static class WellFieldProperty
    {
        public static IBxPEObjectField wellID;
    }




    ////DOP: DataObject Property
    //public interface IBxDODV : IBxDOVariable
    //{
    //    EBxDOType Type { get; }
    //}

    //public interface IBxDataSet
    //{

    //}


    //public class SqlSelectField
    //{
    //    int TableIndex { get; }
    //    int FieldIndex { get; }
    //}

    //public class SqlTable
    //{
    //    string Name { get; }
    //    string Fields { get; }
    //}

    //public enum EBxJoinType
    //{
    //    Inner,
    //    Left,
    //    Right,
    //    Out
    //}
    //public class SQLSelect
    //{
    //    SqlSelectField[] Fields { get; }
    //    SqlTable[] Tables { get; }
    //    EBxJoinType[] JoinTypes { get; }

    //}
}
