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
    [ComImport(),
    Guid("000214F2-0000-0000-C000-000000000046"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumIDList
    {
        [PreserveSig()]
        uint Next(
            uint celt,
            out IntPtr rgelt,
            out int pceltFetched);

        void Skip(
            uint celt);

        void Reset();

        IEnumIDList Clone();
    }
}
