using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.Seller.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ShopViewModel : ListBase {

        public override string Title {
            get {
                return "商店";
            }
        }

        public long ShopID { get; set; }

        public ShopInfo Shop { get; private set; }

        public string ShopName { get; set; }

        public ICommand GoBackCmd { get; }

        public ICommand ShowCatalogCmd { get; }

        public ICommand GotoPrevCmd { get; }

        public ICommand GotoNextCmd { get; }

        public ICommand ShowDetailCmd { get; set; }

        public ShopViewModel() {
            this.GoBackCmd = new Command(async () => {
                await IoC.Get<INavigationService>()
                            .GoBackAsync();
            });

            this.ShowDetailCmd = new Command((o) => {
                var d = (GoodsInfo)o;
                IoC.Get<INavigationService>()
                    .For<GoodsViewModel>()
                    .WithParam(v => v.ID, d.ID)
                    .Navigate();
            });

            this.ShowCatalogCmd = new Command(async () => {
                await PopupHelper.PopupAsync<CatalogViewModel>();
            });

            this.GotoPrevCmd = new Command(async () => {
                await this.LoadPrev();
            });

            this.GotoNextCmd = new Command(async () => {
                await this.LoadNext();
            });
        }

        protected override Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page) {
            var rnd = new Random();
            var datas = Enumerable.Range(page * 20, 20)
                .Select(i => new GoodsInfo() {
                    ID = i,
                    Name = $"本地红薯{i}",
                    Price = 0.8M,
                    OrgPrice = 1.0M,
                    Thumbnail = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg",
                    SaleVolume = rnd.Next(1, 10000)
                }
            );
            return Task.FromResult(new Tuple<bool, IEnumerable<object>>(false, datas));
        }

        protected async override void OnActivate() {
            base.OnActivate();

            this.Shop = new ShopInfo() {
                ID = this.ShopID,
                OwnerID = this.ShopID,
                OwnerName = "李二狗",
                OwnerAvatar = "http://t0.qlogo.cn/mbloghead/d353a99312e7c7a34f26/180",
                ShopName = "大马乡东头镇李二狗农庄"
            };
            this.NotifyOfPropertyChange(() => this.Shop);

            await this.LoadData();
        }
    }
}
