using RRExpress.AppCommon.Attributes;
using RRExpress.Seller.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ShoppingCartViewModel : StoreBaseVM {

        public static readonly string MESSAGE_KEY = "GOODSCOUNT";

        public override string Title {
            get {
                return "购物车";
            }
        }

        public override char Icon {
            get {
                return (char)0xf07a;
            }
        }

        public int GoodsCount {
            get {
                return this.Goods.Count;
            }
        }

        public ObservableCollection<GoodsInfo> Goods {
            get; set;
        } = new ObservableCollection<GoodsInfo>();

        public ShoppingCartViewModel() {
            MessagingCenter.Subscribe<GoodsListViewModel, GoodsInfo>(this, GoodsListViewModel.MESSAGE_KEY, (sender, data) => {
                if (!this.Goods.Any(g => g.ID.Equals(data.ID))) {
                    this.Goods.Add(data);
                    this.NotifyOfPropertyChange(() => this.GoodsCount);
                }
            });
        }
    }
}
