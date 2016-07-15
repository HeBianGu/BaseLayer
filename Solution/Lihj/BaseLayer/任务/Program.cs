using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 任务
{
    class Program
    {

        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            //等待按下任意一个键取消任务
            TaskFactory taskFactory = new TaskFactory();
            Task[] tasks = new Task[]
                {
                    taskFactory.StartNew(() => Add(cts.Token)),
                    taskFactory.StartNew(() => Add(cts.Token)),
                    taskFactory.StartNew(() => Add(cts.Token))
                };
            //CancellationToken.None指示TasksEnded不能被取消
            taskFactory.ContinueWhenAll(tasks, TasksEnded, CancellationToken.None);
            Console.ReadKey();
            cts.Cancel();
            Console.ReadKey();
        }

        static void TasksEnded(Task[] tasks)
        {
            Console.WriteLine("所有任务已完成！");
        }
        static int Add(CancellationToken ct)
        {
            Console.WriteLine("任务开始……");
            int result = 0;
            while (!ct.IsCancellationRequested)
            {
                result++;
                Thread.Sleep(1000);
            }
            return result;
        }

        //static void Main(string[] args)
        //{
        //    CancellationTokenSource cts = new CancellationTokenSource();
        //    Task<int> t = new Task<int>(() => AddCancleByThrow(cts.Token), cts.Token);
        //    t.Start();
        //    t.ContinueWith(TaskEndedByCatch);
        //    //等待按下任意一个键取消任务
        //    Console.ReadKey();
        //    cts.Cancel();
        //    Console.ReadKey();
        //}

        //static void TaskEndedByCatch(Task<int> task)
        //{
        //    Console.WriteLine("任务完成，完成时候的状态为：");
        //    Console.WriteLine("IsCanceled={0}tIsCompleted={1}tIsFaulted={2}", task.IsCanceled, task.IsCompleted, task.IsFaulted);
        //    try
        //    {
        //        Console.WriteLine("任务的返回值为：{0}", task.Result);
        //    }
        //    catch (AggregateException e)
        //    {
        //        e.Handle((err) => err is OperationCanceledException);
        //    }
        //}

        //static int AddCancleByThrow(CancellationToken ct)
        //{
        //    Console.WriteLine("任务开始……");
        //    int result = 0;
        //    while (true)
        //    {
        //        ct.ThrowIfCancellationRequested();
        //        result++;
        //        Thread.Sleep(1000);
        //    }
        //    return result;
        //}
        //static void Main(string[] args)
        //{
        //    CancellationTokenSource cts = new CancellationTokenSource();
        //    Task<int> t = new Task<int>(() => Add(cts.Token), cts.Token);
        //    t.Start();
        //    t.ContinueWith(TaskEnded);
        //    //等待按下任意一个键取消任务
        //    Console.ReadKey();
        //    cts.Cancel();
        //    Console.ReadKey();
        //}




        //static void TaskEnded(Task<int> task)
        //{
        //    Console.WriteLine("任务完成，完成时候的状态为：");
        //    Console.WriteLine("IsCanceled={0}tIsCompleted={1}tIsFaulted={2}", task.IsCanceled, task.IsCompleted, task.IsFaulted);
        //    Console.WriteLine("任务的返回值为：{0}", task.Result);
        //}

        //static int Add(CancellationToken ct)
        //{
        //    Console.WriteLine("任务开始……");
        //    int result = 0;
        //    while (!ct.IsCancellationRequested)
        //    {
        //        result++;
        //        Thread.Sleep(1000);
        //    }
        //    return result;
        //}
        //static void Main(string[] args)
        //{
        //    Task parent = new Task(() =>
        //    {
        //        //   1、 取消任务类
        //        var cts = new CancellationTokenSource();
        //        var taskFactory = new TaskFactory<Int32>(
        //            cts.Token,
        //            TaskCreationOptions.AttachedToParent, // 2、 优先级
        //            TaskContinuationOptions.ExecuteSynchronously, // 3、 创建的任务指定行为
        //            TaskScheduler.Default);

        //        //  4、 创建并启动3个子任务

        //        var childTasks = new[] { 
        //              taskFactory.StartNew(() => Sum(cts.Token, 100)), 
        //              taskFactory.StartNew(() => Sum(cts.Token, 200)), 
        //              taskFactory.StartNew(() => Sum(cts.Token, Int32.MaxValue))  // 这个会抛异常

        //      };


        //        //  设置任何子任务抛出异常就取消其余子任务

        //        for (int i = 0; i < childTasks.Length; i++)
        //        {
        //            childTasks[i].ContinueWith(l => cts.Cancel(), TaskContinuationOptions.OnlyOnFaulted);
        //        }



        //        // 所有子任务完成后，从未出错/未取消的任务获取返回的最大值 
        //        // 然后将最大值传给另一个任务来显示最大结果

        //        taskFactory.ContinueWhenAll(childTasks,
        //            completedTasks => completedTasks.Where(t => !t.IsFaulted && !t.IsCanceled).Max(t => t.Result),
        //            CancellationToken.None).ContinueWith(t => Console.WriteLine("The maxinum is: " + t.Result),
        //            TaskContinuationOptions.ExecuteSynchronously).Wait(); // Wait用于测试

        //    });


        //    // 子任务完成后，也显示任何未处理的异常

        //    parent.ContinueWith(p =>
        //    {
        //        // 用StringBuilder输出所有
        //        StringBuilder sb = new StringBuilder("The following exception(s) occurred:" + Environment.NewLine);
        //        foreach (var e in p.Exception.Flatten().InnerExceptions)
        //        {
        //            sb.AppendLine("   " + e.GetType().ToString());
        //        }

        //        Console.WriteLine(sb.ToString());
        //    }, TaskContinuationOptions.OnlyOnFaulted);


        //    // 启动父任务

        //    parent.Start();

        //    while(true)
        //    {
        //        Thread.Sleep(1000);
        //        if(parent.IsFaulted)
        //        {
        //            Console.WriteLine("异常退出");
        //        }

        //        if (parent.IsCanceled)
        //        {
        //            Console.WriteLine("取消退出");
        //        }

        //        if (parent.IsCompleted)
        //        {
        //            Console.WriteLine("完成");
        //        }
               
        //    }

        //    Console.Read();
        //    //try
        //    //{
        //    //    parent.Wait(); //显示结果

        //    //}
        //    //catch (AggregateException)
        //    //{


        //    //}


        //}


        //private static Int32 Sum(CancellationToken ct, Int32 n)
        //{
        //    Int32 sum = 0;

        //    while (true)
        //    {
        //        if (n < 0)
        //            break;
        //            n--;
        //            sum += n;
        //    }
        //    return sum;
        //}


        //static void Main(string[] args)
        //{
        //    CancellationTokenSource cts = new CancellationTokenSource();


        //    Task<Int32> t = new Task<Int32>(() => Sum(cts.Token, 10000), cts.Token);

        //    //可以现在开始，也可以以后开始 

        //    t.Start();

        //    t.ContinueWith(task => Console.WriteLine("The sum is:{0}", task.Result),
        //        TaskContinuationOptions.OnlyOnRanToCompletion);

        //    t.ContinueWith(task => Console.WriteLine("Sum throw:" + task.Exception),
        //        TaskContinuationOptions.OnlyOnFaulted);

        //    t.ContinueWith(task => Console.WriteLine("Sum was cancel:" + task.IsCanceled),
        //        TaskContinuationOptions.OnlyOnCanceled);
        //    try
        //    {
        //        t.Wait();  // 测试用

        //    }
        //    catch (AggregateException)
        //    {
        //        Console.WriteLine("出错");
        //    }


        //    //在之后的某个时间，取消CancellationTokenSource 以取消Task

        //    cts.Cancel();//这是个异步请求，Task可能已经完成了。我是双核机器，Task没有完成过 16 

        //    //注释这个为了测试抛出的异常 19             //Console.WriteLine("This sum is:" + t.Result);
        //    try
        //    {
        //        //如果任务已经取消了，Result会抛出AggregateException

        //        Console.WriteLine("This sum is:" + t.Result);
        //    }
        //    catch (AggregateException x)
        //    {
        //        //将任何OperationCanceledException对象都视为已处理。
        //        //其他任何异常都造成抛出一个AggregateException，其中 
        //        //只包含未处理的异常

        //        x.Handle(e => e is OperationCanceledException);
        //        Console.WriteLine("Sum was Canceled");
        //    }

        //}

        //private static Int32 Sum(CancellationToken ct, Int32 i)
        //{
        //    Int32 sum = 0;
        //    for (; i > 0; i--)
        //    {
        //        //在取消标志引用的CancellationTokenSource上如果调用 44                
        //        //Cancel，下面这一行就会抛出OperationCanceledException

        //        ct.ThrowIfCancellationRequested();

        //        checked { sum += i; }
        //    }

        //    return sum;
        //}
    }
}
