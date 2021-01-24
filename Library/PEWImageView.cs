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
    public class PEWImageView : ImageView
    {
        private const string LogTag = "ParallaxEverywhere";
        private readonly ParallaxHelper _parallax;

        public PEWImageView(Context context) : base(context)
        {
            _parallax = new ParallaxHelper(this);
            CheckScale();
        }

        public PEWImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            _parallax = new ParallaxHelper(this);
            if (!IsInEditMode)
                CheckAttributes(attrs);
            CheckScale();
        }

        public PEWImageView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            _parallax = new ParallaxHelper(this);
            if (!IsInEditMode)
                CheckAttributes(attrs);
            CheckScale();
        }

        public PEWImageView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            _parallax = new ParallaxHelper(this);
        }

        protected PEWImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
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
            _parallax.CheckAttributes(arr, () => CheckScale());
        }


        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            if (Drawable != null)
            {
                var dheight = Drawable.IntrinsicHeight;
                var dwidth = Drawable.IntrinsicWidth;
                var vheight = MeasuredHeight;
                var vwidth = MeasuredWidth;
                var s = GetScaleType();
                _parallax.OnImageMeasure(s, dheight, dwidth, vheight, vwidth);
            }

            _parallax.ApplyParallax();
        }

        private bool CheckScale()
        {
            var s = GetScaleType();
            return _parallax.CheckImageScale(s);
        }
    }
}