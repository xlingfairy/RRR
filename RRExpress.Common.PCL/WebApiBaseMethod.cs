using RRExpress.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RRExpress.Common {

    /// <summary>
    /// 基于 Web Api 的 API 方法基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class WebApiBaseMethod<T> : BaseMethod<T> {

        /// <summary>
        /// 可用格式器
        /// </summary>
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

        /// <summary>
        /// 格式
        /// </summary>
        public abstract ContentTypes ContentType {
            get;
        }

        /// <summary>
        /// 用于 Post / Put 的数据
        /// </summary>
        /// <returns></returns>
        protected virtual object GetSendData() {
            return null;
        }

        /// <summary>
        /// 将 SendData 转化为 HttpContent
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


        /// <summary>
        /// 获取操作过的 HttpClient ，如添加 Header 等
        /// </summary>
        /// <param name="setup"></param>
        /// <returns></returns>
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
                //设置超时
                hc.Timeout = TimeSpan.FromSeconds(setup.Timeout);
                var request = new HttpRequestMessage(this.HttpMethod, url);

                //根据API方法的 ContentType 获取指定的格式化器的处理程序
                var handler = ContentHandlers[this.ContentType];

                //对 httpClient 进行处理，比如添加 Accept Header 等
                handler.SetRequestHttpClient(hc);

                //如果是 Post/ Put ， 将 SendData 写入 request
                if (content != null) {
                    request.Content = content;
                }

                //发送请求
                var rep = await hc.SendAsync(request);
                
                //获取请求返回的数据
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
