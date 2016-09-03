using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace RRExpress.Common {
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum e) {
            var desc = "";

            if (!string.IsNullOrWhiteSpace(desc))
                return desc;


            FieldInfo fi = e.GetType().GetTypeInfo()
                .GetDeclaredField(e.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return e.ToString();
        }
    }
}
