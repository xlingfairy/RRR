using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
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

    /// <summary>
    /// 该VM没有对应的VIEW， 视图合并在 HomeViw 中， 它的用处只是方便使用 ListBase 而已。
    /// ListBase 和 StoreBase 分别处理不同的东西，
    /// 所以它商品列表的功能从 HomeViewModel 中独立出来了。
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class GoodsListViewModel : ListBase {
        public static string MESSAGE_KEY = "ADDTOCART";

        public override string Title {
            get {
                return "商品列表";
            }
        }

        public ICommand AddToCartCmd {
            get;
        }

        public ICommand ShowDetailCmd {
            get;
        }

        protected override Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page) {
            var datas = Enumerable.Range(page * 20, 20)
                .Select(i => new GoodsInfo() {
                    ID = i,
                    Name = $"本地红薯{i}",
                    Price = 0.8M,
                    OrgPrice = 1.0M,
                    Thumbnail = "http://image99.360doc.com/DownloadImg/2016/08/2203/78452532_2.jpg",
                    StoreName = $"赵老三农庄{i % 3}",
                    StoreID = i % 3
                }
            );
            return Task.FromResult(new Tuple<bool, IEnumerable<object>>(false, datas));
        }

        public async Task FirstLoad() {
            if (this.Datas.Count == 0)
                await this.LoadData();
        }

        public GoodsListViewModel() {
            this.AddToCartCmd = new Command(o => {
                var data = (GoodsInfo)o;
                MessagingCenter.Send(this, MESSAGE_KEY, data);
            });

            this.ShowDetailCmd = new Command(o => {
                IoC.Get<INavigationService>()
                .For<GoodsViewModel>()
                .WithParam(g => g.ID, ((GoodsInfo)o).ID)
                .Navigate();
            });
        }
    }
}
