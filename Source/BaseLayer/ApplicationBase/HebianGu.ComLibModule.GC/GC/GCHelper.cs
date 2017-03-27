using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.GCHelper
{
    partial class GCProvider
    {

        public string GetCurrentMemory()
        {
           return  string.Format("{0:0.00} MB", GC.GetTotalMemory(true) / 1024.0 / 1024.0);
        }

    }


    /// <summary> 此类的说明 </summary>
    partial class GCProvider
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static GCProvider t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static GCProvider Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new GCProvider();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private GCProvider()
        {

        }
        #endregion - 单例模式 End -

    }
}
