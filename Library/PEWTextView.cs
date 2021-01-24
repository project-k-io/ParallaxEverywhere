using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace com.fmsirvent.ParallaxEverywhere
{
    /**
 * Created by fmsirvent on 03/11/14.
 */
    public sealed class PEWTextView : TextView
    {
        private const string LogTag = "PEWTextView";
        private readonly ParallaxHelper _parallax;

        public PEWTextView(Context context) : base(context)
        {
            _parallax = new ParallaxHelper(this);
            if (!IsInEditMode) _parallax.ParallaxAnimation();
        }

        public PEWTextView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            _parallax = new ParallaxHelper(this);
            if (!IsInEditMode)
            {
                CheckAttributes(attrs);
                _parallax.ParallaxAnimation();
            }
        }

        public PEWTextView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            _parallax = new ParallaxHelper(this);
            if (!IsInEditMode)
            {
                CheckAttributes(attrs);
                _parallax.ParallaxAnimation();
            }
        }

        public PEWTextView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            _parallax = new ParallaxHelper(this);
        }

        public PEWTextView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            _parallax = new ParallaxHelper(this);
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            _parallax.OnAttachedToWindow();
        }

        protected override void OnDetachedFromWindow()
        {
            _parallax.OnDetachedFromWindow();
            base.OnDetachedFromWindow();
        }

        private void CheckAttributes(IAttributeSet attrs)
        {
            var arr = Context.ObtainStyledAttributes(attrs, Resource.Styleable.PEWAttrs);
            _parallax.CheckAttributes(arr, () => _parallax.CheckTextScale(arr));
        }


        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            _parallax.ApplyParallax();
        }
    }
}