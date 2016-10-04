using Caliburn.Micro;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Seller.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class AddGoodsViewModel : BaseVM {

        public override string Title {
            get {
                return "发布商品";
            }
        }

        public List<string> DeliveryTypes {
            get;
        } = new List<string>() {
            "平台送", "其它"
        };

        public bool ContainsDeliveryFee { get; set; } = true;

        public string DeliveryType { get; set; }

        public string Category {
            get {
                return string.Join(" / ", this.GoodsCatVM.BigCat?.Data.Name, this.GoodsCatVM.SecondCat?.Data.Name);
            }
        }

        public ICommand ShowCategoriesCmd {
            get;
        }

        public ICommand ShowChannelCmd { get; }

        private GoodsCategoryViewModel GoodsCatVM { get; }

        public ChannelViewModel ChannelVM { get; }

        public AddGoodsViewModel(SimpleContainer container) {
            this.GoodsCatVM = container.GetInstance<GoodsCategoryViewModel>();
            this.GoodsCatVM.ShowSecondCategory = true;
            this.GoodsCatVM.SelectedChanged += GoodsCatVM_SelectedChanged;

            this.ChannelVM = container.GetInstance<ChannelViewModel>();
            this.ChannelVM.SelectedChanged += ChannelVM_SelectedChanged;

            this.DeliveryType = this.DeliveryTypes.First();

            this.ShowCategoriesCmd = new Command(async () => {
                await PopupHelper.PopupAsync(this.GoodsCatVM);
            });

            this.ShowChannelCmd = new Command(async () => {
                await PopupHelper.PopupAsync(this.ChannelVM);
            });
        }

        private void ChannelVM_SelectedChanged(object sender, EventArgs e) {
            this.NotifyOfPropertyChange(() => this.ChannelVM.Selected);
        }

        private void GoodsCatVM_SelectedChanged(object sender, EventArgs e) {
            this.NotifyOfPropertyChange(() => this.Category);
        }
    }
}
