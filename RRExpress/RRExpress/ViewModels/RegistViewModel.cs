using Caliburn.Micro.Xamarin.Forms;
using RRExpress.Api.V1.Methods;
using RRExpress.AppCommon;
using RRExpress.AppCommon.Attributes;
using RRExpress.AppCommon.Services;
using RRExpress.Service.Entity;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class RegistViewModel : BaseVM {
        public override string Title {
            get {
                return "用户注册";
            }
        }

        public ICommand GetCodeCmd { get; set; }
        public ICommand RegistCmd { get; set; }

        private string _phone;
        public string Phone {
            get {
                return this._phone;
            }
            set {
                this._phone = value;
                this.NotifyOfPropertyChange(() => this.Phone);
            }
        }

        private string _pwd;
        public string Pwd {
            get {
                return this._pwd;
            }
            set {
                this._pwd = value;
                this.NotifyOfPropertyChange(() => this.Pwd);
            }
        }

        private string _confirmPwd;
        public string ConfirmPwd {
            get {
                return this._confirmPwd;
            }
            set {
                this._confirmPwd = value;
                this.NotifyOfPropertyChange(() => this.ConfirmPwd);
            }
        }

        private string _code;
        public string Code {
            get {
                return this._code;
            }
            set {
                this._code = value;
                this.NotifyOfPropertyChange(() => this.Code);
            }
        }

        private bool _isAccept = true;
        public bool IsAccept {
            get {
                return this._isAccept;
            }
            set {
                this._isAccept = value;
                this.NotifyOfPropertyChange(() => this.IsAccept);
            }
        }

        public string GetCodeText {
            get; set;
        } = "获取验证码";

        private int _timeWait;
        public int TimeWait {
            get {
                return this._timeWait;
            }
            set {
                this._timeWait = value;
                this.NotifyOfPropertyChange(() => this.TimeWait);
            }
        }

        public bool CanGetCode {
            get {
                return !string.IsNullOrWhiteSpace(this.Phone)
                    && this.TimeWait == 0
                    && !this.IsBusy;
            }
        }

        public bool CanRegist {
            get {
                return !string.IsNullOrWhiteSpace(this.Phone)
                    && !string.IsNullOrWhiteSpace(this.Code)
                    && !string.IsNullOrWhiteSpace(this.Pwd)
                    && !string.IsNullOrWhiteSpace(this.ConfirmPwd)
                    && this.IsAccept
                    && !this.IsBusy;
            }
        }


        private INavigationService NS;
        private string DeviceID = "";

        public RegistViewModel(INavigationService ns) {
            this.NS = ns;

            this.GetCodeCmd = new Command(() => {
                this.TimeWait = 60;
                this.ChangeGetCodeText();
            });

            this.RegistCmd = new Command(() => this.Regist());
            this.PropertyChanged += RegistViewModel_PropertyChanged;

            var device = DependencyService.Get<IDevice>();
            this.Phone = device.GetPhoneNumber();
            this.DeviceID = device.GetDeviceID();
        }

        private void ChangeGetCodeText() {
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                if (this.TimeWait > 0) {
                    this.GetCodeText = $"{--this.TimeWait}";
                    this.NotifyOfPropertyChange(() => this.GetCodeText);
                    return true;
                } else {
                    this.GetCodeText = "获取验证码";
                    this.NotifyOfPropertyChange(() => this.GetCodeText);
                    return false;
                }
            });
        }

        private void RegistViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                case nameof(this.Phone):
                case nameof(this.TimeWait):
                case nameof(this.IsBusy):
                    this.NotifyOfPropertyChange(() => this.CanGetCode);
                    this.NotifyOfPropertyChange(() => this.CanRegist);
                    break;
                case nameof(this.Code):
                case nameof(this.Pwd):
                case nameof(this.ConfirmPwd):
                case nameof(this.IsAccept):
                    this.NotifyOfPropertyChange(() => this.CanRegist);
                    break;
            }
        }


        private async void Regist() {
            this.IsBusy = true;

            var mth = new Regist() {
                Info = new RegistInfo() {
                    Phone = this.Phone,
                    Pwd = this.Pwd,
                    ConfirmPwd = this.ConfirmPwd,
                    Code = this.Code,
                    DeviceID = this.DeviceID
                }
            };

            await Task.Delay(5000);

            var result = await ApiClient.ApiClient.Instance.Value.Execute(mth);
            if (!mth.HasError) {
                await App.Current.MainPage.DisplayAlert("消息", result.Msg, "OK");
                if (result.IsSuccess)
                    await this.NS.GoBackAsync();
            }

            this.IsBusy = false;
        }
    }
}
