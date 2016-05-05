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
using Xamarin.Forms;
using Xamarin.Geolocation;
using System.Threading.Tasks;
using Android.Locations;
using RRExpress.Services;
using RRExpress.Droid.Services;

[assembly: Dependency(typeof(GeolocatorImpl))]
namespace RRExpress.Droid.Services {
    public class GeolocatorImpl : IGeolocator {

        public event EventHandler<LocationGetCallbackEventArgs> LoationGetCallback;

        private LocationManager LM = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);

        public async void GetLocationAsync() {

            Criteria criteria = new Criteria();
            criteria.Accuracy = Accuracy.Low;   //高精度    
            criteria.AltitudeRequired = false;    //不要求海拔    
            criteria.BearingRequired = false; //不要求方位    
            criteria.CostAllowed = false; //不允许有话费    
            criteria.PowerRequirement = Power.Low;   //低功耗  

            var provider = this.LM.GetBestProvider(criteria, true);

            var providers = this.LM.GetProviders(true);
            if (providers.Contains(LocationManager.GpsProvider)) {
                provider = LocationManager.GpsProvider;
            } else if (providers.Contains(LocationManager.NetworkProvider)) {
                provider = LocationManager.NetworkProvider;
            }

            var loc = this.LM.GetLastKnownLocation(provider);//LocationManager.GpsProvider
            if (loc != null) {
                using (var g = new Geocoder(Forms.Context)) {
                    var addrs = await g.GetFromLocationAsync(loc.Latitude, loc.Longitude, 1);
                    if (addrs != null && addrs.Count > 0) {
                        var addr = addrs.First();
                        if (this.LoationGetCallback != null) {
                            this.LoationGetCallback.Invoke(this, new LocationGetCallbackEventArgs() {
                                Lat = loc.Latitude,
                                Lnt = loc.Longitude,
                                Name = addr.Locality
                            });
                        }
                    }
                }
            }

            var listener = new LocationListener();
            listener.LocationChanged += Listener_LocationChanged;

            this.LM.RequestLocationUpdates(provider, 0, 0, listener);
        }

        private void Listener_LocationChanged(object sender, LocationChangedEventArgs e) {
            if (this.LoationGetCallback != null) {
                var loc = e.Location;
                this.LoationGetCallback.Invoke(this, new LocationGetCallbackEventArgs() {
                    Lat = loc.Latitude,
                    Lnt = loc.Longitude
                });
            }
            this.LM.RemoveUpdates((LocationListener)sender);
        }
    }
}