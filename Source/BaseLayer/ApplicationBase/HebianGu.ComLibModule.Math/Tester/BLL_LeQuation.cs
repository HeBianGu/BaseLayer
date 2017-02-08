using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.MathHelper
{
    /// <summary> 线性代数业务类 </summary>
    public class BLL_LeQuation
    {
        /// <summary> 计算线性代数矩阵 </summary>
        public Matrix OperateLeQuation(Matrix matrixAll)
        {
            LEquations l = new LEquations();
            return l.GetRootSetGauss(matrixAll);
        }
    }
}
