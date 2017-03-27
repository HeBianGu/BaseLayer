using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace System.Linq
{

    /// <summary> 全局扩展方法 </summary>
    public static class SystemHelper
    {
        /// <summary> ScottGu In扩展 改进 </summary>
        public static bool In<T>(this T t, params T[] c)
        {
            return c.Any(i => i.Equals(t));

        }

        /// <summary> If扩展方法 </summary>
        public static T If<T>(this T t, Predicate<T> predicate, Func<T, T> func) where T : struct
        {
            return predicate(t) ? func(t) : t;
        }

        /// <summary> If扩展方法 </summary>
        public static T If<T>(this T t, Predicate<T> predicate, params Action<T>[] actions) where T : class
        {
            if (t == null) throw new ArgumentNullException();
            if (predicate(t))
            {
                foreach (var action in actions)
                    action(t);
            }
            return t;
        }

        public static string If(this string s, Predicate<string> predicate, Func<string, string> func)
        {
            return predicate(s) ? func(s) : s;
        }

        /// <summary> Switch方法扩展 </summary>
        public static TOutput Switch<TOutput, TInput>(this TInput input, IEnumerable<TInput> inputSource, IEnumerable<TOutput> outputSource, TOutput defaultOutput)
        {
            IEnumerator<TInput> inputIterator = inputSource.GetEnumerator();
            IEnumerator<TOutput> outputIterator = outputSource.GetEnumerator();

            TOutput result = defaultOutput;
            while (inputIterator.MoveNext())
            {
                if (outputIterator.MoveNext())
                {
                    if (input.Equals(inputIterator.Current))
                    {
                        result = outputIterator.Current;
                        break;
                    }
                }
                else break;
            }
            return result;
        }


        #region - Swich方法扩展 -

        #region SwithCase
        public class SwithCase<TCase, TOther>
        {
            public SwithCase(TCase value, Action<TOther> action)
            {
                Value = value;
                Action = action;
            }
            public TCase Value { get; private set; }
            public Action<TOther> Action { get; private set; }
        }
        #endregion

        #region Swith
        public static SwithCase<TCase, TOther> Switch<TCase, TOther>(this TCase t, Action<TOther> action) where TCase : IEquatable<TCase>
        {
            return new SwithCase<TCase, TOther>(t, action);
        }

        public static SwithCase<TCase, TOther> Switch<TInput, TCase, TOther>(this TInput t, Func<TInput, TCase> selector, Action<TOther> action) where TCase : IEquatable<TCase>
        {
            return new SwithCase<TCase, TOther>(selector(t), action);
        }
        #endregion

        #region Case
        public static SwithCase<TCase, TOther> Case<TCase, TOther>(this SwithCase<TCase, TOther> sc, TCase option, TOther other) where TCase : IEquatable<TCase>
        {
            return Case(sc, option, other, true);
        }


        public static SwithCase<TCase, TOther> Case<TCase, TOther>(this SwithCase<TCase, TOther> sc, TCase option, TOther other, bool bBreak) where TCase : IEquatable<TCase>
        {
            return Case(sc, c => c.Equals(option), other, bBreak);
        }  


        public static SwithCase<TCase, TOther> Case<TCase, TOther>(this SwithCase<TCase, TOther> sc, Predicate<TCase> predict, TOther other) where TCase : IEquatable<TCase>
        {
            return Case(sc, predict, other, true);
        }

        public static SwithCase<TCase, TOther> Case<TCase, TOther>(this SwithCase<TCase, TOther> sc, Predicate<TCase> predict, TOther other, bool bBreak) where TCase : IEquatable<TCase>
        {
            if (sc == null) return null;

            if (predict(sc.Value))
            {
                sc.Action(other);
                return bBreak ? null : sc;
            }
            else return sc;
        }
        #endregion

        #region Default
        public static void Default<TCase, TOther>(this SwithCase<TCase, TOther> sc, TOther other)
        {
            if (sc == null) return;
            sc.Action(other);
        }
        #endregion

        #endregion

    }
}
