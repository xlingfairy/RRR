using Caliburn.Micro.Xamarin.Forms;
using Microsoft.International.Converters.PinYinConverter;
using RRExpress.Attributes;
using RRExpress.Common;
using RRExpress.Models;
using RRExpress.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ContacterViewModel : BaseVM {
        public override string Title {
            get {
                return "选择联系人";
            }
        }

        public static readonly string MESSAGE_KEY = "CHOICE_CONTACTER";

        public IEnumerable<Grouped<Contacter>> Datas { get; set; }

        public Contacter Selected { get; set; }

        public ICommand ChoiceCmd { get; }

        public ContacterViewModel(INavigationService ns) {
            this.ChoiceCmd = new Command(() => {
                MessagingCenter.Send(this, MESSAGE_KEY, this.Selected);
                ns.GoBackAsync();
            });
        }

        protected override void OnActivate() {
            base.OnActivate();

            Task.Delay(500)
                .ContinueWith(t => {
                    this.LoadData();
                });
        }

        private async void LoadData() {

            //https://github.com/jamesmontemagno/Xamarin.Plugins/tree/master/Contacts
            //CrossContacts.Current.PreferContactAggregation = false;//recommended

            //if (CrossContacts.Current.Contacts != null) {
            //    var contacts = CrossContacts.Current.Contacts
            //      .Where(c => c.Phones.Count > 0)
            //      .OrderBy(c => c.DisplayName);
            //}

            this.IsBusy = true;
            var datas = await DependencyService.Get<IAddressBook>()
                .GetContactors();


            this.Datas = datas.ToGroup(c => this.GetFirstChar(c.Name))
                .OrderBy(g => g.Title);
            this.NotifyOfPropertyChange(() => this.Datas);
            this.IsBusy = false;
        }

        private char GetFirstChar(string str) {
            var c = str.ToUpper()[0];
            var cs = c.ToString();

            if (cs.ToIntOrNull().HasValue) {
                return '#';
            } else if (c >= 'A' && c <= 'Z') {
                return c;
            } else if (ChineseChar.IsValidChar(c)) {
                var cc = new ChineseChar(c);
                return cc.Pinyins.First()[0];
            } else {
                return '#';
            }
        }
    }
}
