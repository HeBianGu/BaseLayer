using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPT.Product.BaseInterface;
using System.Globalization;

namespace OPT.Product.Base
{
    static class StringDigitParse
    {
        public static int EatSignum(string s, int posFrom, out bool isMinus)
        {
            int cur = posFrom;
            isMinus = false;
            while (cur < s.Length)
            {
                if (s[cur] == '-')
                    isMinus = !isMinus;
                else if ((s[cur] != '+') && (s[cur] != ' '))
                    break;
                cur++;
            }
            return (cur - posFrom);
        }
        /// <summary>
        /// 返回：吃进去的个数
        /// </summary>
        /// <param name="s"></param>
        /// <param name="posFrom"></param>
        /// <returns></returns>
        public static int EatSimpleDigit(this string s, int posFrom)
        {
            bool bFoundDot = false;
            char temp;
            bool bIsminus;
            int nEatSignum = EatSignum(s, posFrom, out bIsminus);
            int curPos = posFrom + nEatSignum;
            curPos--;
            int nLastPos = s.Length - 1;
            while (curPos < nLastPos)
            {
                curPos++;
                temp = s[curPos];
                if (Char.IsDigit(temp))
                    continue;
                if (temp.Equals('.') && !bFoundDot)
                {
                    bFoundDot = true;
                    continue;
                }
                curPos--;
                break;
            }
            int eat = curPos - posFrom + 1;
            return (eat > nEatSignum) ? eat : 0;
        }
        public static string EatDigit(this string s, int posFrom)
        {
            int nLength = s.Length;
            int curPos = posFrom + s.EatSimpleDigit(posFrom);
            while ((curPos < nLength) && (s[curPos] == ' '))
                curPos++;
            if ((curPos < nLength) && (Char.ToUpper(s[curPos]) == 'E'))
            {
                curPos++;
                curPos += s.EatSimpleDigit(curPos);
            }
            int nEat = curPos - posFrom;
            return (nEat > 0) ? s.Substring(posFrom, nEat) : null;
        }
    }

    class BUOperators
    {
        #region static
        static BUOperators()
        {
            s_binarys = new IFEBinary[4];
            s_binarys[0] = s_binaryAdd;
            s_binarys[1] = s_binarySubtract;
            s_binarys[2] = s_binaryMultiply;
            s_binarys[3] = s_binaryDivide;

            s_braces = new IFormulaElement[2];
            s_braces[0] = s_leftBrace;
            s_braces[1] = s_rightBrace;
        }
        static public IFEBinary[] s_binarys;
        static public IFormulaElement[] s_braces;
        static public readonly FEMinus s_minus = new FEMinus();
        static public readonly FELeftBrace s_leftBrace = new FELeftBrace();
        static public readonly FERightBrace s_rightBrace = new FERightBrace();
        static public readonly FEBinaryAdd s_binaryAdd = new FEBinaryAdd();
        static public readonly FEBinarySubtract s_binarySubtract = new FEBinarySubtract();
        static public readonly FEBinaryMultiply s_binaryMultiply = new FEBinaryMultiply();
        static public readonly FEBinaryDivide s_binaryDivide = new FEBinaryDivide();

        static public FormulaParser s_instance = new FormulaParser();
        #endregion

        static public IFEBinary Parse(string s)
        {
            foreach (IFEBinary b in s_binarys)
            {
                if (string.Compare(s, b.ToString(), true) == 0)
                    return b;
            }
            return null;
        }

        //IFEBinary InverseBinary(IFEBinary b)
        //{
        //    if (b == s_binaryAdd) return s_binarySubtract;
        //    if (b == s_binarySubtract) return s_binaryAdd;
        //    if (b == s_binaryMultiply) return s_binaryDivide;
        //    if (b == s_binaryDivide) return s_binaryMultiply;

        //    return null;
        //}

    }

    class FormulaParser
    {
        #region static
        // static FormulaParser()
        // {
        //     s_binarys = new IFEBinary[4];
        //     s_binarys[0] = new FEBinaryAdd();
        //     s_binarys[1] = new FEBinarySubtract();
        //     s_binarys[2] = new FEBinaryMultiply();
        //     s_binarys[3] = new FEBinaryDivide();

        //     s_braces = new IFormulaElement[2];
        //     s_braces[0] = s_leftBrace;
        //     s_braces[1] = s_rightBrace;
        // }
        // static public IFEBinary[] s_binarys;
        // static public IFormulaElement[] s_braces;
        // static public readonly FEMinus s_minus = new FEMinus();
        //// static public readonly FESelf s_self = new FESelf();
        // static public readonly FELeftBrace s_leftBrace = new FELeftBrace();
        // static public readonly FERightBrace s_rightBrace = new FERightBrace();
        static public FormulaParser s_instance = new FormulaParser();
        #endregion

        //输入的公式
        protected string formula;
        public string Formula { set { formula = value; Reset(); } }
        protected string m_sVariant;
        public string Variant { set { m_sVariant = value; } }
        //公式的总长度
        protected Int32 length;
        protected void Reset() { length = formula.Length; }

        public FormulaParser() { }

        //protected IFormulaElement ParseOne(ref int pos) { return null; }
        protected IFEBinary ParseOneBinary(ref int curPos)
        {
            //去掉空格
            while ((curPos < length) && (formula[curPos] == ' '))
                curPos++;
            if (curPos >= length)
                return null;

            //如果是 ")"
            string temp = BUOperators.s_rightBrace.ToString();
            if (string.Compare(formula, curPos, temp, 0, temp.Length, true) == 0)
            {
                curPos += temp.Length;
                return null;
            }

            foreach (IFEBinary one in BUOperators.s_binarys)
            {
                temp = one.ToString();
                if (string.Compare(formula, curPos, temp, 0, temp.Length, true) == 0)
                {
                    curPos += temp.Length;
                    return one;
                }
            }
            return null;
        }
        protected IBxUnitConvert ParseOneParam(ref int curPos)
        {
            //去掉空格
            while ((curPos < length) && (formula[curPos] == ' '))
                curPos++;
            if (curPos >= length)
                return null;

            //如果是 "("
            string temp = BUOperators.s_leftBrace.ToString();
            if (string.Compare(formula, curPos, temp, 0, temp.Length, true) == 0)
            {
                curPos += temp.Length;
                return Parse(null, null, ref curPos);
            }

            //如果是自变量
            if (string.Compare(formula, curPos, m_sVariant, 0, m_sVariant.Length, true) == 0)
            {
                curPos += m_sVariant.Length;
                return new BUParsedVariant();
            }

            //如果是数字
            string sDigit = formula.EatDigit(curPos);
            if (sDigit != null)
            {
                double d;
                if (!double.TryParse(sDigit, NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                {
                    throw new Exception(string.Format("分析数据错误{0}", sDigit));
                }
                curPos += sDigit.Length;
                return new BUParsedNumber(d);
            }

            //判断是否是负号"-"
            bool bIsMinus;
            int nEat = StringDigitParse.EatSignum(formula, curPos, out bIsMinus);
            if (nEat > 0)
            {
                curPos += nEat;
                IBxUnitConvert next = ParseOneParam(ref curPos);
                if (next != null)
                {
                    if (bIsMinus)
                        return new BUParsedItem(new BUParsedNumber(-1), BUOperators.s_binarys[2], next);
                    else
                        return next;
                }
            }
            return null;
        }

        public IBxUnitConvert Parse(IBxUnitConvert left, IFEBinary mid, ref int pos)
        {
            IBxUnitConvert param = ParseOneParam(ref pos);
            if (param == null)
            {
                if (mid != null)
                    throw new Exception("操作符后未发现公式元素！");
                else
                    return left;
            }
            else
            {
                IFEBinary curOperator = ParseOneBinary(ref pos);
                if (curOperator == null) /*|| (curOperator.ElementType == EFormulaElementType.rightBrace)*/
                {
                    if (mid == null)
                        return param;
                    else
                        return new BUParsedItem(left, mid, param);
                }
                else
                {
                    if (mid == null)
                    {
                        return Parse(param, curOperator, ref pos);
                    }
                    else
                    {
                        if (mid.Grade >= curOperator.Grade)
                        {
                            return Parse(new BUParsedItem(left, mid, param), curOperator, ref pos);
                        }
                        else
                        {
                            IBxUnitConvert right = Parse(param, curOperator, ref pos);
                            if (right == null)
                                throw new Exception(string.Format("符号{0}之后的部分错误！", mid.ToString()));
                            return new BUParsedItem(left, mid, right);
                        }
                    }
                }
            }
        }
        public IBxUnitConvert Parse()
        {
            try
            {
                int nCurPos = 0;
                return Parse(null, null, ref nCurPos);
            }
            catch (System.Exception) 
            {
                return null;
            }
        }
    }

    public class Test
    {
        static public void Parse(string s, string variant)
        {
            FormulaParser.s_instance.Formula = s;
            FormulaParser.s_instance.Variant = variant;
            int nCurPos = 0;
            IBxUnitConvert root = FormulaParser.s_instance.Parse(null, null, ref nCurPos);

            double d = root.Calc(1.2);
            int a = 0;

        }
    }

}
