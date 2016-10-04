using ProtoBuf;
using System;

namespace RRExpress.Service.Entity {

    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields, EnumPassthru = true)]
    public class OrderEvent {

        public long ID { get; set; }

        public string OrderNO { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime EventTime { get; set; }
    }
}
