#region <�� �� ע ��>
/*
 * ========================================================================
 * Copyright(c) �����²���ʯ�ͿƼ����޹�˾, All Rights Reserved.
 * ========================================================================
 *    
 * ���ߣ�[���]   ʱ�䣺2015/11/4 10:07:26  ��������ƣ�DEV-LIHAIJUN
 *
 * �ļ�����CmdExcute
 *
 * ˵���� ִ��DOS�������չ���� 
 * 
 * 
 * �޸��ߣ�           ʱ�䣺               
 * �޸�˵����
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HebianGu.ComLibModule.FormatEx;

namespace HebianGu.ComLibModule.CMD
{
    /// <summary> ִ��DOS�������չ���� </summary>
    public static class CmdAPI
    {
        /// <summary> ����DOS����  DOS�رս�������(ntsd -c q -p PID )PIDΪ���̵�ID   </summary>   
        public static string RunCmd(this string command)
        {
            //  ����һ�������M��   
            System.Diagnostics.Process p = new System.Diagnostics.Process();   
            p.StartInfo.FileName = "cmd.exe"; 
            p.StartInfo.Arguments = "/c " + command; 
            p.StartInfo.UseShellExecute = false;  
            p.StartInfo.RedirectStandardInput = true;  
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;

            p.Start();
            //  ��ݔ����ȡ��������нY�� 
            return p.StandardOutput.ReadToEnd();          

        }

        /// <summary> �ص����� P1 ���̵�PID </summary>
        [Obsolete("δ����")]
        public static string CloseProcessByPid(this string pid)
        {
           return CmdStr.CloseProcessByPid.FormatEx(pid).RunCmd();
        }

        /// <summary> ִ��eclipse���� </summary> 
        public static string CmdEclipseByData(this string dataFullPath)
        {
            return CmdStr.CmdEclipseRun.FormatEx(dataFullPath).RunCmd();
        }
    }
}