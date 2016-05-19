using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Service.Entity {

    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields, EnumPassthru = true)]
    public class Region {

        public string AreaName { get; set; }

        public decimal Lng { get; set; }

        public decimal Lat { get; set; }

        public IEnumerable<Region> Children { get; set; }

    }
}
