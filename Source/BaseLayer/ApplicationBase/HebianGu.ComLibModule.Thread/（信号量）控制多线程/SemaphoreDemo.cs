﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ThreadEx._信号量_控制多线程
{
    class SemaphoreDemo
    {
        //Semaphore（初始授予0个请求数，设置最大可授予5个请求数）
        static Semaphore semaphore = new Semaphore(0, 5);

        static void Main(string[] args)
        {
            for (int i = 1; i <= 5; i++)
            {
                Thread thread = new Thread(work);
                thread.Start(i);
            }

            Thread.Sleep(1000);
            Console.WriteLine("Main方法结束");

            //授予5个请求
            semaphore.Release(5);
            Console.ReadLine();
        }

        static void work(object obj)
        {
            semaphore.WaitOne();
            Console.WriteLine("print: {0}", obj);
            semaphore.Release();
        }
    }
}
