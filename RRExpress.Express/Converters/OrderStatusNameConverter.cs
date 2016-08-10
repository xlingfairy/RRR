using RRExpress.Service.Entity;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace RRExpress.Express.Converters {

    /// <summary>
    /// 订单状态显示名称
    /// </summary>
    public class OrderStatusNameConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || !(value is OrderStatus))
                return null;

            var s = (OrderStatus)value;
            switch (s) {
                case OrderStatus.New:
                    return "下单成功";
                case OrderStatus.Geted:
                    return "自由快递人接单";
                case OrderStatus.Picked:
                    return "已取货";
                case OrderStatus.Deliveried:
                    return "已送达";
                case OrderStatus.Dispute:
                    return "纠纷中";
                case OrderStatus.Paied:
                    return "已支付";
                case OrderStatus.Finished:
                    return "订单完成";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
