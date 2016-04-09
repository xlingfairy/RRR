using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress {

    /// <summary>
    /// Base class of ViewModel
    /// </summary>
    public abstract class BaseVM : Screen {

        public abstract string Title {
            get;
        }

        private bool isBusy = false;
        /// <summary>
        /// Indicate currently whether this model is in processing.
        /// </summary>
        public bool IsBusy {
            get {
                return this.isBusy;
            }
            set {
                this.isBusy = value;
                this.NotifyOfPropertyChange(() => this.IsBusy);
            }
        }

        public BaseVM() {
            this.DisplayName = this.Title;
        }

    }
}
