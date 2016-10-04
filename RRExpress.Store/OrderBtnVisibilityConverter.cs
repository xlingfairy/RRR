using RRExpress.Seller.Entity;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace RRExpress.Store {
    public class OrderBtnVisibilityConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || !(value is OrderInfo) || parameter == null || !(parameter is string))
                return false;

            OrderStatus status;
            if (Enum.TryParse<OrderStatus>((string)parameter, true, out status)) {
                var order = (OrderInfo)value;
                return (order.Status & status) == order.Status;
            }
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
