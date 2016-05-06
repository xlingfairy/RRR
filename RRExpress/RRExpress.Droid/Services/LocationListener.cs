using Android.Locations;
using Android.OS;
using Android.Runtime;
using System;

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