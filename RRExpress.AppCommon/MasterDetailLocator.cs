using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RRExpress.AppCommon {
    public class MasterDetailLocator {

        public static readonly BindableProperty MasterProperty =
            BindableProperty.Create("Master",
                typeof(Screen),
                typeof(MasterDetailLocator),
                propertyChanged: MasterChanged
                );


        public static readonly BindableProperty DetailProperty =
            BindableProperty.Create("Detail",
                typeof(Screen),
                typeof(MasterDetailLocator),
                propertyChanged: DetailChanged
                );

        private static Page GetPage(Screen vm) {
            Element vmView = null;
            try {
                vmView = ViewLocator.LocateForModel(vm, null, null);
            } catch (Exception e) {
                throw e;
            }
            if (vmView == null)
                throw new Exception("没有找到视图");
            ViewModelBinder.Bind(vm, vmView, null);

            var activator = vm as IActivate;
            if (activator != null)
                activator.Activate();

            ///////////
            vmView.Parent = null;

            return (Page)vmView;
        }

        private static void MasterChanged(BindableObject bindable, object oldValue, object newValue) {
            var mdp = bindable as MasterDetailPage;
            if (mdp != null) {
                if (newValue == null)
                    return;

                var vm = (Screen)newValue;
                var page = GetPage((Screen)newValue);
                page.Title = vm.DisplayName;

                mdp.Master = page;
                //mdp.Master.Title = vm.DisplayName;
            }
        }

        private static void DetailChanged(BindableObject bindable, object oldValue, object newValue) {
            var mdp = bindable as MasterDetailPage;
            if (mdp != null) {
                if (newValue == null)
                    return;

                var vm = (Screen)newValue;
                var page = GetPage((Screen)newValue);
                page.Title = vm.DisplayName;
                mdp.Detail = page;
            }
        }

        private static Screen GetMaster(BindableObject bindable) {
            return null;
        }

        private static Screen GetDetail(BindableObject bindable) {
            return null;
        }
    }
}
