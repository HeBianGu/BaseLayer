#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/4 9:19:10  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：Task
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

namespace HebianGu.ComLibModule.TaskEx
{
    class TaskEx
    {

        /// <summary> 创建任务 </summary>
        public void CreateTask()
        {
            //使用TaskFactory创建一个任务
            TaskFactory tf = new TaskFactory();
            Task t1 = tf.StartNew(NewTask);
            //使用Task类de Factory创建一个任务
            Task t2 = Task.Factory.StartNew(NewTask);
            ///////////////////////////////////////
            Task t3 = new Task(NewTask);
            t3.Start();
            Task t4 = new Task(NewTask, TaskCreationOptions.PreferFairness);
            t4.Start();
            Thread.Sleep(1000);//因为任务是后台线程，所以我们这里阻塞主线程一秒钟来等待任务全部执行完成
        }

        void NewTask()
        {
            Console.WriteLine("开始一个任务");
            Console.WriteLine("Task id:{0}", Task.CurrentId);
            Console.WriteLine("任务执行完成");
        }


        /// <summary> 创建连续的任务 </summary>
         void CreateContinueTask()
        {
            Task t1 = new Task(FirstTask);
            Task t2 = t1.ContinueWith(SecondTask);
            t1.Start();
            Thread.Sleep(7000);
        }
        void FirstTask()
        {
            Console.WriteLine("第一个任务开始：TaskID:{0}", Task.CurrentId);
            Thread.Sleep(3000);
        }
         void SecondTask(Task task)
        {
            Console.WriteLine("任务{0}完成", task.Id);
            Console.WriteLine("第二个任务开始：TaskID:{0}", Task.CurrentId);
            Console.WriteLine("清理工作......");
            Thread.Sleep(3000);
        }

    }


}