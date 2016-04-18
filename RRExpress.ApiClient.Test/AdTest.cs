using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRExpress.Api.V1.Methods;
using RRExpress.Service.Entity;
using System.Reflection;
using System.Collections.Generic;
using RRExpress.Api.V1;

namespace RRExpress.ApiClient.Test {
    [TestClass]
    public class AdTest {

        [TestInitialize]
        public void Init() {
            var asms = new List<Assembly>() {
                typeof(RRExpressV1BaseMethod<>).GetTypeInfo().Assembly
            };
            ApiClient.Init(asms);
        }

        [TestMethod]
        public void GetAds() {
            var mth = new GetADs() {
                Type = AdTypes.MobileAdMiddle
            };

            var datas = ApiClient.Instance.Value.Execute(mth).Result;
        }
    }
}
