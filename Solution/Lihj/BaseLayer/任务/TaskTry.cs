#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/4 17:13:07  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：TaskTry
 *
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 任务
{
    class TaskTry
    {
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
        //              taskFactory.StartNew(() => Sum(cts.Token, 10000)), 
        //              taskFactory.StartNew(() => Sum(cts.Token, 20000)), 
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


        //    try
        //    {
        //        parent.Wait(); //显示结果

        //    }
        //    catch (AggregateException)
        //    {
        //    }
        //}


        //private static Int32 Sum(CancellationToken ct, Int32 n)
        //{
        //    Int32 sum = 0;
        //    for (; n > 0; n--)
        //    {
        //        ct.ThrowIfCancellationRequested();
        //        checked { sum += n; }
        //    }
        //    return sum;
        //}
    }
}