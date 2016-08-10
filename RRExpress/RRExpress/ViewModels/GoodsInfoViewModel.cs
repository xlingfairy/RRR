using RRExpress.AppCommon;
using Caliburn.Micro;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class GoodsInfoViewModel : BaseVM {
        public override string Title {
            get {
                return "物品信息";
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

                this.NotifyOfPropertyChange("Selected");
            }
        }

        public GoodsInfoViewModel() {
            this.Datas = new List<Tmp>() {
                new Tmp() { Title = "休闲食品"},
                new Tmp() { Title = "生鲜果蔬"},
                new Tmp() { Title = "办公/居家"},
                new Tmp() { Title = "鲜花"},
                new Tmp() { Title = "蛋糕"},
                new Tmp() { Title = "大件物品"},
                new Tmp() { Title = "其它"}
            };

            this.Selected = this.Datas.First();
        }


        public class Tmp : PropertyChangedBase {
            public string Title { get; set; }
            public bool Checked { get; set; }
        }
    }
}
