using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHebianGu.ComLibModule.DelegateEx
{
    public delegate TResult Function<TResult>();
    public delegate TResult Function<T1, TResult>(T1 arg1);
    public delegate TResult Function<T1, T2, TResult>(T1 arg1, T2 arg2);
    public delegate TResult Function<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3);
    public delegate TResult Function<T1, T2, T3, T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    public delegate void Method();
    public delegate void Method<T1>(T1 arg1);
    public delegate void Method<T1, T2>(T1 arg1, T2 arg2);
    public delegate void Method<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);
    public delegate void Method<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}
