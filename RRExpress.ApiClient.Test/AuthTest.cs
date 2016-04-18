using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRExpress.Api.V1;
using RRExpress.Api.V1.Methods;
using System;
using System.Collections.Generic;
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
                typeof(RRExpressV1BaseMethod<>).GetTypeInfo().Assembly
            };
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
