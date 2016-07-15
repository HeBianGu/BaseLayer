using System;
using System.Collections.Generic;
using System.Xml;
using System.Globalization;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    class BxUnit : IBxIndexedUnit
    {
        protected string _id;
        protected string _code;
        protected string _name;
        protected int _dd = 0; //相对小数位
        protected IBxUnitCategory _cate;
        protected int _nIndex = -1;

        public BxUnit()
        {
            _cate = null;
            _code = null;
        }
        public BxUnit(string id, string code, int dd, IBxUnitCategory cate, int nIndex)
        {
            _id = id;
            _code = code;
            _cate = cate;
            _dd = dd;
            _nIndex = nIndex;
        }
        #region IBxUnit 成员
        public string ID { get { return _id; } }
        public string Code { get { return _code; } }
        public string Name { get { return _name; } set { _name = value; } }
        public Int32 DecimalDigits { get { return _dd; } }
        public IBxUnitCategory Category { get { return _cate; } }
        public int Index { get { return _nIndex; } }
        #endregion


        #region IBxUnit 成员
        public IBxUnit BaseUnit
        {
            get { return this; }
        }
        public IBxUnitCategory BaseUnitCate
        {
            get { return _cate; }
        }
        #endregion
    }
}
