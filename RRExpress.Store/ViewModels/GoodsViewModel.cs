using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.Seller.Entity;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.PreRequest)]
    public class GoodsViewModel : BaseVM {
        public static readonly string MESSAGE_KEY = "ADDTOCART";

        public override string Title {
            get {
                return "商品详情";
            }
        }


        public long ID {
            get; set;
        }

        private GoodsInfo Data { get; set; }

        public List<BaseVM> SubVMs {
            get;
        }

        private GoodsInfoViewModel InfoVM = null;
        private CommentListViewModel CommentsVM = null;

        public ShoppingCartViewModel CartVM { get; }

        public ICommand AddToCartCmd { get; }
        public ICommand GotoCartCmd { get; }
        public ICommand AddToFavoriteCmd { get; }
        public ICommand GotoShopCmd { get; }


        public GoodsViewModel() {
            this.InfoVM = IoC.Get<GoodsInfoViewModel>();
            this.CommentsVM = IoC.Get<CommentListViewModel>();
            this.CartVM = IoC.Get<ShoppingCartViewModel>();

            this.SubVMs = new List<BaseVM>() {
                this.InfoVM,
                this.CommentsVM
            };

            this.AddToCartCmd = new Command(() => {
                MessagingCenter.Send(this, MESSAGE_KEY, this.Data);
            });

            this.GotoCartCmd = new Command(async () => {
                //await PopupHelper.PopupAsync<ShoppingCartPopupViewModel>();
                await IoC.Get<INavigationService>()
                    .NavigateToViewModelAsync<ShoppingCartPopupViewModel>();
            });

            this.AddToFavoriteCmd = new Command(() => { });

            this.GotoShopCmd = new Command(() => {
                IoC.Get<INavigationService>()
                    .For<ShopViewModel>()
                    .WithParam(s => s.ShopID, 0)
                    .Navigate();
            });

        }

        protected override void OnActivate() {
            base.OnActivate();

            this.Data = new GoodsInfo() {
                ID = this.ID,
                Name = $"商品{this.ID}",
                Price = 56,
                Thumbnail = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
            };

            this.InfoVM.ID = this.ID;
            this.CommentsVM.ID = this.ID;
        }
    }
}
