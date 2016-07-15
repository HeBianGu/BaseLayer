#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/5 10:01:13  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：TaskResultID
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

namespace HebianGu.ComLibModule.TaskEx
{
    /// <summary> 任务的执行结果 </summary>
    public class TaskResultID
    {
        /// <summary> 运行结束 </summary>
        public static int CompleteParam { get { return -9999999; } }

        /// <summary> 运行取消 </summary>
        public static int CacelParam { get { return -8888888; } }

        /// <summary> 运行报错 </summary>
        public static int FaultParam { get { return -7777777; } }
    }
}