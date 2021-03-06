﻿using RRExpress.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace RRExpress.Common {
    public static class ParameterHelper {

        /// <summary>
        /// 将 API 方法中，标记 ParamAttribute 的属性提取成字典
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetParams(this BaseMethod method) {
            var dic = new Dictionary<string, object>();
            var props = method.GetType().GetRuntimeProperties().Where(p => p.GetCustomAttributes(typeof(ParamAttribute), true).Count() > 0);
            foreach (var p in props) {
                var pa = (ParamAttribute)p.GetCustomAttributes(typeof(ParamAttribute), true).First();
                var pms = pa.GetParams(method, p);
                if (pms != null) {
                    foreach (var pm in pms) {
                        //dic.Set(pm.Key, pm.Value);
                        if (dic.ContainsKey(pm.Key))
                            dic[pm.Key] = pm.Value;
                        else
                            dic.Add(pm.Key, pm.Value);
                    }
                }
            }

            return dic;
        }


        /// <summary>
        /// 将参数转换为 StringContent
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static StringContent GetStringContent(this BaseMethod method) {
            var dic = method.GetParams();
            var str = string.Join("&", dic.Select(s => string.Format("{0}={1}", s.Key, s.Value)));
            return new StringContent(str);
        }


        /// <summary>
        /// 将参数编进URL
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string BuildUrl(this BaseMethod method, string url) {
            var dic = method.GetParams();
            foreach (var kv in dic)
                url = url.SetUrlKeyValue(kv.Key, WebUtility.UrlEncode(kv.Value.ToString()));

            return url;
        }

        /// <summary>
        /// 将参数编进URI
        /// </summary>
        /// <param name="method"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Uri BuildUrl(this BaseMethod method, Uri uri) {
            return new Uri(method.BuildUrl(uri.AbsoluteUri));
        }
    }
}
