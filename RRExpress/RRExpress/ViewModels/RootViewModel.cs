﻿using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Attributes;
using RRExpress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    /// <summary>
    /// Root 框架页
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class RootViewModel : BaseVM {
        public override string Title {
            get {
                return "";
            }
        }

        public List<BaseVM> SubVMs {
            get; set;
        }

        public BaseVM CurrentVM { get; set; }

        private int _flipPos = 0;
        public int FlipPos {
            get {
                return this._flipPos;
            }
            set {
                this._flipPos = value;
                this.CurrentVM = this.SubVMs[value];
                this.NotifyOfPropertyChange(() => this.CurrentVM);
            }
        }


        public string City { get; set; } = "城市";


        public ICommand ChoiceCityCmd { get; }

        public ICommand ShowMsgCmd { get; }

        public ICommand FlipPosCmd { get; }



        public RootViewModel(SimpleContainer container, INavigationService ns) {
            this.SubVMs = new List<BaseVM>() {
                container.GetInstance<HomeViewModel>(),
                container.GetInstance<OrderCenterViewModel>(),
                container.GetInstance<MyViewModel>()
            };
            this.CurrentVM = this.SubVMs.First();

            this.FlipPosCmd = new Command(o => {
                var pos = ((string)o).ToInt();
                this.FlipPos = pos;
                this.NotifyOfPropertyChange(() => this.FlipPos);
            });

            this.ChoiceCityCmd = new Command(async () => {
                await PopupHelper.PopupAsync<ChoiceProvinceViewModel>();
            });

            this.ShowMsgCmd = new Command(async () => {
                await ns.NavigateToViewModelAsync<MessageListViewModel>();
            });

            MessagingCenter.Subscribe<ChoiceCityViewModel, string>(this, ChoiceCityViewModel.MESSAGE_KEY, (vm, city) => {
                this.City = city;
                this.NotifyOfPropertyChange(() => this.City);
            });
        }
    }
}
