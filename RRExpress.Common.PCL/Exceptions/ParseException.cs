using System;

namespace RRExpress.Common.Exceptions {

    /// <summary>
    /// 返回的数据解析失败
    /// </summary>
    public class ParseException : Exception {

        public byte[] TargetData { get; set; }

        public Type TargetType { get; set; }
    }
}
