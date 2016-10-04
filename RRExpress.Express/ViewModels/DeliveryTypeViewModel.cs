using Caliburn.Micro;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace RRExpress.Express.ViewModels {

    /// <summary>
    /// 配送工具选择
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class DeliveryTypeViewModel : BaseVM {
        public override string Title {
            get {
                return "配送工具";
            }
        }

        public List<Tmp> Datas {
            get;
        }

        private Tmp _selected = null;
        public Tmp Selected {
            get {
                return this._selected;
            }
            set {
                if (this._selected != null) {
                    this._selected.Checked = false;
                    this.Selected.NotifyOfPropertyChange("Checked");
                }
                this._selected = value;
                if (value != null) {
                    value.Checked = true;
                    this.Selected.NotifyOfPropertyChange("Checked");
                }

                //慢
                //this.NotifyOfPropertyChange(() => this.Selected);
                //快
                this.NotifyOfPropertyChange("Selected");
            }
        }



        public DeliveryTypeViewModel() {
            //"不限", "电瓶车", "驾车"
            this.Datas = new List<Tmp>() {
                new Tmp() { Title = "不限", Img="unlimited"},
                new Tmp() { Title = "自行车", Img="bike"},
                new Tmp() { Title = "电瓶车", Img="tram"},
                new Tmp() { Title = "驾车", Img="car"},
            };

            this.Selected = this.Datas.First();
        }

        public class Tmp : PropertyChangedBase {
            public string Title { get; set; }
            public string Img { get; set; }
            public bool Checked { get; set; }
        }
    }
}
