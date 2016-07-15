using System;
using System.Collections;
using System.Collections.Generic;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BLUIConfigColumn : IBLUIConfigColumn
    {
        #region IBLUIConfigColumn 成员
        public Int32 ID { get; set; }
        public string Name { get; set; }
        #endregion
    }

    public class BLUIConfigMultiColumn : IBLUIConfigMultiColumn
    {
        IBLUIConfig[] _columns;
        UInt16[] _ratios;
        Int16 _iddleColumn = -1;

        public BLUIConfigMultiColumn(IBLUIConfig[] columns, UInt16[] ratios)
        {
            _columns = columns;
            _ratios = ratios;
            if (columns.Length != ratios.Length)
                throw new Exception();
        }

        #region IBLUIConfigMultiColumn 成员
        public Int32 Count { get { return _columns.Length; } }
        public IEnumerable<IBLUIConfig> Columns { get { return _columns; } }
        public IEnumerable<ushort> ColumnRatios { get { return _ratios; } }
        public short MiddleColumn { get { return _iddleColumn; } }
        #endregion
    }
}
