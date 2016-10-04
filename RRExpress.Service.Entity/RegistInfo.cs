using ProtoBuf;

namespace RRExpress.Service.Entity {

    [ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields, EnumPassthru = true)]
    public class RegistInfo {

        public string Phone { get; set; }

        public string Code { get; set; }

        public string Pwd { get; set; }

        public string ConfirmPwd { get; set; }

        public string DeviceID { get; set; }

    }
}
