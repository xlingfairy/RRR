using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace RRExpress.AppCommon.Converters {

    /// <summary>
    /// Byte 数组转换为 ImageSource
    /// </summary>
    public class ByteArrayToImageSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return null;
            }

            byte[] bytes = value as byte[];
            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
