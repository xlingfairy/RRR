using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.AppCommon.Models;
using RRExpress.Common;
using RRExpress.Seller.Entity;
using RRExpress.Seller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Seller.ViewModels {


    [Regist(InstanceMode.Singleton)]
    public class MyGoodsFilterViewModel : BaseVM {
        public override string Title {
            get {
                return "过滤条件";
            }
        }

        public IEnumerable<GoodsCategoryTreeNode> Categories {
            get;
            private set;
        }

        public GoodsCategoryTreeNode Cat { get; set; }

        public IEnumerable<string> SortTypes {
            get;
        } = new List<string>() { "不限", "时间", "库存" };

        public string SortType { get; set; }


        public ICommand ResetCmd { get; }
        public ICommand OkCmd { get; }

        public MyGoodsFilterViewModel() {
            this.LoadCats();

            this.ResetCmd = new Command(() => {

            });

            this.OkCmd = new Command(() => { });
        }

        private async void LoadCats() {
            var datas = await ResJsonReader.GetAll<IEnumerable<GoodsCategory>>(this.GetType().GetTypeInfo().Assembly, "RRExpress.Seller.Cats.json");
            var nodes = datas.BuildTree<GoodsCategory, GoodsCategoryTreeNode, int>(c => c.PID, c => c.ID, 0);
            var cats = nodes.ToList();
            cats.Insert(0, new GoodsCategoryTreeNode() {
                ID = -1,
                PID = -1,
                Data = new Entity.GoodsCategory() {
                    Name = "不限"
                }
            });
            this.Categories = cats;
            this.NotifyOfPropertyChange(() => this.Categories);
        }
    }
}
