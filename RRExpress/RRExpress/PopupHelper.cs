using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;

namespace RRExpress {
    static class PopupHelper {

        public static async Task PopupAsync<T>(T vm = null) where T : class {
            var view = ViewLocator.LocateForModelType(typeof(T), null, null);
            if (view == null)
                throw new Exception(string.Format("Can't locate view for type : {0}", typeof(T).Name));

            if (!(view is PopupPage)) {
                throw new NotSupportedException("PopupAsync only support PopupPage");
            }

            if (vm == null)
                vm = IoC.Get<T>();
            if (vm != null)
                ViewModelBinder.Bind(vm, view, null);

            await PopupNavigation.PushAsync((PopupPage)view);
        }

    }
}
