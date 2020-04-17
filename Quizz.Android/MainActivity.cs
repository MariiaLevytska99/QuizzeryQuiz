using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common;
using Xamarin.Facebook;
using Google;
using Plugin.GoogleClient;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Auth.Api;
using Android.Content;
using Quizz.ViewModel;

namespace Quizz.Droid
{
    [Activity(Label = "Quizz", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    { 


        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Lottie.Forms.Droid.AnimationViewRenderer.Init();
            GoogleClientManager.Initialize(this);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            LoadApplication(new App());
            IsPlayServicesAvailable();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {

                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    //msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }

                else
                {
                    // msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                // do whatever if play service is not available
                //msgText.Text = "Google Play Services is available.";
                return true;
            }
        }
    }
}