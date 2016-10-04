using Caliburn.Micro;
using RRExpress.AppCommon.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RRExpress.AppCommon {
    public static class VMRegistHelper {

        /// <summary>
        /// 注册ViewModel
        /// </summary>
        /// <param name="_container"></param>
        public static void RegistInstances(this SimpleContainer container, Assembly asm) {
            var types = asm.DefinedTypes
                .Select(t => {
                    var attr = t.GetCustomAttribute<RegistAttribute>();
                    return new {
                        T = t,
                        Mode = attr?.Mode,
                        TargetType = attr?.ForType
                    };
                })
                .Where(o => o.Mode != null && o.Mode != InstanceMode.None);

            foreach (var t in types) {
                var type = t.T.AsType();
                if (t.Mode == InstanceMode.Singleton) {
                    container.RegisterSingleton(t.TargetType ?? type, null, type);
                }
                else if (t.Mode == InstanceMode.PreRequest) {
                    container.RegisterPerRequest(t.TargetType ?? type, null, type);
                }
            }
        }

        public static void RegistInstances(this SimpleContainer container, IEnumerable<Assembly> asms) {
            foreach (var asm in asms)
                container.RegistInstances(asm);
        }
    }
}
