//���򿪷���lc_mtt
//CSDN���ͣ�http://lemony.cnblogs.com
//������ҳ��http://www.3lsoft.com
//ע���˴����ֹ������ҵ��;�����޸��߷���һ�ݣ�лл��
//---------------- ��Դ���磬���Ҹ����� ----------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace HebianGu.ComLibModule.API.WinShell
{
    [ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), GuidAttribute("000214e4-0000-0000-c000-000000000046")]
    public interface IContextMenu
    {
        [PreserveSig()]
        Int32 QueryContextMenu(
            IntPtr hmenu,
            uint iMenu,
            uint idCmdFirst,
            uint idCmdLast,
            CMF uFlags);

        [PreserveSig()]
        Int32 InvokeCommand(
            ref CMINVOKECOMMANDINFOEX info);

        [PreserveSig()]
        void GetCommandString(
            int idcmd,
            GetCommandStringInformations uflags,
            int reserved,
            StringBuilder commandstring,
            int cch);
    }
}
