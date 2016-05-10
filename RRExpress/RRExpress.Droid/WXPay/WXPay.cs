using Android.Content;
using Android.Util;
using Android.Widget;
using Com.Tencent.MM.Sdk.Modelpay;
using Com.Tencent.MM.Sdk.Openapi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RRExpress.Droid.WXPay {
    class WXPay {

        private static readonly string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

        private PayReq mReq;
        private IWXAPI msgApi;
        private StringBuilder mSb;

        private Context Context;


        public WXPay(Context context) {
            this.Context = context;
            mSb = new StringBuilder();
            msgApi = WXAPIFactory.CreateWXAPI(context, Constants.APPID);
        }

        /// <summary>
        /// 1 生成预支付订单,获得预支付Id以便于发起微信支付
        /// </summary>
        /// <param name="proTitle"></param>
        /// <param name="total_fee"></param>
        /// <returns></returns>
        public async Task<string> GoPrePayId(string proTitle, decimal total_fee) {
            //1 生成预支付订单
            var prePayId = "";
            var prePayErr = "";

            WebClient client = new WebClient();
            string entity = GenProductArgs(proTitle, total_fee);
            client.Headers.Add("Accept", "application/json");
            client.Headers.Add("Content-Type", "application/json");
            byte[] responseByte = null;
            try {
                responseByte = await client.UploadDataTaskAsync(url, "POST", Encoding.UTF8.GetBytes(entity));
                string content = Encoding.UTF8.GetString(responseByte);
                Dictionary<string, string> responseXml = DecodeXml(content);
                prePayId = responseXml["prepay_id"];
                prePayErr = responseXml.ContainsKey("err_code_des") ? responseXml["err_code_des"] : "";
                if (string.IsNullOrEmpty(prePayErr))
                    prePayErr = responseXml.ContainsKey("return_msg") ? responseXml["return_msg"] : "";

                Toast.MakeText(this.Context, "prePayId:" + prePayId, ToastLength.Short).Show();
            }
            catch (Exception ex) {
                Toast.MakeText(this.Context, "请求标识出错:" + prePayId + "_" + prePayErr + "_" + ex.GetAllMsg(), ToastLength.Short).Show();
            }
            finally {
            }

            return prePayId;
        }

        /// <summary>
        /// 2 (根据预支付得到的prepay_id) 生成APP微信支付参数 以发起微信支付
        /// </summary>
        public PayReq GenPayReq(string prepay_id = "") {
            var mReq = new PayReq();
            mReq.AppId = Constants.APPID;
            mReq.PartnerId = Constants.MCHID;
            mReq.PrepayId = prepay_id;
            mReq.PackageValue = "Sign=WXPay";
            mReq.NonceStr = GenNoceStr();
            mReq.TimeStamp = GenTimeStamp();

            mReq.Sign = GenPayReqSign(mReq);
            return mReq;

        }


        /// <summary>
        /// 3 调起微信支付，(前提：微信已安装，且没有被应用回收，即正常人工可以使用)
        ///   新建一个包wxapi,建一个类名为WXPayEntryActivity作为接受微信的支付结果(回调名必须是：
        ///     你的包名+.wxapi.WXPayEntryActivity),添加如下代码到Mainfast里面就行了 exported = true
        ///      activity android:name=".wxapi.WXPayEntryActivity" android:exported="true" 实现onResp函数
        ///   不过，最终结果以服务器的返回为准notify_url，App等待并轮询结果，接下来就是要为了做安全性设计，
        ///      把一些重要的东西放到服务器生生成，这样是为防止客户端被反编译，建议GenPackageSign/genProductArgs方法在服务端生成
        /// </summary>
        public bool SendPayReq(PayReq pReq) {
            bool isOk = false;
            try {
                if (!msgApi.IsWXAppInstalled)
                    Toast.MakeText(this.Context, "您还没有安装微信,暂不支持此功能!", ToastLength.Short).Show();
                else if (!msgApi.IsWXAppSupportAPI)
                    Toast.MakeText(this.Context, "你安装的微信版本不支持当前开放API!", ToastLength.Short).Show();
                else {
                    //msgApi.RegisterApp(Constants.APPID);
                    isOk = msgApi.SendReq(pReq);
                }

            }
            catch (Exception ex) {
                Toast.MakeText(this.Context, ex.GetAllMsg(), ToastLength.Short).Show();
                //throw;
            }

            return isOk;

        }

        private string GenPayReqSign(PayReq mReq) {
            Dictionary<string, string> packageParams = new Dictionary<string, string>();
            packageParams.Add("appid", mReq.AppId);
            packageParams.Add("partnerid", mReq.PartnerId);  //商品或支付单简要描述:如 Ipad mini  16G  白色   detail 可选字段 商品详情
            packageParams.Add("prepayid", mReq.PrepayId);
            packageParams.Add("package", mReq.PackageValue);
            packageParams.Add("noncestr", mReq.NonceStr);  //接收微信支付异步通知回调地址，通知url必须为直接可访问的url，不能携带参数
            packageParams.Add("timestamp", mReq.TimeStamp);
            string sign = GenPackageSign(packageParams);
            packageParams.Add("sign", sign);

            return sign;
        }

        /// <summary>
        /// 准备具体产品订单的-数据发送包，签名后转换成XML数据交换格式
        /// </summary>
        /// <returns></returns>
        private string GenProductArgs(string proTitle, decimal total_fee, string ip = "127.0.0.1") {
            Dictionary<string, string> packageParams = new Dictionary<string, string>();
            packageParams.Add("appid", Constants.APPID);
            packageParams.Add("body", proTitle);  //商品或支付单简要描述:如 Ipad mini  16G  白色   detail 可选字段 商品详情
            packageParams.Add("mch_id", Constants.MCHID);
            packageParams.Add("nonce_str", GetNonceStr());
            packageParams.Add("notify_url", Constants.Notify_Url);  //接收微信支付异步通知回调地址，通知url必须为直接可访问的url，不能携带参数
            packageParams.Add("out_trade_no", GetOutTradNo());
            packageParams.Add("spbill_create_ip", ip);
            packageParams.Add("total_fee", total_fee.ToString());  //订单总金额，单位为分
            packageParams.Add("trade_type", "APP");

            string sign = GenPackageSign(packageParams);
            packageParams.Add("sign", sign);

            string xmlString = ToXml(packageParams);
            return xmlString;
        }

        #region 非数据字段 帮助方法

        /// <summary>
        /// 生成随机数算法
        /// 微信支付API接口协议中包含字段nonce_str，
        ///   主要保证签名不可预测。我们推荐生成随机数算法如下：调用随机数函数生成，将得到的值转换为字符串。 
        /// </summary>
        /// <returns></returns>
        private string GetNonceStr() {
            Random r = new Random();
            return r.Next(10000).ToString().MD5();
        }

        /// <summary>
        /// 生成商户的订单号
        /// </summary>
        /// <returns></returns>
        private string GetOutTradNo() {
            Random r = new Random();
            //return Utility.MD5(r.Next(10000).ToString());
            return DateTime.Now.ToString("yyMMdd") + r.Next(100000).ToString();
        }

        /// <summary>
        /// 数据包签名(不同于包签名)
        /// 安卓端开发步骤说明：https://pay.weixin.qq.com/wiki/doc/api/app/app.php?chapter=8_5
        ///           安全规范：https://pay.weixin.qq.com/wiki/doc/api/app/app.php?chapter=4_3
        ///   根据项目的应用包名和编译使用的keystore，可由签名工具生成一个32位的md5串，在调试的手机上安装签名工具后，运行可生成应用签名串
        ///   参数名ASCII码从小到大排序（字典序）；如果参数的值为空不参与签名； 参数名区分大小写；
        ///   tringSignTemp="stringA&key=192006250b4c09247ec02edce69f6a2d"
        ///   sign=MD5(stringSignTemp).toUpperCase()
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GenPackageSign(Dictionary<string, string> param) {
            var sortParam = param.OrderBy(o => o.Key);
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> dic in sortParam) {
                if (string.IsNullOrEmpty(dic.Value))
                    continue;

                sb.AppendFormat("{0}={1}&", dic.Key, dic.Value);
            }
            sb.AppendFormat("key={0}", Constants.APIKEY);

            string packageSign = sb.ToString().MD5().ToUpper();
            Log.Error("orion", packageSign);

            return packageSign;
        }


        private string GenNoceStr() {
            //throw new NotImplementedException();
            return GetNonceStr();
        }
        #endregion

        #region XML 内存字典结果 互转

        /// <summary>
        /// 将返回的XML解析成键值对字典结果
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private Dictionary<string, string> DecodeXml(string content) {
            XElement xe = XElement.Parse(content);
            var result = (from item in xe.Elements()
                          where item.NodeType == XmlNodeType.Element
                          select new {
                              item.Name.LocalName,
                              item.Value
                          }).ToDictionary(x => x.LocalName, x => x.Value);
            return result;
        }

        private string ToXml(Dictionary<string, string> param) {
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xml = new XmlTextWriter(ms, Encoding.UTF8);
            try {
                xml.Formatting = Formatting.Indented;
                xml.WriteStartDocument();
                xml.WriteStartElement("xml");
                foreach (KeyValuePair<string, string> dic in param) {
                    xml.WriteStartElement(dic.Key);
                    xml.WriteCData(dic.Value);
                    xml.WriteEndElement();
                }
                xml.WriteEndElement();
                xml.WriteEndDocument();
                xml.Flush();

                //byte[] xmlBytes = new byte[ms.Length];
                //ms.Read(xmlBytes, 0, (int)ms.Length);
                var xmlBytes = ms.ToArray();
                return Encoding.UTF8.GetString(xmlBytes);
            }
            catch (Exception) {
                Log.Error("orion", "转换XML失败");
            }
            finally {
                xml.Close();
                ms.Close();
            }
            return null;
        }

        #endregion

        #region 时间戳

        /// <summary>
        /// 自1970年1月1日 0点0分0秒以来的秒数。注意：部分系统取到的值为毫秒级，需要转换成秒(10位数字)
        /// </summary>
        /// <returns></returns>
        private string GenTimeStamp() {
            //throw new NotImplementedException();
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return ((int)(DateTime.Now - startTime).TotalSeconds).ToString();
        }

        /// <summary>  
        /// 时间戳转为C#格式时间  
        /// </summary>  
        /// <param name="timeStamp">Unix时间戳格式</param>  
        /// <returns>C#格式时间</returns>  
        public static DateTime ToTime(string timeStamp) {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        public static int ToInt(System.DateTime time) {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        #endregion

    }
}
