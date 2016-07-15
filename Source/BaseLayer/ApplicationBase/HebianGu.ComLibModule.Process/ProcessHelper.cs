using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ProcessEx
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

    }
}
