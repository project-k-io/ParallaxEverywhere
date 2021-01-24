using Android.App;
using Android.OS;
using Android.Views;

namespace com.fmsirvent.ParallaxEverywhere.Sample.Fragments
{
    public class FragmentAlicante : Android.Support.V4.App.Fragment
    {
        public static FragmentAlicante NewInstance() => new FragmentAlicante { Arguments = new Bundle() };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.fragment_parallax_everywhere_alicante, null);
        }
    }
}