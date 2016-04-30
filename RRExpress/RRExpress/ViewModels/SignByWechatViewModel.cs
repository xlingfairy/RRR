using AsNum.XFControls;
using RRExpress.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RRExpress.ViewModels {

    /// <summary>
    /// 确认送达, 微信签收签收子视图
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class SignByWechatViewModel : BaseVM, ISelectable {
        public bool IsSelected {
            get; set;
        }

        public ICommand SelectCommand {
            get; set;
        }

        public override string Title {
            get {
                return "微信扫描签收";
            }
        }
    }
}
