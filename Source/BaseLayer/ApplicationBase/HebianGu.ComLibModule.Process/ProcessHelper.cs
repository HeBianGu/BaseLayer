 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ProcessHelper
{
    /// <summary> 进程扩展 </summary>
    public static class ProcEx
    {
        /// <summary> 调用批处理执行 </summary>
        /// <param name="process"> 进程 </param>
        /// <param name="batPath"> 批处理名称 ecl.bat(默认放在Bin目录) </param>
        /// <param name="args"> 批处理要传递的参数 </param>
        public static void RunBat(this Process process, string batFileName, string args)
        {
            //string args = "\"" + eclPath + "\" \"" + LicPaht
            //                    + "\" \"" + dataPath + "\" \"" + dataName + "\"";

            //  批处理文件
            ProcessStartInfo psI = new ProcessStartInfo(batFileName, args);
            psI.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            process.StartInfo = psI;
            //psI.RedirectStandardInput = true;
            //psI.RedirectStandardOutput = true;
            //psI.RedirectStandardError = true;
            psI.UseShellExecute = true;
            psI.CreateNoWindow = false;
            process.StartInfo = psI;

            // 接收输出数据
            //p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            // 接收错误数据
            //p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);  
            //process.EnableRaisingEvents = true;// 监视进程退出
            process.Start();
            //p.BeginOutputReadLine();
            //p.BeginErrorReadLine();
            //  等待进程结束
            process.WaitForExit();
        }

        /// <summary> 调用批处理执行 </summary>
        /// <param name="process"> 进程 </param>
        /// <param name="batPath"> 批处理名称 ecl.bat(默认放在Bin目录) </param>
        /// <param name="args"> 批处理要传递的参数 </param>
        public static void RunExe(this Process process, string batFileName, string args)
        {
            //string args = "\"" + eclPath + "\" \"" + LicPaht
            //                    + "\" \"" + dataPath + "\" \"" + dataName + "\"";

            //  批处理文件
            ProcessStartInfo psI = new ProcessStartInfo(batFileName, args);
            psI.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            process.StartInfo = psI;
            //psI.RedirectStandardInput = true;
            //psI.RedirectStandardOutput = true;
            //psI.RedirectStandardError = true;
            psI.UseShellExecute = false;
            psI.CreateNoWindow = true;
            process.StartInfo = psI;
            // 接收输出数据
            //p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            // 接收错误数据
            //p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);  
            //process.EnableRaisingEvents = true;// 监视进程退出
            process.Start();
            //p.BeginOutputReadLine();
            //p.BeginErrorReadLine();
            //  等待进程结束
            process.WaitForExit();
        }


        /// <summary> 杀掉所有子进程 </summary>
        public static void KillProcessAndChildren(int pid)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);

            ManagementObjectCollection moc = searcher.Get();

            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }

            Process proc = Process.GetProcessById(pid);
            Console.WriteLine(pid);
            proc.Kill();

        }

        /// <summary> 删除所有进程包括子进程 64x </summary>
        public static void KillAll(this Process process)
        {
            KillProcessAndChildren(process.Id);
        }


        /// <summary> 只可运行一个进程</summary>
        public static void RunSingle()
        {
            Process current = Process.GetCurrentProcess();

            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    process.Kill();
                }
            }
        }

        /// <summary> 只可运行一个进程</summary>
        public static void RunSingle(Process current)
        {
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    process.Kill();
                }
            }
        }


        /// <summary> 设置进程单例 (删除原有更新)</summary>
        public static Process SetSingleRecover(this Process sender)
        {
            //Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(sender.ProcessName);
            foreach (Process process in processes)
            {
                //Ignore   the   current   process   
                if (process.Id != sender.Id)
                {
                    //  去掉重复进程
                    process.Kill();
                }
            }

            return sender;
        }

        /// <summary> 设置进程单例 (如果有返回当前)</summary>
        public static Process SetSingle(this Process sender)
        {
            Process[] processes = Process.GetProcessesByName(sender.ProcessName);

            foreach (Process process in processes)
            {
                //Ignore   the   current   process   
                if (process.Id != sender.Id)
                {
                    return process;
                }
            }

            return sender;
        }


    }

    static class ProcessExtend
    {
        private struct ProcessBasicInformation
        {
            public int ExitStatus;
            public int PebBaseAddress;
            public int AffinityMask;
            public int BasePriority;
            public uint UniqueProcessId;
            public uint InheritedFromUniqueProcessId;
        }


        [DllImport("ntdll.dll")]
        static extern int NtQueryInformationProcess(
           IntPtr hProcess,
           int processInformationClass,
           ref ProcessBasicInformation processBasicInformation,
           uint processInformationLength,
           out uint returnLength
        );


        public static void KillProcessTree(this Process parent)
        {
            var processes = Process.GetProcesses();

            foreach (var p in processes)
            {
                var pbi = new ProcessBasicInformation();
                try
                {
                    uint bytesWritten;
                    if (NtQueryInformationProcess(p.Handle, 0, ref pbi, (uint)Marshal.SizeOf(pbi), out bytesWritten) == 0)
                        if (pbi.InheritedFromUniqueProcessId == parent.Id)
                            using (var newParent = Process.GetProcessById((int)pbi.UniqueProcessId))
                                newParent.KillProcessTree();
                }
                catch { }
            }
            parent.Kill();
        }
    }

}
