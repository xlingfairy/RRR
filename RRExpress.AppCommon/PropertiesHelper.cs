using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RRExpress.AppCommon {


    /// <summary>
    /// XF 2.0 BUG 注意:
    /// https://forums.xamarin.com/discussion/comment/169577/#Comment_169577
    /// 
    /// 在Release 下,Linker 默认会把 System.Runtime.Serialization 给去除.
    /// 导致 Release 下, Properties 不能保存.
    /// 如果要修复这个问题,请在 Skip Linking 一栏中填写: System.Runtime.Serialization 
    /// </summary>
    public class PropertiesHelper {

        /// <summary>
        /// 从 Propertities 中获取 Key 的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key) {
            if (Application.Current.Properties.ContainsKey(key)) {
                if (typeof(T).GetTypeInfo().IsPrimitive || typeof(T).Equals(typeof(String))) {
                    return (T)Application.Current.Properties[key];
                }
                else {
                    return GetObject<T>(key);
                }
            }

            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<T>(string key, T value) {
            if (Application.Current.Properties.ContainsKey(key))
                Application.Current.Properties[key] = value;
            else
                Application.Current.Properties.Add(key, value);
        }

        /// <summary>
        /// 已 Json 格式保存值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetObject(string key, object value) {
            var json = JsonConvert.SerializeObject(value);
            Set(key, json);
        }

        /// <summary>
        /// 获取已json 格式保存的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetObject<T>(string key) {
            var json = Get<string>(key);
            if (!string.IsNullOrWhiteSpace(json))
                return JsonConvert.DeserializeObject<T>(json);
            return default(T);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key) {
            if (HasKey(key)) {
                Application.Current.Properties.Remove(key);
            }
        }

        /// <summary>
        /// 将 Propertities 的改动保存
        /// </summary>
        /// <returns></returns>
        public async static Task Save() {
            await Application.Current.SavePropertiesAsync();
        }

        /// <summary>
        /// 是否存在键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool HasKey(string key) {
            return Application.Current.Properties.ContainsKey(key);
        }
    }
}
