using System;

namespace RRExpress.AppCommon.Attributes {

    /// <summary>
    /// 模型注册类型
    /// </summary>
    public enum InstanceMode {

        /// <summary>
        /// 不注册
        /// </summary>
        None = 0,

        /// <summary>
        /// 单例
        /// </summary>
        Singleton,

        /// <summary>
        /// 每次实例化
        /// </summary>
        PreRequest
    }


    /// <summary>
    /// 模型注册标识，用于自动注册（在 App 中实现）
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
