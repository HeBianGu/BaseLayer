#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/1/18 14:56:52
 * 文件名：Class1
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

namespace HebianGu.ComLibModule.CaculateEngine
{
    class TaskListEngine
    {
        List<ITaskEntity> _taskList = new List<ITaskEntity>();

        public void Register(ITaskEntity task)
        {
            _taskList.Add(task);
        }

        /// <summary> 同时做N个方法，有一个方法错误则退出 </summary>
        public bool RunOutWhenFalse(Action<ITaskEntity> act)
        {
            foreach (var item in _taskList)
            {
                if (!item.Result())
                {
                    act(item);
                    return false;
                }
            }

            return true;
        }

        /// <summary> 同时做N个方法，错误的退出 act = 碰到错误项要做的事 </summary>
        public bool RunExceptFalse(Action<ITaskEntity> act)
        {
            bool isContainFalse = true;

            foreach (var item in _taskList)
            {
                if (!item.Result())
                {
                    act(item);
                    isContainFalse = false;
                    continue;
                }
            }

            return isContainFalse;
        }

    }


  
}
