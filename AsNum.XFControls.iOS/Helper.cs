using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;

namespace AsNum.XFControls.iOS {
    public static class Helper {


        public static UIFont ToUIFont(this string fontfamilary, nfloat? fontSize = null) {
            try {
                return UIFont.FromName(fontfamilary, fontSize ?? UIFont.SystemFontSize);
            } catch {
                return UIFont.PreferredBody;
            }
        }
    }
}
