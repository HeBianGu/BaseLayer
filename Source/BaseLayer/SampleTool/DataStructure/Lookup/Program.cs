using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 列表;

namespace Lookup
{
    class Program
    {
        static void Main(string[] args)
        {
            //非常类似Dictionary,不能像一般的字典那样创建，必须调用ToLookUp()方法

            List<Racer> racers = new List<Racer>();

            racers.Add(new Racer(1, "234", "343", "dfdg"));
            racers.Add(new Racer(1, "234", "343", "dfdg"));
            racers.Add(new Racer(1, "234", "343", "dfdg"));
            racers.Add(new Racer(1, "234", "343", "dfdg"));
            racers.Add(new Racer(1, "234", "343", "dfdg"));
            racers.Add(new Racer(1, "234", "343", "dfdg"));

            //按contry分组显示Lookup
            var lookupRacer= racers.ToLookup(l => l.Contry);


            //显示分组中的所有人员
            foreach(var contry in  lookupRacer["dfdg"])
            {

                Console.WriteLine(contry.LastName);
            }

            

        }
    }
    
}
