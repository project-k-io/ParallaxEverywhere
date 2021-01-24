using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using com.fmsirvent.ParallaxEverywhere.Utils;

namespace com.fmsirvent.ParallaxEverywhere
{
    public class ParallaxHelper
    {
        private const string LogTag = "ParallaxHelper";
        private readonly View _view;
        private bool _blockParallaxX;
        private bool _blockParallaxY;

        private float _heightView;

        private IInterpolator _interpolator = new LinearInterpolator();
        private ViewTreeObserver.IOnDrawListener _onDrawListener;
        private ViewTreeObserver.IOnGlobalLayoutListener _onGlobalLayoutListener;
        private ViewTreeObserver.IOnScrollChangedListener _onScrollChangedListener;

        private bool _reverseX;
        private bool _reverseY;

        private int _screenHeight;
        private int _screenWidth;

        private float _scrollSpaceX;
        private float _scrollSpaceY;
        private bool _updateOnDraw;
        private float _widthView;

        public ParallaxHelper(View view)
        {
            _view = view;
        }


        public void OnAttachedToWindow()
        {
            _onScrollChangedListener = new OnScrollChangedListener(() => { ApplyParallax(); });

            _onGlobalLayoutListener = new OnGlobalLayoutListener(() =>
            {
                _heightView = _view.Height;
                _widthView = _view.Width;
                ApplyParallax();
            });


            var viewTreeObserver = _view.ViewTreeObserver;
            viewTreeObserver.AddOnScrollChangedListener(_onScrollChangedListener);
            viewTreeObserver.AddOnGlobalLayoutListener(_onGlobalLayoutListener);
            _onDrawListener = new OnDrawListener(() => { ApplyParallax(); });
            viewTreeObserver.AddOnDrawListener(_onDrawListener);

            ParallaxAnimation();
        }

        public void OnDetachedFromWindow()
        {
            var viewTreeObserver = _view.ViewTreeObserver;
            viewTreeObserver.RemoveOnScrollChangedListener(_onScrollChangedListener);
            viewTreeObserver.RemoveOnGlobalLayoutListener(_onGlobalLayoutListener);
            viewTreeObserver.RemoveOnDrawListener(_onDrawListener);
        }

        public void ParallaxAnimation()
        {
            InitSizeScreen();
            ApplyParallax();
        }
        private Point GetScreenSize()
        {
            var wm = _view.Context?.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
            var display = wm?.DefaultDisplay;
            if (display == null)
                return null;

            var size = new Point();
            display.GetSize(size);
            return size;
        }

        private void InitSizeScreen()
        {
            var size = GetScreenSize();
            Log.Debug(LogTag, $"Screen Size=[{size.X}, {size.Y}]");
            _screenHeight = size.Y;
            _screenWidth = size.X;
        }

        public void ApplyParallax()
        {
            var location = new int[2];
            _view.GetLocationOnScreen(location);

            if (_scrollSpaceY != 0 && !_blockParallaxY)
            {
                var locationY = (float) location[1];
                var locationUsableY = locationY + _heightView / 2;
                var scrollDeltaY = locationUsableY / _screenHeight;

                var interpolatedScrollDeltaY = _interpolator.GetInterpolation(scrollDeltaY);

                if (_reverseY)
                    SetMyScrollY((int) (Math.Min(Math.Max(0.5f - interpolatedScrollDeltaY, -0.5f), 0.5f) * -_scrollSpaceY));
                else
                    SetMyScrollY((int) (Math.Min(Math.Max(0.5f - interpolatedScrollDeltaY, -0.5f), 0.5f) * _scrollSpaceY));
            }
            else
            {
                SetMyScrollY(0);
            }

            if (_scrollSpaceX != 0 && !_blockParallaxX)
            {
                var locationX = (float) location[0];
                var locationUsableX = locationX + _widthView / 2;
                var scrollDeltaX = locationUsableX / _screenWidth;

                var interpolatedScrollDeltaX = _interpolator.GetInterpolation(scrollDeltaX);

                if (_reverseX)
                    SetMyScrollX((int) (Math.Min(Math.Max(0.5f - interpolatedScrollDeltaX, -0.5f), 0.5f) * -_scrollSpaceX));
                else
                    SetMyScrollX((int) (Math.Min(Math.Max(0.5f - interpolatedScrollDeltaX, -0.5f), 0.5f) * _scrollSpaceX));
            }
            else
            {
                SetMyScrollX(0);
            }
        }

        private void SetMyScrollX(int value)
        {
            _view.ScrollX = value;
        }

        private void SetMyScrollY(int value)
        {
            _view.ScrollY = value;
        }

        public void SetInterpolator(IInterpolator interpol)
        {
            _interpolator = interpol;
        }

        public bool IsReverseX()
        {
            return _reverseX;
        }

        public void SetReverseX(bool reverseX)
        {
            _reverseX = reverseX;
        }

        public bool IsReverseY()
        {
            return _reverseY;
        }

        public void SetReverseY(bool reverseY)
        {
            _reverseY = reverseY;
        }

        public bool IsBlockParallaxX()
        {
            return _blockParallaxX;
        }

        public void SetBlockParallaxX(bool blockParallaxX)
        {
            _blockParallaxX = blockParallaxX;
        }

        public bool IsBlockParallaxY()
        {
            return _blockParallaxY;
        }

        public void SetBlockParallaxY(bool blockParallaxY)
        {
            _blockParallaxY = blockParallaxY;
        }

        public void CheckAttributes(TypedArray arr, Action checkScale)
        {
            var reverse = arr.GetInt(Resource.Styleable.PEWAttrs_reverse, 1);
            _updateOnDraw = arr.GetBoolean(Resource.Styleable.PEWAttrs_update_onDraw, false);
            _blockParallaxX = arr.GetBoolean(Resource.Styleable.PEWAttrs_block_parallax_x, false);
            _blockParallaxY = arr.GetBoolean(Resource.Styleable.PEWAttrs_block_parallax_y, false);

            _reverseX = false;
            _reverseY = false;
            switch (reverse)
            {
                case AttrConstants.ReverseNone:
                    break;
                case AttrConstants.ReverseX:
                    _reverseX = true;
                    break;
                case AttrConstants.ReverseY:
                    _reverseY = true;
                    break;
                case AttrConstants.ReverseBoth:
                    _reverseX = true;
                    _reverseY = true;
                    break;
            }

            checkScale?.Invoke();

            var interpolationId = arr.GetInt(Resource.Styleable.PEWAttrs_interpolation, 0);

            _interpolator = InterpolatorSelector.InterpolatorId(interpolationId);

            arr.Recycle();
        }

        public void CheckTextScale(TypedArray arr)
        {
            _scrollSpaceX = arr.GetDimensionPixelSize(Resource.Styleable.PEWAttrs_parallax_x, 0);
            _scrollSpaceY = arr.GetDimensionPixelSize(Resource.Styleable.PEWAttrs_parallax_y, 0);
        }

        public bool CheckImageScale(ImageView.ScaleType s)
        {
            if (s == ImageView.ScaleType.Center ||
                s == ImageView.ScaleType.CenterCrop ||
                s == ImageView.ScaleType.CenterInside)
                return true;

            if (s == ImageView.ScaleType.FitCenter ||
                s == ImageView.ScaleType.FitEnd ||
                s == ImageView.ScaleType.FitStart ||
                s == ImageView.ScaleType.FitXy ||
                s == ImageView.ScaleType.Matrix)
                Log.Debug(LogTag, $"Scale type {s} unsupported");
            return false;
        }


        public void OnImageMeasure(ImageView.ScaleType s, int dheight, int dwidth, int vheight, int vwidth)
        {
            float scale;
            float dnewHeight = 0;
            float dnewWidth = 0;

            if (s == ImageView.ScaleType.Center ||
                s == ImageView.ScaleType.CenterCrop ||
                s == ImageView.ScaleType.CenterInside)
            {
                if (dwidth * vheight > vwidth * dheight)
                {
                    scale = vheight / (float) dheight;
                    dnewWidth = dwidth * scale;
                    dnewHeight = vheight;
                }
                else
                {
                    scale = vwidth / (float) dwidth;
                    dnewWidth = vwidth;
                    dnewHeight = dheight * scale;
                }
            }

            _scrollSpaceY = dnewHeight > vheight ? dnewHeight - vheight : 0;
            _scrollSpaceX = dnewWidth > vwidth ? dnewWidth - vwidth : 0;
        }
    }
}