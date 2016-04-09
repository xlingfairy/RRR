using Caliburn.Micro;
using RRExpress.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public RootViewModel(SimpleContainer container) {
            this.SubVMs = new List<BaseVM>() {
                container.GetInstance<HomeViewModel>(),
                container.GetInstance<GetJobViewModel>(),
                container.GetInstance<MyViewModel>()
            };
        }
    }
}
