using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Essentials;
using Plugin.Permissions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Plugin.CurrentActivity;

namespace CustomRenderer.Droid
{
    //[Activity(Label = "CustomRenderer.Droid", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
    [Activity(Label = "C S Photo", Icon = "@drawable/icon1", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }

        string fld;

        //protected override void OnCreate(Bundle bundle)
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Instance = this;

            Xamarin.Essentials.Platform.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);


            //string[] permissions = new string[3];
            //permissions[1] = "android.permission.WRITE_EXTERNAL_STORAGE";
            //permissions[2] = "android.permission.ACCESS_COARSE_LOCATION";
            //permissions[3] = "android.permission.ACCESS_FINE_LOCATION";
            //OnRequestPermissionsResult(this, permissions,0);
            
            CrossCurrentActivity.Current.Activity = this;
           CrossCurrentActivity.Current.Init(this, bundle);


            //Check wherther we got all required permission
            //var status_location = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init(); //James

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

