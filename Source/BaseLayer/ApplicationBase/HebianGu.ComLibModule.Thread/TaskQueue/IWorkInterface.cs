#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/4 10:49:50  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：Work
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
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ThreadEx.TaskQueue
{
    /// <summary> 执行监听的任务接口 </summary>
    public interface IWorkInterface
    {
        /// <summary> 工作函数 </summary>
        bool RunWork();
    }
}