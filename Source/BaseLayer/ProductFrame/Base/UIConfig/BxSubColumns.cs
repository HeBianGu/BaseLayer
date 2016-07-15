using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;


namespace OPT.Product.Base
{
    public class BxSubColumn : IBxSubColumn
    {
        BxSUICSubColum _suicColumn = null;
        BxUIConfigFromSuic _uiConfig = null;

        public BxSubColumn(BxSUICSubColum suicColumn)
        {
            _suicColumn = suicColumn;
        }

        public UInt16 WidthRation
        {
            get { return _suicColumn.WidthRation; }
            //set { _widthRation = value; }
        }
        public string Code
        {
            get { return _suicColumn.Code; }
            //set { _lineIndex = value; }
        }
        public IBxUIConfig UIConfig
        {
            get { return UIConfigEx; }
        }

        public BxUIConfigFromSuic UIConfigEx
        {
            get
            {
                if (_uiConfig == null)
                {
                    _uiConfig = new BxUIConfigFromSuic(_suicColumn.SUICPregnant);
                }
                return _uiConfig;
            }
            //set { _uiConfig = value; }
        }

        public bool Modified
        {
            get
            {
                if (_uiConfig == null)
                    return false;
                return _uiConfig.NeedSave();
            }
        }

    }

    public class BxSubColumns : IBxSubColumns
    {
        List<BxSubColumn> _columns;
        BxSUICSubColums _suicColumns;

        public BxSubColumns()
        {
            _suicColumns = null;
            _columns = null;
        }
        public BxSubColumns(BxSUICSubColums suicColumns)
        {
            _suicColumns = suicColumns;
            _columns = new List<BxSubColumn>(suicColumns.SubColumns.Length);
            Array.ForEach(suicColumns.SubColumns, x => _columns.Add(new BxSubColumn(x)));
        }

        public IEnumerable<IBxSubColumn> Columns
        {
            get { return _columns; }
        }
        public int Count
        {
            get { return _columns.Count; }
        }
        public IBxSubColumn GetAt(int index)
        {
            return _columns[index];
        }
        public IBxSubColumn this[int index] { get { return _columns[index]; } }

        public int IndexOf(IBxSubColumn column)
        {
            return _columns.IndexOf(column as BxSubColumn);
        }
        public int CenterColumn { get { return _suicColumns.CenterCol; } }

        public static BxSubColumns Invalid = new BxSubColumns();

        static protected Int32 S_ParseInt(string val)
        {
            Int32 result;
            if (Int32.TryParse(val, out result))
                return result;
            return -1;
        }
        static protected UInt16 S_ParseUInt16(string val)
        {
            UInt16 result;
            if (UInt16.TryParse(val, out result))
                return result;
            return UInt16.MaxValue;
        }
    }

}