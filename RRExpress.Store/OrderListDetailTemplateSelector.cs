using AsNum.XFControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RRExpress.Store {

    /// <summary>
    /// 订单列表页 单商品、多商品展示模板选择
    /// </summary>
    public class OrderListDetailTemplateSelector : DataTemplateSelector {

        public DataTemplate Single { get; set; }

        public DataTemplate Muti { get; set; }

        public WeakReference<object> Datas { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            var c = ((Repeater)container).ItemsSource?.Cast<object>().Count() ?? 0;
            return c > 1 ? this.Muti : this.Single;
        }
    }
}
