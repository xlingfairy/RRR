using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Common.Exceptions {
    public class ParseException : Exception {

        public byte[] TargetData { get; set; }

        public Type TargetType { get; set; }
    }
}
