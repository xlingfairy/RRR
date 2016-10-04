using RRExpress.Common;
using RRExpress.Common.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Baidu.Methods {
    public class Geocoder : WebApiBaseMethod<object> {
        public override Type ClientSetupType {
            get {
                return typeof(Setup);
            }
        }

        public override ContentTypes ContentType {
            get {
                throw new NotImplementedException();
            }
        }

        public override HttpMethod HttpMethod {
            get {
                return HttpMethod.Get;
            }
        }

        public override string Module {
            get {
                return "geocoder";
            }
        }

        public override Task<string> ExportRequestData() {
            throw new NotImplementedException();
        }


        protected override Task<object> Parse(IClientSetup setup, byte[] result) {
            //return base.Parse(setup, result);

            var str = Encoding.UTF8.GetString(result, 0, result.Length);
            var json = Regex.Match(str, @"^_&&_\((?<json>[\s\S]*?)\)$").Groups["json"].Value;
            if (string.IsNullOrWhiteSpace(json))
                return null;
            else {
                var bs = Encoding.UTF8.GetBytes(json);
                return base.Parse(setup, bs);
            }
        }
    }
}
