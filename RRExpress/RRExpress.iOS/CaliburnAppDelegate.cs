using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RRExpress.iOS {
    public class CaliburnAppDelegate : CaliburnApplicationDelegate {
        private SimpleContainer container;

        public CaliburnAppDelegate() {
            Initialize();
        }

        protected override void Configure() {
            container = new SimpleContainer();
            container.Instance(container);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key) {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<Assembly> SelectAssemblies() {
            return new[]
            {
                GetType().Assembly,
                typeof(App).Assembly
            };
        }
    }
}
