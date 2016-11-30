using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HebianGu.ComLibModule.API
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("准备锁定计算机！");

            Thread.Sleep(3000);

            WindowsManager.Lock();

            Console.WriteLine("锁定完成！");

            Console.Read();
        }
    }
}
