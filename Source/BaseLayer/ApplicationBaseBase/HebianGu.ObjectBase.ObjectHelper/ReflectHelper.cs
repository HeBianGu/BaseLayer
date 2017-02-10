using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.ObjectHelper
{

    /// <summary> 利用反射对Object扩展 </summary>
    public static class ReflectHelper
    {

        /// <summary> 判断两个类型的关系类型可以是父类，接口 用法：父类.IsAssignableFrom(子类)</summary>
        public static bool IsAssignableFrom<T>(this object obj)
        {
            Type t = obj.GetType();

            return t.IsAssignableFrom(typeof(T));
        }

        /// <summary> 判断对象是否是指定类型型可以是父类，接口 用法：父类.IsInstanceOfType(子类对象)</summary>
        public static bool IsInstanceOfType<T>(this object obj)
        {
            Type t = obj.GetType();

            return t.IsInstanceOfType(typeof(T));
        }

        /// <summary> 判断两个类型的关系类型不可以是接口 用法：子类.IsSubClassOf(父类) </summary>
        public static bool IsSubclassOf<T>(this object obj)
        {
            Type t = obj.GetType();

            return t.IsSubclassOf(typeof(T));
        }

        /// <summary> 检查指定类型中是否包含指定构造函数 Type[] parameters = { typeof(string),typeof(DataTable) }</summary>
        public static bool IsHaveParamConstruct(this object obj, Type[] parameters)
        {

            Type t = obj.GetType();

            System.Reflection.ConstructorInfo ci = t.GetConstructor(parameters);

            return ci != null;

        }

        /// <summary> 检查指定类型中是否包含无参数构造函数 </summary>
        public static bool IsHaveNoParamConstruct(this object obj, Type[] parameters)
        {
            return obj.IsHaveParamConstruct(Type.EmptyTypes);
        }


        /// <summary> 类型名称 </summary>
        public static string NameOf(this object obj)
        {
            Type t = obj.GetType();

            return t.Name;
        }


        /// <summary> 应用在基类设置子类属性 </summary>
        public static void SetProperty(this object obj, string proName, object proValue)
        {
            Type t = obj.GetType();

            var prop = t.GetProperty(proName);

            prop.SetValue(obj, proValue);
        }


        /// <summary> 应用在基类获取子类属性 </summary>
        public static object GetProperty(this object obj, string proName)
        {
            Type t = obj.GetType();

            var prop = t.GetProperty(proName);

            return prop.GetValue(obj);
        }

        /// <summary> 执行指定方法 </summary>
        public static object InvokeMethod(this object obj, string methodName, params object[] parameters)
        {
            Type t = obj.GetType();

            Type[] paramsTypes = parameters.Select(l => l.GetType()).ToArray();

            var method = t.GetMethod(methodName, paramsTypes);

            return method.Invoke(obj, parameters);
        }

        /// <summary> 执行指定事件的所有委托 </summary>
        public static void InvokeEvent(this object obj, string eventName, params object[] parameters)
        {
            var deles = obj.GetObjectEventList(eventName);

            if (deles == null) return;

            // Todo ：执行委托方法 
            foreach (var item in deles)
            {
                item.DynamicInvoke(parameters);
            }
        }

        /// <summary> 清空指定事件 </summary>
        public static void ClearEvent(this object obj, string eventName)
        {
            Type t = obj.GetType();

            var ev = t.GetEvent(eventName);

            var deles = obj.GetObjectEventList(eventName);

            // Todo ：执行委托方法 
            foreach (var item in deles)
            {
                ev.RemoveEventHandler(obj, item);
            }
        }

        /// <summary> 是否包含指定事件 </summary>
        public static bool IsHaveRegisterEvent(this object obj, string eventName,string registerMethodName)
        {
            Type t = obj.GetType();

            var ev = t.GetEvent(eventName);

            Delegate[] ds = ev.GetObjectEventList(eventName);

           return ds.ToList().Exists(l => l.Method.Name == registerMethodName);
        }

        ///// <summary> 是否包含指定事件 </summary>
        //public static bool IsHaveRegisterEvent(this Type t, string eventName, string registerMethodName)
        //{
        //    var ev = t.GetEvent(eventName);

        //    var eventHandle = t.GetEvent(eventName);

        //    if (eventHandle == null) return false;

        //    //FieldInfo fieldInfo = (t.GetField(p_EventName, BindingFlags.Static | BindingFlags.NonPublic));

        //    MethodInfo[] ms = eventHandle.GetOtherMethods();

        //    return ms.ToList().Exists(l => l.Name == registerMethodName);

        //    return false;
        //}

        /// <summary> 注册事件 </summary>
        public static void AddEvent(this object obj, string eventName, Delegate dele)
        {
            Type t = obj.GetType();

            var ev = t.GetEvent(eventName);

            ev.AddEventHandler(obj, dele);
        }

        /// <summary> 注册事件 </summary>
        public static void AddEvent(this object obj, string eventName, MethodInfo method)
        {
            var e = obj.GetType().GetEvent(eventName);

            Delegate dele = Delegate.CreateDelegate(e.DeclaringType, method);

            obj.AddEvent(eventName, dele);
        }

        /// <summary> 获取指定事件的所有注册委托 </summary> 
        public static Delegate[] GetObjectEventList(this object obj, string p_EventName)
        {
            Type t = obj.GetType();

            var _PropertyInfo = t.GetField(p_EventName, BindingFlags.Instance | BindingFlags.NonPublic);

            if (_PropertyInfo == null) return null;

            Delegate _EventList = (Delegate)_PropertyInfo.GetValue(obj);

            if (_EventList == null) return null;

            return _EventList.GetInvocationList();
        }

        /// <summary> 获取类型静态事件的所有注册委托 </summary> 
        public static Delegate[] GetStaticEventList(this Type t, string p_EventName)
        {
            var _PropertyInfo = t.GetEvent(p_EventName);

            if (_PropertyInfo == null) return null;

            //FieldInfo fieldInfo = (t.GetField(p_EventName, BindingFlags.Static | BindingFlags.NonPublic));

            _PropertyInfo.GetOtherMethods();
            //Delegate _EventList = (Delegate)_PropertyInfo.GetValue(null);

            //if (_EventList == null) return null;

            //return _EventList.GetInvocationList();
            return null;
        }

    }


    public class TestBase
    {
        private string p1;
        /// <summary> 说明 </summary>
        public string P1
        {
            get { return p1; }
            set { p1 = value; }
        }

    }

    public class TestBase1 : TestBase
    {
        private string p2;
        /// <summary> 说明 </summary>
        public string P2
        {
            get { return p2; }
            set { p2 = value; }
        }

        public void M1()
        {
            Console.WriteLine("执行方法" + MethodInfo.GetCurrentMethod().Name);
        }

        public void M1(int p1, int p2)
        {
            Console.WriteLine("执行方法" + MethodInfo.GetCurrentMethod().Name);

            Console.WriteLine("参数" + p1.ToString());

            Console.WriteLine("参数" + p2.ToString());
        }

        public TestBase1()
        {

            ActEventHandler += () =>
              {
                  Console.WriteLine("注册的方法1");
              };
            ActEventHandler += () =>
            {
                Console.WriteLine("注册的方法2");
            };

            ActEventHandler += M;

            ActEventHandler += M4;

            ActEventHandler.GetInvocationList();

        }

        public event Action ActEventHandler;


        public void M()
        {
            Console.WriteLine("注册的方法M");
        }


        public void M4()
        {
            Console.WriteLine("注册的方法M4");
        }

    }
}
