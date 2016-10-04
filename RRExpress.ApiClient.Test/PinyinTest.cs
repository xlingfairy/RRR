using Microsoft.International.Converters.PinYinConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RRExpress.ApiClient.Test {

    [TestClass]
    public class PinyinTest {

        [TestMethod]
        public void Test() {
            var a = ChineseChar.IsValidChar('4');
            var Aa = ChineseChar.IsValidChar('a');
            var aaa = ChineseChar.IsValidChar(',');
            var aaaa = ChineseChar.IsValidChar('汉');
            var cc = new ChineseChar('汉');
        }

    }
}
