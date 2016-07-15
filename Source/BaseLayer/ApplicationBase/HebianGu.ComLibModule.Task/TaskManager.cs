#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/4 13:18:26  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：TaskManager
 *
 * 说明：这是一个连续的任务，依次执行各个任务,目前只支持一级的顺序任务
 *       继承IWorkInterface接口  使用ContinueWith添加任务  调用Start开始任务链表
 *       
 *       BaseFactory用于实现单例 可以去掉
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HebianGu.ComLibModule.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.TaskEx
{
    /// <summary> 这是一个连续的任务，依次执行各个任务 </summary>
    public class TaskManager<T>: BaseFactory<TaskManager<T>> where T : IWorkInterface
    {

        #region - Start 内部成员-
        //  取消控制
        CancellationTokenSource cts = null;

        LinkedList<Task<int>> taskList = null;

        /// <summary> 任务的双向链表 </summary>
        public LinkedList<Task<int>> TaskList
        {
            get { return taskList; }
            set { taskList = value; }
        }

        /// <summary> 线程互斥 </summary>
        private static object m_obj = new object();

        Task<int> runTask = null;
        /// <summary> 用于记录当前正在执行的任务 </summary>
        public Task<int> RunTask
        {
            get { return runTask; }
            set { runTask = value; }
        }

        /// <summary> 当切换进程时外部调用 </summary>
        Action<Task<int>, Task<int>> _act;

        /// <summary> 当切换进程时外部调用 </summary>
        Func<Task<int>, int> _allOver;

        /// <summary> 任务总数 </summary>
        public int TotalCount
        {
            get { return this.taskList.Count; }
        }

        /// <summary> 完成的任务 </summary>
        public List<Task<int>> CompleteTask
        {
            get { return this.taskList.ToList().FindAll(l => l.IsCompleted); }
        }

        /// <summary> 取消的任务</summary>
        public List<Task<int>> CancelTask
        {
            get { return this.taskList.ToList().FindAll(l => l.IsCanceled); }
        }

        /// <summary> 异常的任务 </summary>
        public List<Task<int>> FaultTask
        {
            get { return this.taskList.ToList().FindAll(l => l.IsFaulted); }
        }

        /// <summary> 是否全部运行完 </summary>
        public bool IsComplete
        {
            get { return (CompleteTask.Count + CancelTask.Count + FaultTask.Count) == TotalCount; }

        }

        /// <summary> act:上一任务完成下一任务开始要处理的方法 </summary>
        public TaskManager(Action<Task<int>, Task<int>> act)
        {
            _act = act;

            if (taskList == null)
                taskList = new LinkedList<Task<int>>();

            if (cts == null)
                cts = new CancellationTokenSource();

        }

        public TaskManager()
        {

            if (taskList == null)
                taskList = new LinkedList<Task<int>>();

            if (cts == null)
                cts = new CancellationTokenSource();

        }

        /// <summary> act:上一任务完成下一任务开始要处理的方法 allOver所有任务都处理完的处理的方法  </summary>
        public TaskManager(Action<Task<int>, Task<int>> act, Func<Task<int>, int> allOver)
        {
            _act = act;

            _allOver = allOver;

            if (taskList == null)
                taskList = new LinkedList<Task<int>>();

            if (cts == null)
                cts = new CancellationTokenSource();

        }



        /// <summary> 运行结束 p1 = 上一任务 p2 = 调用的接口 r = 任务的结果</summary>
        int ContinueWith(Task<int> pTask, T worker)
        {
            //  设置取消参数
            if (cts.IsCancellationRequested)
            {
                return TaskResultID.CacelParam;
            }

            //  正在执行任务
            runTask = taskList.Find(pTask).Next.Value;

            //  触发任务交接
            if (_act != null)
                _act(pTask, runTask);


            int result = worker.RunWork(cts.Token);

            return result;

        }

        int TaskStartTask(T worker)
        {
            return worker.RunWork(cts.Token);
        }

        #endregion - 内部成员 End -E

        /// <summary> 在末尾创建任务 p1=当前任务对应的接口 </summary>
        public void ContinueLast(T worker)
        {

            if (taskList.Count == 0)
            {
                //  创建进程
                Task<int> firstTask = new Task<int>(l => TaskStartTask(worker), cts.Token);
                runTask = firstTask;
                taskList.AddFirst(firstTask);
                return;
            }

            //  获取最后节点
            Task<int> lastTask = taskList.Last.Value;

            //  设置报错取消
            lastTask.ContinueWith<int>(
                l =>
                {
                    cts.Cancel();
                    return TaskResultID.FaultParam;
                },
                TaskContinuationOptions.OnlyOnFaulted
                );

            //  增加当前任务
            Task<int> addTask = lastTask.ContinueWith<int>(
                l => ContinueWith(l, worker),
                cts.Token,
                TaskContinuationOptions.None,
                TaskScheduler.Default
                );

            taskList.AddLast(addTask);

        }

        /// <summary> 执行任务 </summary>
        public void Start()
        {
            //  触发完成事件
            if (_allOver != null)
                taskList.Last.Value.ContinueWith<int>(_allOver, cts.Token);

            taskList.First.Value.Start();
        }

        /// <summary> 等待所有任务结束 p1 = 单位秒 超时限制</summary>
        public void Wait(int millisecondsTimeout = 0)
        {
            if (millisecondsTimeout == 0)
            {
                Task.WaitAll(taskList.ToArray());
            }
            else
            {
                Task.WaitAll(taskList.ToArray(), millisecondsTimeout);
            }

        }

        /// <summary> 停止所有任务 </summary>
        public Task<int> Stop()
        {
            //  调用取消
            cts.Cancel();

            return this.runTask;

        }

        /// <summary> 清理任务 </summary>
        public void ClearTask()
        {
            taskList.Clear();
        }

    }
}