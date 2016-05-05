using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;

namespace RRExpress.Droid.Services {
    public class LocationListener : Java.Lang.Object, ILocationListener {

        public event EventHandler<LocationChangedEventArgs> LocationChanged = null;

        public void OnLocationChanged(Location location) {
            if (this.LocationChanged != null)
                this.LocationChanged.Invoke(this, new LocationChangedEventArgs(location));
        }

        public void OnProviderDisabled(string provider) {

        }

        public void OnProviderEnabled(string provider) {

        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras) {

        }

    }
}