//package com.fmsirvent.ParallaxEverywhere.Utils;
//using Android.Views.Animations.AccelerateDecelerateInterpolator;
//using Android.Views.Animations.AccelerateInterpolator;
//using Android.Views.Animations.AnticipateInterpolator;
//using Android.Views.Animations.AnticipateOvershootInterpolator;
//using Android.Views.Animations.BounceInterpolator;
//using Android.Views.Animations.DecelerateInterpolator;
//using Android.Views.Animations.Interpolator;
//using Android.Views.Animations.LinearInterpolator;
//using Android.Views.Animations.OvershootInterpolator;

// using Android.Graphics;

using Android.Views.Animations;

namespace com.fmsirvent.ParallaxEverywhere.Utils
{
    /**
 * Created by fmsirvent on 06/11/14.
 */
    public class InterpolatorSelector
    {
        public const int Linear = 0;
        public const int AccelerateDecelerate = 1;
        public const int Accelerate = 2;
        public const int Anticipate = 3;
        public const int AnticipateOvershoot = 4;
        public const int Bounce = 5;
        public const int Decelerate = 6;
        public const int Overshoot = 7;

        public static IInterpolator InterpolatorId(int interpolationId)
        {
            switch (interpolationId)
            {
                case Linear:
                default:
                    return new LinearInterpolator();
                case AccelerateDecelerate:
                    return new AccelerateDecelerateInterpolator();
                case Accelerate:
                    return new AccelerateInterpolator();
                case Anticipate:
                    return new AnticipateInterpolator();
                case AnticipateOvershoot:
                    return new AnticipateOvershootInterpolator();
                case Bounce:
                    return new BounceInterpolator();
                case Decelerate:
                    return new DecelerateInterpolator();
                case Overshoot:
                    return new OvershootInterpolator();
                //TODO: this interpolations needs parameters
                //case CYCLE:
                //    return new CycleInterpolator();
                //case PATH:
                //    return new PathInterpolator();
            }
        }
    }
}