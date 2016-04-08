using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RRExpress.Common {
    public static class StringHelper {

        /// <summary>
        /// 從URL中取 Key / Value
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseString(this string s, bool ignoreCase) {
            //必須這樣,請不要修改
            if (string.IsNullOrEmpty(s)) {
                return new Dictionary<string, string>();
            }

            if (s.IndexOf('?') != -1) {
                s = s.Remove(0, s.IndexOf('?'));
            }

            Dictionary<string, string> kvs = new Dictionary<string, string>();
            Regex reg = new Regex(@"[\?&]?(?<key>[^=]+)=(?<value>[^\&]*)");
            MatchCollection ms = reg.Matches(s);
            string key;
            foreach (Match ma in ms) {
                key = ignoreCase ? ma.Groups["key"].Value.ToLower() : ma.Groups["key"].Value;
                if (kvs.ContainsKey(key)) {
                    kvs[key] += "," + ma.Groups["value"].Value;
                } else {
                    kvs[key] = ma.Groups["value"].Value;
                }
            }

            return kvs;
        }

        /// <summary>
        /// 設置 URL中的 key
        /// </summary>
        /// <param name="url"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string SetUrlKeyValue(this string url, string key, string value, Encoding encode = null) {
            if (url == null)
                url = "";

            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (value == null)
                value = "";

            if (null == encode)
                encode = Encoding.UTF8;

            if (url.ParseString(true).ContainsKey(key.ToLower())) {
                Regex reg = new Regex(@"([\?\&])(" + key + @"=)([^\&]*)(\&?)", RegexOptions.IgnoreCase);

                return reg.Replace(url, (ma) => {
                    if (ma.Success) {
                        return string.Format("{0}{1}{2}{3}", ma.Groups[1].Value, ma.Groups[2].Value, value, ma.Groups[4].Value);
                    }
                    return "";
                });

            } else {
                return string.Format("{0}{1}{2}={3}",
                    url,
                    (url.IndexOf('?') > -1 ? "&" : "?"),
                    key,
                    value);
            }
        }

        public static string SetUrlKeyValue(this string url, Dictionary<string, string> kvs, Encoding encode = null) {
            foreach (var kv in kvs) {
                url = url.SetUrlKeyValue(kv.Key, kv.Value, encode);
            }
            return url;
        }

        /// <summary>
        /// 修正URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string FixUrl(this string url) {
            return url.FixUrl("");
        }

        /// <summary>
        /// 修正URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="defaultPrefix"></param>
        /// <returns></returns>
        public static string FixUrl(this string url, string defaultPrefix) {
            // 必須這樣,請不要修改
            if (url == null)
                url = "";

            if (defaultPrefix == null)
                defaultPrefix = "";
            string tmp = url.Trim();
            if (!Regex.Match(tmp, "^(http|https):").Success) {
                tmp = string.Format("{0}/{1}", defaultPrefix, tmp);
            }
            tmp = Regex.Replace(tmp, @"(?<!(http|https):)[\\/]+", "/").Trim();
            return tmp;
        }

    }
}
