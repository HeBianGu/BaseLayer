#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/3/27 14:50:25
 * 文件名：ObjectGroupExtension
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

namespace HebianGu.ObjectBase.ObjectHelper.ObjectExtention
{
    public static class ObjectGroupExtension
    {
        /// <summary> 获取格式转换分组 </summary>
        public static IGroupConvertobject GroupConvertobject(this object s)
        {
            return ExtendGroupProvider.As<IGroupConvertobject>(s);
        }

        /// <summary> 获取反射分组 </summary>
        public static IGroupRelectbject GroupRelectbject(this object s)
        {
            return ExtendGroupProvider.As<IGroupRelectbject>(s);
        }

        /// <summary> 获取基础分组 </summary>
        public static IGroupBaseObject GroupBaseObject(this object s)
        {
            return ExtendGroupProvider.As<IGroupBaseObject>(s);
        }
    }
}
