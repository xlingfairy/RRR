using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Common.Interfaces {
    public interface IClientSetup {
        bool IsValid { get; }

        string GetUrl(BaseMethod baseMethod, bool useSandbox);

        /// <summary>
        /// 请求超时,秒
        /// </summary>
        int Timeout { get; set; }
    }
}
