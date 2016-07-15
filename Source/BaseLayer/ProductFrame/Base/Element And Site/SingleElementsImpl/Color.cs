using System;
using System.Collections.Generic;
using System.Drawing;

namespace OPT.Product.Base
{
    public class BxColorE : BxElementT<Color>
    {
        public BxColorE() : base() { }
        public BxColorE(Color val) : base(val) {}

        public override string SaveToString()
        {
            string strA = Value.A.ToString();
            string strR = Value.R.ToString();
            string strG = Value.G.ToString();
            string strB = Value.B.ToString();
            string strARGB = strA + "," + strR + "," + strG + "," + strB;
            return strARGB;
        }
        public override bool LoadFromString(string s)
        {
            string[] sub = s.Split(',');
            if (sub.Length != 4)
                return false;
            Byte[] val = new Byte[4];
            for (int i = 0; i < 4; i++)
            {
                if (!Byte.TryParse(sub[i], out val[i]))
                    return false;
            }

            Value = Color.FromArgb(val[0], val[1], val[2], val[3]);
            Valid = true;
            return true;
        }
        //static public implicit operator bool(BxColorE val)
        //{
        //    return val.Value;
        //}
    }

    public class BxColorS : BxElementSiteT<BxColorE>
    {
        public BxColorS() : base() { }
        public BxColorS(Color val) : base() { Value.Value = val; }

        public Color ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }

    public class BxColorSArray : BxArray<BxColorS>
    {
    }
    public class BxColorSArrayS : BxElementSiteT<BxColorSArray>
    {
    }


    public class BxColorRE : BxSingleReferableElementT<Color>
    {
        public BxColorRE() : base() { }
        public BxColorRE(Color val) : base(val) { }
        public override string SaveToString()
        {
            string strA = Value.A.ToString();
            string strR = Value.R.ToString();
            string strG = Value.G.ToString();
            string strB = Value.B.ToString();
            string strARGB = strA + "," + strR + "," + strG + "," + strB;
            return strARGB;
        }
        public override bool LoadFromString(string s)
        {
            string[] sub = s.Split(',');
            if (sub.Length != 4)
                return false;
            Byte[] val = new Byte[4];
            for (int i = 0; i < 4; i++)
            {
                if (!Byte.TryParse(sub[i], out val[i]))
                    return false;
            }

            Value = Color.FromArgb(val[0], val[1], val[2], val[3]);
            Valid = true;
            return true;
        }
    }
    public class BxColorRS : BxElementSiteT<BxColorRE>
    {
        public BxColorRS() : base() { }
        public BxColorRS(Color val) : base() { Value.Value = val; }

        public Color ValueEx
        {
            get { return Value.Value; }
            set { Value.Value = value; }
        }
    }


    public class BxColorRSArray : BxArray<BxColorRS>
    {
    }
    public class BxColorRSArrayS : BxElementSiteT<BxColorRSArray>
    {
    }
}
