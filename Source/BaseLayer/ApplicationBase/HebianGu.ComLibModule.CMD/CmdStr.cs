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
        public static string CloseProcessByPid { get { return "ntsd -c q -p {0}"; } }

        /// <summary> D调用eclipse(eclrun eclipse) </summary>
        public static string CmdEclipseRun { get { return "eclrun eclipse {0}"; } }


    }
}