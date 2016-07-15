using System;
using System.Collections.Generic;

namespace OPT.Product.Base
{
    public class BxInt32E : BxElementT<Int32>
    {
        public BxInt32E() : base() { }
        public BxInt32E(int val) : base() { _value = val; }

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
        static public implicit operator Int32(BxInt32E val)
        {
            return val.Value;
        }
    }

    public class BxInt32S : BxElementSiteT<BxInt32E>
    {
        public BxInt32S() : base() { }
        public BxInt32S(int val) : base() { Value.Value = val; }

        public Int32 ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }

    public class BxInt32SArray : BxArray<BxInt32S>
    {
    }
    public class BxInt32SArrayS : BxElementSiteT<BxInt32SArray>
    {
    }


    public class BxInt32RE : BxSingleReferableElementT<Int32>
    {
        public BxInt32RE() : base() { }
        public BxInt32RE(int val) : base(val) { }
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
    public class BxInt32RS : BxElementSiteT<BxInt32RE>
    {
        public BxInt32RS() : base() { }
        public BxInt32RS(int val) : base() { Value.Value = val; }

        public Int32 ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }


    public class BxInt32RSArray : BxArray<BxInt32RS>
    {
    }
    public class BxInt32RSArrayS : BxElementSiteT<BxInt32RSArray>
    {
    }
}
