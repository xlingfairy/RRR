using RRExpress.Common;
using RRExpress.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public override ContentTypes ContentType {
            get {
                return ContentTypes.ProtoBuf;
            }
        }
    }
}
