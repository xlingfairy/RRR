using Caliburn.Micro;

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
            //Debug.WriteLine(this.GetType());
        }

    }
}
