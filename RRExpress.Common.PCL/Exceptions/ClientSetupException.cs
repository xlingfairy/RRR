using RRExpress.Common.Interfaces;
using System;

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
