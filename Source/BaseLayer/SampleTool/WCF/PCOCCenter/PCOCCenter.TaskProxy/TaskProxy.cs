using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OPT.PCOCCenter.Service.Interface;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using OPT.PCOCCenter.Service;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;
using System.ServiceModel.Channels;
using System.IO;

namespace OPT.PCOCCenter.TaskProxy
{
    /// <summary>
    /// 任务信息
    /// </summary>
    public class TaskInfo
    {
        public string ID;                    // 任务ID（自动分配）
        public int Flag;                     // 状态标记， -1--被删除，0--初始值， 1--排队新任务，2--正在运行，3--计算完成，其他表示失败
        public string Name;                  // 任务名称（name.data）
        public string Path;                  // 任务本机路径（D:\\path\\name.data）
        public string ResultPath;            // 结果文件本机路径（不包含具体结果文件名，只有路径）
        public string FuncID;                // 任务功能ID
        public string TaskType;              // 任务类型(功能中存在多类型时，用来识别)
        public string OwnerIP;               // 任务发布者IP
        public string WorkerIP;              // 任务工作主机IP地址
        public string SimulatorPath;         // 任务工作主机模拟器路径
        public string SimulatorLicensePath;  // 任务工作主机模拟器许可路径
        public int SimulationDays;           // 任务模拟天数（用于计算进度百分比）
        public float CompletePercent;        // 运算完成进度
        public string RemoteDataPath;        // 远程数据文件路径
        public string MessageInfo;           // 提示信息
        public List<string> ProblemList;     // 问题列表（用于记录不中断运行过程中的问题信息，客户端输出到日志列表中）

        public TaskInfo()
        {
            ID = (Guid.NewGuid()).ToString();
            OwnerIP = GetLocalIP();
            Flag = 0;
        }

        public void fromSimTaskInfo(SimTaskInfo simTaskInfo)
        {
            ID = simTaskInfo.ID;
            Flag = simTaskInfo.Flag;
            Name = simTaskInfo.Name;
            Path = simTaskInfo.OPath;
            ResultPath = simTaskInfo.ResultPath;
            FuncID = simTaskInfo.FuncID;
            TaskType = simTaskInfo.TaskType;
            OwnerIP = simTaskInfo.OwnerIP;
            SimulatorPath = simTaskInfo.SimulatorPath;
            SimulatorLicensePath = simTaskInfo.SimulatorLicensePath;
            CompletePercent = simTaskInfo.CompletePercent;
            RemoteDataPath = simTaskInfo.RemoteDataPath;
            MessageInfo = simTaskInfo.MessageInfo;
            if (simTaskInfo.ProblemList != null)
            {
                ProblemList = new List<string>();
                ProblemList.AddRange(simTaskInfo.ProblemList);
            }
        }

        //获取本机IP
        public string GetLocalIP()
        {
            IPAddress[] arrIPAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in arrIPAddresses)
            {
                if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }
    }

    /// <summary>
    /// 任务代理（客户端运算接口）
    /// </summary>
    public class TaskProxy
    {
        static string localHostIP = "127.0.0.1";

        /// <summary>
        /// 任务事件
        /// </summary>
        public static TaskEvent taskEvent = new TaskEvent();
        public static System.Timers.Timer tTaskMonitor;

        /// <summary>
        /// 发布任务，进行运算
        /// </summary>
        /// <param name="listTaskPaths">任务路径列表</param>
        /// <returns>返回任务模拟器主机</returns>
        public static int PublishTasks(ref List<TaskInfo> listTaskInfos)
        {
            int nStatus = 0;


            if (tTaskMonitor == null)
            {
                //实例化Timer类，设置间隔时间为1*60 000毫秒(1分钟)；   
                tTaskMonitor = new System.Timers.Timer(60000);
                //到达时间的时候执行事件；   
                tTaskMonitor.Elapsed += new System.Timers.ElapsedEventHandler(handlerTaskMonitor);
                //设置是执行一次（false）还是一直执行(true)；
                tTaskMonitor.AutoReset = true;
                //是否执行System.Timers.Timer.Elapsed事件；   
                tTaskMonitor.Enabled = true;
            }

            // 发布任务设置
            PublishTaskForm publishTaskForm = new PublishTaskForm(ref listTaskInfos);
            DialogResult dr = publishTaskForm.ShowDialog();

            if (dr == DialogResult.OK)
            {
                // 根据分配的任务主机，执行任务流程
                foreach (TaskInfo taskInfo in listTaskInfos)
                {
                    if (Client.SimulationAgent.RequestTask(taskInfo.WorkerIP, taskInfo.OwnerIP) == "Success")
                    {
                        int uploadStatus = 0;
                        string strRunTaskPath = string.Empty;

                        if (taskInfo.WorkerIP == localHostIP)
                        {
                            // 本地主机(127.0.0.1)直接在任务文件原路径运算
                            strRunTaskPath = taskInfo.Path;
                        }
                        else
                        {
                            string remoteUploadPath = @"uploads\" + taskInfo.ID;

                            // 网络主机，需要先上传数据文件，返回服务器端文件路径，然后通知服务器计算
                            strRunTaskPath = Client.FileTransfer.UploadFile(Client.SimulationAgent.RemoteFileTransferAddress, taskInfo.Path, remoteUploadPath);

                            // 打开本地任务“name.data”文件，查找include关键字，循环上传所有任务附加文件到服务器
                            uploadStatus = UploadAllTaskIncludeFiles(taskInfo.Path, remoteUploadPath);
                        }

                        if (uploadStatus == 0)
                        {
                            TaskManager.AddTask(taskInfo); // 加入客户端任务管理器
                            string startRet = Client.SimulationAgent.StartTask(taskInfo.ID, taskInfo.Path, taskInfo.Name, taskInfo.FuncID, taskInfo.TaskType,
                                                taskInfo.OwnerIP, taskInfo.SimulatorPath, taskInfo.SimulatorLicensePath, taskInfo.SimulationDays, strRunTaskPath);
                        }
                        else
                        {
                            // data包含文件上传错误
                            taskInfo.MessageInfo = "error: data包含文件上传错误";
                            nStatus = -1;
                        }
                    }
                }
            }
            else
            {
                // 用户取消
                nStatus = -1;
            }

            return nStatus;
        }

        // 定时获取任务信息
        public static void handlerTaskMonitor(object source, System.Timers.ElapsedEventArgs e)
        {
            TaskMonitor();
        }

        /// <summary>
        /// 任务信息获取
        /// </summary>
        /// <returns></returns>
        public static int TaskMonitor()
        {
            int result = 0;
            List<TaskInfo> taskInfoList = TaskManager.GetTaskInfoList();

            if (taskInfoList.Count <= 0) return result;

            for (int i = 0; i < taskInfoList.Count; i++)
            {
                if (i < 0 || i > taskInfoList.Count) continue;

                TaskInfo taskInfo = taskInfoList[i];

                if (taskInfo.Flag == 1)
                    GetTaskInfo(ref taskInfo);

                if (taskEvent != null)
                {
                    if (taskInfo.Flag == 2) // 表示计算中
                    {
                        taskEvent.doEvent(TaskEvent.TaskRunning, taskInfo);
                    }
                    else if (taskInfo.Flag == 3) // 表示计算完成
                    {
                        // 获取数据文件路径，不包含“name.data”
                        string dataPath = taskInfo.Path;
                        int nPos = dataPath.LastIndexOf(taskInfo.Name, StringComparison.CurrentCultureIgnoreCase);
                        if (nPos > 0)
                        {
                            dataPath = dataPath.Substring(0, nPos - 1);
                        }

                        // 如果是远程计算，下载结果文件
                        if (taskInfo.WorkerIP == localHostIP)
                        {
                            // 本地主机(127.0.0.1)直接在任务文件原路径运算
                            taskInfo.ResultPath = dataPath;
                        }
                        else
                        {
                            // 下载远程任务路径下所有结果文件到本地任务目录
                            string remoteTaskPath = @"uploads\" + taskInfo.ID;

                            // 从远程任务路径下载所有任务结果文件到本机
                            result = DownloadAllTaskResultFiles(dataPath, remoteTaskPath);
                            taskInfo.ResultPath = dataPath;
                        }

                        taskInfo.Flag = 0;
                        taskEvent.doEvent(TaskEvent.TaskCompeleted, taskInfo);
                    }

                    if (taskInfo.Flag < -1)
                    {
                        taskEvent.doEvent(TaskEvent.TaskFailed, taskInfo);
                    }
                }
            }

            return result;
        }

        // 从远程任务路径下载所有任务结果文件到本机
        private static int DownloadAllTaskResultFiles(string localTaskFilePath, string remoteTaskPath)
        {
            int nStatus = 0;

            for (int i = 0; i < 5; i++)
            {
                Client.FileTransfer.DownloadFile(Client.SimulationAgent.RemoteFileTransferAddress, localTaskFilePath, remoteTaskPath, string.Format("DC_E100.X000{0}", i));
            }

            return nStatus;
        }

        // 打开本地任务“name.data”文件，查找include关键字，循环上传所有任务附加文件到服务器
        private static int UploadAllTaskIncludeFiles(string taskFilePath, string uploadPath)
        {
            int nStatus = 0;

            try
            {
                FileStream fs = new FileStream(taskFilePath, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fs);   // 使用StreamReader类来读取文件 
                streamReader.BaseStream.Seek(0, SeekOrigin.Begin);  // 从数据流中读取每一行，直到文件的最后一行
                string strLine = streamReader.ReadLine();
                while (strLine != null)
                {
                    strLine = strLine.Trim();
                    if (strLine.Length > 2 && strLine.Substring(0, 2) != "--")
                    {
                        int nPos = strLine.IndexOf("include", StringComparison.CurrentCultureIgnoreCase);
                        if (nPos >= 0)
                        {
                            // 发现include
                            string includeFileFullPath = GetIncludeFile(ref streamReader, strLine.Substring(nPos + 7, strLine.Length - nPos - 7), taskFilePath);
                            if (string.IsNullOrEmpty(includeFileFullPath))
                            {
                                nStatus = -1;
                                break;
                            }
                            string remotePath = Client.FileTransfer.UploadFile(Client.SimulationAgent.RemoteFileTransferAddress, includeFileFullPath, uploadPath);
                        }

                    }

                    strLine = streamReader.ReadLine();
                }
                //关闭此StreamReader对象  
                streamReader.Close();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            return nStatus;
        }

        private static string GetIncludeFile(ref StreamReader streamReader, string strLine, string taskFilePath)
        {
            int nPos = 0;
            string strIncludePath = string.Empty;
            while (true)
            {
                nPos = strLine.IndexOf("/");
                if (nPos > 0)
                {
                    strIncludePath = strLine.Substring(0, nPos);
                    break;
                }
                strLine = streamReader.ReadLine();
            }

            strIncludePath = strIncludePath.Trim();
            strIncludePath = strIncludePath.Trim('\'');
            strIncludePath = strIncludePath.Trim();

            taskFilePath = taskFilePath.Replace("/", "\\");
            nPos = taskFilePath.LastIndexOf("\\");
            if (nPos > 0)
            {
                taskFilePath = taskFilePath.Substring(0, nPos + 1);
                strIncludePath = taskFilePath + strIncludePath;
            }
            else
            {
                // taskFilePath文件路径错误
                string err = "taskFilePath文件路径错误";
            }

            return strIncludePath;
        }

        public static List<TaskInfo> GetTaskInfoList(string OwnerIP, string FuncID)
        {
            return TaskManager.GetTaskInfoList();
        }

        public static int GetTaskInfo(ref TaskInfo taskInfo)
        {
            int nStatus = 0;

            if (Client.SimulationAgent.RequestTask(taskInfo.WorkerIP, taskInfo.OwnerIP) == "Success")
            {
                SimTaskInfo simTaskInfo = Client.SimulationAgent.GetSimTaskInfo(taskInfo.ID);
                taskInfo.fromSimTaskInfo(simTaskInfo);
            }
            else
            {
                // 发生错误
                nStatus = -1;
            }

            return nStatus;
        }

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="taskInfo"></param>
        /// <returns></returns>
        public static string RemoveTask(TaskInfo taskInfo)
        {
            string result = string.Empty;

            if (Client.SimulationAgent.RequestTask(taskInfo.WorkerIP, taskInfo.OwnerIP) == "Success")
            {
                TaskManager.RemoveTask(taskInfo.ID); // 从客户端任务管理器删除任务
                result = Client.SimulationAgent.RemoveTask(taskInfo.ID);
            }
            else
            {
                // 发生错误
                result = "发生错误";
            }

            return result;
        }
    }
}
