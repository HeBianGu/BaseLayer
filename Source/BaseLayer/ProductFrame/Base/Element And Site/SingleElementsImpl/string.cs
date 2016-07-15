using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace OPT.Product.Base
{
    public class BxStringE : BxElementT<string>
    {
        public BxStringE() : base() { }
        public BxStringE(string val) : base(val) { }

        public override bool LoadFromString(string s)
        {
            Value = s;
            if (s == null)
                Valid = false;
            else
                Valid = true;
            return true;
        }

        static public implicit operator String(BxStringE val)
        {
            return val.Value;
        }
    }
    public class BxStringS : BxElementSiteT<BxStringE>
    {
        public BxStringS() : base() { }
        public BxStringS(string val) : base() { Value.Value = val; }

        public string ValueEx
        {
            get { return Value.Value; }
            set { Value.SetUIValue(value); }
        }
    }

    public class BxStringSArray : BxArray<BxStringS>
    {
        public BxStringSArray() { }
    }
    public class BxStringSArrayS : BxElementSiteT<BxStringSArray>
    {
        public BxStringSArrayS() { }
    }


    public class BxstringRE : BxSingleReferableElementT<string>
    {
        public BxstringRE() : base() { }
        public BxstringRE(string val) : base(val) { }
        public override string SaveToString()
        {
            return Value;
        }
        public override bool LoadFromString(string s)
        {
            Value = s;
            Valid = true;
            return true;
        }
    }
    public class BxstringRS : BxElementSiteT<BxstringRE>
    {
        public BxstringRS() : base() { }
        public BxstringRS(string val) : base() { Value.Value = val; }

        public string ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }


    public class BxstringRSArray : BxArray<BxstringRS>
    {
    }
    public class BxstringRSArrayS : BxElementSiteT<BxstringRSArray>
    {
    }

}
