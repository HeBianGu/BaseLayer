using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace OPT.Product.Base
{
    public class BxDouble : BxSimpleElementT<double>
    {
        public BxDouble() : base() { }
        public BxDouble(double val) : base() { Value = val; }

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
    public class BxDoubleArray : BxArrayValue<BxDouble>
    {
        public BxDoubleArray() { }
    }
    public class BxDoubleArraySite : BxArraySite<BxDoubleArray>
    {
        public BxDoubleArraySite() { }
    }


    public class BxDoubleV : BxSingleElementT<double>
    {
        public BxDoubleV() : base() { }
        public BxDoubleV(double val) : base() { Value = val; }
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
    public class BxDoubleVS : BxGenerousSiteT<BxDoubleV>
    {
    }

    public class BxDoubleVSArray : BxArrayValue<BxDoubleVS>
    {
    }
    public class BxDoubleVSArraySite : BxArraySite<BxDoubleVSArray>
    {
    }

    public class BxDoubleVR : BxReferSiteT<BxDoubleV>
    {
    }

    public class BxDoubleVRArray : BxArrayValue<BxDoubleVR>
    {
    }
    public class BxDoubleVRArraySite : BxArraySite<BxDoubleVRArray>
    {
    }

}
