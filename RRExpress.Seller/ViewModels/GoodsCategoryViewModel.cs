using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.AppCommon.Models;
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

        public IEnumerable<GoodsCategoryTreeNode> Datas {
            get;
            private set;
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
            this.LoadCats();
        }

        private async void LoadCats() {
            var datas = await GoodsCatalogHelper.GetAll();
            this.Datas = datas;
            this.NotifyOfPropertyChange(() => this.Datas);
        }
    }
}
