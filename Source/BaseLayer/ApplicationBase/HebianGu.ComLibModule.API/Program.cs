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
            //Console.WriteLine("准备锁定计算机！");

            //Thread.Sleep(3000);

            ////WindowsAPI..Instance.Lock();

            //Console.WriteLine("锁定完成！");

            //Console.Read();

            

            HookKeyboardEngine.KeyUp += HookKeyboardEngine_KeyUp;

            HookKeyboardEngine.KeyDown += HookKeyboardEngine_KeyDown;

            HookKeyboardEngine.KeyPress += HookKeyboardEngine_KeyPress;

            Console.Read();
        }

        private static void HookKeyboardEngine_KeyPress(object sender, KeyPressEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void HookKeyboardEngine_KeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void HookKeyboardEngine_KeyUp(object sender, KeyEventArgs e)
        {

            string ss = string.Empty;
        }
        
    }
}
