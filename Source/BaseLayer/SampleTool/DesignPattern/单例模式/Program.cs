using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 单例模式
{
    class Program
    {
        static void Main(string[] args)
        {

            //数据和任务的并行
            Parallel.For(1, 10, l =>
                {
                    Console.WriteLine("任务ID:{0}，线程ID:{1}",Task.CurrentId,Thread.CurrentThread.ManagedThreadId);


                    //获取单例
                    Singleton singleton = Singleton.CreateInstance();

                    //获取简单单例
                    Single single = Single.instance;
                }
                );

            Console.Read();
        }
    }
}
