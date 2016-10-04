using AsNum.XFControls;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.PreRequest)]
    public class GoodsInfoViewModel : BaseVM, ISelectable {
        public override string Title {
            get {
                return "详情";
            }
        }

        public long ID { get; set; }

        public Dictionary<string, string> Imgs {
            get;
        } = new Dictionary<string, string>() {
            {"http://img1.ph.126.net/hZ35wHe4KwqgPbh-hkONGQ==/723390690246397027.jpg","散养家鸡" },
            {"http://image.99114.com/2009/12/25/5ea77b1a2c8e40eab8ea938c69e77ba9.jpg","绿色安全" },
            {"http://www.tjyj.org/uploads/allimg/130125/1_0011083762.jpg","自由生态" },
            {"http://htdz.7015.cn/uploadfile/2016/0201/20160201030414712.jpg","描述" }
        };

        #region ISelectable
        public bool IsSelected {
            get; set;
        }

        public ICommand SelectedCommand {
            get; set;
        }

        public ICommand UnSelectedCommand {
            get; set;
        }
        #endregion

        public ICommand ShowFullImgCmd { get; }

        public GoodsInfoViewModel() {
            this.ShowFullImgCmd = new Command(() => {
                IoC.Get<INavigationService>()
                    .For<ImageViewModel>()
                    .WithParam(vm => vm.Datas, this.Imgs.Keys)
                    .Navigate();
            });
        }
    }
}
