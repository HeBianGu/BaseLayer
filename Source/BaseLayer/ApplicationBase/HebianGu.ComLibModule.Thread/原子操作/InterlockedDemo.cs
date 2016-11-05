using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ThreadEx.原子操作
{
    class InterlockedDemo
    {
        //全局变量
        private static int _result;

        //Main方法
        static void Main(string[] args)
        {
            //运行后按住 Enter 键数秒，使用Interlocked.Increment(ref _result); 与 _result++ 的不同
            while (true)
            {
                Task[] _tasks = new Task[100];
                int i = 0;

                for (i = 0; i < _tasks.Length; i++)
                {
                    _tasks[i] = Task.Factory.StartNew((num) =>
                    {
                        var taskid = (int)num;
                        Work(taskid);
                    }, i);
                }

                Task.WaitAll(_tasks);
                Console.WriteLine(_result);

                Console.ReadKey();
            }
        }

        //线程调用方法
        private static void Work(int TaskID)
        {
            for (int i = 0; i < 10; i++)
            {
                _result++;
                //Interlocked.Increment(ref _result);
            }
        }
    }
}
