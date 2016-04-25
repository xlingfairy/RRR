using RRExpress.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;
using ProtoBuf.Meta;

namespace RRExpress.Common {
    public class ProtobufContentHandler : IContentHandler {
        public string ContentType {
            get {
                return "application/x-protobuf";
            }
        }

        public HttpContent GetContent(object data) {
            using (var msm = new MemoryStream()) {
                ProtoBuf.Serializer.Serialize(msm, data);
                var content = new ByteArrayContent(msm.ToArray());
                content.Headers.ContentType = MediaTypeHeaderValue.Parse(this.ContentType);
                return content;
            }
        }

        public T Parse<T>(IClientSetup client, byte[] bytes) {
            using (var msm = new MemoryStream(bytes)) {
                //return ProtoBuf.Serializer.DeserializeWithLengthPrefix<T>(msm, ProtoBuf.PrefixStyle.None);
                return ProtoBuf.Serializer.Deserialize<T>(msm);
            }

        }

        public void SetRequestHttpClient(HttpClient client) {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.ContentType));
        }
    }
}
