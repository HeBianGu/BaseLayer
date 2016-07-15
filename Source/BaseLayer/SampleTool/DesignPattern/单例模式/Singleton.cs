using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 单例模式
{
    //
    //摘要：
    //      单例模式一
    //      1、不可以在类外面new
    //      2、线程安全
    //      缺点：不可实现带参数的实例
    class Singleton
    {

        /// <summary> 单例 </summary>
        private static Singleton instance = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建单例 </summary>
        /// <returns></returns>
        public static Singleton CreateInstance()
        {
            if (instance == null)
            {
                lock (localLock)
                {
                    if (instance == null)
                        return instance = new Singleton();
                }
            }
            return instance;
        }

        /// <summary> 私有化构造函数 </summary>
        private Singleton()
        {
            Console.WriteLine("实例化了一个单例对象!");
            Console.Write("");

        }

    }


    //
    //摘要：
    //      单例模式二
    //      1、不可以在类外面new
    //      2、代码简洁、实用
    //      缺点：不可实现带参数的实例
    class Single
    {

        /// <summary> 单例 </summary>
        public static readonly Single instance = null;

        static Single()
        {
            instance = new Single();
        }

        /// <summary> 私有化构造函数 </summary>
        private Single()
        {
            Console.WriteLine("实例化了一个简单单例对象!");
        }
    }
}
