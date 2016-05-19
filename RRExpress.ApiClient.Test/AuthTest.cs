using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRExpress.Api.V1;
using RRExpress.Api.V1.Methods;
using RRExpress.Common;
using RRExpress.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.ApiClient.Test {
    [TestClass]
    public class AuthTest {

        [TestInitialize]
        public void Init() {
            var asms = new List<Assembly>() {
                typeof(RRExpressV1BaseMethod<>).GetTypeInfo().Assembly,
                typeof(AuthTest).GetTypeInfo().Assembly
            };
            ApiClientOption.Default.UseSandbox = true;
            ApiClient.Init(asms);
        }

        [TestMethod]
        public void DoAuth() {
            var mth = new Auth() {
                UserName = "userB",
                Password = "111111"
            };

            var token = ApiClient.Instance.Value.Execute(mth).Result;
        }
    }
}
