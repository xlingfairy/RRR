using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class GoodsViewModel : BaseVM {
        public override string Title {
            get {
                return "商品详情";
            }
        }


        public long ID {
            get; set;
        }

        public List<BaseVM> SubVMs {
            get;
        }

        private GoodsInfoViewModel InfoVM = null;
        private CommentListViewModel CommentsVM = null;


        public ICommand AddToCartCmd { get; }
        public ICommand GotoCartCmd { get; }
        public ICommand AddToFavoriteCmd { get; }
        public ICommand GotoShopCmd { get; }

        public GoodsViewModel() {
            this.InfoVM = IoC.Get<GoodsInfoViewModel>();
            this.CommentsVM = IoC.Get<CommentListViewModel>();

            this.SubVMs = new List<BaseVM>() {
                this.InfoVM,
                this.CommentsVM
            };

            this.AddToCartCmd = new Command(() => {

            });

            this.GotoCartCmd = new Command(() => { });

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

            this.InfoVM.ID = this.ID;
            this.CommentsVM.ID = this.ID;
        }
    }
}
