using System;
using Android.Views;
using Object = Java.Lang.Object;

namespace com.fmsirvent.ParallaxEverywhere.Utils
{
    public class OnDrawListener : Object, ViewTreeObserver.IOnDrawListener
    {
        private readonly Action _onDrawListener;

        public OnDrawListener(Action onDrawListener)
        {
            _onDrawListener = onDrawListener;
        }

        public void OnDraw()
        {
            _onDrawListener?.Invoke();
        }
    }
}