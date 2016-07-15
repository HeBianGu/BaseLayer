using System;
using System.Collections.Generic;
using System.Xml;
using System.Globalization;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    class BxSubSetUnit : IBxUnit
    {
        protected string _name;
        protected IBxUnit _baseUnit;
        protected IBxUnitCategory _cate;
        protected int _nIndex;


        public BxSubSetUnit(IBxUnit baseUnit, IBxUnitCategory cate, string name, int nIndex)
        {
            _baseUnit = baseUnit;
            _cate = cate;
            _name = name;
            _nIndex = nIndex;
        }

        #region IBxUnit 成员
        public string ID { get { return _baseUnit.ID; } }
        public string Code { get { return _baseUnit.Code; } }
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                    return ID;
                return _name;
            }
            set { _name = value; }
        }
        public Int32 DecimalDigits { get { return _baseUnit.DecimalDigits; } }
        public IBxUnitCategory Category { get { return _cate; } }
        public int Index { get { return _nIndex; } }
        #endregion

        #region IBxUnit 成员
        public IBxUnit BaseUnit { get { return _baseUnit; } }
        public IBxUnitCategory BaseUnitCate { get { return _baseUnit.Category; } }
        #endregion
    }
}
