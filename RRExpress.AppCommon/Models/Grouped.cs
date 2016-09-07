using System;
using System.Collections.Generic;
using System.Linq;

namespace RRExpress.AppCommon.Models {

    /// <summary>
    /// ListView 分组数据模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Grouped<T> : List<T> {

        public Grouped(IEnumerable<T> datas, Func<T, object> sortKey = null) {
            if (datas == null)
                return;

            if (sortKey != null)
                datas = datas.OrderBy(sortKey);

            this.AddRange(datas);
        }

        /// <summary>
        /// 分组短标题
        /// </summary>
        public string Title {
            get;
            set;
        }


        /// <summary>
        /// 分组长标题
        /// </summary>
        public string ShortTitle {
            get;
            set;
        }
    }



    public static class GroupHelper {

        /// <summary>
        /// 将数据源转化为适用于 ListView 分组的数据模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="groupKey">分组依据</param>
        /// <returns></returns>
        public static IEnumerable<Grouped<T>> ToGroup<T>(this IEnumerable<T> source,
                Func<T, object> groupKey,
                Func<T, object> sortKey = null
            ) {

            var a = source.ToLookup(groupKey)
                .Select(l => new Grouped<T>(l, sortKey) {
                    Title = l.Key?.ToString(),
                    ShortTitle = l.Key?.ToString()
                });

            return a;
        }
    }
}
