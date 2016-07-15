#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/9 18:16:24
 * 文件名：Reflect
 * 说明：
 * 有时候，为了快速批量处理已经实现某个基类或者某个接口的子类，需要通过反射的方式获取到他们的类类型(Type),然后再通过

1
Activator.CreateInstance(objType);
或者

1
Assembly.Load(path).CreateInstance(typeName);
或者

1
Assembly.LoadFile(filePath).CreateInstance(typeName);
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Reflect
{
    public static class ReflectEx
    {



        public class MethodMaker
        {

            /// <summary>
            /// 创建对象（当前程序集）
            /// </summary>
            /// <param name="typeName">类型名</param>
            /// <returns>创建的对象，失败返回 null</returns>
            public static object CreateObject(string typeName)
            {
                object obj = null;
                try
                {
                    Type objType = Type.GetType(typeName, true);
                    obj = Activator.CreateInstance(objType);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex);
                }
                return obj;
            }

            /// <summary>
            /// 创建对象(外部程序集)
            /// </summary>
            /// <param name="path"></param>
            /// <param name="typeName">类型名</param>
            /// <returns>创建的对象，失败返回 null</returns>
            public static object CreateObject(string path, string typeName)
            {
                object obj = null;
                try
                {

                    obj = Assembly.Load(path).CreateInstance(typeName);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex);
                }

                return obj;
            }
        }

        public abstract class BaseProcessor
        {
            public abstract int Calc(int a, int b);
        }

        public class Adder : BaseProcessor
        {
            public override int Calc(int a, int b)
            {
                return a + b;
            }
        }

        public class Multiplier : BaseProcessor
        {
            public override int Calc(int a, int b)
            {
                return a * b;
            }
        }
    }
}
