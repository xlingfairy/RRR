using AsNum.XFControls;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class HomeViewModel : StoreBaseVM {
        public override char Icon {
            get {
                return (char)0xf015;
            }
        }

        public override ICommand SelectCommand {
            get; set;
        }

        public override string Title {
            get {
                return "首页";
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

        public class QuickEntry {
            public string Name { get; set; }
            public ICommand Cmd { get; set; }
            public string Img { get; set; }
        }
    }
}
