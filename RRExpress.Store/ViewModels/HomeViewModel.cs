using AsNum.XFControls;
using Caliburn.Micro;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class HomeViewModel : StoreBaseVM {
        public override char Icon {
            get {
                return (char)0xf015;
            }
        }

        public override string Title {
            get {
                return "首页";
            }
        }

        protected override bool ShowNavigationBar {
            get {
                return false;
            }
        }


        public ICommand RefreshCmd { get; }

        public ICommand LoadMoreCmd { get; }

        public Dictionary<string, string> Ads {
            get;
        } = new Dictionary<string, string>() {
            {"舌尖上的美食","http://cdnq.duitang.com/uploads/item/201205/26/20120526011428_eNCCw.jpeg" },
            {"家电下乡","http://pic1.xtuan.com/upload/image/20131210/09153193258_w.jpg" }
        };


        public List<QuickEntry> QuickEntries {
            get;
        } = new List<QuickEntry>() {
            new QuickEntry() { Name = "自营超市", Img="RRExpress.Store.Imgs.cs.png", Cmd=null },
            new QuickEntry() { Name = "水果蔬菜", Img="RRExpress.Store.Imgs.xian.png", Cmd=null },
            new QuickEntry() { Name = "农副产品", Img="RRExpress.Store.Imgs.nf.png", Cmd=null },
            new QuickEntry() { Name = "畜牧水产", Img="RRExpress.Store.Imgs.xmsc.png", Cmd=null },
            new QuickEntry() { Name = "粮油米面", Img="RRExpress.Store.Imgs.lymm.png", Cmd=null }
        };

        public List<string> Msgs { get; }
        = new List<string>() {
            "本地西瓜，新鲜上市",
            "生鲜小龙虾，限量供应",
            "散养土鸡、蛋新鲜供应",
            "洞庭湖野生鲫鱼，每日生鲜"
        };

        //public Dictionary<string, List<HomeProduct>> HomeProducts {
        //    get;
        //} = new Dictionary<string, List<HomeProduct>>() {
        //    { "今日特惠",
        //        #region
        //        new List<HomeProduct>() {
        //            new HomeProduct() {
        //                Name = "本地红薯",
        //                Price = 0.8M,
        //                OrgPrice = 1.0M,
        //                Img = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
        //            },
        //            new HomeProduct() {
        //                Name = "本地红薯",
        //                Price = 0.8M,
        //                OrgPrice = 1.0M,
        //                Img = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
        //            },
        //            new HomeProduct() {
        //                Name = "本地红薯",
        //                Price = 0.8M,
        //                OrgPrice = 1.0M,
        //                Img = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
        //            },
        //            new HomeProduct() {
        //                Name = "本地红薯",
        //                Price = 0.8M,
        //                OrgPrice = 1.0M,
        //                Img = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
        //            }
        //            #endregion
        //        }
        //    },
        //    { "推荐产品",
        //        #region
        //        new List<HomeProduct>() {
        //            new HomeProduct() {
        //                Name = "本地红薯",
        //                Price = 0.8M,
        //                OrgPrice = 1.0M,
        //                Img = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
        //            },
        //            new HomeProduct() {
        //                Name = "本地红薯",
        //                Price = 0.8M,
        //                OrgPrice = 1.0M,
        //                Img = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
        //            },
        //            new HomeProduct() {
        //                Name = "本地红薯",
        //                Price = 0.8M,
        //                OrgPrice = 1.0M,
        //                Img = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
        //            },
        //            new HomeProduct() {
        //                Name = "本地红薯",
        //                Price = 0.8M,
        //                OrgPrice = 1.0M,
        //                Img = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
        //            }
        //        }
        //        #endregion
        //        },
        //    { "新品上架",
        //        #region
        //        new List<HomeProduct>() {
        //            new HomeProduct() {
        //                Name = "本地红薯",
        //                Price = 0.8M,
        //                OrgPrice = 1.0M,
        //                Img = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
        //            },
        //            new HomeProduct() {
        //                Name = "本地红薯",
        //                Price = 0.8M,
        //                OrgPrice = 1.0M,
        //                Img = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
        //            },
        //            new HomeProduct() {
        //                Name = "本地红薯",
        //                Price = 0.8M,
        //                OrgPrice = 1.0M,
        //                Img = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
        //            },
        //            new HomeProduct() {
        //                Name = "本地红薯",
        //                Price = 0.8M,
        //                OrgPrice = 1.0M,
        //                Img = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
        //            }
        //        }
        //        #endregion
        //    }
        //};

        public IEnumerable<HomeProduct> HomeProducts {
            get;
        }

        public ICommand ShowCatlogCmd { get; }

        public HomeViewModel() {
            this.ShowCatlogCmd = new Command(async () => {
                await PopupHelper.PopupAsync<CatalogViewModel>();
            });

            this.HomeProducts = Enumerable.Range(0, 20)
                .Select(i => new HomeProduct() {
                    Name = $"本地红薯{i}",
                    Price = 0.8M,
                    OrgPrice = 1.0M,
                    Img = "http://img005.hc360.cn/g1/M05/77/65/wKhQL1Mmy3-EVxTbAAAAAG6Rdlk741.jpg"
                }
            );
        }

        public class HomeProduct {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public decimal? OrgPrice { get; set; }
            public string Group { get; set; }
            public string Img { get; set; }
        }

        public class QuickEntry {
            public string Name { get; set; }
            public ICommand Cmd { get; set; }
            public string Img { get; set; }
        }
    }
}
