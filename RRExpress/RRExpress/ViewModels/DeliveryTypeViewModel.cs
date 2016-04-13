using RRExpress.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ViewModels {
    [Regist(InstanceMode.Singleton)]
    public class DeliveryTypeViewModel : BaseVM {
        public override string Title {
            get {
                return "配送工具";
            }
        }

        public List<Tmp> Datas {
            get;
        }

        public string Selected {
            get; set;
        }

        public DeliveryTypeViewModel() {
            //"不限", "电瓶车", "驾车"
            this.Datas = new List<Tmp>() {
                new Tmp() { Title = "不限", Img="unlimited", Selected=true },
                new Tmp() { Title = "自行车", Img="bike", Selected=false },
                new Tmp() { Title = "电瓶车", Img="tram", Selected=false },
                new Tmp() { Title = "驾车", Img="car", Selected=false },
            };
        }

        public class Tmp {
            public string Title { get; set; }
            public string Img { get; set; }
            public bool Selected { get; set; }
        }
    }
}
