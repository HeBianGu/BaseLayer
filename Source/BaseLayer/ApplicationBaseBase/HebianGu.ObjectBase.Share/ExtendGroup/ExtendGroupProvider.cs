using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.Share
{
    public static class ExtendGroupProvider
    {
        private static Dictionary<Type, Type> cache = new Dictionary<Type, Type>();


        /// <summary> string类型分组扩展方法 </summary>
        public static T As<T>(this string v) where T : IExtend<string>
        {
            return As<T, string>(v);
        }

        /// <summary> object类型分组扩展方法 </summary>
        public static T As<T>(this object v) where T : IExtend<object>
        {
            return As<T, object>(v);
        }


        /// <summary> 泛型基本分组扩展方法 </summary>
        public static T As<T, V>(this V v) where T : IExtend<V>
        {
            Type t;
            Type valueType = typeof(V);
            if (cache.ContainsKey(valueType))
            {
                t = cache[valueType];
            }
            else
            {
                t = CreateType<T, V>();
                cache.Add(valueType, t);
            }
            object result = Activator.CreateInstance(t, v);
            return (T)result;
        }


        /// <summary> 通过反射发出动态实现接口T </summary>
        private static Type CreateType<T, V>() where T : IExtend<V>
        {
            Type targetInterfaceType = typeof(T);

            string generatedClassName = targetInterfaceType.Name.Remove(0, 1);
            //
            AssemblyName aName = new AssemblyName("ExtensionDynamicAssembly");
            AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);

            ModuleBuilder mb = ab.DefineDynamicModule(aName.Name);
            TypeBuilder tb = mb.DefineType(generatedClassName, TypeAttributes.Public);

            //实现接口
            tb.AddInterfaceImplementation(typeof(T));

            //value字段
            FieldBuilder valueFiled = tb.DefineField("value", typeof(V), FieldAttributes.Private);

            //构造函数
            ConstructorBuilder ctor = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { typeof(V) });
            ILGenerator ctor1IL = ctor.GetILGenerator();

            ctor1IL.Emit(OpCodes.Ldarg_0);
            ctor1IL.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));
            ctor1IL.Emit(OpCodes.Ldarg_0);
            ctor1IL.Emit(OpCodes.Ldarg_1);
            ctor1IL.Emit(OpCodes.Stfld, valueFiled);
            ctor1IL.Emit(OpCodes.Ret);

            //GetValue方法
            MethodBuilder getValueMethod = tb.DefineMethod("GetValue", MethodAttributes.Public | MethodAttributes.Virtual, typeof(V), Type.EmptyTypes);
            ILGenerator numberGetIL = getValueMethod.GetILGenerator();
            numberGetIL.Emit(OpCodes.Ldarg_0);
            numberGetIL.Emit(OpCodes.Ldfld, valueFiled);
            numberGetIL.Emit(OpCodes.Ret);

            //接口实现
            MethodInfo getValueInfo = targetInterfaceType.GetInterfaces()[0].GetMethod("GetValue");
            tb.DefineMethodOverride(getValueMethod, getValueInfo);
            //
            Type t = tb.CreateType();
            return t;


            // HTodo  ：模样如下 
            //    class ConvertableString : IConvertableString
            //{
            //    private string value;
            //    public ConvertableString(string value)
            //    {
            //        this.value = value;
            //    }
            //    public string GetValue()
            //    {
            //        return value;
            //    }
            //}
        }

    }
}
