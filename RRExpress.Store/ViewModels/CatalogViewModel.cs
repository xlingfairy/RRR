using AsNum.XFControls;
using RRExpress.Common;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using System.Windows.Input;
using RRExpress.AppCommon.Models;
using System.Threading.Tasks;
using System.Reflection;
using RRExpress.Seller.Entity;

namespace RRExpress.Store.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class CatalogViewModel : BaseVM {
        public override string Title {
            get {
                return "分类";
            }
        }

        //public override ICommand SelectedCommand {
        //    get; set;
        //}

        public IEnumerable<GoodsCategoryTreeNode> Catalogs {
            get;
            private set;
        }

        //public override char Icon {
        //    get {
        //        return (char)0xf0e8;
        //    }
        //}

        public CatalogViewModel() {
            Task.Delay(500)
                .ContinueWith(async (t) => await this.LoadCats());
        }

        //protected override async Task OnSelected() {
        //    await base.OnSelected();
        //    await this.LoadCats();
        //}

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
