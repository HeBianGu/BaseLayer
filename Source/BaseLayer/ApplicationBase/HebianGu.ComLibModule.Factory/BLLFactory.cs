using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Factory
{
    /// <summary> 业务工厂 </summary>
    public abstract class BLLFactory
    {
        //  单例
        private static BLLFactory _instance = null;

        public static BLLFactory  GetInstance()
        {
            if (_instance == null)
            {
                lock (typeof(BLLFactory))
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
