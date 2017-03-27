using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HebianGu.ComLibModule.WPF.Provider
{
    public partial class WPFHelper
    {
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        /// <summary> 将 Bitmap转换为BitmapSource </summary>
        private ImageSource BitmapToBitmapSource(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            //记得要进行内存释放。否则会有内存不足的报错。
            if (!DeleteObject(hBitmap))
            {
                throw new System.ComponentModel.Win32Exception();
            }

            return wpfBitmap;
        }

        /// <summary> 查找父控件 </summary>
        public T GetParentObject<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);

            while (parent != null)
            {
                if (parent is T && (((T)parent).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)parent;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        /// <summary> 查找子控件 </summary>
        public T GetChildObject<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            T grandChild = null;

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)child;
                }
                else
                {
                    grandChild = GetChildObject<T>(child, name);
                    if (grandChild != null)
                        return grandChild;
                }
            }

            return null;

        }

        /// <summary> 查找所有子控件 </summary>
        public List<T> GetChildObjects<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).Name == name || string.IsNullOrEmpty(name)))
                {
                    childList.Add((T)child);
                }

                childList.AddRange(GetChildObjects<T>(child, ""));
            }

            return childList;

        }

        /// <summary> 获取所属的父级Window </summary>
        public Window GetWindow(DependencyObject obj)
        {
            return Window.GetWindow(obj);
        }
    }


    /// <summary> 此类的说明 </summary>
    partial class WPFHelper
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static WPFHelper t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static WPFHelper Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new WPFHelper();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private WPFHelper()
        {

        }
        #endregion - 单例模式 End -

    }
}
