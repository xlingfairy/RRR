using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class GoodsDetailViewModel : BaseVM {
        public override string Title {
            get {
                return "商品详情";
            }
        }


        public int ID {
            get;set;
        }

        public Dictionary<string, string> Imgs {
            get;
        } = new Dictionary<string, string>() {
            {"http://img1.ph.126.net/hZ35wHe4KwqgPbh-hkONGQ==/723390690246397027.jpg","散养家鸡" },
            {"http://image.99114.com/2009/12/25/5ea77b1a2c8e40eab8ea938c69e77ba9.jpg","绿色安全" },
            {"http://www.tjyj.org/uploads/allimg/130125/1_0011083762.jpg","自由生态" },
            {"http://htdz.7015.cn/uploadfile/2016/0201/20160201030414712.jpg","描述" }
        };


        protected override void OnActivate() {
            base.OnActivate();
        }
    }
}
