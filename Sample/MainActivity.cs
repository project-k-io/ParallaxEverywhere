using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using com.fmsirvent.ParallaxEverywhere.Sample.Fragments;

namespace com.fmsirvent.ParallaxEverywhere.Sample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            // Load the first fragment on creation
            LoadFragment(Resource.Id.navigation_barcelona);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            return LoadFragment(item.ItemId);
        }

        bool LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.navigation_barcelona:
                    fragment = FragmentBarcelona.NewInstance();
                    break;
                case Resource.Id.navigation_alicante:
                    fragment = FragmentAlicante.NewInstance();
                    break;
                case Resource.Id.navigation_about:
                    fragment = FragmentAbout.NewInstance();
                    break;
            }

            if (fragment == null)
                return false;

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();

            return true;
        }
    }
}

