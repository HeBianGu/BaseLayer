using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.CMD
{
    class Program
    {
        static void Main(string[] args)
        {

            // Todo ：打开记事本 
            Process.Start("notepad");

            // Todo ：打开路径 
            Process.Start(@"E:\test");

            // Todo ：打开文件
            Process.Start(@"E:\test\test.txt");

            Console.Read();

        }
    }
}
