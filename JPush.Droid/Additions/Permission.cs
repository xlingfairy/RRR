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
using static Android.Manifest;

[assembly: Permission(Name = "com.halogo.bt.permission.JPUSH_MESSAGE", ProtectionLevel = Android.Content.PM.Protection.Signature)]

[assembly: UsesPermission("com.halogo.bt.permission.JPUSH_MESSAGE")]

[assembly: UsesPermission("android.permission.RECEIVE_USER_PRESENT")]
[assembly: UsesPermission(Permission.Internet)]
[assembly: UsesPermission(Permission.WakeLock)]
[assembly: UsesPermission(Permission.ReadPhoneState)]
[assembly: UsesPermission(Permission.WriteExternalStorage)]
[assembly: UsesPermission(Permission.ReadExternalStorage)]
[assembly: UsesPermission(Permission.Vibrate)]
[assembly: UsesPermission(Permission.MountUnmountFilesystems)]
[assembly: UsesPermission(Permission.AccessNetworkState)]
[assembly: UsesPermission(Permission.WriteSettings)]

[assembly: UsesPermission(Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Permission.AccessWifiState)]
[assembly: UsesPermission(Permission.ChangeWifiState)]
[assembly: UsesPermission(Permission.AccessFineLocation)]
[assembly: UsesPermission(Permission.AccessLocationExtraCommands)]
[assembly: UsesPermission(Permission.ChangeNetworkState)]

namespace JPush.Droid.Additions {
}