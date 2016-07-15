using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.Globalization;

namespace OPT.Product.Base
{
    public class BxFloatE : BxElementT<float>
    {
        public BxFloatE() : base() { }
        public BxFloatE(float val) : base(val) { }

        public override bool LoadFromString(string s)
        {
            float temp;
            if (float.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
            {
                Value = temp;
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }

        public override string GetUIValue(int? decimalDigits)
        {
            if ((decimalDigits == null) || ((decimalDigits.Value < 0)))
                return GetUIValue();

            if (!Valid)
                return null;

            string sFromat = "{0:F" + decimalDigits.Value.ToString() + "}";
            string s = string.Format(sFromat, Value);
            return s;
        }
        public override bool IsLegalValue(string val)
        {
            float temp;
            return float.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out temp);
        }

        static public implicit operator float(BxFloatE val)
        {
            return val.Value;
        }
    }
    public class BxFloatS : BxElementSiteT<BxFloatE>
    {
        public BxFloatS() : base() { }
        public BxFloatS(float val) : base() { Value.Value = val; }

        public float ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }

    public class BxFloatSArray : BxArray<BxFloatS>
    {
    }
    public class BxFloatSArrayS : BxElementSiteT<BxFloatSArray>
    {
    }


    public class BxFloatRE : BxSingleReferableElementT<float>
    {
        public BxFloatRE() : base() { }
        public BxFloatRE(float val) : base(val) { }
        public override bool LoadFromString(string s)
        {
            float temp;
            if (float.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
            {
                Value = temp;
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }
    }
    public class BxFloatRS : BxElementSiteT<BxFloatRE>
    {
        public BxFloatRS() : base() { }
        public BxFloatRS(float val) : base() { Value.Value = val; }

        public float ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }
    public class BxFloatR : BxElementReferT<BxFloatRE>
    {
        public BxFloatR() : base() { }
        public BxFloatR(float val) : base() { Value.Value = val; }

        public float ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }

    public class BxFloatRSArray : BxArray<BxFloatRS>
    {
    }
    public class BxFloatRSArrayS : BxElementSiteT<BxFloatRSArray>
    {
    }
}
