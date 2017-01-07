#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/4 10:11:07  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：Cmd
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
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.CMD
{
    class CmdStr
    {
        /// <summary> DOS关闭进程命令(ntsd -c q -p PID )PID为进程的ID </summary>
        public const string CloseProcessByPid = "ntsd -c q -p {0}";

        /// <summary> D调用eclipse(eclrun eclipse) </summary>
        public const string CmdEclipseRun = "eclrun eclipse {0}";

        /// <summary>  查看本机网卡配置信息 "/c ipconfig /all" </summary>
        public const string CmdIpConfigerAll = "/c ipconfig /all";

        /// <summary> 定时关机 string.Format("/c shutdown -s -t {0}", shijian) </summary>
        public const string CmdShutDown = "/c shutdown -s -t {0}";

        /// <summary> 取消定时关机 "/c shutdown -a" </summary>
        public const string CmdClearShutDown  = "/c shutdown -a";

        /// <summary>  解析域名ip地址 "/c ping {0}" </summary>
        public const string CmdPing= "/c ping {0}";

        /// <summary> 显示所有连接和侦听端口 "/c  netstat -an" </summary>
        public const string CmdNetStat = "/c  netstat -an";

        /// <summary> 显示路由表内容 "/c  netstat -r" </summary>
        public const string CmdNetStat_R = "/c  netstat -r";

        /// <summary>  查询本机系统 "/c winver" </summary>
        public const string CmdWinver = "/c winver";


        /// <summary> IP地址侦测器 "/c  Nslookup" </summary>
        public const string CmdNslookup = "/c  Nslookup";

        /// <summary> 打开磁盘清理工具 "/c  cleanmgr" </summary>
        public const string CmdCleanmgr = "/c  cleanmgr";

        /// <summary> 打开系统的注册表 "/c  regedit" </summary>
        public const string CmdRegedit = "/c  regedit";
    }
}