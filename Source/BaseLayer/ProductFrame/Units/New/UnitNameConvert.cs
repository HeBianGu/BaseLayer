using System;
using System.Collections.Generic;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxUnitNameConvert1
    {
        public const char srcSuperScript = '^';
        public static string Convert(string s)
        {
            int pos = s.IndexOf(srcSuperScript);
            if (pos < 0)
                return s;

            int length = s.EatSimpleDigit(pos + 1);
            string s1 = s.Substring(0, pos);
            string s2 = s.Substring(pos + 1, length);
            s1 += string.Format("<sup>{0}</sup>", s2);
            if ((pos + 1 + length) >= s.Length)
                return s1;
            return s1 += Convert(s.Substring(pos + 2));


            //string s1 = s.Substring(0, pos);
            //char superScript = s[pos + 1];
            //s1 += string.Format("<sup>{0}</sup>", superScript);
            //if ((pos + 2) >= s.Length)
            //    return s1;
            //return s1 += Convert(s.Substring(pos + 2));
        }
    }
}
