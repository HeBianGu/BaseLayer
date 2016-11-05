using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.CaculateEngine
{
    class SplitDataEngine
    {
        public List<List<T>> SpiltSpace<T>(List<T> regions, Func<T, string> getValue, Predicate<string> matchValue) where T : class
        {
            List<List<T>> result = new List<List<T>>();

            List<T> outItems = new List<T>();

            result.Add(outItems);

            for (int i = 0; i < regions.Count; i++)
            {
                T item = regions[i];

                if (i == 0)
                {
                    outItems.Add(item);
                    continue;
                }
                // Todo ：泛型获取值的规则外挂 对应的列值
                string v = getValue(item);

                string old = getValue(regions[i - 1]);

                if (!matchValue(v) && matchValue(old))
                {
                    // Todo ：最后一个不匹配的项 
                    T temp = outItems.Last(l => !matchValue(getValue(l)));

                    // Todo ：上下行格式不一样
                    outItems = new List<T>();
                    result.Add(outItems);

                    // Todo ：从不匹配到匹配 多增加一行 
                    outItems.Add(temp);
                }

                outItems.Add(item);
            }

            return result;

        }

    }
}
