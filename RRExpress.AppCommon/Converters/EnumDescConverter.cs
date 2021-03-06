﻿using RRExpress.Common;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace RRExpress.AppCommon.Converters {
    public class EnumDescConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return "";
            else {
                return EnumHelper.GetDescription((Enum)value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
