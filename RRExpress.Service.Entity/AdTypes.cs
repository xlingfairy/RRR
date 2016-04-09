using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Service.Entity {

    [ProtoContract]
    public enum AdTypes {
        MobileAdSmall,
        MobileAdMiddle,
        MobileAdBig
    }
}
