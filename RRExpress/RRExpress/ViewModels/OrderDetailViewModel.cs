using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ViewModels {
    public class OrderDetailViewModel : BaseVM {
        public override string Title {
            get {
                return "订单详情";
            }
        }

        public Order Data {
            get; set;
        }
    }
}
