using ProtoBuf;

namespace RRExpress.Service.Entity {

    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public enum AdTypes {
        MobileAdSmall = 0,
        MobileAdMiddle = 1,
        MobileAdBig = 2
    }
}
