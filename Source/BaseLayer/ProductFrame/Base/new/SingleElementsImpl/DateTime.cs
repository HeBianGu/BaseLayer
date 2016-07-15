using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace OPT.Product.Base
{
    public class BxDateTimeE : BxElementT<DateTime>
    {
        public BxDateTimeE() : base() { }
        public BxDateTimeE(DateTime val) : base() { Value = val; }

        #region IBxPersistString 成员
        public override string SaveToString()
        {
            return string.Format("{0:X}", _value.ToBinary());
        }
        public override bool LoadFromString(string s)
        {
            long temp;
            if (long.TryParse(s, out temp))
            {
                Value = DateTime.FromBinary(temp);
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }
        #endregion

        #region IBxUIValue 成员
        public override string GetUIValue()
        {
            return _value.ToString();
        }
        public override bool SetUIValue(string s)
        {
            DateTime temp;
            if (DateTime.TryParse(s, out temp))
            {
                Value = temp;
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }
        #endregion

        static public implicit operator DateTime(BxDateTimeE val)
        {
            return val.Value;
        }
    }
    public class BxDateTimeS : BxElementSiteT<BxDateTimeE>
    {
        public BxDateTimeS() : base() { }
        public BxDateTimeS(DateTime val) : base() { Value.Value = val; }

        public DateTime ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }

    public class BxDateTimeSArray : BxArray<BxDateTimeS>
    {
    }
    public class BxDateTimeSArrayS : BxElementSiteT<BxDateTimeSArray>
    {
    }


    public class BxDateTimeRE : BxSingleReferableElementT<DateTime>
    {
        public BxDateTimeRE() : base() { }
        public BxDateTimeRE(DateTime val) : base(val) { }

        #region IBxPersistString 成员
        public override string SaveToString()
        {
            return string.Format("{0:X}", Value.ToBinary());
        }
        public override bool LoadFromString(string s)
        {
            long temp;
            if (long.TryParse(s, out temp))
            {
                Value = DateTime.FromBinary(temp);
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }
        #endregion

        #region IBxUIValue 成员
        public override string GetUIValue()
        {
            return Value.ToString();
        }
        public override bool SetUIValue(string s)
        {
            DateTime temp;
            if (DateTime.TryParse(s, out temp))
            {
                Value = temp;
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }
        #endregion
    }
    public class BxDateTimeRS : BxElementSiteT<BxDateTimeRE>
    {
        public BxDateTimeRS() : base() { }
        public BxDateTimeRS(DateTime val) : base() { Value.Value = val; }

        public DateTime ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }


    public class BxDateTimeRSArray : BxArray<BxDateTimeRS>
    {
    }
    public class BxDateTimeRSArrayS : BxElementSiteT<BxDateTimeRSArray>
    {
    }

}
