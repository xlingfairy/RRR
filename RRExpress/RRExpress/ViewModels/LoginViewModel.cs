using RRExpress.Api.V1.Methods;
using RRExpress.Attributes;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class LoginViewModel : BaseVM {
        public override string Title {
            get {
                return "登陆";
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

        public LoginViewModel() {

            this.LoginCmd = new Command(async () => {
                await this.Login();
            });

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
                ((App)App.Current).ShowRootView();
            }
            this.IsBusy = false;
            this.NotifyOfPropertyChange(() => this.CanLogin);
        }
    }
}
