#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/3/27 11:00:02
 * 文件名：BaseStringExtention
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.String.Extention
{
    public  static class BaseStringExtention
    {

        /// <summary> 是否为空或Null </summary>
        public static bool IsEmptyOrNull(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary> 格式化字符串 </summary>
        public static string Format(this string s,params object[] objs)
        {
            return string.Format(s, objs);
        }

        /// <summary> 按照指定格式转换时间 示例： format="yyyy-MM-dd"</summary>
        public static DateTime ToDateTime(this string s,string format)
        {
            DateTime outTime;

            DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime);

            return outTime;
        }


    }

}
