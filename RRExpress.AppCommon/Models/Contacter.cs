namespace RRExpress.AppCommon.Models {

    /// <summary>
    /// 通讯录中的联系人
    /// </summary>
    public class Contacter {

        public string Name { get; set; }

        public string Phone { get; set; }

        public string PhoneType { get; set; }

        public byte[] Img { get; set; }
    }
}
