using HebianGu.ComLibModule.ThreadEx._信号量_控制多线程;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ThreadEx
{
    class Program
    {
        static void Main()
        {
            //AutoResetEventDemo.Run();

            SemaphoreDemo.Run();
        }


        //    static void TestMoniter()
        //    {
        //        Action act = () =>
        //            {
        //                MoniterEngine.Do();
        //            };

        //        for (int i = 0; i < 5; i++)
        //        {
        //            Task t = new Task(act);

        //            Console.WriteLine("当前任务：" + t.Id);

        //            t.Start();
        //        }

        //        Console.Read();

        //    }

    }

    static class MoniterEngine
    {
        public static object obj = new object();


        public static void Do()
        {
            try
            {
                Monitor.Enter(obj);

                Console.WriteLine("正在写入文件！");

                Thread.Sleep(TimeSpan.FromSeconds(5));
            }

            catch (Exception ex)
            {
                Monitor.Exit(obj);
            }
        }


    }
}
