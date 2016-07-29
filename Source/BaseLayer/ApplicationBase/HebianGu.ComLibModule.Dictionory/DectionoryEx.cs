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
        public static Dictionary<TKey, TValue> ForeachEx<TKey, TValue>(this Dictionary<TKey, TValue> dic, Func<TValue, TValue> func)
        {
            Dictionary<TKey, TValue> dicNew = new Dictionary<TKey, TValue>();

            foreach (TKey d in dic.Keys)
            {
                TValue v;

                if (dic.TryGetValue(d, out v))
                {
                    dicNew.Add(d, func(v));
                }
            }

            return dicNew;
        }

        /// <summary> 对二维表的指定列进行fuc操作 </summary>
        public static List<string[]> ForeachEx(this List<string[]> list, Func<double, double> func, int col)
        {
            List<string[]> listNew = new List<string[]>();

            list.ForEach(
                l =>
                {

                    string[] strs = l.Clone() as string[];

                    strs[col] = func(Double.Parse(strs[col])).ToString();

                    listNew.Add(strs);
                }
                );

            return listNew;
        }

        public static void RemoveAll<TKey, TValue>(this Dictionary<TKey, TValue> dic, Predicate<KeyValuePair<TKey, TValue>> macth)
        {
            foreach (KeyValuePair<TKey, TValue> v in dic)
            {
                if (macth(v))
                {
                    dic.Remove(v.Key);
                }
            }
        }
    }
}
