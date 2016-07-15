#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/12/3 18:11:32
 * 文件名：ReflectFactory
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Reflect
{
    /// <summary> 反射创建对象工厂 </summary>
    public static class ReflectFactory
    {

        /// <summary> 反射创建对象 注：已知类型和参数 </summary> 
        public static T CreateInstance<T>(params object[] objs) where T : class
        {
            Type t = typeof(T);

            return Activator.CreateInstance(t, objs) as T;
        }


        /// <summary> 创建远程对象 </summary> 
        public static T CreateInstance<T>(string url) where T : class
        {

            Type t = typeof(T);

            return Activator.GetObject(t, url) as T;
        }

        /// <summary> 创建对象 通过对象的Type </summary>
        public static object CreateInstance(this Type t, params object[] objs)
        {
            return Activator.CreateInstance(t, objs);
        }

        /// <summary> 获取另一程序集非托管句柄 </summary>
        public static ObjectHandle CreateInstance(string assemblyName, string typeName)
        {
            return Activator.CreateInstance(assemblyName, typeName, null);
        }

        /// <summary> 创建COM实例 </summary>
        public static ObjectHandle ComCreateInstance(string assemblyName, string typeName)
        {
            return Activator.CreateComInstanceFrom(assemblyName, typeName);
        }


        /// <summary> 创建程序集类  程序集名称和类的全名 </summary>
        public static object CreateAssInstance(string assPath, string classNameWithNameSpace, params object[] objs)
        {
            Assembly ass = Assembly.Load(assPath);
            //Assembly ass = Assembly.LoadFrom("ClassLibrary831.dll");
            Type t = ass.GetType(classNameWithNameSpace);   //参数必须是类的全名
            return Activator.CreateInstance(t, objs);


        }

        /// <summary> 创建dll类  dll名称和类的全名 ClassLibrary831.dll </summary>
        public static object CreateDllInstance(string dllFullPath, string classNameWithNameSpace, params object[] objs)
        {
            Assembly ass = Assembly.LoadFrom(dllFullPath);
            Type t = ass.GetType(classNameWithNameSpace);   //参数必须是类的全名
            return Activator.CreateInstance(t, objs);
        }

        /// <summary> 执行程序集中指定类名的指定方法 已知程序集 类全名 方法名和方法参数 </summary>
        public static void CmdAssClassMethod(string assName, string classNameWithNameSpace, string methodName, params object[] objs)
        {
            //通过程序集的名称反射
            Assembly ass = Assembly.Load(assName);
            Type t = ass.GetType(classNameWithNameSpace);
            object o = Activator.CreateInstance(t, objs);
            MethodInfo mi = t.GetMethod(methodName);
            mi.Invoke(o, objs);
        }

        /// <summary> 执行DLL中指定类名的指定方法 已知程序集 类全名 方法名和方法参数 </summary>
        public static void CmdDllClassMethod(string dllFullPath, string classNameWithNameSpace, string methodName, params object[] objs)
        {
            Assembly assembly = Assembly.LoadFrom(dllFullPath);
            Type t = assembly.GetType(classNameWithNameSpace);
            object o = Activator.CreateInstance(t, objs);
            MethodInfo mi = t.GetMethod(methodName);
            mi.Invoke(o, null);
        }

        /// <summary> 反射DLL获取所有类型 </summary>
        public static Type[] DllGetAllClasses(this string dllpath)
        {
            Assembly assembly = Assembly.LoadFrom(dllpath);
            Type[] aa = assembly.GetTypes();
            return aa;
        }

        /// <summary> 反射DLL获取所有类型 </summary>
        public static Type[] AssGetAllClasses(this string assPath)
        {
            Assembly assembly = Assembly.Load(assPath);
            Type[] aa = assembly.GetTypes();
            return aa;
        }

        /// <summary> 获取指定命名空间 </summary>
        public static Type[] GetNameSpaceAllClass(string pNameSpace)
        {
            Type[] classes = Assembly.Load(pNameSpace).GetTypes();
            return classes;
        }

    }
}
