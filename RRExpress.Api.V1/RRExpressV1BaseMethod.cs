using RRExpress.Common;
using RRExpress.Common.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RRExpress.Api.V1 {
    public abstract class RRExpressV1BaseMethod<T> : WebApiBaseMethod<T> {

        public override Type ClientSetupType {
            get {
                return typeof(Setup);
            }
        }

        public override Task<string> ExportRequestData() {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 默认 Protobuf 格式，需要服务端支持 Protobuf 格式
        /// </summary>
        public override ContentTypes ContentType {
            get {
                return ContentTypes.ProtoBuf;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setup"></param>
        /// <returns></returns>
        protected override HttpClient GetHttpClient(IClientSetup setup) {
            var token = ((Setup)setup).GetToken();
            return new BearerHttpClient(token);
        }
    }
}
