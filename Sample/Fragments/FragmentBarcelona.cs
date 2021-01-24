using Android.App;
using Android.OS;
using Android.Views;

namespace com.fmsirvent.ParallaxEverywhere.Sample.Fragments
{
    public class FragmentBarcelona : Android.Support.V4.App.Fragment
    {
        public static FragmentBarcelona NewInstance() => new FragmentBarcelona { Arguments = new Bundle() };


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.fragment_parallax_everywhere_barcelona, null);
        }
    }
}