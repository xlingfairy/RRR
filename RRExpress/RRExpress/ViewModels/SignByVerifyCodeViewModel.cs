using AsNum.XFControls;
using RRExpress.Attributes;
using System.Windows.Input;

namespace RRExpress.ViewModels {

    /// <summary>
    /// 确认送达, 验证码签收子视图
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class SignByVerifyCodeViewModel : BaseVM, ISelectable {
        public bool IsSelected {
            get;set;
        }

        public ICommand SelectCommand {
            get;set;
        }

        public override string Title {
            get {
                return "验证码签收";
            }
        }
    }
}
