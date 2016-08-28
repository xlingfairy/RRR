using AsNum.XFControls.Services;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Seller.Entity;
using RRExpress.Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Store {
    public class ShoppingCart : PropertyChangedBase {

        public static Lazy<ShoppingCart> Instance = new Lazy<ShoppingCart>(() => new ShoppingCart());

        /// <summary>
        /// 商品数（按商品）
        /// </summary>
        public int GoodsCount {
            get {
                return this.Datas.Count;
            }
        }

        /// <summary>
        /// 商品总数
        /// </summary>
        public long TotalCount {
            get {
                return this.Datas
                    .Where(d => d.Checked)
                    .Sum(d => d.Count);
            }
        }

        /// <summary>
        /// 总价（不包括运费)
        /// </summary>
        public decimal BaseAmount {
            get {
                return this.Datas
                    .Where(d => d.Checked)
                    .Sum(d => d.Amount);
            }
        }

        /// <summary>
        /// 总价，带运费
        /// </summary>
        public decimal AmountWithDeliveryFee {
            get {
                return this.BaseAmount;
            }
        }


        public bool CanPay {
            get {
                return this.Datas.Any(d => d.Checked);
            }
        }

        public ObservableCollection<ShoppingCartItem> Datas {
            get; set;
        } = new ObservableCollection<ShoppingCartItem>();

        public ICommand CheckAllCmd { get; }

        public ICommand ShowDetailCmd { get; }

        private ShoppingCart() {

            this.CheckAllCmd = new Command(isCheckAll => {
                var flag = (bool)isCheckAll;

                foreach (var d in this.Datas) {
                    d.Checked = flag;
                    d.NotifyOfPropertyChange(() => d.Checked);
                }
            });

            this.ShowDetailCmd = new Command(o => {
                IoC.Get<INavigationService>()
                .For<GoodsViewModel>()
                .WithParam(g => g.ID, 0 /*((Tmp)o).Data.ID*/)
                .Navigate();
            });

            MessagingCenter.Subscribe<GoodsListViewModel, GoodsInfo>(this, GoodsListViewModel.MESSAGE_KEY, (sender, data) => {
                this.AddToCart(data);
            });

            MessagingCenter.Subscribe<GoodsViewModel, GoodsInfo>(this, GoodsViewModel.MESSAGE_KEY, (sender, data) => {
                this.AddToCart(data);
            });

        }


        private void AddToCart(GoodsInfo data) {
            if (!this.Datas.Any(g => g.Data.ID.Equals(data.ID))) {
                var t = new ShoppingCartItem() {
                    Checked = true,
                    Count = 1,
                    Data = data
                };
                this.Datas.Add(t);
                t.PropertyChanged += T_PropertyChanged;
                DependencyService.Get<IToast>()
                                .Show("添加到购物车成功", false);
                this.NotifyOfPropertyChange(() => this.GoodsCount);

                this.Notify();
            }
            else {
                DependencyService.Get<IToast>()
                                  .Show("该商品已经在购物车中", false);
            }
        }


        private void T_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            this.Notify();
        }

        private void Notify() {
            this.NotifyOfPropertyChange(() => this.TotalCount);
            this.NotifyOfPropertyChange(() => this.BaseAmount);
            this.NotifyOfPropertyChange(() => this.AmountWithDeliveryFee);
            this.NotifyOfPropertyChange(() => this.CanPay);
        }


    }
}
