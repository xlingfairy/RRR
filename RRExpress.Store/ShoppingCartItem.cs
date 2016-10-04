using Caliburn.Micro;
using RRExpress.Seller.Entity;
using System.Runtime.CompilerServices;

namespace RRExpress.Store {
    public class ShoppingCartItem : PropertyChangedBase {
        private bool _checked;
        public bool Checked {
            get {
                return this._checked;
            }
            set {
                this._checked = value;
                this.NotifyOfPropertyChange(() => this._checked);
            }
        }

        private uint _count;
        public uint Count {
            get {
                return this._count;
            }
            set {
                this._count = value;
                this.NotifyOfPropertyChange(() => this._count);
            }
        }

        public GoodsInfo Data { get; set; }

        public decimal Amount {
            get {
                return this.Data.Price * this.Count;
            }
        }

        public override void NotifyOfPropertyChange([CallerMemberName] string propertyName = null) {
            base.NotifyOfPropertyChange(propertyName);
        }
    }
}
