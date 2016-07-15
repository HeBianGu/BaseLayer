using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Formule._1
{
    /// <summary> 公式扩展方法 </summary>
    public static class FormuleEx
    {
        /// <summary> 计算公式 </summary>
        public static string ComputeFormula(this string  formule)
        {
            Microsoft.JScript.Vsa.VsaEngine vsa 
                = Microsoft.JScript.Vsa.VsaEngine.CreateEngine();

            return Microsoft.JScript.Eval.JScriptEvaluate(formule, vsa).ToString();
        }
       
    }
}
