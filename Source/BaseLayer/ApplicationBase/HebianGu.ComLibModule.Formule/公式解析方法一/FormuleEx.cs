using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Formule._2
{
    /// <summary> 公式扩展方法 </summary>
    public static class FormuleEx
    {
        /// <summary> 计算公式 </summary>
        public static string ComputeFormula(this string formule)
        {
            Formula rpn = new Formula();

            if (rpn.Parse(formule))
            {
                return rpn.Evaluate().ToString();
            }
            else
            {
                return null;
            }
        }

    }
}
