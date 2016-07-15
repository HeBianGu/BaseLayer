#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/4 10:23:18  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：Format
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

namespace HebianGu.ComLibModule.FormatEx
{
    public static class Format
    {
        /// <summary> 格式化字符串 p1 格式 p2 参数</summary>
        public static string FormatEx(this string formatStr, object[] objs)
        {
           return string.Format(formatStr, objs);
        }

        /// <summary> 格式化字符串 p1 格式 p2 参数</summary>
        public static string FormatEx(this string formatStr, object obj)
        {
            return string.Format(formatStr, obj);
        }
    }
}