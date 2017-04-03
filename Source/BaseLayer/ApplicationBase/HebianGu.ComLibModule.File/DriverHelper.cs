#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/4/3 17:33:24
 * 文件名：DriverHelper
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.FileHelper
{
    public partial class DriverHelper
    {

        /// <summary> 获取驱动路径 </summary>
        public List<string> GetDrivers()
        {
            return Directory.GetLogicalDrives().ToList();
        }

        public List<string> GetFolders(string parent)
        {
            return Directory.GetDirectories(parent).ToList();
        }

        public List<string> GetFiles(string folder)
        {
            return Directory.GetFiles(folder).ToList();
        }
    }


    /// <summary> 此类的说明 </summary>
    partial class DriverHelper
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static DriverHelper t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static DriverHelper Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new DriverHelper();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private DriverHelper()
        {

        }
        #endregion - 单例模式 End -

    }
}
