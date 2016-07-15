#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/10 9:55:28
 * 文件名：AttributeTool
 * 说明：特性操作扩展方法
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
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.AttributeEx
{
    /// <summary> 特性操作扩展方法 </summary>
    public static class AttributeTool
    {

        /// <summary> 获取指定类的指定字段的特性 </summary>
        /// <typeparam name="T"> 返回的特性类型 </typeparam>
        /// <typeparam name="V"> 指定类型 </typeparam>
        /// <param name="value"> 指定类 </param>
        /// <param name="param"> 指定FieldInfo字段属性名称 </param>
        /// <returns> 指定特性 </returns>
        public static T GetFieldAttributeEx<T, V>(this V value, string fileNanme)
            where T : Attribute
            where V : class
        {
            FieldInfo field = value.GetType().GetField(fileNanme);

            if (field == null)
            {
                return null;
            }
            return field.GetCustomAttribute(typeof(T)) as T;
        }

        /// <summary> 获取方法特性 </summary>
        /// <typeparam name="T"> 返回的特性类型 </typeparam>
        /// <typeparam name="V"> 指定类型 </typeparam>
        /// <param name="value"> 指定类 </param>
        /// <param name="param"> 指定method方法名称 </param>
        /// <returns> 指定特性 </returns>
        public static T GetMethodAttributeEx<T, V>(this V pClass, string methodName)
            where T : Attribute
            where V : class
        {

            var method = pClass.GetType().GetMethod(methodName);

            if (method == null)
            {
                return null;
            }
            return method.GetCustomAttribute(typeof(T)) as T;

        }

        /// <summary> 获取成员属性指定特性(注：如果存在多个默认只去第一个) </summary>
        /// <typeparam name="T"> 返回的特性类型 </typeparam>
        /// <typeparam name="V"> 指定类型 </typeparam>
        /// <param name="value"> 指定类 </param>
        /// <param name="param"> 指定MemberInfo名称 </param>
        /// <returns> 指定特性 </returns>
        public static T GetMemberAttributeEx<T, V>(this V pClass, string memberInfoName)
            where T : Attribute
            where V : class
        {

            MemberInfo[] memberInfo = pClass.GetType().GetMember(memberInfoName);
            
            if(memberInfo==null||memberInfo.Count()==0)
            {
                return null;
            }

            MemberInfo pMember=memberInfo[0];

            return pMember.GetCustomAttribute(typeof(T)) as T;

        }







    }
}
