using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.Factory
{
    /// <summary> 业务工厂 </summary>
    public abstract class BLLFactory
    {
        //  单例
        private static BLLFactory _instance = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        public static BLLFactory  GetInstance()
        {
            if (_instance == null)
            {
                lock (localLock)
                {
                    if (_instance == null)
                    {
                        _instance = new f();
                    }
                }
            }
            return _instance;
        }

        //  无参数
        public T GetObject<T>()
        {
            Type t = typeof(T);
            return
            ObjectFactory.GetInstance()
                .GetObject<T>(Type.GetType(t.FullName + "+" + t.Name + "_"), null, false);
        }

        public object GetObject(Type t)
        {
            return
            ObjectFactory.GetInstance()
                .GetObject<object>(Type.GetType(t.FullName + "+" + t.Name + "_"), null, false);
        }

        class f : BLLFactory
        { }
    }
}
