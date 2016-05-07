using System.Threading.Tasks;

namespace RRExpress.Common.Interfaces {

    public interface IWebApiBearerTokenProvider {


        /// <summary>
        /// 获取 Token
        /// </summary>
        /// <returns></returns>
        string GetToken();

        /// <summary>
        /// 更新Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateToken(Token token);

        bool IsValid { get; }
    }
}
