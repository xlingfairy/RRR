using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Seller.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class OrderDetailViewModel : BaseVM {

        public override string Title {
            get {
                return "订单详情";
            }
        }


    }
}
