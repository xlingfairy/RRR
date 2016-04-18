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
            var str = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            using (var msm = new MemoryStream(bytes)) {
                //return ProtoBuf.Serializer.Deserialize<T>(msm);
                //return (T)ProtoBuf.Serializer.NonGeneric.Deserialize(typeof(T), msm);
                //return (T)RuntimeTypeModel.Default.Deserialize(msm, null, typeof(T));
                return ProtoBuf.Serializer.DeserializeWithLengthPrefix<T>(msm, ProtoBuf.PrefixStyle.Fixed32);
            }

        }

        public void SetRequestHttpClient(HttpClient client) {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.ContentType));
        }
    }
}
