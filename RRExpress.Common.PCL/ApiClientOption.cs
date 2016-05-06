namespace RRExpress.Common {

    /// <summary>
    /// Api Client 选项
    /// </summary>
    public class ApiClientOption {

        /// <summary>
        /// 是否使用测试环境
        /// </summary>
        public bool UseSandbox { get; set; }


        /// <summary>
        /// 默认选项
        /// </summary>
        public static readonly ApiClientOption Default = new ApiClientOption();

    }
}
