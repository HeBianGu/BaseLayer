#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/4 10:49:13  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：TaskManager
 *
 * 说明：开启一个线程监听任务队列 监听IWorkInterface接口RunWork()方法
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

namespace HebianGu.ComLibModule.ThreadEx.TaskQueue
{
    /// <summary> 开启一个线程监听任务队列 监听IWorkInterface接口RunWork()方法 </summary>
    public class TaskManager<T> where T : IWorkInterface
    {

        /// <summary> 任务队列 </summary>
        private static Queue<T> m_List;

        /// <summary> 线程互斥 </summary>
        private static object m_obj = new object();

        /// <summary> 初始化队列 </summary>
        public TaskManager()
        {
            if (m_List == null)
                m_List = new Queue<T>();
        }

        /// <summary> 启动实时监听依次执行 </summary>
        public void ThreadWork()
        {
            while (true)
            {
                //  获取任务
                T work = Pop();

                //  执行任务
               if(!work.RunWork())
               {
                   //  写错误日志
               }

                Thread.Sleep(1);
            }
        }

        /// <summary> 从任务队列中取出任务 </summary>
        public T Pop()
        {

            Monitor.Enter(m_obj);

            T ac = default(T);
            try
            {
                //当队列有数据，出队.否则等待
                if (m_List.Count > 0)
                {
                    ac = m_List.Dequeue();
                }
                else
                {
                    Monitor.Wait(m_obj);
                    ac = m_List.Dequeue();
                }
            }
            finally
            {
                Monitor.Exit(m_obj);
            }
            return ac;
        }

        /// <summary> 把任务加入任务队列 </summary>
        public void Push(T work)
        {
            //  上锁
            Monitor.Enter(m_obj);

            //  把任务加入队列中
            m_List.Enqueue(work);

            //  通知等待队列中的线程锁定对象状态的更改。
            Monitor.Pulse(m_obj);

            //  释放锁
            Monitor.Exit(m_obj);
        }

        /// <summary> 开启线程异步执行监测 </summary>
        public void Start()
        {
            Thread th = new Thread(new ThreadStart(this.ThreadWork));
            th.Start();
        }
    }
}