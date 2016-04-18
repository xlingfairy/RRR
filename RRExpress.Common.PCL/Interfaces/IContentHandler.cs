using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Common.Interfaces {
    public interface IContentHandler {

        string ContentType { get; }

        HttpContent GetContent(object data);

        T Parse<T>(IClientSetup client, byte[] bytes);

        void SetRequestHttpClient(HttpClient client);
    }
}
