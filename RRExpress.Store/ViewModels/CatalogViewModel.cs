using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.AppCommon.Models;
using RRExpress.Common;
using RRExpress.Seller.Entity;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Store.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class CatalogViewModel : BaseVM {
        public override string Title {
            get {
                return "分类";
            }
        }

        public IEnumerable<GoodsCategoryTreeNode> Catalogs {
            get;
            private set;
        }

        public ICommand ChoiceCatalogCmd { get; }

        public CatalogViewModel() {
            this.ChoiceCatalogCmd = new Command((o) => {
                var cat = (GoodsCategoryTreeNode)o;
            });

            Task.Delay(100)
                .ContinueWith(async (t) => await this.LoadCats());
        }


        private async Task LoadCats() {
            this.IsBusy = true;
            if (this.Catalogs != null)
                return;

            var datas = await ResJsonReader.GetAll<IEnumerable<GoodsCategory>>(this.GetType().GetTypeInfo().Assembly, "RRExpress.Store.Cats.json");
            this.Catalogs = datas.BuildTree<GoodsCategory, GoodsCategoryTreeNode, int>
                              (c => c.PID, c => c.ID, 0);

            this.NotifyOfPropertyChange(() => this.Catalogs);
            this.IsBusy = false;
        }
    }
}
