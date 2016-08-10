using RRExpress.AppCommon;
using Caliburn.Micro.Xamarin.Forms;
using Microsoft.International.Converters.PinYinConverter;
using RRExpress.AppCommon.Attributes;
using RRExpress.AppCommon.Models;
using RRExpress.AppCommon.Services;
using RRExpress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.Express.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class ContacterViewModel : BaseVM {
        public override string Title {
            get {
                return "选择联系人";
            }
        }

        public static readonly string MESSAGE_KEY = "CHOICE_CONTACTER";

        private IEnumerable<Contacter> datas = null;
        public IEnumerable<Grouped<Contacter>> Datas { get; set; }


        private string _filter = null;
        public string Filter {
            get {
                return this._filter;
            }
            set {
                this._filter = value;
                this.LoadData(value);
            }
        }

        /// <summary>
        /// 选中的联系人
        /// </summary>
        public Contacter Selected { get; set; }


        /// <summary>
        /// 选中命令
        /// </summary>
        public ICommand ChoiceCmd { get; }

        /// <summary>
        /// 联系人列表是否已加载
        /// </summary>
        private bool HasLoaded = false;

        public ContacterViewModel(INavigationService ns) {
            this.ChoiceCmd = new Command(() => {
                MessagingCenter.Send(this, MESSAGE_KEY, this.Selected);
                ns.GoBackAsync();
            });
        }

        protected override void OnActivate() {
            base.OnActivate();

            if (!this.HasLoaded)
                Task.Delay(500)
                    .ContinueWith(t => {
                        this.LoadData();
                    });
        }

        private async void LoadData(string filter = null) {

            //https://github.com/jamesmontemagno/Xamarin.Plugins/tree/master/Contacts
            //CrossContacts.Current.PreferContactAggregation = false;//recommended

            //if (CrossContacts.Current.Contacts != null) {
            //    var contacts = CrossContacts.Current.Contacts
            //      .Where(c => c.Phones.Count > 0)
            //      .OrderBy(c => c.DisplayName);
            //}

            this.IsBusy = true;

            if (this.datas == null || this.datas.Count() == 0) {
                this.datas = await DependencyService.Get<IAddressBook>()
                    .GetContactors();
            }

            if (!string.IsNullOrWhiteSpace(filter)) {
                filter = filter.Trim();

                this.Datas = this.datas
                    .Where(c =>
                        c.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) > -1 ||
                        c.Phone.IndexOf(filter) > -1
                    )
                    .ToGroup(c => this.GetFirstChar(c.Name))
                    .OrderBy(g => g.Title);

            } else {
                this.Datas = this.datas.ToGroup(c => this.GetFirstChar(c.Name))
                    .OrderBy(g => g.Title);
            }

            this.NotifyOfPropertyChange(() => this.Datas);
            this.IsBusy = false;

            this.HasLoaded = true;
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
