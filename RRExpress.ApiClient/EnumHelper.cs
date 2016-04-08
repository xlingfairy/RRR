using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace RRExpress.ApiClient {
    public static class EnumHelper {


        /// <summary>
        /// 获取 DescriptionAttribute 中的 Description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TA"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TA GetAttribute<T, TA>(T value)
            where T : struct, IComparable, IFormattable
            where TA : Attribute {
            var type = typeof(T);

            var field = type.GetRuntimeField(value.ToString());
            return field.GetCustomAttributes(false).OfType<TA>().FirstOrDefault();
        }
    }
}
