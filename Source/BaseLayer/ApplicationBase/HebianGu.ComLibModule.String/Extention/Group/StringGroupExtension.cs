#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/3/27 14:46:33
 * 文件名：StringGroupExtendtion
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HebianGu.ObjectBase.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.String.Extention
{
    public static class StringGroupExtension
    {

        /// <summary> 获取正则表达式分组 </summary>
        public static IGroupMatchString GroupMatchString(this string s)
        {
            return ExtendGroupProvider.As<IGroupMatchString>(s);
        }

        /// <summary> 获取路径分组 </summary>
        public static IGroupPathString GroupPathString(this string s)
        {
            return ExtendGroupProvider.As<IGroupPathString>(s);
        }
    }
}
