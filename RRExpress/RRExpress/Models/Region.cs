using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Models {
    public class Region {

        public static readonly IEnumerable<Region> Regions = null;

        public string AreaName { get; set; }

        public decimal Lng { get; set; }

        public decimal Lat { get; set; }

        public IEnumerable<Region> Children { get; set; }

        private static IEnumerable<Region> GetAll() {
            var assembly = typeof(Region).GetTypeInfo().Assembly;
            //var res = assembly.GetManifestResourceNames();
            using (var stream = assembly.GetManifestResourceStream("RRExpress.Region.json"))
            using (var reader = new System.IO.StreamReader(stream)) {
                var text = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<IEnumerable<Region>>(text);
            }
        }

        static Region() {
            Regions = GetAll();
        }
    }
}
