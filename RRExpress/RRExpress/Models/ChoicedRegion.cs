namespace RRExpress.Models {

    /// <summary>
    /// 选择的区域
    /// </summary>
    public class ChoicedRegion {

        public string FullName { get; set; }

        public Region Region { get; set; }

        public string ProvinceName { get; set; }

        public string CityName { get; set; }

        public string DetailAddress { get; set; }
    }
}
