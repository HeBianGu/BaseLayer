using System;
using System.Collections.Generic;

namespace OPT.Product.Base
{
    public class BxChar : BxSimpleElementT<char>
    {
        public BxChar() : base() { }
        public BxChar(char val) : base() { Value = val; }

        public override bool LoadFromString(string s)
        {
            char temp;
            if (char.TryParse(s, out temp))
            {
                Value = temp;
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }
    }
    public class BxCharArray : BxArrayValue<BxChar>
    {
        public BxCharArray(){}
    }
    public class BxCharArraySite : BxArraySite<BxCharArray>
    {
        public BxCharArraySite(){}
    }


    public class BxCharV : BxSingleElementT<char>
    {
        public BxCharV() : base() { }
        public BxCharV(char val) : base() { Value = val; }
        public override bool LoadFromString(string s)
        {
            char temp;
            if (char.TryParse(s, out temp))
            {
                Value = temp;
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }
    }
    public class BxCharVS : BxGenerousSiteT<BxCharV>
    {
    }

    public class BxCharVSArray : BxArrayValue<BxCharVS>
    {
    }
    public class BxCharVSArraySite : BxArraySite<BxCharVSArray>
    {
    }

    public class BxCharVR : BxReferSiteT<BxCharV>
    {
    }

    public class BxCharVRArray : BxArrayValue<BxCharVR>
    {
    }
    public class BxCharVRArraySite : BxArraySite<BxCharVRArray>
    {
    }
}
