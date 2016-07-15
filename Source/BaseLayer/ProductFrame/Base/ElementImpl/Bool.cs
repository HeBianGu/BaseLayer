using System;
using System.Collections.Generic;

namespace OPT.Product.Base
{

    public class BxBool : BxSimpleElementT<bool>
    {
        public BxBool() : base() { }
        public BxBool(bool val) : base() { Value = val; }
        public override bool LoadFromString(string s)
        {
            bool temp;
            if (bool.TryParse(s, out temp))
            {
                Value = temp;
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }
    }
    public class BxBoolArray : BxArrayValue<BxBool>
    {
    }
    public class BxBoolArraySite : BxArraySite<BxBoolArray>
    {
    }


    public class BxBoolV : BxSingleElementT<bool>
    {
        public BxBoolV() : base() { }
        public BxBoolV(bool val) : base() { Value = val; }
        public override bool LoadFromString(string s)
        {
            bool temp;
            if (bool.TryParse(s, out temp))
            {
                Value = temp;
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }
    }
    public class BxBoolVS : BxGenerousSiteT<BxBoolV>
    {
    }

    public class BxBoolVSArray : BxArrayValue<BxBoolVS>
    {
    }
    public class BxBoolVSArraySite : BxArraySite<BxBoolVSArray>
    {
    }

    public class BxBoolVR : BxReferSiteT<BxBoolV>
    {
    }

    public class BxBoolVRArray : BxArrayValue<BxBoolVR>
    {
    }
    public class BxBoolVRArraySite : BxArraySite<BxBoolVRArray>
    {
    }



}
