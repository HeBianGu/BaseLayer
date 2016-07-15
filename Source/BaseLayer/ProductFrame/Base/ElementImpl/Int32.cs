using System;
using System.Collections.Generic;

namespace OPT.Product.Base
{
    public class BxInt32 : BxSimpleElementT<Int32>
    {
        public BxInt32() : base() { }
        public BxInt32(int val) : base() { Value = val; }

        public override bool LoadFromString(string s)
        {
            Int32 temp;
            if (Int32.TryParse(s, out temp))
            {
                Value = temp;
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }
    }
    public class BxInt32Array : BxArrayValue<BxInt32>
    {
        public BxInt32Array(){}
    }
    public class BxInt32ArraySite : BxArraySite<BxInt32Array>
    {
        public BxInt32ArraySite(){}
    }


    public class BxInt32V : BxSingleElementT<Int32>
    {
        public BxInt32V() : base() { }
        public BxInt32V(int val) : base() { Value = val; }
        public override bool LoadFromString(string s)
        {
            Int32 temp;
            if (Int32.TryParse(s, out temp))
            {
                Value = temp;
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }
    }
    public class BxInt32VS : BxGenerousSiteT<BxInt32V>
    {
        public BxInt32VS() : base() { }
        public BxInt32VS(int val) : base() { _value.Value = val; }
    }

    public class BxInt32VSArray : BxArrayValue<BxInt32VS>
    {
    }
    public class BxInt32VSArraySite : BxArraySite<BxInt32VSArray>
    {
    }

    public class BxInt32VR : BxReferSiteT<BxInt32V>
    {
    }

    public class BxInt32VRArray : BxArrayValue<BxInt32VR>
    {
    }
    public class BxInt32VRArraySite : BxArraySite<BxInt32VRArray>
    {
    }
}
