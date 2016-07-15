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
    public class TaskListManager<T> : BaseFactory<TaskListManager<T>> where T : ITaskList
    {

        #region - Start �ڲ���Ա-

        //  ȡ������
        CancellationTokenSource cts = null;

        /// <summary> �Ƿ��ѱ�ȡ�� </summary>
        public bool IsCancel
        {
            get
            {
                return cts.IsCancellationRequested;
            }
        }


        LinkedList<T> taskList = null;

        /// <summary> �����˫������ </summary>
        public LinkedList<T> TaskList
        {
            get { return taskList; }
            set { taskList = value; }
        }

        /// <summary> �̻߳��� </summary>
        private static object m_obj = new object();

        T runTask;
        /// <summary> ���ڼ�¼��ǰ����ִ�е����� </summary>
        public T RunTask
        {
            get { return runTask; }
            set { runTask = value; }
        }

        /// <summary> ���л�����ʱ�ⲿ���� </summary>
        Action<Task<int>, T> _act;

        /// <summary> ���л�����ʱ�ⲿ���� </summary>
        Func<Task<int>, int> _allOver;

        /// <summary> �������� </summary>
        public int TotalCount
        {
            get { return this.taskList.Count; }
        }

        /// <summary> ��ɵ����� </summary>
        public List<T> CompleteTask
        {
            get { return this.taskList.ToList().FindAll(l => l.TaskCore.IsCompleted); }
        }

        /// <summary> ȡ��������</summary>
        public List<T> CancelTask
        {
            get { return this.taskList.ToList().FindAll(l => l.TaskCore.IsCanceled); }
        }

        /// <summary> �쳣������ </summary>
        public List<T> FaultTask
        {
            get { return this.taskList.ToList().FindAll(l => l.TaskCore.IsFaulted); }
        }

        /// <summary> �Ƿ�ȫ�������� </summary>
        public bool IsComplete
        {
            get { return (CompleteTask.Count + CancelTask.Count + FaultTask.Count) == TotalCount; }

        }

        /// <summary> act:��һ���������һ����ʼҪ����ķ��� </summary>
        public TaskListManager(Action<Task<int>, T> act)
        {
            _act = act;

            if (taskList == null)
                taskList = new LinkedList<T>();

            if (cts == null)
                cts = new CancellationTokenSource();

        }

        public TaskListManager()
        {

            if (taskList == null)
                taskList = new LinkedList<T>();

            if (cts == null)
                cts = new CancellationTokenSource();

        }

        /// <summary> act:��һ���������һ����ʼҪ����ķ��� allOver�������񶼴�����Ĵ���ķ���  </summary>
        public TaskListManager(Action<Task<int>, T> act, Func<Task<int>, int> allOver)
        {
            _act = act;

            _allOver = allOver;

            if (taskList == null)
                taskList = new LinkedList<T>();

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
            //runTask = taskList.First(l => l.TaskCore.Id == pTask.Id);
            runTask = worker;

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

        /// <summary> ��ĩβ�������� p1=��ǰ�����Ӧ�Ľӿ�  p2 = �Ƿ��Զ�����</summary>
        public void ContinueLast(T worker, bool autoRun=false)
        {
            //  ����������
            if (taskList.Count == 0)
            {
                //  ��������
                Task<int> firstTask = new Task<int>(l => TaskStartTask(worker), cts.Token);
                worker.TaskCore = firstTask;
                runTask = worker;
                taskList.AddFirst(worker);
                return;
            }

            //  ��ȡ���ڵ�
            T lastTask = taskList.Last.Value;

            //  ���ñ���ȡ��
            lastTask.TaskCore.ContinueWith<int>(
                l =>
                {
                    cts.Cancel();
                    return TaskResultID.FaultParam;
                },
                TaskContinuationOptions.OnlyOnFaulted
                );

            //  ���ӵ�ǰ����
            Task<int> addTask = lastTask.TaskCore.ContinueWith<int>
                (
                l => ContinueWith(l, worker),
                cts.Token,
                TaskContinuationOptions.None,
                TaskScheduler.Default
                );

            worker.TaskCore = addTask;

            worker.ParentTaskCore = lastTask.TaskCore;

            taskList.AddLast(worker);


            if (autoRun && this.IsCancel)
            {
                this.Start();
            }

        }

        /// <summary> ִ������ </summary>
        public void Start()
        {
            //  ��������¼�
            if (_allOver != null)
                taskList.Last.Value.TaskCore.ContinueWith<int>(_allOver, cts.Token);

            cts = new CancellationTokenSource();

            if (taskList.First.Value.TaskCore.Status == TaskStatus.Created)
            {
                taskList.First.Value.TaskCore.Start();
            }

            //var find = FindNextWaitRun(taskList.First.Value);

            //if (find != null)
            //{
            //    find.TaskCore.Start();
            //}


        }
        /// <summary> �������¿�ִ������ </summary>
        T FindNextWaitRun(T task)
        {
            var pTask = taskList.Find(task);

            if (pTask != null)
            {
                if (pTask.Value.TaskCore.Status == TaskStatus.WaitingForActivation || pTask.Value.TaskCore.Status == TaskStatus.Created)
                {
                    return pTask.Value;
                }
                else
                {
                    if (pTask.Next != null)
                    {
                        return FindNextWaitRun(pTask.Next.Value);
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
            else
            {
                return default(T);
            }

        }

        /// <summary> �ȴ������������ p1 = ��λ�� ��ʱ����</summary>
        public void Wait(int millisecondsTimeout = 0)
        {
            if (millisecondsTimeout == 0)
            {
                Task.WaitAll(taskList.Select(l => l.TaskCore).ToArray());
            }
            else
            {
                Task.WaitAll(taskList.Select(l => l.TaskCore).ToArray(), millisecondsTimeout);
            }

        }

        /// <summary> ֹͣ�������� </summary>
        public T Stop()
        {
            //  ����ȡ��
            cts.Cancel();

            return this.runTask;

        }

        /// <summary> ��ͣ </summary>
        public T Pause()
        {
            TaskControlModel.Instance.IsPause = true;
            return this.runTask;

        }

        /// <summary> ���� </summary>
        public void Continue()
        {
            cts = new CancellationTokenSource();

            TaskControlModel.Instance.IsPause = false;
        }

        /// <summary> �������� </summary>
        public void ClearTask()
        {
            taskList.Clear();

            cts = new CancellationTokenSource();
        }

    }

    /// <summary> ���Ʋ��� </summary>
    public class TaskControlModel : BaseFactory<TaskControlModel>
    {
        bool isPause = false;

        public bool IsPause
        {
            get { return isPause; }
            set { isPause = value; }
        }
    }
}