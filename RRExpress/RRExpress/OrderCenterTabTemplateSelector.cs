using AsNum.XFControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RRExpress {
    public class OrderCenterTabTemplateSelector : DataTemplateSelector {

        public DataTemplate Normal { get; set; }

        public DataTemplate Selected { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            var selectable = (ISelectable)item;
            if (selectable != null) {
                return selectable.IsSelected ? Selected : Normal;
            }
            return null;
        }
    }
}
