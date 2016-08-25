using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class GoodsListViewModel : ListBase {
        public override string Title {
            get {
                return "商品列表";
            }
        }

        public Xamarin.Forms.View HeaderView { get; set; }

        protected override Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page) {
            throw new NotImplementedException();
        }
    }
}
