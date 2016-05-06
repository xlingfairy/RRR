using Newtonsoft.Json;
using RRExpress.Common.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RRExpress.Common {
    public class JsonContentHandler : IContentHandler {

        public string ContentType {
            get {
                return "application/json";
            }
        }

        /// <summary>
        /// 使 Json.Net 支持抽象类序列化、反序列化
        /// </summary>
        private static readonly JsonSerializerSettings Setting = new JsonSerializerSettings() {
            TypeNameHandling = TypeNameHandling.Auto
        };

        public HttpContent GetContent(object data) {
            var json = JsonConvert.SerializeObject(data, Setting);
            var content = new StringContent(json);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(this.ContentType);
            return content;
        }

        public T Parse<T>(IClientSetup client, byte[] bytes) {
            var json = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 设置 Accept 头
        /// </summary>
        /// <param name="client"></param>
        public void SetRequestHttpClient(HttpClient client) {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.ContentType));
        }
    }
}
