using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RRExpress.Converters {
    public class OrderStatusIconConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || !(value is OrderStatus))
                return null;

            var s = (OrderStatus)value;
            switch (s) {
                case OrderStatus.New:
                    return (char)0xf024;
                case OrderStatus.Geted:
                    return (char)0xf256;
                case OrderStatus.Picked:
                    return (char)0xf291;
                case OrderStatus.Deliveried:
                    return (char)0xf2a3;
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
