using Caliburn.Micro;
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
using System.Runtime.CompilerServices;
using AsNum.XFControls.Services;
using Caliburn.Micro.Xamarin.Forms;

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

        public ICommand CheckAllCmd { get; }

        public ICommand ShowDetailCmd { get; }


        public bool CanPay {
            get {
                return this.Datas.Any(d => d.Checked);
            }
        }

        public ObservableCollection<Tmp> Datas {
            get; set;
        } = new ObservableCollection<Tmp>();

        public ShoppingCartViewModel() {
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
                .WithParam(g => g.ID, ((Tmp)o).Data.ID)
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
                var t = new Tmp() {
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

        //protected override Task OnSelected() {
        //    var view = this.GetView();
        //    ((Element)view).SetValue(Caliburn.Micro.Xamarin.Forms.View.IsGeneratedProperty, false);
        //    //ViewModelBinder.Bind(this, (Element)view, null);
        //    ((IViewAware)this).AttachView(view, null);
        //    return base.OnSelected();
        //}



        public class Tmp : PropertyChangedBase {
            private bool _checked;
            public bool Checked {
                get {
                    return this._checked;
                }
                set {
                    this._checked = value;
                    this.NotifyOfPropertyChange(() => this._checked);
                }
            }

            private uint _count;
            public uint Count {
                get {
                    return this._count;
                }
                set {
                    this._count = value;
                    this.NotifyOfPropertyChange(() => this._count);
                }
            }

            public GoodsInfo Data { get; set; }

            public decimal Amount {
                get {
                    return this.Data.Price * this.Count;
                }
            }

            public override void NotifyOfPropertyChange([CallerMemberName] string propertyName = null) {
                base.NotifyOfPropertyChange(propertyName);
            }
        }
    }
}
