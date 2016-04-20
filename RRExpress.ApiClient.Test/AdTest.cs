using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRExpress.Api.V1.Methods;
using RRExpress.Service.Entity;
using System.Reflection;
using System.Collections.Generic;
using RRExpress.Api.V1;
using RRExpress.Common;

namespace RRExpress.ApiClient.Test {
    [TestClass]
    public class AdTest {

        [TestInitialize]
        public void Init() {
            var asms = new List<Assembly>() {
                typeof(RRExpressV1BaseMethod<>).GetTypeInfo().Assembly
            };
            ApiClientOption.Default.UseSandbox = true;
            ApiClient.Init(asms);
        }

        [TestMethod]
        public void GetAds() {
            var mth = new GetADs() {
                Type = AdTypes.MobileAdMiddle
            };

            var datas = ApiClient.Instance.Value.Execute(mth).Result;
        }

        [TestMethod]
        public void GetNewRequest() {
            var mth = new GetNewRequests();
            var datas = ApiClient.Instance.Value.Execute(mth).Result;
        }
    }
}
