using Caliburn.Micro;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ImageViewModel : BaseVM {
        public override string Title {
            get {
                return "图片浏览";
            }
        }

        private IEnumerable<string> _datas;
        public IEnumerable<string> Datas {
            get {
                return this._datas;
            }
            set {
                this._datas = value;
                this.NotifyOfPropertyChange(() => this.Datas);
            }
        }


    }
}
