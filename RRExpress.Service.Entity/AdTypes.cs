using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Service.Entity {

    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public enum AdTypes {
        MobileAdSmall = 0,
        MobileAdMiddle = 1,
        MobileAdBig = 2
    }
}
