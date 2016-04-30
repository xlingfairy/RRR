using AsNum.XFControls;
using RRExpress.Attributes;
using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RRExpress.ViewModels {
    /// <summary>
    /// 我的发单,详情状态子页
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class MyOrderStatusViewModel : BaseVM, ISelectable {
        public bool IsSelected {
            get; set;
        }

        public ICommand SelectCommand {
            get; set;
        }

        public override string Title {
            get {
                return "订单状态";
            }
        }

        public Order Data { get; set; }
    }
}
