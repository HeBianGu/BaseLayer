using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Reflect
{
    public static class AssemblyHelper
    {
        /// <summary> 获取当前执行的应用程序 </summary>
        public static Assembly GetCurrentAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }


        /// <summary> 获取程序集中继承T类型的所有实例 </summary>
        public static List<T> GetAssemblyClass<T>(this Assembly ass) where T : class,new()
        {
            List<T> ls = new List<T>();

            Type[] classes = ass.GetTypes();

            classes = classes.ToList().FindAll(l => typeof(T).IsAssignableFrom(l)).ToArray();

            if (classes == null || classes.Length == 0)
            {
                return null;
            }

            classes.OrderBy(l => l.Name);


            foreach (Type t in classes)
            {
                T output = Activator.CreateInstance(t) as T;
                ls.Add(output);
            }

            return ls;
        }
    }
}
