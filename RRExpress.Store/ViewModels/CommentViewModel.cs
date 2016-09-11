using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.PreRequest)]
    public class CommentViewModel : BaseVM {

        public override string Title {
            get {
                return "发表评价";
            }
        }

        public string OrderNO { get; set; }
    }
}
