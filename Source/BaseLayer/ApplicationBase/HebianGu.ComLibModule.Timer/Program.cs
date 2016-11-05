using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.TimerHelper
{
    static class Program
    {

        static void Main()
        {
            int v = 0;

            DayTimerEngine day = new DayTimerEngine();

            day.StartTime = new DateTime(1999, 11, 22, 16, 59, 20);

            day.EndTime = new DateTime(1999, 11, 22, 18, 59, 30);


            day.Register(() => Console.WriteLine(v++));

            day.Register(() => Console.WriteLine(v--));

            day.Start();

            Console.Read();
        }
    }
}
