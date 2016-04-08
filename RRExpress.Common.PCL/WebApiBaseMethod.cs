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
using System.IO;

namespace RRExpress.Common {
    public abstract class WebApiBaseMethod<T> : BaseMethod<T> {

        public static readonly string PROTOBUF_META = "application/x-protobuf";
        public static readonly string JSON_META = "application/json";

        /// <summary>
        /// 使 Json.Net 支持抽象类序列化、反序列化
        /// </summary>
        private static readonly JsonSerializerSettings Setting = new JsonSerializerSettings() {
            TypeNameHandling = TypeNameHandling.Auto
        };

        /// <summary>
        /// 请求方式
        /// </summary>
        public abstract HttpMethod HttpMethod {
            get;
        }

        /// <summary>
        /// 是否使用 ProtoBuf ,默认true
        /// </summary>
        public virtual bool UseProtobuf { get; } = true;


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual object GetSendData() {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual HttpContent GetContent() {
            var data = this.GetSendData();
            if (data != null) {

                if (this.UseProtobuf) {
                    //TODO 未验证
                    using (var msm = new MemoryStream()) {
                        ProtoBuf.Serializer.Serialize(msm, data);
                        var content = new ByteArrayContent(msm.ToArray());
                        //var content = new StreamContent(msm);
                        content.Headers.ContentType = MediaTypeHeaderValue.Parse(PROTOBUF_META);
                        return content;
                    }
                }
                else {
                    var json = JsonConvert.SerializeObject(data, Setting);
                    var content = new StringContent(json);
                    content.Headers.ContentType = MediaTypeHeaderValue.Parse(JSON_META);
                    return content;
                }
            }
            return null;
        }


        /// <summary>
        /// 请求API,返回API结果
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        protected override async Task<Tuple<byte[], HttpStatusCode>> GetResult(IClientSetup client, string url) {
            using (var content = this.GetContent())
            using (var hc = new HttpClient()) {
                var request = new HttpRequestMessage(this.HttpMethod, url);

                if (UseProtobuf)
                    hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(PROTOBUF_META));

                if (content != null) {
                    request.Content = content;
                }

                var rep = await hc.SendAsync(request);
                //TODO
                var useProtoBuf = rep.Content.Headers.ContentType.MediaType.Equals(PROTOBUF_META, StringComparison.OrdinalIgnoreCase);

                var bytes = await rep.Content.ReadAsByteArrayAsync();
                return new Tuple<byte[], HttpStatusCode>(bytes, rep.StatusCode);
            }
        }

        /// <summary>
        /// 解析API返回结果
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected override Task<T> Parse(IClientSetup setup, byte[] result) {
            var json = Encoding.UTF8.GetString(result, 0, result.Length);
            return Task.FromResult(JsonConvert.DeserializeObject<T>(json));
        }
    }
}
