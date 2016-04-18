using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Common {
    public class BearerHttpClient : HttpClient {

        public BearerHttpClient(string token)
            : base(new BearerMessageHandler(token)) {
        }
    }
}
