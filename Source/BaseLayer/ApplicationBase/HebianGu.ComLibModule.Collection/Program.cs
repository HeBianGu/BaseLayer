using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Collection
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                TMap<Tuple<string, string>>.Instance.Set(new Tuple<string, string>(i.ToString(), "值" + i.ToString()));
            }


            Tuple<string, string> v = TMap<Tuple<string, string>>.Instance.Get(l => l.Item1 == "5");

            Console.WriteLine(v.Item2);

            Console.Read();

            
        }

    }
}
