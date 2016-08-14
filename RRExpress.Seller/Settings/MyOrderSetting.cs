﻿using RRExpress.AppCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon.Attributes;

namespace RRExpress.Seller.Settings {

    [Regist(InstanceMode.Singleton, ForType = typeof(ISettingItem))]
    public class MyOrderSetting : ISettingItem {
        public bool CanUse {
            get {
                return true;
            }
        }

        public SettingCatlogs? Catlog {
            get {
                return null;
            }
        }

        public string CustomCatlog {
            get {
                return Const.SettingCatlog;
            }
        }

        public string Icon {
            get {
                return null;
            }
        }

        public int Order {
            get {
                return 3;
            }
        }

        public string Title {
            get {
                return "我的订单";
            }
        }

        public Task Execute(SimpleContainer container, INavigationService ns) {
            return Task.FromResult<object>(null);
        }
    }
}