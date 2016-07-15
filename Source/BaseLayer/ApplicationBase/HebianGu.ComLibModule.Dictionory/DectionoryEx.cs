using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.DictionoryEx
{
    public static class DectionoryEx
    {
        /// <summary> 遍历字典对值执行Func操作 </summary>
        public static Dictionary<string, double> ForeachEx(this Dictionary<string, double> dic, Func<double, double> func)
        {
            Dictionary<string, double> dicNew = new Dictionary<string, double>();

            foreach (string d in dic.Keys)
            {
                double v;

                if (dic.TryGetValue(d, out v))
                {
                    dicNew.Add(d, func(v));
                }
            }

            return dicNew;
        }

        /// <summary> 遍历字典对值执行Func操作 </summary>
        public static List<string[]> ForeachEx(this List<string[]> list, Func<double, double> func)
        {
            List<string[]> listNew = new List<string[]>();

            list.ForEach(
                l =>
                {

                    string[] strs = l.Clone() as string[];

                    strs[5] = func(Double.Parse(strs[5])).ToString();

                    listNew.Add(strs);
                }
                );

            return listNew;
        }
    }
}
