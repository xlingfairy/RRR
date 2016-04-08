using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Common.Exceptions {
    /// <summary>
    /// 供应商自定义的错误（嵌入到返回结果中）
    /// </summary>
    public class ContentWithErrorException : Exception {

        public ContentWithErrorException(string msg) : base(msg) {

        }

    }
}
