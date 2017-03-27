using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.DelegateEx
{
    public static class DelegeteHelper
    {

        public static bool IsRegistedMethod(this Delegate handle, string methodName)
        {
            return handle == null ? false : handle.GetInvocationList().ToList().Exists(l => l.Method.Name == "Report");
        }

    }


}
