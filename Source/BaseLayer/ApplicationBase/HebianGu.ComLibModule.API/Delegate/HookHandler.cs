using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HebianGu.ComLibModule.API.Delegate
{

    /// <summary> 钩子触发的委托 </summary>
    internal delegate int HookProc(int nCode, int wParam, IntPtr lParam);


}
