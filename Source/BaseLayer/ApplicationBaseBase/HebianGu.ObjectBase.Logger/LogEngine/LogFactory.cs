using HebianGu.ObjectBase.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.Logger
{
    /// <summary> 日志泛型缓存工厂 （应用于直接自带的日志引擎，对现有已经定义好的殷勤调用方便） </summary>
    public partial class LogFactory
    {

        // Todo ：日志缓存 
        static List<ILogInterface> _cache = new List<ILogInterface>();

        /// <summary> 获取指定类型的日志服务 </summary>
        public T GetLogService<T>() where T : class,ILogInterface, new()
        {
            // Todo ：查找指定类型的日志 
            ILogInterface t = _cache.Find(l => l is T);

            if (t != null)
            {
                return t as T;
            }
            else
            {
                // Todo ：新生成添加到缓存 
                T n = new T();

                _cache.Add(n);

                return n;
            }
        }


    }

    /// <summary> 此类的说明 </summary>
    partial class LogFactory
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static LogFactory t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static LogFactory Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new LogFactory();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private LogFactory()
        {

        }
        #endregion - 单例模式 End -

    }
}
