using ProtoBuf;
using System.Collections.Generic;

namespace RRExpress.Service.Entity {

    [ProtoContract(AsReferenceDefault = false, ImplicitFields = ImplicitFields.AllFields, EnumPassthru = true)]
    public class Region {

        public string AreaName { get; set; }

        public decimal Lng { get; set; }

        public decimal Lat { get; set; }

        public IEnumerable<Region> Children { get; set; }

    }
}
