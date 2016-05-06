using RRExpress.Attributes;
using RRExpress.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class PickupTimeViewModel : BaseVM {

        public static readonly string MESSAGE_KEY = "PICKUPTIME";

        public override string Title {
            get {
                return "收货时间选择";
            }
        }

        public List<PickupTime> Datas { get; set; }

        private PickupTime _selected = null;
        public PickupTime Selected {
            get {
                return this._selected;
            }
            set {
                this._selected = value;
                MessagingCenter.Send(this, MESSAGE_KEY, value);
            }
        }

        public PickupTimeViewModel() {
            Task.Run(() => {
                this.LoadData();
            });
        }

        public void LoadData() {
            var begin = DateTime.Now.Date;

            //3天,每天48个半小时
            var tmp = Enumerable.Range(0, 3 * 48)
                .Select(i => begin.AddMinutes(i * 30))
                .Where(d => d > DateTime.Now && d.Hour >= 6 && d.Hour < 20)
                .GroupBy(d => this.GetLabel(d.Date))
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(t => new PickupTime() {
                        Label = t.ToString("yyyy/MM/dd HH:mm"),
                        Time = t
                    })
                    )
                .Select(d => new PickupTime() {
                    Label = d.Key,
                    Times = d.Value
                });

            this.Datas = tmp.ToList();
            this.Datas.Insert(0, new PickupTime() {
                Label = "立即",
                Times = new List<PickupTime>() {
                    new PickupTime() {
                        Label = "立即",
                        Time = DateTime.Now
                    }
                }
            });
            this.NotifyOfPropertyChange(() => this.Datas);
        }

        private string GetLabel(DateTime d) {
            var c = (d.Date - DateTime.Now.Date).Days;

            if (c == 0)
                return "今天";
            else if (c == 1)
                return "明天";
            else if (c == 2)
                return "后天";
            else
                return "N/A";
        }
    }
}
