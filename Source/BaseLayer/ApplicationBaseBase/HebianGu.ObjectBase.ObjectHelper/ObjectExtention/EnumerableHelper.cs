using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class EnumerableHelper
    {
        /// <summary> 改进的Aggerate扩展（示例代码，实际使用请添加空值检查） </summary>
        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, int, TSource> func)
        {
            int index = 0;

            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                enumerator.MoveNext();
                index++;
                TSource current = enumerator.Current;
                while (enumerator.MoveNext())
                    current = func(current, enumerator.Current, index++);
                return current;


            }
        }

        /// <summary> 将IEnumerable<T>转换为指定类型结合 </summary>
        public static R To<T, R>(this IEnumerable<T> source) where R : ICollection<T>, new()
        {
            int index = 0;

            R r = new R();

            using (IEnumerator<T> enumerator = source.GetEnumerator())
            {
                enumerator.MoveNext();

                index++;
                T current = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    r.Add(current);
                    index++;
                }

                return r;
            }
        }

        /// <summary> 将IEnumerable<T>转换为指定类型结合 </summary>
     
        public static void RemoveAtAfter<T>(this Collection<T> source, int index)
        {
            int temp = source.Count;

            for (int i = index; i < temp; i++)
            {
                source.RemoveAt(index);
            }
        }

    }
}
