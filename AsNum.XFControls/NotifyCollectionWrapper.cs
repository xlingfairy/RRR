using System;
using System.Collections;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {

    /// <summary>
    /// 如果数据源是 INotifyCollectionChanged , 则监听变化事件，否则执行 reset 命令
    /// </summary>
    public class NotifyCollectionWrapper {


        public Action<IList, int> Add { get; }
        public Action<IList, int> Remove { get; }
        public Action Reset { get; }
        public Action Finished { get; }

        public NotifyCollectionWrapper(object source,
            Action<IList, int> add = null,
            Action<IList, int> remove = null,
            Action reset = null,
            Action finished = null
            ) {

            if (source is INotifyCollectionChanged) {
                var collection = (INotifyCollectionChanged)source;
                collection.CollectionChanged -= Collection_CollectionChanged;
                collection.CollectionChanged += Collection_CollectionChanged;

                this.Add = add;
                this.Remove = remove;
                this.Reset = reset;
                this.Finished = finished;
            }

            if (reset != null)
                reset.Invoke();
            if (finished != null)
                finished.Invoke();
        }

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    if (this.Add != null) {
                        Device.BeginInvokeOnMainThread(() => {
                            this.Add.Invoke(e.NewItems, e.NewStartingIndex);
                        });
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (this.Remove != null)
                        Device.BeginInvokeOnMainThread(() => {
                            this.Remove.Invoke(e.OldItems, e.OldStartingIndex);
                        });
                    break;
                case NotifyCollectionChangedAction.Reset:
                    if (this.Reset != null) {
                        Device.BeginInvokeOnMainThread(() => {
                            this.Reset.Invoke();
                        });
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
            }

            if (this.Finished != null)
                this.Finished.Invoke();
        }
    }
}
