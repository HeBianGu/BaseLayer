using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    /// <summary> 循环执行 </summary>
    public static class WhileHelper
    {

        /// <summary> 循环执行指定次数任务 </summary>
        public static void DoCount(this int count, params Action<int>[] act)
        {

            while (count > 0)
            {
                foreach (var item in act)
                {
                    item.Invoke(count);
                }
               

                count--;
            }
        }

        /// <summary> 循环执行指定次数任务 </summary>
        public static void DoCount(this int start, int end, params Action<int>[] act)
        {
            while (end > start)
            {
                foreach (var item in act)
                {
                    item.Invoke(end);
                }

                end--;
            }
        }

        /// <summary> While方法扩展 </summary>
        public static void While<T>(this T t, Predicate<T> predicate, Action<T> action) where T : class
        {
            while (predicate(t)) action(t);
        }

        /// <summary> While方法扩展 </summary>
        public static void While<T>(this T t, Predicate<T> predicate, params Action<T>[] actions) where T : class
        {
            while (predicate(t))
            {
                foreach (var action in actions)
                    action(t);
            }
        }


    }
}
