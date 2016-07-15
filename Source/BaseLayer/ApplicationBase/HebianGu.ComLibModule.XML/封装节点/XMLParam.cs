#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/5 18:58:33  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����XMLParam
 *
 * ˵����
 * 
 * 
 * �޸��ߣ�           ʱ�䣺               
 * �޸�˵����
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
    /// XMLHelper����
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
        /// �ڵ�����
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        /// <summary>
        /// �ڵ��ı�
        /// </summary>
        public string InnerText
        {
            get { return this._innerText; }
            set { this._innerText = value; }
        }
        /// <summary>
        /// �ڵ�ǰ׺xmlns����(�����ռ�URI)
        /// </summary>
        public string NamespaceOfPrefix
        {
            get { return this._namespaceOfPrefix; }
            set { this._namespaceOfPrefix = value; }
        }
        /// <summary>
        /// �ڵ����Լ�
        /// </summary>
        public AttributeParameter[] Attributes
        {
            get { return this._attributes; }
            set { this._attributes = value; }
        }
    }
}