using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Runtime.CompilerServices;

namespace AsNum.XFControls {
    internal static class Helper {

        // http://www.cnblogs.com/LoveJenny/archive/2011/07/07/2100416.html
        // http://stackoverflow.com/questions/4939508/get-value-of-c-sharp-dynamic-property-via-string
        public static object GetProperty(object target, string name) {
            if (target == null || name == null)
                return null;

            var site = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(0, name, target.GetType(), new[] { CSharpArgumentInfo.Create(0, null) }));
            return site.Target(site, target);
        }

        public static object TryGetProperty(object target, string name) {
            try {
                return GetProperty(target, name);
            }
            catch {
                return null;
            }
        }

        public static T GetProperty<T>(object target, string name, T defaultValue = default(T)) {
            try {
                return (T)GetProperty(target, name);
            }
            catch {
                return defaultValue;
            }
        }
    }
}
