using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.WinHelper
{
    /// <summary> Windows系统帮助类 </summary> 
    public partial class WinSysHelper
    {
        /// <summary> 获取系统资源文件 </summary>
        public string GetSystemPath(Environment.SpecialFolder folderEnum)
        {
            if (Environment.SpecialFolder.MyComputer == folderEnum)
                return "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

            return Environment.GetFolderPath(folderEnum);
        }




    }


    /// <summary> 此类的说明 </summary>
    partial class WinSysHelper
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static WinSysHelper t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static WinSysHelper Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new WinSysHelper();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private WinSysHelper()
        {

        }
        #endregion - 单例模式 End -

    }
}
