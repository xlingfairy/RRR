﻿using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class MyViewModel : BaseVM {
        public override string Title {
            get {
                return "我的信息";
            }
        }

        public ICommand ShowEditCmd { get; }

        public ICommand ShowJoinCmd { get; }

        public ICommand ShowMyOrdersCmd { get; }

        public MyViewModel(SimpleContainer container, INavigationService ns) {
            this.ShowEditCmd = new Command(() => {
                ns.NavigateToViewModelAsync<EditMyInfoViewModel>();
            });

            this.ShowJoinCmd = new Command(() => {
                ns.NavigateToViewModelAsync<JoinWizardViewModel>();
            });

            this.ShowMyOrdersCmd = new Command(() => {
                ns.NavigateToViewModelAsync<MyOrdersViewModel>();
            });
        }
    }
}
