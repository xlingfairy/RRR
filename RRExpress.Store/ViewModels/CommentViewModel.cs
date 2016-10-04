using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Store.ViewModels {

    [Regist(InstanceMode.PreRequest)]
    public class CommentViewModel : BaseVM {

        public override string Title {
            get {
                return "发表评价";
            }
        }

        public string OrderNO { get; set; }

        public ICommand RecommendCmd { get; }

        public int Rate1 { get; set; } = 4;

        public CommentViewModel() {
            this.RecommendCmd = new Command(() => {
                IoC.Get<INavigationService>()
                .For<RecommendViewModel>()
                .WithParam(v => v.OrderNO, this.OrderNO)
                .Navigate();
            });
        }
    }
}
