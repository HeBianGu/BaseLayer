using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.Globalization;

namespace OPT.Product.Base
{
    public class BxDoubleE : BxElementT<double>
    {
        public BxDoubleE() : base() { }
        public BxDoubleE(double val) : base(val) { }

        public override bool LoadFromString(string s)
        {
            double temp;
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
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
            double temp;
            return double.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out temp);
        }

        static public implicit operator double(BxDoubleE val)
        {
            return val.Value;
        }
    }
    public class BxDoubleS : BxElementSiteT<BxDoubleE>
    {
        public BxDoubleS() : base() { }
        public BxDoubleS(double val) : base() { Value.Value = val; }

        public Double ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }

    public class BxDoubleSArray : BxArray<BxDoubleS>
    {
    }
    public class BxDoubleSArrayS : BxElementSiteT<BxDoubleSArray>
    {
    }


    public class BxDoubleRE : BxSingleReferableElementT<double>
    {
        public BxDoubleRE() : base() { }
        public BxDoubleRE(double val) : base(val) { }
        public override bool LoadFromString(string s)
        {
            double temp;
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
            {
                Value = temp;
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }
    }
    public class BxDoubleRS : BxElementSiteT<BxDoubleRE>
    {
        public BxDoubleRS() : base() { }
        public BxDoubleRS(double val) : base() { Value.Value = val; }

        public Double ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }
    public class BxDoubleR : BxElementReferT<BxDoubleRE>
    {
        public BxDoubleR() : base() { }
        public BxDoubleR(double val) : base() { Value.Value = val; }

        public Double ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }

    public class BxDoubleRSArray : BxArray<BxDoubleRS>
    {
    }
    public class BxDoubleRSArrayS : BxElementSiteT<BxDoubleRSArray>
    {
    }
}
