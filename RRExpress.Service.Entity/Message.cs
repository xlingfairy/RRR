using ProtoBuf;
using System;

namespace RRExpress.Service.Entity {

    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields, EnumPassthru = true)]
    public class Message {

        public long MessageID { get; set; }

        public string Subject { get; set; }

        public string Ctx { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsReaded { get; set; }
    }
}
