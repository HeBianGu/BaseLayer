using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace OPT.Product.Base
{
    public class BxDoubleE : BxElementT<double>
    {
        public BxDoubleE() : base() { }
        public BxDoubleE(int val) : base() { _value = val; }

        public override bool LoadFromString(string s)
        {
            double temp;
            if (double.TryParse(s, out temp))
            {
                Value = temp;
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }

        static public implicit operator double(BxDoubleE val)
        {
            return val.Value;
        }
    }

    public class BxDoubleS : BxElementSiteT<BxDoubleE>
    {
        public BxDoubleS() : base() { }
        public BxDoubleS(int val) : base() { Value.Value = val; }

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
        public BxDoubleRE(int val) : base(val) { }
        public override bool LoadFromString(string s)
        {
            double temp;
            if (double.TryParse(s, out temp))
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
        public BxDoubleRS(int val) : base() { Value.Value = val; }

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
