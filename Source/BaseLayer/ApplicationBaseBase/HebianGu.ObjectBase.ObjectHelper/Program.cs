using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.ObjectHelper
{
    class Program
    {
        static void Main(string[] args)
        {

            TestBase b = new TestBase1();

            //b.SetProperty("P2", "sssss");

            //Base1 bs = b as Base1;

            //Console.WriteLine(bs.P2);


            //b.InvokeMethod("M1", 1, 2);

            //b.InvokeEvent("ActEventHandler");

            //var deles = b.GetObjectEventList("ActEventHandler");


            //foreach (var item in deles)
            //{
            //    item.Method.Invoke(b, new object[] { });
            //}



            b.ClearEvent("ActEventHandler");

            var m = b.GetType().GetMethod("M4");

            

            b.AddEvent("ActEventHandler", m);

            b.InvokeEvent("ActEventHandler");

            Console.Read();


        }
    }
}
