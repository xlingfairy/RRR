using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RRExpress.Store.Converters {
    public class ResImgConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var source = (string)value;
            if (string.IsNullOrWhiteSpace(source))
                return null;

            //ImageSource.FromResource 会去反射获取 CallingAssembly
            //但是如果在这里调用 ImageSource.FromResource, CallingAssembly 就变成当前这个类所在的 Assembly 了。
            var callingAssemblyMethod = typeof(Assembly).GetTypeInfo().GetDeclaredMethod("GetCallingAssembly");
            if (callingAssemblyMethod != null) {
                var asm = (Assembly)callingAssemblyMethod.Invoke(null, new object[0]);
                //var ress = asm.GetManifestResourceNames();
                var stm = asm.GetManifestResourceStream(source);
                return ImageSource.FromStream(() => {
                    return stm;
                });
            }
            else {
                var img = ImageSource.FromResource(source);
                return img;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
