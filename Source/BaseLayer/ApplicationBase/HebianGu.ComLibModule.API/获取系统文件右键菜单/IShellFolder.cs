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
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214E6-0000-0000-C000-000000000046")]
    public interface IShellFolder
    {
        void ParseDisplayName(
            IntPtr hwnd,
            IntPtr pbc,
            [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName,
            out uint pchEaten,
            out IntPtr ppidl,
            ref uint pdwAttributes);

        [PreserveSig]
        int EnumObjects(IntPtr hWnd, SHCONTF flags, out IntPtr enumIDList);

        void BindToObject(
            IntPtr pidl,
            IntPtr pbc,
            [In()] ref Guid riid,
            out IShellFolder ppv);

        void BindToStorage(
            IntPtr pidl,
            IntPtr pbc,
            [In()] ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out object ppv);

        [PreserveSig()]
        uint CompareIDs(
            int lParam,
            IntPtr pidl1,
            IntPtr pidl2);

        void CreateViewObject(
            IntPtr hwndOwner,
            [In()] ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out object ppv);

        void GetAttributesOf(
            uint cidl,
            [In(), MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl,
           ref SFGAO rgfInOut);

        IntPtr GetUIObjectOf(
            IntPtr hwndOwner,
            uint cidl,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl,
            [In()] ref Guid riid,
            out IntPtr rgfReserved);

        void GetDisplayNameOf(
            IntPtr pidl,
            SHGNO uFlags,
            IntPtr lpName);

        IntPtr SetNameOf(
            IntPtr hwnd,
            IntPtr pidl,
            [MarshalAs(UnmanagedType.LPWStr)] string pszName,
           SHGNO uFlags);
    }
}
