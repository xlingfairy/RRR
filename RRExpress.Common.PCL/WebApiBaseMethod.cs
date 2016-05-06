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


        private static Dictionary<ContentTypes, IContentHandler> ContentHandlers = new Dictionary<ContentTypes, IContentHandler>() {
            { ContentTypes.Json, new JsonContentHandler() },
            { ContentTypes.ProtoBuf, new ProtobufContentHandler() }
        };


        /// <summary>
        /// 请求方式
        /// </summary>
        public abstract HttpMethod HttpMethod {
            get;
        }


        public abstract ContentTypes ContentType {
            get;
        }

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
                var handler = ContentHandlers[this.ContentType];
                return handler.GetContent(data);
            }
            return null;
        }



        protected virtual HttpClient GetHttpClient(IClientSetup setup) {
            return new HttpClient();
        }


        /// <summary>
        /// 请求API,返回API结果
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        protected override async Task<Tuple<byte[], HttpStatusCode>> GetResult(IClientSetup setup, string url) {
            using (var content = this.GetContent())
            using (var hc = this.GetHttpClient(setup)) {
                hc.Timeout = TimeSpan.FromSeconds(setup.Timeout);
                var request = new HttpRequestMessage(this.HttpMethod, url);

                var handler = ContentHandlers[this.ContentType];
                handler.SetRequestHttpClient(hc);

                if (content != null) {
                    request.Content = content;
                }

                var rep = await hc.SendAsync(request);

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
            var handler = ContentHandlers[this.ContentType];
            return Task.FromResult(handler.Parse<T>(setup, result));
        }
    }
}
