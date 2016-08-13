using RRExpress.Common;
using RRExpress.Seller.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace RRExpress.Seller.Models {

    public class GoodsCategoryTreeNode : TreeNode<GoodsCategory, GoodsCategoryTreeNode, int> {
        public bool IsSelected { get; set; }
    }

    //public class GoodsCategoryTree : GoodsCategory {

    //    public bool IsSelected {
    //        get; set;
    //    }

    //    public GoodsCategory Parent { get; set; }

    //    public Expression<Func<int>> ParentID {
    //        get {
    //            return () => this.ID;
    //        }
    //    }

    //    public IEnumerable<GoodsCategoryTree> Subs { get; set; }


    //    public static IEnumerable<GoodsCategoryTree> Build(IEnumerable<GoodsCategory> datas, int? pid = null) {
    //        if (datas == null)
    //            return null;

    //        return datas.Where(d => d.PID == pid)
    //                    .Select(d => new GoodsCategoryTree() {
    //                        ID = d.ID,
    //                        PID = pid,
    //                        Parent = datas.FirstOrDefault(dd => dd.ID == pid),
    //                        Name = d.Name,
    //                        Subs = Build(datas, d.ID)
    //                    });
    //    }
    //}
}
