using System.Web.Http;

namespace RRExpress.Common.Extends {
    public static class HttpConfigurationHelper {

        /// <summary>
        /// 使用 Protobuf
        /// </summary>
        /// <param name="config"></param>
        public static void UseProtobuf(this HttpConfiguration config) {
            config.Formatters.Add(new ProtoBufFormatter());
        }

    }
}
