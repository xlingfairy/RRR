using RRExpress.Common.Interfaces;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public void SetRequestHttpClient(HttpClient client) {
            //添加 Accept
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.ContentType));
        }
    }
}
