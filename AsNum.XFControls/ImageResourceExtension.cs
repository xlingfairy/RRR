using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AsNum.XFControls {


    /// <summary>
    /// https://developer.xamarin.com/guides/xamarin-forms/working-with/images/#Embedded_Images
    /// </summary>
    [ContentProperty("Source")]
    public class ImageResourceExtension : BindableObject, IMarkupExtension {

        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Source),
                typeof(string),
                typeof(ImageResourceExtension),
                null);

        public string Source {
            get {
                return (string)this.GetValue(SourceProperty);
            }
            set {
                this.SetValue(SourceProperty, value);
            }
        }

        public object ProvideValue(IServiceProvider serviceProvider) {
            if (Source == null) {
                return null;
            }

            //ImageSource.FromResource 会去反射获取 CallingAssembly
            //但是如果在这里调用 ImageSource.FromResource, CallingAssembly 就变成当前这个类所在的 Assembly 了。
            var callingAssemblyMethod = typeof(Assembly).GetTypeInfo().GetDeclaredMethod("GetCallingAssembly");
            if (callingAssemblyMethod != null) {
                var asm = (Assembly)callingAssemblyMethod.Invoke(null, new object[0]);
                //var ress = asm.GetManifestResourceNames();
                var stm = asm.GetManifestResourceStream(this.Source);
                return ImageSource.FromStream(() => {
                    return stm;
                });
            }
            else {
                var img = ImageSource.FromResource(Source);
                return img;
            }
        }
    }
}
