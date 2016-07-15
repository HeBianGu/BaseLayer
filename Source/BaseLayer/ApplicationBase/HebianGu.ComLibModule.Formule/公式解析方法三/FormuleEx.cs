using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Formule._3
{
    /// <summary> 公式扩展方法 </summary>
    public static class FormuleEx
    {
        /// <summary> 计算公式 </summary>
        public static string ComputeFormula(this string formule)
        {
            MSScriptControl.ScriptControl sc = new MSScriptControl.ScriptControl();
            sc.Language = "JavaScript";
            return sc.Eval(formule).ToString();
            //sc.Eval("((2*3)-5+(3*4))+6/2").ToString();//1+12+3
        }

    }
}
