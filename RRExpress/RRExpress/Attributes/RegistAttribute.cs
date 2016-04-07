using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Attributes {
    public enum InstanceMode {

        /// <summary>
        /// Not regist
        /// </summary>
        None = 0,

        /// <summary>
        /// Singleton
        /// </summary>
        Singleton,

        /// <summary>
        /// New instance pre request
        /// </summary>
        PreRequest
    }


    /// <summary>
    /// Used for IoC (In this app, it's mean SimpleContainer) auto regist.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RegistAttribute : Attribute {

        public InstanceMode Mode { get; private set; }

        /// <summary>
        /// Regist for which type, default as this attribute attached class's type.
        /// </summary>
        public Type ForType { get; set; }

        public RegistAttribute(InstanceMode mode) {
            this.Mode = mode;
        }

    }
}
