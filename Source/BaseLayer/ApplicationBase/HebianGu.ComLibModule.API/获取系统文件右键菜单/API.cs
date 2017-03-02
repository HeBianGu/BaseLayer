//程序开发：lc_mtt
//CSDN博客：http://lemony.cnblogs.com
//个人主页：http://www.3lsoft.com
//注：此代码禁止用于商业用途。有修改者发我一份，谢谢！
//---------------- 开源世界，你我更进步 ----------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace HebianGu.ComLibModule.API.WinShell
{
    public class API
    {
        #region API 导入

        public const int MAX_PATH = 260;
        public const int S_OK = 0;
        public const int S_FALSE = 1;
        public const uint CMD_FIRST = 1;
        public const uint CMD_LAST = 30000;

        [DllImport("shell32.dll")]
        public static extern Int32 SHGetDesktopFolder(out IntPtr ppshf);

        [DllImport("Shlwapi.Dll", CharSet = CharSet.Auto)]
        public static extern Int32 StrRetToBuf(IntPtr pstr, IntPtr pidl, StringBuilder pszBuf, int cchBuf);

        [DllImport("shell32.dll")]
        public static extern int SHGetSpecialFolderLocation(IntPtr handle, CSIDL nFolder, out IntPtr ppidl);

        [DllImport("shell32",
            EntryPoint = "SHGetFileInfo",
            ExactSpelling = false,
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern IntPtr SHGetFileInfo(
            IntPtr ppidl,
            FILE_ATTRIBUTE dwFileAttributes,
            ref SHFILEINFO sfi,
            int cbFileInfo,
            SHGFI uFlags);

        [DllImport("user32",
            SetLastError = true,
            CharSet = CharSet.Auto)]
        public static extern IntPtr CreatePopupMenu();

        [DllImport("user32.dll",
            ExactSpelling = true,
            CharSet = CharSet.Auto)]
        public static extern uint TrackPopupMenuEx(
            IntPtr hmenu,
            TPM flags,
            int x,
            int y,
            IntPtr hwnd,
            IntPtr lptpm);

        [DllImport("Shell32.Dll")]
        private static extern bool SHGetSpecialFolderPath(
            IntPtr hwndOwner, 
            StringBuilder lpszPath,
            ShellSpecialFolders nFolder,
            bool fCreate);

        #endregion

        /// <summary> 获得桌面 Shell </summary>
        public static IShellFolder GetDesktopFolder(out IntPtr ppshf)
        {
            SHGetDesktopFolder(out ppshf);
            Object obj = Marshal.GetObjectForIUnknown(ppshf);
            return (IShellFolder)obj;
        }

        /// <summary> 获取路径 </summary>
        public static string GetPathByIShell(IShellFolder Root, IntPtr pidlSub)
        {
            IntPtr strr = Marshal.AllocCoTaskMem(MAX_PATH * 2 + 4);
            Marshal.WriteInt32(strr, 0, 0);
            StringBuilder buf = new StringBuilder(MAX_PATH);
            Root.GetDisplayNameOf(pidlSub, SHGNO.FORADDRESSBAR | SHGNO.FORPARSING, strr);
            API.StrRetToBuf(strr, pidlSub, buf, MAX_PATH);
            Marshal.FreeCoTaskMem(strr);
            return buf.ToString();
        }

        /// <summary> 获取显示名称 </summary>
        public static string GetNameByIShell(IShellFolder Root, IntPtr pidlSub)
        {
            IntPtr strr = Marshal.AllocCoTaskMem(MAX_PATH * 2 + 4);
            Marshal.WriteInt32(strr, 0, 0);
            StringBuilder buf = new StringBuilder(MAX_PATH);
            Root.GetDisplayNameOf(pidlSub, SHGNO.INFOLDER, strr);
            API.StrRetToBuf(strr, pidlSub, buf, MAX_PATH);
            Marshal.FreeCoTaskMem(strr);
            return buf.ToString();
        }

        /// <summary> 根据 PIDL 获取显示名称 </summary>
        public static string GetNameByPIDL(IntPtr pidl)
        {
            SHFILEINFO info = new SHFILEINFO();
            API.SHGetFileInfo(pidl, 0, ref info, Marshal.SizeOf(typeof(SHFILEINFO)),
                SHGFI.PIDL | SHGFI.DISPLAYNAME | SHGFI.TYPENAME);
            return info.szDisplayName;
        }

        /// <summary> 获取特殊文件夹的路径 </summary>
        public static string GetSpecialFolderPath(IntPtr hwnd, ShellSpecialFolders nFolder)
        {
            StringBuilder sb = new StringBuilder(MAX_PATH);
            SHGetSpecialFolderPath(hwnd, sb, nFolder, false);
            return sb.ToString();
        }

        /// <summary> 根据路径获取 IShellFolder 和 PIDL </summary>
        public static IShellFolder GetShellFolder(IShellFolder desktop, string path, out IntPtr Pidl)
        {
            IShellFolder IFolder;
            uint i, j = 0;
            desktop.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, path, out i, out Pidl, ref j);
            desktop.BindToObject(Pidl, IntPtr.Zero, ref Guids.IID_IShellFolder, out IFolder);
            return IFolder;
        }

        /// <summary> 根据路径获取 IShellFolder </summary>
        public static IShellFolder GetShellFolder(IShellFolder desktop, string path)
        {
            IntPtr Pidl;
            return GetShellFolder(desktop, path, out Pidl);
        }
    }
}
