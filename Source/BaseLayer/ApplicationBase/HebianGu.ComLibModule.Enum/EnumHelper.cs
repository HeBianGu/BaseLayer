using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.EnumEx
{
    /// <summary> 公用枚举扩展类  --   Add by lihaijun 2015.09.30 11：42 </summary>
    public static partial class EnumHelper
    {
        /// <summary>
        /// 获得枚举字段的特性(Attribute)，该属性不允许多次定义。
        /// </summary>
        /// <typeparam name="T">特性类型。</typeparam>
        /// <param name="value">一个枚举的实例对象。</param>
        /// <returns>枚举字段的扩展属性。如果不存在则返回 <c>null</c> 。</returns>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            return Attribute.GetCustomAttribute(field, typeof(T)) as T;
        }

        /// <summary> 将枚举的名称转换成枚举实例 </summary>
        public static T GetEnumByName<T>(this string str) where T : struct,IComparable
        {
            T testenum = (T)Enum.Parse(typeof(T), str, false);

            return testenum;
        }

        /// <summary> 获取枚举名称 </summary>
        public static string GetEnumName<T>(this Enum value)
        {
            return Enum.GetName(typeof(T), value);
        }


    }

    /// <summary> 基础操作 </summary>
    public static partial class EnumHelper
    {

        /// <summary> 返回指定枚举类型的指定值的描述 </summary>
        public static string GetDescription(Type t, object v)
        {
            try
            {
                FieldInfo fi = t.GetField(GetName(t, v));
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return (attributes.Length > 0) ? attributes[0].Description : GetName(t, v);
            }
            catch
            {
                return "UNKNOWN";
            }
        }

        private static string GetName(Type t, object v)
        {
            try
            {
                return Enum.GetName(t, v);
            }
            catch
            {
                return "UNKNOWN";
            }
        }


        /// <summary> 返回指定枚举类型的所有枚举项 </summary>
        public static SortedList GetStatus(Type t)
        {
            var list = new SortedList();

            Array a = Enum.GetValues(t);

            for (int i = 0; i < a.Length; i++)
            {
                string enumName = a.GetValue(i).ToString();

                var enumKey = (int)Enum.Parse(t, enumName);

                string enumDescription = GetDescription(t, enumKey);

                list.Add(enumKey, enumDescription);
            }
            return list;
        }

        /// <summary> 通过字符串获取枚举成员实例 </summary>
        public static T GetInstance<T>(string member) where T : class
        {
            return Enum.Parse(typeof(T), member, true) as T;
        }

        /// <summary> 获取枚举所有成员名称 </summary>
        public static string[] GetMemberNames<T>()
        {
            return Enum.GetNames(typeof(T));
        }

        /// <summary> 获取枚举所有成员值 </summary>
        public static Array GetMemberValues<T>()
        {
            return Enum.GetValues(typeof(T));
        }

        /// <summary> 获取枚举的基础类型 </summary>
        public static Type GetUnderlyingType(Type enumType)
        {
            //获取基础类型
            return Enum.GetUnderlyingType(enumType);
        }

        /// <summary> 检测枚举是否包含指定成员 </summary>
        public static bool IsDefined<T>(string member)
        {
            return Enum.IsDefined(typeof(T), member);
        }
    }
}
