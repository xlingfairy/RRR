using ProtoBuf;
using System;

namespace RRExpress.Service.Entity {

    /// <summary>
    /// 积分
    /// </summary>
    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields, EnumPassthru = true)]
    public class Point {

        public long ID { get; set; }

        public PointTypes PointType { get; set; }

        public int Value { get; set; }

        public DateTime Time { get; set; }

        public string Desc { get; set; }
    }

    public enum PointTypes : byte {
        Regist = 0,
        Order = 1,
        Evaluate = 2
    }
}
