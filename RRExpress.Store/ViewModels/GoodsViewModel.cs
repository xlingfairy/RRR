using Caliburn.Micro;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public GoodsViewModel() {
            this.InfoVM = IoC.Get<GoodsInfoViewModel>();
            this.CommentsVM = IoC.Get<CommentListViewModel>();

            this.SubVMs = new List<BaseVM>() {
                this.InfoVM,
                this.CommentsVM
            };
        }

        protected override void OnActivate() {
            base.OnActivate();

            this.InfoVM.ID = this.ID;
            this.CommentsVM.ID = this.ID;
        }
    }
}
