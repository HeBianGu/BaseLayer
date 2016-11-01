using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.ObjectHelper
{
    public static class ObjectHelper
    {

        /// <summary> 判断是否为空 </summary>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary> 获取特性 指定参数的类型的特性 </summary>
        public static T GetAttributeInfo<T>(this object o) where T : Attribute
        {
            Type t = o.GetType();

            return (T)Attribute.GetCustomAttribute(t, typeof(T));
        }

        /// <summary> 获取特性 指定参数的字段特性 </summary>
        public static T GetAttribute<T>(this MemberInfo o) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(o, typeof(T));
        }
    }
}
