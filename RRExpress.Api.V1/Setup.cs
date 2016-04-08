using RRExpress.Common;
using RRExpress.Common.Interfaces;
using System.Composition;
using System.Linq;
using System;

namespace RRExpress.Api.V1 {

    [Export(typeof(IClientSetup))]
    public class Setup : IClientSetup {

        public bool IsValid {
            get {
                return true;
            }
        }

        private string BaseUri = "http://www.baidu.com/api/v1";

        public string BuildUri(BaseMethod mth) {
            var url = mth.Module.FixUrl(this.BaseUri);
            return url;
            //if (mth.SendData == null) {
            //    return url.SetUrlKeyValue(mth.GetParams().ToDictionary(d => d.Key, d => d.Value.ToString()));
            //} else
            //    return url;
        }

        public string GetUrl(BaseMethod mth, bool useSandbox) {
            throw new NotImplementedException();
        }
    }
}
