#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/4 13:18:26  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����TaskManager
 *
 * ˵��������һ����������������ִ�и�������,Ŀǰֻ֧��һ����˳������
 *       �̳�IWorkInterface�ӿ�  ʹ��ContinueWith�������  ����Start��ʼ��������
 *       
 *       BaseFactory����ʵ�ֵ��� ����ȥ��
 * 
 * �޸��ߣ�           ʱ�䣺               
 * �޸�˵����
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
    /// <summary> ����һ����������������ִ�и������� </summary>
    public class TaskManager<T>: BaseFactory<TaskManager<T>> where T : IWorkInterface
    {

        #region - Start �ڲ���Ա-
        //  ȡ������
        CancellationTokenSource cts = null;

        LinkedList<Task<int>> taskList = null;

        /// <summary> �����˫������ </summary>
        public LinkedList<Task<int>> TaskList
        {
            get { return taskList; }
            set { taskList = value; }
        }

        /// <summary> �̻߳��� </summary>
        private static object m_obj = new object();

        Task<int> runTask = null;
        /// <summary> ���ڼ�¼��ǰ����ִ�е����� </summary>
        public Task<int> RunTask
        {
            get { return runTask; }
            set { runTask = value; }
        }

        /// <summary> ���л�����ʱ�ⲿ���� </summary>
        Action<Task<int>, Task<int>> _act;

        /// <summary> ���л�����ʱ�ⲿ���� </summary>
        Func<Task<int>, int> _allOver;

        /// <summary> �������� </summary>
        public int TotalCount
        {
            get { return this.taskList.Count; }
        }

        /// <summary> ��ɵ����� </summary>
        public List<Task<int>> CompleteTask
        {
            get { return this.taskList.ToList().FindAll(l => l.IsCompleted); }
        }

        /// <summary> ȡ��������</summary>
        public List<Task<int>> CancelTask
        {
            get { return this.taskList.ToList().FindAll(l => l.IsCanceled); }
        }

        /// <summary> �쳣������ </summary>
        public List<Task<int>> FaultTask
        {
            get { return this.taskList.ToList().FindAll(l => l.IsFaulted); }
        }

        /// <summary> �Ƿ�ȫ�������� </summary>
        public bool IsComplete
        {
            get { return (CompleteTask.Count + CancelTask.Count + FaultTask.Count) == TotalCount; }

        }

        /// <summary> act:��һ���������һ����ʼҪ����ķ��� </summary>
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

        /// <summary> act:��һ���������һ����ʼҪ����ķ��� allOver�������񶼴�����Ĵ���ķ���  </summary>
        public TaskManager(Action<Task<int>, Task<int>> act, Func<Task<int>, int> allOver)
        {
            _act = act;

            _allOver = allOver;

            if (taskList == null)
                taskList = new LinkedList<Task<int>>();

            if (cts == null)
                cts = new CancellationTokenSource();

        }



        /// <summary> ���н��� p1 = ��һ���� p2 = ���õĽӿ� r = ����Ľ��</summary>
        int ContinueWith(Task<int> pTask, T worker)
        {
            //  ����ȡ������
            if (cts.IsCancellationRequested)
            {
                return TaskResultID.CacelParam;
            }

            //  ����ִ������
            runTask = taskList.Find(pTask).Next.Value;

            //  �������񽻽�
            if (_act != null)
                _act(pTask, runTask);


            int result = worker.RunWork(cts.Token);

            return result;

        }

        int TaskStartTask(T worker)
        {
            return worker.RunWork(cts.Token);
        }

        #endregion - �ڲ���Ա End -E

        /// <summary> ��ĩβ�������� p1=��ǰ�����Ӧ�Ľӿ� </summary>
        public void ContinueLast(T worker)
        {

            if (taskList.Count == 0)
            {
                //  ��������
                Task<int> firstTask = new Task<int>(l => TaskStartTask(worker), cts.Token);
                runTask = firstTask;
                taskList.AddFirst(firstTask);
                return;
            }

            //  ��ȡ���ڵ�
            Task<int> lastTask = taskList.Last.Value;

            //  ���ñ���ȡ��
            lastTask.ContinueWith<int>(
                l =>
                {
                    cts.Cancel();
                    return TaskResultID.FaultParam;
                },
                TaskContinuationOptions.OnlyOnFaulted
                );

            //  ���ӵ�ǰ����
            Task<int> addTask = lastTask.ContinueWith<int>(
                l => ContinueWith(l, worker),
                cts.Token,
                TaskContinuationOptions.None,
                TaskScheduler.Default
                );

            taskList.AddLast(addTask);

        }

        /// <summary> ִ������ </summary>
        public void Start()
        {
            //  ��������¼�
            if (_allOver != null)
                taskList.Last.Value.ContinueWith<int>(_allOver, cts.Token);

            taskList.First.Value.Start();
        }

        /// <summary> �ȴ������������ p1 = ��λ�� ��ʱ����</summary>
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

        /// <summary> ֹͣ�������� </summary>
        public Task<int> Stop()
        {
            //  ����ȡ��
            cts.Cancel();

            return this.runTask;

        }

        /// <summary> �������� </summary>
        public void ClearTask()
        {
            taskList.Clear();
        }

    }
}