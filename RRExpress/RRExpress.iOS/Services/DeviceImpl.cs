using Foundation;
using RRExpress.iOS.Services;
using RRExpress.Services;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;


[assembly: Dependency(typeof(DeviceImpl))]
namespace RRExpress.iOS.Services {
    public class DeviceImpl : IDevice {

        public string GetDeviceID() {
            return UIDevice.CurrentDevice.IdentifierForVendor.AsString();
        }

        public string GetPhoneNumber() {
            //SBFormattedPhoneNumber
            return "";
        }
    }
}
