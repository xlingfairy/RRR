using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Seller.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class MyGoodsViewModel : BaseVM {
        public override string Title {
            get {
                return "我的商品";
            }
        }

        public ICommand TestCmd { get; }

        public MyGoodsViewModel() {
            this.TestCmd = new Command(() => {
                Application.Current.MainPage.DisplayAlert("111", "222", "ok");
            });
        }

    }
}
