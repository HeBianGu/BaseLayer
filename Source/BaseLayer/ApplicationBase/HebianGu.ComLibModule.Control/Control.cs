#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/9 10:35:05  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����Control
 *
 * ˵�����첽����Winform�ؼ�
 * 
 * 
 * �޸��ߣ�           ʱ�䣺               
 * �޸�˵����
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
    /// <summary> �첽����Winform�ؼ� </summary>
    public static class InvokeHelper
    {

        #region - Start ί������ -

        private delegate object MethodInvokerEx(Control control, string methodName, params object[] args);

        private delegate object PropertyGetInvoker(Control control, object noncontrol, string propertyName);

        private delegate void PropertySetInvoker(Control control, object noncontrol, string propertyName, object value);

        #endregion - ί������ End -


        /// <summary> ��ȡ�ؼ�ָ������ </summary>
        /// <param name="control"> �ؼ� </param>
        /// <param name="noncontrol"> �ؼ� </param>
        /// <param name="propertyName"> �������� </param>
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

        /// <summary> �ؼ��첽ִ��ָ������ </summary>
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

        /// <summary> �첽��ȡ�ؼ����� </summary>
        public static object Get(this Control control, string propertyName)
        {
            return control.Get(null, propertyName);
        }

        /// <summary> �첽��ȡ���� </summary>
        /// <param name="control"> �ؼ� </param>
        /// <param name="noncontrol"> �ؼ� </param>
        /// <param name="propertyName"> �������� </param>
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

        /// <summary> �첽���ÿؼ����� </summary>
        /// <param name="control"> �ؼ� </param>
        /// <param name="propertyName"> �������� </param>
        /// <param name="value"> ����ֵ </param>
        public static void Set(this Control control, string propertyName, object value)
        {
            Set(control, null, propertyName, value);
        }

        /// <summary> �첽���ÿؼ����� </summary>
        /// <param name="control"> �ؼ� </param>
        /// <param name="noncontrol"> �ο��ؼ� </param>
        /// <param name="propertyName"> �������� </param>
        /// <param name="value"> ���õ�ֵ </param>
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

        /// <summary> �첽ִ��MethodInvokerί�� </summary>
        /// <param name="control"> �ؼ� </param>
        /// <param name="method"> ִ��ί�� </param>
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