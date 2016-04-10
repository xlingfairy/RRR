using RRExpress.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class SendViewModel : BaseVM {
        public override string Title {
            get {
                return "帮我送";
            }
        }


        public DateTime MinDate {
            get; set;
        } = DateTime.Now;

        public DateTime MaxDate {
            get; set;
        } = DateTime.Now.Date.AddDays(2);
    }
}
