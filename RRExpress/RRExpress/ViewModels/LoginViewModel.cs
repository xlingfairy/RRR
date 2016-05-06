using RRExpress.Attributes;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class LoginViewModel : BaseVM {
        public override string Title {
            get {
                return "登陆";
            }
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public void Login() {

        }


    }
}
