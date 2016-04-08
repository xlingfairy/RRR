using RRExpress.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Common.Exceptions {
    public class ClientSetupException : Exception {

        public IClientSetup Setup {
            get;
            private set;
        }

        public ClientSetupException(IClientSetup setup)
            : base(string.Format("{0} 配置错误", setup.GetType().FullName)) {
            this.Setup = setup;
        }

    }
}
