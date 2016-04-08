using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Common {
    public class ApiClientOption {

        public bool UseSandbox { get; set; }


        public static readonly ApiClientOption Default = new ApiClientOption();

    }
}
