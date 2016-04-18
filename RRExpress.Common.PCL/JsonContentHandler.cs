using RRExpress.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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

        public void SetRequestHttpClient(HttpClient client) {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.ContentType));
        }
    }
}
