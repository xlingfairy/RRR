using System;
using System.Globalization;
using Xamarin.Forms;

namespace RRExpress.Converters {

    /// <summary>
    /// 非空
    /// </summary>
    public class NotNullCoverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
