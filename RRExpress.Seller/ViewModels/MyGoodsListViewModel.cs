using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.Seller.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Seller.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class MyGoodsListViewModel : ListBase {
        public override string Title {
            get {
                return "我的商品列表";
            }
        }

        public IEnumerable<GoodsInfo> _Datas {
            get; set;
        } = new List<GoodsInfo>() {
                new GoodsInfo() { ID = 1, BigCat = 2, Name = "小土豆，纯天然无污染", Price=0.5M, Unit="斤", Stock = null, IsPublished = true , CreatedOn = DateTime.Now.AddDays(-10), SaleVolumeByMonth=535, CommentCount=356, PraiseRate = 94.5F , Thumbnail="http://pic2.nipic.com/20090413/2192791_173824063_2.jpg" , Desc ="自家种小土豆"},
                new GoodsInfo() { ID = 1, BigCat = 2, Name = "红薯", Price=0.8M, Unit="斤", Stock = null, IsPublished = true , CreatedOn = DateTime.Now.AddDays(-10), SaleVolumeByMonth=364, CommentCount=265, PraiseRate = 90.5F, Thumbnail="http://d02.res.meilishuo.net/picdetail/a/b9/77/060fcaea0f3688cb9f6f8b177d75_620_462_1_1.c7.jpeg" , Desc = "老头美"},
                new GoodsInfo() { ID = 1, BigCat = 2, Name = "大蒜", Price=1.5M, Unit="斤", Stock = null, IsPublished = true , CreatedOn = DateTime.Now.AddDays(-10), SaleVolumeByMonth=189, CommentCount=156, PraiseRate = 95.7F, Thumbnail="http://pic30.nipic.com/20130623/11984891_173304367135_2.jpg", Desc="红蒜" },
                new GoodsInfo() { ID = 1, BigCat = 2, Name = "家鸡蛋，谷物喂养，园林放养，绿色食品", Price=4.5M, Unit="斤", Stock = null, IsPublished = true , CreatedOn = DateTime.Now.AddDays(-10), SaleVolumeByMonth=130, CommentCount=98, PraiseRate = 92.5F, Thumbnail="http://img3.114pifa.com/5553/kVT0LQidx_1401381950.jpg" ,Desc = "家养土鸡蛋,个头不大"},
                new GoodsInfo() { ID = 1, BigCat = 2, Name = "家鸭蛋", Price=5.5M, Unit="斤", Stock = null, IsPublished = true , CreatedOn = DateTime.Now.AddDays(-10), SaleVolumeByMonth=64, CommentCount=48, PraiseRate = 91.6F, Thumbnail="http://imgsrc.baidu.com/baike/pic/item/d788d43f8794a4c2518276480ff41bd5ad6e392b.jpg" , Desc="散养老鸭，红心蛋"},
                new GoodsInfo() { ID = 1, BigCat = 2, Name = "老母鸡，2年以上老母鸡，园林放养，谷物喂养", Price=18M, Unit="斤", Stock = null, IsPublished = true , CreatedOn = DateTime.Now.AddDays(-10), SaleVolumeByMonth=57, CommentCount=36, PraiseRate = 98.5F, Thumbnail="http://img12.360buyimg.com/n0/jfs/t826/241/782271093/373096/bf6330df/55530672Nc62530b2.jpg" , Desc="散养，2年以上"},
        };

        protected override Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page) {
            var rst = new Tuple<bool, IEnumerable<object>>(false, this._Datas);
            return Task.FromResult(rst);
        }

        protected async override void OnActivate() {
            base.OnActivate();

            if (this.Datas == null || this.Datas.Count == 0) {
                await Task.Delay(500)
                    .ContinueWith(async t => {
                        await this.LoadData(true);
                    });
            }
        }
    }
}
