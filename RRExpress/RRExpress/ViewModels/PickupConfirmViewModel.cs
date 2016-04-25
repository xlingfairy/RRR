using RRExpress.Attributes;
using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class PickupConfirmViewModel : BaseVM {
        public override string Title {
            get {
                return "确认取货";
            }
        }

        public Order Data { get; set; }


    }
}
