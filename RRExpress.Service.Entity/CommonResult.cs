using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Service.Entity {

    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields, EnumPassthru = true)]
    public class CommonResult {

        public bool IsSuccess { get; set; }

        public string Msg { get; set; }
    }
}
