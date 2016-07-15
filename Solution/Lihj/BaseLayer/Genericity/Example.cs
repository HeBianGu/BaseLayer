using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genericity
{
    //  一.派生约束
    /// <summary> 1.常见的 </summary>
    public class MyClass5<T> where T : IComparable { }

    /// <summary> 2.约束放在类的实际派生之后 </summary>
    public class B { }
    public class MyClass6<T> : B where T : IComparable { }
    /// <summary> 3.可以继承一个基类和多个接口，且基类在接口前面 </summary>
    public class B1 { }
    public class MyClass7<T> where T : B, IComparable, ICloneable { }
    //  二.构造函数约束
    /// <summary> 1.常见的 </summary>
    public class MyClass81<T> where T : new() { }
    /// <summary> 2.可以将构造函数约束和派生约束组合起来,前提是构造函数约束出现在约束列表的最后 </summary>
    public class MyClass8<T> where T : IComparable, new() { }
    //  三.值约束
    /// <summary> 1.常见的 </summary>
    public class MyClass9<T> where T : struct { }

    /// <summary> 2.与接口约束同时使用，在最前面(不能与基类约束,构造函数约束一起使用) </summary>
    public class MyClass11<T> where T : struct, IComparable { }
    //  四.引用约束
    /// <summary> 1.常见的 </summary>
    public class MyClass10<T> where T : class { }

    //  五.多个泛型参数
    public class MyClass12<T, U>
        where T : IComparable
        where U : class { }
    //  六.继承和泛型
    public class B<T> { }
    /// <summary>1. 在从泛型基类派生时,可以提供类型实参,而不是基类泛型参数 </summary>
    public class SubClass11 : B<int>
    { }
    /// <summary> 2.如果子类是泛型,而非具体的类型实参,则可以使用子类泛型参数作为泛型基类的指定类型 </summary>
    public class SubClass12<R> : B<R>
    { }

    /// <summary> 3.在子类重复基类的约束(在使用子类泛型参数时,必须在子类级别重复在基类级别规定的任何约束) </summary>
    public class B2<T> where T : ISomeInterface { }
    public class SubClass2<T> : B2<T> where T : ISomeInterface { }

    /// <summary> 4.构造函数约束 </summary>
    public class B1<T> where T : new()
    {
        public T SomeMethod()
        {
            return new T();
        }
    }
    public class SubClass3<T> : B<T> where T : new() { }
    //  七.泛型方法(C#2.0泛型机制支持在"方法声名上包含类型参数",这就是泛型方法)
    /// <summary> 1.泛型方法既可以包含在泛型类型中,又可以包含在非泛型类型中 </summary>
    public class MyClass5
    {
        public void MyMethod<T>(T t) { }
    }
    /// <summary> 2.泛型方法的声明与调用 </summary>
    public class App5
    {
        public void CallMethod()
        {
            MyClass5 myclass5 = new MyClass5();
            myclass5.MyMethod<int>(3);
        }
    }
    /// 3.泛型方法的重载
    //第一组重载
    public class text
    {
        /* 李海军
        void MyMethod1<T>(T t, int i) { }

        void MyMethod1<U>(U u, int i) { }
        //第二组重载
        void MyMethod2<T>(int i) { }
        void MyMethod2(int i) { }
        //第三组重载，假设有两个泛型参数
        void MyMethod3<T>(T t) where T : B1 { }
        void MyMethod3<T>(T t) where T : B { }   */
    }


    //第四组重载
    public class MyClass8<T, U>
    {
        public T MyMothed(T a, U b)
        {
            return a;
        }
        public T MyMothed(U a, T b)
        {
            return b;
        }
        public int MyMothed(int a, int b)
        {
            return a + b;
        }
    }
    //  4.泛型方法的覆写
    public class MyBaseClass1
    {
        public virtual void MyMothed<T>(T t) where T : new() { }
    }
    public class MySubClass1 : MyBaseClass1
    {
        public override void MyMothed<T>(T t) //不能重复任何约束
        { }
    }
    public class MyBaseClass2
    {
        public virtual void MyMothed<T>(T t)
        { }
    }
    public class MySubClass2 : MyBaseClass2
    {
        public override void MyMothed<T>(T t) //重新定义泛型参数T
        { }
    }
    //  八.虚拟方法
    public class BaseClass4<T>
    {
        public virtual T SomeMethod()
        {
            return default(T);
        }
    }
    public class SubClass4 : BaseClass4<int> //使用实参继承的时候方法要使用实参的类型
    {
        public override int SomeMethod()
        {
            return 0;
        }
    }
    public class SubClass5<T> : BaseClass4<T> //使用泛型继承时,方法也是泛型
    {
        public override T SomeMethod()
        {
            return default(T);
        }
    }
    //  九.编译器只允许将泛型参数隐式强制转换到 Object 或约束指定的类型
    class MyClass<T> where T : BaseClass, ISomeInterface
    {
        void SomeMethod(T t)
        {
            ISomeInterface obj1 = t;
            BaseClass obj2 = t;
            object obj3 = t;
        }
    }
    //  变通方法:使用临时的 Object 变量，将泛型参数强制转换到其他任何类型
    class MyClass2<T>
    {
        void SomeMethod(T t)
        {
            object temp = t;
            BaseClass obj = (BaseClass)temp;
        }
    }
    //  十.编译器允许您将泛型参数显式强制转换到其他任何接口，但不能将其转换到类
    class MyClass1<T>
    {
        void SomeMethod(T t)
        {
            ISomeInterface obj1 = (ISomeInterface)t;
            //BaseClass obj2 = (BaseClass)t;           //不能通过编译
        }
    }

    //  十一.使用临时的 Object 变量，将泛型参数强制转换到其他任何类型
    class MyClass21<T>
    {
        void SomeMethod(T t)
        {
            object temp = t;
            BaseClass obj = (BaseClass)temp;
        }
    }
    //  十二.使用is和as运算符
    public class MyClass3<T>
    {
        public void SomeMethod(T t)
        {
            if (t is int) { }
            if (t is LinkedList<int>) { }
            string str = t as string;
            if (str != null) { }
            LinkedList<int> list = t as LinkedList<int>;
            if (list != null) { }
        }
    }

}

public interface ISomeInterface
{ }

public class BaseClass
{ }
