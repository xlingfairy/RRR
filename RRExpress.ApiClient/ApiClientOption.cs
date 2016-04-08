using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ApiClient {
    public class ApiClientOption {

        /// <summary>
        /// Api 的基地址(http://xxx/{controller}/{action} , 基地址即为 http://xxx)
        /// </summary>
        public string BaseUri { get; }

        public ApiClientOption(string baseUri) {
            this.BaseUri = baseUri;
        }

    }
}
