using Caliburn.Micro;
using RRExpress.Attributes;
using RRExpress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

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

        public int FlipPos {
            get; set;
        }

        public ICommand FlipPosCmd {
            get; set;
        }

        public RootViewModel(SimpleContainer container) {
            this.SubVMs = new List<BaseVM>() {
                container.GetInstance<HomeViewModel>(),
                container.GetInstance<GetJobViewModel>(),
                container.GetInstance<MyViewModel>()
            };

            this.FlipPosCmd = new Command(o => {
                var pos = ((string)o).ToInt();
                this.FlipPos = pos;
                this.NotifyOfPropertyChange(() => this.FlipPos);
            });
        }
    }
}
