using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Reflect
{
    public static class ReflectExt
    {

        /// <summary> 获取字段特性 </summary>
        [Obsolete("未测试", true)]
        public static T GetAttributeInfo<T>(this Type t) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(t, typeof(T));
        }

        /// <summary> 反射DLL获取所有类型 </summary>
        public static Type[] DllGetAllClasses(this string dllpath)
        {
            Assembly assembly = Assembly.LoadFrom(dllpath);
            Type[] aa = assembly.GetTypes();
            return aa;
        }

        /// <summary> 一堆dll中获取类 </summary>
        /// <param name="find"> 类名 "SessionState"</param>
        /// <param name="dir"> 文件夹 @"D:\project\website\XXX\website\XXX.Core\trunk\Lib\Dll" </param>
        public static T GetClass<T>(string find, string dir) where T : class
        {
            new DirectoryInfo(dir)
                .EnumerateFiles("*.dll", SearchOption.AllDirectories)
                .AsParallel()
                .Select(f => f.FullName)
            .Select(p =>
            {
                try
                {
                    return new
                    {
                        Path = p.ToLower().Replace(dir.ToLower(), string.Empty),
                        Assembly = Assembly.LoadFrom(p)
                    };
                }
                catch (BadImageFormatException) { return null; }
            }).Where(x => x != null)
            .Select(x =>
            {
                try
                {
                    return new
                    {
                        Path = x.Path,
                        Types = x.Assembly.GetTypes().AsParallel()
                                .Select(t => t.FullName)
                                .Where(s => s.IndexOf(find) > -1)
                    };
                }
                catch (ReflectionTypeLoadException) { return null; }
            }).Where(x => x != null && x.Types.Count() > 0)
                .ToList()
                .ForEach(x =>
                    Console.WriteLine(x.Path + "\n" + x.Types.Aggregate((a, b) => a + "\n" + b) + "\n\n")
                    );
            //  lihj新加
            return null;
        }

        /// <summary> 判断某个类是否继承自某个接口、类的方法： </summary>
        public static bool IsParent(this Type test, Type parent)
        {
            if (test == null || parent == null || test == parent || test.BaseType == null)
            {
                return false;
            }
            if (parent.IsInterface)
            {
                foreach (var t in test.GetInterfaces())
                {
                    if (t == parent)
                    {
                        return true;
                    }
                }
            }
            else
            {
                do
                {
                    if (test.BaseType == parent)
                    {
                        return true;
                    }
                    test = test.BaseType;
                } while (test != null);

            }
            return false;
        }







    }
}
