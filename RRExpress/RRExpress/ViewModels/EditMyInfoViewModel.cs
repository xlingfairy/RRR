using RRExpress.Attributes;
using System.Collections.Generic;

namespace RRExpress.ViewModels {

    /// <summary>
    /// 个人信息修改页
    /// </summary>
    [Regist(InstanceMode.Singleton)]
    public class EditMyInfoViewModel : BaseVM {
        public override string Title {
            get {
                return "个人信息修改";
            }
        }


        public List<string> Sexs {
            get;
        }

        public string Sex { get; set; }

        public EditMyInfoViewModel() {
            this.Sexs = new List<string>() {
                "男","女","保密"
            };
        }
    }
}
