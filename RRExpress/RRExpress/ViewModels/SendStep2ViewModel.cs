using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Attributes;
using RRExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    /// <summary>
    /// 帮我送,第二步
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class SendStep2ViewModel : BaseVM {
        public override string Title {
            get {
                return "帮我送";
            }
        }

        public ICommand ShowContacterCmd {
            get;
        }

        public ICommand ShowAddPriceCmd { get; }

        public Contacter Sender { get; set; }

        public Contacter Receiver { get; set; }

        private string ContacterTag = null;

        public AddPriceViewModel AddPriceVM { get; }

        public SendStep2ViewModel(SimpleContainer container, INavigationService ns) {

            this.AddPriceVM = container.GetInstance<AddPriceViewModel>();

            this.ShowContacterCmd = new Command((o) => {
                this.ContacterTag = (string)o;
                ns.NavigateToViewModelAsync<ContacterViewModel>();
            });

            this.ShowAddPriceCmd = new Command(async () => {
                await PopupHelper.PopupAsync(this.AddPriceVM);
            });

            MessagingCenter.Subscribe<ContacterViewModel, Contacter>(this, ContacterViewModel.MESSAGE_KEY, (sender, contacter) => {
                switch (this.ContacterTag) {
                    case "S":
                        this.Sender = contacter;
                        this.NotifyOfPropertyChange(() => this.Sender);
                        break;
                    case "R":
                        this.Receiver = contacter;
                        this.NotifyOfPropertyChange(() => this.Receiver);
                        break;
                }
            });
        }
    }
}
