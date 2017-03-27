using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HebianGu.ComLibModule.WPF.Provider
{
    [ValueConversion(typeof(Icon), typeof(ImageSource))]   // Icon是源类型，ImageSource是目标类型。
    public class IconConverter : IValueConverter                 //继承了 IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Icon icon = (Icon)value;

            ImageSource imageSource = 
                System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }
        //下面的函数是为了实现上面转换的逆操作的，这里我们不需要把ImageSource再变成Icon所以没有写具体的实现
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    [ValueConversion(typeof(string), typeof(bool))] 
    public class BoolToSexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //将bool值转换为什么呢？自己在这里定义
            return (bool)value ? "男" : "女";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //反转换方法，就是对照上面的把男女再转换回去
            return (string)value == "男";
        }
    }
}
