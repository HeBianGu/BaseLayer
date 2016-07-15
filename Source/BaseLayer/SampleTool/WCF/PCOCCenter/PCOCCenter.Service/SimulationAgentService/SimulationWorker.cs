using System;
using System.Diagnostics;
using OPT.PCOCCenter.Service.Interface;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OPT.PCOCCenter.Service
{
    class SimulationWorker
    {
        SimTaskInfo gSimTaskInfo = null;
        string strRunning = "正在模拟运算...";

        public void Run(ref SimTaskInfo simTaskInfo)
        {
            gSimTaskInfo = simTaskInfo;
            simTaskInfo.Flag = 2;
            simTaskInfo.MessageInfo = strRunning;
            
            try
            {
                // 调用Simulation，计算案例
                Process p = new Process();

                // 获取模拟器路径，不包含"eclipse.exe"
                string simulatorPath = simTaskInfo.SimulatorPath;
                int nPos = simulatorPath.LastIndexOf("eclipse.exe", StringComparison.CurrentCultureIgnoreCase);
                if ( nPos > 0)
                {
                    simulatorPath = simulatorPath.Substring(0, nPos-1);
                }

                // 获取数据文件路径，不包含“name.data”
                string dataPath = simTaskInfo.RemoteDataPath;
                nPos = dataPath.LastIndexOf(simTaskInfo.Name, StringComparison.CurrentCultureIgnoreCase);
                if (nPos > 0)
                {
                    dataPath = dataPath.Substring(0, nPos - 1);
                }

                // 获取数据文件名，不包含“.data”
                string dataName = simTaskInfo.Name;
                nPos = dataName.LastIndexOf(".data", StringComparison.CurrentCultureIgnoreCase);
                if (nPos > 0)
                {
                    dataName = dataName.Substring(0, nPos);
                }


                string args = "\"" + simulatorPath + "\" \"" + simTaskInfo.SimulatorLicensePath
                                + "\" \"" + dataPath + "\" \"" + dataName + "\"";

                Console.WriteLine("\n");
                Console.WriteLine(args);
                Console.WriteLine("\n");

                ProcessStartInfo psI = new ProcessStartInfo(@"ecl.bat", args);

                psI.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                p.StartInfo = psI;
                psI.UseShellExecute = false;
                psI.RedirectStandardInput = true;
                psI.RedirectStandardOutput = true;
                psI.RedirectStandardError = true;
                psI.CreateNoWindow = true;
                p.StartInfo = psI;                
                p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);// 接收输出数据
                p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);  // 接收错误数据
                p.EnableRaisingEvents = true;// 监视进程退出
                p.Exited += new EventHandler(CmdProcess_Exited);   // 注册进程结束事件 
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                simTaskInfo.WorkerProcess = p;

                p.WaitForExit();  //等待进程结束
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static bool bFindProblem = false;
        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Debug.WriteLineIf(!string.IsNullOrEmpty(e.Data), e.Data);

            if (!string.IsNullOrEmpty(e.Data))
            {
                // 输出结果
                string strOutput = e.Data;
                Console.WriteLine(strOutput);
                CalCompletePrecent(strOutput);

                // 过滤运算过程中的 Problem 信息
                bFindProblem = FilterProblemOutput(strOutput);
                if (bFindProblem)
                {
                    if (gSimTaskInfo.ProblemList == null)
                        gSimTaskInfo.ProblemList = new List<string>();
                    gSimTaskInfo.ProblemList.Add(strOutput);
                }
                
                int j = 0;
            }
        }
        
        void CalCompletePrecent(string strOutput)
        {
            if (gSimTaskInfo.SimulationDays != null && gSimTaskInfo.SimulationDays > 0)
            {
                // 查找当前计算时间点
                Match m = Regex.Match(strOutput, @"(?<=TIME=).*?(?=DAY)");
                 if (m.Success)
                 {
                     string time = m.Value;
                     float currentTime = float.Parse(time);
                 
                     // 计算完成百分比
                     gSimTaskInfo.CompletePercent = (currentTime / gSimTaskInfo.SimulationDays) * 100;
                 }
            }
        }

        bool FilterProblemOutput(string strOutput)
        {
            bool bFind = false;

            strOutput = strOutput.Trim();
            if (strOutput.IndexOf("@--PROBLEM", StringComparison.CurrentCultureIgnoreCase) == 0)
            {               
                bFind = true;
            }
            else if (bFindProblem)
            {
                if (strOutput.IndexOf("@", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    bFind = true;
                }
                else
                {
                    bFind = false;
                }
            }

            return bFind;
        }

        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Debug.WriteLineIf(!string.IsNullOrEmpty(e.Data), e.Data);

            if (!string.IsNullOrEmpty(e.Data))
            {
                // 错误信息
                string strError = "\n错误信息:\n" + e.Data;
                Console.WriteLine(strError);

                if (gSimTaskInfo.MessageInfo == strRunning)
                {
                    gSimTaskInfo.MessageInfo = "错误信息:";
                }

                gSimTaskInfo.Flag = -2;
                gSimTaskInfo.MessageInfo += e.Data;
            }
        }
        
        private void CmdProcess_Exited(object sender, EventArgs e)
        {
            // 执行结束后触发
            if (gSimTaskInfo.Flag == 2)
            {
                gSimTaskInfo.Flag = 3;
                gSimTaskInfo.CompletePercent = 100;
                gSimTaskInfo.MessageInfo = "模拟运算完成.";
            }
        }  
    }
}
