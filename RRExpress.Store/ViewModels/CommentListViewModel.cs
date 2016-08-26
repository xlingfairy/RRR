using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class CommentListViewModel : StoreBaseVM {
        public override char Icon {
            get {
                throw new NotImplementedException();
            }
        }

        public override string Title {
            get {
                return "评论";
            }
        }
    }
}
