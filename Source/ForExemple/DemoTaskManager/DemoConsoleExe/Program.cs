using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoConsoleExe
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i < 100;i++ )
            {
                Thread.Sleep(30);
                Console.WriteLine(i);
            }

            //Console.Read();
        }
    }
}
