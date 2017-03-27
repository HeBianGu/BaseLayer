#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/3/24 11:38:34
 * 文件名：SystemInfoExtension
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

    /// <summary> 系统文件扩展方法 </summary>
    public static class SystemInfoExtension
    {
        /// <summary> 查找所有满足条件的文件 T查找文件的类型 </summary>
        public static IEnumerable<T> FindAll<T>(this FileSystemInfo info, Predicate<T> match = null) where T : FileSystemInfo
        {
            if (info is T)
            {
                T t = info as T;

                if (match(t))
                {
                    // HTodo  ：匹配条件返回迭代器 
                    yield return t;

                }
            }

            if (info is DirectoryInfo)
            {
                DirectoryInfo dir = info as DirectoryInfo;

                foreach (var item in dir.EnumerateFileSystemInfos())
                {
                    // HTodo  ：递归查找所有满足条件 
                    yield return item.Find<T>(match);
                }
            }
        }

        /// <summary> 查找第一个满足条件的文件 T查找文件的类型 </summary>
        public static T Find<T>(this FileSystemInfo info, Predicate<T> match = null) where T : FileSystemInfo
        {
            if (info is T)
            {
                T t = info as T;

                if (match(t))
                {
                    // HTodo  ：匹配条件返回迭代器 
                    return t;
                }
            }

            if (info is DirectoryInfo)
            {
                DirectoryInfo dir = info as DirectoryInfo;

                foreach (var item in dir.EnumerateFileSystemInfos())
                {
                    // HTodo  ：递归查找所有满足条件 
                    T f = item.Find<T>(match);

                    if (f != null) return f;
                }

            }

            return null;
        }

        /// <summary> 对所有子文件都执行act操作 T标识要检查文件的类型 </summary>
        public static void Foreach<T>(this FileSystemInfo info, Action<T> act) where T : FileSystemInfo
        {
            if (info is T)
            {
                T t = info as T;
                act(t);
            }

            if (info is DirectoryInfo)
            {
                DirectoryInfo dir = info as DirectoryInfo;

                dir.Foreach(act);
            }
        }
    }
}
