using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HebianGu.ComLibModule.API
{
    /// <summary> 说明 </summary>
    partial class WinAPIServer
    {
        private bool DoExitWin(int DoFlag)
        {
            bool ok;
            WindowsAPI.TokPriv1Luid tp;
            IntPtr hproc = WindowsAPI.GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = WindowsAPI.OpenProcessToken(hproc, WindowsAPI.TOKEN_ADJUST_PRIVILEGES | WindowsAPI.TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = WindowsAPI.SE_PRIVILEGE_ENABLED;
            ok = WindowsAPI.LookupPrivilegeValue(null, WindowsAPI.SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = WindowsAPI.AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = WindowsAPI.ExitWindowsEx(DoFlag, 0);
            return ok;
        }

        /// <summary>  锁定计算机 </summary>
        public void Lock()
        {
            WindowsAPI.LockWorkStation();
        }

        /// <summary>  重新启动  </summary>
        public bool Reboot()
        {
            return DoExitWin(WindowsAPI.EWX_FORCE | WindowsAPI.EWX_REBOOT);
        }

        /// <summary> 关机 </summary>
        public bool PowerOff()
        {
            return DoExitWin(WindowsAPI.EWX_FORCE | WindowsAPI.EWX_POWEROFF);
        }
        /// <summary>  注销  </summary>
        public bool LogOff()
        {
            return DoExitWin(WindowsAPI.EWX_FORCE | WindowsAPI.EWX_LOGOFF);
        }
    }


    /// <summary> 此类的说明 </summary>
    public partial class WinAPIServer
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static WinAPIServer t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static WinAPIServer Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new WinAPIServer();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private WinAPIServer()
        {

        }
        #endregion - 单例模式 End -

    }
}
