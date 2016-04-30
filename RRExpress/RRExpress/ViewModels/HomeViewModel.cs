using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    /// <summary>
    /// 第一页
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class HomeViewModel : BaseVM {

        public override string Title {
            get {
                return "人人快递";
            }
        }

        public List<string> AdImgs {
            get; set;
        }

        public ICommand SendCmd { get; set; }


        private SimpleContainer Container;
        private INavigationService NS;

        public HomeViewModel(SimpleContainer container, INavigationService ns) {
            this.Container = container;
            this.NS = ns;

            this.AdImgs = new List<string>() {
                "http://www.jiaojianli.com/wp-content/uploads/2013/12/banner_send.jpg",
                "http://img1.100ye.com/img2/4/1230/627/10845627/msgpic/62872955.jpg",
                "http://static.3158.com/im/image/20140820/20140820022157_32140.jpg"
            };

            this.SendCmd = new Command(() => this.Send());
        }

        public void Send() {
            //var vm = this.Container.GetInstance<SendViewModel>();
            this.NS.NavigateToViewModelAsync<SendStep1ViewModel>();
        }
    }
}
