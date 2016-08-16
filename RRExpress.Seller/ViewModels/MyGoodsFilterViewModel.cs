using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.Seller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        } = Const.CategoriesTrees;

        public GoodsCategoryTreeNode Cat { get; set; }

        public IEnumerable<string> SortTypes {
            get;
        } = new List<string>() { "不限", "时间", "库存" };

        public string SortType { get; set; }

        public MyGoodsFilterViewModel() {
            var cats = Const.CategoriesTrees.ToList();
            cats.Insert(0, new GoodsCategoryTreeNode() {
                ID = -1,
                PID = -1,
                Data = new Entity.GoodsCategory() {
                    Name = "不限"
                }
            });
            this.Categories = cats;
        }
    }
}
