#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/3/27 11:14:36
 * 文件名：ConvertExtend
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

namespace HebianGu.ObjectBase.ObjectHelper
{
   public static class ConvertExtend
    {
        public static bool ToBool(this IGroupConvertobject o)
        {
            return Convert.ToBoolean(o.Value);
        }

        public static byte ToByte(this IGroupConvertobject o)
        {
            return Convert.ToByte(o.Value);
        }
    }
}
