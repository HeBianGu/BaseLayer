using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPT.PCOCCenter.Service.Interface;
using System.ComponentModel;
using System.Threading;

namespace OPT.PCOCCenter.Service
{
    /// <summary>
    /// 模拟任务管理器
    /// </summary>
    public class SimulationTaskManager
    {
        List<SimTaskInfo> listSimTaskInfos = new List<SimTaskInfo>();
        BackgroundWorker backgroundSimulationWorker = null;

        /// <summary>
        /// 添加任务, 并触发任务运行
        /// </summary>
        /// <param name="simTaskInfo"></param>
        public void AddTask(SimTaskInfo simTaskInfo)
        {
            simTaskInfo.Flag = 1;
            simTaskInfo.MessageInfo = "排队中...";
            lock (listSimTaskInfos)
            {
                listSimTaskInfos.Add(simTaskInfo);
            }
#if false //这段代码在运行完一次计算后，再次点击运算会出现关于进程同步的异常
            if (Monitor.TryEnter(listSimTaskInfos, 1000))
            {
                listSimTaskInfos.Add(simTaskInfo);
            }
            else
            {
                Monitor.Exit(listSimTaskInfos);
            }
#endif
            // 触发任务执行线程
            LoadSimulationWorker();
        }

        /// <summary>   
        /// 运行DOS命令   
        /// DOS关闭进程命令(ntsd -c q -p PID )PID为进程的ID   
        /// </summary>   
        /// <param name="command"></param>   
        /// <returns></returns>   
        public static string RunCmd(string command)
        {
            //實例一個Process類，啟動一個獨立進程   
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //Process類有一個StartInfo屬性，這個是ProcessStartInfo類，包括了一些屬性和方法，下面我們用到了他的幾個屬性：   

            p.StartInfo.FileName = "cmd.exe";           //設定程序名   
            p.StartInfo.Arguments = "/c " + command;    //設定程式執行參數   
            p.StartInfo.UseShellExecute = false;        //關閉Shell的使用   
            p.StartInfo.RedirectStandardInput = true;   //重定向標準輸入   
            p.StartInfo.RedirectStandardOutput = true;  //重定向標準輸出   
            p.StartInfo.RedirectStandardError = true;   //重定向錯誤輸出   
            p.StartInfo.CreateNoWindow = true;          //設置不顯示窗口   

            p.Start();   //啟動  

            return p.StandardOutput.ReadToEnd();        //從輸出流取得命令執行結果   
        }

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="simTaskID"></param>
        public SimTaskInfo RemoveTask(SimTaskInfo request)
        {
            foreach (SimTaskInfo simTaskInfo in listSimTaskInfos)
            {
                if (simTaskInfo.ID == request.ID)
                {
                    if (simTaskInfo.WorkerProcess != null && simTaskInfo.Flag != -1)
                    {
                        try
                        {
                            if (simTaskInfo.WorkerProcess.HasExited == false)
                            {
                                RunCmd("taskkill /im " + "eclipse.exe" + " /f ");
                            }
                            if (simTaskInfo.WorkerProcess.HasExited == true)
                            {
                                simTaskInfo.WorkerProcess.Dispose();
                                simTaskInfo.WorkerProcess = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            simTaskInfo.MessageInfo = ex.Message;
                        }
                    }
                    simTaskInfo.Flag = -1;
                    simTaskInfo.MessageInfo = "任务已经移除";
                    return simTaskInfo;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="simTaskID"></param>
        /// <returns></returns>
        public SimTaskInfo GetSimTaskInfo(SimTaskInfo request)
        {
            foreach (SimTaskInfo simTaskInfo in listSimTaskInfos)
            {
                if (simTaskInfo.ID == request.ID && simTaskInfo.Flag != -1)
                {
                    return simTaskInfo;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="simTaskID"></param>
        /// <returns></returns>
        public List<SimTaskInfo> GetSimTaskList(string OwnerIP, string FuncID)
        {
            if (string.IsNullOrEmpty(OwnerIP) && string.IsNullOrEmpty(FuncID))
            {
                return listSimTaskInfos;
            }

            return null;
        }

        // 执行任务执行线程
        public void LoadSimulationWorker()
        {
            if (backgroundSimulationWorker == null)
            {
                backgroundSimulationWorker = new System.ComponentModel.BackgroundWorker();

                backgroundSimulationWorker.WorkerReportsProgress = true;
                backgroundSimulationWorker.WorkerSupportsCancellation = true;
                backgroundSimulationWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
                backgroundSimulationWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
                backgroundSimulationWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            }

            if (backgroundSimulationWorker.IsBusy == false)
                backgroundSimulationWorker.RunWorkerAsync(listSimTaskInfos);
        }


        //工作完成后执行的事件  
        void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            backgroundSimulationWorker.Dispose();
            backgroundSimulationWorker = null;
        }

        //工作中执行进度更新  ，C#进度条实现之异步实例
        void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int perentage = e.ProgressPercentage;
        }

        // 开始执行任务
        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(200);

                int completed = 0;
                for (int i = 0; i < listSimTaskInfos.Count; i++)
                {
                    if (i < 0 || i > listSimTaskInfos.Count) continue;

                    SimTaskInfo simTaskInfo = listSimTaskInfos[i];

                    // 计算在排队任务
                    if (simTaskInfo.Flag == 1)
                    {
                        SimulationWorker simulationWorker = new SimulationWorker();
                        simulationWorker.Run(ref simTaskInfo);
                    }
                    else if (simTaskInfo.Flag == -1)
                    {
                        // 移除被标记为删除的案例
                        listSimTaskInfos.RemoveAt(i);
                        i--;
                    }

                    Thread.Sleep(10);
                    completed++;
                }

                // 所有任务完成
                if (completed == listSimTaskInfos.Count)
                    break;
            }
        }
    }
}
