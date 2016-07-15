using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovariantAndInverter
{
    /* 协变
 //IEnumerable<string>strings = new List<string>();
 //IEnumerable<object> objects = strings;
 
 //大家看到了么一个声明为IEnumerable<string>的 接口类型被赋给了一个更低 级别的IEnumerable<object>.
 对，这就是协变。再来看一个例子：*/


    class Base
    {
        public static void PrintBases(IEnumerable<Base> bases)
        {
            foreach (Base b in bases)
            {
                Console.WriteLine(b);
            }
        }
    }

    class Derived : Base
    {
        public static void Main()
        {
            List<Derived> dlist = new List<Derived>();

            Derived.PrintBases(dlist);//由于IEnumerable<T>接口是协变的，所以PrintBases(IEnumerable<Base> bases)
            //可以接收一个更加具体化的IEnumerable<Derived>作为其参数。
            IEnumerable<Base> bIEnum = dlist;
        }
    }

    /* // 逆变
 // Assume that the following method is in the class: 
 // static void SetObject(object o) { } 
 Action<object> actObject = SetObject;
 Action<string> actString = actObject; 
 //委托actString中以后要使用更加精细化的类型string不能再使用object啦！
 string strHello(“Hello”); 
 actString(strHello);*/
}
