#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/3/27 9:32:01
 * 文件名：ControlConfiger
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

namespace HebianGu.Product.WinHelper
{
    partial class ControlConfiger
    {
        private int _notePadSaveCount=30;
        /// <summary> 记事本保存的个数 </summary>
        public int NotePadSaveCount
        {
            get { return _notePadSaveCount; }
            set { _notePadSaveCount = value; }
        }

    }


    /// <summary> 此类的说明 </summary>
    partial class ControlConfiger
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static ControlConfiger t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static ControlConfiger Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new ControlConfiger();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private ControlConfiger()
        {

        }
        #endregion - 单例模式 End -

    }
}
