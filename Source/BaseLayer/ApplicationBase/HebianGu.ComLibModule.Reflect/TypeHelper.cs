using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Reflect
{
    public static class TypeHelper
    {
        /// <summary> 检查指定类型是否有构造函数 </summary>
        public static bool IsHaveNoParamConstruct(this Type t)
        {
            return t.IsHaveNoParamConstruct(Type.EmptyTypes);
        }



        /// <summary> 检查指定类型中是否包含指定构造函数 Type[] parameters = { typeof(string),typeof(DataTable) }</summary>
        public static bool IsHaveNoParamConstruct(this Type t, Type[] parameters)
        {
            System.Reflection.ConstructorInfo ci = t.GetConstructor(parameters);

            return ci != null;

        }


    }
}
