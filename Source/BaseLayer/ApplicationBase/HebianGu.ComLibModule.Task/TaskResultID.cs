#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/5 10:01:13  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����TaskResultID
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

namespace HebianGu.ComLibModule.TaskEx
{
    /// <summary> �����ִ�н�� </summary>
    public class TaskResultID
    {
        /// <summary> ���н��� </summary>
        public static int CompleteParam { get { return -9999999; } }

        /// <summary> ����ȡ�� </summary>
        public static int CacelParam { get { return -8888888; } }

        /// <summary> ���б��� </summary>
        public static int FaultParam { get { return -7777777; } }
    }
}