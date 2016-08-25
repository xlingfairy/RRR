using AsNum.XFControls;
using Caliburn.Micro;
using RRExpress.AppCommon;
using RRExpress.Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Store {
    public abstract class StoreBaseVM : BaseVM, ISelectable {
        
        public abstract char Icon { get; }

        protected virtual bool ShowNavigationBar {
            get {
                return true;
            }
        }

        public bool IsSelected { get; set; }
        bool ISelectable.IsSelected {
            get {
                return this.IsSelected;
            }
            set {
                this.IsSelected = value;
            }
        }


        //显示实现的接口成员,在本类和子类中均不可见

        private ICommand _SelectedCommand;
        ICommand ISelectable.SelectedCommand {
            get {
                return this._SelectedCommand;
            }
            set {
                this._SelectedCommand = value;
            }
        }

        private ICommand _UnSelectedCommand;
        ICommand ISelectable.UnSelectedCommand {
            get {
                return this._UnSelectedCommand;
            }
            set {
                this._UnSelectedCommand = value;
            }
        }

        void ISelectable.NotifyOfPropertyChange(string propertyName) {
            this.NotifyOfPropertyChange(propertyName);
        }

        public StoreBaseVM() {
            this._SelectedCommand = new Command(async () => await this.OnSelected());
            this._UnSelectedCommand = new Command(async () => await this.OnUnselected());
            //this.SetNavigationBar();
        }

        private void SetNavigationBar() {
            //var page = Application.Current.MainPage.Navigation.NavigationStack.Last();
            //if (page is Views.RootView)
            //    NavigationPage.SetHasNavigationBar(page, this.ShowNavigationBar);
            IoC.Get<RootViewModel>()
                .ShowNavigationBar = this.ShowNavigationBar;
        }

        protected virtual Task OnSelected() {
            this.SetNavigationBar();
            return Task.FromResult<object>(null);
        }

        protected virtual Task OnUnselected() {
            return Task.FromResult<object>(null);
        }


        protected override void OnViewAttached(object view, object context) {
            base.OnViewAttached(view, context);
        }
    }
}
