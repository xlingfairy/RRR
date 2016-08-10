using RRExpress.AppCommon;
using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Api.V1.Methods;
using RRExpress.AppCommon.Attributes;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class LoginViewModel : BaseVM {
        public override string Title {
            get {
                return "用户登陆";
            }
        }

        private string _userName;
        public string UserName {
            get {
                return this._userName;
            }
            set {
                this._userName = value;
                this.NotifyOfPropertyChange(() => this.CanLogin);
            }
        }

        private string _password;
        public string Password {
            get {
                return this._password;
            }
            set {
                this._password = value;
                this.NotifyOfPropertyChange(() => this.CanLogin);
            }
        }



        public bool CanLogin {
            get {
                return !string.IsNullOrWhiteSpace(this.UserName)
                    && !string.IsNullOrWhiteSpace(this.Password)
                    && !this.IsBusy;
            }
        }


        public ICommand LoginCmd { get; }
        public ICommand RegistCmd { get; }
        public ICommand ForgetPwdCmd { get; }

        private INavigationService NS = null;

        public LoginViewModel(INavigationService ns) {

            this.LoginCmd = new Command(async () => {
                await this.Login();
            });

            this.RegistCmd = new Command(async () => {
                await this.NS.NavigateToViewModelAsync<RegistViewModel>();
            });

            this.ForgetPwdCmd = new Command(async () => {
                await this.NS.NavigateToViewModelAsync<ForgetPwdViewModel>();
            });

            this.NS = ns;

            this.UserName = PropertiesHelper.Get<string>("UserName");
            this.NotifyOfPropertyChange(() => this.UserName);
        }

        public async Task Login() {
            this.IsBusy = true;
            this.NotifyOfPropertyChange(() => this.CanLogin);
            var mth = new Auth() {
                UserName = this.UserName.Trim(),
                Password = this.Password.Trim()
            };
            var token = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            if (!mth.HasError && token != null && token.IsValid) {
                //await this.NS.GoBackAsync();
                ((App)App.Current).ShowRootView();
                PropertiesHelper.Set("UserName", this.UserName);
                await PropertiesHelper.Save();
            }
            this.IsBusy = false;
            this.NotifyOfPropertyChange(() => this.CanLogin);
        }
    }
}
