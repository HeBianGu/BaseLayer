using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using OPT.Product.BaseInterface;
using System.Text;
using System.Globalization;

namespace OPT.Product.Base
{

    public class UnitNameConvert
    {
        public const char srcSuperScript = '^';
        public static string Convert1(string s)
        {
            int pos = s.IndexOf(srcSuperScript);
            if (pos < 0)
                return s;

            string s1 = s.Substring(0, pos);
            char superScript = s[pos + 1];
            if (superScript == '-')
            {
                s1 += string.Format("<sup>-{0}</sup>", s[pos + 2]);
                if ((pos + 3) >= s.Length)
                    return s1;
                return s1 += Convert1(s.Substring(pos + 3));
            }
            else
            {
                s1 += string.Format("<sup>{0}</sup>", superScript);
                if ((pos + 2) >= s.Length)
                    return s1;
                return s1 += Convert1(s.Substring(pos + 2));
            }

        }

        public static string RegularizeUnit(string s)
        {
            int pos = s.IndexOf(srcSuperScript);
            if (pos < 0)
                return s;

            int length = s.EatSimpleDigit(pos + 1);
            string s1 = s.Substring(0, pos);
            string s2 = s.Substring(pos + 1, length);

            string s3 = s1 + ConvertToSuperScript(s2);

            //s1 += string.Format("<sup>{0}</sup>", s2);
            if ((pos + 1 + length) >= s.Length)
                return s3;

            return s3 += RegularizeUnit(s.Substring(pos + 1 + length));

        }

        public static string ConvertToSuperScript(string s)
        {
       
            StringBuilder sb = new StringBuilder(s.Length);
            foreach (char ch in s)
            {
                switch (ch)
                {
                    case '0':
                        sb.Append(Superscript0);
                        break;
                    case '1':
                        sb.Append(Superscript1); break;
                    case '2':
                        sb.Append(Superscript2); break;
                    case '3':
                        sb.Append(Superscript3); break;
                    case '4':
                        sb.Append(Superscript4); break;
                    case '5':
                        sb.Append(Superscript5); break;
                    case '6':
                        sb.Append(Superscript6); break;
                    case '7':
                        sb.Append(Superscript7); break;
                    case '8':
                        sb.Append(Superscript8); break;
                    case '9':
                        sb.Append(Superscript9); break;
                    case '-':
                        sb.Append(SuperscriptMinus); break;
                    case '+':
                        sb.Append(SuperscriptPlus); break;
                    default:
                        break;
                }
            }
            return sb.ToString();

        }

        /// </summary>
        private const string Superscript0 = "\u2070";//上标0
        private const string Superscript1 = "\u00B9";//上标1
        private const string Superscript2 = "\u00B2";//上标2
        private const string Superscript3 = "\u00B3";//上标3
        private const string Superscript4 = "\u2074";//上标4
        private const string Superscript5 = "\u2075";//上标5
        private const string Superscript6 = "\u2076";//上标6
        private const string Superscript7 = "\u2077";//上标7
        private const string Superscript8 = "\u2078";//上标8
        private const string Superscript9 = "\u2079";//上标9
        private const string SuperscriptPlus = "\u207A"; //上标+
        private const string SuperscriptMinus = "\u207B"; //上标-
        private const string SupLeftParentheses = "\u207D"; //上标(
        private const string SupRightParentheses = "\u207E"; //上标）
        private const string SuperscriptLetterN = "\u207F"; //上标n

    }

    public class BCVersion
    {
        protected int[] m_parts = new int[4];
        public int this[int nIndex] { set { m_parts[nIndex] = value; } get { return m_parts[nIndex]; } }

        public BCVersion() { }
        public BCVersion(int part1, int part2 = 0, int part3 = 0, int part4 = 0)
        {
            m_parts[0] = part1; m_parts[1] = part2;
            m_parts[2] = part3; m_parts[3] = part4;
        }

        public override bool Equals(object obj) { return m_parts.Equals(obj); }
        public override int GetHashCode() { return m_parts.GetHashCode(); }

        static public bool operator ==(BCVersion ver1, BCVersion ver2)
        {
            if (object.ReferenceEquals(ver1, ver2))
                return true;

            for (int i = 0; i < 4; i++)
            {
                if (ver1[i] != ver2[i])
                    return false;
            }
            return true;
        }
        static public bool operator !=(BCVersion ver1, BCVersion ver2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (ver1[i] == ver2[i])
                    return false;
            }
            return true;
        }
        static public bool operator >(BCVersion ver1, BCVersion ver2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (ver1[i] < ver2[i])
                    return false;
            }
            return (ver1[3] > ver2[3]);
        }
        static public bool operator <(BCVersion ver1, BCVersion ver2)
        {

            return (ver2 > ver1);
        }
        static public bool operator >=(BCVersion ver1, BCVersion ver2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (ver1[i] < ver2[i])
                    return false;
            }
            return true;
        }
        static public bool operator <=(BCVersion ver1, BCVersion ver2)
        {
            return (ver2 >= ver1);
        }

        public bool FromString(string s)
        {
            int[] temp = new int[4];

            string[] arrParts = s.Split(".".ToArray(), 4);
            int nIndex = 0;
            int var = 0;
            foreach (string one in arrParts)
            {
                temp[nIndex] = Int32.TryParse(one, NumberStyles.Any, CultureInfo.InvariantCulture, out var) ? var : 0;
                nIndex++;
            }
            for (; nIndex < 4; nIndex++)
                temp[nIndex] = 0;
            m_parts = temp;
            return true;
        }
        public override string ToString()
        {
            return m_parts[0].ToString() + "." + m_parts[1].ToString() + "." + m_parts[2].ToString() + "." + m_parts[3].ToString();
        }
        static public BCVersion Parse(string s)
        {
            BCVersion outVar = new BCVersion();
            outVar.FromString(s);
            return outVar.FromString(s) ? outVar : null;
        }
    }

    public class BCLongVersion
    {
        BCVersion[] m_vers = new BCVersion[2];
    }
}
