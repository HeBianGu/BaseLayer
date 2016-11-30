using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.EnumEx
{
    class Program
    {
        static void Main()
        {
          TestEnum ss=  "ttt".GetEnumByNameOrValue<TestEnum>();

          TestEnum ss1 = "0".GetEnumByNameOrValue<TestEnum>();

          Console.WriteLine((int)ss1);

          Console.Read();
        }
    }
}
