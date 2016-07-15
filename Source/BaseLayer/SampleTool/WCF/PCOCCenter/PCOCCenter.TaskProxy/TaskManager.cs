using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPT.PCOCCenter.Service.Interface;
using System.ComponentModel;
using System.Threading;
using System.ServiceModel;

namespace OPT.PCOCCenter.TaskProxy
{
    /// <summary>
    /// 任务管理，只管理本机发布任务
    /// </summary>
    public class TaskManager
    {
        static private List<TaskInfo> taskInfoList = new List<TaskInfo>();

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="taskInfoID"></param>
        /// <returns></returns>
        public static List<TaskInfo> GetTaskInfoList()
        {
            return taskInfoList;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="TaskInfo"></param>
        public static void AddTask(TaskInfo TaskInfo)
        {
            TaskInfo.Flag = 1; // 标识发布任务
            taskInfoList.Add(TaskInfo);
        }

        /// <summary>
        /// 删除指定任务
        /// </summary>
        /// <param name="taskID"></param>
        public static void RemoveTask(string taskID)
        {
            for (int i = 0; i < taskInfoList.Count; i++)
            {
                if (i < 0 || i > taskInfoList.Count) continue;

                TaskInfo TaskInfo = taskInfoList[i];

                if (TaskInfo.ID == taskID)
                {
                    taskInfoList.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
