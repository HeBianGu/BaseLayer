#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/5 18:59:00  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：AttributeParameter
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

namespace HebianGu.ComLibModule.XML
{
    /// <summary>
    /// 节点属性参数
    /// </summary>
    public class AttributeParameter
    {
        private string _name;
        private string _value;

        public AttributeParameter() { }
        public AttributeParameter(string attributeName, string attributeValue)
        {
            this._name = attributeName;
            this._value = attributeValue;
        }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        /// <summary>
        /// 属性值
        /// </summary>
        public string Value
        {
            get { return this._value; }
            set { this._value = value; }
        }
    }
}