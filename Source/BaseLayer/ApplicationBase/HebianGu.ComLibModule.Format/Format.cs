#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/4 10:23:18  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����Format
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

namespace HebianGu.ComLibModule.FormatEx
{
    public static class Format
    {
        /// <summary> ��ʽ���ַ��� p1 ��ʽ p2 ����</summary>
        public static string FormatEx(this string formatStr, object[] objs)
        {
           return string.Format(formatStr, objs);
        }

        /// <summary> ��ʽ���ַ��� p1 ��ʽ p2 ����</summary>
        public static string FormatEx(this string formatStr, object obj)
        {
            return string.Format(formatStr, obj);
        }
    }
}