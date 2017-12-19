using Android.App;
using Android.Content.PM;
using Android.OS;
using CrudSample.Droid.Services;
using CrudSample.Services;
using Prism.Unity;
using Microsoft.Practices.Unity;

namespace CrudSample.Droid
{
    [Activity(Label = "CrudSample", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IPlatformDb, PlatformDBAndroid>();
        }
    }
}

