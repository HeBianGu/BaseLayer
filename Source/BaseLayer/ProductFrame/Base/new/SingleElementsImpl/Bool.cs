using System;
using System.Collections.Generic;

namespace OPT.Product.Base
{
    public class BxBoolE : BxElementT<bool>
    {
        public BxBoolE() : base() { }
        public BxBoolE(bool val) : base() { _value = val; }

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
        static public implicit operator bool(BxBoolE val)
        {
            return val.Value;
        }
    }

    public class BxBoolS : BxElementSiteT<BxBoolE>
    {
        public BxBoolS() : base() { }
        public BxBoolS(bool val) : base() { Value.Value = val; }

        public bool ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }

    public class BxBoolSArray : BxArray<BxBoolS>
    {
    }
    public class BxBoolSArrayS : BxElementSiteT<BxBoolSArray>
    {
    }


    public class BxBoolRE : BxSingleReferableElementT<bool>
    {
        public BxBoolRE() : base() { }
        public BxBoolRE(bool val) : base(val) { }
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
    public class BxBoolRS : BxElementSiteT<BxBoolRE>
    {
        public BxBoolRS() : base() { }
        public BxBoolRS(bool val) : base() { Value.Value = val; }

        public bool ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }


    public class BxBoolRSArray : BxArray<BxBoolRS>
    {
    }
    public class BxBoolRSArrayS : BxElementSiteT<BxBoolRSArray>
    {
    }
}
