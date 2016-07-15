using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;


namespace OPT.Product.Base
{
    public class BxUnitList
    {
        int _index = 0;
        string[] _unitCateString;
        string[] _unitString;
        IBxUnit[] _units;

        public string CurUnitCate { get { return _unitCateString[_index]; } }
        public string CurUnit { get { return _unitString[_index]; } }
        public Int32 CurUnitIndex
        {
            get { return _index; }
            set { _index = value; }
        }

        public IBxUnit GetUnit(Int32 index)
        {
            if (index < 0)
                return null;
            if (_units == null)
                _units = new IBxUnit[_unitCateString.Length];
            if (object.ReferenceEquals(_units[index], null))
            {
                IBxUnitCategory cate = BxSystemInfo.Instance.UnitsCenter.Parse(_unitCateString[index]);
                _units[index] = cate.Parse(_unitString[index]);
            }
            return _units[index];
        }
        public string GetUnitCate(Int32 index) { return _unitCateString[index]; }
        public string GetUnitString(Int32 index) { return _unitString[index]; }
        public IBxUnit GetCurUnit() { return GetUnit(_index); }

        public BxUnitList(string unitCate, string unit)
        {
            _unitCateString = new string[] { unitCate };
            _unitString = new string[] { unit };
            _units = null;
        }
        public BxUnitList(string[] unitCate, string[] unit)
        {
            _unitCateString = unitCate;
            _unitString = unit;
            _units = null;
        }
    }

}