using Caliburn.Micro;
using RRExpress.Attributes;
using RRExpress.Common;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    /// <summary>
    /// Root 框架页
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class RootViewModel : BaseVM {
        public override string Title {
            get {
                return "";
            }
        }

        public List<BaseVM> SubVMs {
            get; set;
        }

        public BaseVM CurrentVM { get; set; }

        private int _flipPos = 0;
        public int FlipPos {
            get {
                return this._flipPos;
            }
            set {
                this._flipPos = value;
                this.CurrentVM = this.SubVMs[value];
                this.NotifyOfPropertyChange(() => this.CurrentVM);
            }
        }

        public ICommand FlipPosCmd {
            get; set;
        }

        public RootViewModel(SimpleContainer container) {
            this.SubVMs = new List<BaseVM>() {
                container.GetInstance<HomeViewModel>(),
                container.GetInstance<OrderCenterViewModel>(),
                container.GetInstance<MyViewModel>()
            };
            this.CurrentVM = this.SubVMs.First();

            this.FlipPosCmd = new Command(o => {
                var pos = ((string)o).ToInt();
                this.FlipPos = pos;
                this.NotifyOfPropertyChange(() => this.FlipPos);
            });
        }
    }
}
