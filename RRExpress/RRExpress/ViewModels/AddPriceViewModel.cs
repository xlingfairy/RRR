using RRExpress.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class AddPriceViewModel : BaseVM {
        public override string Title {
            get {
                return "加价配送";
            }
        }

        public int Max { get; set; } = 50;
        public int Min { get; set; } = 0;
        public int Value { get; set; } = 0;
    }
}
