using System;
using System.Collections.Generic;
using System.Drawing;

namespace OPT.Product.Base
{
    public class BxColorE : BxElementT<Color>
    {
        public BxColorE() : base() { }
        public BxColorE(Color val) : base() { _value = val; }

        public override string SaveToString()
        {
            string strA = _value.A.ToString();
            string strR = _value.R.ToString();
            string strG = _value.G.ToString();
            string strB = _value.B.ToString();
            string strARGB = strA + "," + strR + "," + strG + "," + strB;
            return strARGB;
        }
        public override bool LoadFromString(string s)
        {
            int index = s.IndexOf("A=");
            int index2 = s.IndexOf(",", index);
            string v = s.Substring(index + 2, index2 - index - 2);
            Byte a;
            if (Byte.TryParse(v, out a))
            {
            }

            index = s.IndexOf("R=");
            index2 = s.IndexOf(",", index);
            v = s.Substring(index + 2, index2 - index - 2);
            Byte r;
            if (Byte.TryParse(v, out r))
            {
            }

            index = s.IndexOf("G=");
            index2 = s.IndexOf(",", index);
            v = s.Substring(index + 2, index2 - index - 2);
            Byte g;
            if (Byte.TryParse(v, out g))
            {
            }

            index = s.IndexOf("B=");
            index2 = s.IndexOf(",", index);
            if (index2 == -1)
            {
                index2 = s.Length-1;
            }
            v = s.Substring(index + 2, index2 - index - 2);
            Byte b;
            if (Byte.TryParse(v, out b))
            {
            }

            Value = Color.FromArgb(a, r, g, b);

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
        public override bool LoadFromString(string s)
        {
            int index = s.IndexOf("A=");
            int index2 = s.IndexOf(",", index);
            string v = s.Substring(index + 2, index2 - index - 2);
            Byte a;
            if (Byte.TryParse(v, out a))
            {
            }

            index = s.IndexOf("R=");
            index2 = s.IndexOf(",", index);
            v = s.Substring(index + 2, index2 - index - 2);
            Byte r;
            if (Byte.TryParse(v, out r))
            {
            }

            index = s.IndexOf("G=");
            index2 = s.IndexOf(",", index);
            v = s.Substring(index + 2, index2 - index - 2);
            Byte g;
            if (Byte.TryParse(v, out g))
            {
            }

            index = s.IndexOf("B=");
            index2 = s.IndexOf(",", index);
            v = s.Substring(index + 2, index2 - index - 2);
            Byte b;
            if (Byte.TryParse(v, out b))
            {
            }

            Value = Color.FromArgb(a, r, g, b);

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
