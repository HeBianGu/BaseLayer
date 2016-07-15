#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/4 13:19:42  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����IWorkInterface
 *
 * ˵����
 * 
 * 
 * �޸��ߣ�           ʱ�䣺               
 * �޸�˵����
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.TaskEx
{
    /// <summary> ִ���������͵Ľӿ� </summary>
    public interface IWorkInterface
    {
        /// <summary> �������� </summary>
        int RunWork(CancellationToken pToken);

    }

    /// <summary> ��������� </summary>
    public interface ITaskList : IWorkInterface
    {
        /// <summary> ��ǰ���� </summary>
        Task<int> TaskCore
        {
            get;
            set;
        }

        /// <summary> �ϼ����� </summary>
        Task<int> ParentTaskCore
        {
            get;
            set;
        }
        /// <summary> ȫ��������� </summary>
        CancellationTokenSource Cts
        {
            get;
            set;
        }
    }

    /// <summary> ��������ʼʱ�䣬�����¼�����ʱ������״̬����ͣ���ļ�·�����ļ�����״̬ </summary>
    public class BaseTaskCore : ITaskList
    {
        bool isPause;
        /// <summary> �Ƿ���ͣ״̬ </summary>
        public bool IsPause
        {
            get { return isPause; }
            set { isPause = value; }
        }
        bool isRestart = false;
        /// <summary> ��ͣ�������Ƿ����¼��� </summary>
        public bool IsRestart
        {
            get { return isRestart; }
            set { isRestart = value; }
        }


        /// <summary> ���󷽷����ṩ������չ </summary>
        public virtual int RunCaseTaskWork(System.Threading.CancellationToken pToken)
        {
            //Process p = new Process();

            //p.RunExe("DemoConsoleExe.exe", "");

            return 0;
        }

        public int RunWork(System.Threading.CancellationToken pToken)
        {
            while (TaskControlModel.Instance.IsPause)
            {
                //  ������ͣ״̬
                if (!isPause)
                {
                    isPause = TaskControlModel.Instance.IsPause;
                }

            }

            startTime = DateTime.Now;

            isPause = TaskControlModel.Instance.IsPause;

            RunCaseTaskWork(pToken);


            //  ������м�ֹͣ�����¼���
            while (TaskControlModel.Instance.IsPause)
            {
                Thread.Sleep(1000);
                //  ������ͣ״̬
                if (!IsPause)
                {
                    isPause = TaskControlModel.Instance.IsPause;

                }
            }

            IsPause = TaskControlModel.Instance.IsPause;

            if (isRestart)
            {
                isRestart = false;
                RunWork(pToken);
            }

            endTime = DateTime.Now;

            return 0;

        }

        DateTime startTime;

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        DateTime endTime;

        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        /// <summary> ��¼��ʱ </summary>
        public string Time
        {
            get
            {
                if (endTime == default(DateTime))
                {
                    if (startTime == default(DateTime))
                    {

                        string r = Regex.Replace(new TimeSpan().ToString(), @"\.\d+$", string.Empty);
                        TimeSpan ts = TimeSpan.Parse(r);
                        return ts.ToString();
                    }
                    else
                    {
                        string r = Regex.Replace((DateTime.Now - startTime).ToString(), @"\.\d+$", string.Empty);
                        TimeSpan ts = TimeSpan.Parse(r);
                        return ts.ToString();
                    }
                }
                else
                {
                    string r = Regex.Replace((endTime - startTime).ToString(), @"\.\d+$", string.Empty);
                    TimeSpan ts = TimeSpan.Parse(r);
                    return ts.ToString();
                }
            }
        }

        Task<int> task;

        public Task<int> TaskCore
        {
            get
            {
                return task;
            }
            set
            {
                task = value;
            }
        }

        Task<int> pTask;

        public Task<int> ParentTaskCore
        {
            get
            {
                return pTask;
            }
            set
            {
                pTask = value;
            }
        }

        public string Id
        {
            get
            {
                return task.Id.ToString();
            }
        }

        public string Pid
        {
            get
            {
                return pTask == null ? string.Empty : pTask.Id.ToString();
            }
        }


        public bool IsCanceled
        {
            get
            {
                return task.IsCanceled;
            }
        }
        public bool IsCompleted
        {
            get
            {
                return task.IsCompleted;
            }
        }
        public bool IsFaulted
        {
            get
            {
                return task.IsFaulted;
            }
        }

        public string Status
        {
            get
            {
                if (isPause)
                {
                    return "��ͣ";
                }
                switch (task.Status)
                {
                    case TaskStatus.Created:
                        return "׼��";
                    case TaskStatus.WaitingForActivation:
                        return "�ȴ�";
                    case TaskStatus.WaitingToRun:
                        return "��δ��ʼִ��";
                    case TaskStatus.Running:
                        return "����";
                    case TaskStatus.WaitingForChildrenToComplete:
                        return "�ȴ�������";
                    case TaskStatus.RanToCompletion:
                        return "���";
                    case TaskStatus.Canceled:
                        return "ȡ��";
                    case TaskStatus.Faulted:
                        return "�쳣";
                    default:
                        return "";
                }
            }
        }

        CancellationTokenSource _cts;

        public CancellationTokenSource Cts
        {
            get
            {
                return _cts;
            }
            set
            {
                _cts = value;
            }
        }

    }



}