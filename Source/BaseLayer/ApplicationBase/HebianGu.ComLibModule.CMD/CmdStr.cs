#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/4 10:11:07  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����Cmd
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

namespace HebianGu.ComLibModule.CMD
{
    class CmdStr
    {
        /// <summary> DOS�رս�������(ntsd -c q -p PID )PIDΪ���̵�ID </summary>
        public static string CloseProcessByPid { get { return "ntsd -c q -p {0}"; } }

        /// <summary> D����eclipse(eclrun eclipse) </summary>
        public static string CmdEclipseRun { get { return "eclrun eclipse {0}"; } }


    }
}