using System.Threading.Tasks;

namespace RRExpress.Common.Interfaces {
    public interface IBearerTokenProvider {

        string GetToken(string tag);

        Task UpdateToken(string tag, Token token);
    }
}
