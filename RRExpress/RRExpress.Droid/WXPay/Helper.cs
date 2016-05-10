using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Security.Cryptography;

namespace RRExpress.Droid.WXPay {
    public static class Helper {
        /// <summary>
        /// 获取所有异常的详情，以便于定位错误
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetAllMsg(this Exception ex) {
            var str = ex.Message;
            while (ex.InnerException != null) {
                str += ex.InnerException.Message;
                ex = ex.InnerException;
            }

            return str;
        }


        public static string MD5(this string str) {
            StringBuilder result = new StringBuilder();
            var md5 = new MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            md5.Clear();
            for (int i = 0; i < hashBytes.Length; i++) {
                result.Append(Convert.ToString(hashBytes[i], 16).PadLeft(2, '0'));
            }
            return result.ToString().PadLeft(32, '0');
        }
    }
}