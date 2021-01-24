using System;
using Android.Views;
using Object = Java.Lang.Object;

namespace com.fmsirvent.ParallaxEverywhere.Utils
{
    public class OnScrollChangedListener : Object, ViewTreeObserver.IOnScrollChangedListener
    {
        private readonly Action _onScrollChanged;

        public OnScrollChangedListener(Action onScrollChanged)
        {
            _onScrollChanged = onScrollChanged;
        }

        public void OnScrollChanged()
        {
            _onScrollChanged?.Invoke();
        }
    }
}