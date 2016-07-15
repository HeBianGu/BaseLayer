#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/4 13:19:42  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：IWorkInterface
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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.TaskEx
{
    /// <summary> 执行任务类型的接口 </summary>
    public interface IWorkInterface
    {
        /// <summary> 工作函数 </summary>
        int RunWork(CancellationToken pToken);

    }

    /// <summary> 任务管理器 </summary>
    public interface ITaskList : IWorkInterface
    {
        /// <summary> 当前任务 </summary>
        Task<int> TaskCore
        {
            get;
            set;
        }

        /// <summary> 上级任务 </summary>
        Task<int> ParentTaskCore
        {
            get;
            set;
        }
        /// <summary> 全局任务控制 </summary>
        CancellationTokenSource Cts
        {
            get;
            set;
        }
    }

    /// <summary> 包含：开始时间，结束事件，用时，运行状态，暂停，文件路径，文件名等状态 </summary>
    public class BaseTaskCore : ITaskList
    {
        bool isPause;
        /// <summary> 是否暂停状态 </summary>
        public bool IsPause
        {
            get { return isPause; }
            set { isPause = value; }
        }
        bool isRestart = false;
        /// <summary> 暂停继续后是否重新计算 </summary>
        public bool IsRestart
        {
            get { return isRestart; }
            set { isRestart = value; }
        }


        /// <summary> 抽象方法，提供子类扩展 </summary>
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
                //  设置暂停状态
                if (!isPause)
                {
                    isPause = TaskControlModel.Instance.IsPause;
                }

            }

            startTime = DateTime.Now;

            isPause = TaskControlModel.Instance.IsPause;

            RunCaseTaskWork(pToken);


            //  如果是中间停止则重新计算
            while (TaskControlModel.Instance.IsPause)
            {
                Thread.Sleep(1000);
                //  设置暂停状态
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

        /// <summary> 记录用时 </summary>
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
                    return "暂停";
                }
                switch (task.Status)
                {
                    case TaskStatus.Created:
                        return "准备";
                    case TaskStatus.WaitingForActivation:
                        return "等待";
                    case TaskStatus.WaitingToRun:
                        return "尚未开始执行";
                    case TaskStatus.Running:
                        return "运行";
                    case TaskStatus.WaitingForChildrenToComplete:
                        return "等待子任务";
                    case TaskStatus.RanToCompletion:
                        return "完成";
                    case TaskStatus.Canceled:
                        return "取消";
                    case TaskStatus.Faulted:
                        return "异常";
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