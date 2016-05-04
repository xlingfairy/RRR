using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Models {

    public class Grouped<T> : List<T> {

        public Grouped(IEnumerable<T> datas) {
            if (datas == null)
                return;

            this.AddRange(datas);
        }

        public string Title {
            get;
            set;
        }


        public string ShortTitle {
            get;
            set;
        }
    }

    public static class GroupHelper {
        public static IEnumerable<Grouped<T>> ToGroup<T>(this IEnumerable<T> source,
                Func<T, object> groupKey
            ) {

            var a = source.ToLookup(groupKey)
                .Select(l => new Grouped<T>(l) {
                    Title = l.Key.ToString(),
                    ShortTitle = l.Key.ToString()
                });

            return a;
        }
    }
}
