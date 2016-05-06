namespace RRExpress.Common.Interfaces {

    /// <summary>
    /// API 配置接口
    /// </summary>
    public interface IClientSetup {

        /// <summary>
        /// 配置是否正确
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// 获取 API 方法的最终地址
        /// </summary>
        /// <param name="baseMethod"></param>
        /// <param name="useSandbox"></param>
        /// <returns></returns>
        string GetUrl(BaseMethod baseMethod, bool useSandbox);

        /// <summary>
        /// 请求超时,秒
        /// </summary>
        int Timeout { get; set; }
    }
}
