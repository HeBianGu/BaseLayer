using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace OPT.Product.Base
{
    public class BxString : BxSimpleElementT<string>
    {
        public BxString() : base() { }
        public BxString(string val) : base() { Value = val; }

        public override string SaveToString()
        {
            return _value;
        }
        public override bool LoadFromString(string s)
        {
            Value = s;
            Valid = true;
            return true;
        }
    }
    public class BxStringArray : BxArrayValue<BxString>
    {
        public BxStringArray() { }
    }
    public class BxStringArraySite : BxArraySite<BxStringArray>
    {
        public BxStringArraySite() { }
    }


    public class BxStringV : BxSingleElementT<string>
    {
        public BxStringV() : base() { }
        public BxStringV(string val) : base() { Value = val; }
        public override bool LoadFromString(string s)
        {
            Value = s;
            Valid = true;
            return true;
        }
    }
    public class BxStringVS : BxGenerousSiteT<BxStringV>
    {
    }

    public class BxStringVSArray : BxArrayValue<BxStringVS>
    {
    }
    public class BxStringVSArraySite : BxArraySite<BxStringVSArray>
    {
    }

    public class BxStringVR : BxReferSiteT<BxStringV>
    {
    }

    public class BxStringVRArray : BxArrayValue<BxStringVR>
    {
    }
    public class BxStringVRArraySite : BxArraySite<BxStringVRArray>
    {
    }
}
