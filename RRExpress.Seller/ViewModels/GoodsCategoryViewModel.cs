using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.Common;
using RRExpress.Seller.Entity;
using RRExpress.Seller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Seller.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class GoodsCategoryViewModel : BaseVM {
        public override string Title {
            get {
                return "选择物品种类";
            }
        }

        public event EventHandler SelectedChanged;

        private static List<GoodsCategory> Categories {
            get;
        } = new List<GoodsCategory>() {
            new GoodsCategory() { ID = 1, Name = "休闲食品", PID = 0 },
            new GoodsCategory() { ID = 2, Name = "生鲜果蔬", PID = 0 },
            new GoodsCategory() { ID = 3, Name = "办公/居家", PID = 0 },
            new GoodsCategory() { ID = 4, Name = "鲜花", PID = 0 },
            new GoodsCategory() { ID = 5, Name = "蛋糕", PID = 0 },
            new GoodsCategory() { ID = 6, Name = "其它", PID = 0 },

            new GoodsCategory() { ID = 7, Name = "家畜", PID = 1 },
            new GoodsCategory() { ID = 8, Name = "水产", PID = 1 },
            new GoodsCategory() { ID = 9, Name = "蛋", PID = 1 },
            new GoodsCategory() { ID = 10, Name = "干货", PID = 1 },

            new GoodsCategory() { ID = 11, Name = "时蔬", PID = 2 },
            new GoodsCategory() { ID = 12, Name = "水果", PID = 2 },
            new GoodsCategory() { ID = 13, Name = "干货", PID = 2 },

            new GoodsCategory() { ID = 14, Name = "家具", PID = 3 },
            new GoodsCategory() { ID = 15, Name = "文具", PID = 3 },
            new GoodsCategory() { ID = 16, Name = "其它", PID = 3 }
        };

        public IEnumerable<GoodsCategoryTreeNode> Datas {
            get;
        }

        public bool ShowSecondCategory { get; set; }

        private GoodsCategoryTreeNode _bigCat = null;
        public GoodsCategoryTreeNode BigCat {
            get {
                return this._bigCat;
            }
            set {
                if (this._bigCat == value)
                    return;

                if (this._bigCat != null) {
                    this._bigCat.IsSelected = false;
                }

                this._bigCat = value;
                if (value != null) {
                    value.IsSelected = true;
                }
                this.NotifyOfPropertyChange(() => this.BigCat);
                this.NotifyOfPropertyChange(() => this.CanShowSecondCategory);

                this.SecondCat = null;

                if (this.SelectedChanged != null)
                    this.SelectedChanged.Invoke(this, new EventArgs());
            }
        }

        private GoodsCategoryTreeNode _secondCat = null;
        public GoodsCategoryTreeNode SecondCat {
            get {
                return this._secondCat;
            }
            set {
                if (this._secondCat == value)
                    return;

                if (this._secondCat != null)
                    this._secondCat.IsSelected = false;

                this._secondCat = value;
                if (value != null)
                    value.IsSelected = true;

                if (this.SelectedChanged != null)
                    this.SelectedChanged.Invoke(this, new EventArgs());
            }
        }

        public bool CanShowSecondCategory {
            get {
                return this.ShowSecondCategory && this.BigCat?.Subs?.Count() > 0;
            }
        }

        public GoodsCategoryViewModel() {
            this.Datas = TreeNodeHelper.BuildTree<GoodsCategory, GoodsCategoryTreeNode, int>(Categories, p => p.PID, p => p.ID, 0);
        }
    }
}
