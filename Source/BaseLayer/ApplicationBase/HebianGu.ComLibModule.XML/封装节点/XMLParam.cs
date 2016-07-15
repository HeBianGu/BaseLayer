#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/5 18:58:33  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：XMLParam
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
    /// XMLHelper参数
    /// </summary>
    public class XmlParam
    {
        private string _name;
        private string _innerText;
        private string _namespaceOfPrefix;
        private AttributeParameter[] _attributes;
        public XmlParam() { }
        public XmlParam(string name, params AttributeParameter[] attParas) : this(name, null, null, attParas) { }
        public XmlParam(string name, string innerText, params AttributeParameter[] attParas) : this(name, innerText, null, attParas) { }
        public XmlParam(string name, string innerText, string namespaceOfPrefix, params AttributeParameter[] attParas)
        {
            this._name = name;
            this._innerText = innerText;
            this._namespaceOfPrefix = namespaceOfPrefix;
            this._attributes = attParas;
        }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        /// <summary>
        /// 节点文本
        /// </summary>
        public string InnerText
        {
            get { return this._innerText; }
            set { this._innerText = value; }
        }
        /// <summary>
        /// 节点前缀xmlns声明(命名空间URI)
        /// </summary>
        public string NamespaceOfPrefix
        {
            get { return this._namespaceOfPrefix; }
            set { this._namespaceOfPrefix = value; }
        }
        /// <summary>
        /// 节点属性集
        /// </summary>
        public AttributeParameter[] Attributes
        {
            get { return this._attributes; }
            set { this._attributes = value; }
        }
    }
}