#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/5 18:59:00  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����AttributeParameter
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
    /// �ڵ����Բ���
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
        /// ��������
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        /// <summary>
        /// ����ֵ
        /// </summary>
        public string Value
        {
            get { return this._value; }
            set { this._value = value; }
        }
    }
}