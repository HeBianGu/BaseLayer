using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{


    /// <summary> 比较信息扩展 </summary>
    public static class ComparerExtend
    {

        /// <summary> 是否在两者之间 </summary>
        public static bool IsBetween<T>(this T t, T lowerBound, T upperBound, IComparer<T> comparer,bool includeLowerBound = false, bool includeUpperBound = false)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");

            var lowerCompareResult = comparer.Compare(t, lowerBound);
            var upperCompareResult = comparer.Compare(t, upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);
        }
    }
}
