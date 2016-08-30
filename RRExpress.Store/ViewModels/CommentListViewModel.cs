using AsNum.XFControls;
using RRExpress.AppCommon.Attributes;
using RRExpress.Seller.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.PreRequest)]
    public class CommentListViewModel : ListBase, ISelectable {

        public override string Title {
            get {
                return "评论";
            }
        }

        public long ID { get; set; }

        public Dictionary<int, string> Groups {
            get;
        } = new Dictionary<int, string>() {
            {0, "全部(714)" },
            {1, "好评(558)" },
            {2, "中评(68)" },
            {3, "差评(88)" },
        };

        protected override Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page) {
            var rn = new Random();
            var datas = Enumerable.Range(page * 20, 20)
                .Select(i => new Comment() {
                    ID = i,
                    UserName = $"user_{i}",
                    Content = "这是评论,这只是一个评论而已",
                    CreateOn = DateTime.Now.AddDays(-rn.Next(100)),
                    Star = rn.Next(1, 5)
                });

            return Task.FromResult(new Tuple<bool, IEnumerable<object>>(false, datas));
        }


    }
}
