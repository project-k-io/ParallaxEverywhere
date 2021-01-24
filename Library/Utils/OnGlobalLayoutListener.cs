using System;
using Android.Views;
using Object = Java.Lang.Object;

namespace com.fmsirvent.ParallaxEverywhere.Utils
{
    public class OnGlobalLayoutListener : Object, ViewTreeObserver.IOnGlobalLayoutListener
    {
        private readonly Action _onGlobalLayout;

        public OnGlobalLayoutListener(Action onGlobalLayout)
        {
            _onGlobalLayout = onGlobalLayout;
        }

        public void OnGlobalLayout()
        {
            _onGlobalLayout?.Invoke();
        }
    }
}