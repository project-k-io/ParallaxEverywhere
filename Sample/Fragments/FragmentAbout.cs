using Android.App;
using Android.OS;
using Android.Views;

namespace com.fmsirvent.ParallaxEverywhere.Sample.Fragments
{
    public class FragmentAbout : Android.Support.V4.App.Fragment
    {
        public static FragmentAbout NewInstance() => new FragmentAbout { Arguments = new Bundle() };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.activity_parallax_everywhere_about, null);
        }
    }
}