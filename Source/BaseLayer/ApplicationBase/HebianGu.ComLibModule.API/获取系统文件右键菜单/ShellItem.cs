//程序开发：lc_mtt
//CSDN博客：http://lemony.cnblogs.com
//个人主页：http://www.3lsoft.com
//注：此代码禁止用于商业用途。有修改者发我一份，谢谢！
//---------------- 开源世界，你我更进步 ----------------

using System;
using System.Collections.Generic;
using System.Text;

namespace HebianGu.ComLibModule.API.WinShell
{
    public class ShellItem
    {
        public ShellItem()
        {
        }

        public ShellItem(IntPtr PIDL, IShellFolder ShellFolder)
        {
            m_PIDL = PIDL;
            m_ShellFolder = ShellFolder;
        }

        private IntPtr m_PIDL;

        public IntPtr PIDL
        {
            get { return m_PIDL; }
            set { m_PIDL = value; }
        }


        private IShellFolder m_ShellFolder;

        public IShellFolder ShellFolder
        {
            get { return m_ShellFolder; }
            set { m_ShellFolder = value; }
        }

    }
}
