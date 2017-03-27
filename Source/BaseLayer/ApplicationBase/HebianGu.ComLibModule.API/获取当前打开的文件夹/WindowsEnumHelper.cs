using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HebianGu.ComLibModule.API
{
    public partial class WindowsEnumHelper
    {
        #region - API -

        public delegate bool CallBack(int hwnd, int y);

        //该函数枚举所有屏幕上的顶层窗口，并将窗口句柄传送给应用程序定义的回调函数。
        //回调函数返回FALSE将停止枚举，否则EnumWindows函数继续到所有顶层窗口枚举完为止。
        [DllImport("user32.dll")]
        public static extern int EnumWindows(CallBack x, int y);

        /// <summary> 该函数将指定窗口的标题条文本（如果存在）拷贝到一个缓存区内 </summary>
        [DllImport("user32.dll")]
        public static extern int GetWindowText(int hwnd, StringBuilder lptrString, int nMaxCount);


        /// <summary> 该函数获得一个指定子窗口的父窗口句柄 </summary>
        [DllImport("user32.dll")]
        public static extern int GetParent(int hwnd);


        /// <summary> 该函数获得给定窗口的可视状态。 </summary>
        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(int hwnd);

        //获取窗体类名
        [DllImport("User32.Dll ")]
        public static extern void GetClassName(IntPtr hwnd, StringBuilder s, int nMaxCount);


        /// <summary> 获取窗体的子窗体句柄 </summary>
        [DllImport("User32.dll ")]
        public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childe, string strclass, string FrmText);


        #endregion

        #region - Mehtod -

        /// <summary> 根据句柄获取类名 </summary>
        private string GetFormClassName(IntPtr ptr)
        {
            StringBuilder nameBiulder = new StringBuilder(255);
            GetClassName(ptr, nameBiulder, 255);
            return nameBiulder.ToString();
        }


        /// <summary> 根据句柄获取窗口标题 </summary>
        private string GetFormTitle(IntPtr ptr)
        {
            StringBuilder titleBiulder = new StringBuilder(255);
            GetWindowText((int)ptr, titleBiulder, 255);
            return titleBiulder.ToString();
        }

        #endregion

        CallBack _callBackHandle;

        public CallBack CallBackHandle
        {
            get
            {
                return _callBackHandle;
            }

            set
            {
                _callBackHandle = value;
            }
        }

        /// <summary> 回掉函数 </summary> 
        public bool Report(int hwnd, int lParam)
        {
            int pHwnd = GetParent(hwnd);

            //如果再没有父窗口并且为可视状态的窗口，则遍历
            if (pHwnd == 0 && IsWindowVisible(hwnd) == true)
            {
                IntPtr cabinetWClassIntPtr = new IntPtr(hwnd);
                string cabinetWClassName = GetFormClassName(cabinetWClassIntPtr);
                //如果类名为CabinetWClass ，则为explorer窗口，可以通过spy++查看窗口类型
                if (cabinetWClassName.Equals("CabinetWClass", StringComparison.OrdinalIgnoreCase))
                {
                    //下面为一层层往下查找，直到找到资源管理器的地址窗体，通过他获取窗体地址
                    IntPtr workerWIntPtr = FindWindowEx(cabinetWClassIntPtr, IntPtr.Zero, "WorkerW", null);
                    IntPtr reBarWindow32IntPtr = FindWindowEx(workerWIntPtr, IntPtr.Zero, "ReBarWindow32", null);
                    IntPtr addressBandRootIntPtr = FindWindowEx(reBarWindow32IntPtr, IntPtr.Zero, "Address Band Root", null);
                    IntPtr msctls_progress32IntPtr = FindWindowEx(addressBandRootIntPtr, IntPtr.Zero, "msctls_progress32", null);
                    IntPtr breadcrumbParentIntPtr = FindWindowEx(msctls_progress32IntPtr, IntPtr.Zero, "Breadcrumb Parent", null);
                    IntPtr toolbarWindow32IntPtr = FindWindowEx(breadcrumbParentIntPtr, IntPtr.Zero, "ToolbarWindow32", null);


                    string title = GetFormTitle(toolbarWindow32IntPtr);
                    Console.WriteLine(title);

                    int index = title.IndexOf(':');
                    index++;
                    string path = title.Substring(index, title.Length - index);
                    Console.WriteLine(path);
                }
            }
            return true;
        }

        /// <summary> 入口方法： 注册获取 </summary>
        public void Register(int index = 0)
        {

            this._callBackHandle += Report;

            bool isRegistered = _callBackHandle == null ? false : _callBackHandle.GetInvocationList().ToList().Exists(l => l.Method.Name == "Report");

            if (!isRegistered)
            {
                this._callBackHandle += Report;
            }


            EnumWindows(_callBackHandle, index);
        }
    }


    /// <summary> 此类的说明 </summary>
    partial class WindowsEnumHelper
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static WindowsEnumHelper t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static WindowsEnumHelper Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new WindowsEnumHelper();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private WindowsEnumHelper()
        {

        }
        #endregion - 单例模式 End -

    }
}
