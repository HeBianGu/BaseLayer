#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/4 10:07:26  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：CmdExcute
 *
 * 说明： 执行DOS命令的扩展方法 
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HebianGu.ComLibModule.FormatEx;

namespace HebianGu.ComLibModule.CMD
{
    /// <summary> 执行DOS命令的扩展方法 </summary>
    public static class CmdAPI
    {
        /// <summary> 运行DOS命令  DOS关闭进程命令(ntsd -c q -p PID )PID为进程的ID   </summary>   
        public static string RunCmdOutPut(this string command, EventHandler endEvent = null)
        {
            //  啟動一個獨立進程   
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c " + command;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;

            if (endEvent != null)
            {
                p.EnableRaisingEvents = true;
                p.Exited += endEvent;
            }

            // Todo 2016-11-19 ：從輸出流取得命令執行結果 
            p.Start();

            //  從輸出流取得命令執行結果 
            return p.StandardOutput.ReadToEnd();

        }

        /// <summary> 运行DOS命令  DOS关闭进程命令(ntsd -c q -p PID )PID为进程的ID   </summary>   
        public static void RunCmd(string command, EventHandler endEvent = null)
        {
            //  啟動一個獨立進程   
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c " + command;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.RedirectStandardError = false;
            p.StartInfo.CreateNoWindow = false;

            if (endEvent != null)
            {
                p.EnableRaisingEvents = true;
                p.Exited += endEvent;
            }

            p.Start();

        }

        /// <summary> 关掉进程 P1 进程的PID </summary>
        [Obsolete("未测试")]
        public static string CloseProcessByPid(this string pid)
        {
            return CmdStr.CloseProcessByPid.FormatEx(pid).RunCmdOutPut();
        }

        /// <summary> 执行eclipse程序 </summary> 
        public static string CmdEclipseByData(this string dataFullPath)
        {
            return CmdStr.CmdEclipseRun.FormatEx(dataFullPath).RunCmdOutPut();
        }


    }
}