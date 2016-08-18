using AsNum.XFControls.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AsNum.XFControls.Binders {
    public class ListViewBinder {

        public static readonly BindableProperty LoadMoreCmdProperty =
            BindableProperty.CreateAttached("LoadMoreCmd",
                typeof(ICommand),
                typeof(ListViewBinder),
                null,
                propertyChanged: Changed);

        public static void SetLoadMoreCmd(BindableObject view, ICommand cmd) {
            view.SetValue(LoadMoreCmdProperty, cmd);
        }

        public static ICommand GetLoadCmd(BindableObject view) {
            return (ICommand)view.GetValue(LoadMoreCmdProperty);
        }

        private static void Changed(BindableObject bindable, object oldValue, object newValue) {
            var lv = (ListView)bindable;
            if (lv == null)
                return;

            lv.ItemAppearing -= Lv_ItemAppearing;
            lv.ItemAppearing += Lv_ItemAppearing;
        }

        private static void Lv_ItemAppearing(object sender, ItemVisibilityEventArgs e) {
            var lv = (ListView)sender;
            var cmd = GetLoadCmd(lv);
            if (cmd != null && cmd.CanExecute(null)) {
                var last = lv.ItemsSource?.Cast<object>().LastOrDefault();
                if (last != null && last.Equals(e.Item)) {
                    //DependencyService.Get<IToast>()
                    //    .Show("正在加载...");
                    cmd.Execute(null);
                }
            }
        }
    }
}
