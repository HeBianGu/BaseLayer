using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{

    /// <summary> 基础类型扩展 </summary>
    public static class ObjectExtend
    {

        /// <summary> 判断是否为空 </summary>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary> 拆箱 </summary>
        public static T Cast<T>(this object obj)
        {
            return (T)obj;
        }

        /// <summary> 是否只指定类型 </summary>
        public static bool Is<T>(this object obj)
        {
            return obj is T;
        }

        /// <summary> 获取特性 指定参数的类型的特性 </summary>
        public static T GetAttributeInfo<T>(this IGroupBaseObject o) where T : Attribute
        {
            Type t = o.GetType();

            return (T)Attribute.GetCustomAttribute(t, typeof(T));
        }

        /// <summary> 获取特性 指定参数的字段特性 </summary>
        public static T GetAttribute<T>(this MemberInfo o) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(o, typeof(T));
        }

        /// <summary> 深复制 </summary>
        [Obsolete("未测试过")]
        public static object DeepCopy(this IGroupBaseObject o)
        {
            Type t = o.GetType();
            PropertyInfo[] properties = t.GetProperties();
            Object p = t.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, o, null);
            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanWrite)
                {
                    object value = pi.GetValue(o, null);
                    pi.SetValue(p, value, null);
                }
            }
            return p;
        }
    }
}
