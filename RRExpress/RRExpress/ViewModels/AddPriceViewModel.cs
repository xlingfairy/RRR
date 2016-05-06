using RRExpress.Attributes;

namespace RRExpress.ViewModels {

    [Regist(InstanceMode.Singleton)]
    public class AddPriceViewModel : BaseVM {
        public override string Title {
            get {
                return "加价配送";
            }
        }

        /// <summary>
        /// 最大加价
        /// </summary>
        public int Max { get; set; } = 50;

        /// <summary>
        /// 最小加价
        /// </summary>
        public int Min { get; set; } = 0;

        private int _value = 0;
        /// <summary>
        /// 选择的加价
        /// </summary>
        public int Value {
            get {
                return this._value;
            }
            set {
                this._value = value;
                this.NotifyOfPropertyChange(() => this.Value);
            }
        }
    }
}
