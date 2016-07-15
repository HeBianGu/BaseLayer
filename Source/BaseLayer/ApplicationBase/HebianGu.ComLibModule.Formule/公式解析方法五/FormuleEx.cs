using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Formule._5
{
    /// <summary> 公式扩展方法 </summary>
    public static class FormuleEx
    {
        /// <summary> 计算公式 </summary>
        public static string ComputeFormula(this string formule)
        {
            DataTable dt = new DataTable();
            return dt.Compute("1*2+3", "false").ToString();

        }

    }
}
