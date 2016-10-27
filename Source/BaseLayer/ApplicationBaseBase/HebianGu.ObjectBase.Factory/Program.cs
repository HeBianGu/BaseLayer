using HebianGu.ObjectBase.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Factory
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            TestDemo.Instance.WriteLine("使用Instance单例调用");

            TestDemo.InstanceByName("调用多例一").WriteLine(Environment.UserName);

            TestDemo.InstanceByName("调用多例二").WriteLine(Environment.UserName);
        }
    }

    class TestDemo : BaseFactory<TestDemo>
    {
        public void WriteLine(string str)
        {
            Console.WriteLine(MethodInfo.GetCurrentMethod().Name + "   :   " + str);
        }
    }
}
