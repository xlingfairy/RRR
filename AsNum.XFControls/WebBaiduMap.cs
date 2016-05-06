using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class WebBaiduMap : View {
        public string AK {
            get; set;
        }

        public static readonly BindableProperty KeywordProperty =
            BindableProperty.Create(nameof(Keyword),
                typeof(string),
                typeof(WebBaiduMap),
                null,
                BindingMode.TwoWay,
                propertyChanged: KeywordChanged);

        private static void KeywordChanged(BindableObject bindable, object oldValue, object newValue) {

        }

        public string Keyword {
            get {
                return (string)this.GetValue(KeywordProperty);
            }
            set {
                this.SetValue(KeywordProperty, value);
            }
        }



        public static readonly BindableProperty SuggestionProperty =
            BindableProperty.Create(nameof(Suggestion),
                typeof(IEnumerable<SearchResultItem>),
                typeof(WebBaiduMap),
                null,
                BindingMode.Default);

        public IEnumerable<SearchResultItem> Suggestion {
            get {
                return (IEnumerable<SearchResultItem>)this.GetValue(SuggestionProperty);
            }
            set {
                this.SetValue(SuggestionProperty, value);
            }
        }


        public void SearchCallback(SearchResult result) {
            if (result == null)
                return;

            if (this.Keyword.Equals(result.Keyword, StringComparison.OrdinalIgnoreCase)) {
                this.Suggestion = result.Datas;
            }
        }







        public class SearchResult {
            public string Keyword { get; set; }
            public string City { get; set; }
            public IEnumerable<SearchResultItem> Datas { get; set; }
        }

        public class SearchResultItem {
            public string Title { get; set; }
            public string Addr { get; set; }
            public decimal Lat { get; set; }
            public decimal Lng { get; set; }
        }
    }
}
