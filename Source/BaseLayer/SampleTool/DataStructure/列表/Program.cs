using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 列表
{
    class Program
    {
        static void Main(string[] args)
        {
            //自定义集合类
            ListEx<Racer> racers = new ListEx<Racer>();
            racers.Add(new Racer(1, "Tom", "Json", "English", 2));
            racers.Add(new Racer(2, "Lee", "Tai", "China", 2));
            racers.Add(new Racer(3, "Kate", "Json", "America", 2));
            racers.ForEach(l => l.Wins++);

            //系统自带集合类
            List<Racer> racersList = new List<Racer>();
            racersList.Capacity = 3;
            racersList.TrimExcess();
            racersList.ForEach(l => l.Wins++);
           
        }
    }
}
