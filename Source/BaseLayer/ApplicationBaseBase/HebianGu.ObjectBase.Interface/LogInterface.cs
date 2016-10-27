using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.Interface
{

    /// <summary> 日志接口 </summary>
   public interface ILogInterface
    {
        /// <summary> 运行日志 </summary>
        void RunLog(string message);

        /// <summary> 错误日志 </summary>
        void ErrorLog(string message);

        /// <summary> 错误日志 </summary>
        void ErrorLog(Exception ex);




    }
}
