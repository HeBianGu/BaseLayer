using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HebianGu.ComLibModule.ControlHelper
{

    /// <summary> WinForm Control扩展 </summary>
    public static class ControExtend
    {

        /// <summary> 获取指定控件 </summary>
        public static IEnumerable<T> GetControls<T>(this Control control, Predicate<T> filter) where T : Control
        {
            foreach (Control c in control.Controls)
            {
                if (c is T)
                {
                    T t = c as T;

                    if (filter != null)
                    {
                        if (filter(t))
                        {
                            yield return t;
                        }
                        else
                        {
                            foreach (T _t in GetControls<T>(c, filter))
                                yield return _t;
                        }
                    }
                    else
                        yield return t;
                }
                else
                {
                    foreach (T _t in GetControls<T>(c, filter))
                        yield return _t;
                }
            }
        }


        /// <summary> 遍历指定控件执行方法 </summary>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        /// <summary> 遍历指定控件执行方法 </summary>
        public static void ForEach<T>(this Control control, Action<T> action) where T : Control
        {
            var source = control.GetControls<T>(l => true);

            foreach (var item in source)
                action(item);
        }
    }
}
