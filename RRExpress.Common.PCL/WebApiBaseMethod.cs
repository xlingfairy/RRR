using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RRExpress.Common.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace RRExpress.Common {
    public abstract class WebApiBaseMethod<T> : BaseMethod<T> {


        private static readonly JsonSerializerSettings Setting = new JsonSerializerSettings() {
            TypeNameHandling = TypeNameHandling.Auto
        };

        public abstract HttpMethod HttpMethod {
            get;
        }

        protected virtual object GetSendData() {
            //return this.GetParams();
            return null;
        }

        public virtual HttpContent GetContent() {
            var data = this.GetSendData();
            if (data != null) {
                var json = JsonConvert.SerializeObject(data, Setting);
                var content = new StringContent(json);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                return content;
            }
            return null;
        }



        protected override async Task<Tuple<byte[], HttpStatusCode>> GetResult(IClientSetup client, string url) {
            using (var content = this.GetContent())
            using (var hc = new HttpClient()) {
                var request = new HttpRequestMessage(this.HttpMethod, url);
                if (content != null) {
                    request.Content = content;
                }

                var rep = await hc.SendAsync(request);
                var bytes = await rep.Content.ReadAsByteArrayAsync();
                return new Tuple<byte[], HttpStatusCode>(bytes, rep.StatusCode);
            }
        }

        protected override Task<T> Parse(IClientSetup setup, byte[] result) {
            var json = Encoding.UTF8.GetString(result, 0, result.Length);
            return Task.FromResult(JsonConvert.DeserializeObject<T>(json));
        }
    }
}
