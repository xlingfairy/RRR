using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Caliburn.Micro;
using AsNum.XFControls.iOS;
using System.Threading.Tasks;

namespace RRExpress.iOS {
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {

        private readonly CaliburnAppDelegate appDelegate = new CaliburnAppDelegate();

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {

            Rg.Plugins.Popup.IOS.Popup.Init(); // Init Popup

            global::Xamarin.Forms.Forms.Init();
            this.LoadApplication(new App(IoC.Get<SimpleContainer>()));

            //https://developer.xamarin.com/recipes/ios/content_controls/navigation_controller/change_the_nav_bar_color/
            //导航栏颜色
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(0xde, 0x35, 0x35);
            //导航栏文字颜色
            UINavigationBar.Appearance.TintColor = UIColor.FromRGB(0xff, 0xff, 0xff);

            //标题
            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes() {
                ForegroundColor = UIColor.FromRGB(0xff, 0xff, 0xff)
            };

            //设置导航条颜色
            //UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
            //UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, true);

            //引用 AsNum.XFControls.iOS 程序集，要不然，会整个程序集会被优化掉
            FlipViewRender render = new FlipViewRender();

            return base.FinishedLaunching(app, options);
        }
    }
}
