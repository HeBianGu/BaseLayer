using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;


namespace OPT.Product.Base
{
    public class BxMutiColumn : IBxSubColumn, IBxPersistStorageNode
    {
        #region members
        BxSUICSubColum _suicColumn = null;
        BxUIConfigFromSuic _uiConfig = null;
        //UInt16? _widthRation = null;
        #endregion

        #region functions
        public BxMutiColumn(BxSUICSubColum suicColumn)
        {
            _suicColumn = suicColumn;
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
        public bool NeedSave
        {
            get
            {
                if (_uiConfig == null)
                    return false;
                return _uiConfig.NeedSave();
            }
        }
        #endregion

        #region IBxSubColumn
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
        #endregion

        #region IBxPersistStorageNode 成员
        public void SaveStorageNode(IBxStorageNode node)
        {
            if (_uiConfig != null)
                _uiConfig.SaveStorageNode(node);
        }
        public void LoadStorageNode(IBxStorageNode node)
        {
            //_uiConfig = new BxUIConfigFromSuic();
            UIConfigEx.LoadStorageNode(node);
        }
        #endregion
    }

    public class BxMutiColumns : IBxSubColumns, IBxPersistStorageNode
    {
        #region members
        List<BxMutiColumn> _columns;
        BxSUICSubColums _suicColumns;
        #endregion

        #region functions
        protected BxMutiColumns()
        {
            _suicColumns = null;
            _columns = null;
        }
        public BxMutiColumns(BxSUICSubColums suicColumns)
        {
            _suicColumns = suicColumns;
            _columns = new List<BxMutiColumn>(suicColumns.SubColumns.Length);
            Array.ForEach(suicColumns.SubColumns, x => _columns.Add(new BxMutiColumn(x)));
        }
        public bool NeedSave
        {
            get
            {
                if (_columns == null)
                    return false;
                foreach (BxMutiColumn one in _columns)
                {
                    if (one.NeedSave)
                        return true;
                }
                return false;
            }
        }
        #endregion

        #region IBxSubColumn
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
        public int IndexOf(IBxSubColumn column)
        {
            return _columns.IndexOf(column as BxMutiColumn);
        }
        public int CenterColumn { get { return _suicColumns.CenterCol; } }
        public IBxSubColumn this[int index] { get { return _columns[index]; } }
        #endregion

        #region IBxPersistStorageNode 成员
        public void SaveStorageNode(IBxStorageNode node)
        {
            if (_columns != null)
            {
                IBxStorageNode temp;
                int index = 0;
                foreach (BxMutiColumn one in _columns)
                {
                    if (one.NeedSave)
                    {
                        temp = node.CreateChildNode("Item" + index.ToString());
                        one.SaveStorageNode(temp);
                    }
                    index++;
                }
            }
        }
        public void LoadStorageNode(IBxStorageNode node)
        {
            int index;
            foreach (IBxStorageNode one in node.ChildNodes)
            {
                index = int.Parse(one.Name.Substring(4));
                if ((0 <= index) && (index < _columns.Count))
                {
                    _columns[index].LoadStorageNode(one);
                }
            }
        }
        #endregion

        #region static
        public static BxMutiColumns Invalid = new BxMutiColumns();
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
        #endregion
    }

}