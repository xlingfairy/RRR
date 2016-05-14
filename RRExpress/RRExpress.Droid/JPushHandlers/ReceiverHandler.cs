using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RRExpress.Droid.JPushHandlers {
    public static class ReceiverHandler {

        private static Dictionary<string, BaseHandler> Handlers = null;

        static ReceiverHandler() {
            Handlers = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(BaseHandler)))
                .Select(t => (BaseHandler)Activator.CreateInstance(t))
                .ToDictionary(t => t.Action, t => t);
        }

        public static async Task Handle(string action, Bundle bundle) {
            if (string.IsNullOrWhiteSpace(action) || bundle == null)
                return;

            if (Handlers != null && Handlers.ContainsKey(action)) {
                var handler = Handlers[action];

                await Task.Run(() => {
                    handler.Handle(bundle);
                })
                .ContinueWith(t => {
                    var ex = t.Exception;
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    ex.Handle(e => {
                        return true;
                    });
                }, TaskContinuationOptions.OnlyOnFaulted)
                .ContinueWith(t => {
                }, TaskContinuationOptions.OnlyOnCanceled);
            }
        }
    }
}