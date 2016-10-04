using RRExpress.Store.ViewModels;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace RRExpress.Store.Converters {
    public class IsShoppingCartConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value is ShoppingCartViewModel;
            //if (value != null && value is ShoppingCartViewModel) {
            //    var model = (ShoppingCartViewModel)value;
            //    return model.GoodsCount > 0;
            //}
            //return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
