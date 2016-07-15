using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Formule
{
    /// <summary> 公式解析类 </summary>
    public class ComputeFormula
    {
        internal class ComputeFormula_ : ComputeFormula
        {

        }
        /// <summary> 执行公式 </summary>
        public string Run(string str)
        {
            Formula rpn = new Formula();
            if (rpn.Parse(str))
            {
                return rpn.Evaluate().ToString();
            }
            else
            {
                return "公式无效";
            }
        }

        /// <summary> 解析公式 </summary>
        /// <param name="str">公式字符串</param>
        /// <param name="cs">分割符</param>
        /// <param name="oparator">运算符</param>
        /// <param name="func">解析方法</param>
        /// <returns>解析后的字符串</returns>
        public string Format(string str, char[] cs, string[] oparator, FormulaFunc<string, object[], string> func, object[] objs)
        {
            var str1 = str.Split(cs
                , StringSplitOptions.RemoveEmptyEntries);

            StringBuilder strFind =
                new StringBuilder();

            foreach (string s in str1)
            {
                //不是运算符
                if (Array.Exists(oparator, l => s.Contains(l)))
                {

                    strFind.Append(s);
                }
                else
                {

                    //计算参数
                    strFind.Append(func(s, objs));

                }
            }
            string ssss = strFind.ToString();
            return strFind.ToString();
        }

        /// <summary> 解析公式 </summary>
        public string Format(string str, FormulaFunc<string, object[], string> func, object[] objs)
        {
            char[] cs =
                new char[] { '[', ']' };

            string[] strPara =
                new string[] { "+", "-", "*", "/", "(", ")" };

            return Format(str, cs, strPara, func, objs);
        }


    }

    public delegate TResult FormulaFunc<in T, in T1, out TResult>(T arg, T1 o);
}
