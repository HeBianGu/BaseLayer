#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/9 10:35:05  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：Control
 *
 * 说明：异步操作Winform控件
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HebianGu.ComLibModule.ControlEx
{
    /// <summary> 异步操作Winform控件 </summary>
    public static class InvokeHelper
    {

        #region - Start 委托声明 -

        private delegate object MethodInvokerEx(Control control, string methodName, params object[] args);

        private delegate object PropertyGetInvoker(Control control, object noncontrol, string propertyName);

        private delegate void PropertySetInvoker(Control control, object noncontrol, string propertyName, object value);

        #endregion - 委托声明 End -


        /// <summary> 获取控件指定属性 </summary>
        /// <param name="control"> 控件 </param>
        /// <param name="noncontrol"> 控件 </param>
        /// <param name="propertyName"> 属性名称 </param>
        /// <returns></returns>
        private static PropertyInfo GetPropertyInfoEx(this Control control, object noncontrol, string propertyName)
        {
            if (control != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo pi = null;
                Type t = null;

                if (noncontrol != null)
                    t = noncontrol.GetType();
                else
                    t = control.GetType();

                pi = t.GetProperty(propertyName);

                if (pi == null)
                    throw new InvalidOperationException(
                        string.Format(
                        "Can't find property {0} in {1}.",
                        propertyName,
                        t.ToString()
                        ));

                return pi;
            }
            else
                throw new ArgumentNullException("Invalid argument.");
        }

        /// <summary> 控件异步执行指定方法 </summary>
        /// <param name="control"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object InvokeEx(this Control control, string methodName, params object[] args)
        {
            if (control != null && !string.IsNullOrEmpty(methodName))
            {
                if (control.InvokeRequired)
                {
                    return control.Invoke(new MethodInvokerEx(InvokeEx), control, methodName, args);
                }

                else
                {
                    MethodInfo mi = null;
                    if (args != null && args.Length > 0)
                    {
                        Type[] types = new Type[args.Length];
                        for (int i = 0; i < args.Length; i++)
                        {
                            if (args[i] != null)
                            {
                                types[i] = args[i].GetType();
                            }
                        }
                        mi = control.GetType().GetMethod(methodName, types);
                    }
                    else
                    {
                        mi = control.GetType().GetMethod(methodName);
                    }
                    if (mi != null)
                    {
                        return mi.Invoke(control, args);
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid method.");
                    }
                }
            }
            else
            {
                throw new ArgumentNullException("Invalid argument.");
            }

        }

        /// <summary> 异步获取控件属性 </summary>
        public static object Get(this Control control, string propertyName)
        {
            return control.Get(null, propertyName);
        }

        /// <summary> 异步获取属性 </summary>
        /// <param name="control"> 控件 </param>
        /// <param name="noncontrol"> 控件 </param>
        /// <param name="propertyName"> 属性名称 </param>
        /// <returns></returns>
        public static object Get(this Control control, object noncontrol, string propertyName)
        {
            if (control != null && !string.IsNullOrEmpty(propertyName))
                if (control.InvokeRequired)
                    return control.Invoke(new PropertyGetInvoker(Get),
                        control,
                        noncontrol,
                        propertyName
                        );
                else
                {
                    PropertyInfo pi = control.GetPropertyInfoEx(noncontrol, propertyName);
                    object invokee = (noncontrol == null) ? control : noncontrol;

                    if (pi != null)
                        if (pi.CanRead)
                            return pi.GetValue(invokee, null);
                        else
                            throw new FieldAccessException(
                                string.Format(
                                "{0}.{1} is a write-only property.",
                                invokee.GetType().ToString(),
                                propertyName
                                ));

                    return null;
                }
            else
                throw new ArgumentNullException("Invalid argument.");
        }

        /// <summary> 异步设置控件属性 </summary>
        /// <param name="control"> 控件 </param>
        /// <param name="propertyName"> 属性名称 </param>
        /// <param name="value"> 属性值 </param>
        public static void Set(this Control control, string propertyName, object value)
        {
            Set(control, null, propertyName, value);
        }

        /// <summary> 异步设置控件属性 </summary>
        /// <param name="control"> 控件 </param>
        /// <param name="noncontrol"> 参考控件 </param>
        /// <param name="propertyName"> 属性名称 </param>
        /// <param name="value"> 设置的值 </param>
        public static void Set(this Control control, object noncontrol, string propertyName, object value)
        {
            if (control != null && !string.IsNullOrEmpty(propertyName))
                if (control.InvokeRequired)
                    control.Invoke(new PropertySetInvoker(Set),
                        control,
                        noncontrol,
                        propertyName,
                        value
                        );
                else
                {
                    PropertyInfo pi = control.GetPropertyInfoEx(noncontrol, propertyName);
                    object invokee = (noncontrol == null) ? control : noncontrol;

                    if (pi != null)
                        if (pi.CanWrite)
                            pi.SetValue(invokee, value, null);
                        else
                            throw new FieldAccessException(
                                string.Format(
                                "{0}.{1} is a read-only property.",
                                invokee.GetType().ToString(),
                                propertyName
                                ));
                }
            else
                throw new ArgumentNullException("Invalid argument.");
        }

        /// <summary> 异步执行MethodInvoker委托 </summary>
        /// <param name="control"> 控件 </param>
        /// <param name="method"> 执行委托 </param>
        public static void InvokeEx(this Control control, MethodInvoker method)
        {
            if (control.InvokeRequired)
            {
                MethodInvoker pMethodInvoker = (MethodInvoker)method;
                control.Invoke(pMethodInvoker);
            }
            else
            {
                method();
            }

        }

    }
}