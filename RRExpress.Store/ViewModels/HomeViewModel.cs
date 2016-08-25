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

        public ICommand ShowCatlogCmd { get; }

        public GoodsListViewModel GoodsVM { get; }

        public HomeViewModel() {
            this.GoodsVM = IoC.Get<GoodsListViewModel>();

            this.ShowCatlogCmd = new Command(async () => {
                await PopupHelper.PopupAsync<CatalogViewModel>();
            });
        }


        protected override Task OnSelected() {
            return base.OnSelected()
                .ContinueWith(async (t)=> {
                    await this.GoodsVM.FirstLoad();
                });
        }

        public class QuickEntry {
            public string Name { get; set; }
            public ICommand Cmd { get; set; }
            public string Img { get; set; }
        }
    }
}
