using Android.OS;

namespace RRExpress.Droid.JPushHandlers {
    public abstract class BaseHandler {

        public abstract string Action {
            get;
        }

        public virtual void Handle(Bundle bundle) {
        }
    }
}