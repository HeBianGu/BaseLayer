using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxUnitE : BxElementT<IBxUnit>
    {
        public BxUnitE() : base() { }
        public BxUnitE(IBxUnit val) : base(val) { }
        public BxUnitE(string cate, string unit)
        {
            IBxUnitCategory cate1 = BxSystemInfo.Instance.UnitsCenter.Parse(cate);
            if (cate1 != null)
            {
                IBxUnit unit1 = cate1.Parse(unit);
                if (unit1 != null)
                    InitValue(unit1);
            }
        }

        public string UnitCateName { get { return Value.Category.Name; } }
        public string UnitName { get { return Value.Name; } }

        public void InitValue(string cate, string unit)
        {
            IBxUnitCategory cate1 = BxSystemInfo.Instance.UnitsCenter.Parse(cate);
            if (cate1 != null)
            {
                IBxUnit unit1 = cate1.Parse(unit);
                if (unit1 != null)
                {
                    InitValue(unit1);
                    return;
                }
            }

            Valid = false;
        }
        public void SetUnit(string cate, string unit)
        {
            IBxUnitCategory cate1 = BxSystemInfo.Instance.UnitsCenter.Parse(cate);
            if (cate1 != null)
            {
                IBxUnit unit1 = cate1.Parse(unit);
                if (unit1 != null)
                {
                    InitValue(unit1);
                    return;
                }
            }

            Valid = false;
        }

        public override string SaveToString()
        {
            if (!Valid)
                return null;

            return Value.Category.ID + "," + Value.ID;
        }
        public override bool LoadFromString(string s)
        {
            string[] parts = s.Split(new char[] { ',' });
            if (parts.Length != 2)
            {
                Valid = false;
                return false;
            }
            IBxUnitCategory cate = BxSystemInfo.Instance.UnitsCenter.Parse(parts[1]);
            IBxUnit unit = null;
            if (cate != null)
                unit = cate.Parse(parts[2]);

            if (unit == null)
            {
                Valid = false;
                return false;
            }

            Value = unit;
            Valid = true;
            return true;
        }

        #region IBxUIValue 成员
        public override string GetUIValue()
        {
            if (!Valid)
                return null;
            return Value.Name;
        }
        public override bool SetUIValue(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                Valid = false;
                return true;
            }
            IBxUnit unit = Value.Category.ParseByName(s);
            Value = unit;
            return true;
        }
        #endregion
    }
    public class BxUnitS : BxElementSiteT<BxUnitE>
    {
        public BxUnitS() : base() { }
        public BxUnitS(IBxUnit val) : base() { Value.InitValue(val); }
        public BxUnitS(string cate, string unit)
        {
            Value.InitValue(cate, unit);
        }

        public IBxUnit ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }

    public class BxUnitSArray : BxArray<BxUnitS>
    {
    }
    public class BxUnitSArrayS : BxElementSiteT<BxUnitSArray>
    {
    }



}
