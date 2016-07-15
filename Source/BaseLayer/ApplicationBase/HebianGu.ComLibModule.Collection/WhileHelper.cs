using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Collection
{
    /// <summary> 循环执行 </summary>
    public static class WhileHelper
    {

        /// <summary> 循环执行指定次数任务 </summary>
        public void DoCount(int count, Action act)
        {

            while (count > 0)
            {
                act.Invoke();

                count--;
            }
        }

        /// <summary> 循环执行指定次数任务 </summary>
        public void DoCount(int start, int end, Action act)
        {
            while (end > start)
            {
                act.Invoke();

                end--;
            }
        }
    }
}
