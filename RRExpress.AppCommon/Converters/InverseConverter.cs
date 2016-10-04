using System;
using System.Globalization;
using Xamarin.Forms;

namespace RRExpress.AppCommon.Converters {
    /// <summary>
    /// 取反转换
    /// </summary>
    public class InverseConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var b = (bool)value;
            return !b;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
