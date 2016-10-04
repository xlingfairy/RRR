using RRExpress.Common;
using RRExpress.Common.Interfaces;
using System.Composition;

namespace Baidu {

    [Export(typeof(IClientSetup))]
    public class Setup : IClientSetup {
        public bool IsValid {
            get {
                return true;
            }
        }

        public int Timeout {
            get; set;
        } = 30;

        private string AK = "E4805d16520de693a3fe707cdc962045";

        public string GetUrl(BaseMethod baseMethod, bool useSandbox) {
            return $"http://api.map.baidu.com/{baseMethod.Module}/v2/?ak={this.AK}&callback=_&output=json&pois=1";
        }
    }
}
