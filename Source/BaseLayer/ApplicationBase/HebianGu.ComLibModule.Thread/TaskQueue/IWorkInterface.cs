#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/4 10:49:50  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����Work
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
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ThreadEx.TaskQueue
{
    /// <summary> ִ�м���������ӿ� </summary>
    public interface IWorkInterface
    {
        /// <summary> �������� </summary>
        bool RunWork();
    }
}