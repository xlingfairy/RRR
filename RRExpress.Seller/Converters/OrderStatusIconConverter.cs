using RRExpress.Seller.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RRExpress.Seller.Converters {
    public class OrderStatusIconConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var status = (OrderStatus)value;
            var res = $"RRExpress.Seller.Imgs.OrderStatus.{(int)status}.png";
            return ImageSource.FromResource(res);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
