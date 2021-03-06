﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RRExpress.iOS {
    public class CaliburnAppDelegate : CaliburnApplicationDelegate {
		void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{

		}

		private SimpleContainer container;

        public CaliburnAppDelegate() {
            Initialize();
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        /// <summary>
        /// 用于加载不同DLL内的View
        /// </summary>
        protected override void StartRuntime() {
            base.StartRuntime();

            var asms = App.UsedAssemblies;
            foreach (var asm in asms) {
                if (!AssemblySource.Instance.Contains(asm)) {
                    AssemblySource.Instance.Add(asm);
                }
            }
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
